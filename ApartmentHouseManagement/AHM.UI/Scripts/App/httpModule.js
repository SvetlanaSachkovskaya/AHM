app.factory('httpModule', ['$http', 'ngAuthSettings', '$timeout', 'usSpinnerService',
    function ($http, ngAuthSettings, $timeout, usSpinnerService) {
        'use strict';

        var serviceBase = ngAuthSettings.apiServiceBaseUri;

        var requestResult = {
            isSuccessful: true,
            errorMessage: ''
        };

        var processError = function (error) {
            requestResult.isSuccessful = false;
            requestResult.errorMessage = error.message;

            $timeout(function () {
                requestResult.isSuccessful = true;
                requestResult.errorMessage = '';
            }, 5000);
        }

        var get = function (url, parameter, callback) {
            usSpinnerService.spin('mainSpinner');
            $http.get(serviceBase + url, { params: parameter })
                .success(function (result) {
                    requestResult.isSuccessful = true;

                    if (callback) {
                        callback(result);
                    }
                })
                .error(processError)
                .finally(function () {
                    usSpinnerService.stop('mainSpinner');
                });
        }

        var post = function (url, parameter, callback) {
            usSpinnerService.spin('mainSpinner');
            $http.post(serviceBase + url, parameter)
                .success(function (result) {
                    requestResult.isSuccessful = true;

                    if (callback) {
                        callback(result);
                    }
                })
                .error(processError)
                .finally(function () {
                    usSpinnerService.stop('mainSpinner');
                });
        }


        var self = {};

        self.requestResult = requestResult;
        self.get = get;
        self.post = post;

        return self;
    }]);