<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCloseWalletAccount_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleContracLevel" runat="server"></asp:Label>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.TopupEWalletConsumer%>
                        </h2>
                    </div>
                    <div>
                        <asp:HiddenField runat="server" ID="hdFee" />
                        <asp:HiddenField runat="server" ID="hdAmount" />
                        <asp:HiddenField runat="server" ID="hdContractNo" />
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row">
                                    <div class="col-sm-7">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.TransactionNumber %></label>
                                            <div class="col-sm-8">
                                                <asp:Label class="control-label" ID="txtTransactionNumber" ForeColor="Blue" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.TransactionDate %></label>
                                            <div class="col-sm-8">
                                                <asp:Label class="control-label" ID="txtTransactionDate" ForeColor="Blue" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.PhoneNumber %></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" runat="server" OnTextChanged="loadInfo" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <asp:Label ID="lbCurrency" CssClass="col-sm-2 control-label" Text="LAK" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.fullname %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtFullname" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.TopupAmount%></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtTopupAmount" CssClass="form-control" MaxLength="20" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.BonusAmount%> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtBonusAmount" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.FeeAmount%> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtFeeAmount" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.desc%> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDiscription" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="panel-content form-horizontal p-b-0">
                                            <div class="row">
                                                <div class="col-xs-12" style="padding: unset">
                                                    <div class="well well-sm" style="background-color: #f5f5f500; height: 250px;">
                                                        <div class="row">

                                                            <div class="col-sm-12">
                                                                <label class="col-sm-4 control-label label-bold colon"><%=Resources.labels.sanpham%></label>
                                                                <div class="col-sm-8">
                                                                    <asp:Label class="col-sm-12 control-label" ID="lbProductCode" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="col-sm-4 control-label"></div>
                                                                <div class="col-sm-8">
                                                                    <asp:Label class="col-sm-12 control-label" ID="lbProductName" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <label class="col-sm-4 control-label label-bold colon"><%=Resources.labels.hopdong%></label>
                                                                <div class="col-sm-8">
                                                                    <asp:Label class="col-sm-12 control-label" ID="lbContractCode" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <label class="col-sm-4 control-label label-bold colon"><%=Resources.labels.soduvi%></label>
                                                                <div class="col-sm-8">
                                                                    <asp:Label class="col-sm-12 control-label" ID="lbBalance" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <label class="col-sm-4 control-label label-bold colon"><%=Resources.labels.coinwallet%></label>
                                                                <div class="col-sm-8">
                                                                    <asp:Label class="col-sm-12 control-label" ID="lblCoinWallet" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, Accept %>" OnClick="btnAccept_click" />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, cancel %>" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnPrint" Enabled="false" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, print %> " OnClientClick="Loading();javascript:return poponload()" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script>
    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }
    function poponload() {
        var txref = document.getElementById('<%=txtTransactionNumber.ClientID%>').innerHTML;
        testwindow = window.open("widgets/SEMSTopUpEWalletConsumer/print.aspx?ID=" + txref + '&' + "cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai", "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
</script>
