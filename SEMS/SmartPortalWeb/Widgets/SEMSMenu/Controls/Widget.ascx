<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_Menu_Controls_Widget" %>
<link href="Widgets/Menu/Controls/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    function menuvalidate() {
        if (document.getElementById('<%=txtTitle.ClientID %>').value == '') {
            alert('<%=Resources.labels.menutitlerequire %>');
            return false;
        }
        else {
            return true;
        }
    }
</script>

<div style="padding:5px 0px 5px 5px; text-align:center;">
    
    <asp:Image runat="server" ID="imgSave" AlternateText="" ImageUrl="~/widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="btnSave1" runat="server" Text='<%$ Resources:labels, save %>' OnClientClick="return menuvalidate();" 
                onclick="btnSave_Click"/>
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="btnExit1" runat="server" Text='<%$ Resources:labels, exit %>' 
                CausesValidation="False" onclick="btnExit_Click1" />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A1" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     <hr />
</div>
<div style=" text-align:left; padding:5px 1px 5px 1px; height:auto;">
<asp:Label ID="lblAlert" runat="server" Font-Bold="False" SkinID="lblImportant"></asp:Label>

    <br />

</div>
<div style=" text-align:right; margin:5px 1px 5px 1px; padding-right:5px;">
<asp:Label ID="Label7" runat="server" Font-Bold="False" SkinID="lblImportant">*</asp:Label>
<asp:Label ID="Label8" runat="server" SkinID="lblImportant" Text='<%$ Resources:labels, requiredfield %>'></asp:Label>
</div>

<asp:Panel runat="server" ID="pnFocus" DefaultButton="btnSave">
<table id="pageadd" cellspacing="1">
    <tr>
        <td class="tdleft" style="width:35%;">
            <asp:Label ID="Label3" runat="server" SkinID="lblImportant" Text="*"></asp:Label>
            <asp:Label ID="Label6" runat="server" SkinID="lblImportant" 
                Text="<%$ Resources:labels, menuid %>"></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtMenuid" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="tdleft" style="width:35%;">
            <asp:Label ID="Label9" runat="server" SkinID="lblImportant" Text="*"></asp:Label>
            <asp:Label ID="Label4" runat="server" SkinID="lblImportant" 
                Text="<%$ Resources:labels, title %>"></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label01" runat="server" Text="<%$ Resources:labels, portal %>"></asp:Label>
            &nbsp;:</td>
        <td>
            
            <asp:DropDownList ID="ddlPortal" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlPortal_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, parent %>"></asp:Label>
            :</td>
        <td>
            <asp:DropDownList ID="ddlParent" runat="server" SkinID="extDDL1">
            </asp:DropDownList>
            &nbsp;<img alt="" onmouseout="UnTip()" 
                onmouseover="<%=Resources.labels.parenttip %>" 
                src="App_Themes/Bank2/images/help.gif" style="width: 17px; height: 17px" /></td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, page %>"></asp:Label>
            :</td>
        <td>
            <asp:DropDownList ID="ddlPage" runat="server" SkinID="extDDL2">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, order %>" 
                ></asp:Label>:</td>
        <td>
            <asp:DropDownList ID="ddlOrder" runat="server" >
            </asp:DropDownList>
            &nbsp;<img alt="" onmouseout="UnTip()" 
                onmouseover="<%=Resources.labels.ordertip %>" 
                src="App_Themes/Bank2/images/help.gif" style="width: 17px; height: 17px" /></td>
    </tr>
    <tr>
        <td class="tdleft">
        </td>
        <td>
            <asp:CheckBox ID="cbIsShow" runat="server" Checked="True" 
                Text="<%$ Resources:labels, isshow %>" />
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            
            &nbsp;</td>
        <td>
            <asp:CheckBox ID="cbLink" runat="server" Checked="True" 
                Text="<%$ Resources:labels, enablelink %>"  
                oncheckedchanged="cbLink_CheckedChanged" AutoPostBack="True" />
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
    <asp:Image runat="server" ID="imgSave1" AlternateText="" ImageUrl="~/widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="btnSave" runat="server" Text='<%$ Resources:labels, save %>' OnClientClick="return menuvalidate();" 
                onclick="btnSave_Click"/>
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="btnExit" runat="server" Text='<%$ Resources:labels, exit %>' 
                CausesValidation="False" onclick="btnExit_Click1" />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A2" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     
</div>
</asp:Panel>