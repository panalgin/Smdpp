var data = "{0}";
var task = $.parseJSON(unescape(data));

console.log(task);

//1 = diktörgen
//2 = daire
//3 = kare

var totalTools = task["tools"].length;
var totalEntries = task["entries"].length;

for(var i = 0; i < totalTools; i++) {{
	for(var j = 0; j < totalEntries; j++) {{
		//console.log(task["entries"][j]);

		var entry = task["entries"][j];
		var tool = task["tools"][i];

		if (entry.toolId == tool.id) {{
			printEntry(entry, tool);
		}}
	}}
}}

$("div#editor").css("width", convertMMToPx(task.width));
$("div#editor").css("height", convertMMToPx(task.height));

function printEntry(entry, tool) {{
	var top = convertMMToPx(entry.y);
	var left = convertMMToPx(entry.x);

	var svg = null;
	var draw = null;

	var	right = left;
	var bottom = top;

	if (tool.toolType == 1) {{
		var width = convertMMToPx(tool.width);
		var height = convertMMToPx(tool.height);

		top -= (height / 2);
		left -= (width / 2);

		draw = SVG('editor').size(width, height);
		var rect = draw.rect(width, height).fill('#f06');
	}}
	/*else if (tool.toolType == 2) {{
		var dia = convertMMToPx(tool.diameter);

		draw = SVG('editor').size(dia, dia);
		var circle = draw.circle(dia).fill('#f06');
	}}
	else if (tool.toolType == 3) {{
		var side = convertMMToPx(tool.side);

		draw = SVG('editor').size(side, side);
		var square = draw.rect(side, side).fill('#f06');
	}}*/

	if (draw != null) {{
		var id = draw.node.id;

		svg = $("svg#" + id);
		svg.css("position", "absolute");
		svg.css("top", top);
		svg.css("left", left);
	}}
}}

//1mm = 3.779527559055px
function convertMMToPx(millimeters) {{
	return Math.round((millimeters * 3.779527559055) * 100) / 100;
}}

//1th = 0.0254m
function convertThToMm(thousands) {{
	return thousands * 0.0254;
}}


