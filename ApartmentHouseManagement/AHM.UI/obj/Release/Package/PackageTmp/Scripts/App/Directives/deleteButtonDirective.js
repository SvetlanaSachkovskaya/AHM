app.directive("deleteButton", function () {
    'use strict';

    return {
        restrict: 'E',
        scope: {
            action: '&'
        },
        templateUrl: 'Views/Templates/deleteButton.html'
    };
});