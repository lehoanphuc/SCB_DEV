<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpRoleTransaction_Widget" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<link href="Widgets/semsroletransaction/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script src="Widgets/PagePermission/Scripts/JScript.js" type="text/javascript"></script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div>
<div style="padding:5px 0px 5px 5px; text-align:center;">
    
    <img alt="" src="widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="btnSave1" runat="server" Text='<%$ Resources:labels, save %>' onclick="btnSave_Click" 
                 />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="Button2" runat="server" Text='<%$ Resources:labels, exit %>' 
                CausesValidation="False" onclick="btnExit_Click" />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A1" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     <hr />
</div>

<asp:Panel runat="server" ID="pnFocus" DefaultButton="btnSave">

<div style=" text-align:left; padding:5px 1px 5px 1px; height:auto;">
<asp:Label ID="lblAlert" runat="server" Font-Bold="False" SkinID="lblImportant"></asp:Label>
</div>
<div style="height:15px; padding-bottom:5px; text-align:center;">
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
<ProgressTemplate>
<img alt="" src="Images/WidgetImage/ajaxloader.gif" style="width: 16px; height: 16px;" /> <%=Resources.labels.loading %>
</ProgressTemplate>
</asp:UpdateProgress>
</div>

<table id="pageadd" cellspacing="1">
     <tr>
        <td class="tdleft" style="width:30%;">
            <asp:Label ID="Label1" runat="server" Text='Dịch vụ'></asp:Label>            
            :            
        </td>
        <td>
            <asp:DropDownList ID="ddlService" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlService_SelectedIndexChanged" SkinID="extDDL1" Width="200px">
            </asp:DropDownList>
            </td>
    </tr>
    <tr>
        <td class="tdleft" style="width:30%;">
            <asp:Label ID="Label11" runat="server" Text='<%$ Resources:labels, group %>'></asp:Label>            
            :            
        </td>
        <td>
            <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlRole_SelectedIndexChanged" SkinID="extDDL1" Width="200px">
            </asp:DropDownList>
            </td>
    </tr>
    <tr>
        <td class="tdleft" valign="top">
            
            <asp:Label ID="Label3" runat="server" Text='Chức năng'></asp:Label>
            :
        </td>
        <td>
        <div style="height:400px; width:400px;overflow:auto">
            <asp:TreeView ID="tvPage" runat="server" ImageSet="Simple" 
                ontreenodecheckchanged="tvPage_TreeNodeCheckChanged" ShowLines="True">
                <ParentNodeStyle Font-Bold="False" />
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" 
                    HorizontalPadding="0px" VerticalPadding="0px" />
                <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" 
                    HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
            </asp:TreeView>
        </div>
        </td>
    </tr>
    <tr>
        <td>
            
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    
</table>
<div style="padding:5px 0px 5px 5px; text-align:center;">
    <hr />
    <img alt="" src="widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="btnSave" runat="server" Text='<%$ Resources:labels, save %>' onclick="btnSave_Click" 
                 />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="btnExit" runat="server" Text='<%$ Resources:labels, exit %>' 
                CausesValidation="False" onclick="btnExit_Click" />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="btnBack" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     
</div>

</asp:Panel>
</div>
</ContentTemplate>
</asp:UpdatePanel>