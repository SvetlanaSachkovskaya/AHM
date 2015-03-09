app.controller('createPackageController', ['$scope', '$state', 'buildingService', 'postService',
    function ($scope, $state, buildingService, postService) {
        'use strict';

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
            if ($scope.package.occupantId == null) {
                $scope.package.shouldNotifyAll = true;
            } else if ($scope.package.occupantId == 0) {
                $scope.package.shouldNotifyAll = false;
                $scope.package.occupantId = null;
            }

            postService.createPackage($scope.package).then(function () {
                $state.go('landing.packagesBoard');
            }, function (error) {
                alert(error.data.message);
            });
        };

        $scope.setOccupants = function () {
            if ($scope.package.apartmentId > 0) {
                buildingService.getOccupantsByApartmentId($scope.package.apartmentId).then(function (results) {
                    $scope.occupants = results.data;
                        $scope.occupants.push({ name: "None", id: 0 });
                    },
                function(error) {
                    alert(error);
                });
            }
        }

        buildingService.getPackageTypes().then(function (result) {
            $scope.packageTypes = result.data;
        },
        function (error) {
            alert(error.data.message);
        });

        buildingService.getLocations().then(function(result) {
            $scope.locations = result.data;
        },function (error) {
            alert(error.data.message);
        });

        buildingService.getApartments().then(function(result) {
                $scope.apartments = result.data;
            },
        function (error) {
            alert(error.data.message);
        });
    }]);