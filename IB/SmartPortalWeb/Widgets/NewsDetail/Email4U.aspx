<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Email4U.aspx.cs" Inherits="Widgets_NewsDetail_Email4U" %>

<%@ Register src="../../Controls/SendMail/Widget.ascx" tagname="Widget" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
            font-family:Verdana;
            font-size:9pt;
        }
        .tdEmailLeft
        {
        	
        }
        .emailrequired
        {
        	color:Maroon;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      
    
        <uc1:Widget ID="Widget1" runat="server" />
      
    
    </div>
    </form>
</body>
</html>
