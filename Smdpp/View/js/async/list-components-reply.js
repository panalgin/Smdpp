var data = "";
var task = "";

data = "{0}";
task = $.parseJSON(unescape(data));

console.log("Bileşen Listesi: ", task);

var page = "";
var itemTemplate = "";
var component = "";
var components = "";
var item = "";
var package = "";

$.get("inc/parts/component-row.tpl", function (result) {{
    itemTemplate = result;
}});

$.when($.get("inc/controls/component-list.tpl", function (list) {{
        page = $(list); //objectify html respon se

        for (var i = 0; i < task.length; i++) {{
            item = $(itemTemplate);
            component = task[i];
            package = component.package;

            item.find("div#component-").prop("id", component.id);
            item.find("div#value-").prop("id", "value-" + component.id).html(component.value);
            item.find("div#packageName-").prop("id", "packageName-" + component.id).html(package.name);

            page.find("div.headers").parent().append(item);
        }}
    }})).then(function (result) {{
        createTab("Komponent Listesi", page);
    }});