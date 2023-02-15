<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSViewScheduleNotifiSMS_Widget" %>

<link href="widgets/IBTransactionHistory1/CSS/css.css" rel="stylesheet" type="text/css" />
<script src="widgets/IBTransactionHistory1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/lang/en.js" type="text/javascript"></script>
<link href="widgets/IBTransactionHistory1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSReportManagement/Images/report.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.datlichthongbaoquasms%>
</div>
<div id="divError"></div>
<div>
    <div id="divSearch">
        <table class="style1">
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label5" runat="server" Text='<%$ Resources:labels, tenlich %>'></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtScheduleName" runat="server"></asp:TextBox>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label7" runat="server" Text='<%$ Resources:labels, loaigiaodich %>'></asp:Label>
                </td>
                <td class="thtds">
                    <asp:DropDownList ID="ddlTransactionType" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="thbtn">
                    <asp:Button ID="btnSearch" runat="server" Text='<%$ Resources:labels, timkiem %>' OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    </div>


    <div id="divToolbar">
        &nbsp;<asp:Button ID="Button1" runat="server" Text='<%$ Resources:labels, datlich %>' OnClick="Button1_Click1" />
        &nbsp;<asp:Button ID="btnDelete" runat="server" Text='<%$ Resources:labels, huy %>' OnClick="Button2_Click" />
    </div>
    <!--thong tin tai khoan DD-->
    <asp:Panel ID="pnDD" runat="server">
        <div id="divResult">
            <asp:Literal runat="server" ID="lblAlert"></asp:Literal>
            <asp:GridView ID="gvSTV" runat="server" AutoGenerateColumns="False"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" Width="100%"
                OnRowDataBound="gvSTV_RowDataBound" AllowPaging="True"
                OnPageIndexChanging="gvSTV_PageIndexChanging">
                <RowStyle ForeColor="#000066" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbSelect" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, malich %>" Visible="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpScheCode" runat="server" Visible="false">[hpScheCode]</asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, tenlich %>'>
                        <ItemTemplate>
                            <asp:HyperLink ID="lblScheName" runat="server"></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, kieulich %>'>
                        <ItemTemplate>
                            <asp:Label ID="lblScheType" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, ngaythuchien %>'>
                        <ItemTemplate>
                            <asp:Label ID="lbldateExcute" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, huy %>'>
                        <ItemTemplate>
                            <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center" />
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
    </asp:Panel>
    <!--end-->

</div>
<script>
    function SelectCbx(obj) {
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != 'ctl00_ctl14_gvSTV_ctl01_cbxSelectAll') {
                    elements[i].checked = true;
                    //elements[i].parentNode.parentNode.className="hightlight";
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != 'ctl00_ctl14_gvSTV_ctl01_cbxSelectAll') {
                    elements[i].checked = false;
                    //elements[i].parentNode.parentNode.className="nohightlight";
                }
            }
        }
    }
</script>
