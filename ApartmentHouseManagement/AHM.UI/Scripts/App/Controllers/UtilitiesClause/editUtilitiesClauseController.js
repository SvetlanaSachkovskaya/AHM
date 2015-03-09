app.controller('editUtilitiesClauseController', ['$scope', '$state', '$stateParams', 'utilitiesService',
    function ($scope, $state, $stateParams, utilitiesService) {
        'use strict';

        $scope.utilitiesClause = {
            name: '',
            utilitiesClauseType: 0,
            measure: '',
            calculationType: 0,
            subsidizedTariff: 0,
            fullTariff: 0,
            isLimited: false,
            limitForPerson: 0,
            isActive: true
        };

        $scope.utilitiesClauseTypess = [];
        $scope.calculationTypes = [];

        $scope.isEditMode = false;

        $scope.create = function () {
            utilitiesService.addUtilitiesClause($scope.utilitiesClause).then(
                function () {
                    $state.go('landing.utilitiesClauses');
                },
                function (error) {
                    alert(error);
                });
        }

        $scope.save = function () {
            utilitiesService.updateUtilitiesClause($scope.utilitiesClause).then(
                function () {
                    $state.go('landing.utilitiesClauses');
                },
                function (error) {
                    alert(error);
                });
        }

        utilitiesService.getUtilitiesClauseTypes().then(
                function (result) {
                    $scope.utilitiesClauseTypes = result.data;
                    $scope.utilitiesClauseType = $scope.utilitiesClauseTypes[0].id;
                },
                function (error) {
                    alert(error);
                });

        utilitiesService.getCalculationTypes().then(
                function (result) {
                    $scope.calculationTypes = result.data;
                    $scope.calculationType = $scope.calculationTypes[0].id;
                },
                function (error) {
                    alert(error);
                });

        if ($stateParams.utilitiesClauseId) {
            utilitiesService.getUtilitiesClauseById($stateParams.utilitiesClauseId).then(
                function(result) {
                    $scope.utilitiesClause = result.data;
                    $scope.isEditMode = true;
                },
                function(error) {
                    alert(error);
                });
        }
    }
]);