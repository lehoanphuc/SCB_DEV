<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSUser_Wallet_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<br />
<asp:UpdatePanel ID="UpdateLog" runat="server">
    <ContentTemplate>
        <div id="divCustHeader">
            <%=Resources.labels.thongtinnguoidung %>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<br />
<asp:Panel runat="server" ID="pnPersonal">
    <div class="row" runat="server" id="Div5">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>
                        <%=Resources.labels.thongtinnguoidung%>
                    </h2>
                </div>

                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="Panel5" runat="server">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.tendaydu %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtReFullName" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <%--      <asp:Label ID="Label34" runat="server" Text="<%$ Resources:labels, capbac %> " Visible="False"></asp:Label>--%>
                                        <asp:Label ID="Label1" runat="server" class="col-sm-4 control-label" Text="Local Full Name"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtLocalName" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.phone %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtReMobi" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.gender %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlReGender" runat="server" Enabled="False" CssClass="form-control select2 infinity">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.birthday %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtReBirth" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.address %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtReAddress" CssClass="form-control" runat="server" TextMode="MultiLine" onkeyup="ValidateLimit(this,'250');" onkeyDown="ValidateLimit(this,'250');" onpaste="ValidateLimit(this,'250');" onChange="ValidateLimit(this,'250');" onmousedown="ValidateLimit(this,'250');"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:Label ID="lbltype" runat="server" class="col-sm-4 control-label" Text="<%$ Resources:labels, loainguoidung %>"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control select2 infinity" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblBranch" runat="server" class="col-sm-4 control-label" Text="<%$ Resources:labels, branch %>"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="DropDownListBranch" runat="server" CssClass="form-control select2 infinity">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.dungpolicy %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlpolicyWL" runat="server" CssClass="form-control select2 infinity" AutoPostBack="true" Width="100%"></asp:DropDownList>
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
                                        <asp:DropDownList ID="ddlUserLevel" runat="server" Width="174px" Visible="false" CssClass="form-control select2 infinity">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                              <div class="row">                            
                                <div class="col-sm-6" runat="server">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Default Account</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlDefaultAccountQT" CssClass="form-control select2 infinity" Width="100%" runat="server" Enabled ="false">
                                            </asp:DropDownList>
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

    <div class="row" runat="server" id="divAccount">
        <div class="col-sm-12">
            <div class="nav-tabs-custom">
                <ul class="nav nav-tabs">
                    <%   if (TabCustomerInfoHelperWalletOnly.TabWalletVisibility == 1)
                        { %>

                    <li class="active" id="liTabWL" runat="server"><a href="#tab_1" data-toggle="tab"><%=Resources.labels.walletbanking %></a></li>
                    <%}
                        if (TabCustomerInfoHelperWalletOnly.TabSMSVisibility == 1)
                        { %>

                    <li id="liTabSMS" runat="server"><a href="#tabNUQ_2" data-toggle="tab_2"><%=Resources.labels.smsbanking %></a></li>
                    <%}
                    %>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="tab_1">
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
                                                            <div class="form-group" runat="server">
                                                                <div class="col-sm-10">
                                                                    <asp:TextBox ID="txtWLPhoneNo" onkeypress="return isNumberKeyNumer(event)" CssClass="form-control" runat="server" ClientIDMode="Static" OnTextChanged="ChangePhoneAndUserName_Click" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                                <asp:LinkButton ID="lblChangePhone" OnClick="ChangePhone_Click" runat="server">Edit</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    <div class="col-sm-4">
                                                        <div>
                                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" Visible="false" Enabled="False"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6" rowspan="4">
                                                <div class="form-group">
                                                    <div class="col-sm-12 custom-control">
                                                        <asp:TreeView ID="tvWL" runat="server">
                                                            <SelectedNodeStyle Font-Bold="True" Font-Underline="True" />
                                                            <NodeStyle CssClass="p-l-10" />
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
                    <div class="tab-pane" id="tab_2">
                        <table id="tblSMS" class="style1" cellspacing="0" cellpadding="4">
                            <tr>
                                <td colspan="2" style="background-color: #F5F5F5; color: #38277c; width: 50%;">
                                    <asp:Label ID="Label35" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                </td>
                                <td style="background-color: #F5F5F5; color: #38277c; width: 50%;">
                                    <asp:Label ID="Label271" runat="server" Font-Bold="True" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%;" valign="top">
                                    <asp:Label ID="Label36" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                </td>
                                <td style="width: 25%;" valign="top">
                                    <asp:TextBox ID="txtSMSPhoneNo" runat="server"></asp:TextBox>
                                </td>
                                <td rowspan="5" style="width: 50%;">
                                    <div style="width: 100%; height: 150px; overflow: auto;">
                                        <asp:TreeView ID="tvSMS" runat="server">
                                        </asp:TreeView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;" valign="top">
                                    <asp:Label ID="Label45" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                </td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlSMSDefaultAcctno" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;" valign="top">
                                    <asp:Label ID="Label46" runat="server" Text="<%$ Resources:labels, language %>"></asp:Label>
                                </td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlLanguage" runat="server">
                                        <asp:ListItem Value="M" Text="<%$Resources:labels, myanmar %>"></asp:ListItem>
                                        <asp:ListItem Value="E" Text="<%$Resources:labels, english %>"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%;" valign="top">
                                    <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                    <%=Resources.labels.dungpolicy %>
                                    <%--<asp:Label ID="Label61" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                </td>
                                <td valign="top" style="width: 25%;">
                                    <asp:DropDownList ID="ddlpolicySMS" runat="server"></asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 20%;" valign="top">
                                    <asp:CheckBox ID="cbIsDefault" runat="server" Text="<%$ Resources:labels, sudungmacdinh %>"
                                        Checked="True" />
                                </td>
                                <td valign="top">
                                    <asp:Label ID="Label60" runat="server" Text="<%$ Resources:labels, warningoneaccountononesmsrole %>" Style="color: red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdateProgress runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
            <%=Resources.labels.loading %>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnThemChuTaiKhoan" />
            <asp:AsyncPostBackTrigger ControlID="btnHuy" />
        </Triggers>
        <ContentTemplate>
            <div style="text-align: right;">
                &nbsp;
                                <asp:Button ID="btnThemChuTaiKhoan" runat="server" Text='<%$ Resources:labels, them %>'
                                    OnClick="btnThemChuTaiKhoan_Click" CssClass="btn btn-primary" />
                &nbsp;
                                <asp:Button ID="btnHuy" runat="server" CssClass="btn btn-secondary" OnClick="btnHuy_Click" Text="<%$ Resources:labels, delete %>" />
                &nbsp;
            </div>
            &nbsp;&nbsp;<asp:Label runat="server" ID="lblAlert" ForeColor="Red"></asp:Label>
            <div id="div3" style="margin-top: 20px; height: 150px; overflow: auto;">
                <asp:GridView ID="gvResultChuTaiKhoan" runat="server" AutoGenerateColumns="False" CssClass="table table-hover"
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                    CellPadding="3" Width="100%"
                    OnPageIndexChanging="gvResultChuTaiKhoan_PageIndexChanging"
                    OnRowDeleting="gvResultChuTaiKhoan_RowDeleting">
                    <RowStyle ForeColor="#000066" />
                    <Columns>
                        <asp:BoundField HeaderText="<%$ Resources:labels, tennguoidung1 %>" DataField="colFullName" />
                        <asp:BoundField DataField="colPhone" HeaderText="<%$ Resources:labels, username %>" />
                        <asp:BoundField HeaderText="<%$ Resources:labels, taikhoan %>" DataField="colAccount" />
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
            <div class="row" runat="server" id="divReject" visible="false">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>Reject Reason </h2>
                    </div>
                    <div class="tab-content">
                        <div class="tab-pane active">
                            <div class="panel-container">
                                <div class="panel-content form-horizontal p-b-0">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="row">
                                            <div class="col-sm-2"></div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label">Reason name</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlReason" runat="server" Enabled="false" CssClass="form-control select2">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-2"></div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-2"></div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label">Description</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" Enabled="false" TextMode="MultiLine" onkeyup="ValidateLimit(this,'250');" onkeyDown="ValidateLimit(this,'250');" onpaste="ValidateLimit(this,'250');" onChange="ValidateLimit(this,'250');" onmousedown="ValidateLimit(this,'250');"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-2"></div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <asp:Panel runat="server" ID="pnLuu">
        <div style="text-align: center; padding-top: 10px;">
            <asp:Button ID="btnCustSave" runat="server" OnClick="btnCustSave_Click" OnClientClick="return validate1();"
                Text="<%$ Resources:labels, save %>" Width="69px" CssClass="btn btn-primary" />
            &nbsp;
            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="<%$ Resources:labels, back %>" CssClass="btn btn-secondary" />
            &nbsp; &nbsp;
        </div>
    </asp:Panel>
</asp:Panel>

