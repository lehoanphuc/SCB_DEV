<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContractList_Add_Widget" %>
<link href="widgets/SEMSCustomerList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>
<script src="widgets/SEMSCustomerList/JS/tab-view.js" type="text/javascript"></script>
<script src="Widgets/PagePermission/Scripts/JScript.js" type="text/javascript"></script>
<script type="text/javascript" src="js/Validate.js"> </script>
<link href="widgets/SEMSProduct/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="widgets/IBTransactionHistory1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/lang/en.js" type="text/javascript"></script>
<link href="widgets/IBTransactionHistory1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/border-radius.css" rel="stylesheet"
    type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="widgets/SEMSCustomerList/js/common.js"> </script>
<script type="text/javascript" src="widgets/SEMSCustomerList/js/subModal.js"> </script>
<script type="text/javascript" src="widgets/SEMSCustomerList/js/commonjs.js"> </script>
<link href="widgets/SEMSCustomerList/css/style.css" rel="stylesheet" type="text/css">
<!-- Add this to have a specific theme-->
<link href="widgets/SEMSCustomerList/css/subModal.css" rel="stylesheet" type="text/css">
<script type="text/javascript" src="widgets/SEMSCustomerListCorp/js/tabber.js"></script>
<link rel="stylesheet" href="widgets/SEMSCustomerListCorp/css/example.css" type="text/css"
    media="screen">
<script type="text/javascript">

    /* Optional: Temporarily hide the "tabber" class so it does not "flash"
    on the page as plain HTML. After tabber runs, the class is changed
    to "tabberlive" and it will appear. */

    document.write('<style type="text/css">.tabber{display:none;}<\/style>');
</script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpError" runat="server">
    <ContentTemplate>
        <div id="divCustHeader">
            <img alt="" src="widgets/SEMSContractList/Images/handshake.png" style="width: 32px; height: 32px; margin-bottom: 10px;"
                align="middle" />
            <%=Resources.labels.addnewconsumercontract %>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<br/>
<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>
<asp:Panel runat="server" ID="pnOption">
    <div class="row" runat="server" id="Div1">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>
                        <%=Resources.labels.addnewconsumercontract%>
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="pnAdd" runat="server">
                            <div class="row" style="margin-left: 20px">
                                <div class="col-sm-3">
                                    <div class="form-group custom-control">
                                        <asp:RadioButton ID="radNewCust" runat="server" Checked="True" GroupName="CustInfo"
                                            Text="<%$ Resources:labels, khachhangmoi %>" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group custom-control" >
                                        <asp:RadioButton ID="radCustExists" runat="server" GroupName="CustInfo" Visible="false" Text="<%$ Resources:labels, khachhangdatontai %>" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group custom-control">
                                        <asp:RadioButton ID="radWalletOnly" Visible="false" runat="server" GroupName="CustInfo" Text="<%$ Resources:labels, newewaletonly %>" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnStart" runat="server" Text="<%$ Resources:labels, batdau %>" OnClick="btnStart_Click" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
<br />
<!--end-->
<!-- Thong tin khach hang-->
<asp:Panel runat="server" ID="pnCustInfo">
    <div class="divGetInfoCust">
        <div class="divHeaderStyle">
            <%=Resources.labels.laythongtinkhachhangtucorebanking %>
        </div>
        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, makhachhang %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCustCode" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, tenkhachhang %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFullName" runat="server"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, sochungminh %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLicenseID" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblct" runat="server" Text="<%$ Resources:labels, loaikhachhang %>"
                        Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCustType" runat="server" Visible="False">
                    </asp:DropDownList>
                </td>
                <td>
                    <%--<asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>--%>
                </td>
                <td>
                </td>
                <td>&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="divResult">
        <div style="height: 250px; overflow: auto;">
            <asp:GridView ID="gvCustomerList" runat="server" BackColor="White" BorderColor="#CCCCCC"
                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="98%" AutoGenerateColumns="False"
                PageSize="15" OnRowDataBound="gvCustomerList_RowDataBound">
                <RowStyle ForeColor="#000000" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="cbxSelect" runat="server" GroupName="88" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, makhachhang %>" SortExpression="CUSTID">
                        <ItemTemplate>
                            <asp:HyperLink ID="lblCustCode" runat="server"></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, tenkhachhang %>" SortExpression="FULLNAME">
                        <ItemTemplate>
                            <asp:Label ID="lblCustName" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, phone %>" SortExpression="TEL">
                        <ItemTemplate>
                            <asp:Label ID="lblPhone" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, sochungminh %>" SortExpression="LICENSEID">
                        <ItemTemplate>
                            <asp:Label ID="lblIdentify" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, loaikhachhang %>" SortExpression="CFTYPE">
                        <ItemTemplate>
                            <asp:Label ID="lblCustType" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>" SortExpression="STATUS">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>--%>
                </Columns>
                <FooterStyle CssClass="gvFooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                <SelectedRowStyle />
                <HeaderStyle CssClass="gvHeader" />
            </asp:GridView>
        </div>
    </div>
    <div style="text-align: center; padding-top: 10px;">
        &nbsp;<asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="<%$ Resources:labels, next %>" />
        &nbsp;
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="<%$ Resources:labels, back %>" />
        &nbsp;
    </div>
