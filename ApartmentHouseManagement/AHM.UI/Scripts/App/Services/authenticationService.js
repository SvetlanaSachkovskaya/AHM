app.factory('authenticationService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {
    'use strict';

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var authentication = {
        isAuthenticated: false,
        role: '',
        buildingName: '',
        userName: ""
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
            authentication.userName = loginData.userName;

            $http.get(serviceBase + 'api/account/getByUsername', { params: { username: loginData.userName } }).success(function (user) {
                authentication.role = user.role;
                authentication.buildingName = user.building.name;

                localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, role: user.role, buildingName: user.building.name });

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
            authentication.userName = authData.userName;
            authentication.role = authData.role;
            authentication.buildingName = authData.buildingName;
            authentication.useRefreshTokens = authData.useRefreshTokens;
        }
    };

    var self = {};

    self.saveRegistration = saveRegistration;
    self.login = login;
    self.logOut = logOut;
    self.fillAuthenticationData = fillAuthenticationData;
    self.authentication = authentication;

    return self;
}]);