app.controller('createInstructionController', ['$scope', '$state', 'instructionsService', 'datePickerSettings', 'pages',
    function ($scope, $state, instructionsService, datePickerSettings, pages) {
        'use strict';

        function init() {
            instructionsService.getPriorities(function (data) {
                $scope.priorities = data;
            });
        }

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

        $scope.datePickerSettings = datePickerSettings;

        $scope.priorities = [];

        $scope.createInstruction = function () {
            forceRequiredValidation();

            if ($scope.instructionForm.$valid) {
                instructionsService.createInstruction($scope.instruction, function () {
                    $state.go(pages.instructions);
                });
            }
        }

        $scope.cancel = function () {
            $state.go(pages.instructions);
        }

        init();
    }
]);