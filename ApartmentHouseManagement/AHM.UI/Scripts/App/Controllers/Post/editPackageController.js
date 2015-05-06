app.controller('editPackageController', ['$scope', '$state', '$stateParams', 'buildingService', 'postService',
    function ($scope, $state, $stateParams, buildingService, postService) {
        'use strict';

        $scope.package = null;
        $scope.notification = '';

        $scope.locations = [];

        $scope.save = function () {
            postService.updatePackage($scope.package, function () {
                $state.go('landing.packagesBoard');
            });
        };

        $scope.cancel = function () {
            $state.go('landing.packagesBoard');
        }

        postService.getPackageById($stateParams.packageId, function (data) {
            $scope.package = data;
            if ($scope.package.notificationOptions.shouldNotifyAllOccupants) {
                $scope.notification = 'All';
            }
            else if ($scope.package.notificationOptions.occupantId) {
                $scope.notification = $scope.package.notificationOptions.occupant.name;
            } else {
                $scope.notification = "None";
            }
        });

        buildingService.getLocations(function (data) {
            $scope.locations = data;
        });
    }]);