app.controller('editBillController', ['$scope', '$state', '$stateParams', '$filter', 'utilitiesService', 'buildingService',
    function ($scope, $state, $stateParams, $filter, utilitiesService, buildingService) {
        'use strict';

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

        $scope.bill = {
            id: 0,
            apartmentId: 0,
            date: new Date(),
            utilitiesItems: []
        }

        $scope.apartments = [];
        $scope.utilitiesItems = [];
        $scope.isEditMode = false;

        $scope.create = function () {
            $scope.bill.utilitiesItems = getCheckedItems();
            utilitiesService.addBill($scope.bill).then(
                function () {
                    $state.go('landing.billsBoard');
                },
                function (error) {
                    alert(error);
                });
        }

        $scope.update = function () {
            $scope.bill.utilitiesItems = getCheckedItems();
            utilitiesService.updateBill($scope.bill).then(
                function () {
                    $state.go('landing.billsBoard');
                },
                function (error) {
                    alert(error);
                });
        }

        $scope.openDatePicker  = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.opened = true;
        }

        $scope.dateOptions = {
            minMode: 'month',
            maxMode: 'month'
        };

        utilitiesService.getActiveUtilitiesClauses().then(
            function (typesResult) {
                var types = typesResult.data;
                if ($stateParams.billId) {
                    utilitiesService.getBillById($stateParams.billId).then(
                        function(itemsResult) {
                            $scope.bill = itemsResult.data;
                            $scope.isEditMode = true;

                            initializeUtilitiesItems(types);
                        },
                        function(error) {
                            alert(error);
                        });
                } else {
                    initializeUtilitiesItems(types);
                }
            },
            function (error) {
                alert(error);
            });

        buildingService.getApartments().then(
            function (result) {
                $scope.apartments = result.data;
            },
            function (error) {
                alert(error);
            });
    }
]);