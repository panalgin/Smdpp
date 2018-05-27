
var CSV_RIGHT_CLICK_TEMPLATE = "";

$().ready(function () {
    $.get("inc/controls/right-click-csv-menu.tpl", function (data) {
        if (data) {
            CSV_RIGHT_CLICK_TEMPLATE = data;
        }
    });
});

function showContextMenuForItem(svg, xOff, yOff) {
    closeAllContextMenus();

    var object = $(CSV_RIGHT_CLICK_TEMPLATE);

    var componentInfo = svg.data("component");
    var packageInfo = findPackageDetailsOf(componentInfo.packageId);

    object.find("#referenceName").html(componentInfo.referenceId);
    object.find("#packageName").html(packageInfo.name);
    object.find("#value").html(componentInfo.value);

    $("#pnp-editor").append(object);

    object.css("top", yOff);
    object.css("left", xOff);

    $(document).one("mousewheel", function (e) {
        closeAllContextMenus();
        $("#board").panzoom("enable");
    });
}

function findPackageDetailsOf(packageId) {
    var componentInfo = "";

    for (var i = 0; i < window.currentTask.availablePackages.length; i++) {
        var currentPackage = window.currentTask.availablePackages[i];

        if (currentPackage.id === packageId) {
            componentInfo = currentPackage;

            break;
        }
    }

    return componentInfo;
}

function findPackageOf(packageId) {
    var svgData = "";

    for (var i = 0; i < window.currentTask.availablePackages.length; i++) {
        var currentPackage = window.currentTask.availablePackages[i];

        if (currentPackage.id === packageId) {
            svgData = currentPackage.data;
            break;
        }
    }

    return svgData;
}