<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBQRMerchantHistory_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<style>
    .amount {
        width: 83% !important;
    }

</style>
<link href="CSS/css.css" rel="stylesheet" type="text/css" />
<script src="widgets/IBOutgoingTransfer/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBOutgoingTransfer/JS/lang/en.js" type="text/javascript"></script>
<script src="JS/mask_CR.js" type="text/javascript"></script>
<script src="JS/docso_CR.js" type="text/javascript"></script>
<script src="widgets/IBOutgoingTransfer/JS/common.js" type="text/javascript"></script>
<link href="widgets/IBOutgoingTransfer/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBOutgoingTransfer/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBOutgoingTransfer/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager runat="server">
</asp:ScriptManager>
<!--Transfer In Bank-->
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
    <span><%=Resources.labels.outgoingtransfer %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label runat="server" ID="lblTextError" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div>
            <asp:HiddenField runat="server" ID="hdsendeAccount" />
            <asp:HiddenField runat="server" ID="hdSenderName" />
            <asp:HiddenField runat="server" ID="hdReceiverName" />
            <asp:HiddenField runat="server" ID="hdSenderCCYID" />
            <asp:HiddenField runat="server" ID="hdBalanceSender" />
            <asp:HiddenField runat="server" ID="hdTypeID" />
            <asp:HiddenField runat="server" ID="hdTrancode" />
            <asp:HiddenField runat="server" ID="hdSenderBranch" />
            <asp:HiddenField runat="server" ID="hdtransferID" />
            <asp:HiddenField runat="server" ID="hdReceiverCCYID" />
            <asp:HiddenField runat="server" ID="hdFeeLapNet" />
            <asp:HiddenField runat="server" ID="hdTotalFee" />
        </div>
        <asp:Panel ID="pnTIB" runat="server" class="divcontent">
            <div class="handle">
                <asp:Label ID="Label7" runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
            </div>
            <div class="content_table">

                <div class="row">
                    <div class="col-xs-5 col-md-4 right">
                        <asp:Label ID="Label3" runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-4 line30">
                        <asp:DropDownList ID="ddlSenderAccount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:HiddenField ID="txtChu" runat="server" />
                    </div>
                </div>
                <div class="row hidden">
                    <div class="col-xs-5 col-md-4 right">
                        <asp:Label ID="Label28" runat="server" Text="<%$ Resources:labels, lasttransactiondate %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-4 line30">
                        <asp:Label ID="lblLastTranDate" runat="server"></asp:Label>
                        &nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-4 right">
                        <asp:Label ID="Label53" runat="server" Text="<%$ Resources:labels, availablebalance %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-4 line30">
                        <asp:Label ID="lblAvailableBal" runat="server"></asp:Label>
                        &nbsp;
                        <asp:Label ID="lblAvailableBalCCYID" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="handle">
                <asp:Label ID="Label15" runat="server" Text='<%$ Resources:labels, thongtinnguoinhantien %>'></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-md-4 right">
                        <asp:Label ID="Label5" runat="server" Text='<%$ Resources:labels, BankName %>'></asp:Label>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-md-4 line30">
                        <asp:DropDownList ID="ddlReceiverBank" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                        <asp:Label ID="lberror" runat="server" ForeColor="Red" Visible="False" Text='<%$ Resources:labels, receiverbankerror %>'></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-4 right">
                        <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, taikhoanthuhuong %>'></asp:Label>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-md-4 line30">
                        <asp:TextBox ID="txtReceiverAccount" OnTextChanged="txtReceiverAccount_TextChanged" AutoPostBack="true" runat="server" onkeypress="return isNumberKeyNumer(event)" placeholder="<%$ Resources:labels, notifileReceiveraccount %>"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-4 right">
                        <asp:Label ID="Label19" runat="server" Text='<%$ Resources:labels, interbankreceivername %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-4 line30">
                        <asp:TextBox ID="txtReceiverName" Enabled="false" runat="server" ></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-4 right"></div>
                    <div class="col-xs-7 col-md-4 line30">
                        <asp:Label ID="lblErrorReceiverAccount" Visible="False" ForeColor="Red" runat="server" Text='<%$ Resources:labels, errorcreditaccount %>'></asp:Label>
                    </div>
                </div>
            </div>

            <div class="handle">
                <asp:Label ID="Label17" runat="server" Text='<%$ Resources:labels, noidungchuyenkhoan %>'></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-md-4 right">
                        <asp:Label ID="Label2" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-md-4 line30">
                        <asp:TextBox ID="txtAmount" autocomplete="off" onkeypress="return isNumberKeyNumer(event)" runat="server" Text="" MaxLength="15" CssClass="amount"></asp:TextBox>
                        <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                        <div style="text-align: left">
                            <asp:Label ID="lblText" runat="server" Font-Size="7pt" Font-Bold="True" Font-Italic="True"
                                ForeColor="#0066FF" Style="padding-top: 2px; line-height: 23px !important;"></asp:Label>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.noidungthanhtoan %>*</span>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtDesc" runat="server" Height="50px" TextMode="MultiLine" onkeyDown="ValidateLimit(this,200);" onpaste="ValidateLimit(this,200);" onkeyup="this.value=this.value.replace(/[^a-zA-Z0-9]/g , '')"></asp:TextBox><br />
                        <asp:Label ID="Label1" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666"
                            Text='<%$ Resources:labels, ghichunoidung %>'></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="row form-group">
                        <div class="col-xs-3 col-sm-4 right">
                            <asp:Label ID="LblDocument" Visible="false" runat="server" Text=""><%= Resources.labels.document %></asp:Label>
                        </div>
                        <div class="col-xs-4 col-md-4 col-sm-6 line30">
                            <asp:FileUpload ID="FUDocument" AllowMultiple="false" Visible="false" runat="server" ClientIDMode="Static" accept=".pdf,.png,.jpg,.jpeg" />
                        </div>
                        <div class="col-xs-5 col-md-4 col-sm-6 line30">
                            <asp:Label ID="LblDocumentExpalainion" Visible="false" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666" Text='<%$ Resources:labels, uploadlimit1MB %>'></asp:Label>
                        </div>
                    </div>
                </div>
            </div>

            <div style="text-align: center; margin-top: 10px;">
                <asp:Button ID="btnTIBNext" runat="server" Text='<%$ Resources:labels, tieptuc %>'
                    OnClick="btnTIBNext_Click" OnClientClick="return validate();" class="btn btn-primary" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnConfirm" runat="server" class="divcontent" Visible="false">
            <div class="content">
                <div class="handle">
                    <asp:Label ID="Label20" runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, hotennguoitratien %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label8" runat="server" Text='<%$ Resources:labels, debitacc2 %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblSenderAccount" runat="server"></asp:Label>
                            <asp:Label ID="lblSenderBranch" runat="server" Visible="False"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label13" runat="server" Text='<%$ Resources:labels, sodutruockhighino %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblBalanceSender" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblSenderCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="handle">
                    <asp:Label ID="Label22" runat="server" Text='<%$ Resources:labels, thongtinnguoinhantien %>'></asp:Label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label39" runat="server" Text="<%$ Resources:labels, receiverbank1  %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblConfirmReceiverBankID" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label21" runat="server" Text="<%$ Resources:labels, interbankreceivername %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblReceiverName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, taikhoanbaoco %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblReceiverAccount" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label6" runat="server" Text="Receiver Currency"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblReceiverCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="handle">
                    <asp:Label ID="Label23" runat="server" Text='<%$ Resources:labels, noidungchuyenkhoan %>'></asp:Label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label12" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lbCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label48" runat="server" Text='<%$ Resources:labels, nguoitraphi %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblPhi" runat="server"></asp:Label>
                            <asp:Label ID="hdfSenderFee" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="hdfReceiverFee" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label45" runat="server" Text='<%$ Resources:labels, transactionfee %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblPhiAmount" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblFeeCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label16" runat="server" Text="<%$ Resources:labels, noidungthanhtoan %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblDesc" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="handle"></div>
                <%--<div class="content_table">
                    <div class="row">
                        <div class="col-xs-10 col-md-12">
                            <asp:Label ID="Label36" runat="server" Text='<%$ Resources:labels, noticeinterbank %>'></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-10 col-md-12">
                            <asp:Label ID="Label40" runat="server" Text='<%$ Resources:labels, noticeinterbank1 %>'></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-10 col-md-12">
                            <asp:Label ID="Label41" runat="server" Text='<%$ Resources:labels, noticeinterbank2 %>'></asp:Label>
                        </div>
                    </div>
                </div>--%>
            </div>
            <div style="text-align: center; margin-top: 10px;">
                <asp:Button ID="btnBackTransfer" runat="server" OnClick="btnBackTransfer_Click" Text='<%$ Resources:labels, quaylai %>' class="btn btn-warning" />
                <asp:Button ID="btnApply" runat="server" OnClick="btnApply_Click" Text='<%$ Resources:labels, xacnhan %>' class="btn btn-primary" />
            </div>
        </asp:Panel>
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
                        <asp:TextBox ID="txtOTP" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row" style="text-align: center; color: #7A58BF">
                    <asp:Label ID="lbnotice" runat="server" class="bold" Visible="false">If you do not see the email in your inbox, please check your “junk mail” folder or “spam” folder.</asp:Label>
                </div>
            </div>
            <div class="button-group">
                <asp:Button ID="btnBackConfirm" runat="server" CssClass="btn btn-warning" OnClick="btnBack_Click" Text="<%$ Resources:labels, quaylai %>" />
                <asp:Button ID="btnAction" runat="server" CssClass="btn btn-primary" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="btnAction_Click" Text="<%$ Resources:labels, thuchien %>" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnResultTransaction" runat="server" class="divcontent">
            <div class="content">
                <div class="handle">
                    <asp:Label ID="Label24" runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label14" runat="server" Text='<%$ Resources:labels, hotennguoitratien %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEndSenderName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label10" runat="server" Text='<%$ Resources:labels, debitacc2  %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEndSenderAccount" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label50" runat="server" Text='<%$ Resources:labels, sodusaukhighino %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEndBalanceSender" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblBalanceCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="handle">
                    <asp:Label ID="Label25" runat="server" Text='<%$ Resources:labels, thongtinnguoinhantien %>'></asp:Label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label38" runat="server" Text="<%$ Resources:labels, receiverbank1  %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEndReceiverBank" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label29" runat="server" Text='<%$ Resources:labels, hotennguoinhantien %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEndReceiverName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label27" runat="server" Text='<%$ Resources:labels, taikhoanbaoco %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEndReceiverAccount" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label9" runat="server" Text='Receiver Currency'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEndReceiverCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="handle">
                    <asp:Label ID="Label26" runat="server" Text='<%$ Resources:labels, noidungchuyenkhoan %>'></asp:Label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label30" runat="server" Text='<%$ Resources:labels, sogiaodich %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEndTransactionNo" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label32" runat="server" Text='<%$ Resources:labels, thoidiem %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEndDateTime" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label31" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEndAmount" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblAmountCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label51" runat="server" Text='<%$ Resources:labels, nguoitraphi %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEndPhi" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label54" runat="server" Text='<%$ Resources:labels, transactionfee %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEndPhiAmount" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblEndFeeCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label34" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEndDesc" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="handle"></div>
                <%--<div class="content_table">
                    <div class="row">
                        <div class="col-xs-10 col-md-12">
                            <asp:Label ID="Label19" runat="server" Text='<%$ Resources:labels, noticeinterbank %>'></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-10 col-md-12">
                            <asp:Label ID="Label33" runat="server" Text='<%$ Resources:labels, noticeinterbank1 %>'></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-10 col-md-12">
                            <asp:Label ID="Label35" runat="server" Text='<%$ Resources:labels, noticeinterbank2 %>'></asp:Label>
                        </div>
                    </div>
                </div>--%>
            </div>
            <!--Button next-->
            <div class="divbtn" style="text-align: center; margin-top: 10px;">
                <asp:Button ID="btnView" runat="server" OnClientClick="javascript:return poponloadview()"
                    Text="<%$ Resources:labels, viewphieuin %>" OnClick="btnView_Click" class="btn btn-primary" />
                &nbsp;
            <asp:Button ID="btnPrint" runat="server" OnClientClick="javascript:return poponload()"
                Text="<%$ Resources:labels, inketqua %>" OnClick="btnPrint_Click" class="btn btn-warning"/>
                &nbsp;
            <asp:Button ID="btnNew" runat="server" Text='<%$ Resources:labels, lammoi %>' OnClick="btnNew_Click" class="btn btn-success" />
            </div>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnTIBNext" />
    </Triggers>
