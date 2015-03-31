app.controller('billPdfViewerController', ['$scope', '$state', '$stateParams', 'PDFViewerService', 'utilitiesService',
    function ($scope, $state, $stateParams, pdf, utilitiesService) {
        'use strict';

        $scope.viewer = pdf.Instance("viewer");

        $scope.path = '';

        if ($stateParams.billId) {
            utilitiesService.getBillPdfPath($stateParams.billId).then(
            function (result) {
                $scope.path = result.data;
            },
            function (error) {
                alert(error);
            });
        }
    }
]);