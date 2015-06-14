app.controller('editInstructionController', ['$scope', '$state', '$stateParams', 'instructionsService', 'datePickerSettings', 'pages',
    function ($scope, $state, $stateParams, instructionsService, datePickerSettings, pages) {
        'use strict';

        function init() {
            if ($stateParams.instructionId) {
                instructionsService.getInstructionById($stateParams.instructionId, function (data) {
                    $scope.instruction = data;
                });
            }

            instructionsService.getPriorities(function (data) {
                $scope.priorities = data;
            });
        }

        $scope.instruction = {};
        $scope.priorities = [];

        $scope.datePickerSettings = datePickerSettings;

        $scope.saveInstruction = function () {
            instructionsService.updateInstruction($scope.instruction, function () {
                $state.go(pages.instructions);
            });
        }

        $scope.cancel = function () {
            $state.go(pages.instructions);
        }

        init();
    }
]);