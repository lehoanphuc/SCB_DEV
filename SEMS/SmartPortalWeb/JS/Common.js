function validateEmpty(id, msg) {
    if (document.getElementById(id).value.trim() == "") {
        window.alert(msg);
        return false;
    }
    else {
        return true;
    }
}
function IsDateGreater(DateValue1, DateValue2, alert) {
    var DaysDiff;
    DateValue1 = document.getElementById(DateValue1).value.trim();
    DateValue2 = document.getElementById(DateValue2).value.trim();

    var dt1 = DateValue1.substring(0, 2);
    var mon1 = DateValue1.substring(3, 5);
    var yr1 = DateValue1.substring(6, 10);
    var dt2 = DateValue2.substring(0, 2);
    var mon2 = DateValue2.substring(3, 5);
    var yr2 = DateValue2.substring(6, 10);
    DateValue1 = mon1 + "/" + dt1 + "/" + yr1;
    DateValue2 = mon2 + "/" + dt2 + "/" + yr2;

    Date1 = new Date(DateValue1);
    Date2 = new Date(DateValue2);
    DaysDiff = Math.floor((Date1.getTime() - Date2.getTime()) / (1000 * 60 * 60 * 24));
    if (DaysDiff >= 0)
        return true;
    else
        window.alert(alert);
    return false;
}
//check white space
function hasWhiteSpace(id, msg) {
    var re = /\s/ig;
    if (re.test(document.getElementById(id).value.trim())) {
        window.alert(msg);
        return false;
    }
    else {
        return true;
    }
}

function executeComma(id) {
    var so = document.getElementById(id).value;
    so = so.toString().replace(/\$|\,/g, '');
    so = so.toString().replace(/\$|\-/g, '');
    if (so != "") {
        if (isNaN(so)) {
            so = "";
        } else {
            so = Math.floor(so * 100 + 0.50000000001);
            var cents;
            cents = so % 100;
            so = Math.floor(so / 100).toString();
            if (cents < 10)
                cents = "0" + cents;
            for (var i = 0; i < Math.floor((so.length - (1 + i)) / 3); i++)
                so = so.substring(0, so.length - (4 * i + 3)) + ',' +
                    so.substring(so.length - (4 * i + 3));
        }
    }
	if (so != null && so != "" && (Math.sign(parseInt(so)) == -1 || isNaN(Math.sign(parseInt(so))))) {
        so = "";
    }
    document.getElementById(id).value = so
}
function executeCommaAllowNegative(id) {
   
    var so = document.getElementById(id).value;
    var neg = so.indexOf('-');
    so = so.toString().replace(/\$|\,/g, '');
    so = so.toString().replace(/\$|\-/g, '');
    if (so != "") {
        if (isNaN(so)) {
            so = "";
        } else {
            so = Math.floor(so * 100 + 0.50000000001);
            var cents;
            cents = so % 100;
            so = Math.floor(so / 100).toString();
            if (cents < 10)
                cents = "0" + cents;
            for (var i = 0; i < Math.floor((so.length - (1 + i)) / 3); i++)
                so = so.substring(0, so.length - (4 * i + 3)) + ',' +
                    so.substring(so.length - (4 * i + 3));
        }
    }
    if (so != null && so != "" && (Math.sign(parseInt(so)) == -1 || isNaN(Math.sign(parseInt(so))))) {
        so = "";
    }
    if (neg == 0) {
        document.getElementById(id).value = -1
    }
    else {
        document.getElementById(id).value = so
    }
    
}
function isNumberKey(evt) {
    console.log(evt.keyCode);
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function validateFormTo(id, id2, msg) {
    if (document.getElementById(id2).value.replace(/\$|\,/g, '') - document.getElementById(id).value.replace(/\$|\,/g, '') < 0) {
        window.alert(msg);
        return false;
    }
    else {
        return true;
    }
}
function checkEmail(emailID, aler) {
    var value = document.getElementById(emailID).value;
    if (value != '') {
        var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(value)) {
            alert(aler);
            return false;
        }
        return true;
    }
    else {
        return true;
    }
}

function DateGreater(DateValue1, DateValue2, minDaysDiff, alert) {
    var DaysDiff;
    DateValue1 = document.getElementById(DateValue1).value.trim();
    DateValue2 = document.getElementById(DateValue2).value.trim();

    var dt1 = DateValue1.substring(0, 2);
    var mon1 = DateValue1.substring(3, 5);
    var yr1 = DateValue1.substring(6, 10);
    var dt2 = DateValue2.substring(0, 2);
    var mon2 = DateValue2.substring(3, 5);
    var yr2 = DateValue2.substring(6, 10);
    DateValue1 = mon1 + "/" + dt1 + "/" + yr1;
    DateValue2 = mon2 + "/" + dt2 + "/" + yr2;

    Date1 = new Date(DateValue1);
    Date2 = new Date(DateValue2);
    DaysDiff = Math.floor((Date1.getTime() - Date2.getTime()) / (1000 * 60 * 60 * 24));
    if (DaysDiff >= minDaysDiff)
        return true;
    else
        window.alert(alert);
    return false;
}

function ValidateLimit(obj, length) {
    if (this.id) obj = this;
    var mLen = length;
    var remaningChar = mLen - obj.value.length;
    if (remaningChar <= 0) {
        obj.value = obj.value.substring(mLen, 0);
    }
}