<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SMSNOTIFYCATIONFEE_Controls_Widget" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<link href="widgets/SEMSCustomerList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>
<script src="widgets/SEMSCustomerList/JS/tab-view.js" type="text/javascript"></script>
<script src="widgets/SEMSProduct/JS/common.js" type="text/javascript"></script>
<link href="widgets/SEMSProduct/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/css/style.css" rel="stylesheet" type="text/css">
<link href="widgets/SEMSCustomerList/css/subModal.css" rel="stylesheet" type="text/css">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <contenttemplate>
<br />
<div id="divCustHeader">
    <asp:Image ID="imgLoGo" runat="server" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" /> 
    <asp:Label ID="lblTitleBranch" runat="server"></asp:Label>
</div>
<div id="divError">
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
<ProgressTemplate>
<img alt="" src="widgets/SEMSProduct/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
</ProgressTemplate>
</asp:UpdateProgress>
<asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
</div>

<div class="divGetInfoCust">
    <asp:Panel ID="pnAdd" runat="server" Height="300px">  
<div class="divHeaderStyle">
       <%=Resources.labels.chitietsmsnotifyfee%>
    </div>
    <table class="style1" cellpadding="3">
        <tr> 
            <td class="auto-style1" >
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, role %>"></asp:Label> *
            </td>
            <td class="auto-style2">
                <asp:DropDownList ID="ddlRole" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged"></asp:DropDownList>
               
            </td>
            <td class="auto-style1">
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, loaiphi %>"></asp:Label> *
            </td>
            <td class="auto-style2">
                <asp:DropDownList ID="ddlFeetype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFeetype_SelectedIndexChanged"></asp:DropDownList>
                
            </td>
        </tr>
        <tr>
            <td style="vertical-align:top;padding-top: 7px;">
                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, danhsachquyen %>"></asp:Label>
            </td>
            <td rowspan="10" valign="top"  style="line-height: 200%;" >
                <asp:Label ID="lbrightlist" runat="server" Text="Label"></asp:Label>
                
            </td>
           
              <td>
                <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, maphi %>"></asp:Label>
              </td>
            <td>
                <asp:Label ID="lbmaphi" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
             <td>
                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, tenphi %>"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbtenphi" runat="server" Text=""></asp:Label>
            </td>
            </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, amount %>"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbamount" runat="server" Text=""></asp:Label>&nbsp;
                <asp:Label ID="lblccyid" runat="server" Text=""></asp:Label>
            </td>
        </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="Label3" Visible="false" runat="server" Text="<%$ Resources:labels, desc %>"></asp:Label>
                </td>
                <td>
                  <asp:Label ID="lbdesc" Visible="false" runat="server" Text=""></asp:Label>
                </td>
              </tr>
            <tr><td></td><td></td><td>&nbsp;</td></tr>
            <tr><td></td><td></td><td>&nbsp;</td></tr>
            <tr><td></td><td></td><td>&nbsp;</td></tr>
            <tr><td></td><td></td><td>&nbsp;</td></tr>
            <tr><td></td><td></td><td>&nbsp;</td></tr>
            <tr><td></td><td></td><td>&nbsp;</td></tr>
    </table>  	      
</asp:Panel>
   </div>   
</contenttemplate>

</asp:UpdatePanel>
<div style="text-align: center; margin-top: 10px;">
    &nbsp;<asp:Button ID="btsave" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate();" OnClick="btsave_Click" />
    &nbsp;<asp:Button ID="btback" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
</div>
