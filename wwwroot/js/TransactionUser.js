$(document).ready(function () {
    $("#TransactionUserName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: document.location.origin + "/Funds/CreateData",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { value: item.id, label: item.name }
                    }));
                }
            });
        },
        select: function (event, ui) {
            event.preventDefault();
            $("#TransactionUserName").val(ui.item.label);
            $("#zfunds_TransactionUserID").val(ui.item.value);
        },
        focus: function (event, ui) {
            event.preventDefault();
            $("#TransactionUserName").val(ui.item.label);
        }
    });
    $.validator.setDefaults({ ignore: null });
});