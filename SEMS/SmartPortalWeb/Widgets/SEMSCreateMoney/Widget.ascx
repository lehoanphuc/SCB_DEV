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
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.CreateEMoney%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row">
                                    <div class="col-sm-5">
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

                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.type %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlType" CssClass="form-control select2" Style="width: 100%;" Enabled="false" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-12 control-label" style="font-weight: bold; text-align: center;"><%=Resources.labels.DebitSide%> </label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-12 control-label" style="font-weight: bold; text-align: center;"><%=Resources.labels.CreditSide%> </label>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%= Resources.labels.Baseaccountnumber%> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDebitAccNo" CssClass="form-control" runat="server" OnTextChanged="txtDebitAccNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.accountname%> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDebitAccName" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%= Resources.labels.amount%> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDebitAmt" CssClass="form-control" MaxLength="18" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group" style="display: none">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.currency %> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDebitCurrency" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                                <asp:Button ID="btnDebit" CssClass="btn btn-primary" runat="server" Text="Add Debit Side" OnClick="btnAddDebit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.Baseaccountnumber%> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtCreditAccNo" CssClass="form-control" runat="server" OnTextChanged="txtCreditAccNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%= Resources.labels.accountname%> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtCreAccName" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%= Resources.labels.amount%> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtCreditAmt" CssClass="form-control" MaxLength="18" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group" style="display: none">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.currency %> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtCreditCurrency" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                                <asp:Button ID="btnAddCredit" CssClass="btn btn-primary" runat="server" Text="Add Credit Side" OnClick="btnAddCredit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                        <div id="divResult" style="overflow-x: hidden;">
                            <asp:Panel ScrollBars="Auto" runat="server">
                                <%--<div style="overflow-x:hidden; overflow-y:scroll;">--%>
                                <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                    <HeaderTemplate>
                                        <div class="table-responsive">
                                            <table class="table table-bordered" style="border: 1px solid rgba(0, 0, 0, 0.1); border-style: Solid;">
                                                <thead style="background-color: #7A58BF; color: #FFF;">
                                                    <tr>
                                                        <th style="text-align: center"><%=Resources.labels.postingside%></th>
                                                        <th style="text-align: center"><%=Resources.labels.CreditSide + " - " + Resources.labels.Baseaccountnumber%></th>
                                                        <th style="text-align: center"><%=Resources.labels.CreditSide + " - " + Resources.labels.amount%></th>
                                                        <th style="text-align: center"><%=Resources.labels.Action%></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="txtSide" Text='<%#Eval("Side") %>' runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtACCNO" Text='<%#Eval("AccNo") %>' runat="server"></asp:Label>
                                                <asp:Label ID="txtACNAME" Text='<%#Eval("Aacname") %>' Visible="false" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtAmount" Text='<%#Eval("Amount") %>' runat="server"></asp:Label>
                                            </td>
                                            <td class="action" style="text-align: center">
                                                <asp:LinkButton ID="linkID" runat="server" class="btn btn-secondary" CommandArgument='<%#Eval("Id")%>' CommandName='<%#IPC.ACTIONPAGE.DELETE%>' OnClientClick="return ConfirmDelete('Are you sure you want to delete this entry?');">Delete</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                        </table>
                        </div> 
                                    </FooterTemplate>
                                </asp:Repeater>
                                <%--</div>--%>
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

    function onlyDotsAndNumbers(txt, event, len) {

        var charCode = (event.which) ? event.which : event.keyCode
        if (charCode === 46) {
            if (txt.value.indexOf(".") < 0)
                return true;
            else
                return false;
        }

        if (txt.value.indexOf(".") > 0) {
            var txtlen = txt.value.length;
            var dotpos = txt.value.indexOf(".");
            //Change the number here to allow more decimal points than 2
            if ((txtlen - dotpos) > 2)
                return false;
        }
        if (txt.value.length > len) {
            if (!txt.value.includes('.'))
                txt.value = txt.value + '.';
        }
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
</script>
<script>
    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }
    function poponload() {
        var txref = document.getElementById('<%=txtTransactionNumber.ClientID%>').innerHTML;
        testwindow = window.open("widgets/SEMSCreateMoney/print.aspx?ID=" + txref + '&' + "cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai", "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
</script>
