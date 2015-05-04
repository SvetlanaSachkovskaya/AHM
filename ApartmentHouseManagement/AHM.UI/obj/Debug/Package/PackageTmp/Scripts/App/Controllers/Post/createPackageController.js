app.controller('createPackageController', ['$scope', '$state', 'buildingService', 'postService',
    function ($scope, $state, buildingService, postService) {
        'use strict';

        function forceRequiredValidation() {
            if ($scope.packageForm.$error.required) {
                $scope.packageForm.$error.required.forEach(function (element) {
                    element.$setDirty();
                });
            }
        }

        $scope.package = {
            packageTypeId: 0,
            locationId: 0,
            apartmentId: 0,
            occupantId: null,
            shouldNotifyAll: false,
            openComment: '',
        };

        $scope.packageTypes = [];
        $scope.locations = [];
        $scope.apartments = [];
        $scope.occupants = [];

        $scope.create = function () {
            forceRequiredValidation();

            if ($scope.packageForm.$valid) {
                if ($scope.package.occupantId == null) {
                    $scope.package.shouldNotifyAll = true;
                } else if ($scope.package.occupantId === 0) {
                    $scope.package.shouldNotifyAll = false;
                    $scope.package.occupantId = null;
                }

                postService.createPackage($scope.package, function () {
                    $state.go('landing.packagesBoard');
                });
            }
        };

        $scope.setOccupants = function () {
            if ($scope.package.apartmentId > 0) {
                buildingService.getOccupantsByApartmentId($scope.package.apartmentId, function (data) {
                    $scope.occupants = data;
                    $scope.occupants.push({ name: "None", id: 0 });
                });
            }
        }

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