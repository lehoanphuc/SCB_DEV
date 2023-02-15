<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBSTV_ViewDetails_Widget" %>


<link href="CSS/css.css" rel="stylesheet" type="text/css" />
<%--<link href="Widgets/IBSTV/CSS/css.css" rel="stylesheet" type="text/css" />--%>
<div class="al">
    <span><%=Resources.labels.chitietthongtindatlich %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<!--thong tin tai khoan DD-->
<asp:Panel ID="pnDD" runat="server">
    <figure>
        <%--<legend class="handle hidden-xs"><%=Resources.labels.thongtindatlich %></legend>--%>

        <div class="divcontent">
            <div class="item">
                <div class="handle">
                    <label class="bold"><%= Resources.labels.thongtindatlich %></label>
                </div>
                <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 bold right">
                        <label><%= Resources.labels.tenlich %></label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbschedulename" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 bold right">
                        <label><%= Resources.labels.kieulich %></label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblScheduleType" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 bold right">
                        <label><%= Resources.labels.loaichuyenkhoan %></label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblTransferType" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 bold right">
                        <label><%= Resources.labels.tungay %></label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbfromdate" runat="server" Text="08/03/2010"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 bold right">
                        <label><%= Resources.labels.denngay %></label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbtodate" runat="server" Text="08/04/2010"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 bold right">
                        <label><%= Resources.labels.thoidiemthuchienketiep %></label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbtime" runat="server" Text="08/03/2010"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 bold right">
                        <label><%= Resources.labels.ngayxuly %></label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbdatetransfer" runat="server" Visible="false" Text=""></asp:Label>
                        <asp:Label ID="lbdatetransfer2" runat="server" Visible="true" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row" id="trrepeatmonthly" runat="server" visible="true">
                    <div class="col-xs-5 col-sm-6 bold right">
                        <label><%= Resources.labels.solanlap %></label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbrepeatmonthly" Font-Bold="true" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="table-responsive" style="margin-bottom:0">
                    <asp:Table ID="tbdetailsmonth" Visible="false" border="1" class="table style1 footable" CellSpacing="0" CellPadding="5" runat="server">
                        <asp:TableRow>
                            <asp:TableHeaderCell>Year</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Content</asp:TableHeaderCell>
                            <asp:TableHeaderCell data-breakpoints="xs">Jan</asp:TableHeaderCell>
                            <asp:TableHeaderCell data-breakpoints="xs">Feb</asp:TableHeaderCell>
                            <asp:TableHeaderCell data-breakpoints="xs">March</asp:TableHeaderCell>
                            <asp:TableHeaderCell data-breakpoints="xs">April</asp:TableHeaderCell>
                            <asp:TableHeaderCell data-breakpoints="xs">May</asp:TableHeaderCell>
                            <asp:TableHeaderCell data-breakpoints="xs">June</asp:TableHeaderCell>
                            <asp:TableHeaderCell data-breakpoints="xs">July</asp:TableHeaderCell>
                            <asp:TableHeaderCell data-breakpoints="xs">August</asp:TableHeaderCell>
                            <asp:TableHeaderCell data-breakpoints="xs">Sept</asp:TableHeaderCell>
                            <asp:TableHeaderCell data-breakpoints="xs">Oct</asp:TableHeaderCell>
                            <asp:TableHeaderCell data-breakpoints="xs">Nov</asp:TableHeaderCell>
                            <asp:TableHeaderCell data-breakpoints="xs">Dec</asp:TableHeaderCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
                    </div>
            </div>
            <div class="item">
                <div class="handle">
                    <label class="bold"><%= Resources.labels.thongtinnguoitratien %></label>
                </div>
                <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 bold right">
                        <label><%= Resources.labels.hotennguoitratien %></label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbsender" runat="server" Text="Trần Anh Tuấn"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 bold right">
                        <label><%= Resources.labels.debitaccount %></label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbaccount" runat="server" Text="00009102001"></asp:Label>
                    </div>
                </div>
            </div>
                </div>
            <div class="item">
                <div class="handle">
                    <label class="bold"><%= Resources.labels.thongtinnguoinhantien %></label>
                </div>
                <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 bold right">
                        <label><%= Resources.labels.hotennguoinhantien %></label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbreceiver" runat="server" Text="Trần Văn A"></asp:Label>
                    </div>
                </div>
                <asp:Panel ID="pnTaiKhoanBaoCo" runat="server" CssClass="row">
                    <div class="col-xs-5 col-sm-6 bold right">
                        <label><%= Resources.labels.taikhoanbaoco %></label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbreceiveaccount" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnConfirmCMND" runat="server" CssClass="row">
                    <div class="col-xs-12">
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 bold right">
                                <label><%= Resources.labels.socmnd %></label>
                            </div>
                            <div class="col-xs-7 col-sm-6 line30">
                                <asp:Label ID="lblLicense" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 bold right">
                                <label><%= Resources.labels.ngaycap %></label>
                            </div>
                            <div class="col-xs-7 col-sm-6 line30">
                                <asp:Label ID="lblIssueDate" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 bold right">
                                <label><%= Resources.labels.noicap %></label>
                            </div>
                            <div class="col-xs-7 col-sm-6 line30">
                                <asp:Label ID="lblIssuePlace" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnBank" runat="server" CssClass="row">
                    <div class="col-xs-12">
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 bold right">
                                <label><%= Resources.labels.diachinguoinhantien %></label>
                            </div>
                            <div class="col-xs-7 col-sm-6 line30">
                                <asp:Label ID="lblConfirmReceiverAdd" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-sm-6 bold right">
                                <label><%= Resources.labels.chinhanhphonggiaodich %></label>
                            </div>
                            <div class="col-xs-7 col-sm-6 line30">
                                <asp:Label ID="lblConfirmChildBank" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                    </div>
            </div>
            <div class="item">
                <div class="handle">
                    <label class="bold"><%= Resources.labels.noidungchuyenkhoan %></label>
                </div>
                <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 bold right">
                        <label><%= Resources.labels.sotien %></label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbamount" runat="server" Text="20.000.000"></asp:Label>
                        &nbsp;<asp:Label ID="lbccyid" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <asp:Panel ID="pnConFirmFee" runat="server" CssClass="row">
                    <div class="col-xs-5 col-sm-6 bold right">
                        <label><%= Resources.labels.sotienphi %></label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblPhiAmount" runat="server" Text="0"></asp:Label>
                        &nbsp;<asp:Label ID="lblFeeCCYID" runat="server"></asp:Label>
                    </div>
                </asp:Panel>

                <div class="row">
                    <div class="col-xs-5 col-sm-6 bold right">
                        <label><%= Resources.labels.nguoitraphi %></label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <span><%= Resources.labels.nguoichuyen %></span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 bold right">
                        <label><%= Resources.labels.noidungthanhtoan %></label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbdesc" runat="server" Text="Chuyển lương tháng 3"></asp:Label>
                    </div>
                </div>
                    </div>
            </div>
            <div class="button-group">
                    <asp:Button ID="Button3" runat="server" CssClass="btn btn-warning" Text='<%$ Resources:labels, quaylai %>' OnClick="btnBack_OnClick" />
                
            </div>
        </div>
    </figure>
</asp:Panel>

