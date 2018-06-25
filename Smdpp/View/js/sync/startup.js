var CSV_RIGHT_CLICK_TEMPLATE = "";

$().ready(function () {
    var comPorts = windowsApp.getPortNames();

    for (var i = 0; i < comPorts.length; i++) {
        var item = comPorts[i];
        var optionField = $("<option value='" + item + "'>" + item + "</option>");
        $("select#com-ports-select").append(optionField);
    }

    var result = windowsApp.getBaudRates();

    if (result.success) {
        var baudRates = result.baudRates;

        for (var i = 0; i < baudRates.length; i++) {
            var item = baudRates[i];
            var optionField = $("<option value='" + item + "'>" + item + "</option>");

            if (item === "115200")
                optionField.attr("selected", "selected");

            $("select#baud-rates-select").append(optionField);
        }
    }

    $("body").on("click", "img#wrench", function (e) {
        windowsApp.showDevTools();
    });

    $.get("inc/controls/right-click-csv-menu.tpl", function (data) {
        if (data) {
            CSV_RIGHT_CLICK_TEMPLATE = data;
        }
    });
});

function showContextMenuForItem(svg, xOff, yOff) {
    closeAllContextMenus();

    var object = $(CSV_RIGHT_CLICK_TEMPLATE);

    var componentInfo = svg.data("part");
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

    var slots = getAvailableSlotsFor(componentInfo.id);

    slots.then(function (result) {
        var availableSlots = result;
        var combo = object.find(".context-item select");
        fillComboWithSlots(combo, availableSlots);
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

//Get extensive details about the package-specific data including the svg information.
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

//Returns the available slots that may contain this component
function getAvailableSlotsFor(componentId) {
    return new Promise(resolve => {
        var result = windowsApp.getAppropriateSlotFor(componentId);
        resolve(result);
    });
}

function fillComboWithSlots(combo, availableSlots) {
    combo.html("");

    for (var i = 0; i < availableSlots.length; i++) {
        var template = "<option id='%s'"
    }
}