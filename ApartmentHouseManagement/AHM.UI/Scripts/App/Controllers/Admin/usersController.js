app.controller('usersController', ['$scope', '$state', '$filter', 'adminService', function ($scope, $state, $filter, adminService) {
    'use strict';

    $scope.users = [];
    $scope.buildings = [];

    $scope.removeUser = function (user) {
        if (confirm('Are you sure you want to delete this user?')) {
            adminService.removeUser(user, function () {
                $scope.users.splice($scope.users.indexOf(user), 1);
            });
        }
    }

    $scope.add = function () {
        $state.go('landing.editUser');
    }

    $scope.edit = function (id) {
        $state.go('landing.editUser', { id: id });
    }

    adminService.getUsers(function (data) {
        $scope.users = data;
    });


    adminService.getAllBuildings(function(data) {
        $scope.buildings = data;
    });
}]);