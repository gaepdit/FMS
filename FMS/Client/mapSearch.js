import $ from 'jquery';
import oms from './oms.min';

let searchBool = true;
$(document).ready(function domReady() {
    $("#Address,#City,#PostalCode").change(function () {
        if ($(this).val() !== "") {
            DisableLatLng();
        } else {
            EnableLatLng();
        }
    });

    $("#Latitude,#Latitude").change(function () {
        if ($(this).val() !== "") {
            DisableAddress();
        } else {
            EnableAddress();
        }
    });

    $('#SearchButton').click(function GeoCodeLatLong() {
        const output = $('#Output').val();
        if (output !== "1" && output !== "2" ) {
            return false;
        }

        const lat = $('#Latitude').val();
        const lng = $('#Longitude').val();
        if (lat !== "" && lng !== "") {
            $("#LocalLat").val("");
            $("#LocalLng").val("");
            return true;
        }

        $("#LocalLat").attr("disabled", false);
        $("#LocalLng").attr("disabled", false);
        $("#LocalAddress").attr("disabled", false);

        if (!searchBool) {
            searchBool = true;
            return true;
        }

        const addr = $('#Address').val();
        const city = $('#City').val();
        const zip = $('#PostalCode').val();

        getLatLongs(addr, city, zip);

        searchBool = !searchBool;
        return searchBool;
    });

    if (window.FMS_RESOURCES.showMap) {
        let lat = $('#Latitude').val();
        let lng = $('#Longitude').val();
        let Localaddr = $('#LocalAddress').val();

        if (lat === "" || lng === "")
        {
            lat = $('#LocalLat').val();
            lng = $('#LocalLng').val();
        }
        const radius = $('#Radius').val();

        if (lat !== "" && lat.length > 0) {

            mapInitialize(lat, lng, radius, window.FMS_RESOURCES.markers, Localaddr);
        }
    }

    function DisableLatLng(){
        $("#Latitude").attr("disabled", true).val("");
        $("#Longitude").attr("disabled", true).val("");
    }
    function EnableLatLng(){
        $("#Latitude").attr("disabled", false);
        $("#Longitude").attr("disabled", false);
    }
    function DisableAddress() {
        $("#Address").attr("disabled", true).val("");
        $("#City").attr("disabled", true).val("");
        $("#PostalCode").attr("disabled", true).val("");
    }
    function EnableAddress() {
        $("#Address").attr("disabled", false);
        $("#City").attr("disabled", false);
        $("#PostalCode").attr("disabled", false);
    }

    function mapInitialize(lat, lng, inputRadius, markers, Localaddr) {
        var lat = lat;
        var lng = lng;
        var myradius = parseFloat(inputRadius);
        //debugger;
        var cirRad;
        var cirConst = 1609;
        var zoomLevel;
        switch (true) {
            case (myradius >= 0 && myradius < 0.5):
                cirRad = cirConst * myradius;
                zoomLevel = 16;
                break;
            case (myradius >= 0.5 && myradius < 1):
                cirRad = cirConst * myradius;
                zoomLevel = 15;
                break;
            case (myradius >= 1 && myradius < 1.5):
                cirRad = cirConst * myradius;
                zoomLevel = 14;
                break;
            case (myradius >= 1.5 && myradius < 3):
                cirRad = cirConst * radius;
                zoomLevel = 13;
                break;
            case (myradius >= 3 && myradius < 10):
                cirRad = cirConst * myradius;
                zoomLevel = 12;
                break;

            default:
                cirRad = 402;
                zoomLevel = 12;
        }

        var myLatlng = new google.maps.LatLng(lat, lng);
        var mapOptions = {
            zoom: zoomLevel,
            center: myLatlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        }

        var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);

        // This encapsulates the map to use Spiderfy to determine marker behavior
        const spiderfy = new oms.OverlappingMarkerSpiderfier(map, {
            markersWontMove: true,
            markersWontHide: true,
        });

        var toolContent = Localaddr;
        var centermarker = new google.maps.Marker({
            position: myLatlng,
            map: map,
            icon: '/images/type-icons/icon-map-center.svg',
            title: toolContent
        });

        var circle = new google.maps.Circle({
            map: map,
            radius: cirRad,    // metres
            fillColor: '#fff'
        });
        circle.bindTo('center', centermarker, 'position');

        var infoWndw = new google.maps.InfoWindow({
            content: '<div style="padding:0.5em;border:groove;font-weight:normal;font-size:10pt;color:black">' + Localaddr + '<br>'+ '<br>' + lat + ',     ' + lng + '</div>'
        });

        google.maps.event.addListener(centermarker, 'click', function () {
            if (!centermarker.open) {
                infoWndw.open(map, centermarker);
                centermarker.open = true;
            }
            else {
                infoWndw.close();
                centermarker.open = false;
            }
            google.maps.event.addListener(map, 'click', function () {
                infoWndw.close();
                centermarker.open = false;
            });
            google.maps.event.addListener(circle, 'click', function () {
                infoWndw.close();
                centermarker.open = false;
            });
        });

        var infowindow = new google.maps.InfoWindow();
        const multiImageName = '/images/type-icons/icon-multi.svg';

        for (let i = 0; i < markers.length; i++) {
            (function () {
            var data = markers[i];
            //determine marker image
            var imageName = '/images/type-icons/icon-' + data.facilityType + '.svg';
            var myLatlng = new google.maps.LatLng(data.latitude, data.longitude);
            var marker = new google.maps.Marker({
                    position: myLatlng,
                    icon: imageName
                });
                var zip = data.postalCode;
                if (zip == undefined) {
                    zip = ""
                }
                var hyplink = "./Details/" + data.id;
                var hyplink2 = "../Files/Details/" + data.fileLabel;
                // This defines the contents of the InfoWindow that pops up when a marker is clicked on.
                google.maps.event.addListener(marker, "spider_click", function (e) {
                    infowindow.setContent('<div style="font-family: arial, helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #00F;"><b><a target="_blank" href= ' + '' + hyplink + "" + '>' + data.name + '</a></b></div><div style="font-family: arial, helvetica, sans-serif; font-size: 10px; font-weight: bold; color: #808080;"><b>' + data.address + '</b></div><div style="font-family: arial, helvetica, sans-serif; font-size: 10px; font-weight: bold; color: #808080;"><b>' + data.city + ', GA ' + zip + '</b></div><div style="font-family: arial, helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #00F;"><b><a target="_blank" href= ' + '' + hyplink2 + "" + '> FILE ID: ' + data.fileLabel + '</a></b></div><div style="font-family: arial, helvetica, sans-serif; font-size: 10px; font-weight: bold; color: #808080;"><b>STATUS: ' + data.facilityStatus + '</b></div>');
                    infowindow.open(map, marker);
                });
                // This adds listener to say, if the marker is "Spiderfiable" then Use the image with the "+" plus sign.
                google.maps.event.addListener(marker, 'spider_format', function (status) {
                    marker.setIcon({
                        url: status === 'SPIDERFIABLE' ? multiImageName : imageName
                    });
                });
                // This closes the infowindow if some other part of the map is clicked on.
                google.maps.event.addListener(map, 'click', function () {
                    infowindow.close(map, marker)
                });

                google.maps.event.addListener(circle, 'click', function () {
                    infowindow.close(circle, marker)
                });

                spiderfy.addMarker(marker);
            })();
        }
    }

    function getLatLongs(addr, city, zip) {
        var lat = 0;// 33.879807;
        var lng = 0;// -87.306964;
        var fAddress = "";
        var address = addr + " " + city + ", GA " + zip;
        // var address = $('#Address').val();    
        var request = {
            address: address,
            componentRestrictions: {
                country: 'US'
            }
        }
        var geocoder = new google.maps.Geocoder();
        geocoder.geocode(request, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                lat = results[0].geometry.location.lat();
                lng = results[0].geometry.location.lng();
                let localAddr;
                if (results.length > 1) {
                    localAddr = results[1].formatted_address;
                }
                else {
                    localAddr = results[0].formatted_address;
                }

                //$('#Latitude').val(parseFloat(lat).toFixed(4));
                //$('#Longitude').val(parseFloat(lng).toFixed(4));
                $('#LocalLat').val(parseFloat(lat).toFixed(4));
                $('#LocalLng').val(parseFloat(lng).toFixed(4));
                $('#LocalAddress').val(localAddr.toString());

                if (lat > 0) {
                    searchBool = false;
                    $('#SearchButton').click();
                }
            }
            else {
                alert("Geocode was not successful for the following reason: " + status);
            }

        });
    }
});
