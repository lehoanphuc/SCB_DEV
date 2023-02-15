<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IndividualCorporatesNews_Widget" %>
<%@ Register src="Controls/IndividualCorporatesNews.ascx" tagname="IndividualCorporatesNews" tagprefix="uc1" %>
<div>
<table style="width:100%;">
    <tr>
        <td style="width:50%" valign="top">
            <uc1:IndividualCorporatesNews ID="iN" runat="server" />
        </td>
        <td style="width:50%" valign="top">
            <uc1:IndividualCorporatesNews ID="cN" runat="server" />
        </td>
    </tr>
</table>
</div>