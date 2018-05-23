var data = "{0}";
var task = $.parseJSON(unescape(data));
var page = "";
var itemTemplate = "";

$.get("inc/parts/package-row.tpl", function(pr) {{
	itemTemplate = pr;

	$.when($.get("inc/controls/package-list.tpl", function(dt) {{
		page = $(dt);

		for (var i = 0; i < task.length; i++) {{
			var item = task[i];


			var id = item.id;
			var name = item.name;
			var svg = item.data;
			var template = $(itemTemplate);

			template.find("div[id^='name-']").prop("id", "name-" + id).html(name);
			template.find("div[id^='svg-con-']").prop("id", "svg-con-" + id).html(svg);

			page.append(template);
		}}
	}})).then(function(resp1) {{
		createTab("Kılıf Listesi", page);
	}});
}});