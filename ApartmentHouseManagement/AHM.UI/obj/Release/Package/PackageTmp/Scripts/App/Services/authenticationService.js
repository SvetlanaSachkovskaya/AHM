'use strict';
app.factory('authenticationService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var authentication = {
        isAuthenticated: false,
        role: '',
        building: null,
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

        if (loginData.useRefreshTokens) {
            data = data + "&client_id=" + ngAuthSettings.clientId;
        }

        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

            localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName});
                
            authentication.isAuthenticated = true;
            authentication.userName = loginData.userName;

            $http.get(serviceBase + 'api/account/getByUsername', {params: { username: loginData.userName } } ).success(function (user) {
                authentication.role = user.Role;
                authentication.building = user.Building;
            });

            deferred.resolve(response);

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
    };

    var fillAuthenticationData = function () {
        var authData = localStorageService.get('authorizationData');
        if (authData) {
            authentication.isAuthenticated = true;
            authentication.userName = authData.userName;
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