// JavaScript Document






function geturlparam( name )
{
  name = name.replace(/[\[]/,"\\\[").replace(/[\]]/,"\\\]");
  var regexS = "[\\?&]"+name+"=([^&#]*)";
  var regex = new RegExp( regexS );
  var results = regex.exec( window.location.href );
  if( results == null )
    return "";
  else
    return results[1];
}
function retrieveNearbySites() {
	//parse the address
	var address = 	$F('address') + ", " + $F('city') + ", GA " + $F('zip');
	//get the lat and lon
	
	//pass to servlet to retrieve nearby facilities
	new Ajax.Request('/prod/search/FacilitySearchManager?timeStamp=' + new Date().getTime(), {	
		method: 'post',
		parameters: {outFormat: $F('outFormat'),
					 radius: $F('radius'),
					 'source': 'getByAddress'
		},
		onSuccess: function(transport){		
     		var json = transport.responseText.evalJSON();
     		
     		 
      		     		    		      		    		
    	} 
	});
	
}

function clear_form_elements(ele) {

    tags = ele.getElementsByTagName('input');
    for(i = 0; i < tags.length; i++) {
        switch(tags[i].type) {
            case 'password':
            case 'text':
                tags[i].value = '';
                break;
            case 'checkbox':
            case 'radio':
                tags[i].checked = false;
                break;
        }
    }
   
    tags = ele.getElementsByTagName('select');
    for(i = 0; i < tags.length; i++) {
        if(tags[i].type == 'select-one') {
            tags[i].selectedIndex = 0;
        }
        else {
            for(j = 0; j < tags[i].options.length; j++) {
                tags[i].options[j].selected = false;
            }
        }
    }

    
   
}



function TrimString(sInString) {
  if ( sInString ) {
    sInString = sInString.replace( /^\s+/g, "" );// strip leading
    return sInString.replace( /\s+$/g, "" );// strip trailing
  }
}





