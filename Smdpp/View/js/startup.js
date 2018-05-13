
var CSV_RIGHT_CLICK_TEMPLATE = "";

$().ready(function () {
    $.get("inc/controls/right-click-csv-menu.tpl", function (data) {
        if (data) {
            CSV_RIGHT_CLICK_TEMPLATE = data;
        }
    });
});

function showContextMenuForItem(svg) {
    var object = $(CSV_RIGHT_CLICK_TEMPLATE);

    var x = svg.offset().left;
    var y = svg.offset().top;


    var offset = svg.offset();
    var pos = svg.position();

    console.log(offset);
    console.log(pos);
    console.log(svg);

    $("#pnp-editor").append(object);
    object.css("left", x + "px");
    object.css("top", y + "px");
}