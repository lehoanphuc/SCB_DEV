<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCreditUpdateCard_Widget" %>

<link href="CSS/css.css" rel="stylesheet" />
<link href="Widgets/IBCreditUpdateCard/CSS/css.css" rel="stylesheet" />

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
    <span><%=Resources.labels.updatecardstatus %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<!--Transfer In Bank-->


<asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div style="text-align: center; color: Red;">
            <asp:Label runat="server" ID="lblTextError"></asp:Label>
        </div>
        <asp:Panel ID="pnTIB" runat="server">
            <figure id="Otherdiv" runat="server">
                <%--<legend class="handle"><%=Resources.labels.chitietgiaodich %></legend>--%>
                <div class="divcontent">

                    <div class="item1">
                        <div class="handle">
                            <label class="bold"><%= Resources.labels.thongtinthe %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold "><%= Resources.labels.creditcardno %></label>
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
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
                                    <label class="bold "><%= Resources.labels.cardholdername %></label>
                                </div>
                                <div class="col-xs-7 col-sm-5 line30">
                                    <asp:Label ID="lblcardholdername" runat="server"></asp:Label>
                                    <asp:Label ID="lblLastTranDater" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold "><%= Resources.labels.outstandingamt %></label>
                                </div>
                                <div class="col-xs-7 col-sm-5 line30">
                                    <asp:Label ID="lbloutstanding" runat="server"></asp:Label>
                                    <asp:Label ID="lblAvailableBalCCYIDr" Text='<%$ Resources:labels, lak %>' runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold"><%= Resources.labels.currentstatus %></label>
                                </div>
                                <div class="col-xs-7 col-sm-4 ">
                                    <asp:DropDownList ID="ddlcurrentstatus" Enabled="false" runat="server">
                                        <asp:ListItem Text="Active" Value="A" />
                                        <asp:ListItem Text="Block" Value="B" />
                                        <asp:ListItem Text="Terminated" Value="T" />
                                        <asp:ListItem Text="Voluntary Closed" Value="V" />
                                        <asp:ListItem Text="Lost" Value="L" />
                                        <asp:ListItem Text="Stolen" Value="S" />
                                        <asp:ListItem Text="Block" Value="B" />
                                        <asp:ListItem Text="Unactived" Value="U" />
                                        <asp:ListItem Text="Counterfeit" Value="X" />
                                        <asp:ListItem Text="Destroy" Value="D" />
                                        <asp:ListItem Text="Restricted Card" Value="R" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold"><%= Resources.labels.changestatus %></label>
                                </div>
                                <div class="col-xs-7 col-sm-4">
                                    <asp:DropDownList ID="ddlchangestatus" Enabled="false" runat="server">
                                        <asp:ListItem Text="Active" Value="A" />
                                        <asp:ListItem Text="Block" Value="B" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="button-group">
                    <asp:Button ID="btnTIBNext" CssClass="btn btn-primary" runat="server" Text='<%$ Resources:labels, change %>'
                        OnClick="btnTIBNext_Click" />
                </div>
                </div>
            </figure>


        </asp:Panel>
        <!--end-->
        <!--confirm-->
        <asp:Panel ID="pnConfirm" runat="server">
            <figure>
                <%--<legend class="handle"><%=Resources.labels.chitietgiaodich %></legend>--%>
                <div class="divcontent display-label">
                    <div class="item">
                        <div class="handle">
                            <label class="bold"><%= Resources.labels.thongtinnguoitratien %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold "><%= Resources.labels.hotennguoitratien %></label>
                                </div>
                                <div class="col-xs-7 col-sm-8 line30">
                                    <asp:Label ID="lblSenderName" runat="server" Text="nguyen van a"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold "><%= Resources.labels.debitaccount %></label>
                                </div>
                                <div class="col-xs-7 col-sm-8 line30">
                                    <asp:Label ID="lblSenderAccount" runat="server"></asp:Label>
                                    <asp:Label ID="lblSenderBranch" runat="server" Visible="False"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="row">
                                    <div class="col-xs-5 col-sm-4 right">
                                        <label class="bold "><%= Resources.labels.sodutruockhighino %></label>
                                    </div>
                                    <div class="col-xs-7 col-sm-8 line30">
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
                                    <div class="col-xs-5 col-sm-4 right">
                                        <label class="bold "><%= Resources.labels.creditcardno %></label>
                                    </div>
                                    <div class="col-xs-7 col-sm-8 line30">
                                        <asp:Label ID="lblReceiverAccount" runat="server"></asp:Label>
                                        <asp:Label ID="lblReceiverBranch" runat="server" Visible="False"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-5 col-sm-4 right">
                                        <label class="bold "><%= Resources.labels.cardholdername %></label>
                                    </div>
                                    <div class="col-xs-7 col-sm-8 line30">
                                        <asp:Label ID="lblcardholderconfirm" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-5 col-sm-4 right">
                                        <label class="bold "><%= Resources.labels.outstandingamt %></label>
                                    </div>
                                    <div class="col-xs-7 col-sm-8 line30">
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
                                    <div class="col-xs-5 col-sm-4 right">
                                        <label class="bold "><%= Resources.labels.sotien %></label>
                                    </div>
                                    <div class="col-xs-7 col-sm-8 line30">
                                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                        &nbsp;<asp:Label ID="lblAmountCCYID" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-5 col-sm-4 right">
                                        <label class="bold "><%= Resources.labels.nguoitraphi %></label>
                                    </div>
                                    <div class="col-xs-7 col-sm-8 line30">
                                        <asp:Label ID="lblPhi" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-5 col-sm-4 right">
                                        <label class="bold "><%= Resources.labels.sotienphi %></label>
                                    </div>
                                    <div class="col-xs-7 col-sm-8 line30">
                                        <asp:Label ID="lblPhiAmount" runat="server"></asp:Label>
                                        &nbsp;<asp:Label ID="lblFCCYID" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-5 col-sm-4 right">
                                        <label class="bold "><%= Resources.labels.noidungthanhtoan %></label>
                                    </div>
                                    <div class="col-xs-7 col-sm-8 line30">
                                        <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="button-group">
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
                <%--<legend class="handle"><%=Resources.labels.chitietgiaodich %></legend>--%>
                <div class="divcontent display-label">
                    <div class="item1">
                        <div class="handle">
                            <label class="bold"><%= Resources.labels.xacthucgiaodich %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold "><%= Resources.labels.loaixacthuc %></label>
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
                                    <asp:DropDownList ID="ddlLoaiXacThuc" runat="server" Height="22px" Width="128px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListOTP_SelectedIndexChanged">
                                    </asp:DropDownList>
                                 
                                </div>
                                <div class="col-xs-5 col-sm-4 left">
                                    <asp:Button ID="btnSendOTP" runat="server" OnClick="btnSendOTP_Click" class="btn btn-primary" Text="Send" Visible="False" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold "><%= Resources.labels.maxacthuc %></label>
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
                                    <asp:TextBox ID="txtOTP" runat="server" AutoCompleteType="None"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-4 col-sm-4"></div>
                                <div class="col-xs-8 col-sm-4" style="text-align: center;margin:10px 0;">
                                    <img alt="" src="Images/otp.png" style="width: 100px; " />
                                </div>
                            </div>
                        </div>
                        <div class="button-group">
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
                <legend class="handle"><%=Resources.labels.ketquagiaodich %></legend>
                <div class="content display-label">
                    <div class="row">
                        <div class="header-title">
                            <label class="bold"><%= Resources.labels.xacthucgiaodich %></label>
                        </div>
                        <div class="col-xs-12">
                            <label class="bold col-xs-4 col-sm-4"><%= Resources.labels.hotennguoitratien %></label>
                            <asp:Label ID="lblEndSenderName" CssClass="col-xs-8 col-sm-8" runat="server"></asp:Label>
                        </div>
                        <div class="col-xs-12">
                            <label class="bold col-xs-4 col-sm-4"><%= Resources.labels.debitaccount %></label>
                            <asp:Label ID="lblendSenderAccount" CssClass="col-xs-8 col-sm-8" runat="server"></asp:Label>
                        </div>
                        <div class="col-xs-12">
                            <label class="bold col-xs-4 col-sm-4"><%= Resources.labels.sodusaukhighino %></label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:Label ID="lblEndBalanceSender" runat="server" Text="0"></asp:Label>
                                <asp:Label ID="lblEndSenderCCYID" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="header-title">
                            <label class="bold"><%= Resources.labels.thongtinthe %></label>
                        </div>
                        <div class="col-xs-12">
                            <label class="bold col-xs-4 col-sm-4"><%= Resources.labels.creditcardno %></label>
                            <asp:Label ID="lblEndReceiverAccount" CssClass="col-xs-8 col-sm-8" runat="server"></asp:Label>
                        </div>
                        <div class="col-xs-12">
                            <label class="bold col-xs-4 col-sm-4"><%= Resources.labels.cardholdername %></label>
                            <asp:Label ID="lblcardholdernameres" CssClass="col-xs-8 col-sm-8" runat="server"></asp:Label>
                        </div>
                        <div class="col-xs-12">
                            <label class="bold col-xs-4 col-sm-4"><%= Resources.labels.outstandingamt %></label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:Label ID="lbloutstandingamtres" runat="server" Text="0"></asp:Label>
                                <asp:Label ID="lbloutstandingrestCCYID" runat="server" Text='<%$ Resources:labels, lak %>'></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="header-title">
                            <label class="bold"><%= Resources.labels.thongtinthanhtoan %></label>
                        </div>
                        <div class="col-xs-12">
                            <label class="bold col-xs-4 col-sm-4"><%= Resources.labels.sotien %></label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:Label ID="lblEndAmount" runat="server" Text="0"></asp:Label>
                                &nbsp;<asp:Label ID="lblEndAmountCCYID" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-xs-12">
                            <label class="bold col-xs-4 col-sm-4"><%= Resources.labels.sotienphi %></label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:Label ID="lblEndPhiAmount" runat="server" Text="0"></asp:Label>
                                &nbsp;<asp:Label ID="lblFeeCCYID" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-xs-12">
                            <label class="bold col-xs-4 col-sm-4"><%= Resources.labels.noidungthanhtoan %></label>
                            <asp:Label ID="lblEndDesc" CssClass="col-xs-8 col-sm-8" runat="server"></asp:Label>
                        </div>
                        <div class="col-xs-12">
                            <label class="bold col-xs-4 col-sm-4"><%= Resources.labels.sogiaodich %></label>
                            <asp:Label ID="lblEndTransactionNo" CssClass="col-xs-8 col-sm-8" runat="server"></asp:Label>
                        </div>
                        <div class="col-xs-12">
                            <label class="bold col-xs-4 col-sm-4"><%= Resources.labels.thoidiem %></label>
                            <asp:Label ID="lblEndDateTime" CssClass="col-xs-8 col-sm-8" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="button-group">
                        <div class="col-xs-4 col-sm-4"></div>
                        <div class="col-xs-8 col-sm-8">
                            <asp:Button ID="btnView" runat="server" OnClientClick="javascript:return poponloadview()"
                                Text="<%$ Resources:labels, viewphieuin %>" OnClick="btnView_Click" />
                            <asp:Button ID="btnPrint" runat="server" OnClientClick="javascript:return poponload()"
                                Text="<%$ Resources:labels, inketqua %>" OnClick="btnPrint_Click" />
                            <asp:Button ID="btnNew" runat="server" Text='<%$ Resources:labels, lammoi %>' OnClick="Button6_Click" />

                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </figure>
        </asp:Panel>
        <!--end-->

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
        <%--document.getElementById('<%=txtChu.ClientID %>').value = number2text(replaceAll(document.getElementById(sNumber).value, ",", ""), '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>');--%>
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

