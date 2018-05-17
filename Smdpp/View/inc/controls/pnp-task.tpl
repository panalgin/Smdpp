<div id="pnp-editor" class="pnp-task">
	<div id="board">

	</div>
	<script type="text/javascript">
		$().ready(function() {
			$("body").on("contextmenu", ".pnp-task svg", function(e) {
				var svg = $(this);

				var element = document.getElementById('board');
				var scaleX = element.getBoundingClientRect().width / element.offsetWidth;

				var xOffset = e.pageX - $("#board").offset().left;
				var yOffset = e.pageY - $("#board").offset().top;

				xOffset /= scaleX;
				yOffset /= scaleX;
				
				$("#board").panzoom("disable");
				showContextMenuForItem(svg, xOffset, yOffset);
			});
		});
	</script>
</div>