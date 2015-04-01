app.directive('access', ['authenticationService', function (authenticationService) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            if (attrs.access.length < 0) {
                return;
            }

            var roles = attrs.access.split(',');
            if (authenticationService.hasPermissions(roles)) {
                element.removeClass('hidden');
            } else {
                element.addClass('hidden');
            }                
        }
    };
}]);