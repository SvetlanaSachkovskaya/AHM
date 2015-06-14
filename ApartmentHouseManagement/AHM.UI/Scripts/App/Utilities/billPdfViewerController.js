app.controller('billPdfViewerController', ['$scope', '$state', '$stateParams', 'PDFViewerService', 'utilitiesService', 'pages',
    function ($scope, $state, $stateParams, pdf, utilitiesService, pages) {
        'use strict';

        function init() {
            if ($stateParams.billId) {
                utilitiesService.getBillPdfPath($stateParams.billId, function (data) {
                    $scope.path = data;
                });
            }
        }

        $scope.viewer = pdf.Instance("viewer");
        $scope.path = '';

        $scope.cancel = function() {
            $state.go(pages.billDetails, { billId: $stateParams.billId });
        }

        init();
    }
]);