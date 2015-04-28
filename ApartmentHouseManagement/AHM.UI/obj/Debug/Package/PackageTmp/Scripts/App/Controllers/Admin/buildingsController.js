app.controller('buildingsController', ['$scope', '$state', '$filter', 'adminService', function ($scope, $state, $filter, adminService) {
    'use strict';

    $scope.buildings = [];

    $scope.removeBuilding = function (building) {
        if (confirm('Are you sure you want to delete this building?')) {
            adminService.removeBuilding(building, function () {
                $scope.buildings.splice($scope.buildings.indexOf(building), 1);
            });
        }
    }

    $scope.add = function () {
        $state.go('landing.editBuilding');
    }

    $scope.edit = function (id) {
        $state.go('landing.editBuilding', { id: id });
    }

    adminService.getAllBuildings(function (data) {
        $scope.buildings = data;
    });
}]);