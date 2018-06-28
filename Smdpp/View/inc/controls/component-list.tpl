<div id="components">
	<div id="add-new-btn">Yeni</div>
	<div class="row headers">
		<div class="cell first">Komponent Adı</div>
		<div class="cell second">Önizleme</div>
		<div class="cell third">Araçlar</div>
	</div>
</div>

<script type="text/javascript">
	$().ready(function() {
		$("body").on("click", "#components #add-new-btn", function(e) {
			vex.open({
				content: "hgegeg",
				className: 'vex-theme-default'
			});
		});
	});
</script>