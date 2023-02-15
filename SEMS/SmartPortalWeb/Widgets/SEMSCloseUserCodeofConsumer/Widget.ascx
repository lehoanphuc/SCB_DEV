<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCloseUserCodeofConsumer_Widget" %>

<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>
<div class="row">
    <div class="col-sm-12">
        <div class="panel">
            <div class="panel-hdr">
                <h2>
                    <%=Resources.labels.CloseUserCodeofConsumer%>
                </h2>
            </div>
            <div class="panel-container">
                <div class="panel-content form-horizontal p-b-0">
                    <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnAccept">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label class="col-sm-2 col-sm-offset-2 control-label"><%=Resources.labels.TransactionNumber %></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox CssClass="form-control" ID="txtTransactionNumber" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4"></div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label class="col-sm-2 col-sm-offset-2 control-label"><%=Resources.labels.TransactionDate %></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox CssClass="form-control" ID="txtTransactionDate" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4"></div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label class="col-sm-2 col-sm-offset-2 control-label"><%=Resources.labels.PhoneNumber %> (*)</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox CssClass="form-control" ID="txtPhoneNumber" OnTextChanged="load_info" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4"></div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label class="col-sm-2 col-sm-offset-2 control-label"><%=Resources.labels.fullname %> (*)</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtFullName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4"></div>
                                </div>
                            </div>
                        </div>
                            <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label class="col-sm-2 col-sm-offset-2 control-label"><%=Resources.labels.UserLogin %> (*)</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtUserLogin" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4"></div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label class="col-sm-2 col-sm-offset-2 control-label"><%=Resources.labels.status %> (*)</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4"></div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>


                <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                    <asp:Button ID="btnAccept" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, Accept %>" OnClick="btnAccept_Click" />
                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_Click" />
                </div>
            </div>
        </div>
    </div>
</div>
<script src="/JS/Common.js"></script>

