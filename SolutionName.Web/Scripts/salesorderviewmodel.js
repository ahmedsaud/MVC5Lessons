var ObjectState = {
    Unchanged: 0,
    Added: 1,
    Modified: 2,
    Deleted: 3
};

var salesOrderItemMapping = {
    'SalesOrderItems': {
        key: function (salesOrderItem) {
            return ko.utils.unwrapObservable(salesOrderItem.SalesOrderItemId);
        },
        create: function (options) {
            return new SalesOrderItemViewModel(options.data);
        }
    },
    'ChosenItem': {
        key: function (product) {
            return ko.utils.unwrapObservable(product.ProductId);
        },
        create: function(options) {
            return new ProductViewModel(options.data);
        }
    },
    'products': {
        key: function (product) {
            return ko.utils.unwrapObservable(product.ProductId);
        },
        create: function (options) {
            return new ProductViewModel(options.data);
        }
    }
};

var productMapping = {
    'Products': {
        key: function (product) {
            return ko.utils.unwrapObservable(product.ProductId);
        },
        create: function (options) {
            return new ProductViewModel(options.data);
        }
    }
};

SalesOrderItemViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, salesOrderItemMapping, self);

    self.flagSalesOrderItemAsEdited = function () {
        if (self.ObjectState() != ObjectState.Added) {
            self.ObjectState(ObjectState.Modified);
        }

        return true;
    }
}

ProductViewModel = function (data) {
    var self = this;
    this.valueOf = function () {
        return this.ProductId;
    }

    if (data != null) {
        ko.mapping.fromJS(data, {}, self);
    }
};

var dataConverter = function (key, value) {
    if (key == 'RowVersion' && Array.isArray(value)) {
        var str = String.fromCharCode.apply(null, value);
        return btoa(str);
    }

    return value;
}

SalesOrderViewModel = function (data) {
    var self = this;
    var objProductViewModel;
    ko.mapping.fromJS(data, salesOrderItemMapping, self);
    self.products = ko.observableArray();

    self.save = function () {
        $.ajax({
            url: "/Sales/Save/",
            type: "POST",
            data: ko.toJSON(self, dataConverter),
            contentType: "application/json",
            success: function (data) {
                if (data.salesOrderViewModel != null) {
                    ko.mapping.fromJS(data.salesOrderViewModel, salesOrderItemMapping, self);
                }

                if (data.newLocation != null) {
                    window.location = data.newLocation;
                }
            }

        });
    }

    self.flagSalesOrderAsEdited = function () {
        if (self.ObjectState() != ObjectState.Added) {
            self.ObjectState(ObjectState.Modified);
        }

        return true;
    }

    self.addSalesOrderItem = function () {
        var salesOrderItem = new SalesOrderItemViewModel({ SalesOrderItemId: 0, ProductId: 0, Quantity: 0, UnitPrice: 0, ObjectState: ObjectState.Added, ChosenItem: 0, ChosenItemId: 0 });
        self.SalesOrderItems.push(salesOrderItem);
    }

    self.deleteSalesOrderItem = function (salesOrderItem) {
        self.SalesOrderItems.remove(this);

        if (salesOrderItem.SalesOrderItemId() > 0
            && self.SalesOrderItemsToDelete.indexOf(salesOrderItem.SalesOrderItemId()) == -1) {
            self.SalesOrderItemsToDelete.push(salesOrderItem.SalesOrderItemId());
        }
    }

    self.getAllItems = function () {
        var detailLoadProcess = $.ajax({
            url: "/Sales/GetAllProducts/",
            type: "GET",
            contentType: "application/json",
            //async:   false,
            success: function (data) {
            }
        });

        detailLoadProcess.done(function (data) {

            if (data.productViewModels != null) {
                var len = data.productViewModels.length;
                self.products.removeAll();

                for (i = 0; i < len; i++) {
                    objProductViewModel = new ProductViewModel(data.productViewModels[i]);
                    self.products.push(objProductViewModel);
                }
                ko.applyBindings(self);
            }
        });
    };

    self.getAllItems();
}

ko.bindingHandlers.selectControl = {
    init: function (element, valueAccessor) {
        var id = $(element).attr('id');
        if (id != null && id.toLowerCase().indexOf("productcode") >= 0) {
            var value = valueAccessor();

            if (valueAccessor != null) {
                var len = products.length;

                for (i = 0; i < len; i++) {
                    if (products()[i].ProductId == valueAccessor().ProductId) {
                        valueAccessor(products()[i]);
                    }
                }
            }
        }
    },
    update: function (element, valueAccessor) {
        var id = $(element).attr('id');
        if (id != null && id.toLowerCase().indexOf("productcode") >= 0) {
            var value = valueAccessor();

            if (valueAccessor != null) {
                var len = products.length;

                for (i = 0; i < len; i++) {
                    if (products()[i].ProductId == valueAccessor().ProductId) {
                        valueAccessor(products()[i]);
                    }
                }
            }
        }
    }
}

/*
    ko.utils.registerEventHandler(element, "change", function () {
            var value = valueAccessor();
            if (ko.isWriteableObservable(value)) {
                value(element.selectedIndex);
            }
        });
    }
};
/*
$("form").validate({
    submitHandler: function () {
        salesOrderViewModel.save();
    },

    rules: {
        CustomerName: {
            required: true,
            maxlenght: 30
        },
        PONumber: {
            maxlenght: 10
        },
        ProductCode: {
            required: true
        }
    }

})*/