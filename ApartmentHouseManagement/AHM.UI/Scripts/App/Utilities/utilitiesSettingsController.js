app.controller('utilitiesSettingsController', ['$scope', '$state', '$stateParams', 'adminService', 'utilitiesService',
    function ($scope, $state, $stateParams, adminService, utilitiesService) {
        'use strict';

        function init() {
            adminService.getCurrentBuilding(function (data) {
                $scope.settings = {
                    finePercent: data.finePercent,
                    lastPayUtilitiesDay: data.lastPayUtilitiesDay
                };
            });
        }

        $scope.settings = {};

        $scope.save = function () {
            utilitiesService.updateUtilitiesSettings($scope.settings);
        }

        init();
    }
]);