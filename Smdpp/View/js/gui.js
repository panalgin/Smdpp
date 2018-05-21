$().ready(function () {
    $("body").on("click", "button#connect-button", function (e) {
        var port = $("select#com-ports-select").val();
        var baudRate = parseInt($("select#baud-rates-select").val());

        var result = windowsApp.connect(port, baudRate);

        console.log(result);

        if (result.success) {
            alert("Bağlantı başarılı.");
            //$("div#connection").remove();
        }
    });

});