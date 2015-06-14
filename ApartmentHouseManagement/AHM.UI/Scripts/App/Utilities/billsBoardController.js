app.controller('billsBoardController', ['$scope', '$state', 'utilitiesService', 'buildingService', 'pages', 'datePickerSettings',
    function ($scope, $state, utilitiesService, buildingService, pages, datePickerSettings) {
        'use strict';

        function init() {
            buildingService.getApartments(function (data) {
                $scope.apartments = data;

                $scope.refreshBoard();
            });
        }

        $scope.bills = [];
        $scope.apartments = [];

        $scope.dateFilterTypes = {
            all: 'all',
            month: 'month'
        }

        $scope.dateFilter = $scope.dateFilterTypes.all;
        $scope.selectedDate = new Date();
        $scope.datePickerSettings = datePickerSettings;

        $scope.selectedApartmentId = null;
        $scope.showPaid = false;

        $scope.showDetails = function (id) {
            $state.go(pages.billDetails, { billId: id });
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
            $state.go(pages.payBill, { billId: id });
        }

        $scope.editBill = function (id) {
            $state.go(pages.editBill, { billId: id });
        }

        init();
    }
]);