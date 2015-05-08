app.controller('packagesBoardController', ['$scope', '$state', 'allOption', 'buildingService', 'postService',
    function ($scope, $state, allOption, buildingService, postService) {
        'use strict';

        $scope.packages = [];
        $scope.packageTypes = [];
        $scope.locations = [];

        $scope.locationId = null;
        $scope.packageTypeId = null;
        $scope.apartmentId = null;

        $scope.edit = function (packageId) {
            $state.go('landing.editPackage', { packageId: packageId });
        }

        $scope.create = function () {
            $state.go('landing.createPackage');
        }

        $scope.filterPackages = function (element) {
            return ($scope.packageTypeId === null || element.packageTypeId === $scope.packageTypeId) &&
                ($scope.locationId === null || element.locationId === $scope.locationId) &&
                ($scope.apartmentId === null || element.apartmentId === $scope.apartmentId) ?
                true :
                false;
        }

        postService.getPackages(function (data) {
            $scope.packages = data;
        });

        buildingService.getPackageTypes(function (data) {
            $scope.packageTypes = data;
        });

        buildingService.getLocations(function (data) {
            $scope.locations = data;
        });

        buildingService.getApartments(function (data) {
            $scope.apartments = data;
        });
    }]);