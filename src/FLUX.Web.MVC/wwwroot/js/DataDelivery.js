
$("#DataContainer").on("configuration:changed", function() {
    var container = $(this);
    var url = container.data("url");
    $.get(container.data("url"))
        .always(function() {
            writeLog('StartingProgress');
        })
        .done(function(data) {
            container.html(data);
        });
});

/*
var count = 0;
var timer = setInterval(function () {
    clearInterval(timer);
    count++;
    if (count < 10) {
        $.get('@Url.Action("Index", "DataDelivery")')
            .done(function (data) {
                $('#DataContainer').html(data);
            });
    }
    else {
        clearInterval(timer);
    }
}, 200);
*/