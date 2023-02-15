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
function validateFormTo(id,id2,msg)
{
    if(document.getElementById(id2).value - document.getElementById(id).value<0)
    {
        window.alert(msg);
        return false;
    }
    else
    {
    return true;
    }
}
function IsDateGreater(DateValue1, DateValue2, aler) {
    var DaysDiff;
    DateValue1 = document.getElementById(DateValue1).value;
    DateValue2 = document.getElementById(DateValue2).value;

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
    if (DaysDiff > 0)
        return true;
    else
        window.alert(aler);
    return false;
}