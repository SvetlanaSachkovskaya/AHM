app.factory('buildingService', ['httpModule', function (httpModule) {
    'use strict';

    var getApartments = function (callback) {
        httpModule.get('api/apartment/getAll', null, callback);
    };

    var getApartmentById = function (id, callback) {
        httpModule.get('api/apartment/getById', { id: id }, callback);
    };

    var addApartment = function (apartment, callback) {
        httpModule.post('api/apartment/add', apartment, callback);
    };

    var updateApartment = function (apartment, callback) {
        httpModule.post('api/apartment/update', apartment, callback);
    };

    var removeApartment = function (apartment, callback) {
        httpModule.post('api/apartment/remove', apartment, callback);
    };

    var getLocations = function (callback) {
        httpModule.get('api/location/getAll', null, callback);
    };

    var addLocation = function (location, callback) {
        httpModule.post('api/location/add', location, callback);
    };

    var updateLocation = function (location, callback) {
        httpModule.post('api/location/update', location, callback);
    };

    var removeLocation = function (location, callback) {
        httpModule.post('api/location/remove', location, callback);
    };

    var getPackageTypes = function (callback) {
        httpModule.get('api/packageType/getAll', null, callback);
    };

    var addPackageType = function (packageType, callback) {
        httpModule.post('api/packageType/add', packageType, callback);
    };

    var updatePackageType = function (packageType, callback) {
        httpModule.post('api/packageType/update', packageType, callback);
    };

    var removePackageType = function (packageType, callback) {
        httpModule.post('api/packageType/remove', packageType, callback);
    };

    var getOccupants = function (callback) {
        httpModule.get('api/occupant/getAll', null, callback);
    };

    var getOccupantById = function (id, callback) {
        httpModule.get('api/occupant/getById', {id: id}, callback);
    };

    var getApartmentOwner = function (apartmentId, callback) {
        httpModule.get('api/occupant/getApartmentOwner', {apartmentId : apartmentId}, callback);
    };

    var getOccupantsByApartmentId = function (apartmentId, callback) {
        httpModule.get('api/occupant/getByApartmentId', { apartmentId: apartmentId }, callback);
    };

    var addOccupant = function (occupant, callback) {
        httpModule.post('api/occupant/add', occupant, callback);
    };

    var updateOccupant = function (occupant, callback) {
        httpModule.post('api/occupant/update', occupant, callback);
    };

    var removeOccupant = function (occupant, callback) {
        httpModule.post('api/occupant/remove', occupant, callback);
    };

    var self = {};
    self.getApartments = getApartments;
    self.getApartmentById = getApartmentById;
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
    self.getOccupantById = getOccupantById;
    self.getApartmentOwner = getApartmentOwner;
    self.getOccupantsByApartmentId = getOccupantsByApartmentId;

    return self;
}]);