<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="widgets_ibbuytopup_widget_ascx" %>
<style>
    .amount {
        width: 83% !important;
    }

    .right {
        font-weight: bold;
    }
</style>
<link href="CSS/css.css" rel="stylesheet" type="text/css" />


<asp:ScriptManager runat="server">
</asp:ScriptManager>
<div style="text-align: center; color: Red;">
    <asp:Label ID="Label10" runat="server"></asp:Label>

</div>

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
    <span><%=Resources.labels.mobiletopup %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div id="divError">
            <asp:Label runat="server" ID="lblTextError" Font-Bold="true" ForeColor="Red" Text=""></asp:Label>
        </div>
        <div>
            <asp:HiddenField runat="server" ID="hdAccountName" />
            <asp:HiddenField runat="server" ID="hdIDREF" />
            <asp:HiddenField runat="server" ID="hdPHONETRAN" />
            <asp:HiddenField runat="server" ID="hdAMOUNT" />
            <asp:HiddenField runat="server" ID="hdCCYID" />
            <asp:HiddenField runat="server" ID="hdContent" />
            <asp:HiddenField runat="server" ID="hdfvalidPhone" />
            <asp:HiddenField runat="server" ID="hdfPhoneNo" />
            <asp:HiddenField runat="server" ID="hdfAmount" />
            <asp:HiddenField runat="server" ID="hdTranCode" />
            <asp:HiddenField runat="server" ID="hdFeeSender" />
            <asp:HiddenField runat="server" ID="hdFeeReceiver" />
            <asp:HiddenField runat="server" ID="hdSenderAccountNo" />
            <asp:HiddenField runat="server" ID="hdtecode" />
            <asp:HiddenField ID="hdActTypeSender" runat="server" />    
            <asp:HiddenField ID="hdSenderName" runat="server" />   
            <asp:HiddenField ID="hdSenderBranch" runat="server" />
            <asp:HiddenField ID="hdBILLTYPE" runat="server" />
        </div>
        <asp:Panel ID="pnTIB" runat="server" class="divcontent">

            <figure>
                <div class="handle">
                    <asp:Label runat="server" Text='<%$ Resources:labels, thongtintaikhoanchuyen %>'></asp:Label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-4 col-md-4 right">
                            <asp:Label ID="lbderbit" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-8 col-md-4 line30">
                            <asp:DropDownList ID="ddlSenderAccount" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged"></asp:DropDownList>

                            <asp:HiddenField ID="txtChu" runat="server" />
                        </div>
                    </div>
                    <div class="row" style="display: none">
                        <div class="col-xs-4 col-md-4 right">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, lasttransactiondate %>"></asp:Label>
                        </div>
                        <div class="col-xs-8 col-md-4 line30">
                            <asp:Label ID="lblLastTranDate" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4 col-md-4 right">
                            <asp:Label ID="lbavaila" runat="server" Text="<%$ Resources:labels, availablebalance %>"></asp:Label>
                        </div>
                        <div class="col-xs-8 col-md-4 line30">
                            <asp:Label ID="lblAvailableBal" runat="server"></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblAvailableBalCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </figure>
            <figure>
                <div class="handle">
                    <asp:Label runat="server" Text='<%$ Resources:labels, thongtintopup %>'></asp:Label>
                </div>
                <div class="content_table">
                    <div class="row" runat="server" visible="false">
                        <div class="col-xs-4 col-md-4 right">
                            <asp:Label ID="Label7" runat="server" Text='<%$ Resources:labels, type %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-8 col-md-4 line30">
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Mobile Topup" Value="2" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lbEloadnotice" Font-Bold="true" ForeColor="Blue" runat="server" Text="" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="row" id="trPhoneNumber" runat="server">
                        <div class="col-xs-4 col-md-4 right">
                            <asp:Label ID="lblPhoneNumber" runat="server" Text='<%$ Resources:labels, sodienthoai %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-8 col-md-4" runat="server">
                            <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" AutoPostBack="True" Text="" MaxLength="12"
                                OnTextChanged="txtPhoneNumber_SelectedIndexChanged">
                            </asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-xs-4 col-md-4 right">
                            <span><%=Resources.labels.type %>&nbsp;*</span>
                        </div>
                        <div class="col-xs-8 col-md-4 line30">
                            <asp:Label ID="lblTelcoType" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row" id="divTelcoBalance" Visible="False" runat="server">
                        <div class="col-xs-4 col-md-4 right">
                            <asp:Label ID="lbBalanceTitle" runat="server" Text='<%$ Resources:labels, phonebalance %>'></asp:Label>
                        </div>
                        <div class="col-xs-8 col-md-4 line30">
                            <asp:Label ID="lbBalance" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4 col-md-4 right">
                            <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, telco %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-8 col-md-4 line30">
                            <asp:DropDownList ID="ddlTelco" runat="server" CssClass="form-control" AutoPostBack="True"
                                Enabled="false" OnSelectedIndexChanged="ddlTelco_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4 col-md-4 right">
                            <asp:Label ID="Label26" runat="server" Text='<%$ Resources:labels, listanamount %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-8 col-md-4 line30">
                            <asp:DropDownList ID="ddlAmount" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlAmount_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" style="display:none;">
                        <div class="col-xs-4 col-sm-4 right">
                            <span><%=Resources.labels.sotien %></span>&nbsp;*
                        </div>
                        <div class="col-xs-8 col-md-4 col-sm-6 line30">
                            <asp:TextBox ID="txtAmount" autocomplete="off" runat="server" Text="" MaxLength="21" CssClass="amount"></asp:TextBox>&nbsp;
                             <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                            <div>
                                <asp:Label ID="lblText" runat="server" Font-Size="7pt" Font-Bold="True" Font-Italic="True"
                                    ForeColor="#0066FF" Style="padding-top: 2px; line-height: 23px !important;"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4 col-md-4 right">
                            <span><%=Resources.labels.noidungthanhtoan %></span>&nbsp;*
                        </div>
                        <div class="col-xs-8 col-md-4 line30">
                            <asp:TextBox ID="txtContent" runat="server" Height="50px" TextMode="MultiLine" onkeyup="ValidateLimit(this,200);" onkeyDown="ValidateLimit(this,200);" onpaste="ValidateLimit(this,200);"></asp:TextBox><br />
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
            </figure>
            <div style="text-align: center; margin-top: 10px;">
                <asp:Button ID="btnTIBNext" runat="server" CssClass="btn btn-primary" OnClientClick="return validate();" Text='<%$ Resources:labels, tieptuc %>' OnClick="btnTUIBNext_Click" />
            </div>
            <!--Button next-->

        </asp:Panel>
        <!--end-->
        <!--confirm-->
        <asp:Panel ID="pnConfirm" runat="server" class="divcontent">
            <figure>
                <legend class="handle"><%=Resources.labels.thongtintaikhoanchuyen %></legend>

                <div class="content display-label">
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, hotennguoitratien %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblSenderName" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label8" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblSenderAccount" runat="server"></asp:Label>
                            <asp:Label ID="lblSenderBranch" runat="server" Visible="False"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label18" runat="server" Text='<%$ Resources:labels, sodutruockhighino %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblBalanceSender" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblSenderCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </figure>
            <figure>
                <legend class="handle"><%=Resources.labels.thongtintopup %></legend>
                <div class="content display-label">
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, telco %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblTelco" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, amount %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblACCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label45" runat="server" Text='<%$ Resources:labels, sotienphi %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblPhiAmount" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblFCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row" runat="server" id="discout" visible="false">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label2" runat="server" Text="Discount"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblDiscount" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblDiscountCCIYD" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row" id="trPhone" runat="server">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="lblPhone" runat="server" Text='<%$ Resources:labels, phone %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="txtPhone" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row" id="Div1" runat="server">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label13" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblContent" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </figure>
            <div class="row button-group">
                <asp:Button ID="btnBackTransfer" runat="server" CssClass="btn btn-warning" OnClick="btnBacktoPanel1_Click" Text='<%$ Resources:labels, quaylai %>' />
                <asp:Button ID="btnAddnew" runat="server" CssClass="btn btn-success" OnClick="btnNewTransaction_Click" Text='<%$ Resources:labels, lammoi %>' />
                <asp:Button ID="btnApply" runat="server" CssClass="btn btn-primary" OnClick="btnApply_Click" Text='<%$ Resources:labels, xacnhan %>' />
                <div class="clearfix"></div>
            </div>
        </asp:Panel>
        <!--end-->
        <!--token-->
        <asp:Panel ID="pnOTP" runat="server" Visible="false">
            <div class="handle">
                <label class="bold"><%=Resources.labels.xacthucgiaodich %></label>
            </div>
            <div class="content display-label">
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
                                <span><%=Resources.labels.OTPcodeexpiresin %></span>&nbsp;<span class="countdown_time"></span>
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
                <div class="button-group">
                    <asp:Button ID="Button4" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, quaylai %>" OnClick="Button4_Click" />
                    <asp:Button ID="btnAction" CssClass="btn btn-primary" runat="server" OnClick="btnAction_Click" Text='<%$ Resources:labels, thuchien %>' />
                </div>
            </div>
        </asp:Panel>
        <!--end-->
        <asp:Panel ID="pnResult" runat="server" Visible="false">
            <div class="button-group">
                <asp:Button ID="btnBackResult" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, quaylai %>" OnClick="btnBackResult_Click" />
            </div>
        </asp:Panel>
        <!--sao ke-->
        <asp:Panel ID="pnResultTransaction" runat="server" class="divcontent">
            <div class="content">

                <div class="handle">
                    <label class="bold"><%= Resources.labels.thongtintaikhoanchuyen %></label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <label class="col-sm-6 col-xs-5 right"><%= Resources.labels.hotennguoitratien %></label>
                        <div class="col-sm-6 col-xs-7 line30">
                            <asp:Label ID="lblEndSenderName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-sm-6 col-xs-5 right"><%= Resources.labels.debitaccount %></label>
                        <div class="col-sm-6 col-xs-7 line30">
                            <asp:Label ID="lblendSenderAccount" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-sm-6 col-xs-5 right"><%= Resources.labels.sodusaukhighino %></label>
                        <div class="col-sm-6 col-xs-7 line30">
                            <asp:Label ID="lblEndBalanceSender" runat="server" Text="0"></asp:Label>
                            <asp:Label ID="lblEndSenderCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="handle">
                    <label class="bold"><%= Resources.labels.thongtintopup %></label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <label class="col-sm-6 col-xs-5 right"><%= Resources.labels.telco %></label>
                        <div class="col-sm-6 col-xs-7 line30">
                            <asp:Label ID="lblEndReceiverName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-sm-6 col-xs-5 right"><%= Resources.labels.amount %></label>
                        <div class="col-sm-6 col-xs-7 line30">
                            <asp:Label ID="lblEndBalanceReceiver" runat="server" Text="0"></asp:Label>
                            <asp:Label ID="lblEndReceiverCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="handle">
                    <label class="bold"><%= Resources.labels.noidungchuyenkhoan %></label>
                </div>
                <div class="content_table">
                    <div class="row" id="tranno" runat="server">
                        <label class="col-sm-6 col-xs-5 right"><%= Resources.labels.sogiaodich %></label>
                        <div class="col-sm-6 col-xs-7 line30">
                            <asp:Label ID="lblEndTransactionNo" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-sm-6 col-xs-5 right"><%= Resources.labels.thoidiem %></label>
                        <div class="col-sm-6 col-xs-7 line30">
                            <asp:Label ID="lblEndDateTime" runat="server" Text="0"></asp:Label>
                        </div>
                    </div>
                    <div class="row" id="trPhoneNo" runat="server" visible="false">
                        <label class="col-sm-6 col-xs-5 right"><%= Resources.labels.phone %></label>
                        <div class="col-sm-6 col-xs-7 line30">
                            <asp:Label ID="lblPhoneNo" runat="server" Text="0"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-sm-6 col-xs-5 right"><%= Resources.labels.sotien %></label>
                        <div class="col-sm-6 col-xs-7 line30">
                            <asp:Label ID="lblEndAmount" runat="server" Text="0"></asp:Label>
                            &nbsp;<asp:Label ID="lblEndAmountCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-sm-6 col-xs-5 right"><%= Resources.labels.nguoitraphi %></label>
                        <div class="col-sm-6 col-xs-7 line30">
                            <asp:Label ID="lblEndPhi" runat="server" Text="0"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-sm-6 col-xs-5 right"><%= Resources.labels.sotienphi %></label>
                        <div class="col-sm-6 col-xs-7 line30">
                            <asp:Label ID="lblEndPhiAmount" runat="server" Text="0"></asp:Label>
                            &nbsp;<asp:Label ID="lblFeeCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row" id="trSoftPin" runat="server">
                        <label class="col-sm-6 col-xs-5 right"><%= Resources.labels.softpintopup %></label>
                        <div class="col-sm-6 col-xs-7 line30">
                            <asp:Label ID="LblSoftpin" runat="server" Text="0"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-sm-6 col-xs-5 right"><%= Resources.labels.noidungthanhtoan %></label>
                        <div class="col-sm-6 col-xs-7 line30">
                            <asp:Label ID="lblEndDesc" runat="server" Text="0"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row" style="text-align: center; padding-top: 10px;">
                    <asp:Button ID="btnView" CssClass="btn btn-primary" runat="server" OnClientClick="javascript:return poponloadview()"
                        Text="<%$ Resources:labels, viewphieuin %>" OnClick="btnView_Click" />

                    <asp:Button ID="btnPrint" CssClass="btn btn-warning" runat="server" OnClientClick="javascript:return poponload()"
                        Text="<%$ Resources:labels, inketqua %>" OnClick="btnPrint_Click" />

                    <asp:Button ID="btnNew" CssClass="btn btn-success" runat="server" Text='<%$ Resources:labels, lammoi %>' OnClick="btnNewTransaction_Click"/>

                    <div class="clearfix"></div>
                </div>
            </div>
        </asp:Panel>
        <!--end-->
        <br />
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnTIBNext" />
    </Triggers>
</asp:UpdatePanel>
<script type="text/javascript">
    function validate() {
        if (!validateEmpty('<%=txtContent.ClientID %>', '<%=Resources.labels.bancannhapnoidung %>', '<%=lblTextError.ClientID%>')) {
            document.getElementById('<%=txtContent.ClientID %>').focus();
            return false;
        }

    }
    function poponload() {
        testwindow = window.open("widgets/IBBuyTopup/print.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }

    function poponloadview() {
        testwindow = window.open("widgets/IBBuyTopup/viewprint.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
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
        //validate phone number
        if (document.getElementById('<%=txtPhoneNumber.ClientID%>').value == '' ||
            document.getElementById('<%=txtPhoneNumber.ClientID%>').value == '09') {
            alert('<%=Resources.labels.phonenumberwrong %>');
            return false;
        }
        else
            return true;
    }
</script>

