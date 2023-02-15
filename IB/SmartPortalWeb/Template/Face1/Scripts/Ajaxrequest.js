/*******************************************************
Author: Dexter Zafra
Website: www.ex-designz.net and www.myasp-net.com
Purpose: Check Username Availability and email during registration. 
            This is the same code use in Ex-designz.net registration page.

*******************************************************/

//Global variable
 var http = createRequestObject();

//Return request object
 function createRequestObject() 
     {
         var xmlhttp;
         if (window.XMLHttpRequest) {
             // code for IE7+, Firefox, Chrome, Opera, Safari
             xmlhttp = new XMLHttpRequest();
         }
         else if (window.ActiveXObject) {
             // code for IE6, IE5
             xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
         }
         else {
             alert("Your browser does not support XMLHTTP!");
         }
         return xmlhttp;
 }

//***********************************************************************************************/
//Begin check username availability - Send the username via GET
//***********************************************************************************************/
function sendRequestWidgetPosition(portalID,pageID,moduleID,position,order) 
  {
      try {               
                 http.open('Get','AjaxRequest/AjaxRequest.aspx?mode=wp&po='+portalID+'&p='+pageID+'&w='+moduleID+'&ps='+position+'&o='+order);
                 http.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
                 http.onreadystatechange = handleResponseWidgetPosition;
	             http.send(null);
	 }
	    catch(e){alert(e)}
	    finally{}
  }

//Get the username response text
  function handleResponseWidgetPosition() 
  {
         
         if((http.readyState == 4)&& (http.status == 200))
             {
               
             }
}
//End ************************************************************************************************/

//***********************************************************************************************/
//Begin check username availability - Send the username via GET
//***********************************************************************************************/
function sendRequestRemoveWidgetInPage(wpid) {
    try {
        http.open('Get', 'AjaxRequest/AjaxRequest.aspx?mode=rwip&wpid='+ wpid );
        http.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        http.onreadystatechange = handleResponseRemoveWidgetInPage;
        http.send(null);
    }
    catch (e) { alert(e) }
    finally { }
}

//Get the username response text
function handleResponseRemoveWidgetInPage() {

    if ((http.readyState == 4) && (http.status == 200)) {

    }
}


