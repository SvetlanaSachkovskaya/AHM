app.factory('instructionsService', ['httpModule', function (httpModule) {
    'use strict';

    var getInstructions = function (callback) {
        httpModule.get('api/instructions/getAll', null, callback);
    };

    var getOpenInstructions = function (callback) {
        httpModule.get('api/instructions/getAllOpen', null, callback);
    };

    var getPriorities = function (callback) {
        httpModule.get('api/instructions/getPriorities', null, callback);
    };

    var createInstruction = function (instruction, callback) {
        httpModule.post('api/instructions/add', instruction, callback);
    }

    var updateInstruction = function (instruction, callback) {
        httpModule.post('api/instructions/update', instruction, callback);
    }

    var getInstructionById = function (id, callback) {
        httpModule.get('api/instructions/getById', { id: id }, callback);
    }

    var self = {};
    self.getInstructions = getInstructions;
    self.createInstruction = createInstruction;
    self.updateInstruction = updateInstruction;
    self.getInstructionById = getInstructionById;
    self.getPriorities = getPriorities;
    self.getOpenInstructions = getOpenInstructions;

    return self;
}]);