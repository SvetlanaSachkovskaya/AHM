app.controller('editPackageController', ['$scope', '$state', '$stateParams', 'buildingService', 'postService',
    function ($scope, $state, $stateParams, buildingService, postService) {
        'use strict';

        $scope.package = null;
        $scope.notification = '';

        $scope.locations = [];

        $scope.save = function () {
            postService.updatePackage($scope.package).then(function () {
                $state.go('landing.packagesBoard');
            }, function (error) {
                alert(error.data.message);
            });
        };

        postService.getPackageById($stateParams.packageId).then(function (results) {
            $scope.package = results.data;
            if ($scope.package.notificationOptions.shouldNotifyAllOccupants) {
                $scope.notification = 'All';
            }
            else if ($scope.package.notificationOptions.occupantId) {
                $scope.notification = $scope.package.notificationOptions.occupant.name;
            } else {
                $scope.notification = "None";
            }
        },
            function (error) {
                alert(error);
            });

        buildingService.getLocations().then(function (result) {
            $scope.locations = result.data;
        },
        function (error) {
            alert(error.data.message);
        });
    }]);