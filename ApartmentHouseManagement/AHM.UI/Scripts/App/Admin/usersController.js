app.controller('usersController', ['$scope', '$state', '$stateParams', '$filter', 'adminService', 'pages',
    function ($scope, $state, $stateParams, $filter, adminService, pages) {
        'use strict';

        function init() {
            adminService.getAllBuildings(function (data) {
                if ($stateParams.buildingId) {
                    $scope.search.buildingId = parseInt($stateParams.buildingId);
                }
                $scope.buildings = data;
            });

            adminService.getAllUsers(function (data) {
                $scope.users = data;
            });
        }

        $scope.users = [];
        $scope.buildings = [];
        $scope.search = {
            buildingId: null
        };

        $scope.lock = function (user) {
            if (confirm('Are you sure you want to lock this user?')) {
                adminService.lockUser(user, function () {
                    user.isLocked = true;
                });
            }
        }

        $scope.unlock = function (user) {
            adminService.unlockUser(user, function () {
                user.isLocked = false;
            });
        }

        $scope.edit = function (id) {
            $state.go(pages.editUser, { id: id });
        }

        init();
    }
]);