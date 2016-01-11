function writeLog(text) {

    var elem = document.createElement("br");


    $("#Notifications").append(text).append(elem);
}

$(document).ready(function () {

    $.ajaxSetup({
        statusCode: {
            500: function (jqXhr) {
                $('#Notifications').html(jqXhr.responseText);
            }
        }
    });

    $('#Notifications').on("FLUX:notitfy", function() {
        


    });
});
