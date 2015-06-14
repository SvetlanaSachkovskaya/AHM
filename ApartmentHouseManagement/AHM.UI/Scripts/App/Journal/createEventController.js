app.controller('createEventController', ['$scope', '$state', 'journalService', 'datePickerSettings', 'pages',
    function ($scope, $state, journalService, datePickerSettings, pages) {
        'use strict';

        function forceRequiredValidation() {
            if ($scope.eventForm.$error.required) {
                $scope.eventForm.$error.required.forEach(function (element) {
                    element.$setDirty();
                });
            }
        }

        $scope.event = {
            content: '',
            dateTimeString: ''
        };

        $scope.date = new Date();
        $scope.time = new Date();

        $scope.datePickerSettings = datePickerSettings;

        $scope.createEvent = function () {
            forceRequiredValidation();

            if ($scope.eventForm.$valid) {
                $scope.event.dateTimeString = $scope.date.getDate() + "/" + ($scope.date.getMonth() + 1) + "/" + $scope.date.getFullYear() + " "
                    + $scope.time.getHours() + ':' + $scope.time.getMinutes();

                journalService.createEvent($scope.event, function () {
                    $state.go(pages.journal);
                });
            }
        }

        $scope.cancel = function () {
            $state.go(pages.journal);
        }
    }]);