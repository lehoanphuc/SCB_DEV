<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTransferInBank1_Widget" %>


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
    <span><%=Resources.labels.chuyenkhoantronghethong %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="text-align: center; color: Red; font-weight: bold">
            <asp:Label runat="server" ID="lblTextError"></asp:Label>
        </div>
        <div>
            <asp:HiddenField runat="server" ID="hdsendeAccount" />
            <asp:HiddenField runat="server" ID="hdReceiverAccount" />
            <asp:HiddenField runat="server" ID="hdActTypeSender" />
            <asp:HiddenField runat="server" ID="hdActTypeReceiver" />
            <asp:HiddenField runat="server" ID="hdSenderName" />
            <asp:HiddenField runat="server" ID="hdBalanceSender" />
            <asp:HiddenField runat="server" ID="hdBranchSender" />
            <asp:HiddenField runat="server" ID="hdSenderCCYID" />
            <asp:HiddenField runat="server" ID="hdReceiverName" />
            <asp:HiddenField runat="server" ID="hdBalanceReceiver" />
            <asp:HiddenField runat="server" ID="hdBranchReceiver" />
            <asp:HiddenField runat="server" ID="hdReceiverCCYID" />
            <asp:HiddenField runat="server" ID="hdTrancode" />
        </div>
        <asp:Panel ID="pnTIB" runat="server" class="divcontent">
            <asp:UpdatePanel ID="upTIB" runat="server">
                <ContentTemplate>
                    <%--sender information--%>
                    <div class="handle">
                        <asp:Label runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
                    </div>
                    <div class="content_table">
                        <div class="row">
                            <div class="col-xs-5 col-sm-4 right">
                                <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>&nbsp;*
                            </div>
                            <div class="col-xs-7 col-md-4 col-sm-6 line30">
                                <asp:DropDownList ID="ddlSenderAccount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged">
                                </asp:DropDownList><asp:RadioButton ID="radTS1" runat="server" Checked="True" GroupName="TIB"
                                    onclick="resetTS();" Text="Chuyển ngay" Visible="False" />
                                <asp:HiddenField ID="txtChu" runat="server" />
                            </div>
                        </div>
                        <div class="row" style="display: none">
                            <div class="col-xs-5 col-sm-4 right">
                                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, lasttransactiondate %>"></asp:Label>
                            </div>
                            <div class="col-xs-7 col-md-4 col-sm-6 line30">
                                <asp:Label ID="lblLastTranDate" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-4 right">
                                <asp:Label ID="Label35" runat="server" Text="<%$ Resources:labels, availablebalance %>"></asp:Label>
                            </div>
                            <div class="col-xs-7 col-md-4 col-sm-6 line30">
                                <asp:Label ID="lblAvailableBal" runat="server"></asp:Label>
                                <asp:Label ID="lblAvailableBalCCYID" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <%--receiver information--%>
                    <div class="handle">
                        <asp:Label ID="Label3" runat="server" Text='<%$ Resources:labels, thongtinnguoinhantien %>'></asp:Label>
                    </div>
                    <div class="content_table">
                        <div class="row">
                            <div class="col-xs-5 col-sm-4 right">
                                <asp:Label ID="Label2" runat="server" Text='<%$ Resources:labels, nguoithuhuong %>'></asp:Label>
                            </div>
                            <div class="col-xs-7 col-md-4 col-sm-6 line30">
                                <asp:DropDownList ID="ddlNguoiThuHuong" runat="server" OnSelectedIndexChanged="ddlNguoiThuHuong_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-4 right">
                                <asp:Label ID="Label17" runat="server" Text='<%$ Resources:labels, taikhoanbaoco %>'></asp:Label>&nbsp;*
                            </div>
                            <div class="col-xs-7 col-md-4 col-sm-6 line30">
                                <asp:TextBox ID="txtReceiverAccount" runat="server" OnTextChanged="OnReceiverAccountChanged" AutoPostBack="true"></asp:TextBox>
								<span id="ctl00_ctl24_LabelReceiverCCY" style="position: absolute;   margin-left: 3px;"><asp:Label ID="LabelReceiverCCY" runat="server" Text="" Visible="false"></asp:Label></span>
                                <asp:HiddenField ID="hdflimit" runat="server"></asp:HiddenField>
                                <asp:RadioButton ID="radTS" runat="server" GroupName="TIB" onclick="enableTS();"
                                    Text="<%$ Resources:labels, chuyenvaongayddmmyyyy %>" Visible="False" />
                                <asp:TextBox ID="txtTS" runat="server" Visible="False"></asp:TextBox>
                            </div>
                                   
                        </div>

                        <div class="row">
                            <div class="col-xs-5 col-sm-4 right">
                                <asp:Label ID="LabelReceiver" runat="server" Text='<%$ Resources:labels, hotennguoinhantien %>'></asp:Label>&nbsp;
                            </div>
                            <div class="col-xs-7 col-md-4 col-sm-6 line30">                            
                                    <asp:Label ID="LabelReceiverName" runat="server" Text="" Visible="false"></asp:Label>                              
                        </div>
                        </div>                             
                    </div>
                    <%--transaction detail--%>
                    <div class="handle">
                        <asp:Label ID="Label5" runat="server" Text='<%$ Resources:labels, noidungchuyenkhoan %>'></asp:Label>
                    </div>
                    <div class="content_table">
                        <div class="row">
                            <div class="col-xs-5 col-sm-4 right">
                                <asp:Label ID="Label6" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>&nbsp;*
                            </div>
                            <div class="col-xs-7 col-md-4 col-sm-6 line30">
                                <asp:TextBox ID="txtAmount" autocomplete="off" runat="server" Text="" MaxLength="15" CssClass="amount"></asp:TextBox>
                                <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                                <div style="text-align: left">
                                    <asp:Label ID="lblText" runat="server" Font-Size="7pt" Font-Bold="True" Font-Italic="True"
                                        ForeColor="#0066FF" Style="padding-top: 2px; line-height: 23px !important;"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-4 right">
                                <asp:Label ID="Label7" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>&nbsp;*
                                <asp:Label ID="Label44" runat="server" Text='<%$ Resources:labels, nguoitraphi %>'
                                    Visible="false">*</asp:Label>&nbsp;
                                <asp:RadioButtonList ID="rdNguoiChiuPhi" runat="server" Visible="false" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Text="<%$ Resources:labels, nguoigui %>"></asp:ListItem>
                                    <asp:ListItem Text="<%$ Resources:labels, nguoinhan %>"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-xs-7 col-md-4 col-sm-6 line30">
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
                                <asp:FileUpload ID="FUDocument" Visible="false" runat="server" ClientIDMode="Static" accept=".pdf,.png,.jpg,.jpeg" />
                            </div>
                            <div class="col-xs-5 col-md-4 col-sm-6 line30">
                                <asp:Label ID="LblDocumentExpalainion" Visible="false" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666" Text='<%$ Resources:labels, uploadlimit1MB %>'></asp:Label>
                            </div>
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--save template--%>
            <div class="handle">
                <asp:Label ID="Label9" runat="server" Text='<%$ Resources:labels, luunoidungchuyenkhoannaythanhmau %>'></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="Label15" runat="server" Text='<%$ Resources:labels, luuthanhmau %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-4 col-sm-6 line30 checkbox ">
                        <%--checkbox-primary--%>
                        <asp:CheckBox ID="cbmau" runat="server" />
                        <label for="<%=cbmau.ClientID %>">
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="Label19" runat="server" Text='<%$ Resources:labels, dattenchomau %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-4 col-sm-6 line30">
                        <asp:TextBox ID="txttenmau" autocomplete="off" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>

            <!--Button next-->
            <div style="text-align: center; margin-top: 10px;">
                <asp:Button ID="btnTIBNext" runat="server" CssClass="btn btn-primary" OnClientClick="return validate();" Text='<%$ Resources:labels, tieptuc %>'
                    OnClick="btnTIBNext_Click" />
            </div>

        </asp:Panel>
        <!--end-->
        <!--confirm-->
        <asp:Panel ID="pnConfirm" runat="server" class="divcontent">
            <%--sender information--%>
            <div class="handle">
                <asp:Label ID="Label20" runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, hotennguoitratien %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label11" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblSenderAccount" runat="server"></asp:Label>
                        <asp:Label ID="lblSenderBranch" runat="server" Visible="False"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label18" runat="server" Text='<%$ Resources:labels, sodutruockhighino %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblBalanceSender" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblSenderCCYID" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <%--receiver information--%>
            <div class="handle">
                <asp:Label ID="Label21" runat="server" Text='<%$ Resources:labels, thongtinnguoinhantien %>'></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label33" runat="server" Text="<%$ Resources:labels, hotennguoinhantien %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblReceiverName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label12" runat="server" Text="<%$ Resources:labels, taikhoanbaoco %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblReceiverAccount" runat="server"></asp:Label>
                        <asp:Label ID="lblReceiverBranch" runat="server" Visible="False"></asp:Label>
                    </div>
                </div>
            </div>
            <%--transaction information--%>
            <div class="handle">
                <asp:Label ID="Label13" runat="server" Text='<%$ Resources:labels, noidungchuyenkhoan %>'></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label16" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lbCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label48" runat="server" Text='<%$ Resources:labels, nguoitraphi %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblPhi" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label45" runat="server" Text='<%$ Resources:labels, sotienphi %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblPhiAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblFeeCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label22" runat="server" Text="<%$ Resources:labels, noidungthanhtoan %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 col-sm-6 line30">
                        <asp:Label ID="lblDesc" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="button-group">
                <asp:Button ID="btnBackTransfer" runat="server" CssClass="btn btn-warning" OnClick="btnBackTransfer_Click" Text='<%$ Resources:labels, quaylai %>' />

                <asp:Button ID="btnApply" runat="server" CssClass="btn btn-primary" OnClick="btnApply_Click" Text='<%$ Resources:labels, xacnhan %>' />

                <div class="clearfix"></div>
            </div>
        </asp:Panel>
        <!--end-->
        <!--token-->
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
                <asp:Button ID="btnBackConfirm" runat="server" CssClass="btn btn-warning" OnClick="btnBack_Click" Text="<%$ Resources:labels, quaylai %>" />
                <asp:Button ID="btnAction" runat="server" CssClass="btn btn-primary" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="btnAction_Click" Text="<%$ Resources:labels, thuchien %>" />
            </div>
        </asp:Panel>
        <!--end-->
        <!--sao ke-->
        <asp:Panel ID="pnResultTransaction" runat="server" class="divcontent">

            <div class="handle">
                <asp:Label ID="Label23" runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label14" runat="server" Text='<%$ Resources:labels, hotennguoitratien %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndSenderName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label10" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndSenderAccount" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label50" runat="server" Text='<%$ Resources:labels, sodutruockhighino %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
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
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label43" runat="server" Text='<%$ Resources:labels, hotennguoinhantien %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndReceiverName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label27" runat="server" Text='<%$ Resources:labels, taikhoanbaoco %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndReceiverAccount" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <asp:Label ID="Label26" runat="server" Text='<%$ Resources:labels, noidungchuyenkhoan %>'></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label30" runat="server" Text='<%$ Resources:labels, sogiaodich %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndTransactionNo" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label32" runat="server" Text='<%$ Resources:labels, thoidiem %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndDateTime" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label31" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndAmount" runat="server" Text="0"></asp:Label>
                        &nbsp;<asp:Label ID="lblEndAmountCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label41" runat="server" Text='<%$ Resources:labels, nguoitraphi %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndPhi" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label51" runat="server" Text='<%$ Resources:labels, sotienphi %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndPhiAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblEndFeeCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label36" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndDesc" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <!--Button next-->
            <div class="button-group">
                <asp:Button ID="btnView" runat="server" OnClientClick="javascript:return poponloadview()"
                    Text="<%$ Resources:labels, viewphieuin %>" OnClick="btnView_Click" CssClass="btn btn-primary" />

                <asp:Button ID="btnPrint" runat="server" OnClientClick="javascript:return poponload()"
                    Text="<%$ Resources:labels, inketqua %>" OnClick="btnPrint_Click" CssClass="btn btn-warning" />

                <asp:Button ID="btnNew" runat="server" Text='<%$ Resources:labels, lammoi %>' OnClick="btnNew_Click" CssClass="btn btn-success" />

                <div class="clearfix"></div>
            </div>
        </asp:Panel>
        <!--end-->

        <script type="text/javascript">
            function poponload() {
                testwindow = window.open("widgets/IBTransferInBank1/print.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
                    "menubar=1,scrollbars=1,width=500,height=650");
                testwindow.moveTo(0, 0);
                return false;
            }
            function poponloadview() {
                testwindow = window.open("widgets/IBTransferInBank1/viewprint.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
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
                document.getElementById('<%=lblTextError.ClientID%>').innerHTML = '';

                if (document.getElementById("<%= hdflimit.ClientID %>").value != "") {
                    if (confirm(document.getElementById("<%= hdflimit.ClientID %>").value) == true) {

                    }
                    else {
                        return false;
                    }
                }

                if (!validateEmpty('<%=txtReceiverAccount.ClientID %>', '<%=Resources.labels.taikhoandichkhongrong %>')) {
                    return false;
                }

                if (!validateSameAccount('<%=ddlSenderAccount.ClientID%>', '<%=txtReceiverAccount.ClientID%>', '<%=Resources.labels.Accountnotsame%>')) {
                    return false;
                }

                if (!validateMoney('<%=txtAmount.ClientID %>', '<%=Resources.labels.bancannhapsotien %>')) {
                    document.getElementById('<%=txtAmount.ClientID %>').focus();
                    return false;
                }

                if (!validateEmpty('<%=txtDesc.ClientID %>', '<%=Resources.labels.bancannhapnoidung %>') || check($('<%=txtDesc.ClientID %>').val())) {
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

    </ContentTemplate>
	<Triggers>
        <asp:PostBackTrigger ControlID="btnTIBNext" />
    </Triggers>
</asp:UpdatePanel>
