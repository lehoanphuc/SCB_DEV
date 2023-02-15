<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSUserLevel_Delete_Widgets" %>
<script type="text/javascript" src="widgets/SEMSUser/js/common.js"> </script>
<script type="text/javascript" src="widgets/SEMSUser/js/subModal.js"> </script>
<script type="text/javascript" src="widgets/SEMSUser/js/commonjs.js"> </script>
<link href="widgets/SEMSUser/css/style.css" rel="stylesheet" type="text/css">
<link href="widgets/SEMSUser/css/subModal.css" rel="stylesheet" type="text/css">
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSUserLevel/Images/level.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.xoacapbac %>
</div>
<div id="divError">
    <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
</div>
<asp:Panel runat="server" ID="pnRole">
    <div class="divGetInfoCust">
        <div class="divHeaderStyle">
            <%=Resources.labels.thongtinxacnhan %>
        </div>
        <table class="style1" cellspacing="0" cellpadding="5">
            <tr>
                <td align="center" class="style2">
                    <asp:Label ID="lblConfirm" runat="server"
                        Text="<%$ Resources:labels, banchacchanmuonxoacapbackhong %>"></asp:Label>
                </td>

            </tr>
        </table>
    </div>
</asp:Panel>
<div style="text-align: center; padding-top: 10px;">
    <asp:Button ID="btsaveandcont" runat="server" OnClick="btsaveandcont_Click"
        Text="<%$ Resources:labels, xoa %>" Width="71px" />
    &nbsp;
    <asp:Button ID="btback" runat="server"
        Text="<%$ Resources:labels, quaylai %>" OnClick="btback_Click" />
    &nbsp; &nbsp;
</div>
