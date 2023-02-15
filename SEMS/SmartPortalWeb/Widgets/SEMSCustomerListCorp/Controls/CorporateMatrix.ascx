<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CorporateMatrix.ascx.cs" Inherits="Widgets_SEMSCustomerListCorp_Controls_CorporateMatrix" %>

<style>
    .style1 tr td:nth-child(2n+1) {
        width: 15%;
    }

    .style1 tr td:nth-child(2n) {
        width: 35%;
    }
</style>

<div class="divGetInfoCust" id="divAction" runat="server" visible="false">
    <div class="divHeaderStyle">
        User contract list
    </div>
    <table class="style1" cellspacing="0" cellpadding="5">
        <tr>
            <td>
                <asp:RadioButton ID="rdAdd" Checked="true" GroupName="MTAction" runat="server"
                    Text="New User" onClick="enableMTAdd();" />
            </td>
            <td>
                <asp:TextBox ID="txtMTCuscode" placeholder="<%$ Resources:labels, custcode %>" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RadioButton ID="rdEdit" GroupName="MTAction" runat="server" Text="Edit User"
                    onClick="enableMTEdit();" />
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" />
                <asp:Button ID="btnMatrixDetail" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                    OnClick="btnMatrixDetail_Click" />
            </td>
        </tr>
    </table>
</div>
<div class="divGetInfoCust">
    <div class="divHeaderStyle">
        User information
    </div>
    <asp:Panel ID="Panel6" runat="server">
        <table class="style1" cellspacing="0" cellpadding="5">
            <tr id="trAddUser" runat="server" visible="true">
                <td>
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, custcode %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMTCuscodeAdd" runat="server"></asp:TextBox>
                    <asp:Button ID="btnMatrixDetailAdd" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                        OnClick="btnMatrixDetailAdd_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label174" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                    &nbsp;*
                </td>
                <td>
                    <asp:TextBox ID="txtMTFullName" runat="server" onkeyup="ValidateTextbox(this,200);" onkeyDown="ValidateTextbox(this,200);" onpaste="ValidateTextbox(this,200);"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label175" runat="server" Text="Email"></asp:Label>
                    &nbsp;*
                </td>
                <td>
                    <asp:TextBox ID="txtMTEmail" runat="server" onkeyup="ValidateTextbox(this,200);" onkeyDown="ValidateTextbox(this,200);" onpaste="ValidateTextbox(this,200);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label176" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                    &nbsp;*
                </td>
                <td>
                    <asp:TextBox ID="txtMTPhone" runat="server" onkeyup="ValidatePhone(this,200);" onkeyDown="ValidatePhone(this,200);" onpaste="ValidatePhone(this,200);"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label177" runat="server" Text="<%$ Resources:labels, gender %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlMTGender" runat="server">
                        <asp:ListItem Value="M" Text="<%$ Resources:labels, male %>"></asp:ListItem>
                        <asp:ListItem Value="F" Text="<%$ Resources:labels, female %>"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label178" runat="server" Text="<%$ Resources:labels, birthday %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMTBirth" runat="server" onkeyup="ValidateTextbox(this,200);" onkeyDown="ValidateTextbox(this,200);" onpaste="ValidateTextbox(this,200);"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label179" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMTAddress" runat="server" TextMode="MultiLine" onkeyup="ValidateTextbox(this,200);" onkeyDown="ValidateTextbox(this,200);" onpaste="ValidateTextbox(this,200);"></asp:TextBox>
                </td>
            </tr>

          <%--  <tr>
                <td>
                    <asp:Label ID="Label183" runat="server" Text="<%$ Resources:labels, bac %>"></asp:Label>&nbsp;*
                </td>
                <td>
                    <asp:DropDownList ID="ddlUserLevel" runat="server" OnSelectedIndexChanged="ddlUserLevel_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label184" runat="server" Text="<%$ Resources:labels, group %>"></asp:Label>&nbsp;*
                </td>
                <td>
                    <asp:DropDownList ID="ddlGroup" runat="server" onchange="changeGroup();">
                    </asp:DropDownList>
                    <asp:HiddenField runat="server" ID="hdGroupID" />
                </td>
            </tr>--%>
        </table>
        <%--</ContentTemplate>--%>
    </asp:Panel>
