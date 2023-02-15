<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSSetPermissionForAllContract_Widget" %>
<%@ Register Src="../../Controls/LetterSearch/LetterSearch.ascx" TagName="LetterSearch"
    TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<style type="text/css">
    .style1
    {
        width: 100%;
    }
    #divSearch
    {
        background-color: #F8F8F8;
        border: solid 1px #B9BFC1;
        margin: 5px 5px 5px 5px;
        padding: 5px 5px 5px 5px;
    }
  
    #divProductHeader
    {
        width: 100%;
        font-weight: bold;
        padding: 5px 5px 5px 5px;
    }
    #divError
    {
        width: 100%;
        height: 10px;
        text-align: center;
        padding: 0px 5px 5px 5px;
    }
</style>

<script src="widgets/SEMSSetPermissionForAllContract/JS/common.js" type="text/javascript"></script>
<link href="widgets/SEMSSetPermissionForAllContract/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<br />
<div>
<div id="divProductHeader">
    <img alt="" src="widgets/SEMSSetPermissionForAllContract/Images/tax.png" style="width: 32px; height: 32px;
        margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.setpermissionforallcontract %>
</div>
<%--<div id="divError">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0"
        runat="server">
        <ProgressTemplate>
            <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px;
                height: 16px;" />
            <%=Resources.labels.loading %></ProgressTemplate>
    </asp:UpdateProgress>
</div>--%>
<asp:UpdatePanel ID="pnSearch" runat="server" DefaultButton="btnOK">
<contenttemplate>
<asp:Label runat="server" ID="lblWarning" Font-Bold="true" ForeColor="Red" style="margin-left:45%"></asp:Label>
<div id="divSearch">

        <table class="style1">
            <tr>
                <td style="width:20%;"></td>
                <td><asp:Label runat="server" Text="<%$ Resources:labels, productid %>"></asp:Label></td>
                <td><asp:DropDownList runat="server" ID="ddlProduct" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Label runat="server" Text="<%$ Resources:labels, serviceid %>"></asp:Label></td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlService" OnSelectedIndexChanged="ddlService_SelectedIndexChanged" AutoPostBack="true"> 
                        <asp:ListItem Value="IB" Selected>Internet Banking</asp:ListItem>
                        <asp:ListItem Value="MB">Mobile Banking</asp:ListItem>
                        <asp:ListItem Value="SMS">SMS Banking</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="25%">
                    <asp:Button ID="btnOK" runat="server" Text="OK" OnClick="btnOK_Click" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Label runat="server" Text="<%$ Resources:labels, transaction %>"></asp:Label></td>
                <td><asp:DropDownList runat="server" ID="ddlTransaction" OnSelectedIndexChanged="ddlTransaction_SelectedIndexChanged"></asp:DropDownList></td>
                <td></td>
            </tr>
        </table>
        </contenttemplate>
    </asp:UpdatePanel>
</div>
</div>



