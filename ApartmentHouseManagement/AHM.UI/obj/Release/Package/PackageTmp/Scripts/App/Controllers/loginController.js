'use strict';
app.controller('loginController', ['$scope', '$location', 'authenticationService', 'ngAuthSettings', function ($scope, $location, authenticationService, ngAuthSettings) {

    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function () {

        authenticationService.login($scope.loginData).then(function () {

            $location.path('/home');

        },
         function (err) {
             $scope.message = err.error_description;
         });
    };
}]);