<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_ResetPassword_Widget" %>

<link href="Widgets/Login/CSS/Default.css" rel="stylesheet" type="text/css" />
<script>
    function usernamevalidate() {
        if (document.getElementById('<%=txtUserName.ClientID %>').value != '') {
            return true;
        }
        else {
            alert('<%=Resources.labels.usernamerequire %>');
            return false;
        }
    }
</script>

<div id="login">
<div style=" text-align:center; margin:5px 1px 5px 1px;">
<asp:Label ID="lblAlert" runat="server" Font-Bold="False" SkinID="lblImportant"></asp:Label>
</div>

<asp:Panel ID="pnFocus" DefaultButton="btnLogin" runat="server">
<table class="tblLogin">
    <tr>
        <td class="logintdleft">
            <img alt="" src="widgets/login/images/user.gif" style="width: 16px; height: 16px" /> <asp:Label ID="lblUserName" runat="server" text='<%$ Resources:labels, username %>'></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <img alt="" src="widgets/Widget/view/images/action_refresh.gif" style="width: 16px; height: 16px" />
            <asp:LinkButton ID="btnLogin" runat="server" text='<%$ Resources:labels, reset %>' OnClientClick="return usernamevalidate();" 
                onclick="btnLogin_Click" />
        &nbsp;
            <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
            <a ID="btnExit" 
                href="javascript:history.go(-1);" ><%=Resources.labels.back %></a>
        </td>
    </tr>
</table>
</asp:Panel>
</div>