</asp:UpdatePanel>

<script type="text/javascript">

    function replaceAll(str, from, to) {
        var idx = str.indexOf(from);
        while (idx > -1) {
            str = str.replace(from, to);
            idx = str.indexOf(from);
        }

        return str;
    }
    function ntt(sNumber, idDisplay, event) {

        executeComma(sNumber, event);

        if (document.getElementById(sNumber).value == "") {
            document.getElementById(idDisplay).innerHTML = "";
            return;
        }

        document.getElementById(idDisplay).innerHTML = "(" + number2text(replaceAll(document.getElementById(sNumber).value, ",", ""), '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>') + ")";
        document.getElementById('<%=lblText.ClientID %>').value = number2text(replaceAll(document.getElementById(sNumber).value, ",", ""), '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>');
    }


    function poponload() {
        testwindow = window.open("widgets/IBOutgoingTransfer/print.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
    function poponloadview() {
        testwindow = window.open("widgets/IBOutgoingTransfer/viewprint.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
    function validate() {
        if (validateEmpty('<%=txtReceiverAccount.ClientID %>', '<%=Resources.labels.taikhoandichkhongrong %>')){
            if (validateMoney('<%=txtAmount.ClientID %>', '<%=Resources.labels.bancannhapsotien %>')) {
                if (validateEmpty('<%=txtDesc.ClientID %>', '<%=Resources.labels.bancannhapnoidung %>')) {

                }
                else {
                    document.getElementById('<%=txtDesc.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtAmount.ClientID %>').focus();
                return false;
            }
        }
        else {
            document.getElementById('<%=txtReceiverAccount.ClientID %>').focus();
            return false;
        }
    }
    function ValidateLimit(obj, maxchar) {
        if (this.id) obj = this;
        replaceSQLChar(obj);
        var remaningChar = maxchar - obj.value.length;

        if (remaningChar <= 0) {
            obj.value = obj.value.substring(maxchar, 0);
        }
    }
    function isNumberKeyNumer(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }
    function isKey(evt) {
        var regex = new RegExp("[A-Za-z0-9]");
        var key = String.fromCharCode(event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    }
</script>

