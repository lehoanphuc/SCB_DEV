<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCustomerListCorp_Controls_Widget" %>
<%@ Register Src="../../../Controls/LetterSearch/LetterSearch.ascx" TagName="LetterSearch" TagPrefix="uc1" %>

<link href="widgets/SEMSCustomerList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="widgets/SEMSCustomerList/js/commonjs.js"> </script>
<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>
<script src="widgets/SEMSCustomerList/JS/tab-view.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/lang/en.js" type="text/javascript"></script>
<link href="widgets/IBTransactionHistory1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/border-radius.css" rel="stylesheet" type="text/css" />

<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSCustomerList/Images/messenger.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.thongtinkhachhang %>
</div>
<div style="text-align: center; color: Red; font-weight: bold;">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>

<asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave">
    <div id="dhtmlgoodies_tabView1">
        <div class="dhtmlgoodies_aTab">
            <table id="tblVDC" cellspacing="1" cellpadding="3">
                <tr>
                    <td class="tdVDC">
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, makhachhang %>"></asp:Label>
                        &nbsp;*</td>
                    <td>
                        <asp:TextBox ID="txtCustID" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdVDC">
                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                        &nbsp;*</td>
                    <td>
                        <asp:TextBox ID="txtFullName" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdVDC">
                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, tenviettat %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtShortName" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdVDC">
                        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, loaikhachhang %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCustType" runat="server">
                            <asp:ListItem Value="O" Text="<%$ Resources:labels, doanhnghiep %>"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:HiddenField ID="hfCustType" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="tdVDC">
                        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                        &nbsp;*</td>
                    <td>
                        <asp:TextBox ID="txtMobi" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdVDC">
                        <asp:Label ID="Label8" runat="server" Text="Email"></asp:Label>
                        &nbsp;*</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdVDC">
                        <asp:Label ID="Label45" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>&nbsp;*
                    </td>
                    <td>
                        <asp:TextBox ID="txtResidentAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td class="tdVDC">
                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, ngaythanhlap %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBirth" runat="server"></asp:TextBox></td>
                </tr>

                <tr>
                    <td class="tdVDC">
                        <asp:Label ID="Label57" runat="server" Text="<%$ Resources:labels, gpkd %>"></asp:Label>
                        &nbsp;*</td>
                    <td>
                        <asp:TextBox ID="txtIF" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdVDC">
                        <asp:Label ID="Label59" runat="server" Text="Fax"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFAX" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="tdVDC">
                        <asp:Label ID="Label69" runat="server" Text="<%$ Resources:labels, ghichu %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td class="tdVDC"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="tdVDC">
                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, makhachhang %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCustCode" runat="server"></asp:Label>
                    </td>
                    <td class="tdVDC"></td>
                    <td></td>
                </tr>
            </table>
            <script type="text/javascript">//<![CDATA[

                var cal = Calendar.setup({
                    onSelect: function (cal) { cal.hide() }
                });
                cal.manageFields("<%=txtBirth.ClientID %>", "<%=txtBirth.ClientID %>", "%d/%m/%Y");


    //]]></script>
        </div>
        <div class="dhtmlgoodies_aTab">
            <asp:Panel runat="server" ID="pnToolbarContract">
                <div class="divToolbar">
                    &nbsp;
            <asp:Button ID="btnAddNewContract" runat="server" Text="<%$ Resources:labels, themmoi %>"
                OnClick="btnAddNew_Click" />
                    &nbsp;
            <asp:Button ID="Button6" runat="server" Text="<%$ Resources:labels, donghopdong %>" 
                OnClick="Button6_Click" />
                    &nbsp;
            <asp:Button ID="Button7" runat="server" Text="<%$ Resources:labels, exporttofile %>" OnClientClick="showPopWin('widgets/SEMSCustomerList/ExportToFile.aspx', 170, 160, null);setTitle(this);return false;" />
                </div>
            </asp:Panel>
            <div class="divResult">
                <asp:GridView ID="gvcontractList" runat="server" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                    Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    OnRowDataBound="gvcontractList_RowDataBound" PageSize="15">
                    <RowStyle ForeColor="#000000" />
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbxSelect" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, mahopdong %>" SortExpression="contractCode">
                            <ItemTemplate>
                                <asp:HyperLink ID="hpcontractCode" runat="server">[hpDetails]</asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, khachhang %>">
                            <ItemTemplate>
                                <asp:Label ID="lblcustName" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, nguoimo %>">
                            <ItemTemplate>
                                <asp:Label ID="lblOpen" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, nguoimo %>">
                            <ItemTemplate>
                                <asp:Label ID="lblOpendate" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, ngayhethan %>">
                            <ItemTemplate>
                                <asp:Label ID="lblClosedate" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, loaihopdong %>">
                            <ItemTemplate>
                                <asp:Label ID="lblContractType" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" CssClass="gvHeader" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="<%$ Resources:labels, edit %>" Visible="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="hpEdit" runat="server">[hpEdit]</asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>" Visible="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" CssClass="pager" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#E0ECFF" Font-Bold="True" ForeColor="Black" />
                </asp:GridView>
            </div>

        </div>
        <div class="dhtmlgoodies_aTab">
            <asp:Panel runat="server" ID="pnToolbarUser">
                <div class="divToolbar">
                    &nbsp;
            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" />
                    &nbsp;
            <asp:Button ID="Button3" runat="server" Text="<%$ Resources:labels, delete %>"/>
                    &nbsp;
            <asp:Button ID="Button4" runat="server" Text="<%$ Resources:labels, exporttofile %>" OnClientClick="showPopWin('widgets/SEMSCustomerList/ExportToFile.aspx', 170, 160, null);setTitle(this);return false;" />
                </div>
            </asp:Panel>
            <div class="divResult">
                <asp:GridView ID="gvCustomerList" runat="server" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                    Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvCustomerList_RowDataBound" PageSize="15">
                    <RowStyle ForeColor="#000000" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbxSelect" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tennguoidung %>" SortExpression="FULLNAME">
                            <ItemTemplate>
                                <asp:HyperLink ID="hpUserFullName" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, phone %>">
                            <ItemTemplate>
                                <asp:Label ID="lblPhone" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, kieunguoidung %>">
                            <ItemTemplate>
                                <asp:Label ID="lblUserType" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, edit %>">
                            <ItemTemplate>
                                <asp:HyperLink ID="hpEdit" runat="server">[hpEdit]</asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                            <ItemTemplate>
                                <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" CssClass="pager" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#E0ECFF" Font-Bold="True" ForeColor="Black" />
                </asp:GridView>
            </div>
            <%--</ContentTemplate>
	</asp:UpdatePanel>--%>
        </div>

        <div style="margin-top: 10px; text-align: center;">
            &nbsp;
            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate();" OnClick="btnSave_Click" />
            &nbsp;
            <asp:Button ID="btnDongBoKH" runat="server" Text="<%$ Resources:labels, dongbotucorebank %>" OnClick="btnDongBoKH_Click" />
            &nbsp;
        <asp:Button ID="Button8" runat="server" Text="<%$ Resources:labels, back %>" CssClass="btnGeneral" PostBackUrl="javascript:history.go(-1)" />

        </div>

    </div>
