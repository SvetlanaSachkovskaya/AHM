app.controller('billsBoardController', ['$scope', '$state', 'utilitiesService', 'buildingService',
    function ($scope, $state, utilitiesService, buildingService) {
        'use strict';

        $scope.bills = [];
        $scope.apartments = [];

        $scope.dateInterval = { value: 0 };
        $scope.dateIntervals = [];

        $scope.selectedApartmentId = 0;
        $scope.showPaid = false;

        $scope.create = function () {
            $state.go('landing.editBill');
        }

        $scope.showDetails = function (id) {
            $state.go('landing.billDetails', { billId: id });
        }

        $scope.setDateInterval = function(intervalId) {
            $scope.dateInterval.value = intervalId;
            $scope.refreshBoard();
        }

        $scope.filterByApartment = function (element) {
            return $scope.selectedApartmentId === 0 || element.apartmentId === $scope.selectedApartmentId ? true : false;
        };

        $scope.refreshBoard = function () {
            utilitiesService.getBills($scope.dateInterval.value, $scope.showPaid, function (data) {
                $scope.bills = data;
            });
        }

        $scope.payBill = function (bill) {
            bill.isPaid = true;
            utilitiesService.updateBill(bill, function () {
                if (!$scope.showPaid) {
                    $scope.bills.splice($scope.bills.indexOf(bill), 1);
                }
            });
        }

        utilitiesService.getBillDateIntervals(function (data) {
            $scope.dateIntervals = data;
            $scope.dateInterval.value = $scope.dateIntervals[0].id;

            $scope.refreshBoard();
        });

        buildingService.getApartments(function (data) {
            $scope.apartments = data;
            $scope.apartments.splice(0, 0, { id: 0, number: "All apartments" });
        });
    }
]);