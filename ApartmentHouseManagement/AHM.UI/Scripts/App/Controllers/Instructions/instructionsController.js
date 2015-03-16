app.controller('instructionsController', ['$scope', '$state', 'instructionsService', function ($scope, $state, instructionsService) {
    'use strict';

    $scope.instructions = [];

    $scope.orderByOptions = [{ id: 'executionDate', name: 'Date' }, { id: '-priority', name: 'Priority' }];
    $scope.orderBy = {
        value: $scope.orderByOptions[0].id
    };
    

    $scope.closeInstruction = function (instruction) {
        instruction.isClosed = true;
        instructionsService.updateInstruction(instruction, function () {
            $scope.instructions.splice($scope.instructions.indexOf(instruction), 1);
        });
    }

    $scope.createInstruction = function () {
        $state.go('landing.createInstruction');
    }

    $scope.edit = function (instructionId) {
        $state.go('landing.editInstruction', {instructionId : instructionId});
    }

    instructionsService.getOpenInstructions (function (data) {
        $scope.instructions = data;
    });
}]);