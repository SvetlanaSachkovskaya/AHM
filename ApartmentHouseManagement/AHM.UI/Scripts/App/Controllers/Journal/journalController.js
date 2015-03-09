app.controller('journalController', ['$scope', '$state', 'journalService', function ($scope, $state, journalService) {
    'use strict';

    function resultCallback(result) {
        $scope.events = result.data;
    }

    function errorCallback(error) {
        alert(error);
    }

    $scope.timeInterval = 1;

    $scope.events = [];

    $scope.removeEvent = function (event) {
        journalService.updateEvent(event).then(
            function () {
                $scope.events.splice($scope.events.indexOf(event), 1);
            },
            function (error) {
                alert(error);
            });
    }

    $scope.refreshJournal = function () {
        switch ($scope.timeInterval) {
            case 1:
                journalService.getEventsPerDay.then(resultCallback, errorCallback);
            break;
            case 2:
                journalService.getEventsPerWeek.then(resultCallback, errorCallback);
            break;
            case 3:
                journalService.getEventsPerMonth.then(resultCallback, errorCallback);
            break;
            case 4:
                journalService.getEventsPerYear.then(resultCallback, errorCallback);
            break;
            default:
                journalService.getAllEvents.then(resultCallback, errorCallback);
        }
    }

    $scope.refreshJournal();
}]);