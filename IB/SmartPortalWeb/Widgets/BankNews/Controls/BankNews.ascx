<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BankNews.ascx.cs" Inherits="Widgets_BankNews_Controls_BankNews" %>
<link href="Widgets/BankNews/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<table width="100%">
    <tr>
        <td>            
            <asp:Label ID="hpTitle" CssClass="bankNewsTitle" runat="server">HyperLink</asp:Label> 
            <hr class="linebanknews" />           
        </td>
    </tr>
    <tr>
        <td>            
            <asp:DataList ID="dlBankNews" runat="server">
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <img alt="" src="Widgets/BankNews/Images/iconarrow.gif" 
                                    style="width: 16px; height: 15px" /></td>
                            <td>
                                <asp:HyperLink ID="hpNewsTitle" NavigateUrl='<%#System.Configuration.ConfigurationManager.AppSettings["newsdetail"]+"&nid="+Eval("NewsID") %>' runat="server" Text='<%# Eval("Title") %>'></asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>                                  
                                 <asp:Label Font-Size="8pt" ID="lblSummary" runat="server" Text='<%# Eval("Summary") %>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <ItemStyle CssClass="dlBankNews" />
            </asp:DataList>            
        </td>
    </tr>    
</table>