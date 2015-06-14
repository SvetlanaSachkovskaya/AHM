app.controller('editOccupantController', ['$scope', '$state', '$stateParams', 'buildingService', 'datePickerSettings', 'pages',
    function ($scope, $state, $stateParams, buildingService, datePickerSettings, pages) {
        'use strict';

        function init() {
            buildingService.getApartments(function (data) {
                $scope.apartments = data;

                if ($stateParams.occupantId) {
                    buildingService.getOccupantById($stateParams.occupantId, function (occupant) {
                        $scope.occupant = occupant;
                        $scope.isEditMode = true;
                    });
                }
            });
        }

        function forceRequiredValidation() {
            if ($scope.occupantForm.$error.required) {
                $scope.occupantForm.$error.required.forEach(function (element) {
                    element.$setDirty();
                });
            }
        }

        $scope.apartments = [];

        $scope.occupant = {
            name: '',
            dateOfBirth: new Date(),
            email: '',
            aparmentId: 0,
            isSubTenant: false,
            isActive : true
        }

        $scope.isEditMode = false;

        $scope.create = function () {
            forceRequiredValidation();

            if ($scope.occupantForm.$valid) {
                buildingService.addOccupant($scope.occupant, function () {
                    $state.go(pages.occupants);
                });
            }
        }

        $scope.save = function () {
            forceRequiredValidation();

            if ($scope.occupantForm.$valid) {
                buildingService.updateOccupant($scope.occupant, function () {
                    $state.go(pages.occupants);
                });
            }
        }

        $scope.cancel = function () {
            $state.go(pages.occupants);
        }

        $scope.datePickerSettings = datePickerSettings;

        init();
    }
]);