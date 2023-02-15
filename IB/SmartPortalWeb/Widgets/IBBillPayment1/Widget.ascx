<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBBillPayment1_Widget" %>

<script src="JS/mask.js" type="text/javascript"></script>

<script src="JS/docso.js" type="text/javascript"></script>
<link href="Widgets/IBBillPayment1/CSS/css.css" rel="stylesheet" />
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
    <span>Bill Payment</span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<asp:UpdatePanel runat="server" ID="upnTIB">
    <ContentTemplate>
        <div style="text-align: center; color: Red;">
            <asp:Label runat="server" ID="lblTextError"></asp:Label>
        </div>

        <asp:Panel ID="pnTIB" runat="server">
            <figure>
                <div class="divcontent">
                    <div class="item1">
                        <div class="handle">
                            <asp:Label runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold "><%= Resources.labels.debitaccount %></label>
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
                                    <asp:DropDownList ID="ddlSenderAccount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RadioButton ID="radTS1" runat="server" Checked="True" GroupName="TIB"
                                        onclick="resetTS();" Text="Chuyển ngay" Visible="False" />
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
                                    &nbsp;
                            <asp:Label ID="lblAvailableBalCCYID" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="item1">
                        <div class="handle">
                            <asp:Label runat="server" Text='<%$ Resources:labels, thongtinthanhtoan %>'></asp:Label>
                        </div>
                        <div class="content_table">

                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold"><%= Resources.labels.corporates %></label>
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
                                    <asp:DropDownList ID="ddlCorpList" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlcorp_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold"><%= Resources.labels.dichvu %></label>
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
                                    <asp:DropDownList ID="ddlservice" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlservice_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <asp:Label ID="lblindexref1" runat="server" CssClass="bold" Text="Ref Field 1 "></asp:Label>
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
                                    <asp:TextBox ID="txtREF1" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:Label ID="lblWSError" runat="server" Style="color: Red;"></asp:Label>
                                    <asp:HiddenField ID="hdfTrancode" runat="server"></asp:HiddenField>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <asp:Label ID="lblindexref2" runat="server" CssClass="bold" Text="Ref Field 2 "></asp:Label>
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
                                    <asp:TextBox ID="txtREF2" runat="server" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <asp:Label ID="lblindexref3" runat="server" CssClass="bold" Text="Ref Field 3" Visible="false"></asp:Label>
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
                                    <asp:TextBox ID="txtREF3" runat="server" MaxLength="50" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold">Min amount</label>
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
                                    <asp:Label ID="lblMinAmount" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold">Max amount</label>
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
                                    <asp:Label ID="lblMaxAmount" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <asp:Label ID="lblNoteTipHeader" CssClass="bold" runat="server" Text="Note"></asp:Label>
                                </div>
                                <div class=" col-xs-7 col-sm-4 line30">
                                    <asp:Label ID="lblNoteTip" runat="server"></asp:Label>
                                </div>
                            </div>
                            <asp:UpdateProgress ID="uprgWorkRequest" AssociatedUpdatePanelID="upnTIB" runat="server">
                                <ProgressTemplate>
                                    <div class="row loading-group">
                                        <img alt="" src="/App_Themes/InternetBanking/images/loading.gif" style="width: 16px; height: 16px;" />

                                        <label class="bold" style="color: orangered"><%= Resources.labels.loading %></label>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </div>
                    <div style="text-align: center; margin-top: 10px;">
                        <asp:Button ID="btnP1Next" runat="server" OnClientClick="return validateref();" Text='<%$ Resources:labels, tieptuc %>'
                            OnClick="btnP1Next_Click" CssClass="btn btn-primary" />
                    </div>

                </div>
            </figure>
        </asp:Panel>
        <!--end-->
        <!--confirm-->
        <asp:Panel ID="pnAmount" runat="server">
            <figure>
                <div class="divcontent">
                    <asp:HiddenField ID="txtChu" runat="server" />
                    <div class="item1">
                        <div class="handle">
                            <asp:Label runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold "><%= Resources.labels.debitaccount %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblDebitAccountP2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold "><%= Resources.labels.lasttransactiondate %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblLastTranDateP2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right">
                                    <label class="bold "><%= Resources.labels.availablebalance %></label>
                                </div>
                                <div class="col-xs-7 col-sm-6 line30">
                                    <asp:Label ID="lblAvailableBalP2" runat="server"></asp:Label>
                                    <asp:Label ID="lblAvailableBalCCYIDP2" Text="LAK" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="item1">
                        <div class="handle">
                            <asp:Label runat="server" Text='<%$ Resources:labels, content %>'></asp:Label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <asp:Label ID="Label2" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>&nbsp;*
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
                                    <asp:TextBox ID="txtAmount" autocomplete="off" runat="server" Text="" MaxLength="15" CssClass="amount"></asp:TextBox>
                                    <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                                    <div>
                                        <asp:Label ID="lblText" runat="server" Font-Size="7pt" Font-Bold="True" Font-Italic="True"
                                            ForeColor="#0066FF" Style="padding-top: 2px; line-height: 23px !important;"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4 right">
                                    <label class="bold">
                                        <%= Resources.labels.noidungthanhtoan %></label>

                                    <asp:Label ID="Label25" runat="server" Text='<%$ Resources:labels, nguoitraphi %>'
                                        Visible="false">*</asp:Label>&nbsp;
                               <asp:RadioButtonList ID="rdNguoiChiuPhi" runat="server" Visible="false" RepeatDirection="Horizontal">
                                   <asp:ListItem Selected="True" Text="<%$ Resources:labels, nguoigui %>"></asp:ListItem>
                                   <asp:ListItem Text="<%$ Resources:labels, nguoinhan %>"></asp:ListItem>
                               </asp:RadioButtonList>
                                </div>
                                <div class="col-xs-7 col-sm-4 line30">
                                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" CssClass="form-control" onkeyup="ValidateLimit(this,200);" onkeyDown="ValidateLimit(this,200);" onpaste="ValidateLimit(this,200);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-4"></div>
                                <div class="col-xs-7 col-sm-4">
                                    <asp:Label ID="lblNote" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666"
                                        Text='<%$ Resources:labels, ghichunoidung %>'></asp:Label>
                                </div>
                            </div>
                            <asp:UpdateProgress ID="uprgWorkAmount" AssociatedUpdatePanelID="upnTIB" runat="server">
                                <ProgressTemplate>
                                    <div class="row loading-group">
                                        <img alt="" src="/App_Themes/InternetBanking/images/loading.gif" style="width: 16px; height: 16px;" />

                                        <label class="bold" style="color: orangered"><%= Resources.labels.loading %></label>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>

                        </div>
                    </div>
                    <div class="row" style="text-align: center; margin-top: 10px;">
                        <asp:Button ID="btnBackP2" runat="server" CssClass="btn btn-warning" OnClick="btnBackP2_Click" Text="<%$ Resources:labels, quaylai %>" />
                        <asp:Button ID="btnNextP2" runat="server" CssClass="btn btn-primary" OnClientClick="return validate2();" Text='<%$ Resources:labels, tieptuc %>'
                            OnClick="btnNextP2_Click" />
                        <div class="clearfix"></div>

                    </div>
                </div>
            </figure>

        </asp:Panel>
        <asp:Panel ID="pnConfirm" runat="server">
            <figure>
                <div class="divcontent">
                    <div class="item">

                        <div class="handle">
                            <label><%= Resources.labels.thongtinnguoitratien %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.hotennguoitratien %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblSenderName" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.debitaccount %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblSenderAccount" runat="server"></asp:Label>
                                    <asp:Label ID="lblSenderBranch" runat="server" Visible="False"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.sodutruockhighino %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblBalanceSender" runat="server"></asp:Label>
                                    &nbsp;<asp:Label ID="lblSenderCCYID" runat="server"></asp:Label>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="item">
                        <div class="handle">
                            <label><%= Resources.labels.thongtinthanhtoan %></label>
                        </div>

                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.corporates %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblCorpName" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.dichvu %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblService" runat="server"></asp:Label>
                                    <asp:Label ID="lblReceiverBranch" runat="server" Visible="False"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <asp:Label ID="lblcindexref1" Font-Bold="true" runat="server" Text="Ref Field 1 "></asp:Label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblcvalueref1" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <asp:Label ID="lblcindexref2" Font-Bold="true" runat="server" Text="Ref Field 2 "></asp:Label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblcvalueref2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <asp:Label ID="lblcindexref3" Font-Bold="true" runat="server" Text="Ref Field 3 "></asp:Label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblcvalueref3" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="item">
                        <div class="handle">
                            <label><%= Resources.labels.content %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.sotien %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                    &nbsp;<asp:Label ID="lbCCYID" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.nguoitraphi %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblPhi" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.sotienphi %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblPhiAmount" runat="server"></asp:Label>
                                    &nbsp;<asp:Label ID="lblFeeCCYID" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.noidungthanhtoan %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="button-group" style="text-align: center; margin-top: 10px;">
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
                <div class="divcontent">
                    <div class="handle">
                        <label><%= Resources.labels.xacthucgiaodich %></label>
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
                    <div class="row" style="text-align: center; margin-top: 10px;">
                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-warning" OnClick="btnBack_Click" Text="<%$ Resources:labels, quaylai %>" />
                        <asp:Button ID="btnAction" CssClass="btn btn-primary" runat="server" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="btnAction_Click" Text="<%$ Resources:labels, thuchien %>" />
                        <div class="clearfix"></div>
                    </div>
                </div>
            </figure>
        </asp:Panel>
        <!--end-->
        <!--sao ke-->
        <asp:Panel ID="pnResultTransaction" runat="server">
            <figure>
                <div class="divcontent">
                    <div class="item">
                        <div class="handle">
                            <label><%= Resources.labels.thongtinnguoitratien %></label>
                        </div>

                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.hotennguoitratien %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblEndSenderName" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.debitaccount %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblEndSenderAccount" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.sodusaukhighino %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblEndBalanceSender" runat="server"></asp:Label>
                                    <asp:Label ID="lblBalanceCCYID" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="item">
                        <div class="handle">
                            <label><%= Resources.labels.thongtinthanhtoan %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.corporates %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblscorp" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.dichvu %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblsservice" runat="server"></asp:Label>
                                    <asp:Label ID="Label27" runat="server" Visible="False"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <asp:Label ID="lblsindexref1" runat="server" Text="Ref Field 1 "></asp:Label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblsvalueref1" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <asp:Label ID="lblsindexref2" runat="server" Text="Ref Field 2 "></asp:Label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblsvalueref2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <asp:Label ID="lblsindexref3" runat="server" Text="Ref Field 3 "></asp:Label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblsvalueref3" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="item">
                        <div class="handle">
                            <label><%= Resources.labels.noidungchuyenkhoan %></label>
                        </div>
                        <div class="content_table">
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.sogiaodich %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblEndTransactionNo" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.thoidiem %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblEndDateTime" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.sotien %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblEndAmount" runat="server"></asp:Label>
                                    &nbsp;<asp:Label ID="lblAmountCCYID" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.nguoitraphi %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblEndPhi" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.sotienphi %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblEndPhiAmount" runat="server"></asp:Label>
                                    &nbsp;<asp:Label ID="lblEndFeeCCYID" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-5 col-sm-6 right bold">
                                    <label><%= Resources.labels.noidungthanhtoan %></label>
                                </div>
                                <div class="col-xs-7  col-sm-6 line30">
                                    <asp:Label ID="lblEndDesc" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="text-align: center; margin-top: 10px;">
                        <asp:Button ID="btnView" CssClass="btn btn-primary" runat="server" OnClientClick="javascript:return poponloadview()"
                            Text="<%$ Resources:labels, viewphieuin %>" OnClick="btnView_Click" />

                        <asp:Button ID="btnPrint" CssClass="btn btn-warning" runat="server" OnClientClick="javascript:return poponload()"
                            Text="<%$ Resources:labels, inketqua %>" OnClick="btnPrint_Click" />

                        <asp:Button ID="btnNew" CssClass="btn btn-success" runat="server" Text='<%$ Resources:labels, lammoi %>' OnClick="btnNew_Click" />

                        <div class="clearfix"></div>
                    </div>
                </div>
            </figure>
        </asp:Panel>
        <!--end-->

        <script type="text/javascript">
            function poponload() {
                testwindow = window.open("widgets/IBBillPayment1/print.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
                    "menubar=1,scrollbars=1,width=500,height=650");
                testwindow.moveTo(0, 0);
                return false;
            }
            function poponloadview() {
                testwindow = window.open("widgets/IBBillPayment1/viewprint.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
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
            function validate2() {
                if (validateMoney2('<%=txtAmount.ClientID %>', '<%=Resources.labels.bancannhapsotien %>')) {
                    if (validateEmpty2('<%=txtDesc.ClientID %>', '<%=Resources.labels.bancannhapnoidung %>')) {
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
            function validateref() {
                if (validateEmpty2('<%=txtREF1.ClientID %>', 'You need provide ' + lblindexref1.value)) {
                    if (validateEmpty2('<%=txtREF2.ClientID %>', 'You need provide ' + lblindexref2.value)) {
                    }
                    else {
                        document.getElementById('<%=txtREF2.ClientID %>').focus();
                        return false;
                    }

                }
                else {
                    document.getElementById('<%=txtREF1.ClientID %>').focus();
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
            function validateEmpty2(id, msg) {
                if (document.getElementById(id).value == "") {
                    window.alert(msg);
                    return false;
                }
                else {
                    return true;
                }
            }
            function validateMoney2(id, msg) {
                if (document.getElementById(id).value == "0" || document.getElementById(id).value == "") {
                    window.alert(msg);
                    return false;
                }
                else {
                    return true;
                }
            }
        </script>

    </ContentTemplate>
</asp:UpdatePanel>
