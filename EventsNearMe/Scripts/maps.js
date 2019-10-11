﻿
window.onload = function () {
    L.mapquest.key = 'YVOxelMfyMWXzUCKLHGgoX5FNl13x4R2';

    var map = L.mapquest.map('map', {
        center: [37.7749, -122.4194],
        layers: L.mapquest.tileLayer('map'),
        zoom: 12
    });

    map.addControl(L.mapquest.control());
    map.on('click', onMapClick);
    showEventMarker();
    getBrowserGeoLocation();

    function getBrowserGeoLocation() {
        navigator.geolocation.getCurrentPosition(updatePosition, positionError);
    }

    function updatePosition(position) {
        var latLng = L.latLng(position.coords.latitude, position.coords.longitude);
        map.setView(latLng, 13);
    }

    function positionError(position) {

    }

    function onMapClick(e) {
        console.log(e);
        L.mapquest.geocoding().reverse(e.latlng, updateLocationData);
    }

    function updateLocationData(error, response) {
        console.log(response);
        var result = response.results[0].locations[0];
        $("#Location_Address").val(result.street + ", " + result.adminArea3);
        $("#Location_City").val("Melbourne");
        $("#Location_Latitude").val(result.latLng.lat);
        $("#Location_Longitudes").val(result.latLng.lng);
        $("#Location_PostCode").val(result.postalCode);
    }

    function showEventMarker() {
        if ($("#Location_Latitude").val && $("Location_Longitudes").val) {
            var marker = L.marker([$("#Location_Latitude").val(), $("#Location_Longitudes").val()]).addTo(map);
        }
    }
}
