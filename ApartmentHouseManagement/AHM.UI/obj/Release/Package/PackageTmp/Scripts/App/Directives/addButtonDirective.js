app.directive("addButton", function () {
    return {
        restrict: 'E',
        scope: {
            action: '&'
        },
        templateUrl: '~/Views/Templates/addButton.html'
    };
});