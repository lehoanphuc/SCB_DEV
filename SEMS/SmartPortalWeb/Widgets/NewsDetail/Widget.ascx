<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_NewsDetail_Widget" %>

<link href="widgets/newsdetail/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<div>
    <table class="style1">
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
                <asp:Label ID="lblContent" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ndTDExtend">
                <img align="top" alt="" src="widgets/newsdetail/Images/email_icon.gif" style="width: 16px; height: 16px" />
                <a href="widgets/newsdetail/Email4U.htm" class="lbOn" title="Email"><%=Resources.labels.email %></a>&nbsp;&nbsp;
                <img alt="" src="widgets/newsdetail/Images/print_icon.gif" style="width: 16px; height: 16px" />
                <a href='<%="widgets/newsdetail/PrintPage.aspx?nid="+SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"] %>' target="_blank"><%= Resources.labels.print %></a>
                </td>             
        </tr>        
    </table>
</div>
