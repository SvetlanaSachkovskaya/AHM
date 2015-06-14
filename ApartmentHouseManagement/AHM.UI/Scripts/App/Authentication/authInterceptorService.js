app.factory('authInterceptorService', ['$q', '$location', 'localStorageService',
    function ($q, $location, localStorageService) {
        'use strict';

        var self = {};

        self.request = function (config) {
            config.headers = config.headers || {};

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }

            return config;
        }

        self.responseError = function (rejection) {
            if (rejection.status === 401) {
                var authData = localStorageService.get('authorizationData');

                if (authData) {
                    if (authData.useRefreshTokens) {
                        //$state.go('refresh');
                        return $q.reject(rejection);
                    }
                }
                //authenticationService.logOut();
                $location.path('/login');
            }
            return $q.reject(rejection);
        }

        return self;
    }
]);