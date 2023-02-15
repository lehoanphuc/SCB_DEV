<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSUserApprove_Wallet_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleBranch" runat="server"></asp:Label>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="ltrError" runat="server" Text=""></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.thongtinnguoisudung%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.tendaydu  %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtReFullName" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.email %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtReEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.ngaysinh  %> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtReBirth" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.gioitinh  %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlReGender" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Value="M" Text="<%$ Resources:labels, nam %>"></asp:ListItem>
                                                    <asp:ListItem Value="F" Text="<%$ Resources:labels, nu %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.dienthoai  %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtReMobi" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.loainguoidung %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlUserType" CssClass="form-control select2 infinity" runat="server" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.diachi   %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtReAddress" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6" runat="server" visible="false">
                                        <div class="form-group">
                                            <asp:Label ID="lbltype" runat="server" class="col-sm-4 control-label"><%=Resources.labels. loainguoidung %></asp:Label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlType" CssClass="form-control select2" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblGroup" runat="server" Text="<%$ Resources:labels,group%>" CssClass="form-control" Visible="false"></asp:Label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control select2" DataTextField="GroupName" DataValueField="GroupID" Visible="false">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblStatusG" runat="server" CssClass="form-control" Visible="False"></asp:Label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lblStatus" runat="server" CssClass="form-control" Visible="False"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="div1">
            <div class="col-sm-12">
                <div class="nav-tabs-custom">
                    <ul class="nav nav-tabs">
                        <%   if (TabCustomerInfoHelper.TabMobileVisibility == 1)
                            { %>

                        <li class="active" id="liTabMB" runat="server"><a href="#tab_1" data-toggle="tab"><%=Resources.labels.mobilebanking %></a></li>
                        <%}
                            if (TabCustomerInfoHelper.TabWalletVisibility == 1)
                            { %>

                        <li id="liTabWL" runat="server"><a href="#tab_2" data-toggle="tab"><%=Resources.labels.walletbanking %></a></li>
                        <%}
                            if (TabCustomerInfoHelper.TabAMVisibility == 1)
                            { %>

                        <li id="liTabAM" runat="server"><a href="#tab_3" data-toggle="tab"><%=Resources.labels.agentmerchant %></a></li>
                        <%}
                            if (TabCustomerInfoHelper.TabSMSVisibility == 1)
                            { %>

                        <li id="liTabSMS" runat="server"><a href="#tab_4" data-toggle="tab"><%=Resources.labels.smsbanking %></a></li>
                        <%}
                            if (TabCustomerInfoHelper.TabIBVisibility == 1)
                            { %>

                        <li id="liTabIB" runat="server"><a href="#tab_5" data-toggle="tab"><%=Resources.labels.internetbanking %></a></li>
                        <%}
                        %>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active" id="tab_1">
                            <div class="panel" id="tblMB" runat="server">
                                <div class="panel-container">
                                    <div class="panel-content form-horizontal p-b-0">
                                        <asp:Panel ID="divSearch" runat="server">
                                            <div class="row">
                                                <div class="col-sm-6" style="background-color: #F5F5F5; color: #38277c;">
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <asp:Label class="col-sm-4 control-label" runat="server"><%=Resources.labels.thongtindangnhap %></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6" style="background-color: #F5F5F5; color: #38277c;">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.quyensudung %></label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 5px">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.username %></label>
                                                        <div class="col-sm-8">
                                                            <div>
                                                                <asp:TextBox ID="txtUserNameMB" runat="server" CssClass="form-control " Enabled="false" ClientIDMode="Static" AutoPostBack="true" placeholder="User Name" OnTextChanged="txtUserNameMB_TextChanged"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.phone %></label>
                                                        <div class="col-sm-8">
                                                            <div>
                                                                <asp:TextBox ID="txtMBPhoneNo" CssClass="form-control" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2" runat="server" visible="false">
                                                                <asp:RadioButton ID="RbUserDefault" runat="server" GroupName="rbUserName" Checked="True" onclick="DisabledTextbox(true,'txtUserInputMB')"
                                                                    ClientIDMode="Static" Visible="false" />
                                                            </div>
                                                            <div class="col-sm-10" runat="server" visible="false">
                                                                <asp:TextBox ID="txtMBUserName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <br />
                                                            <div class="col-sm-2" runat="server" visible="false">
                                                                <asp:RadioButton ID="RbChangeUserName" runat="server" GroupName="rbUserName" ClientIDMode="Static" onclick="DisabledTextbox(false,'txtUserInputMB')" Visible="false" />
                                                            </div>
                                                            <div class="col-sm-8" runat="server" visible="false">
                                                                <asp:TextBox ID="txtUserInputMB" runat="server" CssClass="form-control" ClientIDMode="Static" Enabled="false" Visible="false"></asp:TextBox>
                                                                <asp:HiddenField ID="hdUserMB" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.taikhoanmacdinh %></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlAccountQT" CssClass="form-control select2 infinity" Width="100%" runat="server" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.dungpolicy %></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlpolicyMB" runat="server" CssClass="form-control select2 infinity" AutoPostBack="true" OnSelectedIndexChanged="ddlpolicyIB_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 custom-control" rowspan="4">
                                                    <div class="form-group ">
                                                        <div class="col-sm-12">
                                                            <div class="custom-control">
                                                                <asp:TreeView ID="tvMBQT" runat="server">
                                                                    <SelectedNodeStyle Font-Bold="True" Font-Underline="True" />
                                                                </asp:TreeView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" id="tab_2">
                            <div class="panel" id="tblWL" runat="server">
                                <div class="panel-container">
                                    <div class="panel-content form-horizontal p-b-0">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <div class="row">
                                                <div class="col-sm-6" style="background-color: #F5F5F5">
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <asp:Label class="col-sm-4 control-label" runat="server"><%=Resources.labels.thongtindangnhap %></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6" style="background-color: #F5F5F5; color: #38277c;">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.quyensudung %></label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 5px">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.phone %></label>
                                                        <div class="col-sm-8">
                                                            <div>
                                                                <asp:TextBox ID="txtWLPhoneNo" CssClass="form-control" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.dungpolicy %></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlpolicyWL" runat="server" CssClass="form-control select2 infinity" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlpolicyIB_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6" rowspan="4">
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <asp:TreeView ID="tvWL" runat="server">
                                                                <SelectedNodeStyle Font-Bold="True" Font-Underline="True" />
                                                            </asp:TreeView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" id="tab_3">
                            <div class="panel" id="tblAM" runat="server">
                                <div class="panel-container">
                                    <div class="panel-content form-horizontal p-b-0">
                                        <asp:Panel ID="Panel3" runat="server">
                                            <div class="row">
                                                <div class="col-sm-6" style="background-color: #F5F5F5; color: #38277c;">
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <asp:Label class="col-sm-4 control-label" runat="server"><%=Resources.labels.thongtindangnhap %></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6" style="background-color: #F5F5F5; color: #38277c;">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.quyensudung %></label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 5px">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.phone %></label>
                                                        <div class="col-sm-8">
                                                            <div>
                                                                <asp:TextBox ID="txtAMPhoneNo" CssClass="form-control" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.dungpolicy %></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlpolicyAM" runat="server" CssClass="form-control select2 infinity" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="ddlpolicyIB_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6" rowspan="4">
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <asp:TreeView ID="tvAM" runat="server">
                                                                <SelectedNodeStyle Font-Bold="True" Font-Underline="True" />
                                                            </asp:TreeView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" id="tab_4">
                            <table id="tblSMS" class="style1" cellspacing="0" cellpadding="4">
                                <tr>
                                    <td colspan="2" style="background-color: #F5F5F5; color: #38277c;">
                                        <asp:Label ID="Label35" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                    </td>
                                    <td style="background-color: #F5F5F5; color: #38277c;">
                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;" valign="top">
                                        <asp:Label ID="Label36" runat="server" Text="<%$ Resources:labels, sodienthoai %>"></asp:Label>
                                    </td>
                                    <td valign="top" width="25%">
                                        <asp:TextBox ID="txtSMSPhoneNo" runat="server"></asp:TextBox>
                                    </td>
                                    <td rowspan="5" valign="top" width="50%">
                                        <div style="height: 130px; overflow: auto;">
                                            <asp:TreeView ID="tvSMSQT" runat="server">
                                            </asp:TreeView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="Label46" runat="server" Text="<%$ Resources:labels, ngonngumacdinh %>"></asp:Label>
                                    </td>
                                    <td valign="top">
                                        <asp:DropDownList ID="ddlDefaultLang" runat="server">
                                            <asp:ListItem Value="V">Vietnamese</asp:ListItem>
                                            <asp:ListItem Value="E">English</asp:ListItem>
                                            <asp:ListItem Value="M">Myanmar</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="Label45" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                    </td>
                                    <td valign="top">
                                        <asp:DropDownList ID="ddlSMSDefaultAcctno" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;" valign="top">
                                        <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                        <%=Resources.labels.dungpolicy%>
                                        <%--<asp:Label ID="Label61" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                    </td>
                                    <td valign="top" style="width: 25%;">
                                        <asp:DropDownList ID="ddlpolicySMS" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td valign="top">
                                        <asp:CheckBox ID="cbIsDefault" runat="server" Text="<%$ Resources:labels, sudungmacdinh %>" />
                                    </td>
                                    <td valign="top">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3"></td>
                                </tr>
                            </table>
                        </div>
                        <div class="tab-pane" id="tab_5">
                            <table id="tblIB" class="style1" cellspacing="0" border="0" cellpadding="4" width="100%">
                                <tr>
                                    <td colspan="2" style="background-color: #F5F5F5; color: #38277c;">
                                        <asp:Label ID="Label21" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                    </td>
                                    <td style="background-color: #F5F5F5; color: #38277c;">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="width: 25%; height: 25px">
                                        <asp:Label ID="Label20" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                    </td>
                                    <td valign="top" style="width: 25%">
                                        <asp:TextBox ID="txtIBUserName" Width="100%" runat="server"></asp:TextBox>
                                    </td>
                                    <td rowspan="2" valign="top" style="width: 50%">
                                        <div style="height: 130px; overflow: auto;">
                                            <asp:TreeView ID="tvIBQT" runat="server">
                                            </asp:TreeView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;" valign="top">
                                        <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                        <%=Resources.labels.dungpolicy%>
                                        <%--<asp:Label ID="Label60" runat="server" Font-Bold="true" ForeColor="Red" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                    </td>
                                    <td valign="top" style="width: 25%;">
                                        <asp:DropDownList ID="ddlpolicyIB" Width="100%" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div style="text-align: left; padding: 5px;">
                    &nbsp;<asp:Button ID="btnThemNQT" runat="server" Text="<%$ Resources:labels, save %>" Visible="false" CssClass="btn btn-primary" OnClick="btnThemNQT_Click" OnClientClick="return validate4();" />
                </div>
                <div class="divError">
                    <asp:Label runat="server" ID="lblAlert"></asp:Label>
                </div>
                <div id="div3" class="divResult" style="height: 150px; overflow: auto;">
                    <asp:GridView ID="gvResultQuanTri" runat="server" CssClass="table table-hover"
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" OnPageIndexChanging="gvResultQuanTri_PageIndexChanging"
                        OnRowDeleting="gvResultQuanTri_RowDeleting">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField HeaderText="<%$ Resources:labels, tennguoidung1 %>" DataField="colFullName" />
                            <asp:BoundField DataField="colPhone" HeaderText="<%$ Resources:labels, phone %>" />
                            <asp:BoundField HeaderText="<%$ Resources:labels, account %>" DataField="colAccount" />
                            <asp:BoundField HeaderText="<%$ Resources:labels, quyensudung %>" DataField="colRole" />
                        </Columns>
                        <FooterStyle CssClass="gvFooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                        <SelectedRowStyle />
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:GridView>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>
        <br />
        <div class="row" runat="server" id="tblsendinfor">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.thongtinnguoisudung%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.sendcontractinfor  %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSendinfo" CssClass="form-control select2 infinity" Visible="true" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="text-align: center; padding-top: 10px;">
            &nbsp;<asp:Button ID="btnApprove" runat="server" Text="<%$ Resources:labels, duyet %>" CssClass="btn btn-primary" OnClick="btnApprove_Click" Style="height: 26px" />
            &nbsp;<asp:Button ID="btnReject" runat="server" Text="<%$ Resources:labels, khongduyet %>" CssClass="btn btn-secondary" OnClick="btnReject_Click" />
            &nbsp;<asp:Button ID="btback" runat="server" Text="<%$ Resources:labels, back %>" CssClass="btn btn-secondary" OnClick="btback_Click" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script>
    function enableACQT() {
        document.getElementById("<%=ddlAccountQT.ClientID %>").disabled = true;
    }
    function enableAQT() {

        document.getElementById("<%=ddlAccountQT.ClientID %>").disabled = false;

    }
