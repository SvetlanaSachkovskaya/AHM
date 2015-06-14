app.controller('journalController', ['$scope', '$state', 'journalService', 'roles', 'datePickerSettings',
    function ($scope, $state, journalService, roles, datePickerSettings) {
        'use strict';

        $scope.datePickerSettings = datePickerSettings;
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
            journalService.getEventsByDate($scope.filterDate, function(data) {
                $scope.events = data;
            });
        }

        $scope.filter();
    }
]);