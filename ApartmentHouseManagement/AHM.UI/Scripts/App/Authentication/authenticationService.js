app.factory('authenticationService', ['$http', 'httpModule', '$q', 'localStorageService', 'ngAuthSettings', 'usSpinnerService',
    function ($http, httpModule, $q, localStorageService, ngAuthSettings, usSpinnerService) {
        'use strict';

        var serviceBase = ngAuthSettings.apiServiceBaseUri;

        var self = {};

        self.authentication = {
            isAuthenticated: false,
            role: '',
            buildingName: '',
            name: ""
        };

        self.login = function (loginData, callback, errorCallback) {
            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

            usSpinnerService.spin('mainSpinner');
            $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
                self.authentication.isAuthenticated = true;

                httpModule.get('api/account/getByUsername', { username: loginData.userName }, function (user) {
                    self.authentication.name = user.firstName + ' ' + user.lastName;
                    self.authentication.role = user.role;
                    self.authentication.buildingName = user.buildingName;

                    localStorageService.set('authorizationData',
                    {
                        token: response.access_token,
                        name: self.authentication.name,
                        role: self.authentication.role,
                        buildingName: self.authentication.buildingName
                    });

                    if (callback) {
                        callback();
                    }
                });
            }).error(function (error) {
                if (errorCallback) {
                    errorCallback(error.error_description);
                }
            }).finally(function() {
                usSpinnerService.stop('mainSpinner');
            });
        };

        self.logOut = function () {
            localStorageService.remove('authorizationData');

            self.authentication.isAuthenticated = false;
            self.authentication.userName = "";
            self.authentication.buildingName = '';
            self.authentication.role = null;
        };

        self.fillAuthenticationData = function () {
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                self.authentication.isAuthenticated = true;
                self.authentication.name = authData.name;
                self.authentication.role = authData.role;
                self.authentication.buildingName = authData.buildingName;
            }
        };

        self.hasPermissions = function (allowedRoles) {
            return !allowedRoles || allowedRoles.indexOf(self.authentication.role) !== -1;
        };

        return self;
    }
]);