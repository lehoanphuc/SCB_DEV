<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportToFile.aspx.cs" Inherits="Widgets_SEMSCustomerListCorp_ExportToFile_aspx" %>

<%@ Register src="ExportToFile.ascx" tagname="ExportToFile" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form runat="server" id="form1">
    <div>
    
        <uc1:ExportToFile ID="ExportToFile1" runat="server" />
    
    </div>
    </form>
</body>
</html>
