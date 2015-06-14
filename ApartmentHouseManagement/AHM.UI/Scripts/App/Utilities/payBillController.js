app.controller('payBillController', ['$scope', '$state', '$stateParams', 'utilitiesService', 'datePickerSettings', 'pages',
    function ($scope, $state, $stateParams, utilitiesService, datePickerSettings, pages) {
        'use strict';

        function init() {
            if ($stateParams.billId) {
                utilitiesService.getShortBillById($stateParams.billId, function (data) {
                    $scope.bill = data;
                    $scope.bill.paidDate = new Date();
                    $scope.bill.paidAmount = $scope.bill.calculatedAmount + $scope.bill.carryOver + $scope.bill.fine;
                });
            }
        }

        $scope.bill = {};
        $scope.datePickerSettings = datePickerSettings;

        $scope.pay = function () {
            utilitiesService.payBill($scope.bill, function () {
                $state.go(pages.billsBoard);
            });
        }

        $scope.cancel = function () {
            $state.go(pages.billsBoard);
        }

        init();
    }
]);