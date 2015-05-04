app.controller('createOccupantController', ['$scope', '$state', '$stateParams', 'buildingService', function ($scope, $state, $stateParams, buildingService) {
    'use strict';

    function forceRequiredValidation() {
        if ($scope.occupantForm.$error.required) {
            $scope.occupantForm.$error.required.forEach(function (element) {
                element.$setDirty();
            });
        }
    }

    $scope.apartments = [];

    $scope.occupant = {
        name: '',
        dateOfBirth: new Date(),
        email: '',
        aparmentId: 0,
        isSubtenant: false
    }

    $scope.isEditMode = false;

    $scope.create = function () {
        forceRequiredValidation();

        if ($scope.occupantForm.$valid) {
            buildingService.addOccupant($scope.occupant, function () {
                $state.go('landing.occupants');
            });
        }
    }

    $scope.save = function () {
        forceRequiredValidation();

        if ($scope.occupantForm.$valid) {
            buildingService.updateOccupant($scope.occupant, function () {
                $state.go('landing.occupants');
            });
        }
    }

    $scope.cancel = function () {
        $state.go('landing.occupants');
    }

    $scope.datePickerSettings = {
        today: function () {
            $scope.dateOfBirth = new Date();
        },
        clear: function () {
            $scope.dateOfBirth = null;
        },
        open: function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.opened = true;
        }
    }

    buildingService.getApartments(function (data) {
        $scope.apartments = data;

        if ($stateParams.occupantId) {
            buildingService.getOccupantById($stateParams.occupantId, function (occupant) {
                $scope.occupant = occupant;
                $scope.isEditMode = true;
            });
        }
    });
}]);