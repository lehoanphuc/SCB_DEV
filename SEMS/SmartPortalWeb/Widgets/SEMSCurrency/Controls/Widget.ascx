<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCurrency_Controls_Widget" %>

<asp:ScriptManager runat="server" ID="ScriptManager1"/>

<asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label runat="server" ID="lblTitleCurrency"/>
            </h1>
        </div>

        <div id="divError">
            <asp:Label runat="server" ID="lblError" Text="" Font-Bold="True" ForeColor="Red"/>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%= Resources.labels.currencyinfo %>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel runat="server" ID="pnAdd">
                                <div class="row">
                                    <div class="form-group col-sm-6 col-xs-12">
                                        <label class="col-sm-4 control-label col-xs-12 required">
                                            <%= Resources.labels.currencyid %>
                                        </label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtCurrencyId" CssClass="form-control" runat="server" MaxLength="10"/>
                                        </div>
                                    </div>
                                    <div class="form-group col-sm-6 col-xs-12">
                                        <label class="col-sm-4 control-label col-xs-12 required">
                                            <%= Resources.labels.scurrencyid %>
                                        </label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtSCurrencyId" CssClass="form-control" runat="server" MaxLength="2"/>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-6 col-xs-12">
                                        <label class="col-sm-4 control-label col-xs-12 ">
                                            <%= Resources.labels.currencynumber %>
                                        </label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtCurrencyNumber" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server" MaxLength="5"/>
                                        </div>
                                    </div>
                                    <div class="form-group col-sm-6 col-xs-12">
                                        <label class="col-sm-4 control-label col-xs-12 required">
                                            <%= Resources.labels.currencyname %>
                                        </label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtCurrencyName" CssClass="form-control" runat="server"/>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-6 col-xs-12">
                                        <label class="col-sm-4 control-label col-xs-12">
                                            <%= Resources.labels.currencymastername %>
                                        </label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtMasterName" CssClass="form-control" runat="server" MaxLength="250"/>
                                        </div>
                                    </div>
                                    <div class="form-group col-sm-6 col-xs-12">
                                        <label class="col-sm-4 control-label col-xs-12">
                                            <%= Resources.labels.desc %>
                                        </label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtCurrencyDesc" CssClass="form-control" runat="server" MaxLength="250"/>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-6 col-xs-12">
                                        <label class="col-sm-4 control-label col-xs-12">
                                            <%= Resources.labels.status %>
                                        </label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:DropDownList ID="ddlStatus" CssClass="form-control select2 infinity" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group col-sm-6 col-xs-12">
                                        <label class="col-sm-4 control-label col-xs-12 required">
                                            <%= Resources.labels.currencydecimaldigits %>
                                        </label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtDecimalDigits" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server" MaxLength="5"/>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-6 col-xs-12">
                                        <label class="col-sm-4 control-label col-xs-12 required">
                                            <%= Resources.labels.currencyroundingdigit %>
                                        </label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtRoundingDigit" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server" MaxLength="5"/>
                                        </div>
                                    </div>
                                    <div class="form-group col-sm-6 col-xs-12">
                                        <label class="col-sm-4 control-label col-xs-12">
                                            <%= Resources.labels.order %>
                                        </label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:DropDownList ID="ddlOrder" CssClass="form-control select2" runat="server"/>
                                        </div>
                                    </div>

                                </div>
                            </asp:Panel>
                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return checkValidation()" OnClick="btnSave_OnClick"/>
                                <asp:Button runat="server" ID="btnClear" CssClass="btn btn-secondary" Text='<%$ Resources:labels, Clear %>' OnClick="btnClear_Click"/>
                                <asp:Button ID="btnBack" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btnBack_Click"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function checkValidation() {
        if (!validateEmpty('<%= txtCurrencyId.ClientID %>','<%= Resources.labels.currencyidrequired %>')) {
            document.getElementById('<%= txtCurrencyId.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%= txtSCurrencyId.ClientID %>','<%= Resources.labels.scurrencyidrequired %>')) {
            document.getElementById('<%= txtSCurrencyId.ClientID %>').focus();
            return false;
        }

        if (!validateEmpty('<%= txtCurrencyName.ClientID %>','<%= Resources.labels.currencynamerequired %>')) {
            document.getElementById('<%= txtCurrencyName.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%= txtDecimalDigits.ClientID %>',' <%= Resources.labels.currencydecimaldigitsinvalid %>')) {
            document.getElementById('<%= txtDecimalDigits.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%= txtRoundingDigit.ClientID %>',' <%= Resources.labels.currencyroundingdigitinvalid %>')) {
            document.getElementById('<%= txtRoundingDigit.ClientID %>').focus();
            return false;
        }
    }
</script>