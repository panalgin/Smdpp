var data = "{0}";
var task = $.parseJSON(unescape(data));

/* task =>
0:
    currentPart: null
    slot: {{ id: 1, width: 8, pickupX: 0, pickupY: 0, depth: 1 }}
    suggestedPart: null*/

for (var i = 0; i < task.length; i++) {{
    var feederState = task[i];

    var slotTag = $("div#feeder-" + feederState.slot.id);

    slotTag.data(feederState);

    if (feederState.currentPart)
        slotTag.find(".value").text(feederState.currentPart.value);
    else
        slotTag.find(".value").html("---");

}}

console.log("Feeder States: ", task);