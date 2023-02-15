<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_ChangePass_Widget" %>
<div class="al">
    <span><%=Resources.labels.thaydoimatkhau %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<div id="divError">
    <asp:Label runat="server" ID="lblError" Font-Bold="true" ForeColor="Red"></asp:Label>
</div>
<asp:Panel ID="pnFocus" runat="server" class="divcontent">
    <figure>
        <legend class="handle"><%=Resources.labels.thongtinmatkhau %></legend>
        <div class="content display-label">
            <div class="row">
                <div class="col-xs-6 col-md-4 right">
                    <%=Resources.labels.oldpass %>&nbsp;*
                </div>
                <div class="col-xs-6 col-md-4">
                    <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" AutoCompleteType="None"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 col-md-4 right">
                    <%=Resources.labels.newpass %>&nbsp;*
                </div>
                <div class="col-xs-6 col-md-4">
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" AutoCompleteType="None"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 col-md-4 right">
                    <%=Resources.labels.repassword %>&nbsp;*
                </div>
                <div class="col-xs-6 col-md-4">
                    <asp:TextBox ID="txtRePassword" runat="server" TextMode="Password" AutoCompleteType="None"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="button-group">
            <asp:Button ID="btnReset" CssClass="btn btn-warning" runat="server" OnClick="btnReset_Click" Text="<%$ Resources:labels, lamlai %>" />
            <asp:Button ID="btnChange" CssClass="btn btn-primary" runat="server" OnClick="btnChange_Click" OnClientClick="return validate();" Text="<%$ Resources:labels, thaydoi %>" />
        </div>
    </figure>
</asp:Panel>

<asp:Panel ID="pnResult" runat="server">
    <figure>
        <div class="content display-label">
            <div style="text-align: center">
                <asp:Label ID="lblResult" runat="server" ForeColor="Red" Text="<%$ Resources:labels, thaydoimatkhauthanhcong %>" Font-Bold="True"></asp:Label>
            </div>
        </div>
        <div class="button-group">
            <asp:Button ID="Button3" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, thoat %>" OnClick="Button3_Click" />
        </div>
    </figure>
</asp:Panel>
<script>
    function validate() {
        document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        if (!validateEmpty('<%=txtOldPassword.ClientID %>', '<%=Resources.labels.vuilongnhapmatkhaucu %>')) {
            return false;
        }
        if (!validateEmpty('<%=txtNewPassword.ClientID %>', '<%=Resources.labels.vuilongnhapmatkhaumoi %>')) {
            return false;
        }
        if (!validateEmpty('<%=txtRePassword.ClientID %>', '<%=Resources.labels.vuilongnhapmatkhauxacnhan %>')) {
            return false;
        }
        if (!validateSameValue('<%=txtNewPassword.ClientID%>', '<%=txtRePassword.ClientID%>', '<%=Resources.labels.passwordcompare%>')) {
            return false;
        }
        return true;
    }
</script>
