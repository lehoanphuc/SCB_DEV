<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_Pages_View_Widget" %>

<link href="Widgets/Pages/View/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="Widgets/Pages/View/Scripts/JScript.js" type="text/javascript"></script>


<div style="padding:5px 0px 5px 5px;">
    <img alt="" src="widgets/pages/view/images/add.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="lbAddPage" Text='<%$ Resources:labels, addpage %>' 
        runat="server" onclick="lbAddPage_Click"></asp:LinkButton>
    &nbsp;
    <img alt="" src="widgets/pages/view/images/action_delete.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="lbDeleteSelected" 
        Text='<%$ Resources:labels, deleteselected %>' runat="server" 
        onclick="lbDeleteSelected_Click"></asp:LinkButton>
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="btnBack" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     <hr />
</div>
<div style=" text-align:center; margin:5px 1px 5px 1px;">
<asp:Label ID="lblAlert" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
</div>
<div style="text-align:left; padding:5px 5px 5px 5px;">
<asp:Panel runat="server" ID="pnSearch" DefaultButton="ibSearch">
    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
&nbsp;<asp:ImageButton ID="ibSearch" 
        ImageUrl="~/Widgets/widget/view/images/search.gif" runat="server" 
        onclick="ibSearch_Click" />
</asp:Panel>
</div>
<div>
<table style="margin:5px auto 5px auto; width:100%;">
    <tr>
        <td>
            <asp:GridView ID="gvPages" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                ForeColor="#333333" GridLines="None" 
                 PageSize="15" onpageindexchanging="gvPages_PageIndexChanging" 
                onrowdatabound="gvPages_RowDataBound" onsorting="gvPages_Sorting" 
                Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblPageID" runat="server" Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, pagename %>' SortExpression="PageName">
                        <ItemTemplate>
                            <img alt="" src="widgets/pages/view/images/page.gif" />
                           <asp:HyperLink ID="hpPageName" runat="server">[hpPageName]</asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, desc %>' SortExpression="PageDescription">
                        <ItemTemplate>
                            <asp:Label ID="lblPageDescription" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, author %>' SortExpression="Author">
                        <ItemTemplate>
                            <asp:Label ID="lblAuthor" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, datecreated %>' 
                        SortExpression="DateCreated">
                        <ItemTemplate>
                            <asp:Label ID="lblDateCreated" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, masterpage %>' SortExpression="MasterPageName">
                        <ItemTemplate>
                            <asp:Label ID="lblMasterPageName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, theme %>' SortExpression="ThemeName">
                        <ItemTemplate>
                            <asp:Label ID="lblThemeName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, isshow %>'>
                        <ItemTemplate>
                            <asp:Image ID="imgShow" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, edit %>'>
                        <ItemTemplate>
                            
                            <asp:HyperLink ID="hpEdit" runat="server">HyperLink</asp:HyperLink>
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, delete %>'>
                        <ItemTemplate>
                            
                            <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText='<%$ Resources:labels, preview %>'>
                        <ItemTemplate>
                            
                            <asp:HyperLink ID="hpPreview" runat="server">[hpPreview]</asp:HyperLink>
                            
                        </ItemTemplate>
                        
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle CssClass="pager" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#BCDFFB" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </td>
    </tr>
</table>  
</div>

<div style="padding:5px 0px 5px 5px;">
    <hr />   
    <img alt="" src="widgets/pages/view/images/add.gif" style="width: 16px; height: 16px" /> 
    <asp:LinkButton ID="lbAddPage1" Text='<%$ Resources:labels, addpage %>' 
        runat="server" onclick="lbAddPage_Click"></asp:LinkButton>
    &nbsp;
    <img alt="" src="widgets/pages/view/images/action_delete.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="lbDeleteSelected1" 
        Text='<%$ Resources:labels, deleteselected %>' runat="server" 
        onclick="lbDeleteSelected_Click"></asp:LinkButton>
    &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="btnBack1" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>
</div>
<script>
    function DeleteConfirm(obj) {
        if (window.confirm(obj)) {

        }
    }
</script>