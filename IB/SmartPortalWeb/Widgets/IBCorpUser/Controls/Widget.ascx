<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUser_Controls_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<link href="widgets/IBCorpUser/CSS/Control.css" rel="stylesheet" />
<link href="CSS/css.css" rel="stylesheet" />

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="" runat="server">
    <ProgressTemplate>
        <div class="cssProgress">
            <div class="progress1">
                <div class="cssProgress-bar cssProgress-active" data-percent="100" style="transition: none; width: 100%;"></div>
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="pnCorp" runat="server">

    <ContentTemplate>

        <figure>
            <div style="text-align: center; font-weight: bold;">
                <asp:Label ID="ltrError" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div>
                <asp:HiddenField runat="server" ID="hdContractNo" />
            </div>
            <div class="content_table_4c dislay-label">
                <div class="header-title">
                    <label class="bold "><%=Resources.labels.thongtinnguoisudung %></label>
                </div>
                <div class="row ">
                    <div class="col-xs-4 col-sm-2 bold">
                        <label class="required"><%=Resources.labels.tendaydu %></label>
                    </div>
                    <div class="col-xs-8 col-sm-4">
                        <asp:TextBox ID="txtReFullName" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 col-sm-2 bold">
                        <label class="required"><%=Resources.labels.email %></label>
                    </div>
                    <div class="col-xs-8 col-sm-4">
                        <asp:TextBox ID="txtReEmail" runat="server" Width="161px"></asp:TextBox>
                    </div>
                </div>
                <div class="row ">
                    <div class="col-xs-4 col-sm-2 bold">
                        <label class="required"><%=Resources.labels.ngaysinh %></label>
                    </div>
                    <div class="col-xs-8 col-sm-4">
                        <asp:TextBox ID="txtReBirth" CssClass="dateselect15" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 col-sm-2 bold">
                        <label class="required"><%=Resources.labels.gioitinh %></label>
                    </div>
                    <div class="col-xs-8 col-sm-4">
                        <asp:DropDownList ID="ddlReGender" runat="server">
                            <asp:ListItem Value="M" Text="<%$ Resources:labels, nam %>"></asp:ListItem>
                            <asp:ListItem Value="F" Text="<%$ Resources:labels, nu %>"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row ">
                    <div class="col-xs-4 col-sm-2 bold">
                        <label class="required"><%=Resources.labels.dienthoai %></label>
                    </div>
                    <div class="col-xs-8 col-sm-4">
                        <asp:TextBox ID="txtReMobi" MaxLength="11" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 col-sm-2 bold">
                        <label class="required"><%=Resources.labels.accountgroup %></label>
                    </div>
                    <div class="col-xs-8 col-sm-4">
                        <asp:DropDownList ID="ddlGroupUser" runat="server">
                        </asp:DropDownList>
                        <asp:Label runat="server" ForeColor="Red" ID="lblvalidategroup"></asp:Label>
                    </div>
                </div>
                <div class="row ">
                    <div class="col-xs-4 col-sm-2 bold">
                        <label><%=Resources.labels.diachi %></label>
                    </div>
                    <div class="col-xs-8 col-sm-4">
                        <asp:TextBox ID="txtReAddress" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 col-sm-2 bold">
                        <asp:Label ID="Label28" runat="server" Text="<%$ Resources:labels, nguoidung %>" Visible="False"></asp:Label>
                    </div>
                    <div class="col-xs-8 col-sm-4">
                        <asp:DropDownList ID="ddlUserType" runat="server" Width="166px" Visible="False">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="">
                    <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                </div>
            </div>
            <div class="content_table_4c dislay-label">
                <div class="header-title">
                    <label class="bold"><%=Resources.labels.taikhoandangky %></label>
                </div>
                <div class="row ">
                    <div class="col-sm-2  bold">
                        <asp:Label Text="Account" runat="server" />
                    </div>
                    <div class="col-sm-4 col-xs-6 pading0">
                        <asp:ListBox ID="lstDept" runat="server" CssClass="form-control margin-bt15 " Style="min-height: 150px;"
                            AutoPostBack="True" OnSelectedIndexChanged="lstDept_OnSelectedIndexChangedxChanged"></asp:ListBox>
                        <div class="row ">
                            <div class="col-sm-6 col-xs-6 pading0 checkcs" style="padding-left: 5px">
                                <asp:CheckBox ID="isWallet" runat="server" OnCheckedChanged="cbwallet_OnCheckedChanged" checker="true" AutoPostBack="true" Text="Using Wallet" Visible="false"/>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-xs-6 pading0">
                        <div class="tree" style="max-height: 150px;">
                            <asp:TreeView PathSeparator="0" ID="tvRole" runat="server"
                                OnTreeNodeCheckChanged="tvRole_OnTreeNodeCheckChanged">
                                <SelectedNodeStyle Font-Bold="True" Font-Underline="True" />
                                <NodeStyle CssClass="p-l-10" />
                            </asp:TreeView>
                        </div>
                        <div class="tree" style="max-height: 200px;">
                            <asp:TreeView ID="tvWallet" runat="server" OnTreeNodeCheckChanged="tvWl_OnTreeNodeCheckChanged">
                                <SelectedNodeStyle Font-Bold="True" Font-Underline="True" />
                                <NodeStyle CssClass="p-l-10" />
                            </asp:TreeView>
                        </div>
                    </div>

                </div>

            </div>
        </figure>
    </ContentTemplate>
