app.factory('instructionsService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {
    'use strict';

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var getInstructions = function () {
        return $http.get(serviceBase + 'api/instructions/getAll').then(function (results) {
            return results;
        });
    };

    var getPriorities = function () {
        return $http.get(serviceBase + 'api/instructions/getPriorities').then(function (results) {
            return results;
        });
    };

    var createInstruction = function (instruction) {
        return $http.post(serviceBase + 'api/instructions/add', instruction).then(function (result) {
            return result;
        });
    }

    var updateInstruction = function (instruction) {
        return $http.post(serviceBase + 'api/instructions/update', instruction).then(function (result) {
            return result;
        });
    }

    var getInstructionById = function (id) {
        return $http.get(serviceBase + 'api/instructions/getById', { params: { id: id } }).then(function (result) {
            return result;
        });
    }

    var self = {};
    self.getInstructions = getInstructions;
    self.createInstruction = createInstruction;
    self.updateInstruction = updateInstruction;
    self.getInstructionById = getInstructionById;
    self.getPriorities = getPriorities;

    return self;
}]);