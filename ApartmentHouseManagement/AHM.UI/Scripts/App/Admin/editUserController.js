app.controller('editUserController', ['$scope', '$state', '$stateParams', '$filter', 'roles', 'adminService', 'pages',
    function ($scope, $state, $stateParams, $filter, roles, adminService, pages) {
        'use strict';

        function init() {
            if ($stateParams.id) {
                adminService.getUserById($stateParams.id, function (data) {
                    $scope.user = data;
                    $scope.isEditMode = true;
                });
            }

            adminService.getRoles(function (data) {
                $scope.roles = data;

                var admin = $filter('filter')($scope.roles, { name: roles.admin });
                if (admin && admin.length > 0) {
                    $scope.adminId = admin[0].id;
                }
            });

            adminService.getAllBuildings(function (data) {
                $scope.buildings = data;
            });
        }

        function forceRequiredValidation() {
            if ($scope.userForm.$error.required) {
                $scope.userForm.$error.required.forEach(function (element) {
                    element.$setDirty();
                });
            }
        }

        $scope.user = {
            firstName: '',
            lastName: '',
            username: '',
            password: '',
            roleId: null,
            buildingId: null
        };

        $scope.roles = [];
        $scope.buildings = [];

        $scope.isEditMode = false;
        $scope.adminId = 0;

        $scope.create = function () {
            forceRequiredValidation();

            if ($scope.userForm.$valid) {
                adminService.registerUser($scope.user, function () {
                    $state.go(pages.users, { buildingId: $scope.user.buildingId });
                });
            }
        }

        $scope.save = function () {
            forceRequiredValidation();

            if ($scope.userForm.$valid) {
                adminService.updateUser($scope.user, function () {
                    $state.go(pages.users, { buildingId: $scope.user.buildingId });
                });
            }
        }

        $scope.cancel = function () {
            $state.go(pages.users, { buildingId: $scope.user.buildingId });
        }

        init();
    }
]);