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

			var rotation = component.rotation;
			svgEntity.css({{ 'transform': 'rotate(' + rotation + 'deg)' }});


			page.append(svgEntity);

			var yPos = (component.position.y - offset.y);// - (svgEntity.outerHeight() / 2);
			var xPos = (component.position.x - offset.x);// - (svgEntity.outerWidth() / 2);

			svgEntity.css("top", yPos + "mm");
			svgEntity.css("left", xPos + "mm");



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