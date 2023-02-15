﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCustomerListCorp_Add_Widget" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<link href="widgets/SEMSCustomerList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>
<script src="widgets/SEMSCustomerList/JS/tab-view.js" type="text/javascript"></script>
<script src="Widgets/PagePermission/Scripts/JScript.js" type="text/javascript"></script>
<script type="text/javascript" src="js/Validate.js"> </script>
<style type="text/css">
    .style1 {
        width: 100%;
    }

    .divGetInfoCust {
        background-color: #F8F8F8;
        border: solid 1px #B9BFC1;
        margin: 2px 5px 5px 5px;
        padding: 0px 0px 0px 2px;
    }

    .divHeaderStyle {
        background-color: #EAFAFF;
        color: #003366;
        font-weight: bold;
        margin: 0px;
        line-height: 20px;
        padding: 5px;
    }

    .gvHeader {
        text-align: left;
    }

    #divCustHeader {
        width: 100%;
        font-weight: bold;
        padding: 0px 5px 0px 5px;
    }

    #divError {
        width: 100%;
        height: 10px;
        text-align: center;
        padding: 5px 5px 5px 5px;
    }

    .hightlight {
        background-color: #EAFAFF;
        color: #003366;
    }

    .nohightlight {
        background-color: White;
    }

    .tblVDC {
        background-color: #F8F8F8;
        width: 100%;
        border: solid 1px #B9BFC1;
        margin-bottom: 5px;
    }
</style>
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
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%>
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSCustomerList/Images/messenger.png" style="width: 32px; height: 32px; margin-bottom: 10px;"
        align="middle" />
    <%=Resources.labels.themmoikhachangdoanhnghiep%>
</div>
<div id="divError">
    <%--<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
<ProgressTemplate>
<img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" /> <%=Resources.labels.loading %>
</ProgressTemplate>
</asp:UpdateProgress>--%>
    <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="#990000"></asp:Label>
</div>
<!-- Thong tin khach hang-->
<asp:Panel runat="server" ID="pnCustInfo">
    <div class="divGetInfoCust">
        <div class="divHeaderStyle">
            <%=Resources.labels.laythongtinkhachhangtucorebanking %>
        </div>
        <% if (Convert.ToString(ConfigurationManager.AppSettings["AYACorporate"]) == "1")
            {%>
        <table class="style1" cellspacing="0" cellpadding="5">
            <tr>
                <td>
                    <asp:RadioButton ID="rdaPersioner" runat="server" Checked="True" GroupName="GETINFO"
                        onclick="enablePersioner();" Text="<%$ Resources:labels, makhachhang %>" />
                </td>
                <td>
                    <asp:TextBox ID="txtPersioner" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButton ID="radTaxCode1" runat="server" GroupName="GETINFO" onclick="enableTxtTaxCode();"
                        Text="<%$ Resources:labels, hochieuchungminh %>" />
                </td>
                <td>
                    <asp:TextBox ID="txtTaxCode1" runat="server"></asp:TextBox>
                </td>
                <td style="width: 10%;">
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="radLkgCode" runat="server" GroupName="GETINFO" onclick="enableLkgCode();"
                        Text="<%$ Resources:labels, linkagecode %>" />
                </td>
                <td>
                    <asp:TextBox ID="txtLkgCode" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="radGrpCode" runat="server" GroupName="GETINFO" onclick="enableGrpCode();"
                        Text="<%$ Resources:labels, groupcode %>" />
                </td>
                <td>
                    <asp:TextBox ID="txtGrpCode" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <% }
            else
            {%>
        <table class="style1" cellspacing="0" cellpadding="5">
            <tr>
                <td>
                    <asp:RadioButton ID="radCustCode" runat="server" Checked="True" Text="<%$ Resources:labels, makhachhang %>"
                        onclick="enableCustCode();" GroupName="GETINFO" />
                </td>
                <td>
                    <asp:TextBox ID="txtCustCode" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButton ID="radTaxCode" runat="server" GroupName="GETINFO" Text="<%$ Resources:labels, gpkd %>"
                        onclick="enableTaxCode();" />
                </td>
                <td>
                    <asp:TextBox ID="txtTaxCode" runat="server"></asp:TextBox>
                </td>
                <td style="width: 10%; margin-left: 40px;">
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                        OnClick="btnSearch_Click" Style="width: 240px" />
                </td>
            </tr>
        </table>
        <%} %>
    </div>
    <script>
        //Chi Pham modify 5-2-2015
        function enableTaxCode() {
            document.getElementById("<%=txtCustCode.ClientID %>").disabled = true;
            document.getElementById("<%=txtCustCode.ClientID %>").value = "";
            document.getElementById("<%=txtTaxCode.ClientID %>").disabled = false;
        }
        function enableCustCode() {
            document.getElementById("<%=txtTaxCode.ClientID %>").disabled = true;
            document.getElementById("<%=txtCustCode.ClientID %>").disabled = false;
            document.getElementById("<%=txtTaxCode.ClientID %>").value = "";
        }
        function enablePersioner() {
            document.getElementById("<%=txtPersioner.ClientID %>").disabled = false;
            document.getElementById("<%=txtTaxCode1.ClientID %>").disabled = true;
            document.getElementById("<%=txtTaxCode1.ClientID %>").value = "";
            document.getElementById("<%=txtLkgCode.ClientID %>").disabled = true;
            document.getElementById("<%=txtLkgCode.ClientID %>").value = "";
            document.getElementById("<%=txtGrpCode.ClientID %>").disabled = true;
            document.getElementById("<%=txtGrpCode.ClientID %>").value = "";
        }
        function enableTxtTaxCode() {
            document.getElementById("<%=txtPersioner.ClientID %>").disabled = true;
            document.getElementById("<%=txtTaxCode1.ClientID %>").disabled = false;
            document.getElementById("<%=txtPersioner.ClientID %>").value = "";
            document.getElementById("<%=txtLkgCode.ClientID %>").disabled = true;
            document.getElementById("<%=txtLkgCode.ClientID %>").value = "";
            document.getElementById("<%=txtGrpCode.ClientID %>").disabled = true;
            document.getElementById("<%=txtGrpCode.ClientID %>").value = "";
        }
        function enableLkgCode() {
            document.getElementById("<%=txtPersioner.ClientID %>").disabled = true;
            document.getElementById("<%=txtPersioner.ClientID %>").value = "";
            document.getElementById("<%=txtTaxCode1.ClientID %>").disabled = true;
            document.getElementById("<%=txtTaxCode1.ClientID %>").value = "";
            document.getElementById("<%=txtLkgCode.ClientID %>").disabled = false;
            document.getElementById("<%=txtGrpCode.ClientID %>").disabled = true;
            document.getElementById("<%=txtGrpCode.ClientID %>").value = "";
        }
        function enableGrpCode() {
            document.getElementById("<%=txtPersioner.ClientID %>").disabled = true;
            document.getElementById("<%=txtPersioner.ClientID %>").value = "";
            document.getElementById("<%=txtTaxCode1.ClientID %>").disabled = true;
            document.getElementById("<%=txtTaxCode1.ClientID %>").value = "";
            document.getElementById("<%=txtLkgCode.ClientID %>").disabled = true;
            document.getElementById("<%=txtLkgCode.ClientID %>").value = "";
            document.getElementById("<%=txtGrpCode.ClientID %>").disabled = false;
        }
    </script>
    <div class="divGetInfoCust">
        <div class="divHeaderStyle">
            <%=Resources.labels.thongtinkhachhang %>
        </div>
        <table class="style1" cellspacing="0" cellpadding="5">
            <tr>
                <td style="width: 25%;">
                    <asp:Label ID="Label19" runat="server" Text="<%$ Resources:labels, makhachhang %>"></asp:Label>
                    &nbsp;*
                </td>
                <td style="width: 25%;">
                    <asp:TextBox ID="txtCustCodeInfo" runat="server" Enabled="False"></asp:TextBox>
                </td>
                <td style="width: 25%;">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                    &nbsp;*
                </td>
                <td style="width: 25%;">
                    <asp:TextBox ID="txtFullName" runat="server" Enabled="False" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, tenviettat %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtShortName" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="<%$ Resources:labels, loaikhachhang %>"></asp:Label>
                    &nbsp;*
                </td>
                <td>
                    <asp:DropDownList ID="ddlCustType" runat="server" Enabled="false">
                        <asp:ListItem Text="<%$ Resources:labels, doanhnghiep %>" Value="O" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="<%$ Resources:labels, linkage %>" Value="J"></asp:ListItem>
                        <asp:ListItem Text="<%$ Resources:labels, canhan %>" Value="P"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>&nbsp;*
                </td>
                <td>
                    <asp:TextBox ID="txtMobi" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Email"></asp:Label>&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>&nbsp;*
                </td>
                <td>
                    <asp:TextBox ID="txtResidentAddr" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, ngaythanhlap %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBirth" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, gpkd %>"></asp:Label>&nbsp;*
                </td>
                <td>
                    <asp:TextBox ID="txtIF" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;<asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, sofax %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, ghichu %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDesc" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label344" runat="server" Text="<%$ Resources:labels, chinhanh %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlBranch" runat="server" SkinID="extDDL1" Width="130px">
                        <asp:ListItem Value="0">Chi nhánh Sài Gòn</asp:ListItem>
                        <asp:ListItem Value="0">Chi nhánh Hà Nội</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
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
                <td>
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
                    &nbsp;*
                </td>
                <td>
                    <asp:DropDownList ID="ddlProduct" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label82" runat="server" Text="<%$ Resources:labels, loaidanhnghiep %>"></asp:Label>
                    &nbsp;*
                </td>
                <td>
                    <asp:DropDownList ID="ddlCorpType" runat="server">
                        <asp:ListItem Value="<%$ Resources:labels, CONTRACTCORPMATRIX %>" Selected="True">Matrix</asp:ListItem>
                        <asp:ListItem Value="<%$ Resources:labels, CONTRACTCORPADVANCE %>">Advanced</asp:ListItem>
                        <asp:ListItem Value="<%$ Resources:labels, CONTRACTCORPSIMPLE %>">Simple</asp:ListItem>
                    </asp:DropDownList>
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
        <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" OnClientClick="return validate();"
            Text="<%$ Resources:labels, next %>" />
        &nbsp; &nbsp;<asp:Button ID="Button4" runat="server" Text="<%$ Resources:labels, lamlai %>" />
        &nbsp;
    </div>
    <script type="text/javascript">//<![CDATA[

        var cal = Calendar.setup({
            onSelect: function (cal) { cal.hide() }
        });
        cal.manageFields("<%=txtStartDate.ClientID %>", "<%=txtStartDate.ClientID %>", "%d/%m/%Y");
        cal.manageFields("<%=txtEndDate.ClientID %>", "<%=txtEndDate.ClientID %>", "%d/%m/%Y");
        cal.manageFields("<%=txtBirth.ClientID %>", "<%=txtBirth.ClientID %>", "%d/%m/%Y");

    //]]></script>
