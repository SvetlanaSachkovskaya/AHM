'use strict';
app.controller('locationController', ['$scope', 'buildingService', function ($scope, buildingService) {
    $scope.locations = [];

    $scope.newLocation = {
        shortDescription: '',
        longDescription: ''
    };

    $scope.removeLocation = function(location) {
        buildingService.removeLocation(location).then(function() {
            $scope.locations.splice($scope.locations.indexOf(location));
        }, function(error) {
            alert(error);
        });
    }

    $scope.addLocation = function () {
        buildingService.addLocation(newLocation).then(function (result) {
            $scope.locations.push(result);

            $scope.newLocation.shortDescription = '';
            $scope.newLocation.longDescription = '';
        }, function (error) {
            alert(error);
        });
    }

    buildingService.getLocations().then(function (results) {
        $scope.locations = results.data;
    }, function (error) {
        alert(error.data.message);
    });


}]);