﻿var map;

window.onload = function () {
    L.mapquest.key = 'YVOxelMfyMWXzUCKLHGgoX5FNl13x4R2';

     map = L.mapquest.map('map', {
        center: [37.7749, -122.4194],
        layers: L.mapquest.tileLayer('map'),
        zoom: 12
    });

    var layerGroup = L.layerGroup().addTo(map);
    map.addControl(L.mapquest.control());
    getBrowserGeoLocation();
    showEventMarker();

    function getBrowserGeoLocation() {
        navigator.geolocation.getCurrentPosition(updatePosition, positionError);
    }

    function updatePosition(position) {
        var latLng = L.latLng(position.coords.latitude, position.coords.longitude);
        map.setView(latLng, 13);
    }

    function positionError(position) {

    }

    function showEventMarker() {
        if ($("#Event_Location_Latitude").val && $("#Event_Location_Longitudes").val) {
            layerGroup.clearLayers();
            var marker = L.marker([$("#Event_Location_Latitude").val(), $("#Event_Location_Longitudes").val()]).addTo(layerGroup);
            bindEventAddressPopup(marker);
        }
    }

    function bindEventAddressPopup(marker) {
        var eventCity = "<div>" + $("#Event_Location_City").val() + "</div>";
        var eventAddress = "<div>" + $("#Event_Location_Address").val() + "</div>";
        marker.bindPopup(eventCity + eventAddress);

    }
}

