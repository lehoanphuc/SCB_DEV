<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_Setting_Widget" %>
<link href="Widgets/Setting/Controls/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<div style="padding:5px 0px 5px 5px; text-align:center;">
    
    <img alt="" src="widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="LinkButton1" runat="server" 
                 Text="<%$ Resources:labels, save %>" onclick="btnSave_Click" />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
       <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                 Text="<%$ Resources:labels, exit %>" onclick="btnExit_Click" />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A1" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     <hr />
</div>

<div style=" text-align:left; padding:5px 1px 5px 1px; height:auto;">
<asp:Label ID="lblAlert" runat="server" Font-Bold="False" SkinID="lblImportant"></asp:Label>
</div>
<div style=" text-align:right; margin:5px 1px 5px 1px; padding-right:5px;">
<asp:Label ID="Label7" runat="server" Font-Bold="False" SkinID="lblImportant">*</asp:Label>
<asp:Label ID="Label8" runat="server" SkinID="lblImportant" Text='<%$ Resources:labels, requiredfield %>'></asp:Label>
</div>

<asp:Panel runat="server" ID="pnMailSetup" GroupingText="<%$ Resources:labels, mailsetup %>">
<table id="pageadd" cellspacing="1" width="100%">
    <tr>
        <td width="30%">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label2" runat="server" 
                Text="<%$ Resources:labels, smtpserver %>"></asp:Label>
            :
        </td>
        <td>
            <asp:TextBox ID="txtSMTPServer" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, smtpport %>'></asp:Label>
            :
        </td>
        <td>
            <asp:TextBox ID="txtPort" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
            </td>
    </tr>
    <tr>
        <td class="tdleft">
            
            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, username %>"></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtUserName" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, password %>"></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtPassword" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            
            &nbsp;</td>
        <td>
        &nbsp;
            </td>
    </tr>
</table>
</asp:Panel>

<br />
<asp:Panel runat="server" ID="pnCommonSetup" GroupingText="<%$ Resources:labels, commonsetup %>" DefaultButton="btnSave">
<table id="Table1" cellspacing="1" width="100%">
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="tdleft" width="30%">
            <asp:Label ID="Label1" runat="server" 
                Text="<%$ Resources:labels, roleadmin %>"></asp:Label>
            :
        </td>
        <td>
            <asp:DropDownList ID="ddlRoleAdmin" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label3" runat="server" Text='<%$ Resources:labels, usernamedefault %>'></asp:Label>
            :
        </td>
        <td>
            <asp:DropDownList ID="ddlUserNameDefault" runat="server">
            </asp:DropDownList>
            </td>
    </tr>
    <tr>
        <td class="tdleft">
            
            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, portaldefault %>"></asp:Label>
            :</td>
        <td>
            <asp:DropDownList ID="ddlPortalDefault" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, pagedefault %>"></asp:Label>
            :</td>
        <td>
            <asp:DropDownList ID="ddlPageDefault" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            
            <asp:Label ID="Label11" runat="server" 
                Text="<%$ Resources:labels, languagedefault %>"></asp:Label>
            :</td>
        <td>
            <asp:DropDownList ID="ddlLanguageDefault" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label12" runat="server" 
                Text="<%$ Resources:labels, logpath %>"></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtLogPath" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
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
     <asp:LinkButton ID="btnSave" runat="server" 
                 Text="<%$ Resources:labels, save %>" onclick="btnSave_Click" />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
       <asp:LinkButton ID="btnExit" runat="server" CausesValidation="False" 
                 Text="<%$ Resources:labels, exit %>" onclick="btnExit_Click" />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A2" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     
</div>
</asp:Panel>