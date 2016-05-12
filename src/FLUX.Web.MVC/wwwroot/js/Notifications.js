function writeLog(text) {
    $("#Notifications").append(text+ "<br/>");
}

$(document).ready(function () {

    $.ajaxSetup({
        statusCode: {
            500: function (jqXhr) {
                $('#Notifications').append("<div/>").html(jqXhr.responseText);
            }
        }
    });

    $('#Notifications').on("FLUX:notitfy", function() {
        


    });
});
