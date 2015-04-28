app.factory('postService', ['httpModule', function (httpModule) {
    'use strict';

    var getPackages = function (callback) {
        httpModule.get('api/post/getAll', null, callback);
    };

    var createPackage = function (pack, callback) {
        httpModule.post('api/post/add', pack, callback);
    }

    var updatePackage = function (pack, callback) {
        httpModule.post('api/post/update', pack, callback);
    }

    var getPackageById = function (id, callback) {
        httpModule.get('api/post/getById', { id: id }, callback);
    }

    var self = {};

    self.getPackages = getPackages;
    self.createPackage = createPackage;
    self.updatePackage = updatePackage;
    self.getPackageById = getPackageById;

    return self;
}]);