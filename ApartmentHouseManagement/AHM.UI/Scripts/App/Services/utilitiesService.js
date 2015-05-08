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

    var getAllBills = function (showPaid, callback) {
        httpModule.get('api/bill/getAllBills', { showOnlyUnpaid: !showPaid }, callback);
    };

    var getBillsByDate = function (showPaid, date, callback) {
        if (showPaid) {
            httpModule.get('api/bill/GetBillsByDate', { date: date }, callback);
        } else {
            httpModule.get('api/bill/GetUnpaidBillsByDate', { date: date }, callback);
        }
    };

    var addBill = function (utilitiesBill, callback) {
        httpModule.post('api/bill/add', utilitiesBill, callback);
    }

    var updateBill = function (utilitiesBill, callback) {
        httpModule.post('api/bill/update', utilitiesBill, callback);
    }

    var payBill = function (utilitiesBill, callback) {
        httpModule.post('api/bill/pay', utilitiesBill, callback);
    }

    var getFullBillById = function (id, callback) {
        httpModule.get('api/bill/getFullById', { id: id }, callback);
    }

    var getShortBillById = function (id, callback) {
        httpModule.get('api/bill/getShortById', { id: id }, callback);
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

    self.getAllBills = getAllBills;
    self.getBillsByDate = getBillsByDate;
    self.addBill = addBill;
    self.updateBill = updateBill;
    self.payBill = payBill;
    self.getFullBillById = getFullBillById;
    self.getShortBillById = getShortBillById;
    self.getBillPdfPath = getBillPdfPath;
    self.sendEmail = sendEmail;

    return self;
}]);