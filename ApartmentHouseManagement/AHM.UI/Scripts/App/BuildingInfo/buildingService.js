app.factory('buildingService', ['httpModule', function (httpModule) {
    'use strict';

    var self = {};

    self.getApartments = function (callback) {
        httpModule.get('api/apartment/getAll', null, callback);
    };

    self.getApartmentById = function (id, callback) {
        httpModule.get('api/apartment/getById', { id: id }, callback);
    };

    self.addApartment = function (apartment, callback) {
        httpModule.post('api/apartment/add', apartment, callback);
    };

    self.updateApartment = function (apartment, callback) {
        httpModule.post('api/apartment/update', apartment, callback);
    };

    self.removeApartment = function (apartment, callback) {
        httpModule.post('api/apartment/remove', apartment, callback);
    };

    self.getLocations = function (callback) {
        httpModule.get('api/location/getAll', null, callback);
    };

    self.addLocation = function (location, callback) {
        httpModule.post('api/location/add', location, callback);
    };

    self.updateLocation = function (location, callback) {
        httpModule.post('api/location/update', location, callback);
    };

    self.removeLocation = function (location, callback) {
        httpModule.post('api/location/remove', location, callback);
    };

    self.getPackageTypes = function (callback) {
        httpModule.get('api/packageType/getAll', null, callback);
    };

    self.addPackageType = function (packageType, callback) {
        httpModule.post('api/packageType/add', packageType, callback);
    };

    self.updatePackageType = function (packageType, callback) {
        httpModule.post('api/packageType/update', packageType, callback);
    };

    self.removePackageType = function (packageType, callback) {
        httpModule.post('api/packageType/remove', packageType, callback);
    };

    self.getOccupants = function (callback) {
        httpModule.get('api/occupant/getAll', null, callback);
    };

    self.getOccupantById = function (id, callback) {
        httpModule.get('api/occupant/getById', {id: id}, callback);
    };

    self.getApartmentOwner = function (apartmentId, callback) {
        httpModule.get('api/occupant/getApartmentOwner', {apartmentId : apartmentId}, callback);
    };

    self.getOccupantsByApartmentId = function (apartmentId, callback) {
        httpModule.get('api/occupant/getByApartmentId', { apartmentId: apartmentId }, callback);
    };

    self.addOccupant = function (occupant, callback) {
        httpModule.post('api/occupant/add', occupant, callback);
    };

    self.updateOccupant = function (occupant, callback) {
        httpModule.post('api/occupant/update', occupant, callback);
    };

    self.removeOccupant = function (occupant, callback) {
        httpModule.post('api/occupant/remove', occupant, callback);
    };

    return self;
}]);