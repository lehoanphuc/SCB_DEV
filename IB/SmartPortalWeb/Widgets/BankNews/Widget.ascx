<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_BankNews_Widget" %>
<%@ Register src="Controls/BankNews.ascx" tagname="BankNews" tagprefix="uc1" %>
<div>
<table style="width:100%;background-image:url(widgets/banknews/images/banner_bg.gif);background-repeat:repeat-x;">
    <tr>
        <td style="width:50%" valign="top">
            
            <uc1:BankNews ID="inBankNews" runat="server" />
            
        </td>
        <td style="width:50%" valign="top">
            
            <uc1:BankNews ID="outBankNews" runat="server" />
            
        </td>
    </tr>
</table>
</div>