</asp:UpdatePanel>


<asp:UpdateProgress ID="UpdateProgress3" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <ul class="nav nav-tabs">
            <% if (TabCustomerInfoHelper.TabIBVisibility == 1)
                { %>
            <li class="liIB active"><a data-toggle="tab" href="#divib"><%=Resources.labels.internetbanking %></a></li>
            <%}
                if (TabCustomerInfoHelper.TabSMSVisibility == 1)
                { %>
            <li class="liSMS"><a data-toggle="tab" href="#divsms"><%= Resources.labels.smsbanking%></a></li>
            <%}
                if (TabCustomerInfoHelper.TabMobileVisibility == 1)
                { %>

            <%--<li class="liMB"><a data-toggle="tab" href="#divmb"><%= Resources.labels.mobilebanking%></a></li>--%>
            <%}
                if (TabCustomerInfoHelper.TabPhoneVisibility == 1)
                { %>

            <li class="liPHO"><a data-toggle="tab" href="#divphone"><%=Resources.labels.phonebanking %></a></li>
            <%}%>
        </ul>
        <div class="tab-content content_table_4c">
            <% if (TabCustomerInfoHelper.TabIBVisibility == 1)
                { %>
            <div id="divib" class="tab-pane fade in active">
                <div class="row">
                    <div class="header-title">
                        <label class="bold"><%= Resources.labels.thongtindangnhap %></label>
                    </div>
                    <div class="info-left">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="col-xs-4 col-sm-5 bold right">
                                    <label class="required"><%= Resources.labels.tendangnhap %></label>
                                </div>
                                <div class="col-xs-8 col-sm-7">
                                    <asp:TextBox ID="txtIBUserName" MaxLength="20" onkeypress="return isKey(event)" runat="server" AutoPostBack="True" OnTextChanged="txtIBUserName_OnTextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6">

                                <div class="col-xs-4 col-sm-5 right ">
                                    <asp:Button runat="server" CssClass="btn btn-primary" ID="genuser" Text="<%$ Resources:labels, generateuser %>" OnClick="GenUserName" />
                                </div>
                                <div class="col-xs-8 col-sm-7">
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="col-xs-4 col-sm-5 right bold">
                                    <label class="required"><%= Resources.labels.matkhau %></label>
                                </div>
                                <div class="col-xs-8 col-sm-7">
                                    <asp:TextBox ID="txtIBPass" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                                </div>
                            </div>


                            <div class="col-sm-6">
                                <div class="col-xs-4 col-sm-5 right bold">
                                    <label class="required"><%= Resources.labels.nhaplaimatkhau %></label>
                                </div>
                                <div class="col-xs-8 col-sm-7">
                                    <asp:TextBox ID="txtIBRePass" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-6">

                                <div class="col-xs-4 col-sm-5 right bold">
                                    <img alt="" src="widgets/IBCorpUser/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                    <%=Resources.labels.dungpolicy %>
                                </div>
                                <div class="col-xs-8 col-sm-7">
                                    <asp:DropDownList ID="ddlpolicyIB" runat="server" Width="155px">
                                        <asp:ListItem Text="Default" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="col-xs-4 col-sm-5 right bold">
                                    <label><%= Resources.labels.trangthai %></label>
                                </div>
                                <div class="col-xs-8 col-sm-7">
                                    <asp:DropDownList ID="ddlStatusIB" runat="server" Width="155px">
                                        <asp:ListItem Value="A" Text="<%$ Resources:labels, hoatdong %>"></asp:ListItem>
                                        <asp:ListItem Value="I" Text="<%$ Resources:labels, inactive %>"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tree" style="max-height: 200px;">
                    <asp:TreeView ID="tvIBQT" runat="server">
                    </asp:TreeView>
                </div>
                <div class="clearfix"></div>
            </div>
            <%}
                if (TabCustomerInfoHelper.TabSMSVisibility == 1)
                { %>
            <div id="divsms" class="tab-pane fade">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="header-title">
                            <label class="bold"><%= Resources.labels.thongtindangnhap %></label>
                        </div>

                        <div class="info-left">
                            <div class="row">
                                <div class="col-xs-4 col-sm-6 bold">
                                    <label><%= Resources.labels.sodienthoai %></label>
                                </div>
                                <div class="col-xs-8 col-sm-6">
                                    <asp:TextBox ID="txtSMSPhoneNo" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-4 col-sm-6 bold">
                                    <label><%= Resources.labels.ngonngumacdinh %></label>
                                </div>
                                <div class="col-xs-8 col-sm-6">
                                    <asp:DropDownList ID="ddlDefaultLang" runat="server" Width="155px">
                                        <asp:ListItem Value="M" Text="<%$Resources:labels, laos %>"></asp:ListItem>
                                        <asp:ListItem Value="E" Text="<%$Resources:labels, english %>"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-4 col-sm-6 bold">
                                    <label><%= Resources.labels.taikhoanmacdinh %></label>
                                </div>
                                <div class="col-xs-8 col-sm-6">
                                    <asp:DropDownList ID="ddlSMSDefaultAcctno" runat="server" Width="155px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-4 col-sm-6 bold">
                                    <label><%= Resources.labels.trangthai %></label>
                                </div>
                                <div class="col-xs-8 col-sm-6">
                                    <asp:DropDownList ID="ddlStatusSMS" runat="server" Width="155px">
                                        <asp:ListItem Value="A" Text="<%$ Resources:labels, hoatdong %>"></asp:ListItem>
                                        <asp:ListItem Value="I" Text="<%$ Resources:labels, inactive %>"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-4 col-sm-6 bold">
                                    <img alt="" src="widgets/IBCorpUser/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                    <%=Resources.labels.dungpolicy %>
                                </div>
                                <div class="col-xs-8 col-sm-6">
                                    <asp:DropDownList ID="ddlpolicySMS" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-4 col-sm-6 bold">
                                    <asp:CheckBox ID="cbIsDefault" runat="server" Text="<%$ Resources:labels, sudungmacdinh %>" />

                                </div>
                                <div class="col-xs-8 col-sm-6">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tree">
                        <asp:TreeView ID="tvSMSQT" runat="server">
                        </asp:TreeView>
                    </div>
                </div>
            </div>
            <%}
                if (TabCustomerInfoHelper.TabMobileVisibility == 1)
                { %>
            <div id="divmb" class="tab-pane fade">
                <div class="row">


                    <div class="header-title">
                        <label class="bold"><%= Resources.labels.thongtindangnhap %></label>
                    </div>
                    <div class="info-left">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="col-xs-4 col-sm-5 right bold">
                                    <label><%= Resources.labels.tendangnhap %></label>
                                </div>
                                <div class="col-xs-8 col-sm-7">
                                    <asp:TextBox ID="txtMBPhoneNo" Enabled="False" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="col-xs-4 col-sm-5 right bold">
                                    <label><%= Resources.labels.trangthai %></label>
                                </div>
                                <div class="col-xs-8 col-sm-7">
                                    <asp:DropDownList ID="ddlStatusMB" runat="server" Width="155px">
                                        <asp:ListItem Value="A" Text="<%$ Resources:labels, hoatdong %>"></asp:ListItem>
                                        <asp:ListItem Value="D" Text="<%$ Resources:labels, inactive %>"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row hidden">
                            <div class="col-xs-4 col-sm-4 right bold">
                                <label><%= Resources.labels.matkhau %></label>
                            </div>
                            <div class="col-xs-8 col-sm-6">
                                <asp:TextBox ID="txtMBPass" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row hidden">
                            <div class="col-xs-4 col-sm-4 right bold">
                                <label><%= Resources.labels.nhaplaimatkhau %></label>
                            </div>
                            <div class="col-xs-8 col-sm-6">
                                <asp:TextBox ID="txtMBRePass" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-sm-6">
                                <div class="col-xs-4  col-sm-5 right bold">
                                    <img alt="" src="widgets/IBCorpUser/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                    <%=Resources.labels.dungpolicy %>
                                </div>
                                <div class="col-xs-8 col-sm-7">
                                    <asp:DropDownList ID="ddlpolicyMB" runat="server">
                                        <asp:ListItem Text="Default" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>


                        </div>

                    </div>
                    <div class="tree" style="max-height: 200px;">
                        <asp:TreeView ID="tvMBQT" runat="server">
                        </asp:TreeView>
                    </div>
                    <div class="clearfix"></div>
                </div>

            </div>
            <%}
                if (TabCustomerInfoHelper.TabPhoneVisibility == 1)
                { %>
            <div id="divphone" class="tab-pane fade">

                <div class="row">
                    <div class="col-sm-12">
                        <div class="header-title">
                            <label class="bold"><%= Resources.labels.thongtindangnhap %></label>
                        </div>

                        <div class="info-left">
                            <div class="row">
                                <div class="col-xs-4 col-sm-6 bold">
                                    <label class="required"><%= Resources.labels.tendangnhap %></label>
                                </div>
                                <div class="col-xs-8 col-sm-6">
                                    <asp:TextBox ID="txtPHOPhoneNo" runat="server" Enabled="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-4 col-sm-6 bold">
                                    <label><%= Resources.labels.matkhau %></label>
                                </div>
                                <div class="col-xs-8 col-sm-6">
                                    <asp:TextBox ID="txtPHOPass" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-4 col-sm-6 bold">
                                    <label><%= Resources.labels.nhaplaimatkhau %></label>
                                </div>
                                <div class="col-xs-8 col-sm-6">
                                    <asp:TextBox ID="txtPHORepass" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-4 col-sm-6 bold">
                                    <label><%= Resources.labels.taikhoanmacdinh %></label>
                                </div>
                                <div class="col-xs-8 col-sm-6">
                                    <asp:DropDownList ID="ddlPHODefaultAcctno" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-4 col-sm-6 bold">
                                    <label><%= Resources.labels.trangthai %></label>
                                </div>
                                <div class="col-xs-8 col-sm-6">
                                    <asp:DropDownList ID="ddlStatusPHO" runat="server" Width="155px">
                                        <asp:ListItem Value="A" Text="<%$ Resources:labels, hoatdong %>"></asp:ListItem>
                                        <asp:ListItem Value="B" Text="<%$ Resources:labels, ngunghoatdong %>"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tree">
                        <asp:TreeView ID="tvPHOQT" runat="server">
                        </asp:TreeView>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <%}%>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel4">
    <ProgressTemplate>
        <img alt="" src="Images/WidgetImage/ajaxloader.gif" style="width: 16px; height: 16px;" />
        <%=Resources.labels.loading %>
    </ProgressTemplate>
