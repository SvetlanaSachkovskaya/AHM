app.controller('packagesBoardController', ['$scope', '$state', 'buildingService', 'postService', 'pages',
    function ($scope, $state, buildingService, postService, pages) {
        'use strict';

        function init() {
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
        }

        $scope.packages = [];
        $scope.packageTypes = [];
        $scope.locations = [];

        $scope.locationId = null;
        $scope.packageTypeId = null;
        $scope.apartmentId = null;

        $scope.edit = function (packageId) {
            $state.go(pages.editPackage, { packageId: packageId });
        }

        $scope.create = function () {
            $state.go(pages.createPackage);
        }

        $scope.filterPackages = function (element) {
            return ($scope.packageTypeId === null || element.packageTypeId === $scope.packageTypeId) &&
                ($scope.locationId === null || element.locationId === $scope.locationId) &&
                ($scope.apartmentId === null || element.apartmentId === $scope.apartmentId) ?
                true :
                false;
        }

        init();
    }
]);