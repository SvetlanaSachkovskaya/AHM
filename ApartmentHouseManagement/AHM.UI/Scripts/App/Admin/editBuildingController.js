app.controller('editBuildingController', ['$scope', '$state', '$stateParams', '$filter', 'adminService', 'pages',
    function ($scope, $state, $stateParams, $filter, adminService, pages) {
        'use strict';

        function init() {
            if ($stateParams.id) {
                adminService.getBuildingById($stateParams.id, function (data) {
                    $scope.building = data;
                    $scope.isEditMode = true;
                });
            }
        }

        function forceRequiredValidation() {
            if ($scope.buildingForm.$error.required) {
                $scope.buildingForm.$error.required.forEach(function (element) {
                    element.$setDirty();
                });
            }
        }

        $scope.building = {
            name: '',
            state: '',
            city: '',
            street: '',
            number: ''
        };

        $scope.isEditMode = false;

        $scope.create = function () {
            forceRequiredValidation();

            if ($scope.buildingForm.$valid) {
                adminService.addBuilding($scope.building, function () {
                    $state.go(pages.buildings);
                });
            }
        }

        $scope.save = function () {
            forceRequiredValidation();

            if ($scope.buildingForm.$valid) {
                adminService.updateBuilding($scope.building, function () {
                    $state.go(pages.buildings);
                });
            }
        }

        $scope.cancel = function () {
            $state.go(pages.buildings);
        }

        init();
    }
]);