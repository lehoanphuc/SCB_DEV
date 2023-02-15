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
function IsNumeric(sText,aler)
{
   var ValidChars = "0123456789.";
   var IsNumber=true;
   var Char;
   sText=document.getElementById(sText).value;
   
   if(sText!='')
   { 
       for (i = 0; i < sText.length && IsNumber == true; i++) 
          { 
          Char = sText.charAt(i); 
          if (ValidChars.indexOf(Char) == -1) 
             {
             IsNumber = false;
             window.alert(aler);
             return IsNumber;
             }
          }
       return IsNumber;
   }
   else
   {
    return true;
   }
   
 }