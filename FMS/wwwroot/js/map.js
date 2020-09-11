
function mapInitialize(lat, lng, radius, markers) {
    var lat = lat;
    var lng = lng;
    var radius = parseFloat(radius);
    //debugger;
    var cirRad;
    var cirConst = 1609;
    var zoomLevel;
    switch (true) {
        case (radius >= 0 && radius < 0.5):
            cirRad = cirConst * radius;
            zoomLevel = 16;
            break;
        case (radius >= 0.5 && radius < 1):
            cirRad = cirConst * radius;
            zoomLevel = 15;
            break;
        case (radius >= 1 && radius < 1.5):
            cirRad = cirConst * radius;
            zoomLevel = 14;
            break;
        case (radius >= 1.5 && radius < 3):
            cirRad = cirConst * radius;
            zoomLevel = 13;
            break;
        case (radius >= 3 && radius < 10):
            cirRad = cirConst * radius;
            zoomLevel = 12;
            break;

        default:
            cirRad = 402;
            zoomLevel = 16;
    }

    var Latlng = new google.maps.LatLng(lat, lng);
    var myOptions = {
        zoom: zoomLevel,
        center: Latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }
    var map = new google.maps.Map(document.getElementById("map"), myOptions);

    var centermarker = new google.maps.Marker({
        position: Latlng,
        map: map,
        icon: 'center.png'
    });

    var circle = new google.maps.Circle({
        map: map,
        radius: cirRad,    // metres
        fillColor: '#fff'
    });
    circle.bindTo('center', centermarker, 'position');

    var infowindow = new google.maps.InfoWindow();

    var marker, i;

    for (i = 0; i < markers.length; i++) {
        //determine marker image
        var imageName = 'icon_mix.png';
        var type = markers[i].ftype;
        switch (type) {
            case 'gen':
                imageName = 'icon_gen.png';
                break;
            case 'nongen':
                imageName = 'icon_nongen.png';
                break;
            case 'brown':
                imageName = 'icon_bf.png';
                break;
            case 'npl':
                imageName = 'icon_npl.png';
                break;
            case 'dod':
                imageName = 'icon_dod.png';
                break;
            case 'pasi':
                imageName = 'icon_pasi.png';
                break;
            case 'hsra':
                imageName = 'icon_hsra.png';
                break;
            case 'vrp':
                imageName = 'icon_vrp.png';
                break;
            case 'paf':
                imageName = 'icon_paf.png';
                break;
            case 'scraptire':
                imageName = 'icon_scraptire.png';
                break;
            default:
                imageName = 'icon_mix.png';
        }


        marker = new google.maps.Marker({
            position: new google.maps.LatLng(markers[i].latitude, markers[i].longitude),
            map: map,
            icon: imageName
        });

        google.maps.event.addListener(marker, 'click', (function (marker, i) {
            return function () {
                zip = markers[i].zip;
                if (zip == undefined) zip = ""
                if (markers[i].ftype == 'scraptire')
                    infowindow.setContent('<span class="address1"><a href=\"/prod/hwmb/search/facilityDetail.jsp?facilityID=' + markers[i].id + '&facilityName=' + markers[i].name + '\">' + markers[i].name + '</span><br><span class="address2">' + markers[i].address + '</span><br><span class="address2">Estimate # tires: ' + markers[i].numtires + '</span><br><span class="address2">lat: ' + markers[i].latitude + ' lng:' + markers[i].longitude + '</span>');
                else {
                    //infowindow.setContent('<span class="address1"><a href=\"/prod/hwmb/search/facilityDetail.jsp?facilityID='+markers[i].id+'&facilityName='+markers[i].name+'\">'+markers[i].name +'</span><br><span class="address2">'+ markers[i].address+'</span><br><span class="address2">'+ markers[i].city +', GA '+ markers[i].zip+'</span><br><span class="fileID">FILE ID: ' + markers[i].fileID+'</span>');
                    infowindow.setContent('<h5>' + markers[i].name + '</h5><div class="address2"><b>' + markers[i].address + '</b></div><div class="address2"><b>' + markers[i].city + ', GA ' + zip + '</b></div><div class="fileID"><b>FILE ID: ' + markers[i].fileID + '</b></div><div class="address2"><b>STATUS: ' + markers[i].status + '</b></div>');
                }
                infowindow.open(map, marker);
            }
        })(marker, i));
    }


}

function getLatLongs(addr, city, zip) {
    var lat = 0;// 33.879807;
    var lng = 0;// -87.306964;
    var address = addr + " " + city + ", GA " + zip;   
    var address = $('#Address').val();    
    var request = {
        address: address + ' GA ' + zip,
        componentRestrictions: {
            country: 'US'
        }
    }

    var geocoder = new google.maps.Geocoder();
    geocoder.geocode(request, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            lat = results[0].geometry.location.lat();
            lng = results[0].geometry.location.lng();
            //$('#UserLatitude').val(results[0].geometry.location.lat());
            //$('#UserLongitude').val(results[0].geometry.location.lng());
            //$('#localLat').val(results[0].geometry.location.lat());
            //$('#localLng').val(results[0].geometry.location.lng());
            //alert("Latitude: " + results[0].geometry.location.lat());
            //alert("Longitude: " + results[0].geometry.location.lng());

            $('#Latitude').val(parseFloat(lat).toFixed(4));
            $('#Longitude').val(parseFloat(lng).toFixed(4));
            $('#LocalLat').val(parseFloat(lat).toFixed(4));
            $('#LocalLng').val(parseFloat(lng).toFixed(4));

            if (lat > 0) {
                bool = false;
                $('#Search').click();
            }
        }
        else {
            alert("Geocode was not successful for the following reason: " + status);
        }

    });

}


