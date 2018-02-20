$(document).ready(function() {
	$("body").on("click", "div.wrapper ul#nav li", function(e) {
		var menuId = $(this).attr("id");
		showMenu(menuId);

		e.stopPropagation();
	});

	$("body").on("mouseenter", "div.wrapper ul#nav li", function(e) {
		if (isAnyPopOpen()) {
			var menuId = $(this).attr("id");
			var popId = $("ul.pop-menu").filter(":visible").attr("id");

			if (menuId.replace("id", "") != popId.replace("id", "")) {
				showMenu(menuId);
			}
		}
	});

	$(document).on("click", "body", function(e) {
		if (isAnyPopOpen()) {
			closeAllPops();
		}
	});

	$("body").on("click", "ul.pop-menu", function(e) {
	
		e.stopPropagation(); 
	});

	$("body").on("click", "ul.pop-menu li", function(e) {
		e.stopPropagation();

		var linkId = $(this).attr("id");

		switch(linkId) {
			case "exitMenuBtn1":
			case "exitMenuBtn2":
			case "exitMenuBtn3": {
				
				windowsApp.close();

				break;
			}

			case "openGerberBtn": {
				windowsApp.openGerber();

				break;
			}

			default: break;
		}

		closeAllPops();
	});
});

function isAnyPopOpen() {
	var pop = $("ul.pop-menu").filter(":visible");
	return pop.length > 0;
}

function closeAllPops() {
	$("ul#nav .active").removeClass("active");
	var pop = $("ul.pop-menu").filter(":visible").hide();
}

function showMenu(menuId) {
	var menuItem = $("li#" + menuId);
	var popId = menuId.replace("menu", "pop");
	var offset = menuItem.offset();

	offset.top = menuItem.height();

	closeAllPops();

	menuItem.addClass("active");
	$("ul#" + popId).css("top", offset.top).css("left", offset.left).show();
}

