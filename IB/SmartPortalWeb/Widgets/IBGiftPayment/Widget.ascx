<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="widgets_IBGiftPayment_widget_ascx" %>
<script src="JS/mask.js" type="text/javascript"></script>
<script src="JS/docso.js" type="text/javascript"></script>
<style>
    .amount {
        width: 83% !important;
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
    <span><%=Resources.labels.buygiftcard %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="text-align: center; color: Red;">
            <asp:Label ID="lblTextError" runat="server"></asp:Label>
            <asp:HiddenField ID="hdfAcctNo" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfGiftCardType" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfDenominations" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfEquivalentAmount" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfSerialNumber" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfCurrency" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfACCTCCYAMOUNT" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfFEEACCTCCY" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfDISCOUNT" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfACCTCCY" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfAMOUNTBCY" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfDEBITEXCHANGEBCY" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfCREDITEXCHANGEBCY" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfCROSSRATE" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfBranchID" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfORDERID" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfPINCODE" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfBARCODE" runat="server" Visible="false"></asp:HiddenField>
            <asp:HiddenField ID="hdfEXPIREDDATE" runat="server" Visible="false"></asp:HiddenField>
        </div>

        <asp:Panel ID="pnGift2" runat="server" Visible="false" class="divcontent">
            <div class="handle">
                <%=Resources.labels.chitietgiaodich %>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="Label30" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-4 col-sm-6 line30">
                        <asp:DropDownList ID="ddlSenderAccount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:HiddenField ID="txtChu" runat="server" />
                    </div>
                </div>
                <div class="row" style="display: none">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="Label31" runat="server" Text="<%$ Resources:labels, lasttransactiondate %>"></asp:Label>
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
                        &nbsp;<asp:Label ID="lblAvailableBalCCYID" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, giftinformation %>'></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="Label6" runat="server" Text='<%$ Resources:labels, giftcardType %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-4 col-sm-6 line30">
                        <asp:DropDownList ID="ddlGiftCardType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGiftCardType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="Label9" runat="server" Text='<%$ Resources:labels, denominations %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-4 col-sm-6 line30">
                        <asp:DropDownList ID="ddldenom" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnConfim" runat="server" Visible="false" class="divcontent">
            <div class="handle">
                <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, buygiftcard %>'></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, hotennguoitratien %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblAccName" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label7" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblSenderAccount" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label33" runat="server" Text='<%$ Resources:labels, sodutruockhighino %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblBalanceSender" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblSenderCCYID" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="handle">
                <asp:Label ID="Label17" runat="server" Text='<%$ Resources:labels, giftcardinformation %>'></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, type %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblGiftType" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label24" runat="server" Text='<%$ Resources:labels, denominations %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblDenomination" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label34" runat="server" Text='<%$ Resources:labels, EquivalentAmount %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEquivalentAmountConfirm" runat="server" Text=""></asp:Label>
                        &nbsp;<asp:Label ID="lblEquiAmountCurrency" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label8" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblConfirmAmount" runat="server" Text=""></asp:Label>
                        &nbsp;<asp:Label ID="lblConfirmAmountCCY" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label26" runat="server" Text="<%$ Resources:labels, sotienphi %>" Visible="False"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblConfirmFee" runat="server" Text="" Visible="False"></asp:Label>
                        &nbsp;<asp:Label ID="lblConfirmFeeCurrency" runat="server" Visible="False"></asp:Label>
                    </div>
                </div>
            </div>
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
        </asp:Panel>

        <asp:Panel ID="pnFinish" runat="server" Visible="false" class="divcontent">
            <div class="handle">
                <%=Resources.labels.thongtinnguoitratien %>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label11" runat="server" Text='<%$ Resources:labels, hotennguoitratien %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndSenderName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label23" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblendSenderAccount" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label50" runat="server" Text='<%$ Resources:labels, sodusaukhighino %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndBalanceSender" runat="server" Text="0"></asp:Label>&nbsp;
                    <asp:Label ID="lblEndSenderCCYID" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="handle">
                <%=Resources.labels.giftcardinformation %>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label5" runat="server" Text='<%$ Resources:labels, type %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblFinishGiftcardType" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label20" runat="server" Text='<%$ Resources:labels, denominations %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblFinishDenomination" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label28" runat="server" Text='<%$ Resources:labels, EquivalentAmount %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblFinishEquivalentAmount" runat="server" Text=""></asp:Label>&nbsp;
                    <asp:Label ID="lblFininhEquiAmountCurrency" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label10" runat="server" Text='<%$ Resources:labels, giftcardscode %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblFinishGiftCode" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>

            <div class="handle">
                <%=Resources.labels.noidungchuyenkhoan %>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label12" runat="server" Text='<%$ Resources:labels, transref %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblFinishTranRef" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label19" runat="server" Text='<%$ Resources:labels, time %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblFinishTranTime" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label22" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblFinishAmount" runat="server" Text=""></asp:Label>&nbsp;
                    <asp:Label ID="lblFinishAmountCurrency" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row" style="display: none;">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label27" runat="server" Text="<%$ Resources:labels, sotienphi %>" Visible="False"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblFinishFee" runat="server" Text="" Visible="False"></asp:Label>&nbsp;
                    <asp:Label ID="lblFinishFeeCurrency" runat="server" Text="" Visible="False"></asp:Label>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<div style="text-align: center; margin-top: 10px;">
    <asp:Button ID="btnBack" CssClass="btn btn-warning" runat="server" OnClick="btnBack_Click" Text='<%$ Resources:labels, back %>' />
    <asp:Button ID="btnContineAcc" CssClass="btn btn-primary" runat="server" OnClick="btnContineAcc_Click" Text='<%$ Resources:labels, tieptuc %>' />
    <asp:Button ID="btnConfim" CssClass="btn btn-primary" runat="server" OnClick="btnConfim_Click" Text='<%$ Resources:labels, confirm %>' />
    <asp:Button ID="btnAction" CssClass="btn btn-primary" runat="server" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="btnAction_Click" Text='<%$ Resources:labels, thuchien %>' />
</div>
