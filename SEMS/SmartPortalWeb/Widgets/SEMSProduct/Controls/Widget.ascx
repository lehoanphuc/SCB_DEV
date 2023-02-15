<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSProduct_Controls_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleProduct" runat="server"></asp:Label>
            </h1>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.thongtinsanpham%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.masanpham %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtmasp" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tensanpham %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txttensp" MaxLength="250" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.loaihinhsanpham %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlProductType" CssClass="form-control select2 infinity" Width="100%" OnSelectedIndexChanged="ddlProductType_OnSelectedIndexChanged" AutoPostBack="True" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.status %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlStatus" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                    <asp:ListItem Value="A" Text="<%$ Resources:labels, active %>"></asp:ListItem>
                                                    <asp:ListItem Value="I" Text="<%$ Resources:labels, inactive %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.mota %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtdesc" CssClass="form-control" TextMode="MultiLine" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group" id="lblaccount" runat="server">
                                            <label  class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.accountinggroup %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlAccGroup" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                    <div class="panel-container">
                        <asp:Panel ID="subpanel" runat="server">
                            <div class="panel-hdr">
                                <h2>
                                    <%=Resources.labels.subusertype%>
                                </h2>
                            </div>
                            <div class="panel-content form-horizontal p-b-0">
                                <div>
                                    <asp:CheckBoxList ID="cblSubUserType" CssClass="custom-control" runat="server" RepeatColumns="3"
                                                      RepeatDirection="Horizontal" Width="100%">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </asp:Panel>
               
                    </div>
                    <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                        <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="Loading(); return validate()" OnClick="btsave_Click" />
                        <asp:Button ID="btnClear" CssClass="btn btn-secondary"  Text="<%$ Resources:labels, Clear %>" OnClientClick="location.reload(true);" runat="server" />
                        <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClientClick="Loading()" OnClick="btback_OnClick" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="nav-tabs-custom">
                    <ul class="nav nav-tabs">
                        <%   if (TabProductHelper.TabMobileVisibility == 1)
                            { %>

                        <li  id="liTabMB" runat="server"><a href="#tab_1" data-toggle="tab"><%=Resources.labels.mobilebanking %></a></li>
                        <%}
                            if (TabProductHelper.TabAMVisibility == 1)
                            { %>

                        <li id="liTabAM" runat="server"><a href="#tab_3" data-toggle="tab"><%=Resources.labels.agentmerchant %></a></li>
                        <%}
                            if (TabProductHelper.TabAMVisibility == 1)
                            { %>

                        <li class="active" id="liTabIB" runat="server"><a href="#tab_4" data-toggle="tab"><%=Resources.labels.internetbanking %></a></li>
                        <%}
                        %>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane" id="tab_1">
                            <table class="style1" cellspacing="0" cellpadding="4">
                                <tr>
                                    <td colspan="4" style="background-color: #F5F5F5; color: #38277c; font-weight: bold;">
                                        <%=Resources.labels.quyensudung%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:CheckBoxList ID="cblMB" CssClass="custom-control" runat="server" RepeatColumns="4"
                                            RepeatDirection="Horizontal" Width="100%">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="tab-pane" id="tab_2">
                            <table id="tblWallet" class="style1" cellspacing="0" cellpadding="4">
                                <tr>
                                    <td colspan="4" style="background-color: #F5F5F5; color: #38277c; font-weight: bold;">
                                        <%=Resources.labels.quyensudung%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1" colspan="4">
                                        <asp:CheckBoxList ID="cblWallet" CssClass="custom-control" runat="server" RepeatColumns="4"
                                            RepeatDirection="Horizontal" Width="100%">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="tab-pane" id="tab_3">
                            <div class="dhtmlgoodies_aTab">
                                <table id="tblAM" class="style1" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="4" style="background-color: #F5F5F5; color: #38277c; font-weight: bold;">
                                            <%=Resources.labels.quyensudung%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1" colspan="4">
                                            <asp:CheckBoxList ID="cblAM" CssClass="custom-control" runat="server" RepeatColumns="4"
                                                RepeatDirection="Horizontal" Width="100%">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="tab-pane active" id="tab_4">
                            <div class="dhtmlgoodies_aTab">
                                <table id="tblIB" class="style1" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="4" style="background-color: #F5F5F5; color: #38277c; font-weight: bold;">
                                            <%=Resources.labels.quyensudung%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1" colspan="4">
                                            <asp:CheckBoxList ID="cblIB" CssClass="custom-control" runat="server" RepeatColumns="4"
                                                RepeatDirection="Horizontal" Width="100%">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
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
    function validate() {
        if (!validateEmpty('<%=txtmasp.ClientID %>', '<%=Resources.labels.bancannhapmasanpham %>')) {
            document.getElementById('<%=txtmasp.ClientID %>').focus();
            return false;
        }
        var iChars = "!@#$%^&*()+=-[]\\\';,./{}|\":<>?";
        for (var i = 0; i < document.getElementById('<%=txtmasp.ClientID%>').value.length; i++) {
            if (iChars.indexOf(document.getElementById('<%=txtmasp.ClientID%>').value.charAt(i)) != -1) {
                alert('<%=Resources.labels.productidspecialcharactervalidate %>');
                document.getElementById('<%=txtmasp.ClientID %>').focus();
                return false;
            }
        }
        if (!hasWhiteSpace('<%=txtmasp.ClientID %>', '<%=Resources.labels.productidwhitespace %>')) {
            document.getElementById('<%=txtmasp.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txttensp.ClientID %>', '<%=Resources.labels.bancannhaptensanpham %>')) {
            document.getElementById('<%=txttensp.ClientID %>').focus();
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
<style>
    #divResult {
        margin: 15px;
        border: none;
        box-shadow: none;
    }
</style>
