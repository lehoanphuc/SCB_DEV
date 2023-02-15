<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBFXTransfer_Widget" %>
<script src="JS/docso.js" type="text/javascript"></script>
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
    <span><%=Resources.labels.crosscurrencytransfer %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label runat="server" ID="lblError" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div>
            <asp:HiddenField runat="server" ID="hdTypeID" />
            <asp:HiddenField runat="server" ID="hdTrancode" />
            <asp:HiddenField runat="server" ID="hdActTypeReceiver" />
        </div>
        <asp:Panel ID="pnTRF" runat="server" class="divcontent">
            <div class="handle">
                <span><%=Resources.labels.thongtinnguoitratien %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.debitaccount %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:DropDownList ID="ddlSenderAccount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:HiddenField ID="hdBranchID" runat="server" />
                        <asp:HiddenField ID="hdPhoneNo" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.availablebalance %></span>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:Label ID="lblAvailableBal" runat="server"></asp:Label>&nbsp;
                        <asp:Label ID="lblAvailableBalCCYID" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <span><%=Resources.labels.thongtinnguoinhantien %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.taikhoanbaoco %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtCreditAccount" MaxLength="20" runat="server"  onblur="OnBlurCreditAccount();"></asp:TextBox>
                        <asp:LinkButton ID="lbtGetInfoCreditAccount" runat="server" OnClick="lbtGetInfoCreditAccount_OnClick" />
                        <asp:HiddenField ID="hdCreditBranchID" runat="server" />
                        <asp:HiddenField ID="hdCreditCCYID" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="LabelReceiver" runat="server" Text='<%$ Resources:labels, hotennguoinhantien %>'></asp:Label>&nbsp;
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">                    
                           <asp:Label ID="LabelReceiverName" runat="server" Text="" Visible="false"></asp:Label>                      
                    </div>
                </div>
                <div class="handle">
                    <span><%=Resources.labels.noidungchuyenkhoan %></span>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <span><%=Resources.labels.sotien %></span>&nbsp;*
                        </div>
                        <div class="col-xs-7 col-md-4 line30">
                            <asp:TextBox ID="txtAmount" autocomplete="off" runat="server" Text="" MaxLength="15" CssClass="amount"></asp:TextBox>
                            <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdAmount" runat="server" />
                            <asp:HiddenField ID="hdExchangeRate" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            &nbsp;
                        </div>
                        <div class="col-xs-7 col-sm-4 line30">
                            <asp:Label ID="lblText" runat="server" Font-Size="7pt" Font-Bold="True" Font-Italic="True" ForeColor="#0066FF" Style="padding-top: 2px; line-height: 23px !important;"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <span><%=Resources.labels.noidungthanhtoan %>*</span>
                        </div>
                        <div class="col-xs-7 col-sm-4 line30">
                            <asp:TextBox ID="txtDesc" runat="server" Height="50px" TextMode="MultiLine" onkeyup="ValidateLimit(this,200);" onkeyDown="ValidateLimit(this,200);" onpaste="ValidateLimit(this,200);"></asp:TextBox><br />
                            <asp:Label ID="lblNote" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666"
                                Text='<%$ Resources:labels, ghichunoidung %>'></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="row form-group">
                            <div class="col-xs-3 col-sm-4 right">
                                <asp:Label ID="LblDocument" Visible="false" runat="server" Text=""><%= Resources.labels.document %>&nbsp;*</asp:Label>
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
            
                <div class="button-group">
                    <asp:Button ID="btnNext" runat="server" CssClass="btn btn-primary" OnClientClick="return validate();" Text='<%$ Resources:labels, tieptuc %>' OnClick="btnNext_Click" />
                </div>
        </asp:Panel>

        <asp:Panel ID="pnConfirm" runat="server" class="divcontent">
            <div class="handle">
                <span><%=Resources.labels.thongtinnguoitratien %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.hotennguoitratien %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.debitaccount %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblSenderAccount" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.availablebalance %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblBalanceSender" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblSenderCCYID" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <span><%=Resources.labels.thongtinnguoinhantien %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.hotennguoinhantien %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblCreditName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.taikhoanbaoco %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblCreditAccount" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <span><%=Resources.labels.rate %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblRate" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblRateAmount" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <span><%=Resources.labels.noidungchuyenkhoan %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.debitamount %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblAmount_DB" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lbCCYID_DB" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.creditamount %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblAmount_CR" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lbCCYID_CR" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.nguoitraphi %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblPhi" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.sotienphi %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblPhiAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblFeeCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.noidungthanhtoan %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblDesc" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="button-group">
                <asp:Button ID="btnBackTRF" runat="server" CssClass="btn btn-warning" OnClick="btnBackTRF_OnClick" Text='<%$ Resources:labels, quaylai %>' />
                <asp:Button ID="btnApply" runat="server" CssClass="btn btn-primary" OnClick="btnApply_Click" Text='<%$ Resources:labels, xacnhan %>' />
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
                        <asp:TextBox ID="txtOTP" runat="server" Width="100%" AutoCompleteType="None"></asp:TextBox>
                    </div>
                </div>
                <div class="row" style="text-align: center; color: #7A58BF">
                    <asp:Label ID="lbnotice" runat="server" class="bold" Visible="false">If you do not see the email in your inbox, please check your “junk mail” folder or “spam” folder.</asp:Label>
                </div>
            </div>
            <div class="button-group">
                <asp:Button ID="btnBackConfirm" runat="server" CssClass="btn btn-warning" OnClick="btnBackConfirm_Click" Text="<%$ Resources:labels, quaylai %>" />
                <asp:Button ID="btnAction" runat="server" CssClass="btn btn-primary" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="btnAction_Click" Text="<%$ Resources:labels, thuchien %>" />
            </div>
        </asp:Panel>

        <asp:Panel ID="pnResult" runat="server" class="divcontent">
            <div class="handle">
                <span><%=Resources.labels.thongtinnguoitratien %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.hotennguoitratien %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndSenderName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.debitaccount %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndSenderAccount" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.availablebalance %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndBalanceSender" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblBalanceCCYID" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <span><%=Resources.labels.thongtinnguoinhantien %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.hotennguoinhantien %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndReceiverName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.taikhoanbaoco %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndCreditAccount" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <span><%=Resources.labels.noidungchuyenkhoan %></span>
            </div>
            <div class="content_table">
                <div class="row" runat="server" id="SGD">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.sogiaodich %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndTransactionNo" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.thoidiem %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndDateTime" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.sotien %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblEndAmountCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.nguoitraphi %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndPhi" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.sotienphi %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndPhiAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblEndFeeCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.noidungthanhtoan %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndDesc" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="button-group">
                <asp:Button ID="btnView" runat="server" OnClientClick="javascript:return poponloadview();" Text="<%$ Resources:labels, viewphieuin %>" OnClick="btnView_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnPrint" runat="server" OnClientClick="javascript:return poponload();" Text="<%$ Resources:labels, inketqua %>" OnClick="btnPrint_Click" CssClass="btn btn-warning" />
                <asp:Button ID="btnNew" runat="server" Text='<%$ Resources:labels, lammoi %>' OnClick="btnNew_Click" CssClass="btn btn-success" />
            </div>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnNext" />
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
    }

    function OnBlurCreditAccount() {
	
		document.getElementById("<%= lbtGetInfoCreditAccount.ClientID %>").click();

	
    };

    function validate() {
        if (!validateEmpty('<%=txtCreditAccount.ClientID %>', "<%=Resources.labels.bancannhaptaikhoannguoinhan %>")) {
            return false;
        }
        if (!validateMoney('<%=txtAmount.ClientID %>', '<%=Resources.labels.bancannhapsotien %>')) {
            document.getElementById('<%=txtAmount.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtDesc.ClientID %>', '<%=Resources.labels.bancannhapnoidung %>')) {
            return false;
        }
        return true;
    }

    function poponload() {
        testwindow = window.open("widgets/IBFXTransfer1/print.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }

    function poponloadview() {
        testwindow = window.open("widgets/IBFXTransfer1/viewprint.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
</script>
