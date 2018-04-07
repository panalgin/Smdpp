var data = "{0}";
var task = $.parseJSON(unescape(data));

console.log(task);

var page = "";

$.when($.get("inc/controls/pnp-task.tpl", function(dt) {{
    page = $(dt);

	for(var i = 0; i < task.components.length; i++) {{
		var component = task.components[i];

		if (component.packageId > 0) {{
			var svgData = findPackageOf(component.packageId);
		
			console.log(component);
			console.log(svgData);

			page.append($(svgData));
		}}

	}}



}})).then(function(resp1) {{
    createTab("Pnp Çalışması", page);
}});


function findPackageOf(packageId) {{
	var svgData = "Asd";

	for(var i = 0; i < task.availablePackages.length; i++) {{
		var currentPackage = task.availablePackages[i];

		if (currentPackage.id == packageId) {{
			svgData = currentPackage.data;
			break;
		}}
	}}

	return svgData;
}}