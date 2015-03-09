app.controller('apartmentsController', ['$scope', '$state', '$filter', 'buildingService', function ($scope, $state, $filter, buildingService) {
    'use strict';

    $scope.apartments = [];

    $scope.removeApartment = function (apartment) {
        if (confirm('Are you sure you want to delete this apartment?')) {
            buildingService.removeApartment(apartment).then(function () {
                $scope.apartments.splice($scope.apartments.indexOf(apartment), 1);
            }, function (error) {
                alert(error);
            });
        }
    }

    $scope.add = function () {
        $state.go('landing.editApartment');
    }

    $scope.edit = function (id) {
        $state.go('landing.editApartment', {apartmentId: id});
    }

    buildingService.getApartments().then(function (results) {
        $scope.apartments = results.data;
    }, function (error) {
        alert(error.data.message);
    });
}]);