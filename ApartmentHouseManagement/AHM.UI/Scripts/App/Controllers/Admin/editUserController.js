app.controller('editUserController', ['$scope', '$state', '$stateParams', '$filter', 'adminService', function ($scope, $state, $stateParams, $filter, adminService) {
    'use strict';

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
        buildingId: null,
    };

    $scope.roles = [];

    $scope.buildings = [];

    $scope.isEditMode = false;

    $scope.create = function () {
        forceRequiredValidation();

        if ($scope.userForm.$valid) {
            adminService.registerUser($scope.user, function () {
                $state.go('landing.users');
            });
        }
    }

    $scope.save = function () {
        forceRequiredValidation();

        if ($scope.buildingForm.$valid) {
            adminService.updateUser($scope.user, function () {
                $state.go('landing.users');
            });
        }
    }

    $scope.cancel = function () {
        $state.go('landing.users');
    }

    if ($stateParams.id) {
        adminService.getUserById($stateParams.id, function (data) {
            $scope.user = data;
            $scope.isEditMode = true;
        });
    }

    adminService.getRoles(function (data) {
        $scope.roles = data;
    });

    adminService.getAllBuildings(function (data) {
        $scope.buildings = data;
    });
}]);