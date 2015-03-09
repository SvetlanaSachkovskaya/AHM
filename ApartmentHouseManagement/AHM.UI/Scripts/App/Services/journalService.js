app.factory('journalService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {
    'use strict';

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var getAllEvents = function () {
        return $http.get(serviceBase + 'api/journal/getAll').then(function (results) {
            return results;
        });
    };

    var createEvent = function (event) {
        return $http.post(serviceBase + 'api/journal/add', event).then(function (result) {
            return result;
        });
    }

    var updateEvent = function (event) {
        return $http.post(serviceBase + 'api/journal/update', event).then(function (result) {
            return result;
        });
    }

    var getEventById = function (id) {
        return $http.get(serviceBase + 'api/journal/getById', { params: { id: id } }).then(function (result) {
            return result;
        });
    }

    var getEventsPerDay = function () {
        return $http.get(serviceBase + 'api/journal/getEventsPerDay').then(function (result) {
            return result;
        });
    }

    var getEventsPerWeek = function () {
        return $http.get(serviceBase + 'api/journal/getEventsPerWeek').then(function (result) {
            return result;
        });
    }

    var getEventsPerMonth = function () {
        return $http.get(serviceBase + 'api/journal/getEventsPerMonth').then(function (result) {
            return result;
        });
    }

    var getEventsPerYear = function () {
        return $http.get(serviceBase + 'api/journal/getEventsPerYear').then(function (result) {
            return result;
        });
    }

    var self = {};

    self.getAllEvents = getAllEvents;
    self.createEvent = createEvent;
    self.updateEvent = updateEvent;
    self.getEventById = getEventById;
    self.getEventsPerDay = getEventsPerDay;
    self.getEventsPerWeek = getEventsPerWeek;
    self.getEventsPerMonth = getEventsPerMonth;
    self.getEventsPerYear = getEventsPerYear;

    return self;

}]);