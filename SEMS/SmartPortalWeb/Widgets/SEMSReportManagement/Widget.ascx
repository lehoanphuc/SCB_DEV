<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSReportManagement_Widget" %>
<script src="widgets/SEMSProduct/JS/common.js" type="text/javascript"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<br />

<div id="divCustHeader">
    <img alt="" src="widgets/SEMSReportManagement/Images/report.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.quanlybaocao %>
</div>
<div id="divError">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
        <ProgressTemplate>
            <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
            <%=Resources.labels.loading %>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>

<div id="divSearch">
    <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
        <table class="style1" cellpadding="3">
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, mabaocao %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtReportID" runat="server"></asp:TextBox>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, tenbaocao %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtReportName" runat="server"></asp:TextBox>
                </td>
                <td class="thbtn">
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, timkiem %>"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, phanhe %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubSystem" runat="server">
                        <asp:ListItem Value="" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
                        <asp:ListItem Value="SEMS">SEMS</asp:ListItem>
                        <asp:ListItem Value="IB" Text="<%$ Resources:labels, internetbanking %>"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div>
            <div id="divToolbar">
                &nbsp;
    <asp:Button ID="btnAddNew" runat="server" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" />
                &nbsp;
    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" />
            </div>
            <div id="divResult">
                <asp:Literal runat="server" ID="ltrError"></asp:Literal>
                <asp:GridView ID="gvProcessList" runat="server" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                    Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    OnRowDataBound="gvProcessList_RowDataBound" PageSize="15"
                    OnPageIndexChanging="gvProcessList_PageIndexChanging"
                    OnSorting="gvProcessList_Sorting" AllowSorting="True">
                    <RowStyle ForeColor="#000000" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbxSelect" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, mabaocao %>" Visible="true" SortExpression="RPTID">
                            <ItemTemplate>
                                <asp:HyperLink ID="hpRptID" runat="server" Visible="true"></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tenbaocao %>" SortExpression="RPTNAME">
                            <ItemTemplate>
                                <asp:HyperLink ID="hpRptName" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, phanhe %>" SortExpression="SERVICEID">
                            <ItemTemplate>
                                <asp:Label ID="lblSubSystem" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tenfile %>" SortExpression="RPTFILE">
                            <ItemTemplate>
                                <asp:Label ID="lblRptFile" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, edit %>">
                            <ItemTemplate>
                                <asp:HyperLink ID="hpEdit" runat="server">[hpEdit]</asp:HyperLink>
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
    </ContentTemplate>
</asp:UpdatePanel>
<script language="javascript">
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
</script>
