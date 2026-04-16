$(document).ready(function mapSearch() {
    let searchBool = true;
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
        if (output !== "1" && output !== "2") {
            return false;
        }

        const lat = $('#Latitude').val();
        const lng = $('#Longitude').val();

        const localLatEl = $('#LocalLat');
        const localLngEl = $('#LocalLng');
        if (lat !== "" && lng !== "") {
            localLatEl.val("");
            localLngEl.val("");
            return true;
        }

        localLatEl.attr("disabled", false);
        localLngEl.attr("disabled", false);
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

        if (lat === "" || lng === "") {
            lat = $('#LocalLat').val();
            lng = $('#LocalLng').val();
        }
        const radius = $('#Radius').val();

        if (lat !== "" && lat.length > 0) {
            mapInitialize(lat, lng, radius, window.FMS_RESOURCES.markers, Localaddr);
        }
    }

    function DisableLatLng() {
        $("#Latitude").attr("disabled", true).val("");
        $("#Longitude").attr("disabled", true).val("");
    }

    function EnableLatLng() {
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
        const myradius = parseFloat(inputRadius);
        const cirConst = 1609;
        let cirRad = cirConst * myradius;
        let zoomLevel;
        switch (true) {
            case (myradius >= 0 && myradius < 0.5):
                zoomLevel = 16;
                break;
            case (myradius >= 0.5 && myradius < 1):
                zoomLevel = 15;
                break;
            case (myradius >= 1 && myradius < 1.5):
                zoomLevel = 14;
                break;
            case (myradius >= 1.5 && myradius < 3):
                zoomLevel = 13;
                break;
            case (myradius >= 3 && myradius < 10):
                zoomLevel = 12;
                break;

            default:
                cirRad = 402;
                zoomLevel = 12;
        }

        const myLatlng = new google.maps.LatLng(lat, lng);
        const mapOptions = {
            zoom: zoomLevel,
            center: myLatlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        const map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);

        // This encapsulates the map to use Spiderfy to determine marker behavior
        const spiderfy = new OverlappingMarkerSpiderfier(map, {
            markersWontMove: true,
            markersWontHide: true,
        });

        const centermarker = new google.maps.Marker({
            position: myLatlng,
            map: map,
            icon: '/images/type-icons/icon-map-center.svg',
            title: Localaddr
        });

        const circle = new google.maps.Circle({
            map: map,
            radius: cirRad,    // metres
            fillColor: '#fff'
        });
        circle.bindTo('center', centermarker, 'position');

        const infoWndw = new google.maps.InfoWindow({
            content: '<div style="padding:0.5em;border:groove;font-weight:normal;font-size:10pt;color:black">' + Localaddr + '<br>' + '<br>' + lat + ',     ' + lng + '</div>'
        });

        google.maps.event.addListener(centermarker, 'click', function () {
            if (!centermarker.open) {
                infoWndw.open(map, centermarker);
                centermarker.open = true;
            } else {
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

        const infowindow = new google.maps.InfoWindow();
        const multiImageName = '/images/type-icons/icon-multi.svg';

        for (let i = 0; i < markers.length; i++) {
            (function () {
                const data = markers[i];
                //determine marker image
                const imageName = '/images/type-icons/icon-' + data.facilityType + '.svg';
                const myLatlng = new google.maps.LatLng(data.latitude, data.longitude);
                const marker = new google.maps.Marker({
                    position: myLatlng,
                    icon: imageName
                });
                let zip = data.postalCode;
                if (zip === undefined) {
                    zip = ""
                }
                const hyplink = "./Details/" + data.id;
                var hyplink2;
                var fileLabel;
                if (data.fileLabel === null) {
                    hyplink2 = hyplink;
                    fileLabel = "No File Label. Facility Details";
                } else {
                    hyplink2 = "../Files/Details/" + data.fileLabel;
                    fileLabel = data.fileLabel;
                }
                // This defines the contents of the InfoWindow that pops up when a marker is clicked on.
                google.maps.event.addListener(marker, "spider_click", function () {
                    infowindow.setContent('<div style="font-family: arial, helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #00F;"><b><a target="_blank" href= ' + '' + hyplink + "" + '>' + data.name + '</a></b></div><div style="font-family: arial, helvetica, sans-serif; font-size: 10px; font-weight: bold; color: #808080;"><b>' + data.address + '</b></div><div style="font-family: arial, helvetica, sans-serif; font-size: 10px; font-weight: bold; color: #808080;"><b>' + data.city + ', GA ' + zip + '</b></div><div style="font-family: arial, helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #00F;"><b><a target="_blank" href= ' + '' + hyplink2 + "" + '> FILE ID: ' + fileLabel + '</a></b></div><div style="font-family: arial, helvetica, sans-serif; font-size: 10px; font-weight: bold; color: #808080;"><b>STATUS: ' + data.facilityStatus + '</b></div>');
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
                    infowindow.close()
                });

                google.maps.event.addListener(circle, 'click', function () {
                    infowindow.close()
                });

                spiderfy.addMarker(marker);
            })();
        }
    }

    function getLatLongs(addr, city, zip) {
        let lat = 0;// 33.879807;
        let lng = 0;// -87.306964;
        const address = addr + " " + city + ", GA " + zip;
        const request = {
            address: address,
            componentRestrictions: {
                country: 'US'
            }
        };
        const geocoder = new google.maps.Geocoder();
        geocoder.geocode(request, function (results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
                lat = results[0].geometry.location.lat();
                lng = results[0].geometry.location.lng();
                let localAddr;
                if (results.length > 1) {
                    localAddr = results[1].formatted_address;
                } else {
                    localAddr = results[0].formatted_address;
                }

                $('#LocalLat').val(parseFloat(lat).toFixed(4));
                $('#LocalLng').val(parseFloat(lng).toFixed(4));
                $('#LocalAddress').val(localAddr.toString());

                if (lat > 0) {
                    searchBool = false;
                    $('#SearchButton').click();
                }
            } else {
                alert("Geocode was not successful for the following reason: " + status);
            }
        });
    }
});
