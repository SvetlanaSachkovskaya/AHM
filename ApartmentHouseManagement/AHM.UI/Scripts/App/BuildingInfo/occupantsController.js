app.controller('occupantsController', ['$scope', '$state', 'buildingService', 'pages',
    function ($scope, $state, buildingService, pages) {
        'use strict';

        function init() {
            buildingService.getApartments(function (data) {
                $scope.apartments = data;
            });

            buildingService.getOccupants(function (data) {
                $scope.occupants = data;
            });
        }

        $scope.occupants = [];
        $scope.apartments = [];
        $scope.apartmentId = null;

        $scope.removeOccupant = function (occupant) {
            if (confirm('Are you sure you want to delete this occupant?')) {
                buildingService.removeOccupant(occupant, function () {
                    $scope.occupants.splice($scope.occupants.indexOf(occupant), 1);
                });
            }
        }

        $scope.filterOccupants = function (element) {
            return ($scope.apartmentId === null || element.apartmentId === $scope.apartmentId) ? true : false;
        }

        $scope.edit = function (occupant) {
            $state.go(pages.editOccupant, { occupantId: occupant.id });
        }

        init();
    }
]);