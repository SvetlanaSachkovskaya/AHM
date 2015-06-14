app.factory('adminService', ['httpModule', function (httpModule) {
    'use strict';

    var self = {};

    self.getAllBuildings = function (callback) {
        httpModule.get('api/building/getAllBuildings', null, callback);
    };

    self.getBuildingById = function (id, callback) {
        httpModule.get('api/building/getBuildingById', { id: id }, callback);
    };

    self.addBuilding = function (building, callback) {
        httpModule.post('api/building/addBuilding', building, callback);
    }

    self.updateBuilding = function (building, callback) {
        httpModule.post('api/building/updateBuilding', building, callback);
    }

    self.getAllUsers = function (callback) {
        httpModule.get('api/account/getAllUsers', null, callback);
    }

    self.getUserById = function (id, callback) {
        httpModule.get('api/account/getUserById', { id: id }, callback);
    }

    self.getCurrentBuilding = function (callback) {
        httpModule.get('api/building/getCurrentBuilding', null, callback);
    }

    self.registerUser = function (user, callback) {
        httpModule.post('api/account/registerUser', user, callback);
    }

    self.updateUser = function (user, callback) {
        httpModule.post('api/account/updateUser', user, callback);
    }

    self.lockUser = function (user, callback) {
        httpModule.post('api/account/lockUser', user, callback);
    }

    self.unlockUser = function (user, callback) {
        httpModule.post('api/account/unlockUser', user, callback);
    }

    self.getRoles = function (callback) {
        httpModule.get('api/account/getRoles', null, callback);
    }

    return self;
}]);