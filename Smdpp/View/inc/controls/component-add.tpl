<div class="create-component">
	<div class="row">
		<div class="cell left">Komponent Adı:</div>
		<div class="cell right"><input type="text" /></div>
	</div>
	<div class="row">
		<div class="cell left">Kılıf:</div>
		<div class="cell right"><select></select></div>
	</div>
	<div class="row">
		<div class="cell left">Değer:</div>
		<div class="cell right"><input type="text" /></div>
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
		console.log(result);
	});
</script>