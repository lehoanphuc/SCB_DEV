<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSChangeCustomerStatus_Widget" %>
<%@ Register Src="~/Controls/SearchTextBox/CustomerSearch.ascx" TagPrefix="uc1" TagName="CustomerSearch" %>
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
            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label></div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.Thaydoitrangthaikhachhang%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row">
                                    <div class="col-sm-7">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.TransactionNumber %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtTransactionNumber" CssClass="form-control" ReadOnly="true" IsRequired="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label" aria-required="true"><%=Resources.labels.TransactionDate %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtTransactionDate" CssClass="form-control"  IsRequired="true" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                          <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.custtype%></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlcusttype" CssClass="form-control select2" OnSelectedIndexChanged="changeCustomerType_LoadInfo" AutoPostBack="true" ReadOnly="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.PhoneNumber %></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" MaxLength="50" runat="server"  IsRequired="true" OnTextChanged="loadInfo" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <asp:Label ID="lbCurrency" CssClass="col-sm-2 control-label" Text="LAK" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.makhachhang %></label>
                                            <div class="col-sm-8">
                                                <uc1:CustomerSearch runat="server" ID="txtmakhachhang" />
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.fullname %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtFullName" CssClass="form-control"  ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.SourceStatus%></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSourceStatus" CssClass="form-control select2"  ReadOnly="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                          <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.DestinationStatus%></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlDestinationStatus" CssClass="form-control select2"  ReadOnly="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="panel-content form-horizontal p-b-0">
                                        <div class="row">
                                            <div class="col-xs-12" style="padding:unset">
                                                <div class="well well-sm" style="background-color:#f5f5f500">
                                                    <div class="row">
                                                        <div class="col-sm-5" style="text-align:center">
                                                            <img src="http://placehold.it/380x500" alt="" class="img-rounded img-responsive" />
                                                        </div>
                                                         <div class="col-sm-6">
                                                            <label class="col-sm-4 control-label label-bold" ><%=Resources.labels.sanpham+":"%></label>
                                                            <div class="col-sm-8">
                                                                <asp:Label  class="col-sm-4 control-label" ID="lbProductCode" runat="server"></asp:Label>
                                                            </div>
                                                             <div class="col-sm-4 control-label"></div>
                                                             <div class="col-sm-8">
                                                                <asp:Label  class="col-sm-4 control-label" ID="lbProductName" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <label class="col-sm-4 control-label label-bold"><%=Resources.labels.hopdong + ":"%></label>
                                                            <div class="col-sm-8">
                                                                <asp:Label  class="col-sm-4 control-label" ID="lbContractCode" runat="server"></asp:Label>
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
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, Accept %>" OnClick="btnAccept_click"/>
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_click"/>
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, cancel %>" OnClick="btnCancel_Click" />
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
        if (txt.value.length > len - 1) {
            return false;
        }
        var charCode = (event.which) ? event.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
</script>
