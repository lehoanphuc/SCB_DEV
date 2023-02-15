<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContractDelete_Widget" %>

<link href="widgets/SEMSUser/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSUser/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script src="widgets/SEMSUser/JS/ajax.js" type="text/javascript"></script>
<script src="widgets/SEMSUser/JS/tab-view.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/lang/en.js" type="text/javascript"></script>
<link href="widgets/IBTransactionHistory1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="widgets/SEMSUser/js/common.js"> </script>
<script type="text/javascript" src="widgets/SEMSUser/js/subModal.js"> </script>
<script type="text/javascript" src="widgets/SEMSUser/js/commonjs.js"> </script>
<link href="widgets/SEMSUser/css/style.css" rel="stylesheet" type="text/css">
<link href="widgets/SEMSUser/css/subModal.css" rel="stylesheet" type="text/css">
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSContractList/Images/handshake.png" style="width: 40px; height: 32px; margin-bottom: 10px;" align="middle" />
    <asp:Label ID="lbltitle" runat="server" Text="<%$ Resources:labels, donghopdong %>"></asp:Label>
</div>
<asp:Panel runat="server" ID="pnRole">
    <div class="divGetInfoCust">
        <div class="divHeaderStyle">
            <%=Resources.labels.confirm %>
        </div>
        <table class="style1" cellspacing="0" cellpadding="5">
            <tr>
                <td align="center" class="style2">
                    <asp:Label ID="lblquestion" runat="server"
                        Text="<%$ Resources:labels, banchacchanmuondonghopdongkhong %>"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<asp:Panel ID="pnResult" Visible="false" runat="server">
    <div class="divGetInfoCust">
        <div class="divHeaderStyle">
            <%=Resources.labels.ketquagiaodich %>
        </div>
        <div class="content">
            <div style="padding-top: 10px; padding-bottom: 10px; text-align: center;">
                <asp:Label runat="server" ID="lblError" ForeColor="Red" Font-Bold="true"></asp:Label>
            </div>
        </div>
    </div>
</asp:Panel>
<div style="text-align: center; padding-top: 10px;">
    &nbsp;<asp:Button ID="btsaveandcont" runat="server" OnClick="btsaveandcont_Click" Text="<%$ Resources:labels, close %>" />
    &nbsp;<asp:Button ID="btback" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
</div>