</asp:Panel>
<!--end-->
<!--thong tin hop dong-->
<asp:Panel runat="server" ID="pnContractInfo">
    <div class="divGetInfoCust">
        <div class="divHeaderStyle">
            <%=Resources.labels.thongtinhopdong %>
        </div>
        <table class="style1" cellspacing="0" cellpadding="5">
            <tr>
                <td style="width: 25%;">
                    <asp:Label ID="Label22" runat="server" Text="<%$ Resources:labels, mahopdong %>"></asp:Label>
                    &nbsp;*
                </td>
                <td style="width: 25%;">
                    <asp:TextBox ID="txtContractNo" runat="server"></asp:TextBox>
                </td>
                <td style="width: 25%;">
                    <asp:Label ID="Label23" runat="server" Text="<%$ Resources:labels, loaihopdong %>"></asp:Label>
                    &nbsp;*
                </td>
                <td style="width: 25%;">
                    <asp:DropDownList ID="ddlContractType" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label24" runat="server" Text="<%$ Resources:labels, ngayhieuluc %>"></asp:Label>
                    &nbsp;*
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label25" runat="server" Text="<%$ Resources:labels, ngayhethan %>"></asp:Label>
                    &nbsp;*
                </td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label26" runat="server" Text="<%$ Resources:labels, loaihinhsanpham %>"></asp:Label>
                    &nbsp;
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlProduct" runat="server">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>

                    <asp:CheckBox ID="chkRenew" runat="server" Checked="True" Text="<%$ Resources:labels, autorenewlabel %>" />
                </td>
            </tr>
        </table>
    </div>
    <div style="text-align: center; padding-top: 10px;">
        &nbsp;<asp:Button ID="btnContractNext" runat="server" OnClick="btnContractNext_Click"
            OnClientClick="return validate();" Text="<%$ Resources:labels, next %>" />
        &nbsp;
        <asp:Button ID="btnContractPrevious" runat="server" OnClick="btnContractPrevious_Click"
            Text="<%$ Resources:labels, back %>" />
        &nbsp;
    </div>
    <script type="text/javascript">//<![CDATA[

        var cal = Calendar.setup({
            onSelect: function (cal) { cal.hide() }
        });
        cal.manageFields("<%=txtStartDate.ClientID %>", "<%=txtStartDate.ClientID %>", "%d/%m/%Y");
        cal.manageFields("<%=txtEndDate.ClientID %>", "<%=txtEndDate.ClientID %>", "%d/%m/%Y");

    //]]></script>