</asp:UpdateProgress>

<asp:UpdatePanel ID="UpdatePanel4" runat="server">
    <ContentTemplate>
        <div style="text-align: right;">
            &nbsp;
        <asp:Button ID="btnThemNQT" CssClass="btn btn-success" runat="server" Visible="False" Text='<%$ Resources:labels, them %>' OnClick="btnThemNQT_Click"
            OnClientClick="return validate4();" />
            &nbsp;
        <asp:Button ID="btnHuy" Visible="False" runat="server" CssClass="btn btn-danger" OnClick="btnHuy_Click" Text="<%$ Resources:labels, huy %>" />
            &nbsp;
        </div>
        <asp:Label runat="server" Visible="False" ID="lblAlert" ForeColor="Red"></asp:Label>

        <div id="div3" style="margin-top: 20px; max-height: 150px; overflow: auto">
            <asp:Repeater ID="gvResultQuanTri" Visible="False" runat="server">
                <HeaderTemplate>
                    <table class="table table-bordered table-hover footable" data-paging="true">
                        <thead>
                            <tr>
                                <th><%= Resources.labels.nguoidung %></th>
                                <th><%= Resources.labels.tendangnhap %></th>
                                <th data-breakpoints="xs"><%= Resources.labels.taikhoan %></th>
                                <th data-breakpoints="xs"><%= Resources.labels.quyensudung %></th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("colFullName") %></td>
                        <td><%#Eval("colIBUserName") %></td>
                        <td><%#Eval("colAccount") %></td>
                        <td><%#Eval("colRole") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
			</table>
                </FooterTemplate>
            </asp:Repeater>
        </div>


        <div style="text-align: center; padding-top: 10px;">
            <asp:Button ID="btback" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, quaylai %>" OnClick="btback_Click" />
            <asp:Button ID="btsaveandcont" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, luu %>" OnClick="btsaveandcont_Click" OnClientClick="return validate3();" />
            <div class="clearfix"></div>
        </div>
    </ContentTemplate>

