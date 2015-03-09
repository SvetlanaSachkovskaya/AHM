app.controller('locationsController', ['$scope', '$filter', 'buildingService', function ($scope, $filter, buildingService) {
    'use strict';

    function setLocation(location) {
        if (location) {
            $scope.newLocation.id = location.id;
            $scope.newLocation.shortDescription = location.shortDescription;
            $scope.newLocation.longDescription = location.longDescription;
            $scope.newLocation.buildingId = location.buildingId;
        } else {
            $scope.newLocation.id = 0;
            $scope.newLocation.shortDescription = '';
            $scope.newLocation.longDescription = '';
            $scope.newLocation.buildingId = 0;
        }
    }

    $scope.locations = [];

    $scope.newLocation = {
        id: 0,
        shortDescription: '',
        longDescription: '',
        buildingId : 0
    };

    $scope.isEditMode = false;

    $scope.removeLocation = function (location) {
        if (confirm('Are you sure you want to delete this location?')) {
            buildingService.removeLocation(location).then(function () {
                $scope.locations.splice($scope.locations.indexOf(location), 1);
            }, function (error) {
                alert(error);
            });
        }
    }

    $scope.addLocation = function () {
        buildingService.addLocation($scope.newLocation).then(function (result) {
            $scope.locations.push(result.data);
            setLocation();
        }, function (error) {
            alert(error);
        });
    }

    $scope.updateLocation = function () {
        buildingService.updateLocation($scope.newLocation).then(function () {
            var location = $filter('filter')($scope.locations, { id: $scope.newLocation.id })[0];
            location.shortDescription = $scope.newLocation.shortDescription;
            location.longDescription = $scope.newLocation.longDescription;

            setLocation();
            $scope.isEditMode = false;

        }, function (error) {
            alert(error);
        });
    }

    $scope.edit = function (location) {
        setLocation(location);
        $scope.isEditMode = true;
    }

    $scope.cancelEdition = function () {
        setLocation();
        $scope.isEditMode = false;
    }

    buildingService.getLocations().then(function (results) {
        $scope.locations = results.data;
    }, function (error) {
        alert(error.data.message);
    });
}]);