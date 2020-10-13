
function mapInitialize(lat, lng, inputRadius, markers) {
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

     
    var centermarker = new google.maps.Marker({
        position: myLatlng,
        map: map,
        icon: '/images/center.png'       
    });

    var circle = new google.maps.Circle({
        map: map,
        radius: cirRad,    // metres
        fillColor: '#fff'
    });
    circle.bindTo('center', centermarker, 'position');


    for (i = 0; i < markers.length; i++) {
        var data = markers[i];
        //determine marker image
        var imageName = '/images/icon_mix.png';
        var Ftype = (data.facilityType).toUpperCase();
        switch (Ftype) {
            case 'GEN':
                imageName = '/images/icon_gen.png';
                break;
            case 'NONGEN':
                imageName = '/images/icon_nongen.png';
                break;
            case 'BROWN':
                imageName = '/images/icon_bf.png';
                break;
            case 'NPL':
                imageName = '/images/icon_npl.png';
                break;
            case 'DOD':
                imageName = '/images/icon_dod.png';
                break;
            case 'PASI':
                imageName = '/images/icon_pasi.png';
                break;
            case 'HSRA':
                imageName = '/images/icon_hsra.png';
                break;
            case 'VRP':
                imageName = '/images/icon_vrp.png';
                break;
            case 'PAF':
                imageName = '/images/icon_paf.png';
                break;
            case 'SCRAPTIRE':
                imageName = '/images/icon_scraptire.png';
                break;
            default:
                imageName = '/images/icon_mix.png';
        }

        var infowindow = new google.maps.InfoWindow();
        var myLatlng = new google.maps.LatLng(data.latitude, data.longitude);
        var marker = new google.maps.Marker({
            position: myLatlng,
            map: map,
            icon: imageName            
        });

        (function (marker, data) {
            google.maps.event.addListener(marker, "click", function (e) {
                var zip = data.postalCode;
                if (zip == undefined) {
                    zip = ""
                }
                if (data.facilityType == 'SCRAPTIRE') {
                    infowindow.setContent('<span style="font-family: arial, helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #333;"><a href=\"/prod/hwmb/search/facilityDetail.jsp?facilityID=' + data.facilityNumber + '&facilityName=' + data.name + '\">' + data.name + '</span><br><span style="font-family: arial, helvetica, sans-serif; font-size: 10px; font-weight: bold; color: #808080;">' + data.address + '</span><br><span style="font-family: arial, helvetica, sans-serif; font-size: 10px; font-weight: bold; color: #808080;">Estimate # tires: ' + data.numtires + '</span><br><span style="font-family: arial, helvetica, sans-serif; font-size: 10px; font-weight: bold; color: #808080;">lat: ' + data.latitude + ' lng:' + data.longitude + '</span>');
                }
                else {
                    var hyplink = "./Details/" + data.id;
                    var hyplink2 = "../Files/Details/" + data.fileLabel;
                    infowindow.setContent('<div style="font-family: arial, helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #00F;"><b><a target="_blank" href= ' + '' + hyplink + "" + '>' + data.name + '</a></b></div><div style="font-family: arial, helvetica, sans-serif; font-size: 10px; font-weight: bold; color: #808080;"><b>' + data.address + '</b></div><div style="font-family: arial, helvetica, sans-serif; font-size: 10px; font-weight: bold; color: #808080;"><b>' + data.city + ', GA ' + zip + '</b></div><div style="font-family: arial, helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #00F;"><b><a target="_blank" href= ' + '' + hyplink2 + "" + '> FILE ID: ' + data.fileLabel + '</a></b></div><div style="font-family: arial, helvetica, sans-serif; font-size: 10px; font-weight: bold; color: #808080;"><b>STATUS: ' + data.facilityStatus + '</b></div>');
                }
                //infowindow.setContent(data.name);
                infowindow.open(map, marker);
            });
        })(marker, data);
       
    }


}

function getLatLongs(addr, city, zip) {
    var lat = 0;// 33.879807;
    var lng = 0;// -87.306964;
    var address = addr + " " + city + ", GA " + zip;        
   // var address = $('#Address').val();    
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

            //$('#Latitude').val(parseFloat(lat).toFixed(4));
            //$('#Longitude').val(parseFloat(lng).toFixed(4));
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
function getCoordinates(address) {
    var lat = 0;// 33.879807;
    var lng = 0;// -87.306964;   
   
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
            $('#Latitude').val(parseFloat(lat).toFixed(4));
            $('#Longitude').val(parseFloat(lng).toFixed(4));         
        }
        else {
            alert("Geocode was not successful for the following reason: " + status);
        }

    });

}

function mapInitialize1(lat, lng, radius, markers) {
    //debugger;
    var mapOptions = {
        center: new google.maps.LatLng(lat, lng),
        zoom: 10,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var infoWindow = new google.maps.InfoWindow();
    var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);


    for (i = 0; i < markers.length; i++) {
        var data = markers[i]
        var myLatlng = new google.maps.LatLng(data.latitude, data.longitude);
        var marker = new google.maps.Marker({
            position: myLatlng,
            map: map,
            title: data.facilityNumber
        });
        (function (marker, data) {
            google.maps.event.addListener(marker, "click", function (e) {
                infoWindow.setContent(data.name);
                infoWindow.open(map, marker);
            });
        })(marker, data);
    }
}
