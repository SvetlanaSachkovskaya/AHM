app.factory('utilitiesService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {
    'use strict';

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var getUtilitiesClauses = function () {
        return $http.get(serviceBase + 'api/utilitiesClause/getAll').then(function (results) {
            return results;
        });
    };

    var getActiveUtilitiesClauses = function () {
        return $http.get(serviceBase + 'api/utilitiesClause/getActive').then(function (results) {
            return results;
        });
    };

    var addUtilitiesClause = function (utilitiesClause) {
        return $http.post(serviceBase + 'api/utilitiesClause/add', utilitiesClause).then(function (result) {
            return result;
        });
    }

    var updateUtilitiesClause = function (utilitiesClause) {
        return $http.post(serviceBase + 'api/utilitiesClause/update', utilitiesClause).then(function (result) {
            return result;
        });
    }

    var getUtilitiesClauseById = function (id) {
        return $http.get(serviceBase + 'api/utilitiesClause/getById', { params: { id: id } }).then(function (result) {
            return result;
        });
    }

    var getBills = function (dateInterval, showPaid) {
        if (showPaid) {
            return $http.get(serviceBase + 'api/bill/getAll', { params: { dateInterval: dateInterval } }).then(function (results) {
                return results;
            });
        }

        return $http.get(serviceBase + 'api/bill/getUnpaid', { params: { dateInterval: dateInterval } }).then(function (results) {
            return results;
        });
    };

    var getBillDateIntervals = function() {
        return $http.get(serviceBase + 'api/bill/getBillDateIntervals').then(function (results) {
            return results;
        });
    }

    var addBill = function (utilitiesBill) {
        return $http.post(serviceBase + 'api/bill/add', utilitiesBill).then(function (result) {
            return result;
        });
    }

    var updateBill = function (utilitiesBill) {
        return $http.post(serviceBase + 'api/bill/update', utilitiesBill).then(function (result) {
            return result;
        });
    }

    var getBillById = function (id) {
        return $http.get(serviceBase + 'api/bill/getById', { params: { id: id } }).then(function (result) {
            return result;
        });
    }

    var getUtilitiesClauseTypes = function () {
        return $http.get(serviceBase + 'api/utilitiesClause/getUtilitiesClauseTypes').then(function (result) {
            return result;
        });
    }

    var getCalculationTypes = function () {
        return $http.get(serviceBase + 'api/utilitiesClause/getCalculationTypes').then(function (result) {
            return result;
        });
    }

    var self = {};

    self.getUtilitiesClauses = getUtilitiesClauses;
    self.getActiveUtilitiesClauses = getActiveUtilitiesClauses;
    self.addUtilitiesClause = addUtilitiesClause;
    self.updateUtilitiesClause = updateUtilitiesClause;
    self.getUtilitiesClauseById = getUtilitiesClauseById;
    self.getUtilitiesClauseTypes = getUtilitiesClauseTypes;
    self.getCalculationTypes = getCalculationTypes;
    self.getBillDateIntervals = getBillDateIntervals;

    self.getBills = getBills;
    self.addBill = addBill;
    self.updateBill = updateBill;
    self.getBillById = getBillById;

    return self;
}]);