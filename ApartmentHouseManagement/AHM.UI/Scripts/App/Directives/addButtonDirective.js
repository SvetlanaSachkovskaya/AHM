app.directive("addButton", function () {
    'use strict';

    return {
        restrict: 'E',
        replace: true,
        scope: {
            text: '@',
            action: '&'
        },
        templateUrl: 'Views/Templates/addButton.html'
    };
});