</div>


<div style="margin-top: 10px;">
    <div class="divGetInfoCust">
        <div class="divHeaderStyle">
            <%=Resources.labels.taikhoandangky %>
        </div>
        <table class="style1" style="width: 100%;">
            <tr>
                <td>
                    <asp:RadioButton ID="radIBAllAccount" Checked="true" GroupName="MTAccInfo" runat="server"
                        Text="<%$ Resources:labels, tatcataikhoan %>" onClick="disnableMTAQT();" />
                </td>
                <td></td>
                <td>
                    <asp:RadioButton ID="radIBAccount" GroupName="MTAccInfo" runat="server" Text="<%$ Resources:labels, theotaikhoan %>"
                        onClick="enableMTAQT();" />
                </td>
                <td>
                    <asp:DropDownList ID="ddlIBAccount" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>

</div>

<div style="height: 250px; margin: 10px 5px 5px 5px">
    <div id="tabViewMT">
        <% if (TabCustomerInfoHelper.TabIBVisibility == 1)
            { %>
        <div class="dhtmlgoodies_aTab">
            <table class="tblVDC" cellspacing="0" cellpadding="4">
                <tr>
                    <td colspan="2" style="background-color: #E0ECFF">
                        <asp:Label ID="Label180" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                    </td>
                    <td style="background-color: #E0ECFF">
                        <asp:Label ID="Label181" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; height: 50px" valign="top">
                        <asp:Label ID="Label182" runat="server" Text="<%$ Resources:labels, username  %>"></asp:Label>
                    </td>
                    <td valign="top" style="width: 25%;">
                                <asp:RadioButton ID="rbIBGenerate" OnCheckedChanged="rbIBGenerate_CheckedChanged" AutoPostBack="true" runat="server" GroupName="rbIBUserName" Checked="true" onclick="disableUsername()"
                                    ClientIDMode="Static" />
                                <asp:TextBox ID="txtIBGenUserName" Enabled="false" runat="server"></asp:TextBox>
                                <br />
                                <asp:RadioButton ID="rbIBType" runat="server" GroupName="rbIBUserName" ClientIDMode="Static" onclick="enableUsername()" />
                                <asp:TextBox ID="txtIBTypeUserName" runat="server" onkeyup="ValidateTextbox(this,200);" onkeyDown="ValidateTextbox(this,200);" onpaste="ValidateTextbox(this,200);"></asp:TextBox>

                    </td>
                    <td rowspan="2" style="width: 50%;">
                        <div style="width: 100%; height: 150px; overflow: auto;">
                            <asp:TreeView ID="tvIBRole" runat="server">
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
                        <asp:DropDownList ID="ddlPolicyIB" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMTPolicyIB_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>

            </table>
        </div>
        <%}
            if (TabCustomerInfoHelper.TabSMSVisibility == 1 && Convert.ToString(ConfigurationManager.AppSettings["tabSMSVisibilityCorp"]) == "1")
            { %>
        <div class="dhtmlgoodies_aTab">
            <table class="tblVDC" cellspacing="0" cellpadding="4">
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
                        <asp:TextBox ID="txtSMSPhone" runat="server" onkeyup="ValidatePhone(this,200);" onkeyDown="ValidatePhone(this,200);" onpaste="ValidatePhoneValidatePhone(this,200);"></asp:TextBox>
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
                        <asp:Label ID="Label94" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddlSMSDefaultAccount" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" valign="top">
                        <asp:Label ID="Label95" runat="server" Text="<%$ Resources:labels, language %>"></asp:Label>
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddlSMSDefaultLang" runat="server">
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
                        <asp:DropDownList ID="ddlPolicySMS" runat="server"></asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td style="width: 20%;" valign="top">
                        <asp:CheckBox ID="cbSMSIsDefault" runat="server" Text="<%$ Resources:labels, sudungmacdinh %>"
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
                        <asp:TextBox ID="txtMBPhone" type="number" runat="server" Enabled="False" onkeyup="ValidatePhone(this,200);" onkeyDown="ValidatePhone(this,200);" onpaste="ValidatePhoneValidatePhone(this,200);"></asp:TextBox>
                    </td>
                    <td rowspan="2" style="width: 50%;">
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
                        <%--<asp:Label ID="Label179" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                    </td>
                    <td valign="top" style="width: 25%;">
                        <asp:DropDownList ID="ddlPolicyMB" runat="server"></asp:DropDownList>
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
                        <asp:TextBox ID="txtPHOPhone" type="number" runat="server" Enabled="False"></asp:TextBox>
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
                        <asp:Label ID="Label102" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                    </td>
                    <td style="width: 25%;" valign="top">
                        <asp:DropDownList ID="ddlMTPHODefaultAcctno" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <%} %>
    </div>
