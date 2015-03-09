app.factory('apartmentService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {
    'use strict';

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var getApartments = function () {
        return $http.get(serviceBase + 'api/apartment/getApartments').then(function (results) {
            return results;
        });
    };

    var addApartment = function (apartment) {
        return $http.get(serviceBase + 'api/apartment/addApartment', apartment).then(function (result) {
            return result;
        });
    };

    var updateApartment = function (apartment) {
        return $http.get(serviceBase + 'api/apartment/updateApartment', apartment).then(function (result) {
            return result;
        });
    };

    var self = {};
    self.getApartments = getApartments;
    self.addApartment = addApartment;
    self.updateApartment = updateApartment;

    return self;

}]);