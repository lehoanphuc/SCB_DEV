<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSScheduleLog_Widget" %>
<link href="widgets/SEMSContractList/CSS/css.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSContractList/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/SEMSContractList/JS/lang/en.js" type="text/javascript"></script>
<link href="widgets/SEMSContractList/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractList/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractList/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSExchangeRate/Images/exhangerate.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.logdatlichchuyenkhoan %>
</div>
<div id="divError"></div>
<div id="divSearch">
    <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
        <table class="style1">
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, malich %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txScheduleID" runat="server"></asp:TextBox>
                </td>

                <td class="thlb">
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, nguoitao %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txUserCreate" runat="server"></asp:TextBox>
                </td>
                <td class="thbtn">
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, search %>"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, giaodich %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:DropDownList ID="ddlCKType" runat="server" AutoPostBack="true"
                        Width="100px">
                        <asp:ListItem Text="<%$ Resources:labels, tatca %>" Value=""></asp:ListItem>
                        <asp:ListItem Value="IB000201">CK cùng chủ sở hửu</asp:ListItem>
                        <asp:ListItem Value="IB000208">CK trong hệ thống</asp:ListItem>
                        <asp:ListItem Value="IB000206">CK ngoài hệ thống</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtUserApproved" runat="server" Visible="False" Width="1px"></asp:TextBox>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, loailich %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:DropDownList ID="ddlScheduleType" runat="server" AutoPostBack="true" Width="100px">
                        <asp:ListItem Value="" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
                        <asp:ListItem Value="ONETIME" Text="<%$ Resources:labels, motlan %>"></asp:ListItem>
                        <asp:ListItem Value="DAILY" Text="<%$ Resources:labels, hangngay %>"></asp:ListItem>
                        <asp:ListItem Value="WEEKLY" Text="<%$ Resources:labels, hangtuan %>"></asp:ListItem>
                        <asp:ListItem Value="MONTHLY" Text="<%$ Resources:labels, hangthang %>"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>

                <td class="thlb">
                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, tungay %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, denngay %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <script type="text/javascript">    //<![CDATA[

            var cal = Calendar.setup({
                onSelect: function (cal) { cal.hide() }
            });
            cal.manageFields("<%=txtFromDate.ClientID %>", "<%=txtFromDate.ClientID %>", "%d/%m/%Y");
            cal.manageFields("<%=txtToDate.ClientID %>", "<%=txtToDate.ClientID %>", "%d/%m/%Y");
    //]]></script>
    </asp:Panel>
</div>
<div id="divResult" style="height: 500px; overflow: auto;">
    <asp:Literal ID="litError" runat="server"></asp:Literal>
    <asp:GridView ID="gvSchedulelog" runat="server" BackColor="White"
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
        Width="2000" AutoGenerateColumns="False"
        OnRowDataBound="gvSchedulelog_RowDataBound" PageSize="15"
        AllowSorting="True">
        <RowStyle ForeColor="#000000" />
        <Columns>
            <asp:TemplateField HeaderText="<%$ Resources:labels, malich %>">
                <ItemTemplate>
                    <asp:Label ID="lblScheduleID" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, loailich %>">
                <ItemTemplate>
                    <asp:Label ID="lblScheduleType" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, tenlich %>">
                <ItemTemplate>
                    <asp:Label ID="lblScheduleName" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, diengiai %>" SortExpression="Matiente">
                <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, nguoitao %>">
                <ItemTemplate>
                    <asp:Label ID="lblUserCreate" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, ngaytao %>">
                <ItemTemplate>
                    <asp:Label ID="lblCreateDate" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, ngayhethan %>">
                <ItemTemplate>
                    <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="<%$ Resources:labels, ngaychay %>">
                <ItemTemplate>
                    <asp:Label ID="lblIPCTransdate" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, ngaynganhang %>">
                <ItemTemplate>
                    <asp:Label ID="lblIPCTransWorkDate" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, magiaodich %>">
                <ItemTemplate>
                    <asp:HyperLink ID="lblIPCTransID" runat="server"></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, tkgui %>">
                <ItemTemplate>
                    <asp:Label ID="lblAcctNo" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, nguoigui %>">
                <ItemTemplate>
                    <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, tknhan %>">
                <ItemTemplate>
                    <asp:Label ID="lblReceverAccount" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, tennguoinhan %>">
                <ItemTemplate>
                    <asp:Label ID="lblReceiverName" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, sotien %>">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, loaick %>">
                <ItemTemplate>
                    <asp:Label ID="lblCKType" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>

        </Columns>
        <FooterStyle CssClass="gvFooterStyle" />
        <PagerStyle HorizontalAlign="Center" CssClass="pager" />
        <SelectedRowStyle />
        <HeaderStyle CssClass="gvHeader" />
    </asp:GridView>
</div>


<script>

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
