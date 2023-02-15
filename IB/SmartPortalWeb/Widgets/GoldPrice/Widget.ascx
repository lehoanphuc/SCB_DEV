<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_GoldPrice_Widget" %>
<%@ Register src="../../Controls/WidgetHTML/WidgetHTML.ascx" tagname="WidgetHTML" tagprefix="uc1" %>
<style>
    .curTitle
    {
    	font-weight:bold;
    }
</style>
<table width="100%">
    <tr>
        <td style="width:40%">
           <span class="curTitle"><%=Resources.labels.gold %></span> 
        </td>
        <td style="width:30%">
           <span class="curTitle"><%=Resources.labels.buy %></span> 
        </td>
        <td style="width:30%">
           <span class="curTitle"><%=Resources.labels.sell %></span> 
        </td>
    </tr>
    <tr>
        <td colspan="3">            
            <uc1:WidgetHTML ID="WHTMLGoldPrice" runat="server" />            
        </td>
    </tr>
</table>