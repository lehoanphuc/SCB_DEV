<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_LoginInfo_Widget" %>

<link href="Widgets/LoginInfo/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<table style="margin:5px auto 5px auto; width:100%">
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, hello %>'></asp:Label>
            :</td>
        <td>
            <asp:Label ID="lblUser" runat="server" CssClass="logininfo"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label2" runat="server" Text='<%$ Resources:labels, role %>'></asp:Label>
            :</td>
        <td>
            <asp:Label ID="lblRole" runat="server" CssClass="logininfo"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label3" runat="server" Text='<%$ Resources:labels, lastlogintime %>'></asp:Label>
            :</td>
        <td>
            <asp:Label ID="lblLastLoginTime" runat="server" CssClass="logininfo"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>            
            &nbsp;</td>
    </tr>
    <tr>
        
        <td colspan="2" style="text-align:center;">            
            <asp:LinkButton ID="lbLogout" Text='<%$ Resources:labels, logout %>' 
                runat="server" onclick="lbLogout_Click" Font-Bold="True" 
                CausesValidation="False"></asp:LinkButton> &nbsp;|&nbsp
               <%-- <a href='<%= System.Configuration.ConfigurationManager.AppSettings["homebank"] %>'><%= Resources.labels.outsite %></a>&nbsp;|&nbsp;
                <a href='<%= System.Configuration.ConfigurationManager.AppSettings["homeadmin"] %>'><%= Resources.labels.insite %></a>&nbsp;|&nbsp;                
--%>                <a href='<%= System.Configuration.ConfigurationManager.AppSettings["changepasswordlink"] %>'><%= Resources.labels.changepassword %></a>
        </td>
    </tr>
</table>