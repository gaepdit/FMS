$(document).ready(function mapGeocode() {
    $("form input").keypress(function (e) {
        if (e.which === 13) {
            $("#SubmitButton").click();
            return false;
        }
    });

    $("#GeocodeButton").click(function GeocodeAddress()
    {
        const addr = $("#Facility_Address").val();
        const warningLabel = $("#GeocodeAddressWarn");
        if (addr === "") {
            warningLabel.removeClass("d-none");
            $("#Facility_Latitude").val("");
            $("#Facility_Longitude").val("");
            return false;
        }

        warningLabel.addClass("d-none");
        const city = $("#Facility_City").val();
        const st = $("#Facility_State").val();
        const zip = $("#Facility_PostalCode").val();
        getCoordinates(addr + ", " + city + ", " + st + " " + zip);
        return false;
    });

    function getCoordinates(address) {
        const request = {
            address: address,
            componentRestrictions: {
                country: 'US'
            }
        };
        const geocoder = new google.maps.Geocoder();
        geocoder.geocode(request, function (results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
                $("#Facility_Latitude").val(results[0].geometry.location.lat().toFixed(6));
                $("#Facility_Longitude").val(results[0].geometry.location.lng().toFixed(6));
            }
            else {
                alert("Geocode was not successful for the following reason: " + status);
            }
        });
    }
});
