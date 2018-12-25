$(document).ready(function () {
    $("#MeetingTitle").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "https://localhost:80/FilesUpload/CreateData",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { value: item.id, label: item.meetingTitle }
                    }));
                }
            });
        },
        select: function (event, ui) {
            event.preventDefault();
            $("#MeetingTitle").val(ui.item.label);
            $("#MeetingID").val(ui.item.value);
            $("#FileUpload_MeetingID").val(ui.item.value);
        },
        focus: function (event, ui) {
            event.preventDefault();
            $("#MeetingTitle").val(ui.item.label);
        }
    });
    $("#MeetingTitle").on('change', function () {
        if ($(this).val() == "") {
            $("#MeetingID").val("");
            $("#FileUpload_MeetingID").val("");
        }
    });
});