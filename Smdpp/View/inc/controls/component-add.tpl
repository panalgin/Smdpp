<div class="create-component">
	<div class="row">
		<div class="cell left">Kılıf:</div>
		<div class="cell right"><select id="packages-combo"></select></div>
	</div>
	<div class="row">
		<div class="cell left">Değer:</div>
		<div class="cell right"><input id="componentValue-box" type="text" /></div>
	</div>
	<div class="row">

	</div>
	<div class="row">
		<div class="cell left"></div>
		<div class="cell right">
			<button id="save-button">Kaydet</button>
			<button id="cancel-button">İptal</button>
		</div>
	</div>
</div>
<script type="text/javascript">
	$().ready(function() {
		$("button#cancel-button").on("click", function(e) {
			vex.closeAll();
		});

		$("button#save-button").on("click", function(e) {
			var component = { packageId: 0, value: "" };

			component.value = $("input#componentValue-box").val();
			component.packageId = $("select#packages-combo").val();

			var data = JSON.stringify(component);
			windowsApp.addComponent(data);
		});

		var result = JSON.parse(windowsApp.getAvailablePackageNames());

		console.log(result);

		if (result.success) {
			var packages = result.packages;

			for(var i = 0; i < packages.length; i++) {
				var package = packages[i];
				$("select#packages-combo").append("<option value=\"" + package.id + "\">" + package.name + "</option>")
			}
		}
	});
</script>