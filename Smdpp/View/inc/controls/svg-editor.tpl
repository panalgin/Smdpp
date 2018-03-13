<div id="editor" class="editor">
	<script type="text/javascript">
		$(document).ready(function() {
			$("button#so28w-button").click(function(e) {
				$.get("svg/SO28W.SVG", function(data) {
					var holder = $("<div id='svg-holder'></div>");
					var svg = $(data);

					holder.append(svg);

					$("div#editor").append(holder);

					$("div#svg-holder").draggable({ scroll: false,
							drag: function (event, ui) {

								var zoom = $("div#editor").css("zoom");
								var factor = ((1 / zoom) -1);

								 ui.position.top += (ui.position.top - ui.originalPosition.top) * factor;
								 ui.position.left += (ui.position.left- ui.originalPosition.left) * factor;    
							}
					}); 
				});
			});
		});
	</script> 
</div>
<div id="toolbar">
	<button id="so28w-button">Insert Spin Fv</button>
</div>