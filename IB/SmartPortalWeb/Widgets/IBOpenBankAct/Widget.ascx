<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBOpenBankAct_Widget" %>
<script src="JS/mask.js" type="text/javascript"></script>
<script src="JS/docso.js" type="text/javascript"></script>

<style>
    .amount {
        width: 83% !important;
    }

    .countdown > span:first-child {
        font-weight: 500 !important;
    }
</style>
<asp:ScriptManager runat="server">
</asp:ScriptManager>
<div style="text-align: center; height: 8px">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="" runat="server">
        <ProgressTemplate>
            <div class="cssProgress">
                <div class="progress1">
                    <div class="cssProgress-bar cssProgress-active" data-percent="100" style="transition: none; width: 100%;"></div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="th">
    <span><%=Resources.labels.openbankaccount %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label runat="server" ID="lblError" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div>
            <asp:HiddenField runat="server" ID="hdFullName" />
            <asp:HiddenField runat="server" ID="hdfdreceipt" />
            <asp:HiddenField runat="server" ID="hdBalanceSender" />
            <asp:HiddenField runat="server" ID="hdcategoryName" />
            <asp:HiddenField runat="server" ID="hdBirthday" />
            <asp:HiddenField runat="server" ID="hdAddress" />
            <asp:HiddenField runat="server" ID="hdNRC" />
            <asp:HiddenField runat="server" ID="hdEmail" />
            <asp:HiddenField runat="server" ID="hdPhone" />
            <asp:HiddenField runat="server" ID="hdAccountType" />
            <asp:HiddenField runat="server" ID="hdAccountNO" />
            <asp:HiddenField runat="server" ID="HdCatCode" />
            <asp:HiddenField runat="server" ID="hdAmount" />
            <asp:HiddenField runat="server" ID="hdDesc" />
            <asp:HiddenField runat="server" ID="hdFullNameCredit" />
            <asp:HiddenField runat="server" ID="hdclosebalance" />
            <asp:HiddenField runat="server" ID="hdcloseccyid" />
        </div>
        <asp:Panel ID="pnContent" runat="server" class="divcontent">
            <asp:UpdatePanel ID="upTIB" runat="server">
                <ContentTemplate>
                    <div class="divcontent">
                        <div class="handle">
                            <span><%=Resources.labels.userinfo %></span>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <span><%=Resources.labels.fullname %></span>
                                </div>
                                <div class="col-xs-5 col-sm-6 line30">
                                    <asp:Label ID="lblAccountName" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <span><%=Resources.labels.ngaysinh %></span>
                                </div>
                                <div class="col-xs-5 col-sm-6 line30">
                                    <asp:Label ID="lblBirthday" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <span><%=Resources.labels.diachi %></span>
                                </div>
                                <div class="col-xs-5 col-sm-6 line30">
                                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <span><%=Resources.labels.NRICNumber %></span>
                                </div>
                                <div class="col-xs-5 col-sm-6 line30">
                                    <asp:Label ID="lblNRC" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <span><%=Resources.labels.email %></span>
                                </div>
                                <div class="col-xs-5 col-sm-6 line30">
                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <span><%=Resources.labels.phone %></span>
                                </div>
                                <div class="col-xs-5 col-sm-6 line30">
                                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="handle">
                            <span><%=Resources.labels.option9 %></span>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <span><%=Resources.labels.option9 %></span>
                                </div>
                                <div class="col-xs-5 col-sm-4">
                                    <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                        <asp:ListItem Text='<%$ Resources:labels, openfdacount %>' Value="O"></asp:ListItem>
                                        <asp:ListItem Text='<%$ Resources:labels, closefdacount %>' Value="C"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="pnClose" runat="server">
                            <div class="handle">
                                <span><%=Resources.labels.thongtintaikhoan %></span>
                            </div>
                            <div class="content_table">
                                <div class="row">
                                    <div class="col-xs-5 col-sm-4 right">
                                        <span><%=Resources.labels.account %>*</span>
                                    </div>
                                    <div class="col-xs-5 col-sm-4">
                                        <asp:DropDownList ID="ddlAccountNoClose" runat="server" OnSelectedIndexChanged="ddlAccountNoClose_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-5 col-sm-4 right">
                                        <span><%=Resources.labels.desc %> *</span>
                                    </div>
                                    <div class="col-xs-7 col-sm-4 line30">
                                        <asp:TextBox ID="txtDescClose" runat="server" Height="50px" TextMode="MultiLine" onkeyup="ValidateLimit(this,200);" onkeyDown="ValidateLimit(this,200);" onpaste="ValidateLimit(this,200);"></asp:TextBox><br />
                                        <asp:Label ID="Label1" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666"
                                            Text='<%$ Resources:labels, ghichunoidung %>'></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="button-group">
                                <asp:Button ID="btnContineClose" runat="server" CssClass="btn btn-primary" Text='<%$ Resources:labels, tieptuc %>' OnClientClick="return validate1();" OnClick="btnContineClose_Click" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="OpenAccount" runat="server" Visible="false">
                            <div class="handle">
                                <span><%=Resources.labels.thongtintaikhoan %></span>
                            </div>
                            <div class="content_table">
                                <div class="row">
                                    <div class="col-xs-5 col-sm-4 right">
                                        <span><%=Resources.labels.loaitaikhoan %></span>
                                    </div>
                                    <div class="col-xs-5 col-sm-4">
                                        <asp:DropDownList ID="ddlActType" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-5 col-sm-4 right">
                                        <span><%=Resources.labels.debitacc2 %> *</span>
                                    </div>
                                    <div class="col-xs-5 col-sm-4">
                                        <asp:DropDownList ID="ddlDebitAccount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-5 col-sm-4 right">
                                        <asp:Label ID="Label28" runat="server" Text="<%$ Resources:labels, availablebalance %>"></asp:Label>
                                    </div>
                                    <div class="col-xs-7 col-md-4 col-sm-6 line30">
                                        <asp:Label ID="lblAvailableBal" runat="server"></asp:Label>
                                        <asp:Label ID="lblAvailableBalCCYID" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-5 col-sm-4 right">
                                        <span><%=Resources.labels.term %></span>
                                    </div>
                                    <div class="col-xs-5 col-sm-4">
                                        <asp:DropDownList ID="ddlTerm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLoadCategory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-5 col-sm-4 right">
                                        <span><%=Resources.labels.category %></span>
                                    </div>
                                    <div class="col-xs-5 col-sm-4">
                                        <asp:DropDownList ID="ddlCatcode" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-5 col-sm-4 right">
                                        <span><%=Resources.labels.amount %> *</span>
                                    </div>
                                    <div class="col-xs-5 col-sm-4">
                                        <asp:TextBox ID="txtAmount" autocomplete="off" runat="server" Text="" MaxLength="15" CssClass="amount"></asp:TextBox>
                                    </div>
                                    <div>
                                        <asp:Label ID="lblText" runat="server" Font-Size="7pt" Font-Bold="True" Font-Italic="True"
                                            ForeColor="#0066FF" Style="padding-top: 2px; line-height: 23px !important;"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-5 col-sm-4 right">
                                        <span><%=Resources.labels.desc %> *</span>
                                    </div>
                                    <div class="col-xs-7 col-sm-4 line30">
                                        <asp:TextBox ID="txtDesc" runat="server" Height="50px" TextMode="MultiLine" onkeyup="ValidateLimit(this,200);" onkeyDown="ValidateLimit(this,200);" onpaste="ValidateLimit(this,200);"></asp:TextBox><br />
                                        <asp:Label ID="lblNote" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666"
                                            Text='<%$ Resources:labels, ghichunoidung %>'></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-5 col-sm-4 right">
                                        <span>
                                            <asp:Label Text="*" runat="server"></asp:Label></span>
                                    </div>
                                    <div class="col-xs-5 col-sm-4 right">
                                        <asp:CheckBox ID="cbPolicy" runat="server"></asp:CheckBox>
                                        <span><%=Resources.labels.termsandconditionsofpsvb %></span>
                                        <asp:LinkButton runat="server" Text='<%$ Resources:labels, termsandconditions %>' ID="LbOpoen" OnClientClick="javascript:return poponload1()"></asp:LinkButton>
                                        <span><%=Resources.labels.ofpsvb %></span>
                                    </div>
                                    <div class="col-xs-12 col-sm-12 right">
                                    </div>
                                </div>
                            </div>
                            <div class="button-group">
                                <asp:Button ID="btnContine" runat="server" CssClass="btn btn-primary" Text='<%$ Resources:labels, tieptuc %>' OnClientClick="return validate();" OnClick="btnContinue_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Panel ID="pnComfirm" runat="server" class="divcontent" Visible="false">
            <div class="divcontent">
                <div class="handle">
                    <span><%=Resources.labels.userinfo %></span>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <span><%=Resources.labels.fullname %></span>
                        </div>
                        <div class="col-xs-5 col-sm-6 line30">
                            <asp:Label ID="lblAccountNameCfm" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <span><%=Resources.labels.ngaysinh %></span>
                        </div>
                        <div class="col-xs-5 col-sm-6 line30">
                            <asp:Label ID="lblBirthdayCfm" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <span><%=Resources.labels.diachi %></span>
                        </div>
                        <div class="col-xs-5 col-sm-6 line30">
                            <asp:Label ID="lblAddressCfm" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <span><%=Resources.labels.NRICNumber %></span>
                        </div>
                        <div class="col-xs-5 col-sm-6 line30">
                            <asp:Label ID="lblNRCCfm" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <span><%=Resources.labels.email %></span>
                        </div>
                        <div class="col-xs-5 col-sm-6 line30">
                            <asp:Label ID="lblEmailCfm" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <span><%=Resources.labels.phone %></span>
                        </div>
                        <div class="col-xs-5 col-sm-6 line30">
                            <asp:Label ID="lblPhoneCfm" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="handle">
                        <span><%=Resources.labels.thongtintaikhoan %></span>
                    </div>
                    <div class="content_table">
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.loaitaikhoan %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblActTypeCfm" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.debitacc2 %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblDebitAccountCfm" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.availablebalance %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblAvailableBalCfm" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.category %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblCatcodeCfm" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.amount %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblAmountfm" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.desc %></span>
                            </div>
                            <div class="col-xs-7 col-sm-6 line30">
                                <asp:Label ID="lbldescCfm" runat="server" Height="50px" TextMode="MultiLine"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="button-group">
                    <asp:Button ID="btnBakCfm" runat="server" OnClick="btnBackCfm_Click" CssClass="btn btn-warning" Text='<%$ Resources:labels, quaylai %>' />

                    <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" CssClass="btn btn-primary" Text='<%$ Resources:labels, xacnhan %>' />

                    <div class="clearfix"></div>
                </div>
        </asp:Panel>
        <asp:Panel ID="PnComfirmClose" runat="server" class="divcontent" Visible="false">
            <div class="divcontent">
                <div class="handle">
                    <span><%=Resources.labels.userinfo %></span>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <span><%=Resources.labels.fullname %></span>
                        </div>
                        <div class="col-xs-5 col-sm-6 line30">
                            <asp:Label ID="lblAccountNameCfmCL" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <span><%=Resources.labels.ngaysinh %></span>
                        </div>
                        <div class="col-xs-5 col-sm-6 line30">
                            <asp:Label ID="lblBirthdayCfmCL" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <span><%=Resources.labels.diachi %></span>
                        </div>
                        <div class="col-xs-5 col-sm-6 line30">
                            <asp:Label ID="lblAddressCfmCL" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <span><%=Resources.labels.NRICNumber %></span>
                        </div>
                        <div class="col-xs-5 col-sm-6 line30">
                            <asp:Label ID="lblNRCCfmCL" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <span><%=Resources.labels.email %></span>
                        </div>
                        <div class="col-xs-5 col-sm-6 line30">
                            <asp:Label ID="lblEmailCfmCL" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <span><%=Resources.labels.phone %></span>
                        </div>
                        <div class="col-xs-5 col-sm-6 line30">
                            <asp:Label ID="lblPhoneCfmCL" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="handle">
                        <span><%=Resources.labels.thongtintaikhoan %></span>
                    </div>
                    <div class="content_table">
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.loaitaikhoan %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblActTypeCfmCL" Text="Fixed Deposit Account" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.debitacc2 %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblDebitAccountCfmCL" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.taikhoanbaoco %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblCreditAccountCfmCL" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.thepenaltyamountforearlywithdrawal %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblprewithdrawcharge" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.theamounthasbeenrefunded %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblinterestrevert" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.amountofprofitreceived %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblinterestpayable" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.totalamountreceivedwhenclosing %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblTotalCfmCL" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.desc %></span>
                            </div>
                            <div class="col-xs-7 col-sm-6 line30">
                                <asp:Label ID="lbldescCfmCL" runat="server" Height="50px" TextMode="MultiLine"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="button-group">
                    <asp:Button ID="btnBackClose" runat="server" OnClick="btnBackCfmClose_Click" CssClass="btn btn-warning" Text='<%$ Resources:labels, quaylai %>' />

                    <asp:Button ID="btnConfirmClose" runat="server" OnClick="btnConfirmClose_Click" CssClass="btn btn-primary" Text='<%$ Resources:labels, xacnhan %>' />

                    <div class="clearfix"></div>
                </div>
        </asp:Panel>
        <asp:Panel ID="pnOTP" runat="server" class="divcontent" Visible="false">
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
                            <asp:Button ID="btnSendOTP" runat="server" CssClass="btn btn-primary btnSendOTP " OnClick="btnSendOTP_Click" Text="<%$ Resources:labels, resend %>" />
                            <div class="countdown hidden">
                                <span style="font-weight: normal;"><%=Resources.labels.OTPcodeexpiresin %></span>&nbsp;<span class="countdown_time"></span>
                            </div>
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
                <asp:Button ID="btnBackConfirm" runat="server" CssClass="btn btn-warning" OnClick="btnBackOTP_Click" Text="<%$ Resources:labels, quaylai %>" />
                <asp:Button ID="btnAction" runat="server" CssClass="btn btn-primary" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="btnAction_Click" Text="<%$ Resources:labels, thuchien %>" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnResult" runat="server" class="divcontent" Visible="false">
            <div class="handle">
                <span><%=Resources.labels.userinfo %></span>
            </div>
            <div class="content_table">
                <div class="row" id="TranNoResult" runat="server">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.sogiaodich %></span>
                    </div>
                    <div class="col-xs-5 col-sm-6 line30">
                        <asp:Label ID="lblTranID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row" id="TimeResult" runat="server">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.thoidiem %></span>
                    </div>
                    <div class="col-xs-5 col-sm-6 line30">
                        <asp:Label ID="lblEndDateTime" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.fullname %></span>
                    </div>
                    <div class="col-xs-5 col-sm-6 line30">
                        <asp:Label ID="lblAccountNameRS" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.ngaysinh %></span>
                    </div>
                    <div class="col-xs-5 col-sm-6 line30">
                        <asp:Label ID="lblBirthdayRS" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.diachi %></span>
                    </div>
                    <div class="col-xs-5 col-sm-6 line30">
                        <asp:Label ID="lblAddressRS" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.NRICNumber %></span>
                    </div>
                    <div class="col-xs-5 col-sm-6 line30">
                        <asp:Label ID="lblNRCRS" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.email %></span>
                    </div>
                    <div class="col-xs-5 col-sm-6 line30">
                        <asp:Label ID="lblEmailRS" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.phone %></span>
                    </div>
                    <div class="col-xs-5 col-sm-6 line30">
                        <asp:Label ID="lblPhoneRS" runat="server"></asp:Label>
                    </div>
                </div>
                <asp:Panel ID="rsOpen" runat="server" Visible="false">
                    <div class="handle">
                        <span><%=Resources.labels.thongtintaikhoan %></span>
                    </div>
                    <div class="content_table">
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.loaitaikhoan %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblActTypeRS" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.debitacc2 %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblDebitAccountRS" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.availablebalance %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblAvailableBalResult" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.category %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblCatcodeRS" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.amount %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblAmountRS" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span>Account No Open</span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblAccountNoOpen" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.desc %></span>
                            </div>
                            <div class="col-xs-7 col-sm-6 line30">
                                <asp:Label ID="lbldescRS" runat="server" Height="50px" TextMode="MultiLine"></asp:Label>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="rsClose" runat="server" Visible="false">
                    <div class="handle">
                        <span><%=Resources.labels.thongtintaikhoan %></span>
                    </div>
                    <div class="content_table">
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.debitacc2 %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblDebitConfirmClose" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.taikhoanbaoco %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblrsCreditAcount" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.amount %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblrsCreditBalance" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 right">
                                <span><%=Resources.labels.currency %></span>
                            </div>
                            <div class="col-xs-5 col-sm-6 line30">
                                <asp:Label ID="lblrsCurency" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="button-group">
                    <asp:Button ID="btnPrint" runat="server" OnClientClick="javascript:return poponload()"
                        Text="<%$ Resources:labels, inketqua %>" OnClick="btnPrint_Click" CssClass="btn btn-warning" />
                    &nbsp;
                    <asp:Button ID="btnBackRs" runat="server" CssClass="btn btn-warning" OnClick="btnBackRs_Click" Text="<%$ Resources:labels, quaylai %>" />
                </div>
            </div>
        </asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    function validate() {
        if (!validateEmpty('<%=txtDesc.ClientID %>', '<%=Resources.labels.bancannhapnoidung %>')) {
            return false;
        }
        if (!validateMoney('<%=txtAmount.ClientID %>', '<%=Resources.labels.bancannhapsotien %>')) {
            document.getElementById('<%=txtAmount.ClientID %>').focus();
            return false;
        }
        return true;
    }
    function validate1() {
        if (!validateEmpty('<%=txtDescClose.ClientID %>', '<%=Resources.labels.bancannhapnoidung %>')) {
            return false;
        }
        return true;
    }
    function poponload() {
        testwindow = window.open("widgets/IBOpenBankAct/print.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
    function poponload1() {
        testwindow = window.open("widgets/IBOpenBankAct/viewprint.aspx", "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
    function poponloadview() {
        testwindow = window.open("widgets/IBOpenBankAct/viewprint.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
    function ntt(sNumber, idDisplay, event) {

        executeComma(sNumber, event);

        if (document.getElementById(sNumber).value == "") {
            document.getElementById(idDisplay).innerHTML = "";
            return;
        }

        document.getElementById(idDisplay).innerHTML = "(" + number2text(replaceAll(document.getElementById(sNumber).value, ",", ""), '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>') + ")";
    }
</script>
