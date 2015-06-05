function writeLog(text) {

    var elem = document.createElement('br');


    $('#Notifications').append(text).append(elem);
}

$(document).ready(function () {

    $.ajaxSetup({
        statusCode: {
            500: function (jqXHR) {
                $('#Notifications').html(jqXHR.responseText);
            }
        }
    });
});
