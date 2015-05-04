app.factory('authenticationService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {
    'use strict';

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var authentication = {
        isAuthenticated: false,
        role: '',
        buildingName: '',
        name: ""
    };

    var saveRegistration = function (registration) {
        logOut();

        return $http.post(serviceBase + 'api/account/register', registration).then(function (response) {
            return response;
        });
    };

    var login = function (loginData) {
        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
            authentication.isAuthenticated = true;

            $http.get(serviceBase + 'api/account/getByUsername', { params: { username: loginData.userName } }).success(function (user) {
                authentication.name = user.firstName + ' ' + user.lastName;
                authentication.role = user.role;
                authentication.buildingName = user.buildingName;

                localStorageService.set('authorizationData',
                    {
                        token: response.access_token,
                        name: authentication.name,
                        role: authentication.role,
                        buildingName: authentication.buildingName
                    });

                deferred.resolve(response);
            });
        }).error(function (error) {
            logOut();
            deferred.reject(error);
        });

        return deferred.promise;
    };

    var logOut = function () {
        localStorageService.remove('authorizationData');

        authentication.isAuthenticated = false;
        authentication.userName = "";
        authentication.buildingName = '';
        authentication.role = null;
    };

    var fillAuthenticationData = function () {
        var authData = localStorageService.get('authorizationData');
        if (authData) {
            authentication.isAuthenticated = true;
            authentication.name = authData.name;
            authentication.role = authData.role;
            authentication.buildingName = authData.buildingName;
            authentication.useRefreshTokens = authData.useRefreshTokens;
        }
    };

    var hasPermissions = function (allowedRoles) {
        return !allowedRoles || allowedRoles.indexOf(authentication.role) !== -1;
    }

    var self = {};

    self.saveRegistration = saveRegistration;
    self.login = login;
    self.logOut = logOut;
    self.fillAuthenticationData = fillAuthenticationData;
    self.authentication = authentication;
    self.hasPermissions = hasPermissions;

    return self;
}]);