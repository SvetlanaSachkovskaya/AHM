app.factory('instructionsService', ['httpModule', function (httpModule) {
    'use strict';

    var getInstructionsByDate = function (date, showCompleted, callback) {
        if (showCompleted) {
            httpModule.get('api/instructions/getAllInstructionsByDate', { date: date }, callback);
        } else {
            httpModule.get('api/instructions/getAllOpenInstructionsByDate', { date: date }, callback);
        }
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

    var removeInstruction = function (instruction, callback) {
        httpModule.post('api/instructions/remove', instruction, callback);
    }

    var getInstructionById = function (id, callback) {
        httpModule.get('api/instructions/getById', { id: id }, callback);
    }

    var self = {};
    self.getInstructionsByDate = getInstructionsByDate;
    self.createInstruction = createInstruction;
    self.updateInstruction = updateInstruction;
    self.getInstructionById = getInstructionById;
    self.getPriorities = getPriorities;
    self.removeInstruction = removeInstruction;

    return self;
}]);