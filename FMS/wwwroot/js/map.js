function getLatLong(addr, city, zip) {
	var address = 	addr + ", " + city + ", GA " + zip;
	
	geocoder = new google.maps.Geocoder();
	geocoder.geocode( { 'address': address}, function(results, status) {
	  	if (status == google.maps.GeocoderStatus.OK) {
	    	map.setCenter(results[0].geometry.location);
	    	var marker = new google.maps.Marker({
	    	map: map,
	    	position: results[0].geometry.location
	  		});
		}
 	});
}

function initialize() {
	var JSON = http://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&sensor=false;
    var myLatlng = new google.maps.LatLng(-34.397, 150.644);
    var myOptions = {
      zoom: 8,
      center: myLatlng,
      mapTypeId: google.maps.MapTypeId.ROADMAP
    }
    var map = new google.maps.Map(document.getElementById("map"), myOptions);
  }
