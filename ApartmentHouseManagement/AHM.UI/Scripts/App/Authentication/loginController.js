app.controller('loginController', ['$scope', '$state', 'authenticationService', 'roles', 'pages',
    function ($scope, $state, authenticationService, roles, pages) {
        'use strict';

        function getHomePage(role) {
            switch (role) {
                case roles.admin:
                    return pages.buildings;
                case roles.manager:
                    return pages.apartments;
                case roles.concierge:
                    return pages.packagesBoard;
                case roles.accountant:
                    return pages.billsBoard;
                case roles.worker:
                    return pages.instructions;
                default:
                    return pages.notFound;
            }
        }

        $scope.loginData = {
            userName: "",
            password: ""
        };

        $scope.errorMessage = "";

        $scope.login = function () {
            authenticationService.login($scope.loginData, function () {
                $state.go(pages.home);
            },
            function(error) {
                $scope.errorMessage = error;
            });
        };
    }
]);