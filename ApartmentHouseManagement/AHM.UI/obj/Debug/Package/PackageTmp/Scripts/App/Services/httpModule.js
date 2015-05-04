app.factory('httpModule', ['$http', 'ngAuthSettings', '$timeout', function ($http, ngAuthSettings, $timeout) {
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
        $http.get(serviceBase + url, { params: parameter })
            .success(function (result) {
                requestResult.isSuccessful = true;

                if (callback) {
                    callback(result);
                }
            })
            .error(processError);
    }

    var post = function (url, parameter, callback) {
        $http.post(serviceBase + url, parameter)
            .success(function (result) {
                requestResult.isSuccessful = true;

                if (callback) {
                    callback(result);
                }
            })
            .error(processError);
    }


    var self = {};

    self.requestResult = requestResult;
    self.get = get;
    self.post = post;

    return self;
}]);