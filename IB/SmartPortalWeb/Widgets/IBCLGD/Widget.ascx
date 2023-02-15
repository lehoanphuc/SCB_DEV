<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCLGD_Widget" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<link href="CSS/css.css" rel="stylesheet" type="text/css" />

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
    <div style="float: left">
        <span><%=Resources.labels.chuyenkhoantronghethongtheolo %></span><br />
        <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
    </div>
    <div style="text-align: right; float: right;">
        <a href="TemplateDownload/Batch Transfer.xls"><%= Resources.labels.downloadTemplate %></a>
    </div>
</div>
<div class="clearfix"></div>

<!--Kieu nhap lo-->
<asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div id="divError" style="text-align: center">
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div>
            <asp:HiddenField runat="server" ID="hdContractLV" />
        </div>
        <asp:Panel ID="pnCKL" runat="server" CssClass="divcontent">
            <div class="content">
                <div class="handle">
                    <%=Resources.labels.chuyenlogiaodichcungnganhang %>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <label><%= Resources.labels.luachonkieunhaplo %></label>
                        </div>
                        <div class="col-xs-7 col-sm-4 line30">
                            <asp:DropDownList ID="ddlTransferType" runat="server">
                                <asp:ListItem Value="F" Text="<%$ Resources:labels, nhapbangfile %>"></asp:ListItem>
                                <asp:ListItem Value="H" Text="<%$ Resources:labels, nhapbangtay %>"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div style="text-align: center; margin-top: 10px;">
                <asp:Button ID="btnTIBNext" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, next %>" OnClick="btnTIBNext_Click" />
            </div>
        </asp:Panel>
        <!--end-->
        <!--Kieu nhap lo-->
        <asp:Panel ID="pnPT" runat="server" CssClass="divcontent">
            <div class="content">
                <div class="handle">
                    <span><%=Resources.labels.thongtinnguoitratien %></span>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <%= Resources.labels.debitaccount %> &nbsp;*
                        </div>
                        <div class="col-xs-7 col-sm-4 line30">
                            <asp:DropDownList ID="ddlAccount" AutoPostBack="True" OnSelectedIndexChanged="ddlAccount_OnSelectedIndexChanged" runat="server">
                            </asp:DropDownList>
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
                    <span class="bold"><%=Resources.labels.noidungchuyenkhoan %></span>
                </div>
                <div class="content_table">
                    <div class="row form-group">
                        <div class="col-xs-4 col-sm-4 right"><%= Resources.labels.noidungthanhtoan %> &nbsp;*</div>
                        <div class="col-xs-7 col-md-4 col-sm-6 line30">
                            <asp:TextBox ID="txtLDesc" runat="server" TextMode="MultiLine" CssClass="form-control" onkeyup="ValidateLimit(this,200);" onkeyDown="ValidateLimit(this,200);" onpaste="ValidateLimit(this,200);"></asp:TextBox>
                            <asp:Label ID="lblNote" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666"
                                Text='<%$ Resources:labels, ghichunoidung %>'></asp:Label>
                        </div>
                    </div>
                    <div class="row form-group">
                    </div>
                    <div class="row form-group">
                        <div class="col-xs-5 col-sm-4 right"><%= Resources.labels.chonfile %> &nbsp;*</div>
                        <div class="col-xs-4 col-md-4 col-sm-4">
                            <input id="uploadFile" type="text" class="form-control" placeholder="Choose File" disabled="disabled" />
                        </div>
                        <div class="col-xs-1 col-md-1 col-sm-1">
                            <div class="upload-btn-wrapper" style="float: left">
                                <button class="btn btn-success">Upload file</button>
                                <asp:FileUpload ID="fuTransfer" runat="server" OnClientClick="getnamefile();" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="row form-group">
                    <div class="col-xs-3 col-sm-4 right">
                        <asp:Label ID="LblDocument" Visible="false" runat="server" Text=""><%= Resources.labels.document %>&nbsp;*</asp:Label>
                    </div>
                    <div class="col-xs-4 col-md-4 col-sm-6 line30">
                        <asp:FileUpload ID="FUDocument"  AllowMultiple="false" Visible="false" runat="server" ClientIDMode="Static" accept=".pdf,.png,.jpg,.jpeg" />
                    </div>
                    <div class="col-xs-5 col-md-4 col-sm-6 line30">
                        <asp:Label ID="LblDocumentExpalainion" Visible="false" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666" Text='<%$ Resources:labels, uploadlimit1MB %>'></asp:Label>
                    </div>
                </div>
            </div>
            </div>
            <div style="text-align: center; margin-top: 10px;">
                <asp:Button ID="btnLNext" runat="server" CssClass="btn btn-primary"
                    Text="<%$ Resources:labels, tieptuc %>" OnClick="btnLNext_Click" OnClientClick="return vali();" />
            </div>
        </asp:Panel>
        <!--end-->
        <!--Kieu nhap lo-->
        <asp:Panel ID="pnHandInput" runat="server" CssClass="divcontent">
            <div class="handle">
                <%=Resources.labels.chuyenkhoantheolonhapbangtay %>
            </div>
            <div class="content_table">
                <table class="style11" cellspacing="0" cellpadding="5">
                    <tr>
                        <td colspan="2" class="tibtdh">
                            <%=Resources.labels.thongtintaikhoan %>
                        </td>
                    </tr>
                    <tr>
                        <td class="tibtd">
                            <asp:Label ID="Label17" runat="server"
                                Text="<%$ Resources:labels, taikhoannguon %>"></asp:Label>
                            <asp:Label ID="Label4" runat="server"
                                Text=" *"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList2" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="tibtd">
                            <asp:Label ID="Label18" runat="server"
                                Text="<%$ Resources:labels, tentaikhoan %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label19" runat="server"
                                Text="Trần Anh Tuấn" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="tibtd">
                            <asp:Label ID="Label20" runat="server"
                                Text="<%$ Resources:labels, sodu %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label21" runat="server"
                                Text="2.000.000 <%$ Resources:labels, lak %>" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="tibtd">
                            <asp:Label ID="Label22" runat="server"
                                Text="<%$ Resources:labels, hinhthuc %>"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButton ID="RadioButton1" runat="server" GroupName="TIB" Text="<%$ Resources:labels, chuyenngay %>" Checked="True"
                                onclick="resetTS1();" />
                            <br />
                            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="TIB" Text="<%$ Resources:labels, chuyenvaongayddmmyyyy %>"
                                onclick="enableTS1();" />
                            &nbsp;<asp:TextBox ID="txtTS1" runat="server"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td colspan="2" class="tibtdh">
                            <%=Resources.labels.noidungthanhtoan %>
                        </td>
                    </tr>
                    <tr>
                        <td class="tibtd" colspan="2">
                            <table class="style11">
                                <tr>
                                    <td width="30%">
                                        <asp:Label ID="Label66" runat="server" Text="<%$ Resources:labels, sotaikhoan %>"></asp:Label>
                                    </td>
                                    <td width="30%">
                                        <asp:Label ID="Label67" runat="server" Text="<%$ Resources:labels, sotien %>"></asp:Label>
                                    </td>
                                    <td width="30%">
                                        <asp:Label ID="Label68" runat="server" Text="<%$ Resources:labels, diengiai %>"></asp:Label>
                                    </td>
                                    <td width="10%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDestAccount" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmount" onkeyup="ntt('ctl00_ctl15_txtAmount',event);" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDesc" runat="server" onkeyup="ValidateLimit(this,200);" onkeyDown="ValidateLimit(this,200);" onpaste="ValidateLimit(this,200);"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button10" runat="server" Text="<%$ Resources:labels, them %>"
                                            OnClientClick="return validate();" OnClick="Button10_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    </tr>
                            <td colspan="2" style="padding: 0 0 0 0;">
                                <asp:GridView ID="gvHandTransfer" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDeleting="gvHandTransfer_RowDeleting" Width="100%">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField DataField="STT" HeaderText="STT">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Account" HeaderText="<%$ Resources:labels, sotaikhoan %>" />
                                        <asp:BoundField DataField="User" HeaderText="<%$ Resources:labels, tenchutaikhoan %>" />
                                        <asp:BoundField DataField="Name" HeaderText="<%$ Resources:labels, tennguoinhan %>" />
                                        <asp:BoundField DataField="Amount" HeaderText="<%$ Resources:labels, sotien %>" />
                                        <asp:BoundField DataField="Desc" HeaderText="<%$ Resources:labels, diengiai %>" />
                                        <asp:CommandField ButtonType="Button" ShowDeleteButton="True">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:CommandField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </td>
                </table>
            </div>

             

            <!--Button next-->
            <div style="text-align: center; margin-top: 10px;">
                <div style="text-align: center; margin-top: 10px;">
                    <asp:Button ID="btnBack12" runat="server" OnClick="btnBack12_Click"
                        Text="Quay về" />
                    &nbsp;
                    <asp:Button ID="btnNext19" runat="server"
                        Text="<%$ Resources:labels, next %>" OnClick="btnNext19_Click" />
                </div>
            </div>
        </asp:Panel>
        <!--end-->
        <!--confirm-->
        <asp:Panel ID="pnConfirm" runat="server" CssClass="divcontent">
            <div class="content">
                <div class="handle">
                    <span><%=Resources.labels.thongtinnguoitratien %></span>
                </div>
                <div class="content_table">
                    <div class="row form-group">
                        <div class="col-xs-5 col-sm-6 right">
                            <%= Resources.labels.hotennguoitratien %>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblConfirmSenderName" runat="server" Font-Bold="False"></asp:Label>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-xs-5 col-sm-6 right">
                            <%= Resources.labels.debitaccount %>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblConfirmSenderAcctno" runat="server" Font-Bold="False"></asp:Label>
                            <asp:Label ID="lblSenderBranch" runat="server" Visible="False"></asp:Label>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-xs-5 col-sm-6 right">
                            <%= Resources.labels.sodutruockhighino %>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblConfirmBalance" runat="server" Font-Bold="False"></asp:Label>
                            &nbsp;<asp:Label ID="lblCCYID" runat="server" Font-Bold="False"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="handle">
                    <label class="bold"><%=Resources.labels.noidungchuyenkhoan %></label>
                </div>
                <div class="content_table">
                    <div class="row form-group">
                        <div class="col-xs-5 col-sm-6 right">
                            <%= Resources.labels.noidungthanhtoan %>
                        </div>
                        <div class="col-xs-7 col-sm-6 line30">
                            <asp:Label ID="lblConfirmDesc" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>

                <asp:UpdatePanel ID="UpdatePanelCheckAccount" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:GridView ID="gvConfirm" runat="server" AutoGenerateColumns="False" CssClass="table footable"
                            CellPadding="3" Width="100%" OnRowDataBound="gvConfirm_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="<%$ Resources:labels, stt %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("STT") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:labels, taikhoanbaoco %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccount" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Account") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:labels, tenchutaikhoan %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUser" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("User") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:labels, hotennguoinhantien %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:labels, sotien %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Amount") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:labels, errordesc %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblErrorDesc" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("ErrorDesc") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="White" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        </asp:GridView>
                        <div class="col-sm-6">
                            <label class="bold"><%= Resources.labels.tongphi %> :</label>
                            <asp:Label ID="lblTotalFee" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblTotalFeeCCYID" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                        </div>
                        <div class="col-sm-6">
                            <label class="bold"><%= Resources.labels.tongtien %> :</label>
                            <asp:Label ID="lblTotalBalance" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                            <asp:Label ID="lblTotalCCYID" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                            <asp:Image ID="imgLoading" runat="server" ImageUrl="/App_Themes/InternetBanking/images/loading.gif" Width="15px" />
                            <asp:HiddenField ID="hdfIsFinish" runat="server" Value="" />
                            <asp:HiddenField ID="hdfIsError" runat="server" Value="" />
                        </div>

                        <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="btnRefresh_Click">
                        </asp:Timer>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="clearfix"></div>
                <div style="text-align: center; margin-top: 10px; width: 100%">
                    <asp:Button ID="btnBack2" runat="server" CssClass="btn btn-warning" OnClick="btnBack2_Click"
                        Text="<%$ Resources:labels, quaylai %>" />
                    <asp:Button ID="btnRefresh" CssClass="btn btn-success" runat="server" Text="<%$ Resources:labels, refresh %>" OnClick="btnRefresh_Click" />
                    <asp:Button ID="btnConfirm" CssClass="btn btn-primary" runat="server" OnClientClick="return IsFinish();" OnClick="btnConfirm_Click"
                        Text="<%$ Resources:labels, confirm %>" />
                    <div class="clearfix"></div>

                </div>
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
                            <asp:Button ID="btnSendOTP" runat="server" CssClass="btn btn-primary btnSendOTP" OnClick="btnSendOTP_Click" Text="<%$ Resources:labels, resend %>" />
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
                    <asp:Button ID="Button4" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, quaylai %>" OnClick="btnBackA_Click" />
                    <asp:Button ID="btnAction" CssClass="btn btn-primary" runat="server" OnClick="btnAction_Click" Text='<%$ Resources:labels, thuchien %>' />
                </div>
            </div>
        </asp:Panel>
        <!--end-->
        <asp:Panel ID="pnResult" runat="server" CssClass="divcontent">
            <div class="header-title">
                <label class="bold"><%=Resources.labels.thongtintaikhoan %></label>
            </div>
            <div class="content">
                <div class="row form-group">
                    <label class="col-sm-6 col-xs-4 bold right"><%= Resources.labels.hotennguoitratien %></label>
                    <div class="col-sm-6 col-xs-8">
                        <asp:Label ID="lblResultUser" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row form-group">
                    <label class="col-sm-6 col-xs-4 bold right"><%= Resources.labels.debitaccount %></label>
                    <div class="col-sm-6 col-xs-8">
                        <asp:Label ID="lblResultAccount" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row form-group">
                    <label class="col-sm-6 col-xs-4 bold right"><%= Resources.labels.sodusaukhighino %></label>
                    <div class="col-sm-6 col-xs-8">
                        <asp:Label ID="lblResultBalance" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblResultCCYID" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="header-title">
                <label class="bold"><%=Resources.labels.noidungthanhtoan %></label>
            </div>
            <div class="content">
                <div class="row form-group">
                    <label class="col-sm-6 col-xs-4 bold right"><%= Resources.labels.noidungthanhtoan %></label>
                    <div class="col-sm-6 col-xs-8">
                        <asp:Label ID="lblResultDesc" runat="server"></asp:Label>
                    </div>
                </div>
                <asp:Panel runat="server" ID="pnDetails" CssClass="row">
                    <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" CssClass="table footable"
                        CellPadding="3" Width="100%" OnRowDataBound="gvResult_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, stt %>">
                                <ItemTemplate>
                                    <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("STT") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, taikhoanbaoco %>">
                                <ItemTemplate>
                                    <asp:Label ID="lblAccount" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Account") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, hotennguoinhantien %>">
                                <ItemTemplate>
                                    <asp:Label ID="lblUser" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("User") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, sotien %>">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Amount") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle BackColor="White" />
                        <PagerStyle BackColor="White" HorizontalAlign="Left" />
                    </asp:GridView>

                </asp:Panel>
                <div class="row">
                    <div class="col-sm-6 col-xs-6">
                        <label class="bold"><%= Resources.labels.tongphi %> :</label>
                        <asp:Label ID="lblTotalFeeFN" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        <asp:Label ID="lblTotalFeeCCYIDFN" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                    </div>
                    <div class="col-sm-6 col-xs-6">
                        <label class="bold"><%= Resources.labels.tongtien %> :</label>
                        <asp:Label ID="lblTotalBalanceFN" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        <asp:Label ID="lblTotalCCYIDFN" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                    </div>
                </div>
            </div>
            <!--Button next-->
            <div style="text-align: center; margin-top: 10px;">
                <asp:Button ID="btnExit" CssClass="btn btn-warning" runat="server"
                    Text="<%$ Resources:labels, thoat %>" OnClick="btnExit_Click" />
                <asp:Button ID="btnView" CssClass="btn btn-primary" runat="server" OnClientClick="javascript:return poponloadview()"
                    Text="<%$ Resources:labels, viewphieuin %>" />
                <asp:Button ID="btnPrint" CssClass="btn btn-warning" runat="server" OnClientClick="javascript:return poponload()"
                    Text="<%$ Resources:labels, inketqua %>" />
                <div class="clearfix"></div>
            </div>
        </asp:Panel>
        <!--end-->


        <script>
            function getnamefile() {
                document.getElementById("<%=fuTransfer.ClientID%>").onchange = function () {
                    document.getElementById("uploadFile").value = this.files[0].name;
                };
            }
        </script>
        <%--------%>
        <script>
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                onReady();
               <%-- document.getElementById("<%=fuTransfer.ClientID%>").onchange = function () {
                    document.getElementById("uploadFile").value = this.files[0].name;
                };--%>
            }
            document.getElementById("<%=fuTransfer.ClientID%>").onchange = function () {
                document.getElementById("uploadFile").value = this.files[0].name;
            };

            function poponload() {
                testwindow = window.open("widgets/IBCLGD/print.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
                    "menubar=1,scrollbars=1,width=500,height=650");
                testwindow.moveTo(0, 0);
                return false;
            }
            function poponloadview() {
                testwindow = window.open("widgets/IBCLGD/viewprint.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
                    "menubar=1,scrollbars=1,width=500,height=650");
                testwindow.moveTo(0, 0);
                return false;
            }

            function resetTS() {
                document.getElementById("ctl00_ctl15_txtTS").value = "";
                document.getElementById("ctl00_ctl15_txtTS").disabled = true;
            }
            function enableTS() {
                document.getElementById("ctl00_ctl15_txtTS").disabled = false;
            }
            function resetTS1() {
                document.getElementById("ctl00_ctl15_txtTS1").value = "";
                document.getElementById("ctl00_ctl15_txtTS1").disabled = true;
            }
            function enableTS1() {
                document.getElementById("ctl00_ctl15_txtTS1").disabled = false;
            }
            function enableOTP() {
                document.getElementById("ctl00_ctl12_txtOTP").disabled = false;
                document.getElementById("ctl00_ctl12_txtOTPBSMS").value = "";
                document.getElementById("ctl00_ctl12_txtOTPBSMS").disabled = true;
            }
            function enableSMSOTP() {
                document.getElementById("ctl00_ctl12_txtOTP").disabled = true;
                document.getElementById("ctl00_ctl12_txtOTPBSMS").disabled = false;
                document.getElementById("ctl00_ctl12_txtOTP").value = "";
            }
            function ntt(sNumber, event) {

                executeComma(sNumber, event);


            }
            function validateEmpty3(id, msg) {
                if (document.getElementById(id).value == "") {
                    window.alert(msg);
                    return false;
                }
                else {
                    return true;
                }
            }
            function vali() {
                if (validateEmpty3('<%=txtLDesc.ClientID %>','<%=Resources.labels.bancannhapnoidung %>')) {
                }
                else {
                    document.getElementById('<%=txtLDesc.ClientID %>').focus();
                    return false;
                }
            }
            function validateMoney3(id, msg) {
                if (document.getElementById(id).value == "0" || document.getElementById(id).value == "") {
                    window.alert(msg);
                    return false;
                }
                else {
                    return true;
                }
            }

            function validate() {
                if (validateEmpty3('<%=txtDestAccount.ClientID %>','<%=Resources.labels.taikhoandichkhongrong %>')) {
                    if (validateMoney3('<%=txtAmount.ClientID %>','<%=Resources.labels.bancannhapsotien %>')) {
                        if (validateEmpty3('<%=txtDesc.ClientID %>','<%=Resources.labels.bancannhapmota %>')) {
                            if (!check($('<%=txtDesc.ClientID %>').val())) {

                            } else {
                                document.getElementById('<%=txtDesc.ClientID %>').focus();
                                return false;
                            }
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
                else {
                    document.getElementById('<%=txtDestAccount.ClientID %>').focus();
                    return false;
                }

            }
            function IsFinish() {
                var IsFinish = document.getElementById('<%=hdfIsFinish.ClientID %>').value;
                var IsError = document.getElementById('<%=hdfIsError.ClientID %>').value;

                //alert(IsFinish + " " + IsError);
                if (IsFinish != '') {
                    if (IsError == '') {
                        return true;
                    }
                    else {
                        document.getElementById('<%=btnConfirm.ClientID %>').enabled = false;
                        alert(IsError);
                        return false;
                    }
                }
                else {
                    alert('Please wait for checking information finish')
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
        <asp:PostBackTrigger ControlID="btnLNext" />
    </Triggers>
</asp:UpdatePanel>

<style>
    .index9 {
        z-index: 9;
    }

    .index99 {
        position: relative;
        z-index: 99;
    }
</style>
