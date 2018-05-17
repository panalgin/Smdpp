
var CSV_RIGHT_CLICK_TEMPLATE = "";

$().ready(function () {
    $.get("inc/controls/right-click-csv-menu.tpl", function (data) {
        if (data) {
            CSV_RIGHT_CLICK_TEMPLATE = data;
        }
    });
});

function showContextMenuForItem(svg, xOff, yOff) {
    var object = $(CSV_RIGHT_CLICK_TEMPLATE);

    var x = svg.css("left");
    var y = svg.css("top");

    $("#board").append(object);

    object.css("top", yOff);
    object.css("left", xOff);
}