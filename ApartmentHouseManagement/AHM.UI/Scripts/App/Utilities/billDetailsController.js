app.controller('billDetailsController', ['$scope', '$state', '$stateParams', 'utilitiesService', 'pages',
    function ($scope, $state, $stateParams, utilitiesService, pages) {
        'use strict';

        function init() {
            if ($stateParams.billId) {
                utilitiesService.getFullBillById($stateParams.billId, function (data) {
                    $scope.bill = data;
                });
            }
        }

        $scope.bill = {};

        $scope.edit = function () {
            $state.go(pages.editBill, { billId: $scope.bill.id });
        }

        $scope.cancel = function () {
            $state.go(pages.billsBoard);
        }

        $scope.sendEmail = function () {
            utilitiesService.sendEmail($scope.bill, function () {
                $scope.bill.isEmailSent = true;
            });
        }

        $scope.viewPdf = function () {
            $state.go(pages.viewPdf, { billId: $scope.bill.id });
        }

        init();
    }
]);