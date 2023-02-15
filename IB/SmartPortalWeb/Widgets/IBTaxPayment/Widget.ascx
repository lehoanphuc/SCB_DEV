<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTaxPayment_Widget" %>
<script src="JS/mask.js" type="text/javascript"></script>
<script src="JS/docso.js" type="text/javascript"></script>
<style>
    .amount {
        width: 83% !important;
    }

    .h_30 {
        height: 30px;
    }
</style>
<asp:ScriptManager runat="server">
</asp:ScriptManager>
<!--Transfer BAC-->
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
<div class="al">
    <span><%=Resources.labels.taxpayment %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="text-align: center; color: Red; font-weight: bold">
            <asp:Label runat="server" ID="lblTextError" Text="test"></asp:Label>
        </div>
        <asp:Panel ID="pnTIB" runat="server" class="divcontent">
            <figure>
                <legend class="handle"><%=Resources.labels.thongtintaikhoanchuyen %> 
                </legend>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-4 line30">
                            <asp:DropDownList ID="ddlSenderAccount" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged"></asp:DropDownList>
                            <asp:HiddenField ID="txtChu" runat="server" />
                        </div>
                    </div>
                    <div class="row" style="display: none">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, lasttransactiondate %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-4 line30">
                            <asp:Label ID="lblLastTranDate" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, availablebalance %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-4 line30">
                            <asp:Label ID="lblAvailableBal" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblAvailableBalCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="handle">
                    <%=Resources.labels.thongtinthanhtoan %>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="lblTino" runat="server" Text='<%$ Resources:labels, taxpayeridentificationnumber %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:TextBox ID="txtTIN" runat="server" MaxLength="15" CssClass="form-control"></asp:TextBox>
                            <asp:Button runat="server" ID="btnCheckTINO" Style="display: none;" OnClick="btnCheckTIN_OnClick" CssClass="btn btn-primary" />
                        </div>
                        <div class=" col-xs-offset-4 col-xs-7 col-sm-4" style="line-height: 30px; text-align: left;">
                            <asp:Label ID="lblTaxOfficeName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row" id="pnTINO" runat="server" visible="false">
                        <div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <asp:Label ID="lblSettlementBank" runat="server" Text='<%$ Resources:labels, settlementbank %>'></asp:Label>
                                </div>
                                <div class="col-xs-7 col-sm-4">
                                    <asp:TextBox ID="txtSettlementBank" runat="server" MaxLength="15" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-xs-12 col-sm-4 right">
                                    <asp:Panel ID="pnTemplate" Visible="False" runat="server">
                                        <label>Download standard template of employee list with corresponding tax amount </label>
                                        <a href="TemplateDownload/IRD for PAYE.xls">here</a>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <asp:Label ID="lblName" runat="server" Text='<%$ Resources:labels, name %>'></asp:Label>
                                </div>
                                <div class="col-xs-7 col-sm-4">
                                    <asp:TextBox ID="txtName" runat="server" MaxLength="50" Enabled="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <asp:Label ID="lblAddr" runat="server" Text='<%$ Resources:labels, address %>'></asp:Label>
                                </div>
                                <div class="col-xs-7 col-sm-4">
                                    <asp:TextBox ID="txtAddr" runat="server" Enabled="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <asp:Label ID="lblEmail" runat="server" Text='<%$ Resources:labels, contactemail %>'></asp:Label>
                                </div>
                                <div class="col-xs-7 col-sm-4">
                                    <asp:TextBox ID="txtEmail" runat="server" Enabled="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <asp:Label ID="lblPhone" runat="server" Text='<%$ Resources:labels, contactphone %>'></asp:Label>
                                </div>
                                <div class="col-xs-7 col-sm-4">
                                    <asp:TextBox ID="txtPhone" runat="server" MaxLength="50" Enabled="False" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row" id="trPaymentOption" runat="server" visible="false">
                                <div class="col-xs-5 col-sm-4 right">
                                    <asp:Label ID="lblTaxtypes" runat="server" Text='<%$ Resources:labels, taxtype %>'></asp:Label>&nbsp;*
                                </div>
                                <div class="col-xs-7 col-sm-4">
                                    <asp:DropDownList ID="ddlpaymentOption" CssClass="form-control" runat="server" OnSelectedIndexChanged="paymentOption_OnSelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row" id="trPaymentType" runat="server" visible="false">
                                <div class="col-xs-5 col-sm-4 right">
                                    <asp:Label ID="lblPaymenttypes" runat="server" Text='<%$ Resources:labels, paymenttype %>'></asp:Label>&nbsp;*
                                </div>
                                <div class="col-xs-7 col-sm-4">
                                    <asp:HiddenField ID="hdHasPaymentType" Value="0" runat="server" />
                                    <asp:DropDownList ID="ddlpaymenttype" runat="server" OnSelectedIndexChanged="paymenttype_OnSelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row" id="trTaxPeriod" runat="server" visible="false">
                                <div class="col-xs-5 col-sm-4 right">
                                    <asp:Label ID="lblTaxPeriods" runat="server" Text='<%$ Resources:labels, taxperiod %>'></asp:Label>&nbsp;*
                                </div>
                                <div class="col-xs-7 col-sm-4">
                                    <asp:HiddenField ID="hdHasTaxPeriod" Value="0" runat="server" />
                                    <asp:DropDownList ID="ddlTaxPeriod" runat="server" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row" id="trIncomeYear" runat="server" visible="false">
                                <div class="col-xs-5 col-sm-4 right">
                                    <asp:Label ID="lblIncomeYear" runat="server" Text='<%$ Resources:labels, incomeyear %>'></asp:Label>&nbsp;*
                                </div>
                                <div class="col-xs-7 col-sm-4">
                                    <asp:DropDownList ID="ddlpaymentyear" runat="server" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row" id="trUploadFile" runat="server" visible="false">
                                <div class="col-xs-5 col-sm-4 right">
                                    <asp:Label ID="Label11" runat="server" Text="Bulk Employee Income Tax file *"></asp:Label>
                                </div>
                                <div class="col-xs-7 col-sm-4">
                                    <asp:FileUpload ID="fuTransfer" onchange="CheckExt(this)" runat="server" />
                                    <asp:HiddenField ID="hdFilePathUpload" Value="" runat="server" />
                                    <asp:HiddenField ID="hdFileNameUpload" Value="" runat="server" />
                                    <asp:HiddenField ID="hdIsPAYE" Value="0" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="trNote" runat="server" visible="false">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="Label15" runat="server" Text="<%$ Resources:labels, ghichu %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-8 right">
                            နေလည် ၁၂:၁၅ နောက်ပိုင်း အခွန်ပေးဆောင်သောလုပ်ဆောင်ချက်များသည် နောက်တစ်နေ့မှသာ အကျုံးဝင်မည်ဖြစ်ပါသည်။
                        </div>
                    </div>
                </div>
                <div class="row" style="text-align: center; padding-top: 10px;">
                    <asp:Button ID="btnCheckTIN" runat="server" CssClass="btn btn-primary" Text='<%$ Resources:labels, tieptuc %>' OnClick="btnCheckTIN_OnClick" OnClientClick="return Validate3();" />
                    <asp:Button ID="btnNext1" runat="server" CssClass="btn btn-primary" Text='<%$ Resources:labels, tieptuc %>' Visible="False" OnClick="btnP1Next_Click" />
                </div>
            </figure>
        </asp:Panel>

        <asp:Panel ID="pnAmount" runat="server" class="divcontent">
            <figure>
                <legend class="handle"><%=Resources.labels.chitietgiaodich %> 
                </legend>
            </figure>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label2" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblDebitAccountP2" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row" runat="server" visible="false">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label19" runat="server" Text='<%$ Resources:labels, lasttransactiondate %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblLastTranDateP2" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label6" runat="server" Text='<%$ Resources:labels, availablebalance %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblAvailableBalP2" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblAvailableBalCCYIDP2" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="divcontent">
                <div class="handle">
                    <%=Resources.labels.content %>
                </div>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="Label7" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-4">
                        <asp:TextBox ID="txtAmount" autocomplete="off" runat="server" Text="" MaxLength="15" CssClass="amount"></asp:TextBox>
                        <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                    </div>
                    <div class="col-xs-7 col-sm-4">
                        <asp:Label ID="lblText" runat="server" Font-Size="7pt" Font-Bold="True" Font-Italic="True" ForeColor="#0066FF" Width="200px"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="Label17" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtDesc" runat="server" Height="50px" TextMode="MultiLine" Width="300px" onkeyup="ValidateLimit(this,140);" onkeyDown="ValidateLimit(this,140);" onpaste="ValidateLimit(this,140);"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                    </div>
                    <div class="col-xs-7 col-sm-4">
                        <asp:Label ID="lblNote" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666" Text='<%$ Resources:labels, ghichunoidung %>'></asp:Label>
                    </div>
                </div>
                <div class="row">
                        <div class="row form-group">
                            <div class="col-xs-3 col-sm-4 right">
                                <asp:Label ID="LblDocument" Visible="false" runat="server" Text=""><%= Resources.labels.document %>&nbsp;*</asp:Label>
                            </div>
                            <div class="col-xs-4 col-md-4 col-sm-6 line30">
                                <asp:FileUpload ID="FUDocument" Visible="false" runat="server" ClientIDMode="Static" accept=".pdf,.png,.jpg,.jpeg" />
                            </div>
                            <div class="col-xs-5 col-md-4 col-sm-6 line30">
                                <asp:Label ID="LblDocumentExpalainion" Visible="false" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666" Text='<%$ Resources:labels, uploadlimit1MB %>'></asp:Label>
                            </div>
                        </div>
                    </div>
            </div>
            <div class="row" style="text-align: center; padding-top: 10px;">
                <asp:Button ID="btnBackP2" runat="server" OnClick="btnBackP2_Click" Text="<%$ Resources:labels, quaylai %>" CssClass="btn btn-warning" />
                <asp:Button ID="btnNextP2" runat="server" OnClientClick="return validate7();" Text='<%$ Resources:labels, tieptuc %>' CssClass="btn btn-primary" OnClick="btnNextP2_Click" />
            </div>
        </asp:Panel>

        <asp:Panel ID="pnConfirm" runat="server" class="divcontent">
            <figure>
                <legend class="handle"><%=Resources.labels.chitietgiaodich %> 
                </legend>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label8" runat="server" Text='<%$ Resources:labels, hotennguoitratien %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label12" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblSenderAccount" runat="server"></asp:Label>
                            <asp:Label ID="lblSenderBranch" runat="server" Visible="False"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label13" runat="server" Text='<%$ Resources:labels, sodutruockhighino %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblBalanceSender" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblSenderCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="handle">
                    <%=Resources.labels.thongtinthanhtoan %>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label16" runat="server" Text='<%$ Resources:labels, taxpayeridentificationnumber %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblTaxNo" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label20" runat="server" Text='<%$ Resources:labels, settlementbank %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblSettlementbank2" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label23" runat="server" Text='<%$ Resources:labels, name %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblName2" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label18" runat="server" Text='<%$ Resources:labels, address %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblAddress" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label21" runat="server" Text='<%$ Resources:labels, contactemail %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblEmail2" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label22" runat="server" Text='<%$ Resources:labels, contactphone %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblPhone2" runat="server"></asp:Label>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label27" runat="server" Text='<%$ Resources:labels, taxtype %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblTaxtype" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row" id="trPaymentType2" runat="server" visible="False">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label29" runat="server" Text='<%$ Resources:labels, paymenttype %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblPaymenttype" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row" id="trTaxPeriod2" runat="server" visible="False">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label25" runat="server" Text='<%$ Resources:labels, taxperiod %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblTaxperiod2" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label28" runat="server" Text='<%$ Resources:labels, incomeyear %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblIncomeyear2" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="handle">
                    <%=Resources.labels.content %>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label33" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lbCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label36" runat="server" Text='<%$ Resources:labels, nguoitraphi %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblPhi" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label38" runat="server" Text='<%$ Resources:labels, sotienphi %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblPhiAmount" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblFeeCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label35" runat="server" Text="<%$ Resources:labels, noidungthanhtoan %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblDesc" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row" style="text-align: center; padding-top: 10px;">
                    <asp:Button ID="btnBackTransfer" runat="server" OnClick="btnBackTransfer_Click" Text="<%$ Resources:labels, quaylai %>" CssClass="btn btn-warning" />
                    <asp:Button ID="btnApply" runat="server" OnClientClick="return validate();" Text='<%$ Resources:labels, xacnhan %>' CssClass="btn btn-primary"
                        OnClick="btnApply_Click" />
                </div>
            </figure>
        </asp:Panel>

        <asp:Panel ID="pnOTP" runat="server" class="divcontent">
            <div class="handle">
                <%=Resources.labels.xacthucgiaodich %>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-md-4 right">
                        <asp:Label ID="Label58" runat="server" Text="<%$ Resources:labels, loaixacthuc %>"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-md-4 line30">
                        <asp:DropDownList ID="ddlLoaiXacThuc" runat="server" Height="22px" Width="128px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListOTP_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-2 col-sm-4 left">
                        <asp:Button ID="btnSendOTP" runat="server" OnClick="btnSendOTP_Click" Text="Send" Visible="False" class="btn btn-primary"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-4 right">
                        <asp:Label ID="Label59" runat="server" Text="<%$ Resources:labels, maxacthuc %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-4 line30">
                        <asp:TextBox ID="txtOTP" runat="server" AutoCompleteType="None"></asp:TextBox>
                        <input type="text" style="display: none;" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-md-12" style="text-align: center">
                        <img src="Images/otp.png" style="width: 100px; align-items: center" />
                    </div>
                </div>
            </div>
            <div class="row" style="text-align: center; padding-top: 10px;">
                <asp:Button ID="btnBackOTP" runat="server" OnClick="btnBackOTP_Click" Text="<%$ Resources:labels, quaylai %>" CssClass="btn btn-warning" />
                <asp:Button ID="btnAction" CssClass="btn btn-primary" runat="server" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="btnAction_Click" Text='<%$ Resources:labels, thuchien %>' />
            </div>
        </asp:Panel>

        <asp:Panel ID="pnResultTransaction" runat="server" class="divcontent">
            <figure>
                <legend class="handle"><%=Resources.labels.ketquagiaodich %> 
                </legend>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, hotennguoitratien %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblEndSenderName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label14" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblEndSenderAccount" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label30" runat="server" Text='<%$ Resources:labels, sodusaukhighino %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblEndBalanceSender" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblBalanceCCYID" runat="server"></asp:Label>
                        </div>
                    </div>

                </div>
                <div class="handle">
                    <%=Resources.labels.thongtinthanhtoan %>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label10" runat="server" Text='<%$ Resources:labels, taxpayeridentificationnumber %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblTaxno3" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label26" runat="server" Text='<%$ Resources:labels, settlementbank %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblSettlementbank3" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label32" runat="server" Text='<%$ Resources:labels, name %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblName3" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label24" runat="server" Text='<%$ Resources:labels, address %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblAddress3" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label31" runat="server" Text='<%$ Resources:labels, contactemail %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblEmail3" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label34" runat="server" Text='<%$ Resources:labels, contactphone %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblPhone3" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label41" runat="server" Text='<%$ Resources:labels, taxtype %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblTaxtype3" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row" id="trPaymentType3" runat="server" visible="False">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label43" runat="server" Text='<%$ Resources:labels, paymenttype %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblPaymentType3" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row" id="trTaxPeriod3" runat="server" visible="False">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label45" runat="server" Text='<%$ Resources:labels, taxperiod %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblTaxperiod3" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row" id="Div1" runat="server" visible="False">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label46" runat="server" Text='<%$ Resources:labels, incomeyear %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblIncomeyear3" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="handle">
                    <%=Resources.labels.noidungchuyenkhoan %>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label47" runat="server" Text='<%$ Resources:labels, sogiaodich %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblEndTransactionNo" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label49" runat="server" Text='<%$ Resources:labels, thoidiem %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblEndDateTime" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label51" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblEndAmount" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblAmountCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label53" runat="server" Text='<%$ Resources:labels, nguoitraphi %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblEndPhi" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label48" runat="server" Text='<%$ Resources:labels, sotienphi %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblEndPhiAmount" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblEndFeeCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-6 right">
                            <asp:Label ID="Label55" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblEndDesc" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row" style="text-align: center; padding-top: 10px;">
                    <asp:Button ID="btnView" runat="server" OnClientClick="javascript:return poponloadview()" Text="<%$ Resources:labels, viewphieuin %>" OnClick="btnView_Click" CssClass="btn btn-primary" />
                    <asp:Button ID="btnPrint" runat="server" OnClientClick="javascript:return poponload()" CssClass="btn btn-warning" Text="<%$ Resources:labels, inketqua %>" OnClick="btnPrint_Click" />
                    <asp:Button ID="btnNew" runat="server" Text='<%$ Resources:labels, lammoi %>' OnClick="btnNew_Click" CssClass="btn btn-success" />
                </div>
            </figure>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    function isNumberKey(evt) {//
        var charCode = (evt.which) ? evt.which : event.keyCode;

        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            if (charCode == 127) return true;
            else return false;
        }
        return true;
    }

    function Validate3() {
        var tinno = document.getElementById('<%=txtTIN.ClientID %>').value;
        if (tinno.length == 0) {
            alert("Please enter TIN.");
            return false;
        }
        else {
            if (tinno.length < 9 || tinno.length > 15) {
                alert("TIN must be at least 9 digits and maximum is 15.");
                return false;
            }
        }
        return true;
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
    function validateMoney7(id, msg) {
        if (document.getElementById(id).value == "0" || document.getElementById(id).value == "") {
            window.alert(msg);
            return false;
        }
        else {
            return true;
        }
    }
    function validate7() {
        if (validateMoney7('<%=txtAmount.ClientID %>', '<%=Resources.labels.bancannhapsotien %>')) {
        }
        else {
            document.getElementById('<%=txtAmount.ClientID %>').focus();
            return false;
        }
    }
    function poponload() {
        testwindow = window.open("widgets/IBTaxPayment/print.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }

    function poponloadview() {
        testwindow = window.open("widgets/IBTaxPayment/viewprint.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
    function ValidateLimit(obj, maxchar) {
        if (this.id) obj = this;
        replaceSQLChar(obj);
        var remaningChar = maxchar - obj.value.length;

        if (remaningChar <= 0) {
            obj.value = obj.value.substring(maxchar, 0);
        }
    }
    var validFiles = ["xls", "xlsx"];
    function CheckExt(obj) {
        var source = obj.value;
        var ext = source.substring(source.lastIndexOf(".") + 1, source.length).toLowerCase();
        for (var i = 0; i < validFiles.length; i++) {
            if (validFiles[i] == ext)
                break;
        }
        if (i >= validFiles.length) {
            obj.value = "";
            alert("Invalid Data format in Uploading Excel, Please Check and Refill the corrected data in Excel");
        }
    }


</script>
