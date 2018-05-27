var data = "{0}";
var task = $.parseJSON(unescape(data));

var template = "";

$.get("inc/parts/feeder-slot.tpl", function (data) {{
    template = data;

    for (var i = 0; i < task.length; i++) {{
        var slot = task[i];

        var item = $(template);

        item.prop("id", "feeder-" + slot.id);
        item.find("span.referenceId").html(slot.id);
        item.find("span.value").html("");
        item.find("span.status").html("");

        item.addClass("mm" + slot.width);

        $("div#feeders").append(item);
    }}
}});