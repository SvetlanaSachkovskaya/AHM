app.controller('instructionsController', ['$scope', '$state', 'instructionsService', 'datePickerSettings', 'pages',
    function ($scope, $state, instructionsService, datePickerSettings, pages) {
        'use strict';

        $scope.instructions = [];

        $scope.filterDate = new Date();
        $scope.showCompleted = false;

        $scope.datePickerSettings = datePickerSettings;

        $scope.filter = function () {
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
            $state.go(pages.createInstruction);
        }

        $scope.edit = function (instructionId) {
            $state.go(pages.editInstruction, { instructionId: instructionId });
        }

        $scope.filter();
    }
]);