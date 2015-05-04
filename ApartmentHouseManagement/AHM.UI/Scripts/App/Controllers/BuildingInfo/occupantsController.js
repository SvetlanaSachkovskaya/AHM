app.controller('occupantsController', ['$scope', '$state', 'buildingService', function ($scope, $state, buildingService) {
    'use strict';

    $scope.occupants = [];
    $scope.apartments = [];
    $scope.apartmentId = null;

    $scope.addOccupant = function () {
        $state.go('landing.createOccupant');
    }

    $scope.removeOccupant = function(occupant) {
        if (confirm('Are you sure you want to delete this occupant?')) {
            buildingService.removeOccupant(occupant, function() {
                $scope.occupants.splice($scope.occupants.indexOf(occupant), 1);
            });
        }
    }

    $scope.filterOccupants = function (element) {
        return ($scope.apartmentId === null || element.apartmentId === $scope.apartmentId) ? true : false;
    }

    $scope.edit = function(occupant) {
        $state.go('landing.createOccupant', { occupantId: occupant.id });
    }

    buildingService.getApartments(function (data) {
        $scope.apartments = data;
    });

    buildingService.getOccupants(function(data) {
        $scope.occupants = data;
    });
}]);