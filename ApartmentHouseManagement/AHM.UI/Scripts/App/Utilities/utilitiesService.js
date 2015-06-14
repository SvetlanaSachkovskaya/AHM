app.factory('utilitiesService', ['httpModule', function (httpModule) {
    'use strict';

    var self = {};

    self.getUtilitiesClauses = function (callback) {
        httpModule.get('api/utilitiesClause/getAll', null, callback);
    };

    self.getActiveUtilitiesClauses = function (callback) {
        httpModule.get('api/utilitiesClause/getActive', null, callback);
    };

    self.addUtilitiesClause = function (utilitiesClause, callback) {
        httpModule.post('api/utilitiesClause/add', utilitiesClause, callback);
    }

    self.updateUtilitiesClause = function (utilitiesClause, callback) {
        httpModule.post('api/utilitiesClause/update', utilitiesClause, callback);
    }

    self.getUtilitiesClauseById = function (id, callback) {
        httpModule.get('api/utilitiesClause/getById', { id: id }, callback);
    }

    self.getUtilitiesClauseTypes = function (callback) {
        httpModule.get('api/utilitiesClause/getUtilitiesClauseTypes', null, callback);
    }

    self.getCalculationTypes = function (callback) {
        httpModule.get('api/utilitiesClause/getCalculationTypes', null, callback);
    }

    self.getAllBills = function (showPaid, callback) {
        httpModule.get('api/bill/getAllBills', { showOnlyUnpaid: !showPaid }, callback);
    };

    self.getBillsByDate = function (showPaid, date, callback) {
        if (showPaid) {
            httpModule.get('api/bill/GetBillsByDate', { date: date }, callback);
        } else {
            httpModule.get('api/bill/GetUnpaidBillsByDate', { date: date }, callback);
        }
    };

    self.addBill = function (utilitiesBill, callback) {
        httpModule.post('api/bill/add', utilitiesBill, callback);
    }

    self.updateBill = function (utilitiesBill, callback) {
        httpModule.post('api/bill/update', utilitiesBill, callback);
    }

    self.updateUtilitiesSettings = function (settings) {
        httpModule.post('api/building/updateUtilitiesSettings', settings);
    }

    self.payBill = function (utilitiesBill, callback) {
        httpModule.post('api/bill/pay', utilitiesBill, callback);
    }

    self.getFullBillById = function (id, callback) {
        httpModule.get('api/bill/getFullById', { id: id }, callback);
    }

    self.getShortBillById = function (id, callback) {
        httpModule.get('api/bill/getShortById', { id: id }, callback);
    }

    self.getBillPdfPath = function (billId, callback) {
        httpModule.get('api/bill/getBillPdfPath', { billId: billId }, callback);
    }

    self.sendEmail = function (bill, callback) {
        httpModule.post('api/bill/sendEmail', bill, callback);
    }

    return self;
}]);