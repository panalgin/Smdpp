var data = "{0}";
var task = $.parseJSON(unescape(data));

window.currentTask = task;

console.log(currentTask);

var page = "";
var board = "";
var offset = 0.0;
var size = "";

$.when($.get("inc/controls/pnp-task.tpl", function(dt) {{
    page = $(dt);
	board = page.find("div#board");

	offset = task.offset;
	size = task.boardSize;

	board.width(size.width + "mm");
	board.height(size.height + "mm");

	for(var i = 0; i < task.parts.length; i++) {{
		var part = task.parts[i];

		if (part.packageId > 0) {{
			var svgData = findPackageOf(part.packageId);
            var svgEntity = $(svgData);
            svgEntity.data("part", part);

			var rotation = -part.rotation;
			svgEntity.css({{ 'transform': 'rotate(' + rotation + 'deg)' }});

			board.append(svgEntity);

			var yPos = 20 + (part.position.y - offset.y);// - (svgEntity.outerHeight() / 2);
			var xPos = 20 + (part.position.x - offset.x);// - (svgEntity.outerWidth() / 2);

			var xCenterOff = parseFloat(svgEntity.attr("width").replace(/[^-\d\.]/g, '')) / 3.779528;
			var yCenterOff = parseFloat(svgEntity.attr("height").replace(/[^-\d\.]/g, '')) / 3.779528;

			svgEntity.css("top", (yPos - yCenterOff) + "mm");
			svgEntity.css("left", (xPos - xCenterOff) + "mm");

			
		}}
	}}


}})).then(function(resp1) {{
    createTab("Pnp Çalışması", page);

	/*setTimeout(function() {{

		$("div#board svg").each(function() {{
			var xCenterOffset = $(this).width() / 2;
			var yCenterOffset = $(this).height() / 2;

			var curTop = parseFloat($(this).css("top").replace(/[^-\d\.]/g, '')) / 3.779528;
			var curLeft = parseFloat($(this).css("left").replace(/[^-\d\.]/g, '')) / 3.779528;
			
			console.log(curTop);
			console.log(curLeft);
			 
			$(this).css("top", (curTop - yCenterOffset) + "mm");
			$(this).css("left", (curLeft - xCenterOffset) + "mm");
		}});
	}}, 1000);*/


	setTimeout(function() {{
	      var $section = $('div.pnp-task');
          var $panzoom = $section.find('#board').panzoom( {{
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

	}}, 1000);

	
}});