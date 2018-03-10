<div id="editor" class="editor">
	<script type="text/javascript">
		$(document).ready(function() {
			
			$("button#so28w-button").click(function(e) {
				$.get("svg/SO28W.SVG", function(data) {
					var holder = $("<div id='svg-holder'></div>");
					var svg = $(data);

					holder.append(svg);

					$("div#editor").append(holder);
					$("#svg-holder").draggable({ scroll: false, grid: [ 0.01, 0.01 ] });


				});
			});
		});
	</script> 
</div>
<div id="toolbar">
	<button id="so28w-button">Insert Spin Fv</button>
</div>