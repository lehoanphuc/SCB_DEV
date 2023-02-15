<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSPRODUCTROLE_Controls_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.thongtinquyensanpham%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnFocus" runat="server">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.tensanpham %> *</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlProductType" CssClass="form-control select2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductType_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.contractlevel %> *</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlContractLevel" CssClass="form-control select2" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClick="btsave_Click" />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_OnClick" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="nav-tabs-custom">
                    <ul class="nav nav-tabs">
                        <%   if (TabProductHelper.TabMobileVisibility == 1)
                            { %>

                        <li class="active" id="liTabMB" runat="server"><a href="#tab_1" data-toggle="tab"><%=Resources.labels.mobilebanking %></a></li>
                        <%}
                            if (TabProductHelper.TabWalletVisibility == 1)
                            { %>

                        <li id="liTabWL" runat="server"><a href="#tab_2" data-toggle="tab"><%=Resources.labels.walletbanking %></a></li>
                        <%}
                            if (TabProductHelper.TabAMVisibility == 1)
                            { %>

                        <li id="liTabAM" runat="server"><a href="#tab_3" data-toggle="tab"><%=Resources.labels.agentmerchant %></a></li>
                        <%}
                        %>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active" id="tab_1">
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
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
