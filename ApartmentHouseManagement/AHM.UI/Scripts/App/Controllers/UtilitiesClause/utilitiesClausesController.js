app.controller('utilitiesClausesController', ['$scope', '$state', 'utilitiesService',
    function ($scope, $state, utilitiesService) {
        'use strict';

        $scope.utilitiesClauses = [];

        $scope.edit = function(id) {
            $state.go('landing.editUtilitiesClause', { utilitiesClauseId: id });
        }

        $scope.create = function() {
            $state.go('landing.editUtilitiesClause');
        }

        utilitiesService.getUtilitiesClauses().then(
            function(result) {
                $scope.utilitiesClauses = result.data;
            },
            function(error) {
                alert(error);
            });
    }
]);