</div>

<asp:UpdateProgress ID="UpdateProgress5" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel9">
    <ProgressTemplate>
        <div style="text-align: center">
            <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
            <%=Resources.labels.loading %>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="btnAddNewUser" />
        <asp:PostBackTrigger ControlID="btnMTCancel" />
    </Triggers>
    <ContentTemplate>
        <div style="text-align: right;">
            &nbsp;
                                <asp:Button ID="btnAddUserMT" runat="server" Text="<%$ Resources:labels, adduser %>" Width="170px"
                                    OnClick="btnAddUserMT_Click" OnClientClick="return validate();" />
            &nbsp;
                                <asp:Button ID="btnMTCancel" runat="server" OnClick="btnMTCancel_Click" Text="<%$ Resources:labels, xoa %>" Visible="false" />
            &nbsp;
        </div>
        <div style="text-align: center">
            <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
        </div>
        <div id="div6" style="margin: 15px 5px 5px 5px; padding: 0px 0px 0px 2px; max-width: 100%; max-height: 150px; overflow: auto;">
            <asp:GridView ID="gvMTUser" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                OnPageIndexChanging="gvMTUser_PageIndexChanging" OnRowDeleting="gvMTUser_RowDeleting">
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
        <div style="margin-top: 10px;">
            <div class="addnewuser" style="text-align: right">
                &nbsp;
        <asp:Button ID="btnAddNewUser" Visible="false" runat="server" Text="<%$ Resources:labels, newuser %>" OnClick="btnAddNewUser_Click" />&nbsp;
            </div>

        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:Panel runat="server" ID="pnLuu">
    <div style="text-align: center; padding-top: 10px;">
        <asp:Button ID="btnCustSave" runat="server" OnClick="btnCustSave_Click"
            Text="Save Contract" Width="69px" />
        &nbsp;
            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="<%$ Resources:labels, back %>" />
        &nbsp; &nbsp;
    </div>