</script>
<script type="text/javascript">//<![CDATA[

    var cal = Calendar.setup({
        onSelect: function (cal) { cal.hide() }
    });

    cal.manageFields("<%=txtReBirth.ClientID %>", "<%=txtReBirth.ClientID %>", "%d/%m/%Y");
    //]]></script>
<!--end-->
<script type="text/javascript">

    function validate1() {
        try {
            if (IsNumeric('<%=txtSMSPhoneNo.ClientID %>','<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                if (IsNumeric('<%=txtMBPhoneNo.ClientID %>','<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                    if (validateEmpty('<%=txtReFullName.ClientID %>',' <%=Resources.labels.bancannhaptennguoisudung %>')) {
                        if (validateEmpty('<%=txtReMobi.ClientID %>',' <%=Resources.labels.bancannhapsodienthoainguoisudung %>')) {
                            if (validateEmpty('<%=txtReEmail.ClientID %>',' <%=Resources.labels.bancannhapemailnguoisudung %>')) {

                                //kiem tra so
                                if (IsNumeric('<%=txtReMobi.ClientID %>','<%=Resources.labels. sodienthoaikhongdungdinhdangso %>')) {
                                    //kiem tra email
                                    if (checkEmail('<%=txtReEmail.ClientID %>','<%=Resources.labels. emailkhongdinhdang %>')) {

                                    }
                                    else {
                                        document.getElementById('<%=txtReEmail.ClientID %>').focus();
                                        return false;
                                    }
                                }
                                else {
                                    document.getElementById('<%=txtReMobi.ClientID %>').focus();
                                    return false;
                                }
                            }
                            else {
                                document.getElementById('<%=txtReEmail.ClientID %>').focus();
                                return false;
                            }
                        }
                        else {
                            document.getElementById('<%=txtReMobi.ClientID %>').focus();
                            return false;
                        }

                    }
                    else {
                        document.getElementById('<%=txtReFullName.ClientID %>').focus();
                        return false;
                    }
                }
                else {

                    return false;
                }
            }
            else {

                return false;
            }
        }
        catch (err) {
        }
    }

    function validate4() {
        try {
            if (IsNumeric('<%=txtSMSPhoneNo.ClientID %>','<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                if (IsNumeric('<%=txtMBPhoneNo.ClientID %>','<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {

                    if (validateEmpty('<%=txtReFullName.ClientID %>',' <%=Resources.labels.bancannhaptennguoisudung %>')) {
                        if (validateEmpty('<%=txtReMobi.ClientID %>',' <%=Resources.labels.bancannhapsodienthoainguoisudung %>')) {
                            if (validateEmpty('<%=txtReEmail.ClientID %>',' <%=Resources.labels.bancannhapemailnguoisudung %>')) {

                                //kiem tra so
                                if (IsNumeric('<%=txtReMobi.ClientID %>','<%=Resources.labels. sodienthoaikhongdungdinhdangso %>')) {
                                    //kiem tra email
                                    if (checkEmail('<%=txtReEmail.ClientID %>','<%=Resources.labels. emailkhongdinhdang %>')) {
                                    }
                                    else {
                                        document.getElementById('<%=txtReEmail.ClientID %>').focus();
                                        return false;
                                    }
                                }
                                else {
                                    document.getElementById('<%=txtReMobi.ClientID %>').focus();
                                    return false;
                                }
                            }
                            else {
                                document.getElementById('<%=txtReEmail.ClientID %>').focus();
                                return false;
                            }
                        }
                        else {
                            document.getElementById('<%=txtReMobi.ClientID %>').focus();
                            return false;
                        }

                    }
                    else {
                        document.getElementById('<%=txtReFullName.ClientID %>').focus();
                        return false;
                    }
                }
                else {

                    return false;
                }
            }
            else {

                return false;
            }
        }
        catch (err) {
        }
    }  
</script>


<script type="text/javascript">
    //On Page Load

    //On UpdatePanel Refresh
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        debugger;
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                var activetab = initTabs('dhtmlgoodies_tabView1', Array(<%=TabCustomerInfoHelper.TabName%>), 0, '100%', 200, Array(false, false, false, false, false));
                showTab('dhtmlgoodies_tabView1', 1);
                showTab('dhtmlgoodies_tabView1', 0);
            }
        });
    };
    //vutt 0110016 rad check not work inside updatepanel
    function DisabledTextbox(diabled, txtID) {
        var el = document.getElementById(txtID);
        el.disabled = diabled;
    }
</script>