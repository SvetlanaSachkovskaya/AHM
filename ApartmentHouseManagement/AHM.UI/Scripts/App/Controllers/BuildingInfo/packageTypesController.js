app.controller('packageTypesController', ['$scope', '$filter', 'buildingService', function ($scope, $filter, buildingService) {
    'use strict';

    function setPackageType(packageType) {
        if (packageType) {
            $scope.newPackageType.id = packageType.id;
            $scope.newPackageType.shortDescription = packageType.shortDescription;
            $scope.newPackageType.longDescription = packageType.longDescription;
            $scope.newPackageType.buildingId = packageType.buildingId;
        } else {
            $scope.newPackageType.id = 0;
            $scope.newPackageType.shortDescription = '';
            $scope.newPackageType.longDescription = '';
            $scope.newPackageType.buildingId = 0;
        }
    }

    $scope.packageTypes = [];

    $scope.newPackageType = {
        id: 0,
        shortDescription: '',
        longDescription: '',
        buildingId : 0
    };

    $scope.isEditMode = false;

    $scope.removePackageType = function (type) {
        if (confirm('Are you sure you want to delete this type?')) {
            buildingService.removePackageType(type).then(function() {
                $scope.packageTypes.splice($scope.packageTypes.indexOf(type), 1);
            }, function(error) {
                alert(error);
            });
        }
    }

    $scope.addPackageType = function () {
        buildingService.addPackageType($scope.newPackageType).then(function (result) {
            $scope.packageTypes.push(result.data);
            setPackageType();

        }, function (error) {
            alert(error);
        });
    }

    $scope.updatePackageType = function () {
        buildingService.updatePackageType($scope.newPackageType).then(function () {
            var packageType = $filter('filter')($scope.packageTypes, { id: $scope.newPackageType.id })[0];
            packageType.shortDescription = $scope.newPackageType.shortDescription;
            packageType.longDescription = $scope.newPackageType.longDescription;

            setPackageType();
            $scope.isEditMode = false;

        }, function (error) {
            alert(error);
        });
    }

    $scope.edit = function (packageType) {
        setPackageType(packageType);
        $scope.isEditMode = true;
    }

    $scope.cancelEdition = function () {
        setPackageType();
        $scope.isEditMode = false;
    }

    buildingService.getPackageTypes().then(function (results) {
        $scope.packageTypes = results.data;
    }, function (error) {
        alert(error.data.message);
    });
}]);