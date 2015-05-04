app.directive('floatNumber', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        scope: {
            ngModel: '='
        },
        link: function (scope) {
            scope.$watch('ngModel', function (newValue, oldValue) {
                if (typeof(newValue) === "undefined" || newValue === '') {
                    return;
                }

                var characters = String(newValue).split("");
                if (characters.length === 0) {
                    return;
                }
                if (characters.length === 1 && (characters[0] === '-' || characters[0] === '.')) {
                    return;
                }
                if (characters.length === 2 && newValue === '-.') {
                    return;
                }
                if (isNaN(newValue)) {
                    scope.ngModel = oldValue;
                }
            });
        }
    };
});