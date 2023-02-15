var temp;
var i = 0;
var k = 0;
function executeCommaDo(id) {    
	temp = document.getElementById(id).value;
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

function executeComma(id,event) {
    if(document.getElementById(id).value.substring(0,1)=="0")
	    {	        
		    document.getElementById(id).value = "";		
	    }
	else if ((event.keyCode >= 96 && event.keyCode <= 105)) {
		executeCommaDo(id);
	}
	else if (event.keyCode >= 48 && event.keyCode <= 57) {
		executeCommaDo(id);
	}
	else if (event.keyCode == 8 || event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9) {
		executeCommaDo(id);
	}
	else {
		alert("Vui lòng nhập đúng định dạng số !");
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
	else if (event.keyCode == 8 || event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9) {
		executeCommaDo2(obj);
	}
	else {
		alert("Vui lòng nhập đúng định dạng số !");
		obj.value = "";
	}
}