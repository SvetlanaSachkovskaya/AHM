app.controller('editInstructionController', ['$scope', '$state', '$stateParams', 'instructionsService',
    function ($scope, $state, $stateParams, instructionsService) {
        'use strict';

        $scope.instruction = null;
        $scope.priorities = [];

        $scope.saveInstruction = function () {
            instructionsService.updateInstruction($scope.instruction).then(
                function () {
                    $state.go('landing.instructions');
                },
                function (error) {
                    alert(error);
                });
        }

        instructionsService.getById($stateParams.instructionId).then(
            function (result) {
                $scope.instruction = result.data;
            },
            function (error) {
                alert(error);
            });

        instructionsService.getPriorities().then(
            function(result) {
                $scope.priorities = result.data;
            },
            function(error) {
                alert(error);
            });
    }]);