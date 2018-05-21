'use strict';

$(document).ready(function(e) {
	var comPorts = windowsApp.getPortNames();

	for(var i = 0; i < comPorts.length; i++) {
		var item = comPorts[i];
		var optionField = $("<option value='" + item + "'>" + item + "</option>");
		$("select#com-ports-select").append(optionField);
	}

	var baudRates = windowsApp.getBaudRates();

	for(var i = 0; i < baudRates.length; i++) {
		var item = baudRates[i];
		var optionField = $("<option value='" + item + "'>" + item + "</option>");

		if (item === "115200")
			optionField.attr("selected", "selected");
		
		$("select#baud-rates-select").append(optionField);
	}

	$("body").on("click", "img#wrench", function(e) {
		windowsApp.showDevTools();
	});
});

var videoElement = document.getElementById("video");
var videoElement2 = document.getElementById("video2");

function gotStream(stream) {
	if (!videoElement.srcObject)
		videoElement.srcObject = stream;
	else
		videoElement2.srcObject = stream;
  
	var devices = navigator.mediaDevices.enumerateDevices();

  return devices;
}

function start() {
	if (window.stream) {
		window.stream.getTracks().forEach(function(track) {
		  track.stop();
		});
	}
  
	//cam2: 9dfd77aa6a915e1a8112bde7e4884ed02588360058e5010b0284d3e493d31f42
	//cam1: 3f4bd793b5df13e89658391dfeeb0ec2c57aaa2a170b1922fac5e392daa42797
	var idTable = [];

	var promise = navigator.mediaDevices.enumerateDevices();

	promise.then(function(devices) {

		for(var i = 0; i < devices.length; i++) {
			var device = devices[i];

			if (device.kind == 'videoinput')
				idTable.push(device.deviceId);
		}
	}).then(function() {
		for(var i = 0; i < 2; i++) {
			var videoSource = idTable[i];

			var constraints = {
				video: 	{ deviceId: videoSource }
			};
  
			navigator.mediaDevices.getUserMedia(constraints).
				then(gotStream).catch(handleError);
		}
	});
}

start();

function handleError(error) {
	console.log('navigator.getUserMedia error: ', error);
}
