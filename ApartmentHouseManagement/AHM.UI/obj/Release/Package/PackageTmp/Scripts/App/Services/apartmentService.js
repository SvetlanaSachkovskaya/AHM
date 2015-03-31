app.factory('apartmentService', ['$http', 'ngAuthSettings', 'httpModule', function ($http, ngAuthSettings, httpModule) {
    'use strict';

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var getApartments = function (callback) {
        httpModule.get('api/apartment/getAll', null, callback);
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