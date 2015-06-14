app.controller('editPackageController', ['$scope', '$state', '$stateParams', 'buildingService', 'postService', 'pages',
    function ($scope, $state, $stateParams, buildingService, postService, pages) {
        'use strict';

        function init() {
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
        }

        $scope.package = null;
        $scope.notification = '';

        $scope.locations = [];

        $scope.save = function () {
            postService.updatePackage($scope.package, function () {
                $state.go(pages.packagesBoard);
            });
        };

        $scope.cancel = function () {
            $state.go(pages.packagesBoard);
        }

        init();
    }
]);