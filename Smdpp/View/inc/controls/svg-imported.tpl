<div class="svg-import-page">
	<div class="row">
		<div class="cell left">
			<label for="package-name">Kılıf Adı:</label>
		</div>
		<div class="cell right">
			<input type="text" value="" name="package-name" id="package-name" tabindex="1" />
		</div>
	</div>
	<div class="row">
		<div class="cell left"></div>
		<div class="cell right">
			<button id="save-package-button" type="button">Kaydet</button>
		</div>
	</div>
	<div id="current-svg">
	</div>
</div>

<script type="text/javascript">
	$(document).ready(function() {
		$("button#save-package-button").on("click", function(e) {
			var packageName = $("input#package-name").val();

			if (packageName.length < 3) 
				alert("Kılıf adı 3 karakterden kısa olamaz.");
			else {
				var svgData = JSON.stringify($("div#current-svg").html());

				var response = windowsApp.savePackage(packageName, svgData);
				
				if (!response.success)
					alert(response.message);
				else
					closeCurrentTab();
			}
		});
	});
</script>