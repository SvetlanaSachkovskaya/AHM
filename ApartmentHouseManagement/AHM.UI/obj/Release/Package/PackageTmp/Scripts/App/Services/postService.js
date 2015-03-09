'use strict';
app.factory('postService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var getPackages = function () {

        return $http.get(serviceBase + 'api/post/packages').then(function (results) {
            return results;
        });
    };

    var createPackage = function (pack) {
        return $http.post(serviceBase + 'api/post/createPackage', pack).then(function (result) {
            //return results;
        });
    }

    var updatePackage = function (pack) {
        return $http.post(serviceBase + 'api/post/updatePackage', pack).then(function (result) {
            //return results;
        });
    }

    var getPackageById = function (id) {
        return $http.post(serviceBase + 'api/post/getPackageById', id).then(function (result) {
            return result;
        });
    }

    var getPackageTypes = function () {

        return $http.get(serviceBase + 'api/post/getPackageTypes').then(function (results) {
            return results;
        });
    };

    var addPackageType = function (packageType) {

        return $http.get(serviceBase + 'api/post/addPackageType', packageType).then(function (result) {
            return result;
        });
    };

    var updatePackageType = function (packageType) {

        return $http.get(serviceBase + 'api/post/updatePackageType', packageType).then(function (result) {
            return result;
        });
    };

    var getLocations = function () {

        return $http.get(serviceBase + 'api/post/getPackageTypes').then(function (results) {
            return results;
        });
    };

    var addLocation = function (packageType) {

        return $http.get(serviceBase + 'api/post/addPackageType', packageType).then(function (result) {
            return result;
        });
    };

    var updateLocation = function (packageType) {

        return $http.get(serviceBase + 'api/post/updatePackageType', packageType).then(function (result) {
            return result;
        });
    };

    var self = {};
    self.getPackages = getPackages;
    self.createPackage = createPackage;
    self.updatePackage = updatePackage;
    self.getPackageById = getPackageById;

    self.getPackageTypes = getPackageTypes;
    self.addPackageType = addPackageType;
    self.updatePackageType = updatePackageType;

    self.getLocations = getLocations;
    self.addLocation = addLocation;
    self.updateLocation = updateLocation;

    return self;

}]);