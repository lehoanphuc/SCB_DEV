function validateEmpty(id,msg)
{
    if(document.getElementById(id).value=="")
    {
        window.alert(msg);
        return false;
    }
    else
    {
    return true;
    }
}
function validateMoney(id,msg)
{
    if(document.getElementById(id).value=="0" || document.getElementById(id).value=="")
    {
        window.alert(msg);
        return false;
    }
    else
    {
    return true;
    }
}
function IsNumeric(sText) {
    var ValidChars = "0123456789.";
    var Char;


    for (i = 0; i < sText.length && IsNumber == true; i++) {
        Char = sText.charAt(i);
        if (ValidChars.indexOf(Char) == -1) {
            IsNumber = false;
            alert(aler);
        }
    }
    return IsNumber;

}
function checkEmail(emailID, aler) {
    var email = document.getElementById(emailID);
    var value = document.getElementById(emailID).value;
    if (value != '') {
        var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(email.value)) {
            alert(aler);
            return false;
        }
        return true;
    }
    else {
        return true;
    }
}