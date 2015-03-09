app.controller('instructionsController', ['$scope', '$state', 'instructionsService', function ($scope, $state, instructionsService) {
    'use strict';

    $scope.instructions = [];

    $scope.completeInstruction = function (instruction) {
        instruction.isClosed = true;
        instructionsService.updateInstruction(instruction).then(function () {
            $scope.instructions.splice($scope.instructions.indexOf(instruction), 1);
        },
        function (error) {
            alert(error);
        });
    }

    $scope.createInstruction = function () {
        $state.go('landing.createInstruction');
    }

    $scope.edit = function (instruction) {
        $state.go('landing.editInstruction', {instructionId : instruction.id});
    }

    instructionsService.getInstructions().then(function (results) {
        $scope.instructions = results.data;
    }, function (error) {
        alert(error.data.message);
    });
}]);