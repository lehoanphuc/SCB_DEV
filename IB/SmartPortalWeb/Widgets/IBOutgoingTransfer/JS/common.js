function validateEmpty(id,msg)
{
    if(document.getElementById(id).value=="")
    {
        swalWarning(msg);
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
        swalWarning(msg);
        return false;
    }
    else
    {
    return true;
    }
}