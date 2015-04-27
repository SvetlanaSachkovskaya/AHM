app.controller('editBillController', ['$scope', '$state', '$stateParams', '$filter', 'utilitiesService', 'buildingService',
    function ($scope, $state, $stateParams, $filter, utilitiesService, buildingService) {
        'use strict';

        function forceRequiredValidation() {
            if ($scope.billForm.$error.required) {
                $scope.billForm.$error.required.forEach(function (element) {
                    element.$setDirty();
                });
            }
        }

        //todo: move to separate file
        function UtilitiesItem(utilitiesClause, utilitiesItem) {
            var self = {};
            self.utilitiesClauseName = utilitiesClause.name;
            self.utilitiesClauseId = utilitiesClause.id;

            if (utilitiesItem) {
                self.quantity = utilitiesItem.quantity;
                self.isChecked = true;
            } else {
                self.quantity = 0;
                self.isChecked = false;
            }

            self.getServerUtilitiesItem = function () {
                var item = utilitiesItem ?
                    utilitiesItem :
                    {
                        id: 0,
                        amount: 0,
                        utilitiesClauseId: self.utilitiesClauseId
                    };

                item.quantity = parseFloat(self.quantity);

                return item;
            }

            return self;
        }

        function getCheckedItems() {
            var items = [];
            var checkedModels = $filter('filter')($scope.utilitiesItems, { isChecked: true });
            for (var i = 0; i < checkedModels.length; i++) {
                items.push(checkedModels[i].getServerUtilitiesItem());
            }

            return items;
        }

        function initializeUtilitiesItems(types) {
            var j;
            if ($scope.bill.utilitiesItems.length > 0) {
                for (j = 0; j < types.length; j++) {
                    var searchedItems = $filter('filter')($scope.bill.utilitiesItems, { utilitiesClauseId: types[j].id });
                    var item = searchedItems.length > 0 ? searchedItems[0] : null;
                    $scope.utilitiesItems.push(new UtilitiesItem(types[j], item));
                }
            } else {
                for (j = 0; j < types.length; j++) {
                    $scope.utilitiesItems.push(new UtilitiesItem(types[j]));
                }
            }
        }

        function validate() {
            forceRequiredValidation();
            if (!$scope.billForm.$valid) {
                return false;
            }

            var checkedItems = $filter('filter')($scope.utilitiesItems, { isChecked: true });
            if (checkedItems.length <= 0) {
                $scope.isSelectionValidationError = true;
                return false;
            }

            return true;
        }

        $scope.bill = {
            id: 0,
            apartmentId: null,
            date: new Date(),
            utilitiesItems: []
        }

        $scope.apartments = [];
        $scope.utilitiesItems = [];
        $scope.isEditMode = false;
        $scope.isSelectionValidationError = false;

        $scope.create = function () {
            if (validate()) {
                $scope.bill.utilitiesItems = getCheckedItems();
                utilitiesService.addBill($scope.bill, function (data) {
                    $state.go('landing.billDetails', { billId: data.id });
                });
            }
        }

        $scope.update = function () {
            if (validate()) {
                $scope.bill.utilitiesItems = getCheckedItems();
                utilitiesService.updateBill($scope.bill, function () {
                    $state.go('landing.billDetails', { billId: $stateParams.billId });
                });
            }
        }

        $scope.openDatePicker  = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.opened = true;
        }

        $scope.checkUtilitiesItem = function(item) {
            item.isChecked = !item.isChecked;
            if (item.isChecked) {
                $scope.isSelectionValidationError = false;
            }
        }

        $scope.cancel = function() {
            $state.go('landing.billsBoard');
        }

        $scope.dateOptions = {
            minMode: 'month',
            maxMode: 'month'
        };

        utilitiesService.getActiveUtilitiesClauses(function (typesData) {
            var types = typesData;
            if ($stateParams.billId) {
                utilitiesService.getFullBillById($stateParams.billId, function (itemsData) {
                    $scope.bill = itemsData;
                    $scope.isEditMode = true;

                    initializeUtilitiesItems(types);
                });
            } else {
                initializeUtilitiesItems(types);
            }
        });

        buildingService.getApartments(function (data) {
            $scope.apartments = data;
        });
    }
]);