<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDetailNews.aspx.cs" Inherits="Widgets_NewsDetail_PrintPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="widgets/newsdetail/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <table class="style1">
        <tr>
            <td>                
                <img src="images/logo.gif" 
                    style="height: 68px; width: 138px" />
                    <hr />
            </td>
        </tr>
        <tr>
            <td>                
                <asp:Label ID="lblTitle" runat="server" CssClass="ndTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ndTDDate">
                <asp:Label ID="lblDateCreated" CssClass="ndDate" runat="server"></asp:Label>
                <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, postby %>'></asp:Label>
                <asp:Label ID="lblAuthor" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ndTDContent">
                <asp:Label ID="lblSummary" runat="server" Font-Italic="True" Font-Size="Medium" 
                    ForeColor="Gray"></asp:Label>
            </td>
        </tr>        
        <tr>
            <td class="ndTDContent">
                <asp:Label ID="lblContent" runat="server"></asp:Label>
            </td>
        </tr>        
        <tr>
            <td class="ndTDContent">                      
            </td>
        </tr>        
    </table>
    
    </div>
    </form>
</body>
</html>
