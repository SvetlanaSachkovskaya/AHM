app.factory('postService', ['httpModule', function (httpModule) {
    'use strict';

    var self = {};

    self.getPackages = function (callback) {
        httpModule.get('api/post/getAll', null, callback);
    };

    self.createPackage = function (pack, callback) {
        httpModule.post('api/post/add', pack, callback);
    };
    self.updatePackage = function (pack, callback) {
        httpModule.post('api/post/update', pack, callback);
    };

    self.getPackageById = function (id, callback) {
        httpModule.get('api/post/getById', { id: id }, callback);
    };

    return self;
}]);