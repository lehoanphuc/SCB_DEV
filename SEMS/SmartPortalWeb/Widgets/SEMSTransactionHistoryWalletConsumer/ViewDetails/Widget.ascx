<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSViewLogTransactions_ViewDetails_Widget" %>
<div class="subheader">
    <h1 class="subheader-title">
        <%=Resources.labels.nhatkygiaodich %>
    </h1>
</div>
<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>

<asp:Panel runat="server" ID="pnDefault">
    <div class="row">
        <div class="col-sm-12 col-xs-12">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>
                        <%=Resources.labels.chitietgiaodich%>
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.sogiaodich %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblTransID" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.ngaygiogiaodich %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.debitaccount %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblAccount" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.hotennguoitratien %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.sotien %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                        <asp:Label ID="lblCCYID" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.sotienphi %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblFee" runat="server"></asp:Label>
                                        &nbsp;<asp:Label ID="lblCCYIDPhi" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.sotienbangchu %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblstbc" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.vat %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblVAT" runat="server"></asp:Label>
                                        &nbsp;<asp:Label ID="lblCCYIDVAT" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.mota %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.laiduochuong %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblLDH" runat="server"></asp:Label>
                                        &nbsp;<asp:Label ID="lblCCYIDLDH" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.taikhoanbaoco %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblAccountNo" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.hotennguoinhantien %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblReceiverName" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.nganhang %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblBank" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tinhthanh %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lbltinhthanh" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.socmnd %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblLicense" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.ngaycap %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblIssueDate" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.noicap %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblIssuePlace" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.diachinguoinhantien %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblReceiverAdd" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.nguoiduyetcuoi %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblLastApp" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.trangthai %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.nguoiduyet %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblAppSts" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.nguoithuchien %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblUserCreate" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.ngayduyet %></label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblApproveDate_0" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <asp:Label ID="Label49" runat="server" CssClass="col-sm-4 col-xs-12 control-label" Text="<%$ Resources:labels, ketqua %>" Visible="False"></asp:Label>
                                    <div class="col-sm-8 col-xs-12 control-label">
                                        <asp:Label ID="lblResult" runat="server" Visible="False"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                        <asp:Button ID="btnPrint" CssClass="btn btn-primary" runat="server" Text='<%$ Resources:labels, inketqua %>' OnClientClick="javascript:return poponload();" />
                        <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" PostBackUrl="javascript:history.go(-1);" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>

<script type="text/javascript">
    function poponload() {
        testwindow = window.open("widgets/SEMSViewLogTransactions/print.aspx?pt=P&cul=" +'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=800,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
</script>
