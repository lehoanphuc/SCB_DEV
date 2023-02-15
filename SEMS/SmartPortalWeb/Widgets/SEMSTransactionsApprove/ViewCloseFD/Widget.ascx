<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSTransactionsApprove_ViewCloseFD_Widget" %>
<%@ Register Src="../../../Controls/LetterSearch/LetterSearch.ascx" TagName="LetterSearch"
    TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<style type="text/css">
    .style1
    {
        width: 100%;
    }
    #divSearch
    {
        background-color: #F8F8F8;
        border: solid 1px #B9BFC1;
        margin: 5px 5px 5px 5px;
        padding: 5px 5px 5px 5px;
    }
    #divToolbar
    {
        background-color: #F8F8F8;
        border: solid 1px #B9BFC1;
        margin: 20px 5px 5px 5px;
        padding: 5px 5px 5px 5px;
    }
    #divLetter
    {
        background-color: #F8F8F8;
        border: solid 1px #B9BFC1;
        margin: 20px 5px 5px 5px;
        padding: 5px 5px 5px 5px;
    }
    #divResult
    {
        margin: 20px 5px 5px 5px;
        padding: 5px 5px 5px 5px;
    }
    .gvHeader
    {
        text-align: left;
    }
    #divCustHeader
    {
        width: 100%;
        font-weight: bold;
        padding: 0px 5px 0px 5px;
    }
    #divError
    {
        width: 100%;
        font-weight: bold;
        height: 10px;
        text-align: center;
        padding: 5px 5px 5px 5px;
    }
    .hightlight
    {
        background-color: #EAFAFF;
        color: #003366;
    }
    .nohightlight
    {
        background-color: White;
    }
</style>
<link href="widgets/SEMSContractApprove/CSS/css.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSContractApprove/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/SEMSContractApprove/JS/lang/en.js" type="text/javascript"></script>
<link href="widgets/SEMSContractApprove/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractApprove/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractApprove/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%>
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSTransactionsApprove/Images/icon_transactions.jpg" style="width: 40px;
        height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.duyetgiaodichtattoantietkiemonline %>
</div>
<div id="divError" style="text-align: center;">
    <%--<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" runat="server">
<ProgressTemplate>
<img alt="" src="widgets/SEMScontractList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
</ProgressTemplate>
</asp:UpdateProgress>--%>
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</div>
<div id="divSearch">
    <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, sogiaodich %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTranID" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, taikhoantietkiemcokyhan %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, timkiem %>"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, tenkhachhang %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCustName" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, mahopdong %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtContractNo" runat="server"></asp:TextBox>
                </td>
                <td width="10%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, tungay %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, denngay %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, trangthai %>" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" Visible="False">
                        <asp:ListItem Text="<%$ Resources:labels, tatca %>" Value="ALL"></asp:ListItem>
                        <asp:ListItem Value="0">Đang xử lý</asp:ListItem>
                        <asp:ListItem Value="1">Hoàn thành</asp:ListItem>
                        <asp:ListItem Value="2">Lỗi</asp:ListItem>
                        <asp:ListItem Value="3">Chờ duyệt</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Kết quả" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlResult" runat="server" Visible="False">
                        <asp:ListItem Text="<%$ Resources:labels, tatca %>" Value="ALL"></asp:ListItem>
                        <asp:ListItem Value="0">Processing</asp:ListItem>
                        <asp:ListItem Value="2">Pendding to approve</asp:ListItem>
                        <asp:ListItem Value="3">Aprroved</asp:ListItem>
                        <asp:ListItem Value="4">Deleted</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
<div>
    <asp:Panel ID="pnbutton" runat="server">
        <div id="divToolbar" style="text-align: left;">
            <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" Text="<%$ Resources:labels, duyet %>"
                Width="100px" />
            &nbsp;
            <asp:Button ID="btnReject" runat="server" OnClick="btnReject_Click" Text="<%$ Resources:labels, khongduyet %>"
                Width="100px" />
        </div>
    </asp:Panel>
    <%--   <asp:Button ID="Button3" runat="server" Text="<%$ Resources:labels, exporttofile %>" />--%>
    <%--<div id="divLetter">
    <uc1:LetterSearch ID="LetterSearch1" runat="server" />
</div>--%>
    <div id="divResult">
        <asp:Literal runat="server" ID="ltrError"></asp:Literal>
        <asp:GridView ID="gvcontractList" runat="server" BackColor="White" BorderColor="#CCCCCC"
            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%" AllowPaging="True"
            AutoGenerateColumns="False" OnRowDataBound="gvcontractList_RowDataBound" PageSize="15"
            OnPageIndexChanging="gvcontractList_PageIndexChanging" OnSorting="gvcontractList_Sorting"
            AllowSorting="True">
            <RowStyle ForeColor="#000000" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbxSelect" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, ngaygiogiaodich %>">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, sogiaodich %>">
                    <ItemTemplate>
                        <asp:HyperLink ID="hpTranID" runat="server">[hpDetails]</asp:HyperLink>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, tenkhachhang %>">
                    <ItemTemplate>
                        <asp:Label ID="lblCustName" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, taikhoantietkiemcokyhan %>">
                    <ItemTemplate>
                        <asp:Label ID="lblAccount" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, sotien %>">
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblCCYID" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, mota %>">
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Kết quả" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblResult" runat="server"></asp:Label>
                    </ItemTemplate>
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
    <div style="text-align: center;">
        <asp:Button ID="Button1" runat="server" CssClass="btnGeneral" Text="<%$ Resources:labels, quaylai %>"
            OnClick="Button1_Click" />
        <%--PostBackUrl="javascript:history.go(-1)"--%>
    </div>
</div>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>
<script type="text/javascript">//<![CDATA[

    var cal = Calendar.setup({
        onSelect: function (cal) { cal.hide() }
    });
    cal.manageFields("<%=txtFrom.ClientID %>", "<%=txtFrom.ClientID %>", "%d/%m/%Y");
    cal.manageFields("<%=txtTo.ClientID %>", "<%=txtTo.ClientID %>", "%d/%m/%Y");      
    //]]></script>
<script>
    function SelectCbx(obj) {
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != '') {
                    elements[i].checked = true;
                    //elements[i].parentNode.parentNode.className="hightlight";
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != '') {
                    elements[i].checked = false;
                    //elements[i].parentNode.parentNode.className="nohightlight";
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

    function pop(obj) {
        if (window.confirm(obj)) {
            return true;
        }
        else {
            return false;
        }
    }
</script>
