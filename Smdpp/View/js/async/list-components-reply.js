var data = "{0}";
var task = $.parseJSON(unescape(data));

console.log("Components: ", task);

var page = "";
var itemTemplate = "";

$.get("inc/parts/component-row.tpl", function (pr) {{
    itemTemplate = pr;

    $.when($.get("inc/controls/component-list.tpl", function (dt) {{
        page = $(dt);
    }})).then(function (resp1) {{
        createTab("Komponent Listesi", page);
    }});
 }});