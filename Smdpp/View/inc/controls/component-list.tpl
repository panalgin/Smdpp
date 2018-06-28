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
			var text = "";
			
			$.get("inc/controls/component-add.tpl").done(function(data) {
				var result = windowsApp.getAvailablePackageNames();
				console.log(result);

				vex.open({
					unsafeContent: "",
					className: 'vex-theme-default'
				});

				$("div.vex-content").append(data);
			});
		});
	});
</script>