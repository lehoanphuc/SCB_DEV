<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSUserOTP_Widget" %>
<script src="widgets/SEMSProduct/JS/common.js" type="text/javascript"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSUserOTP/Images/otp.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.dangkysudungotp %>
</div>
<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
        <ProgressTemplate>
            <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
            <%=Resources.labels.loading %>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>

<div id="divSearch">
    <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
        <table class="style1">
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, tennguoidung %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:DropDownList ID="ddlStatus" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="thbtn">
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, timkiem %>"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, loaixacthuc %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:DropDownList ID="ddlAuthenType" runat="server" Width="57%">
                    </asp:DropDownList>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, maxacthuc %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtauthenCode" runat="server"></asp:TextBox>
                </td>
                <td class="thbtn"></td>
            </tr>
        </table>
    </asp:Panel>
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divToolbar">
            &nbsp;<asp:Button ID="btnAddNew" runat="server" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" />
            &nbsp;<asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" Visible="False" />
            &nbsp;<asp:Button ID="Button3" runat="server" Text="<%$ Resources:labels, exporttofile %>" OnClientClick="showPopWin('widgets/SEMSCustomerList/ExportToFile.aspx', 170, 160, null);setTitle(this);return false;" Visible="false" />
        </div>
        <div id="divResult">
            <asp:Literal ID="litError" runat="server"></asp:Literal>
            <asp:GridView ID="gvProductList" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                OnRowDataBound="gvProductList_RowDataBound" PageSize="15"
                OnPageIndexChanging="gvProductList_PageIndexChanging"
                OnSorting="gvProductList_Sorting" AllowSorting="True">
                <RowStyle ForeColor="#000000" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxSelect" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, tenkhachhang %>">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomer" runat="server">[hpCustomer]</asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, nguoidung %>">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpUser" runat="server">[hpUser]</asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, username %>">
                        <ItemTemplate>
                            <asp:Label ID="lblUsername" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, loaixacthuc %>">
                        <ItemTemplate>
                            <asp:Label ID="lblAuthenType" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, maxacthuc %>">
                        <ItemTemplate>
                            <asp:Label ID="lblAuthenCode" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %>" Visible="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpEdit" runat="server" Visible="false"></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpDelete" runat="server"></asp:HyperLink>
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
