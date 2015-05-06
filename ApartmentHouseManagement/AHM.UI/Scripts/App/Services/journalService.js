app.factory('journalService', ['httpModule', function (httpModule) {
    'use strict';

    var getAllActiveEvents = function (callback) {
        httpModule.get('api/journal/getAllActive', null, callback);
    };

    var createEvent = function (event, callback) {
        httpModule.post('api/journal/add', event, callback);
    }

    var updateEvent = function (event, callback) {
        httpModule.post('api/journal/update', event, callback);
    }

    var getEventById = function (id, callback) {
        httpModule.get('api/journal/getById', id, callback);
    }

    var getEventsByDate = function (date, callback) {
        httpModule.get('api/journal/getEventsBydate', { date: date}, callback);
    }

    var self = {};

    self.getAllActiveEvents = getAllActiveEvents;
    self.createEvent = createEvent;
    self.updateEvent = updateEvent;
    self.getEventById = getEventById;
    self.getEventsByDate = getEventsByDate;

    return self;

}]);