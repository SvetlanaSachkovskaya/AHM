app.controller('editApartmentController', ['$scope', '$state', '$stateParams', '$filter', 'buildingService', function ($scope, $state, $stateParams, $filter, buildingService) {
    'use strict';

    $scope.apartment = {
        floor: 0,
        number: 0,
        square: 0,
        ownerId : 0
    };

    $scope.occupants = [];
    $scope.isEditMode = false;

    $scope.create = function () {
        buildingService.addApartment($scope.apartment).then(function () {
            $state.go('landing.apartments');
        },
        function (error) {
            alert(error);
        });
    }

    $scope.save = function () {
        buildingService.updateApartment($scope.apartment).then(function () {
            $state.go('landing.apartments');
        },
        function (error) {
            alert(error);
        });
    }

    if ($stateParams.apartmentId) {
        buildingService.getApartmentById($stateParams.apartmentId).then(
            function (result) {
                $scope.apartment = result.data;
                $scope.isEditMode = true;

                buildingService.getOccupantsByApartmentId($stateParams.apartmentId).then(function (results) {
                    $scope.occupants = results.data;
                    var owner = $filter('filter')($scope.occupants, { isOwner: true });
                    if (owner.length > 0) {
                        $scope.apartment.ownerId = owner[0].id;
                    }
                }, function (error) {
                    alert(error.data.message);
                });
            },
            function(error) {
                alert(error);
            });
    }
}]);