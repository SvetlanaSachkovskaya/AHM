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

        $scope.senEmail = function () {
            $state.go('landing.billDetails', { billId: id });
        }

        if ($stateParams.billId) {
            utilitiesService.getBillById($stateParams.billId).then(
            function (result) {
                $scope.bill = result.data;
            },
            function (error) {
                alert(error);
            });
        }
    }
]);