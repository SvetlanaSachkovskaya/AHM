app.controller('billDetailsController', ['$scope', '$state', '$stateParams', 'utilitiesService',
    function ($scope, $state, $stateParams, utilitiesService) {
        'use strict';

        $scope.bill = {};

        $scope.edit = function () {
            $state.go('landing.editBill', {billId: $scope.bill.id});
        }

        $scope.cancel = function () {
            $state.go('landing.billsBoard');
        }

        $scope.sendEmail = function () {
            utilitiesService.sendEmail($scope.bill, function () {
                $scope.bill.isEmailSent = true;
            });
        }

        $scope.viewPdf = function () {
            $state.go('landing.viewPdf', { billId: $scope.bill.id });
        }

        if ($stateParams.billId) {
            utilitiesService.getBillById($stateParams.billId, function (data) {
                $scope.bill = data;
            });
        }
    }
]);