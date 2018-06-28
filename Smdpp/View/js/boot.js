function require(script) {
    $.getScript(script, function (data) {
        //console.log("Script Loaded: " + script);
    });
}

require("js/sync/jquery-ui.min.js");
require("js/sync/svg.js");
require("js/sync/jquery.panzoom.min.js");
require("js/sync/jquery.context-menu.js");
require("js/sync/startup.js");
require("js/sync/gui.js");
require("js/sync/tabbed-mdi.js");
require("js/sync/camera.js");
require("js/sync/pop-menu.js");
require("js/sync/vex.js");