</asp:Panel>

<script>
    function SelectCbx(obj) {
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].disabled != true && elements[i].id != 'ctl00_ContentPlaceHolder_Content_ctl00_ctl00_ctl00_gvCustomerView_ctl01_cbxSelectAll') {
                    elements[i].checked = true;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != 'ctl00_ContentPlaceHolder_Content_ctl00_ctl00_ctl00_gvCustomerView_ctl01_cbxSelectAll') {
                    elements[i].checked = false;
                }
            }
        }
    }

    function HighLightCBX(obj, obj1) {
        if (obj1.checked) {
            document.getElementById(obj).className = "hightlight";
        }
        else {
            document.getElementById(obj).className = "nohightlight";
        }
    }

    function checkColor(obj, obj1) {
        var obj2 = document.getElementById(obj);
        if (obj2.checked) {
            obj1.className = "hightlight";
        }
        else {
            obj1.className = "nohightlight";
        }
    }
</script>

<script type="text/javascript">
    initTabs('dhtmlgoodies_tabView1', Array('<%=Resources.labels.chitietkhachhang %>','<%=Resources.labels.hopdongcuakhachhang %>'), 0, 960, 330, Array(false, false, false, false));



    function validate() {
        if (validateEmpty('<%=txtCustID.ClientID %>','<%=Resources.labels.makhachhangkhongrong %>')) {
            if (validateMoney('<%=txtFullName.ClientID %>','<%=Resources.labels.bannhaptenkhachhang %>')) {
                if (validateEmpty('<%=txtMobi.ClientID %>','<%=Resources.labels.bannhapsodienthoai %>')) {
                    if (validateEmpty('<%=txtEmail.ClientID %>','<%=Resources.labels.bannhapemail %>')) {
                        if (validateEmpty('<%=txtIF.ClientID %>','<%=Resources.labels.bannhapsochungminh %>')) {

                            //kiem tra so
                            if (IsNumeric('<%=txtMobi.ClientID %>','<%=Resources.labels. sodienthoaikhongdungdinhdangso %>')) {
                                //kiem tra email
                                if (checkEmail('<%=txtEmail.ClientID %>','<%=Resources.labels. emailkhongdinhdang %>')) {

                                }
                                else {
                                    document.getElementById('<%=txtEmail.ClientID %>').focus();
                                    return false;
                                }
                            }
                            else {
                                document.getElementById('<%=txtMobi.ClientID %>').focus();
                                return false;
                            }
                        }
                        else {
                            document.getElementById('<%=txtIF.ClientID %>').focus();
                            return false;
                        }
                    }
                    else {
                        document.getElementById('<%=txtEmail.ClientID %>').focus();
                        return false;
                    }
                }
                else {
                    document.getElementById('<%=txtMobi.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtFullName.ClientID %>').focus();
                return false;
            }
        }
        else {
            document.getElementById('<%=txtCustID.ClientID %>').focus();
            return false;
        }

    }
</script>
