app.factory('instructionsService', ['httpModule', function (httpModule) {
    'use strict';

    var self = {};

    self.getInstructionsByDate = function (date, showCompleted, callback) {
        if (showCompleted) {
            httpModule.get('api/instructions/getAllInstructionsByDate', { date: date }, callback);
        } else {
            httpModule.get('api/instructions/getAllOpenInstructionsByDate', { date: date }, callback);
        }
    };

    self.getPriorities = function (callback) {
        httpModule.get('api/instructions/getPriorities', null, callback);
    };

    self.createInstruction = function (instruction, callback) {
        httpModule.post('api/instructions/add', instruction, callback);
    }

    self.updateInstruction = function (instruction, callback) {
        httpModule.post('api/instructions/update', instruction, callback);
    }

    self.removeInstruction = function (instruction, callback) {
        httpModule.post('api/instructions/remove', instruction, callback);
    }

    self.getInstructionById = function (id, callback) {
        httpModule.get('api/instructions/getById', { id: id }, callback);
    }

    return self;
}]);