

 function UpdateIsLogin(filename) {
     var xmlHttp = getXmlHttp();
        var file = filename+"?r="+Math.random()+"&mode=bb";
        xmlHttp.open("GET", file, true);
        xmlHttp.onreadystatechange=function() 
        {
            if (xmlHttp.readyState==4) 
            {
                
            }
        }
        xmlHttp.send(null);
    }
    ;
    
    function getXmlHttp()
    {
        var xmlHttp;
        try
        {  // Firefox, Opera 8.0+, Safari  
            xmlHttp=new XMLHttpRequest();  
        }
        catch (e)
        {  // Internet Explorer  
            try
            {    
                xmlHttp=new ActiveXObject("Msxml2.XMLHTTP");    
            }
            catch (e)
            {    
                try
                {      
                    xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");      
                }
                catch (e)
                {      
                    alert("Your browser does not support AJAX!");      
                    return false;      
                }    
            }  
        }  
        return xmlHttp;
    }
    