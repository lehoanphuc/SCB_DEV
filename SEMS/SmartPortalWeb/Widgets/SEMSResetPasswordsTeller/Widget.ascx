<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSResetPasswordsTeller_Widget" %>
<div class="subheader">
    <h1 class="subheader-title">
        <%=Resources.labels.laylaimatkhau%>
    </h1>
</div>
<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>
<div class="row">
    <div class="col-sm-12 col-xs-12">
        <div class="panel">
            <div class="panel-hdr">
                <h2>
                    <%=Resources.labels.thongtinlaymatkhau%>
                </h2>
            </div>
            <div class="panel-container">
                <div class="panel-content form-horizontal p-b-0">
                    <asp:Panel ID="pnFocus" runat="server" DefaultButton="btnChange">
                        <div class="row">
                            <div class="col-sm-12 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label required"><%=Resources.labels.username %></label>
                                    <div class="col-sm-4 col-xs-12">
                                        <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4 col-xs-12"></div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label required"><%=Resources.labels.email %></label>
                                    <div class="col-sm-4 col-xs-12">
                                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4 col-xs-12"></div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                    <asp:Button ID="btnChange" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, laymatkhau %>" OnClientClick="Loading(); return validate();" OnClick="btnChange_Click" />
                    <asp:Button ID="btnReset" CssClass="btn btn-secondary" runat="server" OnClick="btnReset_Click" Text="<%$ Resources:labels, lamlai %>" />
                </div>
            </div>
        </div>
    </div>
</div>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function validate() {
        if (!validateEmpty('<%=txtUserName.ClientID %>', '<%=Resources.labels.bancannhaptendangnhap %>')) {
            document.getElementById('<%=txtUserName.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtEmail.ClientID %>', '<%=Resources.labels.bancannhapemail %>')) {
            document.getElementById('<%=txtEmail.ClientID %>').focus();
            return false;
        }
        if (!checkEmail('<%=txtEmail.ClientID %>','<%=Resources.labels.emailkhongdungdinhdang %>')) {
            document.getElementById('<%=txtEmail.ClientID %>').focus();
            return false;
        }
        return true;
    }
    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }
</script>
