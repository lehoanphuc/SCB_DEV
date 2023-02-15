<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSChangeWalletAccountLevel_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
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
                            <%=Resources.labels.ChangeWalletAccountLevel%>
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
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label"><%=Resources.labels.TransactionDate %></label>
                                            <div class="col-sm-7">
                                                <asp:Label class="control-label" ID="txtTransactionDate" ForeColor="Blue" runat="server"></asp:Label>

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
                                            <label class="col-sm-5 control-label required"><%=Resources.labels.SourceWalletAccountLevel %></label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlSourceWalletAccountLevel" CssClass="form-control select2" ReadOnly="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label required"><%=Resources.labels.DestinationWalletAccountLevel %></label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlDestinationWalletAccountLevel" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="panel-content form-horizontal p-b-0">
                                            <div class="row">
                                                <div class="col-xs-12" style="padding: unset">
                                                    <div class="well well-sm" style="background-color: #f5f5f500; height: 200px">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label class="col-sm-4 control-label label-bold colon"><%=Resources.labels.sanpham%></label>
                                                                <div class="col-sm-8">
                                                                    <asp:Label class="col-sm-4 control-label" ID="lbProductCode" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="col-sm-4 control-label"></div>
                                                                <div class="col-sm-8">
                                                                    <asp:Label class="col-sm-4 control-label" ID="lbProductName" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12">
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
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
