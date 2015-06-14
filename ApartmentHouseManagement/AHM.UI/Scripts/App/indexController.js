app.controller('indexController', ['$scope', '$state', 'authenticationService', 'httpModule', 'roles', 'pages',
    function ($scope, $state, authenticationService, httpModule, roles, pages) {
        'use strict';

        $scope.logOut = function () {
            authenticationService.logOut();
            $state.go(pages.login);
        };

        $scope.authentication = authenticationService.authentication;
        $scope.roles = roles;
        $scope.requestResult = httpModule.requestResult;

        $scope.closeAlert = function () {
            $scope.requestResult.isSuccessful = true;
        };
    }
]);