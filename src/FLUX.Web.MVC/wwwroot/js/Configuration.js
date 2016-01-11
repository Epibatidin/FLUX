$(document).ready(function () {
    $(".context").submit(function () {
        writeLog("configuration:update");
        $.ajax({
            dataType: "html",
            url: this.action,
            type: "POST",
            data: $(this).serialize(),
            context: this,
            cache: false
        }).done(function (data) {
            $(".loadHere", this).empty().html(data);

            $("#DataContainer").trigger("configuration:changed");
        });
        return false; // do not bubble
    });
});
