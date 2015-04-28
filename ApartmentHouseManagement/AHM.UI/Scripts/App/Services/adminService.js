app.factory('adminService', ['httpModule', function (httpModule) {
    'use strict';

    var getAllBuildings = function (callback) {
        httpModule.get('api/building/getAllBuildings', null, callback);
    };

    var getBuildingById = function (id, callback) {
        httpModule.get('api/building/getBuildingById', { id: id }, callback);
    };

    var addBuilding = function (building, callback) {
        httpModule.post('api/building/addBuilding', building, callback);
    }

    var updateBuilding = function (building, callback) {
        httpModule.post('api/building/updateBuilding', building, callback);
    }

    var getUsersByBuildingId = function (id, callback) {
        httpModule.get('api/account/getUsersByBuildingId', id, callback);
    }

    var getUserById = function (id, callback) {
        httpModule.get('api/account/getUserById', id, callback);
    }

    var registerUser = function (user, callback) {
        httpModule.post('api/account/registerUser', user, callback);
    }

    var updateUser = function (user, callback) {
        httpModule.post('api/account/updateUser', user, callback);
    }

    var getRoles = function (callback) {
        httpModule.get('api/account/getRoles', null, callback);
    }

    var self = {};

    self.getAllBuildings = getAllBuildings;
    self.getBuildingById = getBuildingById;
    self.addBuilding = addBuilding;
    self.updateBuilding = updateBuilding;
    self.getUsersByBuildingId = getUsersByBuildingId;
    self.getUserById = getUserById;
    self.registerUser = registerUser;
    self.updateUser = updateUser;
    self.getRoles = getRoles;

    return self;

}]);