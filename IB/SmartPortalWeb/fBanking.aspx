<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fBanking.aspx.cs" Inherits="fBanking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=Resources.labels.iblogintitle %></title>

    <script src="Template/Face1/Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>

    <link href="App_Themes/InternetBanking/login.css" rel="stylesheet" type="text/css" />
    <link rel="icon" type="image/ico" href="~/favicon.ico"></link>
    <link rel="shortcut icon" href="~/favicon.ico" type="image/x-icon" />
</head>
<form id="form1" runat="server">
    <div style="margin-left: auto; margin-right: auto; text-align: center;">
        <br></br><br></br><br></br>
        <asp:Label ID="LbResult" runat="server" Text="Label" Font-Bold="True" Font-Size="Medium" ForeColor="#FF5050"></asp:Label>
    </div>
</form>
</html>
