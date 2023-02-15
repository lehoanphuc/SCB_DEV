<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTransactionHistoryTKCKH_Widget" %>
<%@ Import Namespace="SmartPortal.Common.Utilities" %>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
    <ProgressTemplate>
        <div class="cssProgress">
            <div class="progress1">
                <div class="cssProgress-bar cssProgress-active" data-percent="100" style="transition: none; width: 100%;"></div>
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>


<div class="th">
    <span><%=Resources.labels.chitiettaikhoantietkiemcokyhan %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="divcontent">
            <div class="row form-search">
                <div class="form-group">
                    <label class="col-md-2 col-xs-3 bold"><%= Resources.labels.account %></label>
                    <div class="col-md-4 col-xs-6">
                        <asp:DropDownList ID="ddlAccount" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-xs-3" style="text-align: left;">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text='<%$ Resources:labels, view %>' OnClick="Button1_Click" />
                    </div>
                </div>
            </div>
            <!--thong tin tai khoan tiet kiem có KKH-->
            <asp:Panel ID="pnFD" runat="server">
                <figure>
                    <legend class="handle"><%=Resources.labels.thongtintaikhoan %></legend>
                    <div class="content_table_4c_cl">
                        <div class="row">
                            <div class="col-xs-6 col-sm-3 line30 ">
                                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, accountnumber %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 line30">
                                <asp:Label ID="lblAccountNumber_FD" runat="server"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 line30 ">
                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, accountname %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 line30">
                                <asp:Label ID="lblAccountName_FD" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6 col-sm-3 line30 heightmin23">
                                <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, dateopened %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 line30 height30">
                                <asp:Label ID="lblDO_FD" runat="server"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 line30 heightmin23">
                                <asp:Label ID="Label16" runat="server" Text="<%$ Resources:labels, ExpireDate %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 line30 height30">
                                <asp:Label ID="lblLT_FD" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6 col-sm-3 line30 heightmin23">
                                <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, currentbalance %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 line30 height30">
                                <asp:Label ID="lblCB_FD" runat="server"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 line30 heightmin23">
                                <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, accruedcreditinterest %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 line30 height30">
                                <asp:Label ID="lblACRI_FD" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6 col-sm-3 line30 heightmin23">
                                <asp:Label ID="Label14" runat="server" Text="<%$ Resources:labels, interestrate %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 line30 height30">
                                <asp:Label ID="lblIR_FD" runat="server" Text=""></asp:Label>
                                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, percentyear %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 line30 heightmin23">
                                <asp:Label ID="Label17" runat="server" Text="<%$ Resources:labels, currency %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 line30 height30">
                                <asp:Label ID="lblCurrency_FD" runat="server" Text="<%$ Resources:labels, lak %>"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6 col-sm-3 line30 heightmin23">
                                <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, branch %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 line30 height30">
                                <asp:Label ID="lblBranch_DD" runat="server" Text=""></asp:Label>
                            </div>

                        </div>
                    </div>
                </figure>
            </asp:Panel>
            <!--end-->
            <!--chi tiet giao dich -->
            <asp:Panel ID="pnTH" runat="server">
                <figure>
                    <legend class="handle"><%=Resources.labels.chitietgiaodich %></legend>
                    <div class="row search">
                        <div class="col-md-5">
                            <div class="row form-group">
                                <label class="col-xs-3"><%= Resources.labels.fromdate %></label>
                                <div class="col-xs-9">
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control pull-right dateselect" data-name="date1" data-level="0"></asp:TextBox>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="row form-group">
                                <label class="col-xs-3"><%= Resources.labels.todate %></label>
                                <div class="col-xs-9">
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control pull-right dateselect" data-name="date1" data-level="1"></asp:TextBox>

                                </div>
                            </div>
                        </div>


                        <div class="col-md-2">
                            <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary btn-right" Text="<%$ Resources:labels, view %>" OnClick="Button2_Click" />
                        </div>
                        <div class="clearfix"></div>
                        <div style="margin-top: 10px;">
                            <asp:Literal ID="ltrTH" runat="server"></asp:Literal>
                        </div>
                    </div>
                </figure>
            </asp:Panel>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
