app.factory('journalService', ['httpModule', function (httpModule) {
    'use strict';

    var self = {};

    self.getAllActiveEvents = function (callback) {
        httpModule.get('api/journal/getAllActive', null, callback);
    };

    self.createEvent = function (event, callback) {
        httpModule.post('api/journal/add', event, callback);
    }

    self.updateEvent = function (event, callback) {
        httpModule.post('api/journal/update', event, callback);
    }

    self.getEventById = function (id, callback) {
        httpModule.get('api/journal/getById', id, callback);
    }

    self.getEventsByDate = function (date, callback) {
        httpModule.get('api/journal/getEventsBydate', { date: date}, callback);
    }

    return self;
}]);