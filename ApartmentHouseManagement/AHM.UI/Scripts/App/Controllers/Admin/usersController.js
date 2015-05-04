app.controller('usersController', ['$scope', '$state', '$stateParams', '$filter', 'adminService', function ($scope, $state, $stateParams, $filter, adminService) {
    'use strict';

    $scope.users = [];
    $scope.buildings = [];
    $scope.search = {
        buildingId: null
    };

    $scope.lockUser = function (user) {
        if (confirm('Are you sure you want to lock this user?')) {
            adminService.removeUser(user, function () {
                $scope.users.splice($scope.users.indexOf(user), 1);
            });
        }
    }

    $scope.edit = function (id) {
        $state.go('landing.editUser', { id: id });
    }

    adminService.getAllBuildings(function (data) {
        if ($stateParams.buildingId) {
            $scope.search.buildingId = parseInt($stateParams.buildingId);
        }
        $scope.buildings = data;
    });

    adminService.getAllUsers(function (data) {
        $scope.users = data;
    });
}]);