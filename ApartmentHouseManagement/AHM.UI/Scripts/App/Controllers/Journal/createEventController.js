app.controller('journalController', ['$scope', '$state', 'journalService', function ($scope, $state, journalService) {
    'use strict';

    $scope.event = {
        content: '',
        date: new Date(),
        time: new Date()
    };

    $scope.datePickerSettings = {
        open: function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.opened = true;
        }
    }

    $scope.createEvent = function () {
        journalService.createEvent($scope.event).then(
            function () {
                $state.go('landing.journal');
            },
            function (error) {
                alert(error);
            });
    }
}]);