</asp:Panel>
<!--end-->
<asp:Panel runat="server" ID="pnPersonal">
    <div class="tabber">
        <div class="tabbertab">
            <h2>
                <%=Resources.labels.thongtinnguoiquantrihethong %></h2>
            <p>
                <!--Thong tin chu tai khoan-->
                <asp:Panel ID="Panel2" runat="server">
                    <div class="divGetInfoCust">
                        <div class="divHeaderStyle">
                            <%=Resources.labels.thongtinnguoiquantrihethong %>
                        </div>
                        <asp:Panel ID="UpdatePanel1" runat="server">
                            <%--<ContentTemplate>--%>
                            <table class="style1" cellspacing="0" cellpadding="5">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label34" runat="server" Text="<%$ Resources:labels, administratorcode %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAdministratorCode" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnAdministratorDetail" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                                            OnClick="btnAdministratorDetail_Click" />
                                        <%--<asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1"
                                                runat="server">
                                                <ProgressTemplate>
                                                    <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px;
                                                        height: 16px;" />
                                                    <%=Resources.labels.loading %>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label61" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFullNameQT" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label86" runat="server" Text="Email"></asp:Label>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmailQT" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label85" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPhoneQT" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label84" runat="server" Text="<%$ Resources:labels, gender %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGenderQT" runat="server">
                                            <asp:ListItem Value="M" Text="<%$ Resources:labels, male %>"></asp:ListItem>
                                            <asp:ListItem Value="F" Text="<%$ Resources:labels, female %>"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlLevelQT" runat="server" Visible="False">
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label83" runat="server" Text="<%$ Resources:labels, birthday %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBirthQT" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label87" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddressQT" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <%--</ContentTemplate>--%>
                        </asp:Panel>
                    </div>
                    <script type="text/javascript">//<![CDATA[

                        var cal = Calendar.setup({
                            onSelect: function (cal) { cal.hide() }
                        });

                        cal.manageFields("<%=txtBirthQT.ClientID %>", "<%=txtBirthQT.ClientID %>", "%d/%m/%Y");
                        //]]></script>

                    <div id="div4" style="margin-top: 10px;">
                        <div class="divGetInfoCust">
                            <div class="divHeaderStyle">
                                <%=Resources.labels.taikhoandangky %>
                            </div>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 25%;">
                                        <asp:RadioButton ID="radAllAccountQT" Checked="true" GroupName="AccInfoQT" runat="server"
                                            Text="<%$ Resources:labels, tatcataikhoan %>" onClick="enableACQT();" />
                                    </td>
                                    <td style="width: 25%;"></td>
                                    <td style="width: 25%;">
                                        <asp:RadioButton ID="radAccountQT" GroupName="AccInfoQT" runat="server" Text="<%$ Resources:labels, theotaikhoan %>"
                                            onClick="enableAQT();" />
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:DropDownList ID="ddlAccountQT" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <script>
                            function enableACQT() {
                                document.getElementById("<%=ddlAccountQT.ClientID %>").disabled = true;
                            }
                            function enableAQT() {

                                document.getElementById("<%=ddlAccountQT.ClientID %>").disabled = false;

                            }
                        </script>
                    </div>

                    <div id="div5" style="height: 250px; margin-top: 10px;">
                        <div id="dhtmlgoodies_tabViewCTK">
                            <% if (TabCustomerInfoHelper.TabIBVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table9" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label88" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label89" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; height: 50px" valign="top">
                                            <asp:Label ID="Label90" runat="server" Text="<%$ Resources:labels, username  %>"></asp:Label>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:RadioButton ID="rbGenerateQT" runat="server" GroupName="rbUserNameQT" Checked="True" onclick="DisabledTextbox(true,'txtIBTypeUserNameQT')"
                                                ClientIDMode="Static" />
                                            <asp:TextBox ID="txtIBGenUserNameQT" runat="server"></asp:TextBox>
                                            <br />
                                            <asp:RadioButton ID="rbTypeQT" runat="server" GroupName="rbUserNameQT" ClientIDMode="Static" onclick="DisabledTextbox(false,'txtIBTypeUserNameQT')"/>
                                            <asp:TextBox ID="txtIBTypeUserNameQT" runat="server" ClientIDMode="Static"></asp:TextBox>
                                            <asp:HiddenField ID="hdfIBUserNameQT" runat="server" />
                                        </td>
                                        <td rowspan="2" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvIBQT" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label171" runat="server" Font-Bold="true" ForeColor="Red" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%; padding-left: 28px">
                                            <asp:DropDownList ID="ddlpolicyIBqt" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpolicyIBqt_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabSMSVisibility == 1 && Convert.ToString(ConfigurationManager.AppSettings["tabSMSVisibilityCorp"]) == "1")
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table10" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #EAFAFF; color: #003366; width: 50%;">
                                            <asp:Label ID="Label91" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #EAFAFF; color: #003366; width: 50%;">
                                            <asp:Label ID="Label92" runat="server" Font-Bold="True" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label93" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtSMSPhoneQT" runat="server"></asp:TextBox>
                                        </td>
                                        <td rowspan="5" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvSMSQT" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:Label ID="Label94" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="ddlDefaultAccountQT" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:Label ID="Label95" runat="server" Text="<%$ Resources:labels, language %>"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="ddlDefaultLangQT" runat="server">
                                                <asp:ListItem Value="E">English</asp:ListItem>
                                                <asp:ListItem Value="M">Myanmar</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label178" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicySMSqt" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:CheckBox ID="cbQTHTIsDefault" runat="server" Text="<%$ Resources:labels, sudungmacdinh %>"
                                                Checked="True" />
                                        </td>
                                        <td valign="top">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabMobileVisibility == 1 && Convert.ToString(ConfigurationManager.AppSettings["tabMobileVisibilityCorp"]) == "1")
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table11" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label96" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label97" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label98" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtMBPhoneQT" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td rowspan="2" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvMBQT" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label179" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicyMBqt" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabPhoneVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table12" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label99" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label100" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label101" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtPHOPhoneQT" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%;" rowspan="2" valign="top">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvPHOQT" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label102" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:DropDownList ID="ddlQTHTPHODefaultAcctno" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%} %>
                        </div>
                        <script type="text/javascript">
                            initTabs('dhtmlgoodies_tabViewCTK', Array(<%=TabCustomerInfoHelper.TabName%>), 0, 967, 188, Array(false, false, false, false));

                        </script>
                    </div>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel4">
                        <ProgressTemplate>
                            <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                            <%=Resources.labels.loading %>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div style="text-align: right;">
                                &nbsp;
                                <asp:Button ID="btnThemNQT" runat="server" Text='<%$ Resources:labels, them %>' Width="170px"
                                    OnClick="btnThemNQT_Click" OnClientClick="return validate4();" />
                                &nbsp;
                                <asp:Button ID="btnHuy" runat="server" OnClick="btnHuy_Click" Text="<%$ Resources:labels, huy %>" />
                                &nbsp;
                            </div>
                            <asp:Label runat="server" ID="lblAlertQTHT" ForeColor="Red"></asp:Label>
                            <div id="div3" style="margin-top: 20px; width: 100%; height: 150px; overflow: auto;">
                                <asp:GridView ID="gvResultQuanTri" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                    OnPageIndexChanging="gvResultQuanTri_PageIndexChanging" OnRowDeleting="gvResultQuanTri_RowDeleting">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField HeaderText="<%$ Resources:labels, nguoidung %>" DataField="colFullName" />
                                        <asp:BoundField DataField="colIBUserName" HeaderText="<%$ Resources:labels, username %>" />
                                        <asp:BoundField HeaderText="<%$ Resources:labels, taikhoan %>" DataField="colAccount" />
                                        <asp:BoundField HeaderText="<%$ Resources:labels, quyensudung %>" DataField="colRole" />
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#E0ECFF" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <!--end-->
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
            </p>
        </div>
        <div class="tabbertab">
            <h2>
                <%=Resources.labels.thongtinchutaikhoan %></h2>
            <p>
                <!--Thong tin chu tai khoan-->
                <asp:Panel ID="pnChuTaiKhoan" runat="server">
                    <div class="divGetInfoCust">
                        <div class="divHeaderStyle">
                            <%=Resources.labels.thongtinchutaikhoan %>
                        </div>
                        <asp:Panel ID="UpdatePanelk5" runat="server">
                            <%-- <ContentTemplate>--%>
                            <table class="style1" cellspacing="0" cellpadding="5">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label53" runat="server" Text="<%$ Resources:labels, ownercode %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOwnerCode" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnOwnerDetail" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                                            OnClick="btnOwnerDetail_Click" />
                                        <%--<asp:UpdateProgress ID="UpdateProgress6" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel5"
                                                runat="server">
                                                <ProgressTemplate>
                                                    <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px;
                                                        height: 16px;" />
                                                    <%=Resources.labels.loading %>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>--%>
                                    </td>
                                </tr>
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
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReEmail" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblLevel" runat="server" Text="1" Visible="False"></asp:Label>
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
                                            <asp:ListItem Value="M" Text="<%$ Resources:labels, nam%>"></asp:ListItem>
                                            <asp:ListItem Value="F" Text="<%$ Resources:labels, nu%>"></asp:ListItem>
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
                                        <asp:Label ID="Label33" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <%-- </ContentTemplate>--%>
                        </asp:Panel>
                    </div>
                    <script type="text/javascript">//<![CDATA[

                        var cal = Calendar.setup({
                            onSelect: function (cal) { cal.hide() }
                        });

                        cal.manageFields("<%=txtReBirth.ClientID %>", "<%=txtReBirth.ClientID %>", "%d/%m/%Y");
    //]]></script>
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
                    <div id="divAccount" style="height: 250px; margin-top: 10px;">
                        <div id="dhtmlgoodies_tabView1">
                            <% if (TabCustomerInfoHelper.TabIBVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tblIB" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label21" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; height: 50px" valign="top">
                                            <asp:Label ID="Label20" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%; height: 50px" valign="top">
                                            <!--<asp:TextBox ID="txtIBUserName" runat="server"></asp:TextBox>-->
                                            <asp:RadioButton ID="rbGenerate" runat="server" GroupName="rbUserName" Checked="True" onclick="DisabledTextbox(true,'txtIBTypeUserName')"
                                                ClientIDMode="Static" />
                                            <asp:TextBox ID="txtIBGenUserName" runat="server"></asp:TextBox>
                                            <br />
                                            <asp:RadioButton ID="rbType" runat="server" GroupName="rbUserName" ClientIDMode="Static" onclick="DisabledTextbox(false,'txtIBTypeUserName')" />
                                            <asp:TextBox ID="txtIBTypeUserName" runat="server" ClientIDMode="Static"></asp:TextBox>
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
                                            <%--<asp:Label ID="Label172" runat="server" Font-Bold="true" ForeColor="Red" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%; padding-left: 28px">
                                            <asp:DropDownList ID="ddlpolicyIB" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabSMSVisibility == 1 && Convert.ToString(ConfigurationManager.AppSettings["tabSMSVisibilityCorp"]) == "1")
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tblSMS" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #EAFAFF; color: #003366; width: 50%;">
                                            <asp:Label ID="Label35" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #EAFAFF; color: #003366; width: 50%;">
                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
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
                                                <asp:ListItem Value="E">English</asp:ListItem>
                                                <asp:ListItem Value="M">Myanmar</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label180" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicySMS" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:CheckBox ID="cbCTKIsDefault" runat="server" Text="<%$ Resources:labels, sudungmacdinh %>"
                                                Checked="True" />
                                        </td>
                                        <td valign="top">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabMobileVisibility == 1 && Convert.ToString(ConfigurationManager.AppSettings["tabMobileVisibilityCorp"]) == "1")
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tblMB" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label39" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label9" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; height: 25px" valign="top">
                                            <asp:Label ID="Label40" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtMBPhoneNo" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvMB" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label181" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicyMB" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabPhoneVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tblPHO" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label42" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label10" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label43" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
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
                                            <asp:Label ID="Label103" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:DropDownList ID="ddlCTKPHODefaultAcctno" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%} %>
                        </div>
                        <script type="text/javascript">
                            initTabs('dhtmlgoodies_tabView1', Array(<%=TabCustomerInfoHelper.TabName%>), 0, 948, 188, Array(false, false, false, false));

                        </script>
                    </div>
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel2">
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
                                <asp:Button ID="btnHuyCTK" runat="server" OnClick="btnHuyCTK_Click" Text="<%$ Resources:labels, huy %>" />
                                &nbsp;
                            </div>
                            <asp:Label runat="server" ID="lblAlertCTK" ForeColor="Red"></asp:Label>
                            <div id="div6" style="margin-top: 20px; height: 150px; overflow: auto;">
                                <asp:GridView ID="gvResultChuTaiKhoan" runat="server" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" Width="100%" OnPageIndexChanging="gvResultChuTaiKhoan_PageIndexChanging"
                                    OnRowDeleting="gvResultChuTaiKhoan_RowDeleting">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField HeaderText="<%$ Resources:labels, nguoidung %>" DataField="colFullName" />
                                        <asp:BoundField DataField="colIBUserName" HeaderText="<%$ Resources:labels, username %>" />
                                        <asp:BoundField HeaderText="<%$ Resources:labels, taikhoan %>" DataField="colAccount" />
                                        <asp:BoundField HeaderText="<%$ Resources:labels, quyensudung %>" DataField="colRole" />
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#E0ECFF" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnThemChuTaiKhoan" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
                <!--end-->
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
            </p>
        </div>
        <div class="tabbertab">
            <h2>
                <%=Resources.labels.thongtinnguoiuyquyen %></h2>
            <p>
                <!--Thong tin nguoi uy quyen-->
                <asp:Panel ID="pnNguoiUyQuyen" runat="server">
                    <div class="divGetInfoCust">
                        <div class="divHeaderStyle">
                            <%=Resources.labels.thongtinnguoiuyquyen %>
                        </div>
                        <asp:Panel ID="UpdatePanel6" runat="server">
                            <%-- <ContentTemplate>--%>
                            <table class="style1" cellspacing="0" cellpadding="5">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label60" runat="server" Text="<%$ Resources:labels, coownercode %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAuthorizerCode" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnAuthorizerDetail" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                                            OnClick="btnAuthorizerDetail_Click" />
                                        <%--<asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel6"
                                                runat="server">
                                                <ProgressTemplate>
                                                    <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px;
                                                        height: 16px;" />
                                                    <%=Resources.labels.loading %>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label28" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFullnameNguoiUyQuyen" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label58" runat="server" Text="Email"></asp:Label>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmailNguoiUyQuyen" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblLevelNguoiUyQuyen" runat="server" Text="1" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label57" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPhoneNguoiUyQuyen" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label56" runat="server" Text="<%$ Resources:labels, gender %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGenderNguoiUyQuyen" runat="server">
                                            <asp:ListItem Value="M" Text="<%$ Resources:labels, nam%>"></asp:ListItem>
                                            <asp:ListItem Value="F" Text="<%$ Resources:labels, nu%>"></asp:ListItem>
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
                                        <asp:Label ID="Label59" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddressNguoiUyQuyen" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <%--</ContentTemplate>--%>
                        </asp:Panel>
                    </div>
                    <script type="text/javascript">//<![CDATA[

                        var cal = Calendar.setup({
                            onSelect: function (cal) { cal.hide() }
                        });

                        cal.manageFields("<%=txtBirthNguoiUyQuyen.ClientID %>", "<%=txtBirthNguoiUyQuyen.ClientID %>", "%d/%m/%Y");
    //]]></script>
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
                                            Text="<%$ Resources:labels, theotaikhoan %>" onclick="enableANUQ();" />
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
                    <div id="divRoleNguoiUyQuyen" style="height: 250px; margin-top: 20px;">
                        <div id="dhtmlgoodies_tabView2">
                            <% if (TabCustomerInfoHelper.TabIBVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table1" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label12" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label14" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td valign="top" style="width: 25%; height: 50px">
                                            <!--<asp:TextBox ID="txtUserNameNguoiUyQuyen" runat="server"></asp:TextBox>-->
                                            <asp:RadioButton ID="rbGenerateNguoiUyQuyen" runat="server" GroupName="rbUserNameNguoiUyQuyen" onclick="DisabledTextbox(true,'txtIBTypeUserNameNguoiUyQuyen')"
                                                Checked="True" ClientIDMode="Static" />
                                            <asp:TextBox ID="txtIBGenUserNameNguoiUyQuyen" runat="server"></asp:TextBox>
                                            <br />
                                            <asp:RadioButton ID="rbTypeNguoiUyQuyen" runat="server" GroupName="rbUserNameNguoiUyQuyen" onclick="DisabledTextbox(false,'txtIBTypeUserNameNguoiUyQuyen')"
                                                ClientIDMode="Static" />
                                            <asp:TextBox ID="txtIBTypeUserNameNguoiUyQuyen" runat="server" ClientIDMode="Static"></asp:TextBox>
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
                                            <%--<asp:Label ID="Label173" runat="server" Font-Bold="true" ForeColor="Red" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%; padding-left: 28px">
                                            <asp:DropDownList ID="ddlpolicyIBau" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabSMSVisibility == 1 && Convert.ToString(ConfigurationManager.AppSettings["tabSMSVisibilityCorp"]) == "1")
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table2" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #EAFAFF; color: #003366; width: 50%;">
                                            <asp:Label ID="Label16" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #EAFAFF; color: #003366; width: 50%;">
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
                                                <asp:ListItem Value="E">English</asp:ListItem>
                                                <asp:ListItem Value="M">Myanmar</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label182" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicySMSau" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:CheckBox ID="cbNUYIsDefault" runat="server" Text="<%$ Resources:labels, sudungmacdinh %>"
                                                Checked="True" />
                                        </td>
                                        <td valign="top">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabMobileVisibility == 1 && Convert.ToString(ConfigurationManager.AppSettings["tabMobileVisibilityCorp"]) == "1")
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table3" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label47" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label48" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; height: 25px" valign="top">
                                            <asp:Label ID="Label49" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtMBPhoneNguoiUyQuyen" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvMBUyQuyen" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label183" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicyMBau" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabPhoneVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table4" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label50" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label51" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label52" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
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
                                            <asp:Label ID="Label104" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:DropDownList ID="ddlNUYPHODefaultAcctno" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%} %>
                        </div>
                    </div>
                    <script type="text/javascript">
                        initTabs('dhtmlgoodies_tabView2', Array(<%=TabCustomerInfoHelper.TabName%>), 0, 967, 188, Array(false, false, false, false));

                    </script>
                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upNUY">
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
                                &nbsp;&nbsp;<asp:Button ID="btnNUY" runat="server" OnClick="btnNUY_Click" Text="<%$ Resources:labels, huy %>" />
                                &nbsp;&nbsp;<asp:Button ID="btnResetNguoiUyQuyen" runat="server" OnClick="btnResetNguoiUyQuyen_Click"
                                    Text="<%$ Resources:labels, themmoi %>" />
                                &nbsp;
                            </div>
                            <asp:Label runat="server" ID="lblAlertNUY" ForeColor="Red"></asp:Label>
                            <div id="divResultNguoiUyQuyen" style="margin-top: 20px; height: 150px; overflow: auto;">
                                <asp:GridView ID="gvResultNguoiUyQuyen" runat="server" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" Width="100%" OnPageIndexChanging="gvResultNguoiUyQuyen_PageIndexChanging"
                                    OnRowDeleting="gvResultNguoiUyQuyen_RowDeleting">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField HeaderText="<%$ Resources:labels, nguoidung %>" DataField="colFullName" />
                                        <asp:BoundField DataField="colIBUserName" HeaderText="<%$ Resources:labels, username %>" />
                                        <asp:BoundField HeaderText="<%$ Resources:labels, taikhoan %>" DataField="colAccount" />
                                        <asp:BoundField HeaderText="<%$ Resources:labels, quyensudung %>" DataField="colRole" />
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#E0ECFF" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnThemNguoiUyQuyen" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
                <!--end-->
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
            </p>
        </div>
        <div class="tabbertab">
            <h2>
                <%=Resources.labels.thongtinnguoidungcaphai %></h2>
            <p>
                <!--Thong tin nguoi level 2-->
                <asp:Panel ID="Panel1" runat="server">
                    <div class="divGetInfoCust">
                        <div class="divHeaderStyle">
                            <%=Resources.labels.thongtinnguoidungcaphai %>
                        </div>
                        <asp:Panel ID="UpdatePanel7" runat="server">
                            <%--<ContentTemplate>--%>
                            <table class="style1" cellspacing="0" cellpadding="5">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label81" runat="server" Text="<%$ Resources:labels, secondusercode %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSecondUserCode" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnSecondUserDetail" runat="server"
                                            Text="<%$ Resources:labels, xemchitiet %>" OnClick="btnSecondUserDetail_Click" />
                                        <%--<asp:UpdateProgress ID="UpdateProgress8" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel7"
                                                runat="server">
                                                <ProgressTemplate>
                                                    <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px;
                                                        height: 16px;" />
                                                    <%=Resources.labels.loading %>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label54" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFullNameLevel2" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label65" runat="server" Text="Email"></asp:Label>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmailLevel2" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblLevelLevel2" runat="server" Text="2" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label64" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPhoneLevel2" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label63" runat="server" Text="<%$ Resources:labels, gender %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGenderLevel2" runat="server">
                                            <asp:ListItem Value="M" Text="<%$ Resources:labels, nam%>"></asp:ListItem>
                                            <asp:ListItem Value="F" Text="<%$ Resources:labels, nu%>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label62" runat="server" Text="<%$ Resources:labels, birthday %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBirthdayLevel2" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label66" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddresslevel2" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <%--</ContentTemplate>--%>
                        </asp:Panel>
                    </div>
                    <script type="text/javascript">//<![CDATA[

                        var cal = Calendar.setup({
                            onSelect: function (cal) { cal.hide() }
                        });

                        cal.manageFields("<%=txtBirthdayLevel2.ClientID %>", "<%=txtBirthdayLevel2.ClientID %>", "%d/%m/%Y");

    //]]></script>
                    <div id="div1" style="margin-top: 10px;">
                        <div class="divGetInfoCust">
                            <div class="divHeaderStyle">
                                <%=Resources.labels.taikhoandangky %>
                            </div>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 25%;">
                                        <asp:RadioButton ID="radAllAccountLevel2" Checked="true" GroupName="AccInfoL2" runat="server"
                                            Text="<%$ Resources:labels, tatcataikhoan %>" onclick="enableACL2();" />
                                    </td>
                                    <td style="width: 25%;"></td>
                                    <td style="width: 25%;">
                                        <asp:RadioButton ID="radAccountLevel2" GroupName="AccInfoL2" runat="server" Text="<%$ Resources:labels, theotaikhoan %>"
                                            onclick="enableAL2();" />
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:DropDownList ID="ddlAccountLevel2" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <script>
                            function enableACL2() {
                                document.getElementById("<%=ddlAccountLevel2.ClientID %>").disabled = true;
                            }
                            function enableAL2() {

                                document.getElementById("<%=ddlAccountLevel2.ClientID %>").disabled = false;

                            }
                        </script>
                    </div>
                    <div id="div2" style="height: 250px; margin-top: 20px;">
                        <div id="tabLevel2">
                            <% if (TabCustomerInfoHelper.TabIBVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table5" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label67" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label68" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label69" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td valign="top" style="width: 25%; height: 50px">
                                            <!--<asp:TextBox ID="txtUserNameLevel2" runat="server"></asp:TextBox>-->
                                            <asp:RadioButton ID="rbGenerateLevel2" runat="server" GroupName="rbUserNameLevel2" onclick="DisabledTextbox(true,'txtIBTypeUserNameLevel2')"
                                                Checked="True" ClientIDMode="Static" />
                                            <asp:TextBox ID="txtIBGenUserNameLevel2" runat="server"></asp:TextBox>
                                            <br />
                                            <asp:RadioButton ID="rbTypeLevel2" runat="server" GroupName="rbUserNameLevel2" ClientIDMode="Static" onclick="DisabledTextbox(false,'txtIBTypeUserNameLevel2')" />
                                            <asp:TextBox ID="txtIBTypeUserNameLevel2" runat="server" ClientIDMode="Static"></asp:TextBox>
                                            <asp:HiddenField ID="hdfIBUserNameLevel2" runat="server" />
                                        </td>
                                        <td rowspan="2" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvIBLevel2" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label174" runat="server" Font-Bold="true" ForeColor="Red" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%; padding-left: 28px">
                                            <asp:DropDownList ID="ddlpolicyIBl2" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabSMSVisibility == 1 && Convert.ToString(ConfigurationManager.AppSettings["tabSMSVisibilityCorp"]) == "1")
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table6" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #EAFAFF; color: #003366; width: 50%;">
                                            <asp:Label ID="Label70" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #EAFAFF; color: #003366; width: 50%;">
                                            <asp:Label ID="Label71" runat="server" Font-Bold="True" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label72" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtSMSPhoneLevel2" runat="server"></asp:TextBox>
                                        </td>
                                        <td rowspan="5" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvSMSLevel2" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:Label ID="Label73" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="ddlDefaultAccountLevel2" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:Label ID="Label74" runat="server" Text="<%$ Resources:labels, language %>"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="ddlDefaultLangLevel2" runat="server">
                                                <asp:ListItem Value="E">English</asp:ListItem>
                                                <asp:ListItem Value="M">Myanmar</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label184" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicySMSl2" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:CheckBox ID="cbC2IsDefault" runat="server" Text="<%$ Resources:labels, sudungmacdinh %>"
                                                Checked="True" />
                                        </td>
                                        <td valign="top">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabMobileVisibility == 1 && Convert.ToString(ConfigurationManager.AppSettings["tabMobileVisibilityCorp"]) == "1")
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table7" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label75" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label76" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td rowspan="2" style="width: 25%;" valign="top">
                                            <asp:Label ID="Label77" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtMBPhoneLevel2" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvMBLevel2" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label185" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicyMBl2" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabPhoneVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="Table8" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label78" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label79" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label80" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtPHOPhoneLevel2" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%;" rowspan="2" valign="top">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvPHOLevel2" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label105" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:DropDownList ID="ddlC2PHODefaultAcctno" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%} %>
                        </div>
                    </div>
                    <script type="text/javascript">
                        initTabs('tabLevel2', Array(<%=TabCustomerInfoHelper.TabName%>), 0, 967, 188, Array(false, false, false, false));

                    </script>
                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel3">
                        <ProgressTemplate>
                            <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                            <%=Resources.labels.loading %>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div style="text-align: right;">
                                <asp:Button ID="btnThemQuyenLevel2" runat="server" OnClick="btnThemQuyenLevel2_Click"
                                    OnClientClick="return validate3();" Text='<%$ Resources:labels, them %>' Width="170px" />
                                &nbsp;
                                <asp:Button ID="btnHuyL2" runat="server" OnClick="btnHuyL2_Click" Text="<%$ Resources:labels, huy %>" />
                                &nbsp;
                                <asp:Button ID="btnResetLevel2" runat="server" OnClick="btnResetLevel2_Click" Text="<%$ Resources:labels, themmoi %>" />
                                &nbsp;
                            </div>
                            <asp:Label runat="server" ID="lblAlertL2" ForeColor="Red"></asp:Label>
                            <div id="divResultNguoiUyQuyen" style="margin-top: 20px; height: 150px; overflow: auto;">
                                <asp:GridView ID="gvResultLevel2" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                    OnPageIndexChanging="gvResultLevel2_PageIndexChanging" OnRowDeleting="gvResultLevel2_RowDeleting">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField HeaderText="<%$ Resources:labels, nguoidung %>" DataField="colFullName" />
                                        <asp:BoundField DataField="colIBUserName" HeaderText="<%$ Resources:labels, username %>" />
                                        <asp:BoundField HeaderText="<%$ Resources:labels, taikhoan %>" DataField="colAccount" />
                                        <asp:BoundField HeaderText="<%$ Resources:labels, quyensudung %>" DataField="colRole" />
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#E0ECFF" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnThemQuyenLevel2" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
                <!--end-->
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
            </p>
        </div>
    </div>
