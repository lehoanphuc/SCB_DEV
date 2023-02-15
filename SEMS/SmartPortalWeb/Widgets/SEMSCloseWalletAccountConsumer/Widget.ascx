<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCloseWalletAccount_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>

<div id="divError">
    <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
</div>
<div class="row">
    <div class="col-sm-12">
        <div class="panel">
            <div class="panel-hdr">
                <h2>
                    <%=Resources.labels.closewalletaccountofconsumer%>
                </h2>
            </div>
            <div class="panel-container">
                <div class="panel-content form-horizontal p-b-0">
                    <asp:Panel ID="pnAdd" runat="server">
                        <div class="row">
                            <div class="col-sm-7">
                                <div class="form-group">
                                    <label class="col-sm-5 control-label"><%=Resources.labels.TransactionNumber %></label>
                                    <div class="col-sm-7">
                                        <asp:Label class="control-label" ID="txtTransactionNumber" ForeColor="Blue" runat="server"></asp:Label>
                                        <%--<asp:TextBox ID="txtTransactionNumber" CssClass="form-control" ReadOnly="true" IsRequired="true" runat="server"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label"><%=Resources.labels.TransactionDate %></label>
                                    <div class="col-sm-7">
                                        <asp:Label class="control-label" ID="txtTransactionDate" ForeColor="Blue" runat="server"></asp:Label>
                                        <%--<asp:TextBox ID="txtTransactionDate" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label required"><%=Resources.labels.PhoneNumber %></label>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" runat="server" OnTextChanged="loadInfo" AutoPostBack="true"></asp:TextBox>
                                    </div>
                                    <asp:Label ID="lbCurrency" CssClass="col-sm-2 control-label" Text="LAK" runat="server"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label required"><%=Resources.labels.fullname %></label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txtFullname" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label required"><%=Resources.labels.WalletAccount %></label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txtWalletAccount" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label required"><%=Resources.labels.SourceStatus %></label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="ddlSourceStatus" CssClass="form-control select2" ReadOnly="true" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-5">
                                <div class="panel-content form-horizontal p-b-0">
                                    <div class="row">
                                        <div class="col-xs-12" style="padding: unset">
                                            <div class="well well-sm" style="background-color: #f5f5f500">
                                                <div class="row">
                                                    <div class="col-sm-5" style="text-align: center">
                                                        <img src="http://placehold.it/380x500" alt="" class="img-rounded img-responsive" />
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <label class="col-sm-4 control-label label-bold colon"><%=Resources.labels.sanpham%></label>
                                                        <div class="col-sm-8">
                                                            <asp:Label class="col-sm-4 control-label" ID="lbProductCode" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-4 control-label"></div>
                                                        <div class="col-sm-8">
                                                            <asp:Label class="col-sm-4 control-label" ID="lbProductName" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <label class="col-sm-4 control-label label-bold colon"><%=Resources.labels.hopdong%></label>
                                                        <div class="col-sm-8">
                                                            <asp:Label class="col-sm-4 control-label" ID="lbContractCode" runat="server"></asp:Label>
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
                </div>
            </div>
        </div>
    </div>
</div>
<script src="/JS/Common.js"></script>
