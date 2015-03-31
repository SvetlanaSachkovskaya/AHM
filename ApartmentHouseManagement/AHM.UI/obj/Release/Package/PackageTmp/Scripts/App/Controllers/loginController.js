app.controller('loginController', ['$scope', '$state', 'authenticationService',
    function ($scope, $state, authenticationService) {
        'use strict';

        $scope.loginData = {
            userName: "",
            password: ""
        };

        $scope.message = "";

        $scope.login = function () {

            authenticationService.login($scope.loginData).then(function () {
                $state.go('landing.home');
            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };
    }]);