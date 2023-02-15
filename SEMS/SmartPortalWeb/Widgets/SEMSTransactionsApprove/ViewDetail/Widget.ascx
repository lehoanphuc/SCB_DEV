<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSTransactionsApprove_ViewDetail_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%= Resources.labels.thongtingiaodich %>
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
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.chitietgiaodich%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, sogiaodich %>"></asp:Label>
                                            <asp:Label ID="lblTransID" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, ngaygiogiaodich %>"></asp:Label>
                                            <asp:Label ID="lblDate" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label8" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, debitaccount %>"></asp:Label>
                                            <asp:Label ID="lblSenderAccount" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label9" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, taikhoanbaoco %>"></asp:Label>
                                            <asp:Label ID="lblReceiverAccount" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label6" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, sotien %>"></asp:Label>
                                            <div class="col-sm-8 col-xs-12 control-label">
                                                <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                &nbsp;<asp:Label ID="lblCCYID" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label18" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, sotienphi %>"></asp:Label>
                                            <div class="col-sm-8 col-xs-12 control-label">
                                                <asp:Label ID="lblFee" runat="server"></asp:Label>
                                                &nbsp;<asp:Label ID="lblCCYIDPhi" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, nganhang %>"></asp:Label>
                                            <asp:Label ID="lblBank" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label19" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, socmnd %>"></asp:Label>
                                            <asp:Label ID="lblLicense" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label10" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, ngaycap %>"></asp:Label>
                                            <asp:Label ID="lblIssueDate" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label12" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, noicap %>"></asp:Label>
                                            <asp:Label ID="lblIssuePlace" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label11" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, hotennguoitratien %>"></asp:Label>
                                            <asp:Label ID="lblSenderName" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label14" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, hotennguoinhantien %>"></asp:Label>
                                            <asp:Label ID="lblReceiverName" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label13" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, diachinguoinhantien %>"></asp:Label>
                                            <asp:Label ID="lblReceiverAdd" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label16" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, mota %>"></asp:Label>
                                            <asp:Label ID="lblDesc" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
                                            <asp:Label ID="lblStatus" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label17" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, nguoiduyet %>"></asp:Label>
                                            <asp:Label ID="lblAppSts" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label5" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, nguoithuchien %>"></asp:Label>
                                            <asp:Label ID="lblUserCreate" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label7" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, ketqua %>" Visible="False"></asp:Label>
                                            <asp:Label ID="lblResult" CssClass="col-sm-8 col-xs-12 control-label" runat="server" Visible="False"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.chitietduyetgiaodich%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <div class="row">
                                <div class="col-sm-12 col-xs-12">
                                    <asp:Literal ID="ltrTH" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.thongtinduyetgiaodich%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <div class="row">
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label15" CssClass="col-sm-4 col-xs-12 control-label" runat="server" Text="<%$ Resources:labels, diengiai %>"></asp:Label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtDesc" CssClass="form-control" runat="server" TextMode="MultiLine" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12"></div>
                            </div>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btnPrevious" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, giaodichtruoc %>" OnClick="btnPrevious_Click" />
                            <asp:Button ID="btApprove" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, duyet %>" OnClick="btApprove_Click" />
                            <asp:Button ID="btReject" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, khongduyet %>" OnClick="btReject_Click1" OnClientClick="return reject();" />
                            <asp:Button ID="btnExit" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btnExit_Click" />
                            <asp:Button ID="btnNext" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, giaodichketiep %>" OnClick="btnNext_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function reject() {
        if (document.getElementById('<%=txtDesc.ClientID %>').value == '') {
            window.alert('<%=Resources.labels.banvuilongnhaplydokhongduyetgiaodichnay %>');
            document.getElementById('<%=txtDesc.ClientID %>').focus();
            return false;
        }
    }
</script>





