<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_RelatedNews_Widget" %>

<link href="widgets/relatednews/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<table class="style1">
    <tr>
        <td>
            <asp:Label ID="lblRNTitle" CssClass="rnTitle" runat="server" Text='<%$ Resources:labels, relatednews %>'></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataList ID="dlRelatedNews" runat="server">
                <ItemTemplate>
                    <img alt="" align="top" src="widgets/relatednews/Images/iconarrow.gif" 
    style="width: 16px; height: 15px" /><asp:HyperLink ID="lblTitle" runat="server" 
                        Text='<%# Eval("Title") %>' NavigateUrl='<%#System.Configuration.ConfigurationManager.AppSettings["newsdetail"]+"&nid="+Eval("NewsID").ToString() %>'></asp:HyperLink>
                    <asp:Label ID="lblDateCreated" CssClass="rnDate" runat="server" Text='<%# "("+SmartPortal.Common.Utilities.Utility.FormatDatetime(SmartPortal.Common.Utilities.Utility.IsDateTime2(Eval("DateCreated").ToString()).ToString("dd/MM/yyyy HH:mm:ss"),"dd/MM/yyyy HH:mm:ss", SmartPortal.Common.Utilities.DateTimeStyle.DateMMM)+")" %>'></asp:Label>
                </ItemTemplate>
            </asp:DataList>
        </td>
    </tr>
</table>
