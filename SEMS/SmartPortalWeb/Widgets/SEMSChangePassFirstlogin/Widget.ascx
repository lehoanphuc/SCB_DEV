<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSChangePassFirstlogin_Widget" %>
<div class="subheader">
    <h1 class="subheader-title">
        <%=Resources.labels.changepassword%>
    </h1>
</div>
<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>
<div class="row">
    <div class="col-sm-12">
        <div class="panel">
            <div class="panel-hdr">
                <h2>
                    <%=Resources.labels.thongtinmatkhau%>
                </h2>
            </div>
            <div class="panel-container">
                <div class="panel-content form-horizontal p-b-0">
                    <asp:Panel ID="pnFocus" runat="server" DefaultButton="btnChange">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label class="col-sm-2 col-sm-offset-2 control-label"><%=Resources.labels.oldpass %> *</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtOldPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4"></div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label class="col-sm-2 col-sm-offset-2 control-label"><%=Resources.labels.newpass %> *</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtNewPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4"></div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label class="col-sm-2 col-sm-offset-2 control-label"><%=Resources.labels.repassword %> *</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtRePassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4"></div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                    <asp:Button ID="btnChange" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, change %>" OnClientClick="return validate()" OnClick="btnChange_Click" />
                    <asp:Button ID="btnReset" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, lamlai %>" OnClick="btnReset_Click" />
                </div>
            </div>
        </div>
    </div>
</div>
<script>
    function validate() {
        if (document.getElementById('<%=txtOldPassword.ClientID%>').value == '') {
            alert('<%=Resources.labels.vuilongnhapmatkhaucu %>');
            document.getElementById('<%=txtOldPassword.ClientID %>').focus();
            return false;
        }
        if (document.getElementById('<%=txtNewPassword.ClientID%>').value == '') {
            alert('<%=Resources.labels.vuilongnhapmatkhaumoi %>');
            document.getElementById('<%=txtNewPassword.ClientID %>').focus();
            return false;
        }
        if (document.getElementById('<%=txtRePassword.ClientID%>').value == '') {
            alert('<%=Resources.labels.vuilongnhapmatkhauxacnhan %>');
            document.getElementById('<%=txtRePassword.ClientID %>').focus();
            return false;
        }
        if (document.getElementById('<%=txtNewPassword.ClientID%>').value != document.getElementById('<%=txtRePassword.ClientID%>').value) {
            alert('<%=Resources.labels.passwordcompare %>');
            document.getElementById('<%=txtRePassword.ClientID %>').focus();
            return false;
        }
        return true;
    }
</script>
