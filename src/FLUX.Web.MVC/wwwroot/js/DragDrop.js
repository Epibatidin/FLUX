var pattern = /[0-9]+/g;
function readId(trElement) {
    var nameOfIdField = $('.idHolder', trElement).attr('name');

    var ids = nameOfIdField.match(pattern);

    for (var i = 0; i < ids.length; i++) {
        ids[i] = parseInt(ids[i]);
    }

    return ids;
}

function rewriteNames(trElement, ids) {

    // select all input elements 

    $('input', trElement).each(function () {

        var runner = 0;
        this.name = this.name.replace(pattern, function () {
            var id = ids[runner];
            runner++;
            return id;
        });
    });
}


function sortEnd(event, ui) {
    var r = this;
    var ids;

    // check if first element - we take the element after as id provider 
    var prev = ui.item.prev();
    if (prev.length == 0)
    {
        var next = ui.item.next();
        ids = readId(next);
        ids[ids.length - 1] = 0;
    }
    else // check if middle or last element - we take the element before as id provider 
    {
        ids = readId(prev);
        ids[ids.length - 1] = ids[ids.length - 1] + 1;
    }

    // ich hab jetzt die id des ersten elements 
    // ich renne jetzt so lange nach hinten bis es kein hinten mehr gibt 
    var runner = ui.item;
    while (true) {
        rewriteNames(runner, ids);
        ids[ids.length - 1] = ids[ids.length - 1] + 1;
        runner = runner.next();
        if (runner.length == 0) break;
    }
}

function sortStart(event, ui) {
    var nameOfIdField = $('.idHolder', ui.item).attr('name');
    
    var ids = nameOfIdField.match(pattern);

    ui.item.data('ids', ids);
}


$(document).ready(function () {
    var wrapper = $(".CDWrapper");

    wrapper.sortable({
        revert: false,
        items: "tr",
        axis: 'y',
        placeholder: "dragPlaceHolder",
        tolerance: "pointer",
        cursor: "move",
        stop: sortEnd,
        start: sortStart,
    });
    //wrapper.disableSelection();
});