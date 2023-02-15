<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_Widget_Controls_Widget" %>
<link href="Widgets/Widget/Controls/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    function widgetvalidate() {
        if (document.getElementById('<%=txtTitle.ClientID %>').value == '') {
            alert('<%=Resources.labels.widgettitlerequire %>');
            return false;
        }
        else {
            if (document.getElementById('<%=txtPath.ClientID %>').value == '') {
                alert('<%=Resources.labels.widgetpathrequire %>');
                return false;
            }
            else {
                return true;
            }
        }
    }
</script>

<div style="padding:5px 0px 5px 5px; text-align:center;">
    
    <asp:Image runat="server" ID="imgSave1" AlternateText="" ImageUrl="~/widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="btnSave" runat="server" Text='<%$ Resources:labels, save %>' OnClientClick="return widgetvalidate();" onclick="btnSave_Click" 
                 />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="btnExit" runat="server" Text='<%$ Resources:labels, exit %>' 
                CausesValidation="False" onclick="btnExit_Click1"  />
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
        <td class="tdleft">
            <asp:Label ID="Label1" runat="server" SkinID="lblImportant" Text="*"></asp:Label>
&nbsp;<asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, widgetid %>" 
                SkinID="lblImportant"></asp:Label>
            :
        </td>
        <td>
            <asp:TextBox ID="txtWidgetid" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
            </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label9" runat="server" SkinID="lblImportant" Text="*"></asp:Label>
&nbsp;<asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, title %>" 
                SkinID="lblImportant"></asp:Label>
            :
        </td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
            </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label10" runat="server" SkinID="lblImportant" Text="*"></asp:Label>
            &nbsp;<asp:Label ID="Label11" runat="server" 
                Text="<%$ Resources:labels, path %>" SkinID="lblImportant"></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtPath" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
            &nbsp;<img alt="" src="App_Themes/Bank2/images/help.gif" onmouseover="<%=Resources.labels.widgetpathtip %>" onmouseout="UnTip()"
                style="width: 17px; height: 17px" /></td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label12" runat="server" Text="<%$ Resources:labels, icon %>"></asp:Label>
            :</td>
        <td>
            <asp:FileUpload ID="fuIcon" runat="server" />
            &nbsp;<asp:Label ID="lblPicture" runat="server" Visible="False"></asp:Label>
            &nbsp;<img alt="" onmouseout="UnTip()" 
                onmouseover="<%=Resources.labels.widgeticontip %>" 
                src="App_Themes/Bank2/images/help.gif" style="width: 17px; height: 17px" />&nbsp;</td>
    </tr>
    <tr>
        <td class="tdleft">
            
            &nbsp;</td>
        <td>
            <asp:CheckBox ID="cbEnableTheme" runat="server" 
                Text="<%$ Resources:labels, enabletheme %>" Checked="True" />
            </td>
    </tr>
    <tr>
        <td class="tdleft">
            &nbsp;</td>
        <td>
            <asp:CheckBox ID="cbShowTitle" runat="server" 
                Text="<%$ Resources:labels, publishtitle %>" Checked="True" />
        </td>
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
        <td>
            
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>    
</table>
<div style="padding:5px 0px 5px 5px; text-align:center;">
    <hr />
    <asp:Image runat="server" ID="imgSave" AlternateText="" ImageUrl="~/widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%$ Resources:labels, save %>' OnClientClick="return widgetvalidate();" onclick="btnSave_Click" 
                 />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="LinkButton2" runat="server" Text='<%$ Resources:labels, exit %>' 
                CausesValidation="False" onclick="btnExit_Click1"  />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A2" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     
</div>
</asp:Panel>