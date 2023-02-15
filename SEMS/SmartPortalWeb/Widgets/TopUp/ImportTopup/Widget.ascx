<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_ImportTopUp_Widget" %>
<link href="Widgets/Topup/CSS/Default.css" rel="stylesheet" type="text/css" />
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSContractLimit_HIS/Images/log.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.topupimport%>
</div>

<div id="divError">
    <asp:Label ID="lblAlert" runat="server"></asp:Label>
</div>

<div id="divSearch">
    <asp:Panel ID="pnFocus" runat="server">
        <table class="style1">
            <tr id="TelcoList">
                <td class="right width50">
                    <asp:Label ID="lblTelco" runat="server" Text='<%$ Resources:labels, telco %>'></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlTelco" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTelco_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="right width50">
                    <asp:Label ID="lblAmount" runat="server" Text='<%$ Resources:labels, amount %>'></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlAmount" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="right width50">
                    <asp:Label ID="Label2" runat="server" Text='<%$ Resources:labels, buyrate %>'></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBuyRate" runat="server"></asp:TextBox>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="right width50">
                    <asp:Label ID="Label3" runat="server" Text='<%$ Resources:labels, sellrate %>'></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSellRate" runat="server"></asp:TextBox>
                    <td>&nbsp;</td>
                </td>
            </tr>
            <tr>
                <td class="right width50">
                    <asp:Label ID="lblPassword0" runat="server"
                        Text="<%$ Resources:labels, chonfileimport %>"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
                <td>
                    <asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server"
                        ErrorMessage="Only rar or zip files are allowed!"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.rar|.RAR|.zip|.ZIP)$"
                        ControlToValidate="FileUpload1"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="right width50">
                    <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, matkhau %>'></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ipPassword" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
<div style="text-align: center; margin: 10px;">
    &nbsp;<asp:Button ID="btsave" runat="server" Text="<%$ Resources:labels, save %>" OnClick="btsave_Click" OnClientClick="return validate();" />
    &nbsp;<asp:Button ID="btback" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
    &nbsp;<asp:Button ID="btDownload" runat="server" Text="<%$ Resources:labels, taibaocaotopup %>" OnClick="btDownload_Click" Style="width: 130px;" />
</div>
