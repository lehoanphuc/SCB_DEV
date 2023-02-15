<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_ExchangeRate_Widget" %>
<%@ Register src="../../Controls/WidgetHTML/WidgetHTML.ascx" tagname="WidgetHTML" tagprefix="uc1" %>
<style>
    .curTitle
    {
    	font-weight:bold;
    }
</style>
<table width="100%">
    <tr>
        <td style="width:25%">
           <span class="curTitle"><%=Resources.labels.currency %></span> 
        </td>
        <td style="width:25%">
           <span class="curTitle"><%=Resources.labels.buytm %></span> 
        </td>
        <td style="width:25%">
           <span class="curTitle"><%=Resources.labels.buy %></span> 
        </td>
        <td style="width:25%">
           <span class="curTitle"><%=Resources.labels.sell %></span> 
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <uc1:WidgetHTML ID="whtml" runat="server" />
        </td>
    </tr>
</table>