</asp:UpdatePanel>



<script type="text/javascript">
    Onload();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        Onload();
        onReady();
    }
    function Onload() {
        $('.dateselect15').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy',
            language: 'en',
            todayBtn: "linked"
        });
    }

    function validateEmpty4(id, msg) {
        if (document.getElementById(id).value == "") {
            window.alert(msg);
            return false;
        }
        else {
            return true;
        }
    }

    function IsNumeric4(sText, aler) {
        var ValidChars = "0123456789.";
        var IsNumber = true;
        var Char;
        sText = document.getElementById(sText).value;

        if (sText != '') {
            for (i = 0; i < sText.length && IsNumber == true; i++) {
                Char = sText.charAt(i);
                if (ValidChars.indexOf(Char) == -1) {
                    IsNumber = false;
                    window.alert(aler);
                    return IsNumber;
                }
            }
            return IsNumber;
        }
        else {
            return true;
        }

    }

    function checkEmail4(emailID, aler) {
        var email = document.getElementById(emailID);
        var value = document.getElementById(emailID).value;
        if (value != '') {
            var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
            if (!filter.test(email.value)) {
                alert(aler);
                return false;
            }
            return true;
        }
        else {
            return true;
        }
    }

    function validate4() {
        if (!validateEmpty4('<%=txtReFullName.ClientID %>', '<%=Resources.labels.bancannhaptennguoisudung %>')) {
            document.getElementById('<%=txtReFullName.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty4('<%=txtReEmail.ClientID %>', '<%=Resources.labels.bancannhapemailnguoisudung %>')) {
            document.getElementById('<%=txtReEmail.ClientID %>').focus();
            return false;
        }
        if (!checkEmail4('<%=txtReEmail.ClientID %>', '<%=Resources.labels.emailkhongdungdinhdang %>')) {
            document.getElementById('<%=txtReEmail.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty4('<%=txtReMobi.ClientID %>', '<%=Resources.labels.bancannhapsodienthoainguoisudung %>')) {
            document.getElementById('<%=txtReMobi.ClientID %>').focus();
            return false;
        }
        if (!IsNumeric4('<%=txtReMobi.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
            document.getElementById('<%=txtReMobi.ClientID %>').focus();
            return false;
        }
        if (document.getElementById('<%=txtIBPass.ClientID%>').value != "") {
            if (document.getElementById('<%=txtIBPass.ClientID%>').value.length < 6) {
                alert('<%=Resources.labels.passwordlength %>');
                document.getElementById('<%=txtIBPass.ClientID %>').focus();
                return false;
            }
            if (!validateEmpty4('<%=txtIBRePass.ClientID %>', '<%=Resources.labels.retypepasswordrequire %>')) {
                document.getElementById('<%=txtIBRePass.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtIBPass.ClientID%>').value != document.getElementById('<%=txtIBRePass.ClientID%>').value) {
                alert('<%=Resources.labels.passwordcompare %>');
                document.getElementById('<%=txtIBRePass.ClientID %>').focus();
                return false;
            }
        }
        return true;
    }

    function validate3() {
        if (!validateEmpty('<%=txtReFullName.ClientID %>', '<%=Resources.labels.bancannhaptennguoisudung %>')) {
            document.getElementById('<%=txtReFullName.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtReEmail.ClientID %>', '<%=Resources.labels.bancannhapemailnguoisudung %>')) {
            document.getElementById('<%=txtReEmail.ClientID %>').focus();
            return false;
        }
        if (!checkEmail4('<%=txtReEmail.ClientID %>', '<%=Resources.labels.emailkhongdungdinhdang %>')) {
            document.getElementById('<%=txtReEmail.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtReBirth.ClientID %>', '<%=Resources.labels.ngaysinhkhongdetrong %>')) {
            document.getElementById('<%=txtReBirth.ClientID %>').focus();
            return false;
        }
<%--    if (!validateyearold('<%=txtReBirth.ClientID %>', '<%=Resources.labels.birthdaylessthan18year %>'),18) {
            document.getElementById('<%=txtReBirth.ClientID %>').focus();
            return false;
        }--%>

        if (!validateEmpty('<%=txtReMobi.ClientID %>', '<%=Resources.labels.bancannhapsodienthoainguoisudung %>')) {
            document.getElementById('<%=txtReMobi.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=ddlGroupUser.ClientID %>', 'User groups are not set by default')) {
            document.getElementById('<%=ddlGroupUser.ClientID %>').focus();
            return false;
        }



        if (!IsNumeric4('<%=txtReMobi.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
            document.getElementById('<%=txtReMobi.ClientID %>').focus();
            return false;
        }

        if (document.getElementById('<%=txtIBUserName.ClientID%>').value.length < 6) {
            alert('<%=Resources.labels.usernamelength %>');
            document.getElementById('<%=txtIBUserName.ClientID %>').focus();
            return false;
        }
        if (document.getElementById('<%=txtIBUserName.ClientID%>').value.length > 20) {
            alert('<%=Resources.labels.usernamelength %>');
            document.getElementById('<%=txtIBUserName.ClientID %>').focus();
            return false;
        }
<%if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"] != null && SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"] == "add")
    { %>
        if (!validateEmpty('<%=txtIBPass.ClientID %>', '<%=Resources.labels.passwordrequire %>')) {
            document.getElementById('<%=txtIBPass.ClientID %>').focus();
            return false;
        }
        if (document.getElementById('<%=txtIBPass.ClientID%>').value != "") {
            if (document.getElementById('<%=txtIBPass.ClientID%>').value.length < 6) {
                alert('<%=Resources.labels.passwordlength %>');
                document.getElementById('<%=txtIBPass.ClientID %>').focus();
                return false;
            }
            if (!validateEmpty('<%=txtIBRePass.ClientID %>', '<%=Resources.labels.retypepasswordrequire %>')) {
                document.getElementById('<%=txtIBRePass.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtIBPass.ClientID%>').value != document.getElementById('<%=txtIBRePass.ClientID%>').value) {
                alert('<%=Resources.labels.passwordcompare %>');
                document.getElementById('<%=txtIBRePass.ClientID %>').focus();
                return false;
            }
        }
    <%} %>

        return true;
    }
</script>

<script type="text/javascript">
    function postBackByObject() {
        var o = window.event.srcElement;
        debugger;
        if (o.tagName == "INPUT" && o.type == "checkbox") {
            __doPostBack("<%=pnCorp.ClientID %>", "");
        }
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

<style>
    select[multiple], select[size] {
        height: auto;
        overflow: auto;
    }

    .checkcs input {
        margin-right: 5px;
    }
</style>
