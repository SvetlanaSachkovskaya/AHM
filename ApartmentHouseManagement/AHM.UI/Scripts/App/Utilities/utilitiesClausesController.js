app.controller('utilitiesClausesController', ['$scope', '$state', 'utilitiesService', 'pages',
    function ($scope, $state, utilitiesService, pages) {
        'use strict';

        function init() {
            utilitiesService.getUtilitiesClauses(function (data) {
                $scope.utilitiesClauses = data;
            });
        }

        $scope.utilitiesClauses = [];

        $scope.edit = function(id) {
            $state.go(pages.editUtilitiesClause, { utilitiesClauseId: id });
        }

        init();
    }
]);