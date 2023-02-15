<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCreditPaymentOtherCard_Widget" %>

<link href="CSS/css.css" rel="stylesheet" />
<link href="widgets/IBCreditPaymentOtherCard/CSS/css.css" rel="stylesheet" />
<script src="JS/mask.js"></script>
<script src="JS/docso.js"></script>

<asp:ScriptManager runat="server">
</asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="" runat="server">
    <ProgressTemplate>
        <div class="cssProgress">
            <div class="progress1">
                <div class="cssProgress-bar cssProgress-active" data-percent="100" style="transition: none; width: 100%;"></div>
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>

<div class="th">
    <span><%=Resources.labels.paymentforothercard %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div style="text-align: center; color: Red;">
            <asp:Label runat="server" ID="lblTextError"></asp:Label>
        </div>
        <asp:Panel ID="pnTIB" runat="server">
            <figure id="Otherdiv" runat="server">
                <div class="divcontent">
                    <div class="item1">
                        <div class="handle">
                            <label class="bold"><%= Resources.labels.thongtinnguoitratien %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold"><%= Resources.labels.debitaccount %></label>
                                </div>
                                <div class="col-xs-7 col-sm-4">
                                    <asp:DropDownList ID="ddlSenderAccount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RadioButton ID="radTS1" runat="server" Checked="True" GroupName="TIB" onclick="resetTS();"
                                        Text="Chuyển ngay" Visible="False" />
                                    <asp:HiddenField ID="txtChu" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold"><%= Resources.labels.lasttransactiondate %></label>
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
                                    <asp:Label ID="lblLastTranDate" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold"><%= Resources.labels.availablebalance %></label>
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
                                    <asp:Label ID="lblAvailableBal" runat="server"></asp:Label>
                                    <asp:Label ID="lblAvailableBalCCYID" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="item1">
                        <div class="handle">
                            <label class="bold"><%= Resources.labels.thongtinthe %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold"><%= Resources.labels.creditcardno %></label>
                                </div>
                                <div class="col-xs-7 col-sm-4">
                                    <asp:DropDownList ID="ddlcreditcardno" runat="server" OnSelectedIndexChanged="OnReceiverAccountChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdflimit" runat="server" />
                                    <asp:RadioButton ID="radTS" runat="server" GroupName="TIB" onclick="enableTS();"
                                        Text="Chuyển vào ngày (dd/mm/yyyy)" Visible="False" />
                                    <asp:TextBox ID="txtTS" runat="server" Visible="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold"><%= Resources.labels.cardholdername %></label>
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
                                    <asp:Label ID="lblcardholdername" runat="server"></asp:Label>
                                    <asp:Label ID="lblLastTranDater" Visible="false" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold"><%= Resources.labels.outstandingamt %></label>
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
                                    <asp:Label ID="lbloutstanding" runat="server"></asp:Label>
                                    <asp:Label ID="lblAvailableBalCCYIDr" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="item1">
                        <div class="handle">
                            <label class="bold"><%= Resources.labels.thongtinthanhtoan %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold"><%= Resources.labels.sotien %></label>&nbsp;*
                                </div>
                                <div class="col-xs-5 col-sm-4">
                                    <asp:TextBox ID="txtAmount" runat="server" Text="" CssClass="amount" MaxLength="15"></asp:TextBox>

                                    <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                                    <asp:Label ID="lblText" runat="server" Font-Size="7pt" Font-Bold="True" Font-Italic="True"
                                        ForeColor="#0066FF" Width="200px"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold">
                                        <%= Resources.labels.noidungthanhtoan %></label>&nbsp;*

                                    <asp:Label ID="Label40" runat="server" Text='<%$ Resources:labels, nguoitraphi %>'
                                        Visible="false">*</asp:Label>&nbsp;
                                <asp:RadioButtonList ID="rdNguoiChiuPhi" runat="server" RepeatDirection="Horizontal"
                                    Visible="false">
                                    <asp:ListItem Selected="True" Text="<%$ Resources:labels, nguoigui %>"></asp:ListItem>
                                    <asp:ListItem Text="<%$ Resources:labels, nguoinhan %>"></asp:ListItem>
                                </asp:RadioButtonList>
                                </div>
                                <div class="col-xs-7 col-sm-4 ">
                                    <asp:TextBox ID="txtDesc" runat="server" Height="50px" TextMode="MultiLine" CssClass="form-control" onkeyup="ValidateLimit(this,200);" onkeyDown="ValidateLimit(this,200);" onpaste="ValidateLimit(this,200);"></asp:TextBox>
                                    <div style="vertical-align: text-top;">
                                        <asp:Label ID="lblNote" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666"
                                            Text='<%$ Resources:labels, ghichunoidung %>'></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="button-group">
                        <asp:Button ID="btnTIBNext" runat="server" CssClass="btn btn-primary" OnClientClick="return validate();" Text='<%$ Resources:labels, tieptuc %>'
                            OnClick="btnTIBNext_Click" />
                    </div>
                    <asp:HiddenField ID="hfccyid" runat="server" />
                    <asp:HiddenField ID="hfcif" runat="server" />
                    <asp:HiddenField ID="hfcurid" runat="server" />
                </div>
            </figure>
        </asp:Panel>
        <!--end-->
        <!--confirm-->
        <asp:Panel ID="pnConfirm" runat="server">
            <figure>
                <div class="divcontent display-label">
                    <div class="item">
                        <div class="handle">
                            <label class="bold"><%= Resources.labels.thongtinnguoitratien %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.hotennguoitratien %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblSenderName" runat="server" Text="nguyen van a"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.debitaccount %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblSenderAccount" runat="server"></asp:Label>
                                    <asp:Label ID="lblSenderBranch" runat="server" Visible="False"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.sodutruockhighino %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblBalanceSender" runat="server"></asp:Label>
                                    &nbsp;<asp:Label ID="lblSenderCCYID" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="item">
                        <div class="handle">
                            <label class="bold"><%= Resources.labels.thongtinthe %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.creditcardno %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblReceiverAccount" runat="server"></asp:Label>
                                    <asp:Label ID="lblReceiverBranch" runat="server" Visible="False"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.cardholdername %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblcardholderconfirm" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.outstandingamt %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lbloutstandingconfirm" runat="server"></asp:Label>
                                    <asp:Label ID="lbloutstandingconfirmCCYID" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="item">
                        <div class="handle">
                            <label class="bold"><%= Resources.labels.thongtinthanhtoan %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.sotien %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                    &nbsp;<asp:Label ID="lblAmountCCYID" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.nguoitraphi %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblPhi" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.sotienphi %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblPhiAmount" runat="server"></asp:Label>
                                    &nbsp;<asp:Label ID="lblFCCYID" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.noidungthanhtoan %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="button-group" style="text-align: center;">
                        <asp:Button ID="btnBackTransfer" CssClass="btn btn-warning" runat="server" OnClick="btnBackTransfer_Click" Text='<%$ Resources:labels, quaylai %>' />
                        <asp:Button ID="btnApply" CssClass="btn btn-primary" runat="server" OnClick="btnApply_Click" Text='<%$ Resources:labels, xacnhan %>' />
                        <div class="clearfix"></div>
                    </div>
                </div>
            </figure>
        </asp:Panel>
        <!--end-->
        <!--token-->
        <asp:Panel ID="pnOTP" runat="server">
            <figure>
                <div class="divcontent display-label">
                    <div class="item1">
                        <div class="handle">
                            <label class="bold"><%= Resources.labels.xacthucgiaodich %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-4 col-sm-4 right">
                                    <label class="bold"><%= Resources.labels.loaixacthuc %></label>
                                </div>
                                <div class="col-xs-6 col-sm-4">
                                    <asp:DropDownList ID="ddlLoaiXacThuc" runat="server" Height="22px" Width="128px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListOTP_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-1">
                                    <asp:Button ID="btnSendOTP" runat="server" OnClick="btnSendOTP_Click" Text="Send" CssClass="btn btn-primary" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-4 col-sm-4 right">
                                    <label class="bold"><%= Resources.labels.maxacthuc %></label>
                                </div>
                                <div class="col-xs-7 col-sm-4">
                                    <asp:TextBox ID="txtOTP" runat="server" AutoCompleteType="None"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-2 col-sm-4"></div>
                                <div class="col-xs-8 col-sm-4" style="text-align: center; margin: 10px 0;">
                                    <img alt="" src="Images/otp.png" style="width: 100px;" />
                                </div>
                            </div>
                        </div>
                        <div class="button-group" style="text-align: center">
                            <asp:Button ID="btnBack" CssClass="btn btn-warning" runat="server" OnClick="btnBack_Click" Text='<%$ Resources:labels, quaylai %>' />
                            <asp:Button ID="btnAction" CssClass="btn btn-primary" runat="server" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="btnAction_Click" Text='<%$ Resources:labels, thuchien %>' />
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </figure>
        </asp:Panel>
        <!--end-->
        <!--sao ke-->
        <asp:Panel ID="pnResultTransaction" runat="server">
            <figure id="Figure3" runat="server">
                <%--<legend class="handle"><%=Resources.labels.ketquagiaodich %></legend>--%>
                <div class="divcontent display-label">
                    <div class="item">
                        <div class="handle">
                            <label class="bold"><%= Resources.labels.xacthucgiaodich %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.hotennguoitratien %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblEndSenderName" CssClass="col-xs-8 col-sm-8" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.debitaccount %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblendSenderAccount" CssClass="col-xs-8 col-sm-8" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.sodusaukhighino %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblEndBalanceSender" runat="server" Text="0"></asp:Label>
                                    <asp:Label ID="lblEndSenderCCYID" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="item">
                        <div class="handle">
                            <label class="bold"><%= Resources.labels.thongtinthe %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.creditcardno %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblEndReceiverAccount" CssClass="col-xs-8 col-sm-8" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.cardholdername %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblcardholdernameres" CssClass="col-xs-8 col-sm-8" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.outstandingamt %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lbloutstandingamtres" runat="server" Text="0"></asp:Label>
                                    <asp:Label ID="lbloutstandingrestCCYID" runat="server" Text='<%$ Resources:labels, lak %>'></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="item">
                        <div class="handle">
                            <label class="bold"><%= Resources.labels.thongtinthanhtoan %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.sotien %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblEndAmount" runat="server" Text="0"></asp:Label>
                                    &nbsp;<asp:Label ID="lblEndAmountCCYID" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.sotienphi %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblEndPhiAmount" runat="server" Text="0"></asp:Label>
                                    &nbsp;<asp:Label ID="lblFeeCCYID" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.noidungthanhtoan %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblEndDesc" CssClass="col-xs-8 col-sm-8" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.sogiaodich %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblEndTransactionNo" CssClass="col-xs-8 col-sm-8" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold"><%= Resources.labels.thoidiem %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblEndDateTime" CssClass="col-xs-8 col-sm-8" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="button-group" style="text-align: center">
                        <asp:Button ID="btnView" CssClass="btn btn-primary" runat="server" OnClientClick="javascript:return poponloadview()"
                            Text="<%$ Resources:labels, viewphieuin %>" OnClick="btnView_Click" />
                        <asp:Button ID="btnPrint" CssClass="btn btn-warning" runat="server" OnClientClick="javascript:return poponload()"
                            Text="<%$ Resources:labels, inketqua %>" OnClick="btnPrint_Click" />
                        <asp:Button ID="btnNew" CssClass="btn btn-success" runat="server" Text='<%$ Resources:labels, lammoi %>' OnClick="Button6_Click" />

                        <div class="clearfix"></div>
                    </div>
                </div>
            </figure>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    function poponload() {
        testwindow = window.open("widgets/IBCreditPaymentOwnCard/print.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }

    function poponloadview() {
        testwindow = window.open("widgets/IBCreditPaymentOwnCard/viewprint.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }

    function resetTS() {
        document.getElementById("<%=txtTS.ClientID %>").value = "";
        document.getElementById("<%=txtTS.ClientID %>").disabled = true;
    }
    function enableTS() {
        document.getElementById("<%=txtTS.ClientID %>").disabled = false;
    }

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
        document.getElementById('<%=txtChu.ClientID %>').value = number2text(replaceAll(document.getElementById(sNumber).value, ",", ""), '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>');
    }


    function validate() {
        if (document.getElementById("<%= hdflimit.ClientID %>").value != "") {
            if (confirm(document.getElementById("<%= hdflimit.ClientID %>").value) == true) {

            }
            else {
                return false;
            }
        }
        if (validateMoney('<%=txtAmount.ClientID %>', '<%=Resources.labels.bancannhapsotien %>', '<%=lblTextError.ClientID %>')) {
            if (validateEmpty('<%=txtDesc.ClientID %>', '<%=Resources.labels.bancannhapnoidung %>', '<%=lblTextError.ClientID %>')) {
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
    function ValidateLimit(obj, maxchar) {
        if (this.id) obj = this;
        replaceSQLChar(obj);
        var remaningChar = maxchar - obj.value.length;

        if (remaningChar <= 0) {
            obj.value = obj.value.substring(maxchar, 0);
        }
    }

</script>

