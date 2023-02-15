<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="widgets_IBBillPayment2_widget_ascx" %>

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
    <span><%=Resources.labels.thanhtoanhoadon %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label runat="server" ID="lblError" Font-Bold="true" ForeColor="Red"></asp:Label>
            <asp:HiddenField ID="hdTranCode" runat="server" />
            <asp:HiddenField ID="hdIDREF" runat="server" />
            <asp:HiddenField ID="hdRefVal01" runat="server" />
            <asp:HiddenField ID="hdRefVal02" runat="server" />
            <asp:HiddenField ID="hdRefVal03" runat="server" />
            <asp:HiddenField ID="hdRefVal04" runat="server" />
            <asp:HiddenField ID="hdRefVal05" runat="server" />
            <asp:HiddenField ID="hdPhoneNo" runat="server" />
            <asp:HiddenField ID="hdFeeSender" runat="server" />
            <asp:HiddenField ID="hdFeeReceiver" runat="server" />
            <asp:HiddenField ID="hdActTypeSender" runat="server" />
            <asp:HiddenField ID="hdBILLTYPE" runat="server" />
        </div>
        <asp:Panel ID="pnBill" runat="server" class="divcontent">
            <%--sender information--%>
            <div class="handle">
                <asp:Label runat="server" Text='<%$ Resources:labels, thongtintaikhoanchuyen %>'></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-4 col-sm-6 line30">
                        <asp:DropDownList ID="ddlSenderAccount" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:HiddenField ID="txtChu" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.availablebalance %></span>
                    </div>
                    <div class="col-xs-7 col-md-4 col-sm-6 line30">
                        <asp:Label ID="lblAvailableBal" runat="server"></asp:Label>&nbsp;
                                <asp:Label ID="lblAvailableBalCCYID" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <span><%=Resources.labels.thongtinthanhtoan %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.billertype %></span>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:DropDownList ID="ddlBillType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBillType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="lblddlRefVal01" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:DropDownList ID="ddlRefVal01" Visible="False" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="lbltxtRefVal01" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtRefVal01" Visible="False" runat="server" MaxLength="100"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="lblddlRefVal02" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:DropDownList ID="ddlRefVal02" Visible="False" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="lbltxtRefVal02" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtRefVal02" Visible="False" runat="server" MaxLength="100"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="lblddlRefVal03" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:DropDownList ID="ddlRefVal03" Visible="False" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="lbltxtRefVal03" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtRefVal03" Visible="False" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="lblddlRefVal04" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:DropDownList ID="ddlRefVal04" Visible="False" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="lbltxtRefVal04" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtRefVal04" Visible="False" runat="server" MaxLength="100"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="lblddlRefVal05" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:DropDownList ID="ddlRefVal05" Visible="False" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="lbltxtRefVal05" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtRefVal05" Visible="False" runat="server" MaxLength="100"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.noidungthanhtoan %></span>&nbsp;*
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
                <span><%=Resources.labels.thongtinhoadon %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.type %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblBillerCode" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblRef1Verf" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="refval01" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblRef2Verf" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="refval02" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblRef3Verf" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="refval03" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblRef4Verf" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="refval04" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblRef5Verf" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="refval05" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblBill01Name" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblBill01Value" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblBill02Name" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblBill02Value" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblBill03Name" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblBill03Value" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblBill04Name" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblBill04Value" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblBill05Name" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblBill05Value" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <span><%=Resources.labels.thongtinthanhtoan %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.billamout %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lbCCYID" runat="server"></asp:Label>
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
                        <asp:Label ID="lblPhiAmount" Text="0" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblFeeCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.discount %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblDiscountAmount" Text="0" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblDiscountCCYID" runat="server"></asp:Label>
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
                <div class="row" id="divAmount" visible="False" runat="server">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.enterpaymentamount %></span>&nbsp;*
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
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <asp:Label ID="lblWarning" Visible="False" runat="server" ForeColor="#0066FF" Style="padding-top: 2px; line-height: 23px !important;"></asp:Label>
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
                <div class="row" style="text-align:center; color:#7A58BF">
                    <asp:Label ID="lbnotice" runat="server" class="bold" Visible="false">If you do not see the email in your inbox, please check your “junk mail” folder or “spam” folder.</asp:Label>
                </div>
            </div>
            <div class="button-group">
                <asp:Button ID="btnBackConfirm" runat="server" CssClass="btn btn-warning" OnClick="btnBackConfirm_Click" Text="<%$ Resources:labels, quaylai %>" />
                <asp:Button ID="btnAction" runat="server" CssClass="btn btn-primary" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="btnAction_Click" Text="<%$ Resources:labels, thuchien %>" />
            </div>
        </asp:Panel>

        <asp:Panel ID="pnResult" runat="server" class="divcontent" Visible="false">
            <%--sender information--%>
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
                <span><%=Resources.labels.thongtinhoadon %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.type %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblEndBillerCode" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblEndRef1Verf" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="Endrefval01" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblEndRef2Verf" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="Endrefval02" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblEndRef3Verf" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="Endrefval03" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblEndRef4Verf" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="Endrefval04" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblEndRef5Verf" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="Endrefval05" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblEndBill01Name" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblEndBill01Value" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblEndBill02Name" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblEndBill02Value" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblEndBill03Name" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblEndBill03Value" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblEndBill04Name" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblEndBill04Value" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="lblEndBill05Name" Visible="False" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblEndBill05Value" Visible="False" runat="server"></asp:Label>
                    </div>
                </div>



            </div>
            <div class="handle">
                <span><%=Resources.labels.noidungchuyenkhoan %></span>
            </div>
            <div class="content_table">
                <div class="row" id="tranno" runat="server">
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
                        <span><%=Resources.labels.paymentamout %></span>
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
                        <asp:Label ID="lblEndPhiAmount" Text="0" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblEndFeeCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.discount %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndDiscountAmount" Text="0" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblEndDiscountCCYID" runat="server"></asp:Label>
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

<script>
    function ValidateLimit(obj, maxchar) {
        if (this.id) obj = this;
        replaceSQLChar(obj);
        var remaningChar = maxchar - obj.value.length;

        if (remaningChar <= 0) {
            obj.value = obj.value.substring(maxchar, 0);
        }
    }
    function ntt(sNumber, idDisplay, event) {
        executeComma(sNumber, event);
        if (document.getElementById(sNumber).value == "") {
            document.getElementById(idDisplay).innerHTML = "";
            return;
        }

        document.getElementById(idDisplay).innerHTML = "(" + number2text(replaceAll(document.getElementById(sNumber).value, ",", ""), '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>') + ")";
    }
    function replaceAll(str, from, to) {
        var idx = str.indexOf(from);


        while (idx > -1) {
            str = str.replace(from, to);
            idx = str.indexOf(from);
        }

        return str;
    }
    function poponload() {
        testwindow = window.open("widgets/IBBillPayment2/print.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }

    function poponloadview() {
        testwindow = window.open("widgets/IBBillPayment2/viewprint.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }

</script>