</asp:Panel>

<asp:Panel runat="server" ID="pnPersonalSimple">
    <div class="tabber">

        <%--chu tai khoan don gian holder simple--%>

        <div class="tabbertab">
            <h2>
                <%=Resources.labels.thongtinchutaikhoan %>
            </h2>
            <p>
                <!--Thong tin chu tai khoan-->
                <asp:Panel ID="Panel3" runat="server">
                    <div class="divGetInfoCust">
                        <div class="divHeaderStyle">
                            <%=Resources.labels.thongtinchutaikhoan %>
                        </div>
                        <%--<asp:UpdatePanel ID="UpdatePanel5" runat="server">--%>
                        <asp:Panel ID="UpdatePanel5" runat="server">
                            <%--<contenttemplate>--%>
                            <table class="style1" cellspacing="0" cellpadding="5">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label106" runat="server" Text="<%$ Resources:labels, ownercode %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOwnerCodeS" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnOwnerDetailS" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                                            OnClick="btnOwnerDetailS_Click" />
                                        <%--<asp:UpdateProgress ID="UpdateProgress9" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel5"
                                                                runat="server">
                                                <progresstemplate>--%>
                                        <%--<asp:UpdateProgress ID="UpdateProgress9" DisplayAfter="0" 
                                                                runat="server">--%>
                                        <%--<progresstemplate>--%>
                                        <%--     <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px;
                                                        height: 16px;" />
                                                    <%=Resources.labels.loading %>--%>
                                        <%--</progresstemplate>--%>
                                        <%--</asp:UpdateProgress>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label107" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReFullNameS" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label108" runat="server" Text="Email"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReEmailS" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblLevelS" runat="server" Text="1" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label109" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReMobiS" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label30S" runat="server" Text="<%$ Resources:labels, gender %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlReGenderS" runat="server">
                                            <asp:ListItem Value="M" Text="<%$ Resources:labels, nam%>"></asp:ListItem>
                                            <asp:ListItem Value="F" Text="<%$ Resources:labels, nu%>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label110" runat="server" Text="<%$ Resources:labels, birthday %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReBirthS" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label111" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReAddressS" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <%--</contenttemplate>--%>
                        </asp:Panel>
                    </div>
                    <script type="text/javascript">
                        //<![CDATA[

                        var cal = Calendar.setup({
                            onSelect: function (cal) { cal.hide() }
                        });

                        cal.manageFields("<%=txtReBirthS.ClientID %>", "<%=txtReBirthS.ClientID %>", "%d/%m/%Y");
                        //]]></script>
                    <div id="divAccountInfo" style="margin-top: 10px;">
                        <div class="divGetInfoCust">
                            <div class="divHeaderStyle">
                                <%=Resources.labels.taikhoandangky %>
                            </div>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 25%;">
                                        <asp:RadioButton ID="radAllAccountS" Checked="true" GroupName="AccInfoCTKS" runat="server"
                                            Text="<%$ Resources:labels, tatcataikhoan %>" onClick="enableACCTKS();" />
                                    </td>
                                    <td style="width: 25%;"></td>
                                    <td style="width: 25%;">
                                        <asp:RadioButton ID="radAccountS" GroupName="AccInfoCTKS" runat="server" Text="<%$ Resources:labels, theotaikhoan %>"
                                            onClick="enableACTKS();" />
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:DropDownList ID="ddlAccountS" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <script>
                            function enableACCTKS() {
                                document.getElementById("<%=ddlAccountS.ClientID %>").disabled = true;
                            }
                            function enableACTKS() {

                                document.getElementById("<%=ddlAccountS.ClientID %>").disabled = false;

                            }
                        </script>
                    </div>
                    <div id="divAccount" style="height: 250px; margin-top: 10px;">
                        <div id="dhtmlgoodies_tabView1">
                            <% if (TabCustomerInfoHelper.TabIBVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tblIB" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label112" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label113" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label114" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td valign="top" style="width: 25%; height: 50px">
                                            <!--<asp:TextBox ID="txtIBUserNameS" runat="server"></asp:TextBox>-->
                                            <asp:RadioButton ID="rbGenerateS" runat="server" GroupName="rbUserNameS" Checked="True" onclick="DisabledTextbox(true,'txtIBTypeUserNameS')"
                                                ClientIDMode="Static" />
                                            <asp:TextBox ID="txtIBGenUserNameS" runat="server"></asp:TextBox>
                                            <br />
                                            <asp:RadioButton ID="rbTypeS" runat="server" GroupName="rbUserNameS" ClientIDMode="Static" onclick="DisabledTextbox(false,'txtIBTypeUserNameS')"/>
                                            <asp:TextBox ID="txtIBTypeUserNameS" runat="server" ClientIDMode="Static"></asp:TextBox>
                                            <asp:HiddenField ID="hdfIBUserNameS" runat="server" />
                                        </td>
                                        <td rowspan="2" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvIBS" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label175" runat="server" Font-Bold="true" ForeColor="Red" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%; padding-left: 28px">
                                            <asp:DropDownList ID="ddlpolicyIBs" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpolicyIBs_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabSMSVisibility == 1 && Convert.ToString(ConfigurationManager.AppSettings["tabSMSVisibilityCorp"]) == "1")
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tblSMS" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #EAFAFF; color: #003366; width: 50%;">
                                            <asp:Label ID="Label115" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #EAFAFF; color: #003366; width: 50%;">
                                            <asp:Label ID="Label116" runat="server" Font-Bold="True" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label117" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtSMSPhoneNoS" runat="server"></asp:TextBox>
                                        </td>
                                        <td rowspan="5" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvSMSS" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:Label ID="Label118" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="ddlSMSDefaultAcctnoS" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:Label ID="Label119" runat="server" Text="<%$ Resources:labels, language %>"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="ddlLanguageS" runat="server">
                                                <asp:ListItem Value="E">English</asp:ListItem>
                                                <asp:ListItem Value="M">Myanmar</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label186" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicySMSs" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:CheckBox ID="cbCTKIsDefaultS" runat="server" Text="<%$ Resources:labels, sudungmacdinh %>"
                                                Checked="True" />
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Label171" runat="server" Text="<%$ Resources:labels, warningoneaccountononesmsrole %>" Style="color: red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabMobileVisibility == 1 && Convert.ToString(ConfigurationManager.AppSettings["tabMobileVisibilityCorp"]) == "1")
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tblMB" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label120" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label121" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td rowspan="2" style="width: 25%;" valign="top">
                                            <asp:Label ID="Label122" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtMBPhoneNoS" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvMBS" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label187" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicyMBs" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabPhoneVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tblPHOS" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label123" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label124" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label125" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtPHOPhoneNoS" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%;" rowspan="2" valign="top">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvPHOS" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label126" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:DropDownList ID="ddlCTKPHODefaultAcctnoS" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%} %>
                        </div>
                        <script type="text/javascript">
                            initTabs('dhtmlgoodies_tabView1', Array(<%=TabCustomerInfoHelper.TabName%>), 0, 948, 188, Array(false, false, false, false));

                        </script>
                    </div>
                    <asp:UpdateProgress ID="UpdateProgress10" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel8">
                        <ProgressTemplate>
                            <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                            <%=Resources.labels.loading %>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <div style="text-align: right;">
                                &nbsp;
                                <asp:Button ID="btnThemChuTaiKhoanS" runat="server" Text='<%$ Resources:labels, them %>'
                                    Width="170px" OnClick="btnThemChuTaiKhoanS_Click" OnClientClick="return validate5();" />
                                &nbsp;
                                <asp:Button ID="btnHuyCTKS" runat="server" OnClick="btnHuyCTKS_Click" Text="<%$ Resources:labels, huy %>" />
                                &nbsp;
                            </div>
                            <asp:Label runat="server" ID="lblAlertCTKS" ForeColor="Red"></asp:Label>
                            <div id="div6" style="margin-top: 20px; height: 150px; overflow: auto;">
                                <asp:GridView ID="gvResultChuTaiKhoanS" runat="server" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" Width="100%" OnPageIndexChanging="gvResultChuTaiKhoanS_PageIndexChanging"
                                    OnRowDeleting="gvResultChuTaiKhoanS_RowDeleting">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField HeaderText="<%$ Resources:labels, nguoidung %>" DataField="colFullName" />
                                        <asp:BoundField DataField="colIBUserName" HeaderText="<%$ Resources:labels, username %>" />
                                        <asp:BoundField HeaderText="<%$ Resources:labels, taikhoan %>" DataField="colAccount" />
                                        <asp:BoundField HeaderText="<%$ Resources:labels, quyensudung %>" DataField="colRole" />
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#E0ECFF" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnThemChuTaiKhoanS" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
                <!--end-->
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
            </p>
        </div>

        <%--nguoi dung level 4--%>
        <div class="tabbertab">
            <h2>
                <%=Resources.labels.thongtinnguoiquanlytiente %>
            </h2>
            <p>
                <!--Thong tin chu tai khoan-->
                <asp:Panel ID="pnLevel4" runat="server">
                    <div class="divGetInfoCust">
                        <div class="divHeaderStyle">
                            <%=Resources.labels.thongtinchutaikhoan %>
                        </div>
                        <%-- <asp:UpdatePanel ID="UpdatePanellv4" runat="server">--%>
                        <asp:Panel ID="UpdatePanellv4" runat="server">
                            <%-- <contenttemplate>--%>
                            <table class="style1" cellspacing="0" cellpadding="5">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label127" runat="server" Text="<%$ Resources:labels, ownercode %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlv4Code" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnlv4Detail" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                                            OnClick="btnlv4Detail_Click" />
                                        <%--<asp:UpdateProgress ID="UpdateProgress11" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanellv4"
                                                        runat="server">
                                        <progresstemplate>--%>
                                        <%-- <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px;
                                                        height: 16px;" />
                                            <%=Resources.labels.loading %>--%>
                                        <%-- </progresstemplate>
                                    </asp:UpdateProgress>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label128" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlv4FullName" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label129" runat="server" Text="Email"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlv4Email" runat="server"></asp:TextBox>
                                        <asp:Label ID="lbllv4Level" runat="server" Text="4" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label130" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlv4Mobi" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label131" runat="server" Text="<%$ Resources:labels, gender %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddllv4Gender" runat="server">
                                            <asp:ListItem Value="M" Text="<%$ Resources:labels, nam%>"></asp:ListItem>
                                            <asp:ListItem Value="F" Text="<%$ Resources:labels, nu%>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label132" runat="server" Text="<%$ Resources:labels, birthday %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlv4Birth" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label133" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlv4Address" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <%--  </contenttemplate>--%>
                        </asp:Panel>
                    </div>
                    <script type="text/javascript">
                        //<![CDATA[

                        var cal = Calendar.setup({
                            onSelect: function (cal) { cal.hide() }
                        });

                        cal.manageFields("<%=txtlv4Birth.ClientID %>", "<%=txtlv4Birth.ClientID %>", "%d/%m/%Y");
                //]]></script>
                    <div id="divAccountInfolv4" style="margin-top: 10px;">
                        <div class="divGetInfoCust">
                            <div class="divHeaderStyle">
                                <%=Resources.labels.taikhoandangky %>
                            </div>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 25%;">
                                        <asp:RadioButton ID="radlv4AllAccount" Checked="true" GroupName="AccInfolv4" runat="server"
                                            Text="<%$ Resources:labels, tatcataikhoan %>" onClick="enableACCTKlv4();" />
                                    </td>
                                    <td style="width: 25%;"></td>
                                    <td style="width: 25%;">
                                        <asp:RadioButton ID="radlv4Account" GroupName="AccInfolv4" runat="server" Text="<%$ Resources:labels, theotaikhoan %>"
                                            onClick="enableACTKlv4();" />
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:DropDownList ID="ddllv4Account" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <script>
                            function enableACCTKlv4() {
                                document.getElementById("<%=ddllv4Account.ClientID %>").disabled = true;
                            }
                            function enableACTKlv4() {

                                document.getElementById("<%=ddllv4Account.ClientID %>").disabled = false;

                            }
                        </script>
                    </div>
                    <div id="divAccountlv4" style="height: 250px; margin-top: 10px;">
                        <div id="dhtmlgoodies_tabViewlv4">
                            <% if (TabCustomerInfoHelper.TabIBVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tbllv4IB" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label134" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label135" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label136" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td valign="top" style="width: 25%; height: 50px">
                                            <asp:RadioButton ID="rblv4Generate" runat="server" GroupName="rblv4UserName" Checked="True" onclick="DisabledTextbox(true,'txtlv4IBTypeUserName')"
                                                ClientIDMode="Static" />
                                            <asp:TextBox ID="txtlv4IBGenUserName" runat="server"></asp:TextBox>
                                            <br />
                                            <asp:RadioButton ID="rblv4Type" runat="server" GroupName="rblv4UserName" ClientIDMode="Static" onclick="DisabledTextbox(false,'txtlv4IBTypeUserName')"/>
                                            <asp:TextBox ID="txtlv4IBTypeUserName" runat="server" ClientIDMode="Static"></asp:TextBox>
                                            <asp:HiddenField ID="hdflv4IBUserName" runat="server" />
                                        </td>
                                        <td rowspan="2" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvlv4IB" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label176" runat="server" Font-Bold="true" ForeColor="Red" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%; padding-left: 28px">
                                            <asp:DropDownList ID="ddlpolicyIBsl4" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpolicyIBsl4_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabSMSVisibility == 1 && Convert.ToString(ConfigurationManager.AppSettings["tabSMSVisibilityCorp"]) == "1")
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tbllv4SMS" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #EAFAFF; color: #003366; width: 50%;">
                                            <asp:Label ID="Label137" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #EAFAFF; color: #003366; width: 50%;">
                                            <asp:Label ID="Label138" runat="server" Font-Bold="True" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label139" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtlv4SMSPhoneNo" runat="server"></asp:TextBox>
                                        </td>
                                        <td rowspan="5" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvlv4SMS" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:Label ID="Label140" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="ddllv4SMSDefaultAcctno" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:Label ID="Label141" runat="server" Text="<%$ Resources:labels, language %>"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="ddllv4Language" runat="server">
                                                <asp:ListItem Value="E">English</asp:ListItem>
                                                <asp:ListItem Value="M">Myanmar</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label188" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicySMSsl4" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:CheckBox ID="cblv4CTKIsDefault" runat="server" Text="<%$ Resources:labels, sudungmacdinh %>"
                                                Checked="True" />
                                        </td>
                                        <td valign="top">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabMobileVisibility == 1 && Convert.ToString(ConfigurationManager.AppSettings["tabMobileVisibilityCorp"]) == "1")
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tbllv4MB" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label142" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label143" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td rowspan="2" style="width: 25%;" valign="top">
                                            <asp:Label ID="Label144" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtlv4MBPhoneNo" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvlv4MB" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label189" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicyMBsl4" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabPhoneVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tbllv4PHO" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label145" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label146" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label147" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtlv4PHOPhoneNo" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%;" rowspan="2" valign="top">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvlv4PHO" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label148" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:DropDownList ID="ddllv4PHODefaultAcctno" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%} %>
                        </div>
                        <script type="text/javascript">
                            initTabs('dhtmlgoodies_tabViewlv4', Array(<%=TabCustomerInfoHelper.TabName%>), 0, 948, 188, Array(false, false, false, false));

                        </script>
                    </div>
                    <asp:UpdateProgress ID="UpdateProgress12" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel2lv4">
                        <ProgressTemplate>
                            <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                            <%=Resources.labels.loading %>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanel2lv4" runat="server">
                        <ContentTemplate>
                            <div style="text-align: right;">
                                &nbsp;
                        <asp:Button ID="btnThemlv4" runat="server" Text='<%$ Resources:labels, them %>'
                            Width="170px" OnClick="btnThemlv4_Click" OnClientClick="    return validate6();" />
                                &nbsp;
                        <asp:Button ID="btnlv4Huy" runat="server" OnClick="btnlv4Huy_Click" Text="<%$ Resources:labels, huy %>" />
                                &nbsp;
                            </div>
                            <asp:Label runat="server" ID="lbllv4Alert" ForeColor="Red"></asp:Label>
                            <div id="div6" style="margin-top: 20px; height: 150px; overflow: auto;">
                                <asp:GridView ID="gvResultlv4" runat="server" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" Width="100%" OnPageIndexChanging="gvResultlv4_PageIndexChanging"
                                    OnRowDeleting="gvResultlv4_RowDeleting">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField HeaderText="<%$ Resources:labels, nguoidung %>" DataField="colFullName" />
                                        <asp:BoundField DataField="colIBUserName" HeaderText="<%$ Resources:labels, username %>" />
                                        <asp:BoundField HeaderText="<%$ Resources:labels, taikhoan %>" DataField="colAccount" />
                                        <asp:BoundField HeaderText="<%$ Resources:labels, quyensudung %>" DataField="colRole" />
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#E0ECFF" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnThemlv4" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
                <!--end-->
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
            </p>
        </div>

        <%--nguoi dung level 5--%>
        <div class="tabbertab">
            <h2>
                <%=Resources.labels.thongtinketoan %>
            </h2>
            <p>
                <!--Thong tin chu tai khoan-->
                <asp:Panel ID="Panel5" runat="server">
                    <div class="divGetInfoCust">
                        <div class="divHeaderStyle">
                            <%=Resources.labels.thongtinchutaikhoan %>
                        </div>
                        <%--<asp:UpdatePanel ID="UpdatePanellv5" runat="server">--%>
                        <asp:Panel ID="UpdatePanellv5" runat="server">
                            <%--<contenttemplate>--%>
                            <table class="style1" cellspacing="0" cellpadding="5">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label149" runat="server" Text="<%$ Resources:labels, ownercode %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlv5Code" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnlv5Detail" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                                            OnClick="btnlv5Detail_Click" />
                                        <%--<asp:UpdateProgress ID="UpdateProgress13" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanellv5"
                                                            runat="server">
                                            <progresstemplate>--%>
                                        <%-- <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px;
                                                            height: 16px;" />
                                                <%=Resources.labels.loading %>--%>
                                        <%--</progresstemplate>
                                        </asp:UpdateProgress>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label150" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlv5FullName" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label151" runat="server" Text="Email"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlv5Email" runat="server"></asp:TextBox>
                                        <asp:Label ID="lbllv5Level" runat="server" Text="5" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label152" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlv5Mobi" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label153" runat="server" Text="<%$ Resources:labels, gender %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddllv5Gender" runat="server">
                                            <asp:ListItem Value="M" Text="<%$ Resources:labels, nam%>"></asp:ListItem>
                                            <asp:ListItem Value="F" Text="<%$ Resources:labels, nu%>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label154" runat="server" Text="<%$ Resources:labels, birthday %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlv5Birth" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label155" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlv5Address" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <%--</contenttemplate>--%>
                        </asp:Panel>
                    </div>
                    <script type="text/javascript">
                        //<![CDATA[

                        var cal = Calendar.setup({
                            onSelect: function (cal) { cal.hide() }
                        });

                        cal.manageFields("<%=txtlv5Birth.ClientID %>", "<%=txtlv5Birth.ClientID %>", "%d/%m/%Y");
                    //]]></script>
                    <div id="divAccountInfolv5" style="margin-top: 10px;">
                        <div class="divGetInfoCust">
                            <div class="divHeaderStyle">
                                <%=Resources.labels.taikhoandangky %>
                            </div>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 25%;">
                                        <asp:RadioButton ID="radlv5AllAccount" Checked="true" GroupName="AccInfolv5" runat="server"
                                            Text="<%$ Resources:labels, tatcataikhoan %>" onClick="enableACCTKlv5();" />
                                    </td>
                                    <td style="width: 25%;"></td>
                                    <td style="width: 25%;">
                                        <asp:RadioButton ID="radlv5Account" GroupName="AccInfolv5" runat="server" Text="<%$ Resources:labels, theotaikhoan %>"
                                            onClick="enableACTKlv5();" />
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:DropDownList ID="ddllv5Account" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <script>
                            function enableACCTKlv5() {
                                document.getElementById("<%=ddllv5Account.ClientID %>").disabled = true;
                            }
                            function enableACTKlv5() {

                                document.getElementById("<%=ddllv5Account.ClientID %>").disabled = false;

                            }
                        </script>
                    </div>
                    <div id="divAccountlv5" style="height: 250px; margin-top: 10px;">
                        <div id="dhtmlgoodies_tabViewlv5">
                            <% if (TabCustomerInfoHelper.TabIBVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tbllv5IB" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label156" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label157" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; height: 50px" valign="top">
                                            <asp:Label ID="Label158" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:RadioButton ID="rblv5Generate" runat="server" GroupName="rblv5UserName" Checked="True" onclick="DisabledTextbox(true,'txtlv5IBTypeUserName')"
                                                ClientIDMode="Static" />
                                            <asp:TextBox ID="txtlv5IBGenUserName" runat="server"></asp:TextBox>
                                            <br />
                                            <asp:RadioButton ID="rblv5Type" runat="server" GroupName="rblv5UserName" ClientIDMode="Static" onclick="DisabledTextbox(false,'txtlv5IBTypeUserName')"/>
                                            <asp:TextBox ID="txtlv5IBTypeUserName" runat="server" ClientIDMode="Static"></asp:TextBox>
                                            <asp:HiddenField ID="hdflv5IBUserName" runat="server" />
                                        </td>
                                        <td rowspan="2" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvlv5IB" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label177" runat="server" Font-Bold="true" ForeColor="Red" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%; padding-left: 28px">
                                            <asp:DropDownList ID="ddlpolicyIBsl5" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpolicyIBsl5_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabSMSVisibility == 1 && Convert.ToString(ConfigurationManager.AppSettings["tabSMSVisibilityCorp"]) == "1")
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tbllv5SMS" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #EAFAFF; color: #003366; width: 50%;">
                                            <asp:Label ID="Label159" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #EAFAFF; color: #003366; width: 50%;">
                                            <asp:Label ID="Label160" runat="server" Font-Bold="True" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label161" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtlv5SMSPhoneNo" runat="server"></asp:TextBox>
                                        </td>
                                        <td rowspan="5" style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvlv5SMS" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:Label ID="Label162" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="ddllv5SMSDefaultAcctno" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:Label ID="Label163" runat="server" Text="<%$ Resources:labels, language %>"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="ddllv5Language" runat="server">
                                                <asp:ListItem Value="E">English</asp:ListItem>
                                                <asp:ListItem Value="M">Myanmar</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label190" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicySMSsl5" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 20%;" valign="top">
                                            <asp:CheckBox ID="cblv5CTKIsDefault" runat="server" Text="<%$ Resources:labels, sudungmacdinh %>"
                                                Checked="True" />
                                        </td>
                                        <td valign="top">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabMobileVisibility == 1 && Convert.ToString(ConfigurationManager.AppSettings["tabMobileVisibilityCorp"]) == "1")
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tbllv5MB" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label164" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label165" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td rowspan="2" style="width: 25%;" valign="top">
                                            <asp:Label ID="Label166" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtlv5MBPhoneNo" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%;">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvlv5MB" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                            <%=Resources.labels.dungpolicy %>
                                            <%--<asp:Label ID="Label191" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                        </td>
                                        <td valign="top" style="width: 25%;">
                                            <asp:DropDownList ID="ddlpolicyMBsl5" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <%}
                                if (TabCustomerInfoHelper.TabPhoneVisibility == 1)
                                { %>
                            <div class="dhtmlgoodies_aTab">
                                <table id="tbllv5PHO" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label167" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
                                            <asp:Label ID="Label168" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label169" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:TextBox ID="txtlv5PHOPhoneNo" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%;" rowspan="2" valign="top">
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:TreeView ID="tvlv5PHO" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;" valign="top">
                                            <asp:Label ID="Label170" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                        </td>
                                        <td style="width: 25%;" valign="top">
                                            <asp:DropDownList ID="ddllv5PHODefaultAcctno" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%} %>
                        </div>
                        <script type="text/javascript">
                            initTabs('dhtmlgoodies_tabViewlv5', Array(<%=TabCustomerInfoHelper.TabName%>), 0, 948, 188, Array(false, false, false, false));

                        </script>
                    </div>
                    <asp:UpdateProgress ID="UpdateProgress14" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel2lv5">
                        <ProgressTemplate>
                            <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                            <%=Resources.labels.loading %>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanel2lv5" runat="server">
                        <ContentTemplate>
                            <div style="text-align: right;">
                                &nbsp;
                            <asp:Button ID="btnThemlv5" runat="server" Text='<%$ Resources:labels, them %>'
                                Width="170px" OnClick="btnThemlv5_Click" OnClientClick="    return validate7();" />
                                &nbsp;
                            <asp:Button ID="btnlv5Huy" runat="server" OnClick="btnlv5Huy_Click" Text="<%$ Resources:labels, huy %>" />
                                &nbsp;
                            </div>
                            <asp:Label runat="server" ID="lbllv5Alert" ForeColor="Red"></asp:Label>
                            <div id="div6" style="margin-top: 20px; height: 150px; overflow: auto;">
                                <asp:GridView ID="gvResultlv5" runat="server" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" Width="100%" OnPageIndexChanging="gvResultlv5_PageIndexChanging"
                                    OnRowDeleting="gvResultlv5_RowDeleting">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField HeaderText="<%$ Resources:labels, nguoidung %>" DataField="colFullName" />
                                        <asp:BoundField DataField="colIBUserName" HeaderText="<%$ Resources:labels, username %>" />
                                        <asp:BoundField HeaderText="<%$ Resources:labels, taikhoan %>" DataField="colAccount" />
                                        <asp:BoundField HeaderText="<%$ Resources:labels, quyensudung %>" DataField="colRole" />
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#E0ECFF" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnThemlv5" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
                <!--end-->
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
            </p>
        </div>

    </div>
