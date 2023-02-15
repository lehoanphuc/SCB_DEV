<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContractList_AddWallet_Widget" %>
<link href="widgets/SEMSCustomerList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>
<script src="widgets/SEMSCustomerList/JS/tab-view.js" type="text/javascript"></script>
<script src="Widgets/PagePermission/Scripts/JScript.js" type="text/javascript"></script>
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
<script type="text/javascript" src="js/Validate.js"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSCustomerList/Images/messenger.png" style="width: 32px; height: 32px; margin-bottom: 10px;"
        align="middle" />
    <%=Resources.labels.themmoikhachhang %>
</div>
<div id="divError">
    <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="#990000"></asp:Label>
</div>
<asp:Panel runat="server" ID="pnCustInfo">
        <div class="divGetInfoCust">
            <div class="divHeaderStyle">
                <%=Resources.labels.laythongtinkhachhang %>
            </div>
            <table class="style1" cellspacing="0" cellpadding="5">
                <tr>
                    <td>
                    <asp:Label runat="server"   Text="<%$ Resources:labels, phone %>" />
                    </td>

                    <td>
                        <asp:TextBox ID="txtCustCodeWL" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 10%;">
                        <asp:Button ID="btnSearchWL" runat="server" Text="<%$ Resources:labels, checkphone %>"
                            OnClick="btnSearchWL_Click"/>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
             
            </table>
        </div>

         <div class="divGetInfoCust"> 
                  <div class="divHeaderStyle">
            <%=Resources.labels.thongtinkhachhang %>
        </div>
                <br />
            <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                    <td style="width: 25%;">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                    &nbsp;*
                </td>
                <td style="width: 25%;">
                    <asp:TextBox ID="txtFullName" runat="server" ></asp:TextBox>
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
                    <asp:DropDownList ID="ddlCustType" runat="server" Enabled="False">
                    </asp:DropDownList>
                </td>
            </tr>
                            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, birthday %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBirth" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, gender %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGender" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
                            <tr>
                <td valign="top">
                    <asp:Label ID="Label15" runat="server" Text="<%$ Resources:labels, diachithuongtru %>"></asp:Label>&nbsp;*
                </td>
                <td>
                    <asp:TextBox ID="txtResidentAddr" runat="server" TextMode="MultiLine" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');"></asp:TextBox>
                </td>
                <td valign="top">
                    <asp:Label ID="Label16" runat="server" Text="<%$ Resources:labels, diachitamtru %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTempAddress" runat="server" TextMode="MultiLine" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');"></asp:TextBox>
                </td>
            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, hochieuchungminh %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIF" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, ngaycap %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIssueDate" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, noicap %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIssuePlace" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, quocgia %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlNation" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, sofax %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:labels, nghenghiep %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtJob" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:labels, diachilamviec %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOfficeAddr" runat="server" TextMode="MultiLine" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Text="<%$ Resources:labels, dienthoaicoquan %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCompanyPhone" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, ghichu %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');"></asp:TextBox>
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
                    &nbsp;*
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlProduct" runat="server" SkinID="extDDL1">
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
        &nbsp;<asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click"
            Text="<%$ Resources:labels, next %>"  OnClientClick="return validateWL();" />
                    &nbsp;
             <asp:Button ID="Button4" runat="server" Text="<%$ Resources:labels, lamlai %>" />
        &nbsp;
    </div>
 </asp:Panel>
<asp:Panel runat="server" ID="pnPersonal">
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
                                    &nbsp;
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
                                    <asp:Label ID="Label34" runat="server" Text="<%$ Resources:labels, capbac %> " Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblLevel" runat="server" Text="0" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                   </asp:Panel>
        <asp:Panel ID="Panel1" runat="server">
                         <table id="tblWL" class="tblVDC" cellspacing="0" cellpadding="4">
                                    <tr>
                                        <td colspan="2" style="background-color: #E0ECFF">
                                            <asp:Label ID="Label62" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                        </td>
                                        <td style="background-color: #E0ECFF">
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
            </asp:Panel>
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
                                    OnClick="btnThemChuTaiKhoan_Click"    />
                                &nbsp;
                                <asp:Button ID="btnHuy" runat="server" OnClick="btnHuy_Click" Text="<%$ Resources:labels, delete %>" />
                                &nbsp;
                            </div>
                            <asp:Label runat="server" ID="lblAlert" ForeColor="Red"></asp:Label>
                            <div id="div3" style="margin-top: 20px; height: 150px; overflow: auto;">
                                <asp:GridView ID="gvResultChuTaiKhoan" runat="server" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" Width="100%" 
                                    OnPageIndexChanging="gvResultChuTaiKhoan_PageIndexChanging"
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
                    </asp:UpdatePanel>
     <asp:Panel runat="server" ID="pnLuu">
        <div style="text-align: center; padding-top: 10px;">
            <asp:Button ID="btnCustSave" runat="server" OnClick="btnCustSave_Click" OnClientClick="return validate1();"
                Text="<%$ Resources:labels, save %>" Width="69px" />
            &nbsp;
            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="<%$ Resources:labels, back %>" />
            &nbsp; &nbsp;
        </div>
    </asp:Panel>
</asp:Panel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function validateWL() {
        if (validateEmpty('<%=txtContractNo.ClientID %>', '<%=Resources.labels.mahopdongkhongrong %>')) {
            if (validateEmpty('<%=txtFullName.ClientID %>', '<%=Resources.labels.bannhaptenkhachhang %>')) {
                if (checkEmail('<%=txtEmail.ClientID %>', '<%=Resources.labels.emailkhongdungdinhdang %>')) {
                    if (validateEmpty('<%=txtResidentAddr.ClientID %>', '<%=Resources.labels.bancannhapdiachithuongtru %>')) {

                    } else {
                        document.getElementById('<%=txtResidentAddr.ClientID %>').focus();
                        return false;
                    }
                }
                else {
                    document.getElementById('<%=txtEmail.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtFullName.ClientID %>').focus();
                return false;
            }
        }
        else {
            document.getElementById('<%=txtContractNo.ClientID %>').focus();
            return false;
        }
    }
    function validate1() {
        if (validateEmpty('<%=txtContractNo.ClientID %>', '<%=Resources.labels.mahopdongkhongrong %>')) {
        }
    }
  </script>