<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTransactionHistory1_Widget" %>
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

<div class="al">
   QR merchant History<br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label runat="server" ID="lblError" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div>
            <asp:HiddenField runat="server" ID="hdAccountType" />
        </div>
        <figure>
            <div class="row">
                <div class="form-group">
                    <label class="col-md-2 col-xs-3 bold"><%= Resources.labels.account %></label>
                    <div class="col-md-4 col-xs-6">
                        <asp:DropDownList ID="ddlAccount" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAccount_OnSelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
        </figure>

        <!--thong tin tai khoan DD-->
        <asp:Panel ID="pnDD" runat="server" CssClass="divcontent">
            <figure>
                <legend class="handle"><%=Resources.labels.thongtintaikhoan %></legend>
                <div class="content_table_4c_cl line30">
                    <div class="row">
                        <div class="col-xs-3 col-sm-2">
                            <label class="bold">
                                <%= Resources.labels.accountnumber %>
                            </label>
                        </div>
                        <div class="col-xs-9 col-sm-4">
                            <asp:Label ID="lblAccountNumber_DD" runat="server"></asp:Label>
                        </div>
                        <div class="col-xs-3 col-sm-2">
                            <label class="bold">
                                <%= Resources.labels.accountname %>
                            </label>
                        </div>
                        <div class="col-xs-9 col-sm-4">
                            <asp:Label ID="lblAccountName_DD" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-3 col-sm-2">
                            <label class="bold">
                                <%= Resources.labels.currentbalance %>
                            </label>
                        </div>
                        <div class="col-xs-9 col-sm-4">
                            <asp:Label ID="lblCB_DD" runat="server"></asp:Label>
                        </div>
                        <div class="col-xs-3 col-sm-2">
                            <label class="bold">
                                <%= Resources.labels.currency %>
                            </label>
                        </div>
                        <div class="col-xs-9 col-sm-4">
                            <asp:Label ID="lblCurrency_DD" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-3 col-sm-2">
                            <label class="bold">
                                <%= Resources.labels.dateopened %>
                            </label>
                        </div>
                        <div class="col-xs-9 col-sm-4">
                            <asp:Label ID="lblDateOpen" runat="server"></asp:Label>
                        </div>
                        <div class="col-xs-3 col-sm-2">
                            <label class="bold">
                                <%= Resources.labels.availablebalance %>
                            </label>
                        </div>
                        <div class="col-xs-9 col-sm-4">
                            <asp:Label ID="lblAB_DD" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-3 col-sm-2">
                            <label class="bold">
                                <%= Resources.labels.interestrate %>
                            </label>
                        </div>
                        <div class="col-xs-9 col-sm-4">
                            <asp:Label ID="lblIR_DD" runat="server"></asp:Label>
                            <asp:Label ID="Label153" runat="server" Text="<%$ Resources:labels, percentyear %>"></asp:Label>
                        </div>
                    </div>
                    <asp:Panel runat="server" ID="pnFD" Visible="false">
                        <div class="row">
                            <div class="col-xs-3 col-sm-2">
                                <label class="bold">
                                    <%= Resources.labels.fromdate %>
                                </label>
                            </div>
                            <div class="col-xs-9 col-sm-4">
                                <asp:Label ID="lblFromDateFD" runat="server"></asp:Label>
                            </div>
                            <div class="col-xs-3 col-sm-2">
                                <label class="bold">
                                    <%= Resources.labels.todate %>
                                </label>
                            </div>
                            <div class="col-xs-9 col-sm-4">
                                <asp:Label ID="lblToDateFD" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-3 col-sm-2">
                                <label class="bold">
                                    <%= Resources.labels.category %>
                                </label>
                            </div>
                            <div class="col-xs-9 col-sm-4">
                                <asp:Label ID="lblCategory" runat="server"></asp:Label>
                            </div>
                            <div class="col-xs-3 col-sm-2">
                                <label class="bold">
                                    <%= Resources.labels.debitacc2 %>
                                </label>
                            </div>
                            <div class="col-xs-9 col-sm-4">
                                <asp:Label ID="lblDebitAcct" runat="server"></asp:Label>
                            </div>
                        </div>
                    </asp:Panel>

                </div>
            </figure>
        </asp:Panel>

        <!--end-->
        <br />
        <!--chi tiet giao dich -->
        <asp:Panel ID="pnCTGG" runat="server">
            <figure>
                <legend class="handle"><%=Resources.labels.transactionsearch3month %></legend>
                <div class="row form-group">
                    <div class="col-md-5">
                        <div class="col-xs-4 col-md-4">
                            <asp:Label ID="Label3" runat="server" Text='<%$ Resources:labels, tungay %>'></asp:Label>
                        </div>
                        <div class="col-xs-8 col-md-8">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="dateselect"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="col-xs-4 col-md-4">
                            <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, denngay %>'></asp:Label>
                        </div>
                        <div class="col-xs-8 col-md-8">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="dateselect"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnViewHT" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, search %>" OnClick="Button2_Click" />
                    </div>
                </div>
            </figure>
            <div class="button-group">
            </div>
        </asp:Panel>
        <div style="margin-top: 15px;">
            <div class="content" style=" overflow: auto">
            <asp:Literal ID="ltrTH" runat="server"></asp:Literal>
            <div class="hidden">
                <asp:Literal ID="ltrSession" runat="server"></asp:Literal>
            </div>
                </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="button-group">
    <asp:Button ID="btnExport" runat="server" CssClass="btn btn-success" Text="<%$ Resources:labels, xuatraexcel %>"
        OnClick="btnExport_Click" />
    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, banin %>" OnClick="btnPrint_Click"
        OnClientClick="javascript:return poponload();" />
</div>

<script type="text/javascript">
    var cal = Calendar.setup({
        onSelect: function (cal) { cal.hide() }
    });
    cal.manageFields("<%=txtFromDate.ClientID %>", "<%=txtFromDate.ClientID %>", "%d/%m/%Y");
    cal.manageFields("<%=txtToDate.ClientID %>", "<%=txtToDate.ClientID %>", "%d/%m/%Y");


    function poponload() {
        testwindow = window.open('widgets/IBQRMerchantHistory/print.aspx', "Print",
            "menubar=1,scrollbars=1,width=800,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
</script>
