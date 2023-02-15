<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCustomerList_Widget" %>
<%@ Register Src="../../Controls/LetterSearch/LetterSearch.ascx" TagName="LetterSearch" TagPrefix="uc1" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSCustomerList/Images/messenger.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.danhsachkhachhang %>
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
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, makhachhang %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtCustCode" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, tenkhachhang %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtFullName" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
                <td class="thbtn">
                    <asp:Button ID="btnSearch" CssClass="btn btnGeneral" runat="server" Text="<%$ Resources:labels, xemchitiet %>" OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTel" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, cmndgpkd %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLicenseID" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblct" runat="server" Text="<%$ Resources:labels, loaikhachhang %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCustType" runat="server" CssClass="form-control select2 infinity">
                    </asp:DropDownList>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </asp:Panel>
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div>
            <div id="divLetter">
                <uc1:LetterSearch ID="LetterSearch" runat="server" />
            </div>
            <div id="divResult">
                <asp:GridView ID="gvCustomerList" runat="server" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                    Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    OnRowDataBound="gvCustomerList_RowDataBound" PageSize="15"
                    OnPageIndexChanging="gvCustomerList_PageIndexChanging"
                    OnSorting="gvCustomerList_Sorting" AllowSorting="True">
                    <RowStyle ForeColor="#000000" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbxSelect" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, makhachhang %>" SortExpression="CUSTID">
                            <ItemTemplate>
                                <asp:HyperLink ID="lblCustCode" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tenkhachhang %>" SortExpression="FULLNAME">
                            <ItemTemplate>
                                <asp:Label ID="lblCustName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, phone %>" SortExpression="TEL">
                            <ItemTemplate>
                                <asp:Label ID="lblPhone" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, cmndgpkd %>" SortExpression="LICENSEID">
                            <ItemTemplate>
                                <asp:Label ID="lblIdentify" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, mathamchieu %>" SortExpression="CUSTCODE">
                            <ItemTemplate>
                                <asp:Label ID="lblCustCodeCore" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, loaikhachhang %>" SortExpression="CFTYPE">
                            <ItemTemplate>
                                <asp:Label ID="lblCustType" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                            <ItemTemplate>
                                <asp:HyperLink ID="hpEdit" runat="server">[hpEdit]</asp:HyperLink>
                            </ItemTemplate>
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
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
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
</script>

