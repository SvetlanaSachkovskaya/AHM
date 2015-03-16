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

    var getEventsPerDay = function (callback) {
        httpModule.get('api/journal/getEventsPerDay', null, callback);
    }

    var getEventsPerWeek = function (callback) {
        httpModule.get('api/journal/getEventsPerWeek', null, callback);
    }

    var getEventsPerMonth = function (callback) {
        httpModule.get('api/journal/getEventsPerMonth', null, callback);
    }

    var getEventsPerYear = function (callback) {
        httpModule.get('api/journal/getEventsPerYear', null, callback);
    }

    var self = {};

    self.getAllActiveEvents = getAllActiveEvents;
    self.createEvent = createEvent;
    self.updateEvent = updateEvent;
    self.getEventById = getEventById;
    self.getEventsPerDay = getEventsPerDay;
    self.getEventsPerWeek = getEventsPerWeek;
    self.getEventsPerMonth = getEventsPerMonth;
    self.getEventsPerYear = getEventsPerYear;

    return self;

}]);