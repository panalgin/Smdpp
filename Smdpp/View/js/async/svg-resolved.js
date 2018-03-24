var data = "{0}";
var task = $.parseJSON(unescape(data));
var page = "";

$.when($.get("inc/controls/svg-imported.tpl", function(dt) {{
    page = $(dt);
    page.find("div#current-svg").append(task.data);

}})).then(function(resp1) {{
    createTab("Svg İçe Aktar", page);
}});