var data = "{0}";
var task = $.parseJSON(unescape(data));

//1 = diktörgen
//2 = daire
//3 = kare

var totalTools = task["tools"].length;
var totalEntries = task["entries"].length;
var svg = SVG('editor').size(convertMMToPx(task.width), convertMMToPx(task.height));

for(var i = 0; i < totalTools; i++) {{
	for(var j = 0; j < totalEntries; j++) {{
		//console.log(task["entries"][j]);

		var entry = task["entries"][j];
		var tool = task["tools"][i];

		if (entry.toolId == tool.id) {{
			printEntry(svg, entry, tool);
		}}
	}}
}}

var svg_object = $("div#editor svg");
	svg_object.css("vector-effect","non-scaling-stroke");
	svg_object.css("fill-rule", "evenodd");

$("div#editor").css("width", convertMMToPx(task.width));
$("div#editor").css("height", convertMMToPx(task.height));


        (function() {{
          var $section = $('#tab-1');
          var $panzoom = $section.find('#editor').panzoom( {{
				/*disablePan: true*/
				minScale: 0.1,
				maxScale: 10.0
		  }});
          $panzoom.parent().on('mousewheel.focal', function( e ) {{
            e.preventDefault();
            var delta = e.delta || e.originalEvent.wheelDelta;
            var zoomOut = delta ? delta < 0 : e.originalEvent.deltaY > 0;
            $panzoom.panzoom('zoom', zoomOut, {{
              animate: false,
              focal: e,
            }});
          }});
        }})();
      

function printEntry(svg, entry, tool) {{
	var top = convertMMToPx(entry.y);
	var left = convertMMToPx(entry.x);

	var	right = left;
	var bottom = top;

	if (tool.toolType == 1) {{
		var width = convertMMToPx(tool.width);
		var height = convertMMToPx(tool.height);

		left -= width / 2;
		top -= height / 2;

		var rect = svg.rect(width, height).fill('#fff');
		rect.x(left).y(top);
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


