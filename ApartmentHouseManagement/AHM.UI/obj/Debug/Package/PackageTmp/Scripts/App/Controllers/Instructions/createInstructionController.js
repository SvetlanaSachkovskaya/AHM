app.controller('createInstructionController', ['$scope', '$state', 'instructionsService', function ($scope, $state, instructionsService) {
    'use strict';

    function forceRequiredValidation() {
        if ($scope.instructionForm.$error.required) {
            $scope.instructionForm.$error.required.forEach(function (element) {
                element.$setDirty();
            });
        }
    }

    $scope.instruction = {
        title: '',
        content: '',
        executionDate: new Date(),
        priority: 0,
        isClosed: false
    };

    $scope.datePickerSettings = {
        open: function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.opened = true;
        }
    }

    $scope.priorities = [];

    $scope.createInstruction = function () {
        forceRequiredValidation();

        if ($scope.instructionForm.$valid) {
            instructionsService.createInstruction($scope.instruction, function () {
                $state.go('landing.instructions');
            });
        }
    }

    instructionsService.getPriorities(function (data) {
        $scope.priorities = data;
    });
}]);