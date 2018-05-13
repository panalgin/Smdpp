<div id="pnp-editor" class="pnp-task">
	<div id="board">

	</div>
	<script type="text/javascript">
		$().ready(function() {
			$("body").on("contextmenu", ".pnp-task svg", function(e) {
				var svg = $(this);
				
				showContextMenuForItem(svg);
			});
		});
	</script>
</div>