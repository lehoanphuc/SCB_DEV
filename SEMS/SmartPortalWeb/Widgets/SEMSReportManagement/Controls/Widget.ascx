<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSReportManagement_Controls_Widget" %>
<link href="widgets/SEMSCustomerList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>
<script src="widgets/SEMSProductLimit/JS/mask.js" type="text/javascript"></script>
<script src="widgets/SEMSTellerApproveTrans/JS/common.js" type="text/javascript"></script>
<script src="widgets/SEMSCustomerList/JS/tab-view.js" type="text/javascript"></script>
<script src="widgets/SEMSProduct/JS/common.js" type="text/javascript"></script>
<link href="widgets/SEMSProduct/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/css/style.css" rel="stylesheet" type="text/css">
<link href="widgets/SEMSCustomerList/css/subModal.css" rel="stylesheet" type="text/css">
<br />
<div id="divProductHeader">
    <asp:Image ID="imgLoGo" runat="server" Style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <asp:Label ID="lblTitleProduct" runat="server"></asp:Label>
</div>
<div id="divError">
    <asp:Label runat="server" ID="lblWarning" ForeColor="Red"></asp:Label>
</div>
<div class="divAddInfoPro">
    <asp:Panel ID="pnResult" runat="server">
        <div class="divHeaderQuyen">
            <%=Resources.labels.ketquathuchien %>
        </div>
        <div style="text-align: center; font-weight: bold">
            <br />
            <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
            <br />
            <br />
        </div>

    </asp:Panel>
    <asp:Panel ID="pnFee" runat="server">
        <div class="divHeaderStyle">
            <%=Resources.labels.thongtinbaocao %>
        </div>
        <table class="style1" cellpadding="3">
            <tr>
                <td width="25%">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, tenbaocao %>"></asp:Label>
                    <asp:Label ID="Label5" runat="server" Text=" *"></asp:Label>
                </td>
                <td width="25%">
                    <asp:TextBox ID="txtReportName" runat="server"></asp:TextBox>
                </td>
                <td width="25%">
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, phanhe %>"></asp:Label>
                </td>
                <td width="25%">
                    <asp:DropDownList ID="ddlSubSystem" runat="server" Width="57%">
                        <asp:ListItem Value="SEMS">SEMS</asp:ListItem>
                        <asp:ListItem Value="IB" Text="<%$ Resources:labels, internetbanking %>"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, trangthamso %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlParameterPage" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, trangxembaocao %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlViewReportPage" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="cbIsDisPlay" runat="server" Text="<%$ Resources:labels, hienthi %>" />
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, filebaocao %>"></asp:Label>

                </td>
                <td>
                    <asp:FileUpload ID="FileUploadRPT" runat="server" />
                </td>
                <td colspan="2">
                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, justonlyuploadfileextensionrpt %>"></asp:Label>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:labels, thamsobaocao %>"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtparamreport" runat="server" Text=""></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:labels, theparameterseparatebysharp %>"></asp:Label>
                </td>
                <td></td>
            </tr>
            <asp:Label ID="lbtemp" runat="server" Visible="false"></asp:Label>
        </table>
    </asp:Panel>
</div>
<div style="text-align: center; margin-top: 10px;">
    &nbsp;                
    <asp:Button ID="btsave" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate();" OnClick="btsave_Click" />
    &nbsp;
    <asp:Button ID="btback" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
</div>
<script language="javascript">
    function validate() {
        if (validateEmpty('<%=txtReportName.ClientID %>', '<%=Resources.labels.tenbaocaokhongrong %>')) {
        }
        else {
            document.getElementById('<%=txtReportName.ClientID %>').focus();
            return false;
        }
    }
</script>





