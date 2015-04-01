app.controller('indexController', ['$scope', '$state', 'authenticationService', 'httpModule', 'roles',
    function ($scope, $state, authenticationService, httpModule, roles) {
        'use strict';

        $scope.logOut = function () {
            authenticationService.logOut();
            $state.go('login');
        };

        $scope.authentication = authenticationService.authentication;
        $scope.roles = roles;
        $scope.requestResult = httpModule.requestResult;

        $scope.closeAlert = function () {
            $scope.requestResult.isSuccessful = true;
        };
    }]);