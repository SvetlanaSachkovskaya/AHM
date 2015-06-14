﻿app.controller('editApartmentController', ['$scope', '$state', '$stateParams', '$filter', 'buildingService', 'pages',
    function ($scope, $state, $stateParams, $filter, buildingService, pages) {
        'use strict';

        function init() {
            if ($stateParams.apartmentId) {
                buildingService.getApartmentById($stateParams.apartmentId, function (data) {
                    $scope.apartment = data;
                    $scope.isEditMode = true;

                    buildingService.getOccupantsByApartmentId($stateParams.apartmentId, function (occupants) {
                        $scope.occupants = occupants;
                        var owner = $filter('filter')($scope.occupants, { isOwner: true });
                        if (owner.length > 0) {
                            $scope.apartment.ownerId = owner[0].id;
                        }
                    });
                });
            }
        }

        function forceRequiredValidation() {
            if ($scope.apartmentForm.$error.required) {
                $scope.apartmentForm.$error.required.forEach(function (element) {
                    element.$setDirty();
                });
            }
        }

        $scope.apartment = {
            floor: 0,
            number: '',
            square: 0,
            ownerId: 0,
            personalAccount: ''
        };

        $scope.occupants = [];
        $scope.isEditMode = false;

        $scope.create = function () {
            forceRequiredValidation();

            if ($scope.apartmentForm.$valid) {
                buildingService.addApartment($scope.apartment, function () {
                    $state.go(pages.apartments);
                });
            }
        }

        $scope.save = function () {
            forceRequiredValidation();

            if ($scope.apartmentForm.$valid) {
                buildingService.updateApartment($scope.apartment, function () {
                    $state.go(pages.apartments);
                });
            }
        }

        $scope.cancel = function () {
            $state.go(pages.apartments);
        }

        init();
    }
]);