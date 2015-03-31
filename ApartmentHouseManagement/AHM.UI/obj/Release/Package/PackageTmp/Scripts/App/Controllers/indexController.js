app.controller('indexController', ['$scope', '$state', 'authenticationService', 'httpModule', function ($scope, $state, authenticationService, httpModule) {
    'use strict';

    $scope.logOut = function() {
        authenticationService.logOut();
        $state.go('login');
    };

    $scope.authentication = authenticationService.authentication;

    $scope.requestResult = httpModule.requestResult;

    $scope.closeAlert = function () {
        $scope.requestResult.isSuccessful = true;
    };
}]);