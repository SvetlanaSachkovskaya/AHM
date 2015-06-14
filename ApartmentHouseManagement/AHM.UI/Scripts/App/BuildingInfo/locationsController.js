app.controller('locationsController', ['$scope', '$filter', 'buildingService',
    function ($scope, $filter, buildingService) {
        'use strict';

        function init() {
            buildingService.getLocations(function (data) {
                $scope.locations = data;
            });
        }

        function forceRequiredValidation() {
            if ($scope.locationForm.$error.required) {
                $scope.locationForm.$error.required.forEach(function (element) {
                    element.$setDirty();
                });
            }
        }

        function setPristine() {
            $scope.locationForm.$setPristine();
        }

        function setLocation(location) {
            if (location) {
                angular.copy(location, $scope.newLocation);
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
            buildingId: 0
        };

        $scope.isEditMode = false;

        $scope.removeLocation = function (location) {
            if (confirm('Are you sure you want to delete this location?')) {
                buildingService.removeLocation(location, function () {
                    $scope.locations.splice($scope.locations.indexOf(location), 1);
                });
            }
        }

        $scope.addLocation = function () {
            forceRequiredValidation();

            if ($scope.locationForm.$valid) {
                buildingService.addLocation($scope.newLocation, function (data) {
                    $scope.locations.push(data);
                    setLocation();
                    setPristine();
                });
            }
        }

        $scope.updateLocation = function () {
            forceRequiredValidation();

            if ($scope.locationForm.$valid) {
                buildingService.updateLocation($scope.newLocation, function () {
                    var location = $filter('filter')($scope.locations, { id: $scope.newLocation.id })[0];
                    location.shortDescription = $scope.newLocation.shortDescription;
                    location.longDescription = $scope.newLocation.longDescription;

                    setLocation();
                    $scope.isEditMode = false;
                    setPristine();
                });
            }
        }

        $scope.edit = function (location) {
            setLocation(location);
            $scope.isEditMode = true;
        }

        $scope.cancelEdition = function () {
            setLocation();
            $scope.isEditMode = false;
            setPristine();
        }

        init();
    }
]);