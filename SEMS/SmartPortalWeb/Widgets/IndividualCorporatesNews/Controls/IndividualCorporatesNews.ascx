<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IndividualCorporatesNews.ascx.cs" Inherits="Widgets_IndividualCorporatesNews_Controls_IndividualNews" %>
<link href="Widgets/IndividualCorporatesNews/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<table width="100%">
    <tr>
        <td>
            <asp:Label ID="hpTitle" CssClass="icTitle" runat="server">HyperLink</asp:Label>
            <hr class="linenews" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataList ID="dlInvidualNews" runat="server">
                <ItemTemplate>
                    <img alt="" src="Widgets/IndividualCorporatesNews/Images/iconarrow.gif" style="width: 16px; height: 15px" /><asp:HyperLink
                        ID="hpCategory" runat="server" Text='<%# Eval("CatName") %>'
                        NavigateUrl='<%#SmartPortal.Common.Encrypt.EncryptURL(ConfigurationManager.AppSettings["newsofcategoryIC"]+"&catid="+Eval("CatID")) %>'></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle CssClass="dlIC" />
            </asp:DataList>
        </td>
    </tr>
</table>
