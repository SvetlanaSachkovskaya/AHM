'use strict';
app.controller('createPackageController', ['$scope', 'postService', function ($scope, postService) {
    $scope.package = {
        type: null,
        location: null,
        apartment: null,
        occupant: null,
        comment: '',
    };

    $scope.packageTypes = [];
    $scope.locations = [];
    $scope.apartments = [];
    $scope.occupants = [];

    $scope.create = function () {
        postService.createPackage($scope.package).then(function (result) {

        }, function (error) {
            //alert(error.data.message);
        });
    };
}]);