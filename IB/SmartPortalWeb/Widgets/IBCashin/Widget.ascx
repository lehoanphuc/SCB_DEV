<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCashin_Widget" %>
<script src="JS/docso.js" type="text/javascript"></script>
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
    <span><%=Resources.labels.cashinbycard %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        
        <asp:Button runat="server" ID="btnDoTrans" CssClass="hidden" OnClick="btnDoTrans_Click" />
        <asp:HiddenField ID="hdTransID" runat="server" />
        <asp:HiddenField ID="hdAmount" runat="server" />
        <asp:HiddenField ID="hdSession" runat="server" />
        <asp:HiddenField ID="hdMerchant" runat="server" />

        <div id="divError">
            <asp:Label runat="server" ID="lblError" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <asp:Panel ID="pnAmount" runat="server" class="divcontent">
            <div class="handle">
                <span><%=Resources.labels.noidungthanhtoan %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.sotien %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtAmount" autocomplete="off" runat="server" Text="" MaxLength="21" CssClass="amount"></asp:TextBox>&nbsp;
                        <asp:Label ID="lblCurrency" runat="server"></asp:Label>
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
            </div>
            <div class="button-group">
                <asp:Button ID="btnNext" runat="server" CssClass="btn btn-primary" OnClientClick="return validate();" Text='<%$ Resources:labels, tieptuc %>' OnClick="btnNext_Click" />
            </div>
        </asp:Panel>

        <asp:Panel ID="pnConfirm" runat="server" class="divcontent">
            <div class="handle">
                <span><%=Resources.labels.noidungthanhtoan %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.sotien %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lbAmountCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.sotienphi %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblFeeAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblFeeCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.cashinamount %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblTotalAmountCCYID" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="button-group">
                <asp:Button ID="btnBackAmount" runat="server" CssClass="btn btn-warning" OnClick="btnBackAmount_OnClick" Text='<%$ Resources:labels, quaylai %>' />
                <asp:Button ID="btnApply" runat="server" CssClass="btn btn-primary" OnClick="btnApply_Click" OnClientClick="SetUpCheckout(); Checkout.showLightbox();" Text='<%$ Resources:labels, xacnhan %>' />
            </div>
        </asp:Panel>

        <asp:Panel ID="pnResult" runat="server" class="divcontent">
            <div class="handle">
                <span><%=Resources.labels.thongtinnguoitratien %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.tenchuthe %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndSenderName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.sothe %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndSenderAccount" runat="server"></asp:Label>
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
                        <asp:Label ID="lblEndReceiverAccount" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <span><%=Resources.labels.noidungthanhtoan %></span>
            </div>
            <div class="content_table">
                <div class="row">
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
                        <span><%=Resources.labels.invoicenumber %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblInvoiceNumber" runat="server"></asp:Label>
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
                        <span><%=Resources.labels.sotienphi %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndFeeAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblEndFeeCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.cashinamount %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndTotalAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblEndTotalAmountCCYID" runat="server"></asp:Label>
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
    
    function validate() {
        if (!validateMoney('<%=txtAmount.ClientID %>', "<%=Resources.labels.bancannhapsotien %>")) {
            document.getElementById('<%=txtAmount.ClientID %>').focus();
            return false;
        }
        return true;
    }

    function poponload() {
        testwindow = window.open("widgets/IBCashin/print.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }

    function poponloadview() {
        testwindow = window.open("widgets/IBCashin/viewprint.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
</script>

<script src="<%=System.Configuration.ConfigurationManager.AppSettings["LinkCashin"]%>" data-error="errorCallback" data-cancel="cancelCallback" data-complete="complete" data-beforeRedirect="getPageState" data-afterRedirect="restorePageState">
</script>
<script type="text/javascript">

    function getPageState() {
        return {
            foo: "string",
            bar: 123.45
        };
    }

    function restorePageState(data) {
        //set page state from data object
    }

    function complete() {
        document.getElementById('<%=btnDoTrans.ClientID %>').click();
    }

    function errorCallback(error) {
        alert(JSON.stringify(error));
    }

    function completeCallback(resultIndicator, sessionVersion) {
        alert("Result Indicator");
        alert(JSON.stringify(resultIndicator));
        alert("Session Version:");
        alert(JSON.stringify(sessionVersion));
        alert("Successful Payment");
    }

    function cancelCallback() {
        alert('Payment cancelled');
        document.getElementById('<%=btnBackAmount.ClientID %>').click();

    }

    function SetUpCheckout() {
        Checkout.configure({
            merchant: document.getElementById('<%=hdMerchant.ClientID %>').value,
            order: {
                amount: document.getElementById('<%=hdAmount.ClientID %>').value,
                currency: 'LAK',
                description: 'PSVB Hi Digital Banking - Cash in by card',
                id: document.getElementById('<%=hdTransID.ClientID %>').value,
            },
			billing: {
				address: {
					street: '3 Adelaide Street',
					stateProvince: 'QLD',
					city: 'Brisbane',
					company: 'Mastercard Pty Ltd',		
					postcodeZip: '4000',
					country: 'AUS'
				}
			},
			interaction: {
				merchant: {
					name: 'PSVB Hi Digital Banking',
				},
				displayControl: {
					billingAddress  : 'HIDE',
					customerEmail   : 'HIDE',						
					shipping        : 'HIDE'
				}
			},
            session: {
                id: document.getElementById('<%=hdSession.ClientID %>').value
            }
        });
    }
</script>

