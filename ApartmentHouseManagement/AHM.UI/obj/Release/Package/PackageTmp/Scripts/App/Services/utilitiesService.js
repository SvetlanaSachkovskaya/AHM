app.factory('utilitiesService', ['httpModule', function (httpModule) {
    'use strict';

    var getUtilitiesClauses = function (callback) {
        httpModule.get('api/utilitiesClause/getAll', null, callback);
    };

    var getActiveUtilitiesClauses = function (callback) {
        httpModule.get('api/utilitiesClause/getActive', null, callback);
    };

    var addUtilitiesClause = function (utilitiesClause, callback) {
        httpModule.post('api/utilitiesClause/add', utilitiesClause, callback);
    }

    var updateUtilitiesClause = function (utilitiesClause, callback) {
        httpModule.post('api/utilitiesClause/update', utilitiesClause, callback);
    }

    var getUtilitiesClauseById = function (id, callback) {
        httpModule.get('api/utilitiesClause/getById', { id: id }, callback);
    }

    var getBills = function (dateInterval, showPaid, callback) {
        if (showPaid) {
            httpModule.get('api/bill/getAll', { dateInterval: dateInterval }, callback);
        } else {
            httpModule.get('api/bill/getUnpaid', { dateInterval: dateInterval }, callback);
        }
    };

    var getBillDateIntervals = function (callback) {
        httpModule.get('api/bill/getBillDateIntervals', null, callback);
    }

    var addBill = function (utilitiesBill, callback) {
        httpModule.post('api/bill/add', utilitiesBill, callback);
    }

    var updateBill = function (utilitiesBill, callback) {
        httpModule.post('api/bill/update', utilitiesBill, callback);
    }

    var getBillById = function (id, callback) {
        httpModule.get('api/bill/getById', { id: id }, callback);
    }

    var getBillPdfPath = function (billId, callback) {
        httpModule.get('api/bill/getBillPdfPath', { billId: billId }, callback);
    }

    var sendEmail = function (bill, callback) {
        httpModule.post('api/bill/sendEmail', bill, callback);
    }

    var getUtilitiesClauseTypes = function (callback) {
        httpModule.get('api/utilitiesClause/getUtilitiesClauseTypes', null, callback);
    }

    var getCalculationTypes = function (callback) {
        httpModule.get('api/utilitiesClause/getCalculationTypes', null, callback);
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
    self.getBillPdfPath = getBillPdfPath;
    self.sendEmail = sendEmail;

    return self;
}]);