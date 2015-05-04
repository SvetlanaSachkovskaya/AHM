app.directive('integerNumber', function() {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelController) {
            if (!ngModelController) {
                return;
            }

            ngModelController.$parsers.push(function (value) {
                var clean = value.replace(/[^0-9]+/g, '');
                if (value !== clean) {
                    ngModelController.$setViewValue(clean);
                    ngModelController.$render();
                }
                return clean;
            });

            element.bind('keypress', function(event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    };
});