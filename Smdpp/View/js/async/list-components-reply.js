var data = "{0}";
var task = $.parseJSON(unescape(data));

console.log("Bileşenler: ", task);

var page = "";
var itemTemplate = "";

$.get("inc/parts/component-row.tpl", function (pr) {{
    itemTemplate = pr;

    $.when($.get("inc/controls/component-list.tpl", function (dt) {{
        page = $(dt); //objectify html respon se

        var item = $(itemTemplate);

        var components = page;
        components.append("<h1>hehehe<h1>");
        console.log(components);

        /*for (var i = 0; i < task.length; i++) {{
            var component = task[i];

            //console.log(component);

            /*var id = item.prop("id");
            var packageName = item.find("div#packageName-").prop("id");
            var value = item.find("div#value-").prop("id");

            item.prop("id", id + component.id);
            item.find("div#packageName-").prop("id", packageName + component.package.name);


        }}*/



    }})).then(function (resp1) {{
        createTab("Komponent Listesi", page);
    }});
 }});