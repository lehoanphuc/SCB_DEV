var temp;
var i = 0;
var k = 0;
function executeCommaDo(id) {    
    temp = document.getElementById(id).value;
    
    afstr = temp.substr(temp.indexOf(".") + 1)
    temp = temp.substr(temp.indexOf(".") + 1)
	for (i=0;i<temp.length;i++) {
		for (k=i;k<temp.length;k++) {
			if (temp.charAt(k) == ',') {
				temp = temp.replace(',','');
			}
		}
	}

	var j = 0;
	var s = "";
	var s1 = "";
	var s2 = "";
	for (i=temp.length-1;i>=0;i--) {
		j = j+1;
		if (j == 3) {
			j = 0;
			s1 = temp.substring(0,i);
			s2 = temp.substring(i,i+3);
			s = "," + s2 + s;
		}
	}
	if (s1.length > 0) {
		s = s1 + s;
		document.getElementById(id).value = s;
	}else if (s.length > 0 && s2.length > 0){
		document.getElementById(id).value = s.substring(1,s.length);
	}
}

function executeCommaDo2(obj) {
	temp = obj.value;
	for (i=0;i<temp.length;i++) {
		for (k=i;k<temp.length;k++) {
			if (temp.charAt(k) == ',') {
				temp = temp.replace(',','');
			}
		}
	}

	var j = 0;
	var s = "";
	var s1 = "";
	var s2 = "";
	for (i=temp.length-1;i>=0;i--) {
		j = j+1;
		if (j == 3) {
			j = 0;
			s1 = temp.substring(0,i);
			s2 = temp.substring(i,i+3);
			s = "," + s2 + s;
		}
	}
	if (s1.length > 0) {
		s = s1 + s;
		obj.value = s;
	}else if (s.length > 0 && s2.length > 0){
		obj.value = s.substring(1,s.length);
	}
}

function executeComma(id, event) {
    if (document.getElementById(id).value.indexOf(".")>0 && document.getElementById(id).value.indexOf(".") + 3 < document.getElementById(id).value.length)
    {
        alert("Amount allows up to two decimal places");
        document.getElementById(id).value = "";
    }
    else if (document.getElementById(id).value.substring(0, 1) == "0" && document.getElementById(id).value.length > 1)
	    {	        
		    document.getElementById(id).value = "";		
	    }
        else if (document.getElementById(id).value.substring(0, 1) == "." && document.getElementById(id).value.length == 1) {
                alert("aa");
                document.getElementById(id).value = "";
            }
   	else if ((event.keyCode >= 96 && event.keyCode <= 105)) {
   	    executeCommaDo(id);
	}
	else if (event.keyCode >= 48 && event.keyCode <= 57) {
	    executeCommaDo(id);
	}
	else if (event.keyCode == 8 || event.keyCode == 46 || event.keyCode == 19 || event.keyCode == 9) {
	
	    executeCommaDo(id);
	}
	else if (event.keyCode == 190 || event.keyCode == 110) {

	    executeCommaDo(id);
	}
	else if (detectmob()) {
	    executeCommaDo(id);
	}
    
	
	else {
	    alert("Please input number !" + getElementById(id).value);
		document.getElementById(id).value = "";		
	}
}

function executeComma1(event, obj) {
	if ((event.keyCode >= 96 && event.keyCode <= 105)) {
		executeCommaDo2(obj);
	}
	else if (event.keyCode >= 48 && event.keyCode <= 57) {
		executeCommaDo2(obj);
	}
	else if (event.keyCode == 8 || event.keyCode == 46 || event.keyCode == 19 || event.keyCode == 9) {
		executeCommaDo2(obj);
	}
	else if (detectmob()) {
	    executeCommaDo(id);
	}
	else {
		alert("Please input number !");
		obj.value = "";
	}
}

function detectmob() {
    if (navigator.userAgent.match(/Android/i))
    {
        return true;
    }
    else
    {
        return false;
    }
}
function replaceSQLChar(obj) {
    if (obj.value.length > 0) {
        obj.value = obj.value.replace(/'|!|#|\$|%|\^|&|\*/g, "");
    }
}


function numberWithCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}
function validatedecimal(id)
{
    temp = document.getElementById(id).value;
    if (id.indexOf(".") + 3 < id.length)
    {
        return true;
    }
    else
    {
        return false;
    }
}