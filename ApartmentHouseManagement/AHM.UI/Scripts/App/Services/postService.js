app.factory('postService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {
    'use strict';

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var getPackages = function () {

        return $http.get(serviceBase + 'api/post/getAll').then(function (results) {
            return results;
        });
    };

    var createPackage = function (pack) {
        return $http.post(serviceBase + 'api/post/add', pack).then(function (result) {
            return result;
        });
    }

    var updatePackage = function (pack) {
        return $http.post(serviceBase + 'api/post/update', pack).then(function (result) {
            return result;
        });
    }

    var getPackageById = function (id) {
        return $http.get(serviceBase + 'api/post/getById', {params: {id : id}}).then(function (result) {
            return result;
        });
    }

    var self = {};

    self.getPackages = getPackages;
    self.createPackage = createPackage;
    self.updatePackage = updatePackage;
    self.getPackageById = getPackageById;

    return self;
}]);