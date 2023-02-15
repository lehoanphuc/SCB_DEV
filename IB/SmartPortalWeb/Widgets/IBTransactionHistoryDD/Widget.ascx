<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTransactionHistoryDD_Widget" %>
<%@ Import Namespace="SmartPortal.Common.Utilities" %>

<link href="/CSS/css.css" rel="stylesheet" />
<style>
    .th {
        font-weight: bold;
        padding-left: 5px;
        padding-top: 10px;
        padding-bottom: 10px;
    }
</style>
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
    <span><%=Resources.labels.chitietgiaodichtaikhoanthanhtoan%></span><br />
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
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text='<%$ Resources:labels, view %>'
                            OnClick="Button1_Click" /></td>                        
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>

            <asp:Panel ID="pnDD" runat="server">
                <figure>
                    <legend class="handle"><%=Resources.labels.thongtintaikhoan %></legend>
                    <div class="content_table_4c_cl">
                        <div class="row">
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, accountnumber %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="lblAccountNumber" runat="server"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, accountname %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="lblAccountName_DD" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, dateopened %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="lblDO_DD" runat="server"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="Label16" runat="server" Text="<%$ Resources:labels, lasttransactiondate %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="lblLTD_DD" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, currentbalance %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="lblCB_DD" runat="server"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, currency %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="lblODPDL_DD" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, holdamount %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="lblHA_DO" runat="server"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="Label13" runat="server" Text="<%$ Resources:labels, odpinterest %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="lblODPI_DD" runat="server">
                                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, percentyear %>"></asp:Label></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="Label15" runat="server" Text="<%$ Resources:labels, availablebalance %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="lblAB_DD" runat="server"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, interestrate %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="lblIR_DD" runat="server"></asp:Label>
                                <asp:Label ID="Label5" runat="server"
                                    Text="<%$ Resources:labels, percentyear %>"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="Label12" runat="server" Text="<%$ Resources:labels, currency %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="lblCurrency_DD" runat="server"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="Label17" runat="server" Text="<%$ Resources:labels, accruedcreditinterest %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="lblACR_DD" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="Label14" runat="server" Text="<%$ Resources:labels, chinhanhphonggiaodich %>"></asp:Label>
                            </div>
                            <div class="col-xs-6 col-sm-3 heightmin23">
                                <asp:Label ID="lblBranch_DD" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </figure>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnTH">
                <figure>
                    <legend class="handle"><%=Resources.labels.transactionsearch3month %></legend>
                    <div class="row search">
                        <div class="col-md-5">
                            <div class="row form-group">
                                <label class="col-xs-3"><%= Resources.labels.fromdate %></label>
                                <div class="col-xs-9">
                                    <asp:TextBox ID="txtFromDate" ReadOnly="true" runat="server" CssClass="form-control pull-right dateselect" data-name="date1" data-level="0"></asp:TextBox>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="row form-group">
                                <label class="col-xs-3"><%= Resources.labels.todate %></label>
                                <div class="col-xs-9">
                                    <asp:TextBox ID="txtToDate" runat="server" ReadOnly="true" CssClass="form-control pull-right dateselect" data-name="date1" data-level="1"></asp:TextBox>

                                </div>
                            </div>
                        </div>

                        <div class="col-md-2" style="text-align: right">
                            <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary btn-right" Text="<%$ Resources:labels, view %>" OnClick="Button2_Click" />
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </figure>
                <div style="margin: 5px 0; padding-left: 2px;">
                    <asp:Button ID="btnExport" runat="server" CssClass="btn btn-success " Text="<%$ Resources:labels, xuatraexcel %>"
                        OnClick="btnExport_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, banin %>"
                        OnClientClick="javascript:return poponload()" />
                </div>

                <script type="text/javascript">
                    function poponload() {
                        testwindow = window.open('widgets/IBTransactionHistoryDD/print.aspx?ACCTNO=<%=SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ACCTNO"].ToString() %>', "Print",
                            "menubar=1,scrollbars=1,width=800,height=650");
                        testwindow.moveTo(0, 0);
                        return false;
                    }
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                    function EndRequestHandler(sender, args) {
                        onReady();
                    }
                </script>

            </asp:Panel>
            <div>
                <asp:Literal ID="ltrTH" runat="server"></asp:Literal>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
