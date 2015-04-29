app.controller('usersController', ['$scope', '$state', '$filter', 'adminService', function ($scope, $state, $filter, adminService) {
    'use strict';

    $scope.users = [];
    $scope.buildings = [];

    $scope.lockUser = function (user) {
        if (confirm('Are you sure you want to delete this user?')) {
            adminService.removeUser(user, function () {
                $scope.users.splice($scope.users.indexOf(user), 1);
            });
        }
    }

    $scope.edit = function (id) {
        $state.go('landing.editUser', { id: id });
    }

    adminService.getAllBuildings(function (data) {
        $scope.buildings = data;
    });

    adminService.getAllUsers(function (data) {
        $scope.users = data;
    });
}]);