<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSBankUser_Delete_Widget" %>

<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSCustomerList/Images/messenger.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.deleteuser %>
</div>
<div id="divError"></div>
<asp:Panel runat="server" ID="pnRole">
    <div class="divGetInfoCust">
        <div class="divHeaderStyle">
            <%=Resources.labels.thongtinxacnhan %>
        </div>
        <table class="style1" cellspacing="0" cellpadding="5">
            <tr>
                <td align="center" class="style2">
                    <asp:Label runat="server" ID="lblDelete" Text='<%$ Resources:labels, areyousuredeletethisrecord %>'>
                    </asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<asp:Panel ID="pnResult" Visible="false" runat="server">
    <div class="divGetInfoCust">
        <div class="divHeaderStyle">
            <%=Resources.labels.ketqua %>
        </div>
        <div class="content">
            <div style="padding-top: 10px; padding-bottom: 10px; text-align: center;">
                <asp:Label runat="server" ID="lblError" ForeColor="Red" Font-Bold="true"></asp:Label>
            </div>
        </div>
    </div>
</asp:Panel>
<div style="text-align: center; padding-top: 10px;">
    &nbsp;
    <asp:Button ID="btnDelete" runat="server" Text='<%$ Resources:labels, delete %>' OnClick="btnDelete_Click" />
    &nbsp;
    <asp:Button ID="btnBack" runat="server" Text="<%$ Resources:labels, back %>" CssClass="btnGeneral" PostBackUrl="javascript:history.go(-1)" />
</div>
