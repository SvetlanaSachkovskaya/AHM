app.controller('apartmentsController', ['$scope', '$state', '$filter', 'buildingService', 'pages',
    function ($scope, $state, $filter, buildingService, pages) {
        'use strict';

        function init() {
            buildingService.getApartments(function (data) {
                $scope.apartments = data;
            });
        }

        $scope.apartments = [];

        $scope.removeApartment = function (apartment) {
            if (confirm('Are you sure you want to delete this apartment?')) {
                buildingService.removeApartment(apartment, function () {
                    $scope.apartments.splice($scope.apartments.indexOf(apartment), 1);
                });
            }
        }

        $scope.add = function () {
            $state.go(pages.editApartment);
        }

        $scope.edit = function (id) {
            $state.go(pages.editApartment, { apartmentId: id });
        }

        init();
    }
]);