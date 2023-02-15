<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_Login_Widget" %>
<link href="Widgets/Login/CSS/Default.css" rel="stylesheet" type="text/css" />

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
        <td class="logintdleft">
            <img alt="" src="widgets/login/images/password.gif" style="width: 16px; height: 16px" /> <asp:Label ID="lblPassword" runat="server" text='<%$ Resources:labels, password %>'></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"  autocomplete="off" AutoCompleteType="Disabled"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:CheckBox ID="cbRememberMe" text='<%$ Resources:labels, rememberme %>' runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td style="padding-left:20px;">
            <asp:LinkButton ID="lbForgotPassword" 
                text='<%$ Resources:labels, forgotpassword %>' runat="server" 
                onclick="lbForgotPassword_Click">LinkButton</asp:LinkButton>
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
            <asp:Button ID="btnLogin" runat="server" text='<%$ Resources:labels, login %>' 
                onclick="btnLogin_Click" BackColor="#CCCCCC" BorderColor="#CCCCCC" 
                BorderStyle="Outset" />
        &nbsp;
            <asp:Button ID="btnExit" runat="server" text='<%$ Resources:labels, exit %>' 
                onclick="btnExit_Click" BackColor="#CCCCCC" BorderColor="#CCCCCC" 
                BorderStyle="Outset" />
        </td>
    </tr>
</table>
</asp:Panel>
</div>