</asp:Panel>
<!--end-->
<!--Thong tin nguoi dai dien-->
<asp:Panel runat="server" ID="pnPersonal">
    <div class="tabber">
        <div class="tabbertab">
            <h2>
                <%=Resources.labels.thongtinchutaikhoan %></h2>
            <p>
                <!--Thong tin chu tai khoan-->
                <asp:Panel ID="pnChuTaiKhoan" runat="server">
                    <div class="divGetInfoCust">
                        <div class="divHeaderStyle">
                            <%=Resources.labels.thongtinnguoidung %>
                        </div>
                        <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td>
                                    <asp:Label ID="Label27" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                                    &nbsp;*
                                </td>
                                <td>
                                    <asp:TextBox ID="txtReFullName" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label32" runat="server" Text="Email"></asp:Label>
                                    &nbsp;*
                                </td>
                                <td>
                                    <asp:TextBox ID="txtReEmail" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label31" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                    &nbsp;*
                                </td>
                                <td>
                                    <asp:TextBox ID="txtReMobi" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label30" runat="server" Text="<%$ Resources:labels, gender %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReGender" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label29" runat="server" Text="<%$ Resources:labels, birthday %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtReBirth" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label33" runat="server" Text="<%$ Resources:labels, address %> "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtReAddress" runat="server" TextMode="MultiLine" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label34" runat="server" Text="<%$ Resources:labels, capbac %>" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblLevel" runat="server" Text="0" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <script type="text/javascript">//<![CDATA[

                        var cal = Calendar.setup({
                            onSelect: function (cal) { cal.hide() }
                        });

                        cal.manageFields("<%=txtReBirth.ClientID %>", "<%=txtReBirth.ClientID %>", "%d/%m/%Y");
    //]]></script>
                    <asp:Panel runat="server" ID="pnAcc" Visible="false">
                        <div id="divAccountInfo" style="margin-top: 10px;">
                            <div class="divGetInfoCust">
                                <div class="divHeaderStyle">
                                    <%=Resources.labels.taikhoandangky %>
                                </div>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:RadioButton ID="radAllAccount" Checked="true" GroupName="AccInfoCTK" runat="server"
                                                Text="<%$ Resources:labels, tatcataikhoan %>" onClick="enableACCTK();" />
                                        </td>
                                        <td style="width: 25%;"></td>
                                        <td style="width: 25%;">
                                            <asp:RadioButton ID="radAccount" GroupName="AccInfoCTK" runat="server" Text="<%$ Resources:labels, theotaikhoan %>"
                                                onClick="enableACTK();" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:DropDownList ID="ddlAccount" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <script>
                                function enableACCTK() {
                                    document.getElementById("<%=ddlAccount.ClientID %>").disabled = true;
                                }
                                function enableACTK() {

                                    document.getElementById("<%=ddlAccount.ClientID %>").disabled = false;

                                }
                            </script>
                        </div>
                    </asp:Panel>
                    <div id="divAccount" style="height: 250px; margin: 10px 5px 5px 5px;">
                        <div id="dhtmlgoodies_tabView1">
                            <%  
                                if (TabCustomerInfoHelper.TabMobileVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tblMB" class="style1" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #F5F5F5; color: #38277c;">
                                            <asp:Label ID="Label39" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #F5F5F5; color: #38277c;">
                                            <asp:Label ID="Label28" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; height: 25px" valign="top">
                                            <asp:Label ID="Label40" runat="server" Text="<%$ Resources:labels, username %>"></asp:Label>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:RadioButton ID="RbUserDefault" runat="server" GroupName="rbUserName" Checked="True" onclick="DisabledTextbox(true,'txtUserInputMB')"
                                                ClientIDMode="Static" Visible="false" />
                                            <asp:TextBox ID="txtMBUsersName" runat="server" Enabled="false"></asp:TextBox>
                                            <br />
                                            <asp:RadioButton ID="RbChangeUserName" runat="server" GroupName="rbUserName" ClientIDMode="Static" onclick="DisabledTextbox(false,'txtUserInputMB')" Visible="false" />
                                            <asp:TextBox ID="txtUserInputMB" runat="server" ClientIDMode="Static" Enabled="false" Visible="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdUserMB" runat="server" />
                                        </td>
                                        <td rowspan="4" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvMB" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label66" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMBPhoneNo" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label64" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlAccountMB" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                        </td>
                                        <td colspan="4" valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicyMB" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpolicyIB_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}

                                if (TabCustomerInfoHelper.TabWalletVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tblWL" class="style1" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #F5F5F5; color: #38277c;">
                                            <asp:Label ID="Label62" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #F5F5F5; color: #38277c;">
                                            <asp:Label ID="Label63" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; height: 25px" valign="top">
                                            <asp:Label ID="lbWL13" runat="server" Text="<%$ Resources:labels, username %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtWLPhoneNo" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td rowspan="2" style="width: 100%;" valign="top">
                                            <div style="height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvWL" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="lbWL13" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicyWL" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}

                                if (TabCustomerInfoHelper.TabSMSVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
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
                                                <asp:ListItem Value="E" Text="<%$Resources:labels, english %>"></asp:ListItem>
                                                <asp:ListItem Value="M" Text="<%$Resources:labels, myanmar %>"></asp:ListItem>
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
                                            <asp:CheckBox ID="cbCTKTKMD" runat="server" Text="<%$ Resources:labels, sudungmacdinh %>" />
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, warningoneaccountononesmsrole %>" Style="color: red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%}

                                if (TabCustomerInfoHelper.TabIBVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tblIB" class="style1" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #F5F5F5; color: #38277c;">
                                            <asp:Label ID="Label21" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #F5F5F5; color: #38277c;">
                                            <asp:Label ID="Label20" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; height: 50px" valign="top">
                                            <asp:Label ID="Label211" runat="server" Text="<%$ Resources:labels, username %>"></asp:Label>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:RadioButton ID="rbGenerate" runat="server" GroupName="rbUserName" Checked="True"
                                                ClientIDMode="Static" />
                                            <asp:TextBox ID="txtIBGenUserName" runat="server"></asp:TextBox>
                                            <br />
                                            <asp:RadioButton ID="rbType" runat="server" GroupName="rbUserName" ClientIDMode="Static" />
                                            <asp:TextBox ID="txtIBTypeUserName" runat="server"></asp:TextBox>
                                            <asp:HiddenField ID="hdfIBUserName" runat="server" />
                                        </td>
                                        <td rowspan="2" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvIB" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label60" runat="server" Font-Bold="true" ForeColor="Red" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%; padding-left: 28px">
                                            <asp:DropDownList ID="ddlpolicyIB" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlpolicyIB_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}

                                if (TabCustomerInfoHelper.TabPhoneVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tblPHO" class="style1" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #F5F5F5; color: #38277c;">
                                            <asp:Label ID="Label42" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #F5F5F5; color: #38277c;">
                                            <asp:Label ID="Label291" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label43" runat="server" Text="<%$ Resources:labels, username %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtPHOPhoneNo" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%;" rowspan="2" valign="top">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvPHO" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:DropDownList ID="ddlCTKDefaultAcctno2" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%} %>
                        </div>
                        <script type="text/javascript">
                            initTabs('dhtmlgoodies_tabView1', Array(<%=TabCustomerInfoHelper.TabName%>), 0, '100%', 188, Array(false, false, false, false, false));

                        </script>
                    </div>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel2">
                        <ProgressTemplate>
                            <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                            <%=Resources.labels.loading %>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div style="text-align: right;">
                                &nbsp;
                                <asp:Button ID="btnThemChuTaiKhoan" runat="server" Text='<%$ Resources:labels, them %>'
                                    Width="170px" OnClick="btnThemChuTaiKhoan_Click" OnClientClick="return validate1();" />
                                &nbsp;
                                <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="<%$ Resources:labels, delete %>" />
                                &nbsp;
                            </div>
                            &nbsp;&nbsp;<asp:Label runat="server" ID="lblAlert" ForeColor="Red"></asp:Label>
                            <div id="div3" style="margin: 20px 5px 5px 5px; height: 150px; overflow: auto;">
                                <asp:GridView ID="gvResultChuTaiKhoan" runat="server" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" Width="100%" OnPageIndexChanging="gvResultChuTaiKhoan_PageIndexChanging"
                                    OnRowDeleting="gvResultChuTaiKhoan_RowDeleting">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField HeaderText="<%$ Resources:labels, nguoidung %>" DataField="colFullName" />
                                        <asp:BoundField DataField="colIBUserName" HeaderText="<%$ Resources:labels, username %>" />
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
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnThemChuTaiKhoan" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="hdfIBUserName" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
        </div>
        <div class="tabbertab">
            <h2>
                <%=Resources.labels.thongtinnguoidongsohuu %></h2>
            <p>
                <!--Thong tin nguoi uy quyen-->
                <asp:Panel ID="pnNguoiUyQuyen" runat="server">
                    <div class="divGetInfoCust">
                        <div class="divHeaderStyle">
                            <%=Resources.labels.thongtinnguoidongsohuu %>
                        </div>
                        <!--11/9/2015 minh change updatepanel to panel to generate username of co owner-->
                        <asp:Panel ID="UpdatePanel1" runat="server">
                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                            <table class="style1" cellspacing="0" cellpadding="5">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label54" runat="server" Text="<%$ Resources:labels, coownercode %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCoownerCode" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnCoreownerDetail" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                                            OnClick="btnCoreownerDetail_Click" />
                                        <%--<asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1"--%>
                                        <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="0"
                                            runat="server">
                                            <ProgressTemplate>
                                                <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                                                <%=Resources.labels.loading %>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label301" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFullnameNguoiUyQuyen" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label58" runat="server" Text="Email"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmailNguoiUyQuyen" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label57" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPhoneNguoiUyQuyen" runat="server"
                                            OnTextChanged="txtPhoneNguoiUyQuyen_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label56" runat="server" Text="<%$ Resources:labels, gender %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGenderNguoiUyQuyen" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label55" runat="server" Text="<%$ Resources:labels, birthday %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBirthNguoiUyQuyen" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label59" runat="server" Text="<%$ Resources:labels, address %> "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddressNguoiUyQuyen" runat="server" TextMode="MultiLine" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label53" runat="server" Text="<%$ Resources:labels, capbac %>" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLevelNguoiUyQuyen" runat="server" Text="0" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                        </asp:Panel>
                    </div>
                    <script type="text/javascript">//<![CDATA[

                        var cal = Calendar.setup({
                            onSelect: function (cal) { cal.hide() }
                        });

                        cal.manageFields("<%=txtBirthNguoiUyQuyen.ClientID %>", "<%=txtBirthNguoiUyQuyen.ClientID %>", "%d/%m/%Y");
    //]]></script>
                    <asp:Panel runat="server" ID="PnAccUyQuyen" Visible="false">
                        <div id="divAccountSelectUyQuyen" style="margin-top: 10px;">
                            <div class="divGetInfoCust">
                                <div class="divHeaderStyle">
                                    <%=Resources.labels.taikhoandangky %>
                                </div>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:RadioButton ID="radAllAccountNguoiUyQuyen" Checked="true" GroupName="AccInfoNUQ"
                                                runat="server" Text="<%$ Resources:labels, tatcataikhoan %>" onclick="enableACNUQ();" />
                                        </td>
                                        <td style="width: 25%;"></td>
                                        <td style="width: 25%;">
                                            <asp:RadioButton ID="radAccountNguoiUyQuyen" GroupName="AccInfoNUQ" runat="server"
                                                Text="<%$ Resources:labels, account %>" onclick="enableANUQ();" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:DropDownList ID="ddlAccountUyQuyen" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <script>
                                function enableACNUQ() {
                                    document.getElementById("<%=ddlAccountUyQuyen.ClientID %>").disabled = true;
                                }
                                function enableANUQ() {

                                    document.getElementById("<%=ddlAccountUyQuyen.ClientID %>").disabled = false;

                                }
                            </script>
                        </div>
                    </asp:Panel>
                    <div id="divRoleNguoiUyQuyen" style="height: 250px; margin-top: 20px;">
                        <div id="dhtmlgoodies_tabView2">
                            <% 
                                if (TabCustomerInfoHelper.TabMobileVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table3" class="style1" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #F5F5F5; color: #38277c;">
                                            <asp:Label ID="Label47" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #F5F5F5; color: #38277c;">
                                            <asp:Label ID="Label48" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; height: 25px" valign="top">
                                            <asp:Label ID="Label65" runat="server" Text="<%$ Resources:labels, username %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%; height: 25px" valign="top">
                                            <asp:RadioButton ID="rbMBGenerateNguoiUyQuyen" runat="server" GroupName="rbUserNameNguoiUyQuyen"
                                                Checked="True" ClientIDMode="Static" onclick="DisabledTextbox(true,'txtMBTypeUserNameNguoiUyQuyen')" Visible="false" />
                                            <asp:TextBox ID="txtMBUserNguoiUyQuyen" runat="server" Enabled="false"></asp:TextBox>
                                            <br />
                                            <asp:RadioButton ID="rbMBTypeNguoiUyQuyen" runat="server"
                                                ClientIDMode="Static" Visible="false" />
                                            <asp:TextBox ID="txtMBGenUserNameNguoiUyQuyen" runat="server" ClientIDMode="Static" Visible="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdfMBUserNameNguoiUyQuyen" runat="server" />
                                        </td>
                                        <td rowspan="5" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvMBUyQuyen" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMBPhoneNguoiUyQuyen" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlAccUyQuyen" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                        </td>
                                        <td valign="top" style="width: 25%">
                                            <asp:DropDownList ID="ddlpolicyMBco" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpolicyMBco_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabWalletVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table3" class="style1" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #F5F5F5; color: #38277c;">
                                            <asp:Label ID="Label147" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #F5F5F5; color: #38277c;">
                                            <asp:Label ID="Label148" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; height: 25px" valign="top">
                                            <asp:Label ID="Label149" runat="server" Text="<%$ Resources:labels, username %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtWLNguoiUyQuyen" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td rowspan="4" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvWLUyQuyen" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label65" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicyWLco" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabSMSVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table2" class="style1" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #F5F5F5; color: #38277c; width: 50%;">
                                            <asp:Label ID="Label341" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #F5F5F5; color: #38277c; width: 50%;">
                                            <asp:Label ID="Label37" runat="server" Font-Bold="True" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label38" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtSMSPhoneNguoiUyQuyen" runat="server"></asp:TextBox>
                                        </td>
                                        <td rowspan="5" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvSMSUyQuyen" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:Label ID="Label41" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="ddlSMSDefaultAcctnoUyQuyen" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:Label ID="Label44" runat="server" Text="<%$ Resources:labels, language %>"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="ddlDefaultLanguageNguoiUyQuyen" runat="server">
                                                <asp:ListItem Value="E" Text="<%$Resources:labels, english %>"></asp:ListItem>
                                                <asp:ListItem Value="M" Text="<%$Resources:labels, myanmar %>"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label7" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicySMSco" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:CheckBox ID="cbCTKTKMDNUQ" runat="server" Text="<%$ Resources:labels, sudungmacdinh %>" />
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Label60" runat="server" Text="<%$ Resources:labels, warningoneaccountononesmsrole %>" Style="color: red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabIBVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table1" class="style1" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #F5F5F5; color: #38277c;">
                                            <asp:Label ID="Label311" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #F5F5F5; color: #38277c;">
                                            <asp:Label ID="Label321" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; height: 50px" valign="top">
                                            <asp:Label ID="Label331" runat="server" Text="<%$ Resources:labels, username %>"></asp:Label>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:RadioButton ID="rbGenerateNguoiUyQuyen" runat="server" GroupName="rbUserNameNguoiUyQuyen"
                                                Checked="True" ClientIDMode="Static" />
                                            <asp:TextBox ID="txtIBGenUserNameNguoiUyQuyen" runat="server"></asp:TextBox>
                                            <br />
                                            <asp:RadioButton ID="rbTypeNguoiUyQuyen" runat="server" GroupName="rbUserNameNguoiUyQuyen"
                                                ClientIDMode="Static" />
                                            <asp:TextBox ID="txtIBTypeUserNameNguoiUyQuyen" runat="server"></asp:TextBox>
                                            <asp:HiddenField ID="hdfIBUserNameNguoiUyQuyen" runat="server" />
                                        </td>
                                        <td rowspan="2" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvIBUyQuyen" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label6" runat="server" Font-Bold="true" ForeColor="Red" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%; padding-left: 28px">
                                            <asp:DropDownList ID="ddlpolicyIBco" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlpolicyIBco_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabPhoneVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table4" class="style1" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #F5F5F5; color: #38277c;">
                                            <asp:Label ID="Label50" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #F5F5F5; color: #38277c;">
                                            <asp:Label ID="Label51" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label52" runat="server" Text="<%$ Resources:labels, username %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtPHOPhoneNguoiUyQuyen" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%;" rowspan="2" valign="top">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvPHOUyQuyen" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label343" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:DropDownList ID="ddlCTKDefaultAcctno" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%} %>
                        </div>
                    </div>
                    <script type="text/javascript">
                        initTabs('dhtmlgoodies_tabView2', Array(<%=TabCustomerInfoHelper.TabName%>), 0, '100%', 188, Array(false, false, false, false));

                    </script>
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upNUY">
                        <ProgressTemplate>
                            <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                            <%=Resources.labels.loading %>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel runat="server" ID="upNUY">
                        <ContentTemplate>
                            <div style="text-align: right;">
                                <asp:Button ID="btnThemNguoiUyQuyen" runat="server" OnClick="btnThemNguoiUyQuyen_Click"
                                    OnClientClick="return validate2();" Text='<%$ Resources:labels, them %>' Width="170px" />
                                &nbsp;
                                <asp:Button ID="btnHuyDSH" runat="server" OnClick="btnHuyDSH_Click" Text="<%$ Resources:labels, huy %>" />
                                &nbsp;
                                <asp:Button ID="btnResetNguoiUyQuyen" runat="server" OnClick="btnResetNguoiUyQuyen_Click"
                                    Text="<%$ Resources:labels, themmoi %>" />
                                &nbsp;
                            </div>
                            &nbsp;&nbsp;<asp:Label runat="server" ID="lblAlertDSH" ForeColor="Red"></asp:Label>
                            <div id="divResultNguoiUyQuyen" style="margin: 20px 5px 5px 5px; height: 150px; overflow: auto;">
                                <asp:GridView ID="gvResultNguoiUyQuyen" runat="server" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" Width="100%" OnPageIndexChanging="gvResultNguoiUyQuyen_PageIndexChanging"
                                    OnRowDeleting="gvResultNguoiUyQuyen_RowDeleting">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField HeaderText="<%$ Resources:labels, nguoidung %>" DataField="colFullName" />
                                        <asp:BoundField DataField="colIBUserName" HeaderText="<%$ Resources:labels, username %>" />
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
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnThemNguoiUyQuyen" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
                <!--end-->
        </div>
    </div>
    <asp:Panel runat="server" ID="pnLuu">
        <div style="text-align: center; padding-top: 10px;">
            <asp:Button ID="btnCustSave" runat="server" OnClick="btnCustSave_Click" OnClientClick="return validate1();"
                Text="<%$ Resources:labels, save %>" Width="69px" />
            &nbsp;
            <asp:Button ID="btnBack" runat="server" OnClick="Button2_Click" Text="<%$ Resources:labels, back %>" />
            &nbsp; &nbsp;
        </div>
    </asp:Panel>
</asp:Panel>
<!--end--->
<asp:HiddenField ID="hidCustID" runat="server" />
<script type="text/javascript">

    function validate() {

        if (validateEmpty('<%=txtContractNo.ClientID %>', '<%=Resources.labels.mahopdongkhongrong %>')) {
            if (validateEmpty('<%=txtStartDate.ClientID %>', '<%=Resources.labels.bannhapngayhieuluc %>')) {
                if (validateEmpty('<%=txtEndDate.ClientID %>', '<%=Resources.labels.bannhapngayhethan %>')) {




                    if (IsDateGreater('<%=txtEndDate.ClientID %>', '<%=txtStartDate.ClientID %>', '<%=Resources.labels.ngayhethanphailonhonngayhieuluc %>')) {

                    }
                    else {
                        document.getElementById('<%=txtStartDate.ClientID %>').focus();
                        return false;
                    }





                }
                else {
                    document.getElementById('<%=txtEndDate.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtStartDate.ClientID %>').focus();
                return false;
            }
        }
        else {
            document.getElementById('<%=txtContractNo.ClientID %>').focus();
            return false;
        }


    }


    function validate1() {
        try {
            if (IsNumeric('<%=txtSMSPhoneNo.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                //edit by vutran 05082014 IB,MB sample user
                if (validateEmpty('<%=txtMBPhoneNo.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                    if (IsPhoneNum('<%=txtPHOPhoneNo.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                        if (validateEmpty('<%=txtReFullName.ClientID %>', '<%=Resources.labels.bannhaptennguoidaidien %>')) {
                            if (validateEmpty('<%=txtReMobi.ClientID %>', '<%=Resources.labels.bannhapsodienthoai %>')) {
                                if (validateEmpty('<%=txtReEmail.ClientID %>', 'bannhapemail')) {

                                    //kiem tra so
                                    if (IsPhoneNum('<%=txtReMobi.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                                        //kiem tra email
                                        if (checkEmail('<%=txtReEmail.ClientID %>', '<%=Resources.labels.emailkhongdinhdang %>')) {
                                            if (document.getElementById('<%=rbType.ClientID %>').checked) {
                                                if (validateEmpty('<%=txtIBTypeUserName.ClientID %>', '<%=Resources.labels. bancannhaptendangnhap %>')) {
                                                    var minlength = '<%= int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString())%>'
                                                    var maxlength = '<%= int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString())%>'
                                                    if (document.getElementById('<%=txtIBTypeUserName.ClientID %>').value.length >= minlength && document.getElementById('<%=txtIBTypeUserName.ClientID %>').value.length <= maxlength) {

                                                    } else {
                                                        alert('<%=string.Format(Resources.labels.usernamemustbetween, ConfigurationManager.AppSettings["minlengthloginname"].ToString(), ConfigurationManager.AppSettings["maxlengthloginname"].ToString()) %>');
                                                        return false;
                                                    }
                                                } else {
                                                    return false;
                                                }
                                            }
                                        }
                                        else {
                                            //document.getElementById('<%=txtReEmail.ClientID %>').focus();
                                            return false;
                                        }
                                    }
                                    else {
                                        //document.getElementById('<%=txtReMobi.ClientID %>').focus();
                                        return false;
                                    }
                                }
                                else {
                                    //document.getElementById('<%=txtReEmail.ClientID %>').focus();
                                    return false;
                                }
                            }
                            else {
                                //document.getElementById('<%=txtReMobi.ClientID %>').focus();
                                return false;
                            }

                        }
                        else {
                            //document.getElementById('<%=txtReFullName.ClientID %>').focus();
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
            else {

                return false;
            }
        }
        catch (err) {
        }
    }


    function validate2() {
        try {
            if (IsNumeric('<%=txtSMSPhoneNguoiUyQuyen.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                //edit by vutran 05082014 IB,MB sample user
                if (validateEmpty('<%=txtMBPhoneNguoiUyQuyen.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                    if (IsPhoneNum('<%=txtPHOPhoneNguoiUyQuyen.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                        if (validateEmpty('<%=txtFullnameNguoiUyQuyen.ClientID %>', '<%=Resources.labels.bannhaptennguoidaidien %>')) {
                            if (validateEmpty('<%=txtPhoneNguoiUyQuyen.ClientID %>', '<%=Resources.labels.bannhapsodienthoai %>')) {
                                if (validateEmpty('<%=txtEmailNguoiUyQuyen.ClientID %>', '<%=Resources.labels.bannhapemail %>')) {

                                    //kiem tra so
                                    if (IsPhoneNum('<%=txtPhoneNguoiUyQuyen.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                                        //kiem tra email
                                        if (checkEmail('<%=txtEmailNguoiUyQuyen.ClientID %>', '<%=Resources.labels.emailkhongdinhdang %>')) {
                                            if (document.getElementById('<%=rbTypeNguoiUyQuyen.ClientID %>').checked) {
                                                if (validateEmpty('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>', '<%=Resources.labels. bancannhaptendangnhap %>')) {
                                                    var minlength = '<%= int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString())%>'
                                                    var maxlength = '<%= int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString())%>'
                                                    if (document.getElementById('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>').value.length >= minlength && document.getElementById('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>').value.length <= maxlength) {

                                                    } else {
                                                        alert('<%=string.Format(Resources.labels.usernamemustbetween, ConfigurationManager.AppSettings["minlengthloginname"].ToString(), ConfigurationManager.AppSettings["maxlengthloginname"].ToString()) %>');
                                                        return false;
                                                    }
                                                } else {
                                                    return false;
                                                }
                                            }

                                        }
                                        else {
                                            document.getElementById('<%=txtEmailNguoiUyQuyen.ClientID %>').focus();
                                            return false;
                                        }
                                    }
                                    else {
                                        document.getElementById('<%=txtPhoneNguoiUyQuyen.ClientID %>').focus();
                                        return false;
                                    }
                                }
                                else {
                                    document.getElementById('<%=txtEmailNguoiUyQuyen.ClientID %>').focus();
                                    return false;
                                }
                            }
                            else {
                                document.getElementById('<%=txtPhoneNguoiUyQuyen.ClientID %>').focus();
                                return false;
                            }

                        }
                        else {
                            document.getElementById('<%=txtFullnameNguoiUyQuyen.ClientID %>').focus();
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
            else {

                return false;
            }
        }
        catch (err) {
        }
    }


    function SelectRAD(obj, obj1) {
        document.getElementById('<%=hidCustID.ClientID%>').value = obj1;
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'radio' && elements[i].id != obj.id) {
                    elements[i].checked = false;
                }
            }

        }
    }
</script>
<script>
    //Edit by VuTran 04082014 IB,MB Sample user
    var minlength = <%= int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString())%>;
    var maxlength = <%= int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString())%>;
    var IBMBSameUser =<%= int.Parse(ConfigurationManager.AppSettings["IBMBSameUser"].ToString())%>;
    $(document).on("click", "#tabTabdhtmlgoodies_tabView2_2", function () {
        var elem = document.getElementById('<%=rbGenerateNguoiUyQuyen.ClientID %>')
        var txtType = document.getElementById('<%=txtIBGenUserNameNguoiUyQuyen.ClientID %>')
        var txtType1 = document.getElementById('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>')
        if (IBMBSameUser == 1) {
            if (elem.checked == true) {
                document.getElementById('<%=txtMBPhoneNguoiUyQuyen.ClientID %>').value = txtType.value;
                txtType = '=========';
            }
            else {
                document.getElementById('<%=txtMBPhoneNguoiUyQuyen.ClientID %>').value = txtType1.value;
            }
        }
    });

    function SetUserName(custcode, custtype, length) {
        var date = new Date();
        var a = ticks(date).toString().length - 10;
        var b = length - ((custcode + custtype).length);
        var c = 10 - (custcode.length);

        var un = custcode + custtype + ticks(date).toString().substring(a, a + b) + "3";
        var mb = custcode + ticks(date).toString().substring(a, a + c) + "3";
        var pho = custcode + ticks(date).toString().substring(a, a + c) + "3";

        document.getElementById('<%=txtIBGenUserNameNguoiUyQuyen.ClientID %>').value = un;

        document.getElementById('<%=txtFullnameNguoiUyQuyen.ClientID %>').value = '';
        document.getElementById('<%=txtPhoneNguoiUyQuyen.ClientID %>').value = '';
        document.getElementById('<%=txtEmailNguoiUyQuyen.ClientID %>').value = '';
        document.getElementById('<%=txtBirthNguoiUyQuyen.ClientID %>').value = '';
        document.getElementById('<%=txtAddressNguoiUyQuyen.ClientID %>').value = '';

        document.getElementById('<%=txtSMSPhoneNguoiUyQuyen .ClientID %>').value = '';
        document.getElementById('<%=txtMBPhoneNguoiUyQuyen .ClientID %>').value = mb;
        document.getElementById('<%=txtPHOPhoneNguoiUyQuyen .ClientID %>').value = pho;

        document.getElementById('<%=txtFullnameNguoiUyQuyen.ClientID %>').focus();
        return false;
    }

    function ticks(date) {

        this.day = date.getDate();
        this.month = date.getMonth() + 1;
        this.year = date.getFullYear();
        this.hour = date.getHours();
        this.minute = date.getMinutes();
        this.second = date.getSeconds();
        this.ms = date.getMilliseconds();

        this.monthToDays = function (year, month) {
            var add = 0;
            var result = 0;
            if ((year % 4 == 0) && ((year % 100 != 0) || ((year % 100 == 0) && (year % 400 == 0)))) add++;

            switch (month) {
                case 0: return 0;
                case 1: result = 31; break;
                case 2: result = 59; break;
                case 3: result = 90; break;
                case 4: result = 120; break;
                case 5: result = 151; break;
                case 6: result = 181; break;
                case 7: result = 212; break;
                case 8: result = 243; break;
                case 9: result = 273; break;
                case 10: result = 304; break;
                case 11: result = 334; break;
                case 12: result = 365; break;
            }
            if (month > 1) result += add;
            return result;
        }

        this.dateToTicks = function (year, month, day) {
            var a = parseInt((year - 1) * 365);
            var b = parseInt((year - 1) / 4);
            var c = parseInt((year - 1) / 100);
            var d = parseInt((a + b) - c);
            var e = parseInt((year - 1) / 400);
            var f = parseInt(d + e);
            var monthDays = this.monthToDays(year, month - 1);
            var g = parseInt((f + monthDays) + day);
            var h = parseInt(g - 1);
            return h * 864000000000;
        }

        this.timeToTicks = function (hour, minute, second) {
            return (((hour * 3600) + minute * 60) + second) * 10000000;
        }

        return this.dateToTicks(this.year, this.month, this.day) + this.timeToTicks(this.hour, this.minute, this.second) + (this.ms * 10000);
    }
    $(document).on("click", "#rbType", function () {
        var elem = document.getElementById('<%=rbType.ClientID %>')
        var txtType = document.getElementById('<%=txtIBTypeUserName.ClientID %>')
        var hdf = document.getElementById('<%=hdfIBUserName.ClientID %>')
        if (elem.checked == true) {
            txtType.disabled = false;
            hdf.value = txtType.value;
        }
    });
    $(document).on("click", "#rbGenerate", function () {
        var elem = document.getElementById('<%=rbGenerate.ClientID %>')
        var txtType = document.getElementById('<%=txtIBTypeUserName.ClientID %>')
        var txtGen = document.getElementById('<%=txtIBGenUserName.ClientID %>')
        var hdf = document.getElementById('<%=hdfIBUserName.ClientID %>')
        if (elem.checked == true) {
            txtType.disabled = true;
            hdf.value = txtGen.value;
        }
    });
    $(document).on("change", "#<%=txtIBTypeUserName.ClientID %>", function () {
        var txtType = document.getElementById('<%=txtIBTypeUserName.ClientID %>');
        if (txtType.value.length < minlength || txtType.value.length > maxlength) {
            alert('<%=string.Format(Resources.labels.usernamemustbetween, ConfigurationManager.AppSettings["minlengthloginname"].ToString(), ConfigurationManager.AppSettings["maxlengthloginname"].ToString()) %>');
        }
        validateCode(txtType.value);

    });

    $(document).on("click", "#rbTypeNguoiUyQuyen", function () {
        var elem = document.getElementById('<%=rbTypeNguoiUyQuyen.ClientID %>')
        var txtType = document.getElementById('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>')
        var hdf = document.getElementById('<%=hdfIBUserNameNguoiUyQuyen.ClientID %>')
        if (elem.checked == true) {
            txtType.disabled = false;
            hdf.value = txtType.value;
        }
    });
    $(document).on("click", "#rbGenerateNguoiUyQuyen", function () {
        var elem = document.getElementById('<%=rbGenerateNguoiUyQuyen.ClientID %>')
        var txtType = document.getElementById('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>')
        var txtGen = document.getElementById('<%=txtIBGenUserNameNguoiUyQuyen.ClientID %>')
        var hdf = document.getElementById('<%=hdfIBUserNameNguoiUyQuyen.ClientID %>')
        if (elem.checked == true) {
            txtType.disabled = true;
            hdf.value = txtGen.value;
        }
    });
    $(document).on("change", "#<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>", function () {
        var txtType = document.getElementById('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>');
        if (txtType.value.length < minlength || txtType.value.length > maxlength) {
            alert('<%=string.Format(Resources.labels.usernamemustbetween, ConfigurationManager.AppSettings["minlengthloginname"].ToString(), ConfigurationManager.AppSettings["maxlengthloginname"].ToString()) %>');

        }
        validateCode(txtType.value);

    });

</script>

<script src="/JS/Common.js"></script>