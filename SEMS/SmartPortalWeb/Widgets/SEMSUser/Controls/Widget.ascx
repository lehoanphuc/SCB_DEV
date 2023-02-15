<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSUser_Controls_Widget" %>
<script src="Widgets/PagePermission/Scripts/JScript.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/lang/en.js" type="text/javascript"></script>
<script type="text/javascript" src="widgets/SEMSUser/js/common.js"> </script>
<script type="text/javascript" src="widgets/SEMSUser/js/commonjs.js"> </script>
<!-- Add this to have a specific theme-->
<link href="widgets/IBCorpUser/css/subModal.css" rel="stylesheet" type="text/css">
<script src="/JS/Common.js"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleUser" runat="server"></asp:Label>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1"
                runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel runat="server" ID="pnPersonal">
    <ContentTemplate>
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
                                <%if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"] != null && SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"] == "add")
                                    { %>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.customercode %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtCustomerCode" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Button ID="btnCustomerDetail" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, xemchitiet %>"
                                            OnClick="btnCustomerDetail_Click" />

                                    </div>
                                </div>
                                <%} %>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.fullname %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtReFullName" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6" runat="server"  id ="divnamecsm">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.qrcodename %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtLocalFullName" CssClass="form-control" placeholder="<%$ Resources:labels, qrcodename %>" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6" runat="server" id ="divnamenameagent" visible="false">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.agentmerchantname %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtagentmerchantname" CssClass="form-control" placeholder="<%$ Resources:labels, agentmerchantname %>" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required">Birthday</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtReBirth" CssClass="form-control datetimepicker " runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Gender</label>
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
                                            <label class="col-sm-4 control-label required">Phone</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtReMobi" CssClass="form-control" runat="server" OnTextChanged="txtReMobi_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">UserType</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlUserType" runat="server" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged"
                                                    AutoPostBack="True" CssClass="form-control select2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Address</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtReAddress" CssClass="form-control" runat="server" TextMode="MultiLine" onkeyup="ValidateLimit(this,'250');" onkeyDown="ValidateLimit(this,'250');" onpaste="ValidateLimit(this,'250');" onChange="ValidateLimit(this,'250');" onmousedown="ValidateLimit(this,'250');"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Branch</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="DropDownListBranch" runat="server" CssClass="form-control select2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.account %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlAccountQT" CssClass="form-control select2 infinity" Width="100%" runat="server">  

                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <asp:Label class="col-sm-4 control-label" runat="server" ID="Label32">Email</asp:Label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtReEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                 <div class="row">
                                        <div class="col-sm-6" runat="server">
                                            <div class="form-group"  >
                                               <label class="col-sm-4 control-label required"  runat="server" id="lblDefaultAccount" >Default Account</label>
                                                  <div class="col-sm-8">
                                                      <asp:DropDownList ID="ddlDefaultAccountQT" CssClass="form-control select2 infinity" Width="100%" runat="server" >
                                                         </asp:DropDownList>
                                                  </div>
                                             </div>
                                         </div>                                                       
                                  </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group" runat="server" visible="false">
                                            <label class="col-sm-4 control-label" runat="server" id="lbltype"><%=Resources.labels.loainguoidung %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                                    AutoPostBack="True" CssClass="form-control select2">
                                                    <asp:ListItem Value="<%$ Resources:labels, CONTRACTCORPMATRIX %>">Matrix</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group" runat="server" visible="false">
                                            <label class="col-sm-4 control-label" runat="server" id="lblGroup"><%=Resources.labels.group %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control select2" DataTextField="GroupName" DataValueField="GroupID">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group" runat="server" visible="false">
                                            <label class="col-sm-4 control-label" runat="server" id="lblLevel"><%=Resources.labels.bac %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlUserLevel" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Value="0" Text="<%$ Resources:labels, chutaikhoan %>"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="<%$ Resources:labels, nguoiuyquyen %>"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="<%$ Resources:labels, nguoidungcap2 %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
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
                                                        <label class="col-sm-4 control-label required"><%=Resources.labels.username %></label>
                                                        <div class="col-sm-8">
                                                            <div class="form-group" runat="server">
                                                                <div class="col-sm-10">
                                                                    <asp:TextBox ID="txtUserNameMB" onkeypress="return isKey(event)" runat="server" CssClass="form-control " ClientIDMode="Static" MaxLength="50" AutoPostBack="true" placeholder="User Name" Enabled="false" OnTextChanged="CheckUserNameChange_Click"></asp:TextBox>
                                                                    <asp:TextBox ID="txtUserID" runat="server" Visible="false"></asp:TextBox>
                                                                </div>
                                                                <asp:LinkButton ID="lbCreateusername" OnClick="CreateUserName_Click" runat="server">Generate</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label required"><%=Resources.labels.phone %></label>
                                                        <div class="col-sm-8">
                                                            <div class="form-group" runat="server">
                                                                <div class="col-sm-10">
                                                                    <asp:TextBox ID="txtMBPhoneNo" onkeypress="return isNumberKeyNumer(event)" CssClass="form-control" runat="server" OnTextChanged="CheckPhoneChange_Click" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                                <asp:LinkButton ID="lblChangePhone" OnClick="ChangePhone_Click" runat="server">Edit</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="pnLoginMethod" runat="server">
                                                        <label class="col-sm-4 control-label required"><%=Resources.labels.loginmethod %></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlLoginMethod" CssClass="form-control select2 infinity" AutoPostBack="true" OnSelectedIndexChanged="ddlLoginMethod_Click" Width="100%" runat="server">
                                                                <asp:ListItem Value="USERNAME" Text="User Name"></asp:ListItem>
                                                                <asp:ListItem Value="PHONENO" Text="Phone"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" runat="server" visible="false">
                                                        <label class="col-sm-4 control-label required"><%=Resources.labels.authentype %></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlauthenType" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                                <asp:ListItem Value="PASSWORD" Text="Password"></asp:ListItem>
                                                                <asp:ListItem Value="PINCODE" Text="Pincode"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label">Policy </label>
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
                                                                    <NodeStyle CssClass="p-l-10" />
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
                                                        <label class="col-sm-4 control-label required"><%=Resources.labels.phone %></label>
                                                        <div class="col-sm-8">
                                                            <div>
                                                                <asp:TextBox ID="txtWLPhoneNo" CssClass="form-control" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
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
                                                        <label class="col-sm-4 control-label required"><%=Resources.labels.phone %></label>
                                                        <div class="col-sm-8">
                                                            <div>
                                                                <asp:TextBox ID="txtAMPhoneNo" CssClass="form-control" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6" rowspan="4">
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <asp:TreeView ID="tvAM" runat="server">
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
                        <div class="tab-pane" id="tab_4">
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
                                        <asp:DropDownList ID="ddlDefaultLang" runat="server" CssClass="form-control select2 infinity">
                                            <asp:ListItem Value="M">Myanmar</asp:ListItem>
                                            <asp:ListItem Value="E">English</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="Label45" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                    </td>
                                    <td valign="top">
                                        <asp:DropDownList ID="ddlSMSDefaultAcctno" runat="server" Width="155px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;" valign="top">
                                        <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                        <%=Resources.labels.dungpolicy %>
                                    </td>
                                    <td valign="top" style="width: 25%;">
                                        <asp:DropDownList ID="ddlpolicySMS" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:CheckBox ID="cbIsDefault" runat="server" Text="<%$ Resources:labels, sudungmacdinh %>" />
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="Label60" runat="server" Text="<%$ Resources:labels, warningoneaccountononesmsrole %>" Style="color: red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label runat="server" ID="lblerrorSMS" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td valign="top">
                                        <asp:Button ID="btn_HuySMS" runat="server" Text="<%$ Resources:labels, huy %>"
                                            Visible="false" OnClick="btn_HuySMS_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="tab-pane" id="tab_5">
                            <div class="panel" id="tblIB" runat="server">
                                <div class="panel-container">
                                    <div class="panel-content form-horizontal p-b-0">
                                        <asp:Panel ID="pn16" runat="server">
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
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.tendangnhap %></label>
                                                        <div class="col-sm-8">
                                                            <div>
                                                                <asp:TextBox ID="txtIBUserName" CssClass="form-control" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.dungpolicy %></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlpolicyIB" runat="server" Width="100%" CssClass="form-control select2 infinity" AutoPostBack="true" OnSelectedIndexChanged="ddlpolicyIB_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6" rowspan="4">
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <div class="custom-control custom-control">
                                                                <asp:TreeView ID="tvIB" runat="server">
                                                                    <SelectedNodeStyle Font-Bold="True" Font-Underline="True" />
                                                                    <NodeStyle CssClass="p-l-10" />
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
                    </div>
                </div>

            </div>
        </div>
        <div style="text-align: right;">
            &nbsp;
                    <asp:Button ID="btnThemNQT" CssClass="btn btn-primary" runat="server" Text='<%$ Resources:labels, them %>' OnClick="btnThemNQT_Click" OnClientClick="return validate4();" />
            &nbsp;
                    <asp:Button ID="btnHuy" CssClass="btn btn-secondary" runat="server" OnClick="btnHuy_Click" Text="<%$ Resources:labels, huy %>" />
            &nbsp;
        </div>
        &nbsp;&nbsp;<asp:Label runat="server" ID="lblAlert" ForeColor="Red"></asp:Label>
        <div class="row" runat="server" id="div1">
            <div class="col-sm-12">
                <div id="div3" style="margin: 10px 5px 5px 5px; height: 150px; overflow: auto;">
                    <asp:GridView ID="gvResultQuanTri" runat="server" AutoGenerateColumns="False" BackColor="White" CssClass="table table-hover"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                        OnPageIndexChanging="gvResultQuanTri_PageIndexChanging" OnRowDeleting="gvResultQuanTri_RowDeleting">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField HeaderText="<%$ Resources:labels, tennguoidung1 %>" DataField="colFullName" />
                            <asp:BoundField DataField="colPhone" HeaderText="<%$ Resources:labels, phone %>" />
                            <asp:BoundField HeaderText="<%$ Resources:labels, taikhoan %>" DataField="colAccount" />
                            <asp:BoundField HeaderText="<%$ Resources:labels, quyensudung %>" DataField="colRole" />
                        </Columns>
                        <FooterStyle CssClass="gvFooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                        <SelectedRowStyle />
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:GridView>
                </div>
            </div>
        </div>
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
                                                        <asp:TextBox ID="txtDescription" CssClass="form-control" Enabled="false" runat="server" TextMode="MultiLine" onkeyup="ValidateLimit(this,'250');" onkeyDown="ValidateLimit(this,'250');" onpaste="ValidateLimit(this,'250');" onChange="ValidateLimit(this,'250');" onmousedown="ValidateLimit(this,'250');"></asp:TextBox>
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
        <div style="text-align: center; padding-top: 10px;">
            <asp:Button ID="btsaveandcont" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, save %>"
                OnClick="btsaveandcont_Click" />
            &nbsp;
    <asp:Button ID="btback" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
            &nbsp; &nbsp;
        </div>

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID ="btsaveandcont" />
    </Triggers>
