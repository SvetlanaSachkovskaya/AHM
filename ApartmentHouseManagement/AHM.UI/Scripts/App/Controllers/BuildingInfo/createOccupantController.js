app.controller('createOccupantController', ['$scope', '$state', '$stateParams', 'buildingService', function ($scope, $state, $stateParams, buildingService) {
    'use strict';

    $scope.apartments = [];

    $scope.occupant = {
        firstName: '',
        lastName: '',
        dateOfBirth: new Date(),
        email: '',
        aparmentId: 0,
        isSubtenant: false
    }

    $scope.isEditMode = false;

    $scope.create = function () {
        buildingService.addOccupant($scope.occupant).then(function () {
            $state.go('landing.occupants');
        },
        function (error) {
            alert(error);
        });
    }

    $scope.save = function () {
        buildingService.updateOccupant($scope.occupant).then(function () {
            $state.go('landing.occupants');
        },
        function (error) {
            alert(error);
        });
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

    buildingService.getApartments().then(function (results) {
        $scope.apartments = results.data;

        if ($stateParams.occupantId) {
            buildingService.getOccupantById($stateParams.occupantId).then(function (result) {
                $scope.occupant = result.data;
                $scope.isEditMode = true;
            },
            function (error) {
                alert(error);
            });
        }
    }, function (error) {
        alert(error.data.message);
    });
}]);