app.controller('buildingsController', ['$scope', '$state', '$filter', 'adminService', 'pages',
    function ($scope, $state, $filter, adminService, pages) {
        'use strict';

        function init() {
            adminService.getAllBuildings(function (data) {
                $scope.buildings = data;
            });
        }

        $scope.buildings = [];

        $scope.removeBuilding = function (building) {
            if (confirm('Are you sure you want to delete this building?')) {
                adminService.removeBuilding(building, function () {
                    $scope.buildings.splice($scope.buildings.indexOf(building), 1);
                });
            }
        }

        $scope.add = function () {
            $state.go(pages.editBuilding);
        }

        $scope.edit = function (id) {
            $state.go(pages.editBuilding, { id: id });
        }

        init();
    }
]);