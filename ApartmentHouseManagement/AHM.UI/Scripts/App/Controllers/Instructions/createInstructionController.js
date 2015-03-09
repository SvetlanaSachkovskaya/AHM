app.controller('createInstructionController', ['$scope', '$state', 'instructionsService', function ($scope, $state, instructionsService) {
    'use strict';

    $scope.instruction = {
        title: '',
        content: '',
        completionDate: new Date(),
        priority: 0,
        isClosed: false
    };

    $scope.createInstruction = function () {
        instructionsService.createInstruction($scope.instruction).then(function () {
            $state.go('landing.instructions');
        },
        function (error) {
            alert(error);
        });
    }
}]);