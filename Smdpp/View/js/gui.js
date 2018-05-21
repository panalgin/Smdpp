$().ready(function () {
    $("body").on("click", "button#connect-button", function (e) {
        var port = $("select#com-ports-select").value();
        var baudRate = $("select#baud-rates-select").value();

        windowsApp.connect(port, baudRate);
    });

});