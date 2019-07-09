var viewModel = kendo.observable({
    isVisible: true,
    onSave: function (e) {
        //console.info(e);
    },
    products: new kendo.data.DataSource({
        schema: {
            model: {
                id: "Id",
                fields: {
                    ProductName: { type: "string" },
                    UnitPrice: { type: "number" },
                    UnitsInStock: { type: "number" }
                }
            }
        },
        batch: true,
        transport: {
            read: {
                url: "http://localhost:49200/Home/GetProducts",
                dataType: "json",
                type: "POST"
            },
            update: {
                url: "http://localhost:49200/Home/update",
                dataType: "json",
                type: "POST",
                contentType: "application/json"
            },
            create: {
                url: "http://localhost:49200/Home/create",
                dataType: "json",
                type: "POST",
                contentType: "application/json"
            },
            parameterMap: function (options, operation) {
                //if (operation !== "read" && options.models) {
                //console.info(kendo.stringify(options.models));
                //return { models: kendo.stringify(options.models) };
                //}

                if (operation != "read") {
                    // web service method parameters need to be send as JSON. The Create, Update and Destroy methods have a "products" parameter.
                    return JSON.stringify(options.models)
                } else {
                    return JSON.stringify(options);
                }

            }
        }
    })
});