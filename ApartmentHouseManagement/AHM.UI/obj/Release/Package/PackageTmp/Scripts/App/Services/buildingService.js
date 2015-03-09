'use strict';
app.factory('buildingService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var getApartments = function () {

        return $http.get(serviceBase + 'api/apartment/getAll').then(function (results) {
            return results;
        });
    };

    var addApartment = function (apartment) {

        return $http.get(serviceBase + 'api/apartment/add', apartment).then(function (result) {
            return result;
        });
    };

    var updateApartment = function (apartment) {

        return $http.get(serviceBase + 'api/apartment/update', apartment).then(function (result) {
            return result;
        });
    };

    var removeApartment = function (apartment) {

        return $http.get(serviceBase + 'api/apartment/remove', apartment).then(function (result) {
            return result;
        });
    };

    var getLocations = function () {

        return $http.get(serviceBase + 'api/location/getAll').then(function (results) {
            return results;
        });
    };

    var addLocation = function (location) {

        return $http.get(serviceBase + 'api/location/add', location).then(function (result) {
            return result;
        });
    };

    var updateLocation = function (location) {

        return $http.get(serviceBase + 'api/location/update', location).then(function (result) {
            return result;
        });
    };
        
    var removeLocation = function (location) {

        return $http.get(serviceBase + 'api/location/remove', location).then(function (result) {
            return result;
        });
    };

    var getPackageTypes = function () {

        return $http.get(serviceBase + 'api/packageType/getAll').then(function (results) {
            return results;
        });
    };

    var addPackageType = function (packageType) {

        return $http.get(serviceBase + 'api/packageType/add', packageType).then(function (result) {
            return result;
        });
    };

    var updatePackageType = function (packageType) {

        return $http.get(serviceBase + 'api/packageType/update', packageType).then(function (result) {
            return result;
        });
    };

    var removePackageType = function (packageType) {

        return $http.get(serviceBase + 'api/packageType/remove', packageType).then(function (result) {
            return result;
        });
    };

    var getOccupants = function () {

        return $http.get(serviceBase + 'api/occupant/getAll').then(function (results) {
            return results;
        });
    };

    var addOccupant = function (occupant) {

        return $http.get(serviceBase + 'api/occupant/add', occupant).then(function (result) {
            return result;
        });
    };

    var updateOccupant = function (occupant) {

        return $http.get(serviceBase + 'api/occupant/update', occupant).then(function (result) {
            return result;
        });
    };

    var removeOccupant = function (occupant) {

        return $http.get(serviceBase + 'api/occupant/remove', occupant).then(function (result) {
            return result;
        });
    };

    var self = {};
    self.getApartments = getApartments;
    self.addApartment = addApartment;
    self.updateApartment = updateApartment;
    self.removeApartment = removeApartment;

    self.getLocations = getLocations;
    self.addLocation = addLocation;
    self.updateLocation = updateLocation;
    self.removeLocation = removeLocation;

    self.getPackageTypes = getPackageTypes;
    self.addPackageType = addPackageType;
    self.updatePackageType = updatePackageType;
    self.removePackageType = removePackageType;

    self.getOccupants = getOccupants;
    self.addOccupant = addOccupant;
    self.updateOccupant = updateOccupant;
    self.removeOccupant = removeOccupant;

    return self;

}]);