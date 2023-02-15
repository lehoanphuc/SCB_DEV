<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_MenuPermission_Widget" %>
<link href="Widgets/MenuPermission/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script src="Widgets/MenuPermission/Scripts/JScript.js" type="text/javascript"></script>
<div>
<div style="padding:5px 0px 5px 5px; text-align:center;">
  
    <img alt="" src="widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="btnSave1" runat="server" Text='<%$ Resources:labels, save %>' onclick="btnSave_Click" 
                 />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="LinkButton2" runat="server" Text='<%$ Resources:labels, exit %>' 
                CausesValidation="False" onclick="btnExit_Click" />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A1" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
       <hr />
</div>
<div style=" text-align:left; padding:5px 1px 5px 1px; height:auto;">
<asp:Label ID="lblAlert" runat="server" Font-Bold="False" SkinID="lblImportant"></asp:Label>
</div>

<asp:Panel runat="server" ID="pnFocus" DefaultButton="btnSave">
<table id="pageadd" cellspacing="1">
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label11" runat="server" Text='<%$ Resources:labels, role %>'></asp:Label>            
            :            
        </td>
        <td>
            <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlRole_SelectedIndexChanged">
            </asp:DropDownList>
            </td>
    </tr>
    <tr>
        <td class="tdleft" valign="top">
            
            <asp:Label ID="Label3" runat="server" Text='<%$ Resources:labels, menu %>'></asp:Label>
            :
        </td>
        <td>
        <div style="height:400px; width:300px;overflow:auto">
            <asp:TreeView ID="tvMenu" runat="server" ImageSet="Simple" ShowLines="True">
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