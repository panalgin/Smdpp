var data = "{0}";
var task = $.parseJSON(unescape(data));
var page = "";

$.when($.get("inc/controls/package-list.tpl", function(dt) {{
    page = $(dt);
	var svg = task.data;

	page.find("div#current-svg").html(svg);
	page.find("input#package-name:first").val(task.name.replace(".svg", ""));
}})).then(function(resp1) {{
    createTab("Svg İçe Aktar", page);
}});