app.factory('datePickerSettings', [function () {
    'use strict';

    var self = {};

    self.opened = false;

    self.open = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();

        self.opened = true;
    }

    self.monthDateOptions = {
        minMode: 'month',
        maxMode: 'month'
    };

    self.prevMonthMinDate = (new Date()).setMonth((new Date()).getMonth() - 1);

    return self;
}]);