var data = "{0}";
var task = $.parseJSON(unescape(data));

console.log(task);

var page = "";

$.when($.get("inc/controls/pnp-task.tpl", function(dt) {{
    page = $(dt);

}})).then(function(resp1) {{
    createTab("Pnp Çalışması", page);
}});


