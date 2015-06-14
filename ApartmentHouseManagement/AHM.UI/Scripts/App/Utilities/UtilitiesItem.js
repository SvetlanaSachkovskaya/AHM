app.factory('UtilitiesItem', function() {
    var UtilitiesItem = function(utilitiesClause, utilitiesItem) {

        var self = {};

        self.utilitiesClauseName = utilitiesClause.name;
        self.utilitiesClauseId = utilitiesClause.id;

        if (utilitiesItem) {
            self.quantity = utilitiesItem.quantity;
            self.isChecked = true;
        } else {
            self.quantity = 0;
            self.isChecked = false;
        }

        self.getServerUtilitiesItem = function () {
            var item = utilitiesItem ?
                utilitiesItem :
                    {
                        id: 0,
                        amount: 0,
                        utilitiesClauseId: self.utilitiesClauseId
                    };

            item.quantity = parseFloat(self.quantity);

            return item;
        }

        return self;
    }

    return UtilitiesItem;
});