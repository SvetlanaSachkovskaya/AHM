app.controller('billPdfViewerController', ['$scope', '$state', '$stateParams', 'PDFViewerService', 'utilitiesService',
    function ($scope, $state, $stateParams, pdf, utilitiesService) {
        'use strict';

        $scope.viewer = pdf.Instance("viewer");

        $scope.path = '';

        $scope.cancel = function() {
            $state.go('landing.billDetails', { billId: $stateParams.billId });
        }

        if ($stateParams.billId) {
            utilitiesService.getBillPdfPath($stateParams.billId, function (result) {
                $scope.path = result.data;
            });
        }
    }
]);