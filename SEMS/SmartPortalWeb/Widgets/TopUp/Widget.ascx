<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_Admintopup_Widget" %>
<link href="widgets/SEMSCustomerList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>
<script src="JS/mask.js" type="text/javascript"></script>
<script src="widgets/SEMSProductLimit/JS/common.js" type="text/javascript"></script>

<script src="widgets/SEMSCustomerList/JS/tab-view.js" type="text/javascript"></script>
<link href="widgets/IBListTransWaitApprove/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/IBListTransWaitApprove/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBListTransWaitApprove/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/IBListTransWaitApprove/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBListTransWaitApprove/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBListTransWaitApprove/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSProduct/JS/common.js" type="text/javascript"></script>

<link href="widgets/SEMSCustomerList/css/style.css" rel="stylesheet" type="text/css">
<link href="widgets/SEMSCustomerList/css/subModal.css" rel="stylesheet" type="text/css">
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSContractLimit_HIS/Images/log.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.admintopup%>
</div>
<div id="divError">
    <asp:Label runat="server" ID="lblError"></asp:Label>
</div>

<div id="divSearch">
    <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
        <table class="style1">
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels,Telco %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:DropDownList ID="ddlTelco" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTelco_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, serialtopup %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtSerialNo" runat="server"></asp:TextBox>
                </td>
                <td class="thbtn">
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, timkiem %>"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, amount %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAmount" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, softpintopup %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSoftPin" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, availabletopup %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAvailable" runat="server">
                        <asp:ListItem>True</asp:ListItem>
                        <asp:ListItem>False</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
<div style="text-align: center; padding: 10px;">
    &nbsp;<asp:Button ID="btnImport0" runat="server" OnClick="btnImport_Click" Text="<%$ Resources:labels, import %>" />
    &nbsp;<asp:Button ID="Button1" runat="server" Text="<%$ Resources:labels, xoa %>" OnClick="btnDelete_Click" />
</div>
<div id="divResult">
    <asp:Literal ID="litError" runat="server"></asp:Literal>
    <asp:GridView ID="gvTopUp" runat="server" BackColor="White"
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
        Width="100%" AllowPaging="True" AutoGenerateColumns="False"
        OnRowDataBound="gvTopUp_RowDataBound" PageSize="15"
        OnPageIndexChanging="gvTopUp_PageIndexChanging">
        <RowStyle ForeColor="#000000" HorizontalAlign="Center" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbxSelect" runat="server" align="center" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, Telco %>">
                <ItemTemplate>
                    <asp:Label ID="lbltelco" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, serialtopup %>">
                <ItemTemplate>
                    <asp:Label ID="lblserial" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, softpintopup %>">
                <ItemTemplate>
                    <asp:Label ID="lblsoftpin" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, CreateDateContract %>">
                <ItemTemplate>
                    <asp:Label ID="lbldatecreate" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, ngayhethieuluc %>">
                <ItemTemplate>
                    <asp:Label ID="lblexpriedate" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, buyrate %>">
                <ItemTemplate>
                    <asp:Label ID="lblbuyrate" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, sellrate %>">
                <ItemTemplate>
                    <asp:Label ID="lblsellrate" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, menhgiathe %>">
                <ItemTemplate>
                    <asp:Label ID="lblcardprice" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, availabletopup %>">
                <ItemTemplate>
                    <asp:Label ID="lblavailable" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                <ItemTemplate>
                    <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
        </Columns>
        <FooterStyle CssClass="gvFooterStyle" />
        <PagerStyle HorizontalAlign="Center" CssClass="pager" />
        <SelectedRowStyle />
        <HeaderStyle CssClass="gvHeader" />
    </asp:GridView>
    <br />
    <asp:Literal ID="litPager" runat="server"></asp:Literal>
</div>



<script language="javascript">
    function SelectCbx(obj) {
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].disabled != true && elements[i].id != 'ctl00_ContentPlaceHolder_Content_ctl00_ctl00_ctl00_gvCustomerView_ctl01_cbxSelectAll') {
                    elements[i].checked = true;
                    //elements[i].parentNode.parentNode.className = "hightlight";
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != 'ctl00_ContentPlaceHolder_Content_ctl00_ctl00_ctl00_gvCustomerView_ctl01_cbxSelectAll') {
                    elements[i].checked = false;
                    //elements[i].parentNode.parentNode.className = "nohightlight";
                }
            }
        }
    }

    function HighLightCBX(obj, obj1) {
        //var obj2=document.getElementById(obj1);
        if (obj1.checked) {
            document.getElementById(obj).className = "hightlight";
        }
        else {
            document.getElementById(obj).className = "nohightlight";
        }
    }
</script>