</asp:Panel>
<asp:Panel runat="server" ID="pnCorpMatrix">
</asp:Panel>

<asp:Panel runat="server" ID="pnLuu">
    <div style="text-align: center; padding-top: 10px;">
        <asp:Button ID="btnCustSave" runat="server" OnClick="btnCustSave_Click" OnClientClick="return validate4();"
            Text="<%$ Resources:labels, save %>" Width="69px" />
        &nbsp;
            <asp:Button ID="btnBack" runat="server" OnClick="Button2_Click" Text="<%$ Resources:labels, back %>" />
        &nbsp; &nbsp;
    </div>
</asp:Panel>
<!--end-->
<%--</ContentTemplate>
</asp:UpdatePanel>--%>
<script type="text/javascript">
    function DisabledTextbox(diabled, txtID) {
        var el = document.getElementById(txtID);
        el.disabled = diabled;
    }

    function validate() {
        if (validateEmpty('<%=txtCustCodeInfo.ClientID %>', '<%=Resources.labels.makhachhangkhongrong %>')) {

            if (validateMoney('<%=txtFullName.ClientID %>', '<%=Resources.labels.bannhaptenkhachhang %>')) {
                if (validateEmpty('<%=txtContractNo.ClientID %>', '<%=Resources.labels.mahopdongkhongrong %>')) {
                    if (validateEmpty('<%=txtStartDate.ClientID %>', '<%=Resources.labels.bannhapngayhieuluc %>')) {
                        if (validateEmpty('<%=txtEndDate.ClientID %>', '<%=Resources.labels.bannhapngayhethan %>')) {
                            if (validateEmpty('<%=txtMobi.ClientID %>', '<%=Resources.labels.bannhapsodienthoai %>')) {
                              <%--  if (validateEmpty('<%=txtEmail.ClientID %>', '<%=Resources.labels.bannhapemail %>')) {--%>
                                //kiem tra so
                                if (IsPhoneNum('<%=txtMobi.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>')) {
                                        //kiem tra email
                                       <%-- if (checkEmail('<%=txtEmail.ClientID %>', '<%=Resources.labels. emailkhongdinhdang %>')) {--%>

                                    //kiem tra ngay hết hạn lớn hơn ngày bắt đầu
                                    if (validateEmpty('<%=txtResidentAddr.ClientID %>', '<%=Resources.labels.bancannhapdiachi %>')) {
                                        if (validateEmpty('<%=txtIF.ClientID %>', '<%=Resources.labels.bancannhapmasothuegpkd %>')) {


                                            if (IsDateGreater('<%=txtEndDate.ClientID %>', '<%=txtStartDate.ClientID %>', '<%=Resources.labels.ngayhethanphailonhonngayhieuluc %>')) {

                                            }
                                            else {
                                                document.getElementById('<%=txtStartDate.ClientID %>').focus();
                                                return false;
                                            }


                                        }
                                        else {
                                            document.getElementById('<%=txtIF.ClientID %>').focus();
                                            return false;
                                        }
                                    }
                                    else {
                                        document.getElementById('<%=txtResidentAddr.ClientID %>').focus();
                                        return false;
                                    }

<%--                                        }
                                        else {
                                            document.getElementById('<%=txtEmail.ClientID %>').focus();
                                            return false;
                                        }--%>

                                }
                                else {
                                    document.getElementById('<%=txtMobi.ClientID %>').focus();
                                    return false;
                                }
                               <%-- }
                                else {
                                    document.getElementById('<%=txtEmail.ClientID %>').focus();
                                    return false;
                                }--%>
                            }
                            else {
                                document.getElementById('<%=txtMobi.ClientID %>').focus();
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
            else {
                //document.getElementById('<%=txtFullName.ClientID %>').focus();
                return false;
            }
        }

        else {

            //document.getElementById('<%=txtCustCodeInfo.ClientID %>').focus();
            return false;
        }

    }


    function validate1() {
        try {
            if (IsNumeric('<%=txtSMSPhoneNo.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                if (IsNumeric('<%=txtMBPhoneNo.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                    if (IsNumeric('<%=txtPHOPhoneNo.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                        if (validateEmpty('<%=txtReFullName.ClientID %>', '<%=Resources.labels. bannhaptenchutaikhoan %>')) {
                            if (validateEmpty('<%=txtReMobi.ClientID %>', '<%=Resources.labels. bannhapsodienthoaichutaikhoan %>')) {
                                <%--if (validateEmpty('<%=txtReEmail.ClientID %>', '<%=Resources.labels. bannhapemailchutaikhoan %>')) {--%>
                                //kiem tra so
                                if (IsNumeric('<%=txtReMobi.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>')) {
                                        //kiem tra email
                                        <%--if (checkEmail('<%=txtReEmail.ClientID %>', '<%=Resources.labels. emailkhongdinhdang %>')) {--%>
                                    if (document.getElementById('<%=rbType.ClientID %>').checked) {
                                        if (validateEmpty('<%=txtIBTypeUserName.ClientID %>', '<%=Resources.labels. bancannhaptendangnhap %>')) {
                                            var minlength = <%= int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString())%>;
                                            var maxlength = <%= int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString())%>;
                                            if (document.getElementById('<%=txtIBTypeUserName.ClientID %>').value.length >= minlength || document.getElementById('<%=txtIBTypeUserName.ClientID %>').value.length <= maxlength) {

                                            } else {
                                                alert('<%=string.Format(Resources.labels.usernamemustbetween, ConfigurationManager.AppSettings["minlengthloginname"].ToString(), ConfigurationManager.AppSettings["maxlengthloginname"].ToString()) %>');
                                                return false;
                                            }
                                        } else {
                                            return false;
                                        }
                                    }
                                        <%--}
                                        else {
                                            //document.getElementById('<%=txtReEmail.ClientID %>').focus();
                                            return false;
                                        }--%>
                                }
                                else {
                                        //document.getElementById('<%=txtReMobi.ClientID %>').focus();
                                    return false;
                                }
                                <%--}
                                else {
                                    //document.getElementById('<%=txtReEmail.ClientID %>').focus();
                                    return false;
                                }--%>
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
            if (IsNumeric('<%=txtSMSPhoneNguoiUyQuyen.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                if (IsNumeric('<%=txtMBPhoneNguoiUyQuyen.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                    if (IsNumeric('<%=txtPHOPhoneNguoiUyQuyen.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                        if (validateEmpty('<%=txtFullnameNguoiUyQuyen.ClientID %>', '<%=Resources.labels. bannhaptennguoiuyquyen %>')) {
                            if (validateEmpty('<%=txtPhoneNguoiUyQuyen.ClientID %>', '<%=Resources.labels. bannhapsodienthoainguoiuyquyen %>')) {
                                <%--if (validateEmpty('<%=txtEmailNguoiUyQuyen.ClientID %>', '<%=Resources.labels. bannhapemailnguoiuyquyen %>')) {--%>

                                //kiem tra so
                                if (IsNumeric('<%=txtPhoneNguoiUyQuyen.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>')) {
                                        //kiem tra email
                                        <%--if (checkEmail('<%=txtEmailNguoiUyQuyen.ClientID %>', '<%=Resources.labels. emailkhongdinhdang %>')) {--%>
                                    if (document.getElementById('<%=rbTypeNguoiUyQuyen.ClientID %>').checked) {
                                        if (validateEmpty('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>', '<%=Resources.labels. bancannhaptendangnhap %>')) {
                                            var minlength = <%= int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString())%>;
                                            var maxlength = <%= int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString())%>;
                                            if (document.getElementById('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>').value.length >= minlength || document.getElementById('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>').value.length <= maxlength) {

                                            } else {
                                                alert('<%=string.Format(Resources.labels.usernamemustbetween, ConfigurationManager.AppSettings["minlengthloginname"].ToString(), ConfigurationManager.AppSettings["maxlengthloginname"].ToString()) %>');
                                                return false;
                                            }
                                        } else {
                                            return false;
                                        }
                                    }
                                        <%--}
                                        else {
                                            document.getElementById('<%=txtEmailNguoiUyQuyen.ClientID %>').focus();
                                            return false;
                                        }--%>
                                }
                                else {
                                    document.getElementById('<%=txtPhoneNguoiUyQuyen.ClientID %>').focus();
                                    return false;
                                }
                               <%-- }
                                else {
                                    document.getElementById('<%=txtEmailNguoiUyQuyen.ClientID %>').focus();
                                    return false;
                                }--%>
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


    function validate3() {
        try {
            if (IsNumeric('<%=txtSMSPhoneLevel2.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                if (IsNumeric('<%=txtMBPhoneLevel2.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                    if (IsNumeric('<%=txtPHOPhoneLevel2.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                        if (validateEmpty('<%=txtFullNameLevel2.ClientID %>', '<%=Resources.labels. bannhaptennguoidungcaphai %>')) {
                            if (validateEmpty('<%=txtPhoneLevel2.ClientID %>', '<%=Resources.labels. bannhapsodienthoainguoidungcaphai %>')) {
                                <%--if (validateEmpty('<%=txtEmailLevel2.ClientID %>', '<%=Resources.labels. bannhapemailnguoidungcaphai %>')) {--%>

                                //kiem tra so
                                if (IsNumeric('<%=txtPhoneLevel2.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>')) {
                                        //kiem tra email
                                        <%--if (checkEmail('<%=txtEmailLevel2.ClientID %>', '<%=Resources.labels. emailkhongdinhdang %>')) {--%>
                                    if (document.getElementById('<%=rbTypeLevel2.ClientID %>').checked) {
                                        if (validateEmpty('<%=txtIBTypeUserNameLevel2.ClientID %>', '<%=Resources.labels. bancannhaptendangnhap %>')) {
                                            var minlength = <%= int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString())%>;
                                            var maxlength = <%= int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString())%>;
                                            if (document.getElementById('<%=txtIBTypeUserNameLevel2.ClientID %>').value.length >= minlength || document.getElementById('<%=txtIBTypeUserNameLevel2.ClientID %>').value.length <= maxlength) {

                                            } else {
                                                alert('<%=string.Format(Resources.labels.usernamemustbetween, ConfigurationManager.AppSettings["minlengthloginname"].ToString(), ConfigurationManager.AppSettings["maxlengthloginname"].ToString()) %>');
                                                return false;
                                            }
                                        } else {
                                            return false;
                                        }
                                    }
                                       <%-- }
                                        else {
                                            document.getElementById('<%=txtEmailLevel2.ClientID %>').focus();
                                            return false;
                                        }--%>
                                }
                                else {
                                    document.getElementById('<%=txtPhoneLevel2.ClientID %>').focus();
                                    return false;
                                }
                                <%--}
                                else {
                                    document.getElementById('<%=txtEmailLevel2.ClientID %>').focus();
                                    return false;
                                }--%>
                            }
                            else {
                                document.getElementById('<%=txtPhoneLevel2.ClientID %>').focus();
                                return false;
                            }

                        }
                        else {
                            document.getElementById('<%=txtFullNameLevel2.ClientID %>').focus();
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

    function validate4() {
        try {
            if (IsNumeric('<%=txtSMSPhoneQT.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                if (IsNumeric('<%=txtMBPhoneQT.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                    if (IsNumeric('<%=txtPHOPhoneQT.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                        if (validateEmpty('<%=txtFullNameQT.ClientID %>', '<%=Resources.labels. banhaptennguoiquantrihethong %>')) {
                            if (validateEmpty('<%=txtPhoneQT.ClientID %>', '<%=Resources.labels. bannhapsodienthoainguoiquantrihethong %>')) {
                                if (validateEmpty('<%=txtEmailQT.ClientID %>', '<%=Resources.labels. bannhapemailnguoiquantrihethong %>')) {

                                        //kiem tra so
                                        if (IsNumeric('<%=txtPhoneQT.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>')) {
                                            //kiem tra email
                                            if (checkEmail('<%=txtEmailQT.ClientID %>', '<%=Resources.labels. emailkhongdinhdang %>')) {
                                                if (document.getElementById('<%=rbTypeQT.ClientID %>').checked) {
                                                    if (validateEmpty('<%=txtIBTypeUserNameQT.ClientID %>', '<%=Resources.labels. bancannhaptendangnhap %>')) {
                                                        var minlength = <%= int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString())%>
                                                        var maxlength = <%= int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString())%>
                                                        if (document.getElementById('<%=txtIBTypeUserNameQT.ClientID %>').value.length >= minlength || document.getElementById('<%=txtIBTypeUserNameQT.ClientID %>').value.length <= maxlength) {

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
                                                //document.getElementById('<%=txtEmailQT.ClientID %>').focus();
                                                return false;
                                            }
                                        }
                                        else {
                                            //document.getElementById('<%=txtPhoneQT.ClientID %>').focus();
                                            return false;
                                        }
                                    }
                                    else {
                                        //document.getElementById('<%=txtEmailQT.ClientID %>').focus();
                                        return false;
                                    }
                                }
                                else {
                                    //document.getElementById('<%=txtPhoneQT.ClientID %>').focus();
                                return false;
                            }

                        }
                        else {
                                //document.getElementById('<%=txtFullNameQT.ClientID %>').focus();
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
    function validate5() {
        try {
            if (IsNumeric('<%=txtSMSPhoneNoS.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                if (IsNumeric('<%=txtMBPhoneNoS.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                    if (IsNumeric('<%=txtPHOPhoneNoS.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                        if (validateEmpty('<%=txtReFullNameS.ClientID %>', '<%=Resources.labels. bannhaptenchutaikhoan %>')) {
                            if (validateEmpty('<%=txtReMobiS.ClientID %>', '<%=Resources.labels. bannhapsodienthoaichutaikhoan %>')) {
                                    if (validateEmpty('<%=txtReEmailS.ClientID %>', '<%=Resources.labels. bannhapemailchutaikhoan %>')) {
                                        //kiem tra so
                                        if (IsNumeric('<%=txtReMobiS.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>')) {
                                        //kiem tra email
                                        if (checkEmail('<%=txtReEmailS.ClientID %>', '<%=Resources.labels. emailkhongdinhdang %>')) {
                                            if (document.getElementById('<%=rbTypeS.ClientID %>').checked) {
                                                if (validateEmpty('<%=txtIBTypeUserNameS.ClientID %>', '<%=Resources.labels. bancannhaptendangnhap %>')) {
                                                    var minlength = <%= int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString())%>
                                                    var maxlength = <%= int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString())%>
                                                    if (document.getElementById('<%=txtIBTypeUserNameS.ClientID %>').value.length >= minlength || document.getElementById('<%=txtIBTypeUserNameS.ClientID %>').value.length <= maxlength) {

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
                                        //document.getElementById('<%=txtReEmailS.ClientID %>').focus();
                                            return false;
                                        }
                                    }
                                    else {
                                    //document.getElementById('<%=txtReMobiS.ClientID %>').focus();
                                        return false;
                                    }
                                }
                                else {
                                //document.getElementById('<%=txtReEmailS.ClientID %>').focus();
                                        return false;
                                    }
                                }
                                else {
                            //document.getElementById('<%=txtReMobiS.ClientID %>').focus();
                                    return false;
                                }

                            }
                            else {
                        //document.getElementById('<%=txtReFullNameS.ClientID %>').focus();
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
    function validate6() {
        try {
            if (IsNumeric('<%=txtlv4SMSPhoneNo.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                if (IsNumeric('<%=txtlv4MBPhoneNo.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                    if (IsNumeric('<%=txtlv4PHOPhoneNo.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                        if (validateEmpty('<%=txtlv4FullName.ClientID %>', '<%=Resources.labels. bannhaptenchutaikhoan %>')) {
                            if (validateEmpty('<%=txtlv4Mobi.ClientID %>', '<%=Resources.labels. bannhapsodienthoaichutaikhoan %>')) {
                                    if (validateEmpty('<%=txtlv4Email.ClientID %>', '<%=Resources.labels. bannhapemailchutaikhoan %>')) {
                                        //kiem tra so
                                        if (IsNumeric('<%=txtlv4Mobi.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>')) {
                                            //kiem tra email
                                            if (checkEmail('<%=txtlv4Email.ClientID %>', '<%=Resources.labels. emailkhongdinhdang %>')) {
                                                if (document.getElementById('<%=rblv4Type.ClientID %>').checked) {
                                                    if (validateEmpty('<%=txtlv4IBTypeUserName.ClientID %>', '<%=Resources.labels. bancannhaptendangnhap %>')) {
                                                        var minlength = <%= int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString())%>
                                                    var maxlength = <%= int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString())%>
                                                    if (document.getElementById('<%=txtlv4IBTypeUserName.ClientID %>').value.length >= minlength || document.getElementById('<%=txtlv4IBTypeUserName.ClientID %>').value.length <= maxlength) {

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
                                        //document.getElementById('<%=txtlv4Email.ClientID %>').focus();
                                                return false;
                                            }
                                        }
                                        else {
                                    //document.getElementById('<%=txtlv4Mobi.ClientID %>').focus();
                                            return false;
                                        }
                                    }
                                    else {
                                //document.getElementById('<%=txtlv4Email.ClientID %>').focus();
                                        return false;
                                    }
                                }
                                else {
                            //document.getElementById('<%=txtlv4Mobi.ClientID %>').focus();
                                    return false;
                                }

                            }
                            else {
                            //document.getElementById('<%=txtlv4FullName.ClientID %>').focus();
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
    function validate7() {
        try {
            if (IsNumeric('<%=txtlv5SMSPhoneNo.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                if (IsNumeric('<%=txtlv5MBPhoneNo.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                    if (IsNumeric('<%=txtlv5PHOPhoneNo.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                        if (validateEmpty('<%=txtlv5FullName.ClientID %>', '<%=Resources.labels. bannhaptenchutaikhoan %>')) {
                            if (validateEmpty('<%=txtlv5Mobi.ClientID %>', '<%=Resources.labels. bannhapsodienthoaichutaikhoan %>')) {
                                    if (validateEmpty('<%=txtlv5Email.ClientID %>', '<%=Resources.labels. bannhapemailchutaikhoan %>')) {
                                        //kiem tra so
                                        if (IsNumeric('<%=txtlv5Mobi.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>')) {
                                            //kiem tra email
                                            if (checkEmail('<%=txtlv5Email.ClientID %>', '<%=Resources.labels. emailkhongdinhdang %>')) {
                                                if (document.getElementById('<%=rblv5Type.ClientID %>').checked) {
                                                    if (validateEmpty('<%=txtlv5IBTypeUserName.ClientID %>', '<%=Resources.labels. bancannhaptendangnhap %>')) {
                                                        var minlength = <%= int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString())%>
                                                    var maxlength = <%= int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString())%>
                                                    if (document.getElementById('<%=txtlv5IBTypeUserName.ClientID %>').value.length >= minlength || document.getElementById('<%=txtlv5IBTypeUserName.ClientID %>').value.length <= maxlength) {

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
                                        //document.getElementById('<%=txtlv5Email.ClientID %>').focus();
                                                return false;
                                            }
                                        }
                                        else {
                                    //document.getElementById('<%=txtlv5Mobi.ClientID %>').focus();
                                            return false;
                                        }
                                    }
                                    else {
                                    //document.getElementById('<%=txtlv5Email.ClientID %>').focus();
                                        return false;
                                    }
                                }
                                else {
                                //document.getElementById('<%=txtlv5Mobi.ClientID %>').focus();
                                    return false;
                                }

                            }
                            else {
                            //document.getElementById('<%=txtlv5FullName.ClientID %>').focus();
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
</script>
<script type="text/javascript">
    function SetUserNameNUY(custcode, custtype, length) {
        var date = new Date();
        var a = ticks(date).toString().length - 10;
        var b = length - ((custcode + custtype).length);
        var c = 10 - (custcode.length);

        var un = custcode + custtype + ticks(date).toString().substring(a, a + b) + "5";
        var NUYMB = custcode + ticks(date).toString().substring(a, a + c) + "5";
        var NUYPHO = custcode + ticks(date).toString().substring(a, a + c) + "5";

        document.getElementById('<%=txtUserNameNguoiUyQuyen.ClientID %>').value = un;

        document.getElementById('<%=txtFullnameNguoiUyQuyen.ClientID %>').value = '';
        document.getElementById('<%=txtPhoneNguoiUyQuyen.ClientID %>').value = '';
        document.getElementById('<%=txtEmailNguoiUyQuyen.ClientID %>').value = '';
        document.getElementById('<%=txtBirthNguoiUyQuyen.ClientID %>').value = '';
        document.getElementById('<%=txtAddressNguoiUyQuyen.ClientID %>').value = '';

        document.getElementById('<%=txtSMSPhoneNguoiUyQuyen.ClientID %>').value = '';
        document.getElementById('<%=txtMBPhoneNguoiUyQuyen.ClientID %>').value = NUYMB;
        document.getElementById('<%=txtPHOPhoneNguoiUyQuyen.ClientID %>').value = NUYPHO;

        document.getElementById('<%=txtFullnameNguoiUyQuyen.ClientID %>').focus();
        return false;
    }

    function SetUserNameL2(custcode, custtype, length) {
        var date = new Date();
        var a = ticks(date).toString().length - 10;
        var b = length - ((custcode + custtype).length);
        var c = 10 - (custcode.length);

        var un = custcode + custtype + +ticks(date).toString().substring(a, a + c) + "6";
        var L2MB = custcode + ticks(date).toString().substring(a, a + c) + "6";
        var L2PHO = custcode + ticks(date).toString().substring(a, a + c) + "6";

        document.getElementById('<%=txtUserNameLevel2.ClientID %>').value = un;

        document.getElementById('<%=txtFullNameLevel2.ClientID %>').value = '';
        document.getElementById('<%=txtPhoneLevel2.ClientID %>').value = '';
        document.getElementById('<%=txtEmailLevel2.ClientID %>').value = '';
        document.getElementById('<%=txtBirthdayLevel2.ClientID %>').value = '';
        document.getElementById('<%=txtAddresslevel2.ClientID %>').value = '';

        document.getElementById('<%=txtSMSPhoneLevel2.ClientID %>').value = '';
        document.getElementById('<%=txtMBPhoneLevel2.ClientID %>').value = L2MB;
        document.getElementById('<%=txtPHOPhoneLevel2.ClientID %>').value = L2PHO;

        document.getElementById('<%=txtFullNameLevel2.ClientID %>').focus();
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
</script>
<script type="text/javascript">
    var minlength = <%= int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString())%>;
    var maxlength = <%= int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString())%>;

    //check QT
    $(document).on("click", "#rbTypeQT", function () {
        var elem = document.getElementById('<%=rbTypeQT.ClientID %>')
        var txtType = document.getElementById('<%=txtIBTypeUserNameQT.ClientID %>')
        var hdf = document.getElementById('<%=hdfIBUserNameQT.ClientID %>')
        if (elem.checked == true) {
            txtType.disabled = false;
            hdf.value = txtType.value;
        }
    });
    $(document).on("click", "#rbGenerateQT", function () {
        var elem = document.getElementById('<%=rbGenerateQT.ClientID %>')
        var txtType = document.getElementById('<%=txtIBTypeUserNameQT.ClientID %>')
        var txtGen = document.getElementById('<%=txtIBGenUserNameQT.ClientID %>')
        var hdf = document.getElementById('<%=hdfIBUserNameQT.ClientID %>')
        if (elem.checked == true) {
            txtType.disabled = true;
            hdf.value = txtGen.value;
        }
    });
    $(document).on("change", "#<%=txtIBTypeUserNameQT.ClientID %>", function () {
        var txtType = document.getElementById('<%=txtIBTypeUserNameQT.ClientID %>');
        if (txtType.value.length < minlength || txtType.value.length > maxlength) {
            alert('<%=string.Format(Resources.labels.usernamemustbetween, ConfigurationManager.AppSettings["minlengthloginname"].ToString(), ConfigurationManager.AppSettings["maxlengthloginname"].ToString()) %>');
        }
        validateCode(txtType.value);
    });

    //check Holder
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

    //check Holder simple
    $(document).on("click", "#rbTypeS", function () {
        var elem = document.getElementById('<%=rbTypeS.ClientID %>')
        var txtType = document.getElementById('<%=txtIBTypeUserNameS.ClientID %>')
        var hdf = document.getElementById('<%=hdfIBUserNameS.ClientID %>')
        if (elem.checked == true) {
            txtType.disabled = false;
            hdf.value = txtType.value;
        }
    });
    $(document).on("click", "#rbGenerateS", function () {
        var elem = document.getElementById('<%=rbGenerateS.ClientID %>')
        var txtType = document.getElementById('<%=txtIBTypeUserNameS.ClientID %>')
        var txtGen = document.getElementById('<%=txtIBGenUserNameS.ClientID %>')
        var hdf = document.getElementById('<%=hdfIBUserNameS.ClientID %>')
        if (elem.checked == true) {
            txtType.disabled = true;
            hdf.value = txtGen.value;
        }
    });
    $(document).on("change", "#<%=txtIBTypeUserNameS.ClientID %>", function () {
        var txtType = document.getElementById('<%=txtIBTypeUserNameS.ClientID %>');
        if (txtType.value.length < minlength || txtType.value.length > maxlength) {
            alert('<%=string.Format(Resources.labels.usernamemustbetween, ConfigurationManager.AppSettings["minlengthloginname"].ToString(), ConfigurationManager.AppSettings["maxlengthloginname"].ToString()) %>');
        }
        validateCode(txtType.value);
    });

    //check Finacial Controller
    $(document).on("click", "#rblv4Type", function () {
        var elem = document.getElementById('<%=rblv4Type.ClientID %>')
        var txtType = document.getElementById('<%=txtlv4IBTypeUserName.ClientID %>')
        var hdf = document.getElementById('<%=hdflv4IBUserName.ClientID %>')
        if (elem.checked == true) {
            txtType.disabled = false;
            hdf.value = txtType.value;
        }
    });
    $(document).on("click", "#rblv4Generate", function () {
        var elem = document.getElementById('<%=rblv4Generate.ClientID %>')
        var txtType = document.getElementById('<%=txtlv4IBTypeUserName.ClientID %>')
        var txtGen = document.getElementById('<%=txtlv4IBGenUserName.ClientID %>')
        var hdf = document.getElementById('<%=hdflv4IBUserName.ClientID %>')
        if (elem.checked == true) {
            txtType.disabled = true;
            hdf.value = txtGen.value;
        }
    });
    $(document).on("change", "#<%=txtlv4IBTypeUserName.ClientID %>", function () {
        var txtType = document.getElementById('<%=txtlv4IBTypeUserName.ClientID %>');
        if (txtType.value.length < minlength || txtType.value.length > maxlength) {
            alert('<%=string.Format(Resources.labels.usernamemustbetween, ConfigurationManager.AppSettings["minlengthloginname"].ToString(), ConfigurationManager.AppSettings["maxlengthloginname"].ToString()) %>');
        }
        validateCode(txtType.value);
    });

    //check Accountant
    $(document).on("click", "#rblv5Type", function () {
        var elem = document.getElementById('<%=rblv5Type.ClientID %>')
        var txtType = document.getElementById('<%=txtlv5IBTypeUserName.ClientID %>')
        var hdf = document.getElementById('<%=hdflv5IBUserName.ClientID %>')
        if (elem.checked == true) {
            txtType.disabled = false;
            hdf.value = txtType.value;
        }
    });
    $(document).on("click", "#rblv5Generate", function () {
        var elem = document.getElementById('<%=rblv5Generate.ClientID %>')
        var txtType = document.getElementById('<%=txtlv5IBTypeUserName.ClientID %>')
        var txtGen = document.getElementById('<%=txtlv5IBGenUserName.ClientID %>')
        var hdf = document.getElementById('<%=hdflv5IBUserName.ClientID %>')
        if (elem.checked == true) {
            txtType.disabled = true;
            hdf.value = txtGen.value;
        }
    });
    $(document).on("change", "#<%=txtlv5IBTypeUserName.ClientID %>", function () {
        var txtType = document.getElementById('<%=txtlv5IBTypeUserName.ClientID %>');
        if (txtType.value.length < minlength || txtType.value.length > maxlength) {
            alert('<%=string.Format(Resources.labels.usernamemustbetween, ConfigurationManager.AppSettings["minlengthloginname"].ToString(), ConfigurationManager.AppSettings["maxlengthloginname"].ToString()) %>');
        }
        validateCode(txtType.value);
    });
    //check Authorized
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

    //check UsernameLV2
    $(document).on("click", "#rbTypeLevel2", function () {
        var elem = document.getElementById('<%=rbTypeLevel2.ClientID %>')
        var txtType = document.getElementById('<%=txtIBTypeUserNameLevel2.ClientID %>')
        var hdf = document.getElementById('<%=hdfIBUserNameLevel2.ClientID %>')
        if (elem.checked == true) {
            txtType.disabled = false;
            hdf.value = txtType.value;
        }
    });
    $(document).on("click", "#rbGenerateLevel2", function () {
        var elem = document.getElementById('<%=rbGenerateLevel2.ClientID %>')
        var txtType = document.getElementById('<%=txtIBTypeUserNameLevel2.ClientID %>')
        var txtGen = document.getElementById('<%=txtIBGenUserNameLevel2.ClientID %>')
        var hdf = document.getElementById('<%=hdfIBUserNameLevel2.ClientID %>')
        if (elem.checked == true) {
            txtType.disabled = true;
            hdf.value = txtGen.value;
        }
    });
    $(document).on("change", "#<%=txtIBTypeUserNameLevel2.ClientID %>", function () {
        var txtType = document.getElementById('<%=txtIBTypeUserNameLevel2.ClientID %>');
        if (txtType.value.length < minlength || txtType.value.length > maxlength) {
            alert('<%=string.Format(Resources.labels.usernamemustbetween, ConfigurationManager.AppSettings["minlengthloginname"].ToString(), ConfigurationManager.AppSettings["maxlengthloginname"].ToString()) %>');
        }
        validateCode(txtType.value);
    });
</script>