</asp:UpdatePanel>

<script type="text/javascript">    
    function validate1() {
        try {
            if (IsNumeric('<%=txtSMSPhoneNo.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                //edit by vutran 05082014 IB,MB sample user
                if (validateEmpty('<%=txtMBPhoneNo.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                    if (validateEmpty('<%=txtReFullName.ClientID %>', '<%=Resources.labels.bancannhaptennguoisudung %>')) {
                        if (validateEmpty('<%=txtReMobi.ClientID %>', '<%=Resources.labels.bancannhapsodienthoainguoisudung %>')) {
                                <%--if (validateEmpty('<%=txtReEmail.ClientID %>', '<%=Resources.labels.bancannhapemailnguoisudung %>')) {--%>

                            //kiem tra so
                            if (IsNumeric('<%=txtReMobi.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>')) {
                                        //kiem tra email
                                        <%--if (checkEmail('<%=txtReEmail.ClientID %>', '<%=Resources.labels. emailkhongdinhdang %>')) {--%>
                                        <%--}
                                        else {
                                            document.getElementById('<%=txtReEmail.ClientID %>').focus();
                                            return false;
                                        }--%>
                            }
                            else {
                                document.getElementById('<%=txtReMobi.ClientID %>').focus();
                                return false;
                            }
                               <%-- }
                                else {
                                    document.getElementById('<%=txtReEmail.ClientID %>').focus();
                                    return false;
                                }--%>
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
                document.getElementById('<%=txtSMSPhoneNo.ClientID %>').focus()
                return false;
            }
        }
        catch (err) {
        }
    }

    function validate4() {
        try {
            if (IsNumeric('<%=txtSMSPhoneNo.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                //edit by vutran 05082014 IB,MB sample user
                if (validateEmpty('<%=txtMBPhoneNo.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                    if (validateEmpty('<%=txtReMobi.ClientID %>', '<%=Resources.labels.bancannhapsodienthoainguoisudung %>')) {
                                <%--if (validateEmpty('<%=txtReEmail.ClientID %>', '<%=Resources.labels.bancannhapemailnguoisudung %>')) {--%>

                        //kiem tra so
                        if (IsNumeric('<%=txtReMobi.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>')) {
                                        //kiem tra email
                                        <%--if (checkEmail('<%=txtReEmail.ClientID %>', '<%=Resources.labels. emailkhongdinhdang %>')) {--%>


                                       <%-- }
                                        else {
                                            document.getElementById('<%=txtReEmail.ClientID %>').focus();
                                            return false;
                                        }--%>
                        }
                        else {
                            document.getElementById('<%=txtReMobi.ClientID %>').focus();
                            return false;
                        }
                               <%-- }
                                else {
                                    document.getElementById('<%=txtReEmail.ClientID %>').focus();
                                    return false;
                                }--%>
                    }
                    else {
                        document.getElementById('<%=txtReMobi.ClientID %>').focus();
                        return false;
                    }
                }
                else {

                    return false;
                }
            }
            else {
                document.getElementById('<%=txtSMSPhoneNo.ClientID %>').focus()
                return false;
            }
        }
        catch (err) {
        }
    }
</script>

<script type="text/javascript">
    function DisabledTextbox(diabled, txtID) {
        var el = document.getElementById(txtID);
        el.disabled = diabled;
    }
    function isNumberKeyNumer(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }
    function isKey(evt) {
        var regex = new RegExp("[A-Za-z0-9]");
        var key = String.fromCharCode(event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    }
</script>
