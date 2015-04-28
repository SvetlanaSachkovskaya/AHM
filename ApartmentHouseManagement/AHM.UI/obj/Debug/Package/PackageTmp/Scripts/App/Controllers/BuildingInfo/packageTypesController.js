app.controller('packageTypesController', ['$scope', '$filter', 'buildingService', function ($scope, $filter, buildingService) {
    'use strict';

    function forceRequiredValidation() {
        if ($scope.packageTypeForm.$error.required) {
            $scope.packageTypeForm.$error.required.forEach(function (element) {
                element.$setDirty();
            });
        }
    }

    function setPristine() {
        $scope.packageTypeForm.$setPristine();
    }

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
            buildingService.removePackageType(type, function() {
                $scope.packageTypes.splice($scope.packageTypes.indexOf(type), 1);
            });
        }
    }

    $scope.addPackageType = function () {
        forceRequiredValidation();

        if ($scope.packageTypeForm.$valid) {
            buildingService.addPackageType($scope.newPackageType, function (data) {
                $scope.packageTypes.push(data);
                setPackageType();

                setPristine();
            });
        }
    }

    $scope.updatePackageType = function () {
        forceRequiredValidation();

        if ($scope.packageTypeForm.$valid) {
            buildingService.updatePackageType($scope.newPackageType, function () {
                var packageType = $filter('filter')($scope.packageTypes, { id: $scope.newPackageType.id })[0];
                packageType.shortDescription = $scope.newPackageType.shortDescription;
                packageType.longDescription = $scope.newPackageType.longDescription;

                setPackageType();
                $scope.isEditMode = false;
                setPristine();
            });
        }
    }

    $scope.edit = function (packageType) {
        setPackageType(packageType);
        $scope.isEditMode = true;
    }

    $scope.cancelEdition = function () {
        setPackageType();
        $scope.isEditMode = false;
        setPristine();
    }

    buildingService.getPackageTypes(function (data) {
        $scope.packageTypes = data;
    });
}]);