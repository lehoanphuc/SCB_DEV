<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_NewsOfCategory_Widget" %>

<link href="widgets/newsofcategory/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<div id="divNOC">
    <asp:GridView ID="gvNOC" runat="server" AutoGenerateColumns="False" 
        GridLines="None" onrowdatabound="gvNOC_RowDataBound" ShowHeader="False" 
        Width="100%" AllowPaging="True" 
        onpageindexchanging="gvNOC_PageIndexChanging" PageSize="7">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <table class="tblNOC" >
                        <tr>
                            <td>
                                <img align="bottom" alt="" src="widgets/newsofcategory/Images/rt.gif" 
                                     />&nbsp; <asp:HyperLink ID="hpTitle" 
                                    runat="server" CssClass="nocTitle">[hpTitle]</asp:HyperLink>
                              
                                <asp:Label ID="lblDateCreated" runat="server" CssClass="nocDateCreated"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="nocTDSummary">
                                <asp:Label ID="lblSummary" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center">
                                <img alt="" src="widgets/newsofcategory/Images/content_break_line.jpg" 
                                    style="width: 458px; height: 10px" /></td>
                        </tr>
                        <tr>
                            <td align="center">
                                &nbsp;</td>
                        </tr>
                    </table>
                </ItemTemplate>                
            </asp:TemplateField>            
        </Columns>
         <PagerStyle CssClass="pager" HorizontalAlign="Center" />
    </asp:GridView>
</div>

<div>
<asp:Panel runat="server" ID="pnNewsDetail">
    <table class="style1">
        <tr>
            <td>                
                <asp:Label ID="lblTitle" runat="server" CssClass="ndTitle"></asp:Label>
                <asp:Label ID="lblNewsID" runat="server" Visible="False"></asp:Label>
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
                <a href='<%="widgets/newsdetail/PrintPage.aspx?nid="+lblNewsID.Text %>' target="_blank"><%= Resources.labels.print %></a>
                </td>             
        </tr>
    </table>
</asp:Panel>
</div>