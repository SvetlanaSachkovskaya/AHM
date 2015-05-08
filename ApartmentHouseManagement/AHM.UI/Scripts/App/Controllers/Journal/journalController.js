app.controller('journalController', ['$scope', '$state', 'journalService', 'roles', function ($scope, $state, journalService, roles) {
    'use strict';

    function resultCallback(data) {
        $scope.events = data;
    }

    $scope.openDatePicker = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.opened = true;
    }

    $scope.events = [];

    $scope.roles = roles;

    $scope.filterDate = new Date();

    $scope.removeEvent = function (event) {
        if (confirm("Are you sure that you want to delete this event?")) {
            event.isRemoved = true;

            journalService.updateEvent(event, function () {
                $scope.events.splice($scope.events.indexOf(event), 1);
            });
        }
    }

    $scope.filter = function () {
        journalService.getEventsByDate($scope.filterDate, resultCallback);
    }

    $scope.filter();
}]);