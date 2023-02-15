﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpRole_View_Widget" %>
<link href="Widgets/Group/View/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="Widgets/Group/View/Scripts/JScript.js" type="text/javascript"></script>


<div style="padding:5px 0px 5px 5px; text-align:center;">
    <img alt="" src="widgets/Group/view/images/add.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="lbAddPage" Text='<%$ Resources:labels, addgroup %>' 
        runat="server" onclick="lbAddPage_Click" ></asp:LinkButton>
    &nbsp;
    <img alt="" src="widgets/Group/view/images/action_delete.gif" style="width: 16px; height: 16px" /><asp:LinkButton ID="lbDeleteSelected" 
        Text='<%$ Resources:labels, deleteselected %>' runat="server" onclick="lbDeleteSelected_Click"  
        ></asp:LinkButton>
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
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, keyword %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Dịch vụ"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlServiceID" runat="server" SkinID="extDDL1">
                </asp:DropDownList>
            </td>
            <td>
                <asp:ImageButton ID="ibSearch" runat="server" 
                    ImageUrl="~/Widgets/widget/view/images/search.gif" onclick="ibSearch_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
</div>


<div>
<table style="margin:5px auto 5px auto; width:100%;">
    <tr>
        <td>
            <asp:GridView ID="gvGroup" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                ForeColor="#333333" GridLines="None" 
                 PageSize="15" Width="100%" onrowdatabound="gvUser_RowDataBound" 
                onpageindexchanging="gvGroup_PageIndexChanging" onsorting="gvGroup_Sorting" 
                >
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                           <center><asp:CheckBox ID="cbxSelect" runat="server" /></center> 
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField>
                        <ItemTemplate>
                            <center><asp:Label ID="lblRoleID" runat="server" Visible="false"></asp:Label></center>
                        </ItemTemplate>
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText='<%$ Resources:labels, rolename %>' SortExpression="RoleName">
                        <ItemTemplate>
                            <img src="widgets/group/view/images/group.gif" />
                           <asp:HyperLink ID="hpRoleName" runat="server"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    
                    <asp:TemplateField HeaderText='<%$ Resources:labels, roledesc %>' SortExpression="RoleDescription">
                        <ItemTemplate>                            
                           <asp:Label ID="lblRoleDescription" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                     
                    <asp:TemplateField HeaderText='<%$ Resources:labels, author %>' SortExpression="UserCreated">
                        <ItemTemplate>
                            <center><asp:Label ID="lblAuthor" runat="server"></asp:Label></center>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText='<%$ Resources:labels, datecreated %>' 
                        SortExpression="DateCreated">
                        <ItemTemplate>
                            <center><asp:Label ID="lblDateCreated" runat="server"></asp:Label></center>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    
                    <asp:TemplateField HeaderText='<%$ Resources:labels, edit %>'>
                        <ItemTemplate>
                            
                           <center><asp:HyperLink ID="hpEdit" runat="server">HyperLink</asp:HyperLink></center> 
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, delete %>'>
                        <ItemTemplate>
                            
                           <center><asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink></center> 
                            
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
            <br />
    <asp:Literal ID="litPager" runat="server"></asp:Literal>
        </td>
    </tr>
</table>  
</div>
<%--<div style="padding:5px 0px 5px 5px;text-align:center;">
    <hr />
    <img alt="" src="widgets/Group/view/images/add.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="LinkButton1" Text='<%$ Resources:labels, addgroup %>' 
        runat="server" onclick="lbAddPage_Click" ></asp:LinkButton>
    &nbsp;
    <img alt="" src="widgets/Group/view/images/action_delete.gif" style="width: 16px; height: 16px" /><asp:LinkButton ID="LinkButton2" 
        Text='<%$ Resources:labels, deleteselected %>' runat="server" onclick="lbDeleteSelected_Click"  
        ></asp:LinkButton>
      &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A1" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     
</div>--%>