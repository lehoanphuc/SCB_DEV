<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_Widget_Translate_Widget" %>
<link href="Widgets/Widget/Translate/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />


<div style="padding:5px 0px 5px 5px; text-align:center;">
    
    <img alt="" src="widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%$ Resources:labels, save %>' onclick="btnSave_Click" 
                 />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="LinkButton2" runat="server" Text='<%$ Resources:labels, exit %>' 
                CausesValidation="False" onclick="btnExit_Click1"  />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A1" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     <hr />
</div>

<div style=" text-align:left; padding:5px 1px 5px 1px; height:auto;">
<asp:Label ID="lblAlert" runat="server" Font-Bold="False" SkinID="lblImportant"></asp:Label>
    <br />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  
                ControlToValidate="txtTitle" 
        ErrorMessage='<%$ Resources:labels, widgettitlerequire %>' SetFocusOnError="True">
</asp:RequiredFieldValidator>

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
            <asp:Label ID="Label10" runat="server" Text='<%$ Resources:labels, language %>'></asp:Label>
            :</td>
        <td>
            <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlLanguage_SelectedIndexChanged">
            </asp:DropDownList>
            </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label9" runat="server" SkinID="lblImportant" Text="*"></asp:Label>
            &nbsp;<asp:Label ID="Label4" runat="server" SkinID="lblImportant" 
                Text="<%$ Resources:labels, title %>"></asp:Label>
            :
        </td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
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
                CausesValidation="False" onclick="btnExit_Click1"  />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A2" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     
</div>
</asp:Panel>