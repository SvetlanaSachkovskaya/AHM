app.controller('createEventController', ['$scope', '$state', 'journalService', function ($scope, $state, journalService) {
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
        dateTime: new Date()
    };

    $scope.datePickerSettings = {
        open: function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.opened = true;
        }
    }

    $scope.createEvent = function () {
        forceRequiredValidation();

        if ($scope.eventForm.$valid) {
            journalService.createEvent($scope.event, function () {
                $state.go('landing.journal');
            });
        }
    }
}]);