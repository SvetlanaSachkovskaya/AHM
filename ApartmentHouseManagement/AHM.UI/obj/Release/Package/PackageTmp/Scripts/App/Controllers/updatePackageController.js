'use strict';
app.controller('createPackageController', ['$scope', 'postService', function ($scope, postService) {
    $scope.package = {};

    postService.getPackageById().then(function (result) {
        $scope.package = result;
    })

    $scope.update = function () {
        postService.updatePackage($scope.package).then(function (result) {

        }, function (error) {
            //alert(error.data.message);
        });
    };
}]);