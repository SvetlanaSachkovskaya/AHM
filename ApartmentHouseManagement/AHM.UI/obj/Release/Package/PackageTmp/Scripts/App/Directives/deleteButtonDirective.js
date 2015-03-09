app.directive("deleteButton", function() {
    return {
        restrict: 'E',
        scope: {
            action: '&'
        },
        templateUrl: '/Views/Templates/deleteButton.html'
    };
});