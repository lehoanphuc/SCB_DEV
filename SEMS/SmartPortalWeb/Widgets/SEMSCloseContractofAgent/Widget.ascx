<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCloseContractofAgent_Widget" %>

<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>
<div class="row">
    <div class="col-sm-12">
        <div class="panel">
            <div class="panel-hdr">
                <h2>
                    <%=Resources.labels.CloseContractofAgent%>
                </h2>
            </div>
            <div class="panel-container">
                <div class="panel-content form-horizontal p-b-0">
                    <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnAccept">
                        <div class="row">
                            <div class="col-sm-7">
                                <div class="form-group">
                                    <label class="col-sm-5 control-label"><%=Resources.labels.TransactionNumber %></label>
                                    <div class="col-sm-7">
                                        <asp:TextBox CssClass="form-control" ID="txtTransactionNumber" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label"><%=Resources.labels.TransactionDate %></label>
                                    <div class="col-sm-7">
                                        <asp:TextBox CssClass="form-control" ID="txtTransactionDate" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label required"><%=Resources.labels.PhoneNumber %></label>
                                    <div class="col-sm-5">
                                        <asp:TextBox CssClass="form-control" ID="txtPhoneNumber" OnTextChanged="load_info" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                         <asp:Label ID="lbCurrency" CssClass="col-sm-2 control-label" Text="LAK" runat="server"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label required"><%=Resources.labels.fullname %></label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txtFullName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label required"><%=Resources.labels.status %></label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server"></asp:DropDownList>
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
                    <asp:Button ID="btnAccept" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, Accept %>" OnClick="btnAccept_Click" />
                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, cancel %>" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>
    </div>
</div>
<script src="/JS/Common.js"></script>

