var data = "{0}";
var task = $.parseJSON(unescape(data));

console.log(task);

var page = "";
var offset = 0.0;

$.when($.get("inc/controls/pnp-task.tpl", function(dt) {{
    page = $(dt);
	offset = task.offset;

	for(var i = 0; i < task.components.length; i++) {{
		var component = task.components[i];

		if (component.packageId > 0) {{
			var svgData = findPackageOf(component.packageId);
			var svgEntity = $(svgData);

			svgEntity.css("top", (component.position.y + offset.y) + "mm");
			svgEntity.css("left", (component.position.x + offset.x) + "mm");


			page.append(svgEntity);
		}}

	}}



}})).then(function(resp1) {{
    createTab("Pnp Çalışması", page);
}});


function findPackageOf(packageId) {{
	var svgData = "";

	for(var i = 0; i < task.availablePackages.length; i++) {{
		var currentPackage = task.availablePackages[i];

		if (currentPackage.id == packageId) {{
			svgData = currentPackage.data;
			break;
		}}
	}}

	return svgData;
}}