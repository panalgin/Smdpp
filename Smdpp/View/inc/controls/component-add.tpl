<div class="create-component">
	<div class="row">
		<div class="cell left">Kılıf:</div>
		<div class="cell right"><select id="packageName-combo"></select></div>
	</div>
	<div class="row">
		<div class="cell left">Değer:</div>
		<div class="cell right"><input id="partValue-box" type="text" /></div>
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
		var result = windowsApp.getAvailablePackageNames();

		if (result.success) {
			var packageNames = result.names;

			console.log(packageNames);

			for(var i = 0; i < packageNames.length; i++) {
				var packageName = packageNames[i];
				$("select#packageName-combo").append("<option value=\"" + packageName + "\">" + packageName + "</option>")
			}

			$("button#cancel-button").on("click", function(e) {
				vex.closeAll();
			});
		}
	});
</script>