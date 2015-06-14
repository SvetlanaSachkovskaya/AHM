app.directive("tableGrayCheckbox", function () {
    'use strict';

    return {
        restrict: 'E',
        scope: {
            checked: '='
        },
        templateUrl: 'Views/Templates/tableGrayCheckbox.html'
    };
});