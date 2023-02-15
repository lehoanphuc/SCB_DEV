<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popUpAjax.aspx.cs" Inherits="popUpAjax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Popup Ajax</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 225px; height: 67px; border: solid 1px #A0BEE2; background-color: #FFFFFF;">
     <div style="text-align: right; margin-right: 3px;"><a href="javascript:void(0);" title="close" onclick="ajax_hideTooltip();return false;"><img src="images/icon_delete.gif" align="absmiddle" border="0" alt="Close" /></a></div>
    <div style="margin: 2px;">
    <span class="cyel">&raquo;</span>&nbsp;<a href="findallrecipebyauthor.aspx?author=<%=SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["author"]%>" title="Browse all recipe by <%=SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["author"]%>." class="content2">Browse All Recipe by <%=SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["author"]%></a>
    </div>
    <div style="margin: 2px;">
    <span class="cyel">&raquo;</span>&nbsp;<a href="userprofile.aspx?uid=<%=SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"]%>" title="View <%=SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["author"]%> profile." class="content2">View <%=SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["author"]%> profile</a>
    </div> 
    </div>   
    </form>
</body>
</html>
