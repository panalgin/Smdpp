var data = "{0}";
var task = $.parseJSON(unescape(data));

var page = "";

$.when($.get("inc/controls/pnp-task.tpl", function(dt) {{
    page = $(dt);

	for(var i = 0; i < task["smtParts"].length; i++) {{
		var packageId = task["smtParts"][i].referenceId;
		var componentName = task["smtParts"][i].value;

		//var pnpPartInfo = windowsApp.getPnpPartInfo(packageId, componentName);

		//console.log(pnpPartInfo);
	}}


}})).then(function(resp1) {{
    createTab("Pnp Çalışması", page);
}});


