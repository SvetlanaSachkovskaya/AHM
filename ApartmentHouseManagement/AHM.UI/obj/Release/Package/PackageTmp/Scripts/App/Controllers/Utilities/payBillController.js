app.controller('payBillController', ['$scope', '$state', '$stateParams', 'utilitiesService',
    function ($scope, $state, $stateParams, utilitiesService) {
        'use strict';

        $scope.bill = {};

        $scope.datePickerSettings = {
            open: function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                $scope.opened = true;
            }
        }

        $scope.pay = function () {
            utilitiesService.payBill($scope.bill, function () {
                $state.go('landing.billsBoard');
            });
        }

        $scope.cancel = function () {
            $state.go('landing.billsBoard');
        }

        if ($stateParams.billId) {
            utilitiesService.getShortBillById($stateParams.billId, function (data) {
                $scope.bill = data;
                $scope.bill.paidDate = new Date();
                $scope.bill.paidAmount = $scope.bill.totalAmount;
            });
        }
    }
]);