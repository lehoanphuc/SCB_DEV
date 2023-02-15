<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSChangePasswordsTeller_Widget" %>
<link href="CSS/bootstrap.min.css" rel="stylesheet" />
<link href="CSS/smallBoostrap.css" rel="stylesheet" />
<link href="widgets/SEMSHeader/css/style.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="al">
    <img alt="" src="widgets/SEMSChangePasswordsTeller/Image/cp.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.changepassword %>
</div>
<div id="divError">
    <asp:Label ID="lblError" ForeColor="Red" Font-Bold="true" runat="server" />
</div>
<div class="col-md-12">
    <div class="row">
        <div class="content form-horizontal">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <div class="form-group">
                    <div class="col-sm-4 control-label">
                        <%=Resources.labels.username %>
                    </div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-4 control-label">
                        <label style="color: red; font-weight: bold"><%=Resources.labels.oldpass %> *</label>
                    </div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-4 control-label">
                        <label style="color: red; font-weight: bold"><%=Resources.labels.newpass %> *</label>
                    </div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-4 control-label">
                        <label style="color: red; font-weight: bold"><%=Resources.labels.repassword %> *</label>
                    </div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtRePassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-4"></div>
        </div>
    </div>
</div>
<div class="row center" style="clear: both; padding: 10px;">
    <asp:Button ID="btnChange" runat="server" OnClick="btnChange_Click" CssClass="btn btn-submit" Text="<%$ Resources:labels, change %>" />
    &nbsp;&nbsp;
    <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" CssClass="btn" Text="<%$ Resources:labels, back %>" />
</div>
