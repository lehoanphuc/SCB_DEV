<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSPoster_Delete_Widget" %>
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSProduct/Images/product.png" style="width: 30px; height: 32px;
        margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.deleteposter %>
</div>
<!-- Thong tin nguoi dai dien-->
<asp:Panel runat="server" ID="pnRole">
    <div class="divGetInfoCust">
        <div class="divHeaderStyle">
            <%=Resources.labels.thongtinxacnhan %>
        </div>
        <table class="style1" cellspacing="0" cellpadding="5">
            <tr>
                <td align="center" class="style2">
                    <asp:Label ID="Label1" runat="server" Text="Are you sure delete poster"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<asp:Panel ID="pnResult" runat="server">
    <div class="divGetInfoCust">
        <div class="divHeaderStyle">
            <%=Resources.labels.ketquathuchien %>
        </div>
        <table class="style1" cellspacing="0" cellpadding="5">
            <tr>
                <td align="center" class="style3">
                    <asp:Label runat="server" ID="lblError" ForeColor="Red" Text="<%$ Resources:labels, xoasanphamthanhcong %>"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<div style="text-align: center; padding-top: 10px;">
    <asp:Button ID="btsaveandcont" runat="server" Text="<%$ Resources:labels, delete %>"
        Width="71px" OnClick="btsaveandcont_Click" />
    &nbsp;
    <asp:Button ID="btback" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
</div>
