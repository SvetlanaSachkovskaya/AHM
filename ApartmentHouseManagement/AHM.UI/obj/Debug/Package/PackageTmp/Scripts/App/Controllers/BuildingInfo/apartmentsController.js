app.controller('apartmentsController', ['$scope', '$state', '$filter', 'buildingService', function ($scope, $state, $filter, buildingService) {
    'use strict';

    $scope.apartments = [];

    $scope.removeApartment = function (apartment) {
        if (confirm('Are you sure you want to delete this apartment?')) {
            buildingService.removeApartment(apartment, function () {
                $scope.apartments.splice($scope.apartments.indexOf(apartment), 1);
            });
        }
    }

    $scope.add = function () {
        $state.go('landing.editApartment');
    }

    $scope.edit = function (id) {
        $state.go('landing.editApartment', {apartmentId: id});
    }

    buildingService.getApartments(function (data) {
        $scope.apartments = data;
    });
}]);