</asp:Panel>
<script type="text/javascript">
    onReady();
    //changeGroup();
    //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    //function EndRequestHandler(sender, args) {
    //    onReady();
    //}
    <%--function changeGroup() {
        document.getElementById("<%=hdGroupID.ClientID %>").value = document.getElementById("<%=ddlGroup.ClientID %>").value;
    }--%>

    function onReady() {
        initTabs('tabViewMT', Array(<%=TabCustomerInfoHelper.TabName%>), 0, 967, 188, Array(false, false, false, false));
        var cal = Calendar.setup({
            onSelect: function (cal) { cal.hide() }
        });
        cal.manageFields("<%=txtMTBirth.ClientID %>", "<%=txtMTBirth.ClientID %>", "%d/%m/%Y");

        if (document.getElementById("<%=rdAdd.ClientID %>").checked) {
            enableMTAdd();
        }
        else {
            enableMTEdit();
        }
    }

    function disableUsername() {
        document.getElementById("<%=txtIBTypeUserName.ClientID %>").disabled = true;
    }
    function enableUsername() {
        document.getElementById("<%=txtIBTypeUserName.ClientID %>").disabled = false;
    }
    function disnableMTAQT() {
        document.getElementById("<%=ddlIBAccount.ClientID %>").disabled = true;
    }
    function enableMTAQT() {
        document.getElementById("<%=ddlIBAccount.ClientID %>").disabled = false;
    }

    function enableMTAdd() {
        document.getElementById("<%=txtMTCuscode.ClientID %>").disabled = false;
        document.getElementById("<%=ddlUser.ClientID %>").disabled = true;
    }
    function enableMTEdit() {
        document.getElementById("<%=txtMTCuscode.ClientID %>").disabled = true;
        document.getElementById("<%=ddlUser.ClientID %>").disabled = false;
    }
    function PhoneReplace(obj) {
        if (obj.value.length > 0) {
            obj.value = obj.value.replace(/'|!|@|#|\,|=|:|;|"|\{|\}|\[|\]|\$|%|\^|&|\/|\\|\||<|>|\?|\*/g, "");
        }
    }
    function replaceChar(obj) {
        if (obj.value.length > 0) {
            obj.value = obj.value.replace(/'|!|#|\,|=|:|;|"|\{|\}|\[|\]|\$|%|\^|&|\/|\\|\||<|>|\?|\*/g, "");
        }
    }
    function ValidateTextbox(obj, maxchar) {
        if (this.id) obj = this;
        replaceChar(obj);
        var remaningChar = maxchar - obj.value.length;

        if (remaningChar <= 0) {
            obj.value = obj.value.substring(maxchar, 0);
        }
    }
     function ValidatePhone(obj, maxchar) {
        if (this.id) obj = this;
        PhoneReplace(obj);
        var remaningChar = maxchar - obj.value.length;

        if (remaningChar <= 0) {
            obj.value = obj.value.substring(maxchar, 0);
        }
    }
    function validate() {
        try {
            if (IsNumeric('<%=txtSMSPhone.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                if (IsNumeric('<%=txtMBPhone.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                    if (IsNumeric('<%=txtPHOPhone.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>.')) {
                        if (validateEmpty('<%=txtMTFullName.ClientID %>', '<%=Resources.labels. banhaptennguoiquantrihethong %>')) {
                            if (validateEmpty('<%=txtMTPhone.ClientID %>', '<%=Resources.labels. bannhapsodienthoainguoiquantrihethong %>')) {
                                if (validateEmpty('<%=txtMTEmail.ClientID %>', '<%=Resources.labels. bannhapemailnguoiquantrihethong %>')) {

                                    //kiem tra so
                                    if (IsNumeric('<%=txtMTPhone.ClientID %>', '<%=Resources.labels. sodienthoaikhongdungdinhdangso %>')) {
                                        //kiem tra email
                                        if (checkEmail('<%=txtMTEmail.ClientID %>', '<%=Resources.labels. emailkhongdinhdang %>')) {
                                            if (document.getElementById('<%=rbIBType.ClientID %>').checked) {
                                                if (validateEmpty('<%=txtIBTypeUserName.ClientID %>', '<%=Resources.labels. bancannhaptendangnhap %>')) {
                                                    var minlength = <%= ConfigurationManager.AppSettings["minlengthloginname"].ToString()%>
                                                        var maxlength = <%= ConfigurationManager.AppSettings["maxlengthloginname"].ToString()%>
                                                        if (document.getElementById('<%=txtIBTypeUserName.ClientID %>').value.length >= minlength || document.getElementById('<%=txtIBTypeUserName.ClientID %>').value.length <= maxlength) {

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
            else {

                return false;
            }
        }
        catch (err) {
        }
    }

</script>
