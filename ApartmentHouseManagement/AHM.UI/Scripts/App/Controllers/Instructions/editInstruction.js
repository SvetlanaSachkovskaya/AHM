app.controller('editInstructionController', ['$scope', '$state', '$stateParams', 'instructionsService',
    function ($scope, $state, $stateParams, instructionsService) {
        'use strict';

        $scope.instruction = {};
        $scope.priorities = [];

        $scope.datePickerSettings = {
            open: function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                $scope.opened = true;
            }
        }

        $scope.saveInstruction = function () {
            instructionsService.updateInstruction($scope.instruction, function () {
                $state.go('landing.instructions');
            });
        }

        $scope.cancel = function () {
            $state.go('landing.instructions');
        }

        if ($stateParams.instructionId) {
            instructionsService.getInstructionById($stateParams.instructionId, function (data) {
                $scope.instruction = data;
            });
        }

        instructionsService.getPriorities(function (data) {
            $scope.priorities = data;
        });
    }]);