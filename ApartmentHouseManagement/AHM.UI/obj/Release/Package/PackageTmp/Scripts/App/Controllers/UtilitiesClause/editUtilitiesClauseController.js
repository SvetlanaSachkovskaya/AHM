app.controller('editUtilitiesClauseController', ['$scope', '$state', '$stateParams', 'utilitiesService',
    function ($scope, $state, $stateParams, utilitiesService) {
        'use strict';

        function forceRequiredValidation() {
            if ($scope.clauseForm.$error.required) {
                $scope.clauseForm.$error.required.forEach(function (element) {
                    element.$setDirty();
                });
            }
        }

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
            forceRequiredValidation();

            if ($scope.clauseForm.$valid) {
                utilitiesService.addUtilitiesClause($scope.utilitiesClause, function () {
                    $state.go('landing.utilitiesClauses');
                });
            }
        }

        $scope.save = function () {
            forceRequiredValidation();

            if ($scope.clauseForm.$valid) {
                utilitiesService.updateUtilitiesClause($scope.utilitiesClause, function() {
                    $state.go('landing.utilitiesClauses');
                });
            }
        }

        utilitiesService.getUtilitiesClauseTypes(function (data) {
            $scope.utilitiesClauseTypes = data;
            $scope.utilitiesClauseType = $scope.utilitiesClauseTypes[0].id;
        });

        utilitiesService.getCalculationTypes(function (data) {
            $scope.calculationTypes = data;
            $scope.calculationType = $scope.calculationTypes[0].id;
        });

        if ($stateParams.utilitiesClauseId) {
            utilitiesService.getUtilitiesClauseById($stateParams.utilitiesClauseId, function (data) {
                $scope.utilitiesClause = data;
                $scope.isEditMode = true;
            });
        }
    }
]);