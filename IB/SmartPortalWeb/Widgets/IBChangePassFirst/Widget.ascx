<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBChangePassFirst_Widget" %>
<style>
    #sidepanel {
        background-color: transparent;
    }

    #divcontent {
        width: 100%;
    }

    #mobile_button {
        display: none;
    }
</style>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="al">
            <span><%=Resources.labels.thaydoimatkhau %></span><br />
            <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
        </div>

        <div id="divError">
            <asp:Label runat="server" ID="lblError" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <asp:Panel ID="pnOTP" runat="server" CssClass="divcontent">
            <div class="handle">
                <span><%=Resources.labels.xacthucgiaodich %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <label class="bold"><%=Resources.labels.loaixacthuc %></label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:DropDownList ID="ddlLoaiXacThuc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLoaiXacThuc_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-5 col-sm-4 left">
                        <asp:Panel ID="pnSendOTP" runat="server">
                            <asp:Button ID="btnSendOTP" runat="server" CssClass="btn btn-primary btnSendOTP " OnClick="btnSendOTP_Click" Text="Send OTP" />
                            <asp:Panel ID="pnCountDownOTP" class="countdown hidden" runat="server">
                                <span style="font-weight: normal;"><%=Resources.labels.OTPcodeexpiresin %></span>&nbsp;<span class="countdown_time"></span>
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <label class="bold"><%=Resources.labels.maxacthuc %></label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtOTP" runat="server" Width="100%" AutoCompleteType="None"></asp:TextBox>
                    </div>
                </div>
                <div class="row" style="text-align:center; color:#7A58BF">
                    <asp:Label ID="lbnotice" runat="server" class="bold" Visible="false">If you do not see the email in your inbox, please check your “junk mail” folder or “spam” folder.</asp:Label>
                </div>
            </div>
            <div class="button-group">
                <asp:Button ID="btnAction" runat="server" CssClass="btn btn-primary" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="btnAction_Click" Text="<%$ Resources:labels, thuchien %>" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnFocus" DefaultButton="btnChange" runat="server">
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
        </asp:Panel>
        <asp:Panel ID="pnResult" runat="server">
            <figure>
                <div class="content display-label">
                    <div style="text-align: center">
                        <asp:Label ID="lblResult" runat="server" ForeColor="Red" Text="<%$ Resources:labels, thaydoimatkhauthanhcong %>" Font-Bold="True"></asp:Label>
                    </div>
                </div>
                <div class="button-group">
                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, thoat %>" OnClick="btnLogin_Click" />
                </div>
            </figure>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
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
    function validate2() {
        if (!validateEmpty('<%=txtOTP.ClientID %>', '<%=Resources.labels.nhapvaomaxacthuc %>')) {
            return false;
        }
        return true;
    }
</script>
