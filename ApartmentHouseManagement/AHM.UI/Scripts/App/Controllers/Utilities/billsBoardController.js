app.controller('billsBoardController', ['$scope', '$state', 'utilitiesService', 'buildingService',
    function ($scope, $state, utilitiesService, buildingService) {
        'use strict';

        $scope.bills = [];
        $scope.apartments = [];

        $scope.dateFilterTypes = {
            all: 'all',
            month: 'month'
        }

        $scope.dateFilter = $scope.dateFilterTypes.all;
        $scope.selectedDate = new Date();

        $scope.selectedApartmentId = null;
        $scope.showPaid = false;

        $scope.showDetails = function (id) {
            $state.go('landing.billDetails', { billId: id });
        }

        $scope.filterByDate = function (dateFilter) {
            $scope.dateFilter = dateFilter;
            $scope.refreshBoard();
        }

        $scope.filterByApartment = function (element) {
            return $scope.selectedApartmentId === null || element.apartmentId === $scope.selectedApartmentId ? true : false;
        };

        $scope.refreshBoard = function () {
            switch ($scope.dateFilter) {
                case $scope.dateFilterTypes.all:
                    utilitiesService.getAllBills($scope.showPaid, function (data) {
                        $scope.bills = data;
                    });
                    break;
                case $scope.dateFilterTypes.month:
                    utilitiesService.getBillsByDate($scope.showPaid, $scope.selectedDate, function (data) {
                        $scope.bills = data;
                    });
                    break;
            }
        }

        $scope.payBill = function (id) {
            $state.go('landing.payBill', { billId: id });
        }

        $scope.editBill = function (id) {
            $state.go('landing.editBill', { billId: id });
        }

        $scope.openDatePicker = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.opened = true;
        }

        $scope.dateOptions = {
            minMode: 'month',
            maxMode: 'month'
        };

        buildingService.getApartments(function (data) {
            $scope.apartments = data;

            $scope.refreshBoard();
        });
    }
]);