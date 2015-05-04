app.controller('journalController', ['$scope', '$state', 'journalService', function ($scope, $state, journalService) {
    'use strict';

    function resultCallback(data) {
        $scope.events = data;
    }

    $scope.timeIntervals = [
        { id: 1, name: 'Day' },
        { id: 2, name: 'Week' },
        { id: 3, name: 'Month' },
        { id: 4, name: 'Year' },
        { id: 5, name: 'All' }
    ];

    $scope.timeInterval = {
        value: $scope.timeIntervals[0].id
    };

    $scope.events = [];

    $scope.removeEvent = function (event) {
        if (confirm("Are you sure that you want to delete this event?")) {
            event.isRemoved = true;

            journalService.updateEvent(event, function () {
                $scope.events.splice($scope.events.indexOf(event), 1);
            });
        }
    }

    $scope.createEvent = function () {
        $state.go('landing.createEvent');
    }

    $scope.$watch('timeInterval.value', function(newValue) {
        switch (newValue) {
        case 1:
            journalService.getEventsPerDay(resultCallback);
            break;
        case 2:
            journalService.getEventsPerWeek(resultCallback);
            break;
        case 3:
            journalService.getEventsPerMonth(resultCallback);
            break;
        case 4:
            journalService.getEventsPerYear(resultCallback);
            break;
        default:
            journalService.getAllActiveEvents(resultCallback);
        }
    });
}]);