'use strict';
app.controller('packagesController', ['$scope', 'postService', function ($scope, postService) {
    $scope.packages = [];

    postService.getPackages().then(function (results) {
        $scope.packages = results.data;
    }, function (error) {
        //alert(error.data.message);
    });
}]);