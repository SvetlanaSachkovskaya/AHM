app.controller('indexController', ['$scope', '$state', 'authenticationService', function ($scope, $state, authenticationService) {
    'use strict';

    $scope.logOut = function() {
        authenticationService.logOut();
        $state.go('login');
    };

    $scope.authentication = authenticationService.authentication;

}]);