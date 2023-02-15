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