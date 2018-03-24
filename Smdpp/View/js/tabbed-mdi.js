function closeTab(headerId) {
    var tabId = headerId.replace("header", "tab");

    selectClosestPage(headerId);

    $("div#" + tabId).remove();
    $("div#" + headerId).remove();
}

function selectClosestPage(headerId) {
    var closest = $("div.headers div#" + headerId).next();

    if (!closest.hasClass("tab-header"))
        closest = $("div.headers div#" + headerId).prev();

    if (closest.hasClass("tab-header")) {
        var headerId = closest.attr("id");

        selectPage(headerId);
    }
}

function selectPage(headerId) {
    $("div.headers div").removeClass("active");
    $("div.tab").removeClass("active");

    $("div.headers div#" + headerId).addClass("active");

    var tabId = headerId.replace("header", "tab");
    $("div#" + tabId).addClass("active");
}

function getAvailableId() {
    var index = 0;

    $("div#tabbed-mdi div.headers div.tab-header").each(function() {
        var currentId = parseInt($(this).attr("id").replace("header-", ""));

        if (currentId > index)
            index = currentId;
    });

    return index + 1;
}

function createTab(title, content) {
    var header = "";
    var tab = "";

    var id = getAvailableId();

    $.when($.get("inc/parts/tab-header.tpl", function(data) {
        header = $(data);
        header.attr("id", "header-" + id);
		header.prepend(title);

        //console.log(header);
    }), $.get("inc/parts/tab-content.tpl", function(data) {
        tab = $(data);
        tab.attr("id", "tab-" + id);

		tab.html(content.html());

        //console.log(tab);
    })).then(function(resp1, resp2) {
        $("div.headers").append(header);
        $("div#tabbed-mdi").append(tab);
    });
}

$(document).ready(function() {
    $("body").on("click", "div#tabbed-mdi .headers .tab-header i", function(e) {
        e.preventDefault();
        e.stopPropagation();

        var headerId = $(this).parent().attr("id");

        closeTab(headerId);
    });

    $("body").on("click", "div#tabbed-mdi .headers .tab-header", function(e) {
        if (!$(this).hasClass("active")) {
            var headerId = $(this).attr("id");

            selectPage(headerId);
        }
    });

    /*$(window).bind('mousewheel DOMMouseScroll', function(event) {
		if (event.originalEvent.wheelDelta > 0 || event.originalEvent.detail < 0) {
			if ($("div#editor").css("zoom") < 5.0)
				$("div#editor").css("zoom", "+=0.1");
		}
		else {
			if ($("div#editor").css("zoom") > 0.1)
				$("div#editor").css("zoom", "-=0.1");
		}
    });*/
});