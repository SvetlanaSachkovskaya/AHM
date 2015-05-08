app.controller('instructionsController', ['$scope', '$state', 'instructionsService', function ($scope, $state, instructionsService) {
    'use strict';

    $scope.instructions = [];

    $scope.filterDate = new Date();
    $scope.showCompleted = false;

    $scope.openDatePicker = function($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.opened = true;
    };

    $scope.filter = function() {
        instructionsService.getInstructionsByDate($scope.filterDate, $scope.showCompleted, function (data) {
            $scope.instructions = data;
        });
    }

    $scope.closeInstruction = function (instruction) {
        if (confirm('Are you sure you want to close this instruction?')) {
            instruction.isClosed = true;
            instructionsService.updateInstruction(instruction, function () {
                $scope.instructions.splice($scope.instructions.indexOf(instruction), 1);
            });
        }
    }

    $scope.removeInstruction = function (instruction) {
        if (confirm('Are you sure you want to remove this instruction?')) {
            instructionsService.removeInstruction(instruction, function () {
                $scope.instructions.splice($scope.instructions.indexOf(instruction), 1);
            });
        }
    }

    $scope.createInstruction = function () {
        $state.go('landing.createInstruction');
    }

    $scope.edit = function (instructionId) {
        $state.go('landing.editInstruction', {instructionId : instructionId});
    }

    $scope.filter();
}]);