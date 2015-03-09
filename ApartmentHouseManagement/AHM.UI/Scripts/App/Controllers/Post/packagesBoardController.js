app.controller('packagesBoardController', ['$scope', '$state', 'allOption', 'buildingService', 'postService',
    function ($scope, $state, allOption, buildingService, postService) {
        'use strict';

        $scope.packages = [];
        $scope.packageTypes = [];
        $scope.locations = [];

        $scope.locationId = allOption.value;
        $scope.packageTypeId = allOption.value;

        $scope.edit = function (packageId) {
            $state.go('landing.editPackage', { packageId: packageId });
        }

        $scope.create = function () {
            $state.go('landing.createPackage');
        }

        $scope.filterPackages = function (element) {
            return ($scope.packageTypeId === 0 || element.packageTypeId === $scope.packageTypeId) &&
                ($scope.locationId === 0 || element.locationId === $scope.locationId) ?
                true :
                false;
        }

        postService.getPackages().then(function (results) {
            $scope.packages = results.data;
        }, function (error) {
            alert(error.data.message);
        });

        buildingService.getPackageTypes().then(function (result) {
            $scope.packageTypes = result.data;
            $scope.packageTypes.splice(0, 0, {
                longDescription: "All types",
                id: allOption.value
            });
        },
            function (error) {
                alert(error.data.message);
            });

        buildingService.getLocations().then(function (result) {
            $scope.locations = result.data;
            $scope.locations.splice(0, 0, {
                longDescription: "All locations",
                id: allOption.value
            });
        }, function (error) {
            alert(error.data.message);
        });
    }]);