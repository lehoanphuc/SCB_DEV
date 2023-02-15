<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCustFXLimit_Widget" %>
<link href="widgets/SEMSCustFXLimit/css/style.css" rel="stylesheet" type="text/css">
<link href="widgets/SEMSCustFXLimit/css/subModal.css" rel="stylesheet" type="text/css">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSCustFXLimit/Images/limit.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.FxTransactionLimitList %>
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
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
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, contractno %>"></asp:Label>
                        </td>
                        <td class="thtds">
                            <asp:TextBox ID="txtContractNo" runat="server"></asp:TextBox>
                        </td>
                        <td class="thlb">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, tenkhachhang %>"></asp:Label>
                        </td>
                        <td class="thtds">
                            <asp:TextBox ID="txtFullName" runat="server"></asp:TextBox>
                        </td>
                        <td class="thbtn">
                            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                                OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="thlb">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, FromCCY %>"></asp:Label>
                        </td>
                        <td class="thtds">
                            <asp:DropDownList ID="ddlFromCCY" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="thlb">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, ToCCY %>"></asp:Label>
                        </td>
                        <td class="thtds">
                            <asp:DropDownList ID="ddlToCCY" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="thbtn"></td>
                    </tr>
                    <tr>
                        <td class="thlb">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, chophepngoaihoirabenngoai%>"></asp:Label>
                        </td>
                        <td class="thtds">
                            <asp:DropDownList ID="ddlFx" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="thlb"></td>
                        <td class="thtds"></td>
                        <td class="thbtn"></td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div>
            <div id="divToolbar">
                &nbsp;<asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:labels, add %>" OnClick="btnAddNew_Click" />
                &nbsp;<asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:labels, delete %>" Width="109px" OnClick="btnDelete_Click" OnClientClick="return confirmDelete('Are you sure delete this record(s)!');" />
            </div>
            <div id="divResult">
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:GridView ID="gvCustomerList" runat="server" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                    Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    OnRowDataBound="gvCustomerList_RowDataBound" PageSize="15"
                    OnPageIndexChanging="gvCustomerList_PageIndexChanging"
                    AllowSorting="True">
                    <RowStyle ForeColor="#000000" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="5px">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbxSelect" runat="server" Width="100%" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, contractno %>">
                            <ItemTemplate>
                                <asp:HyperLink ID="hpContract" runat="server">[hpContract]</asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false" HeaderText="<%$ Resources:labels, giaodich %>">
                            <ItemTemplate>
                                <asp:Label ID="TranName" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, FromCCY %>">
                            <ItemTemplate>
                                <asp:Label ID="FromCCY" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, ToCCY %>">
                            <ItemTemplate>
                                <asp:Label ID="ToCCY" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, hanmucmotgiaodich %>">
                            <ItemTemplate>
                                <asp:Label ID="LimitTran" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Number Transaction / Day">
                            <ItemTemplate>
                                <asp:Label ID="NumberTran" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tonghanmucngay %>">
                            <ItemTemplate>
                                <asp:Label ID="LimitTotal" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, chophepngoaihoirabenngoai %>" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="fx" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, edit %>">
                            <ItemTemplate>
                                <asp:LinkButton ID="EditFX" runat="server" OnClick="EditFX_Click"
                                    CommandArgument='<%# Eval("CONTRACTNO") + "#" + Eval("TRANDCODE")+"#"+Eval("FROMCCYID")+"#"+Eval("TOCCYID")%>' Text="<%$ Resources:labels, edit %>">
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                            <ItemTemplate>
                                <asp:LinkButton ID="DeleteFX" runat="server" OnClick="DeleteFX_Click" OnClientClick="return confirmDelete('Are you sure delete this Record!',this);"
                                    CommandArgument='<%# Eval("CONTRACTNO") + "#" + Eval("TRANDCODE")+"#"+Eval("FROMCCYID")+"#"+Eval("TOCCYID")%>' Text="<%$ Resources:labels, delete %>">
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
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
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != '') {
                    elements[i].checked = false;
                }
            }
        }
    }

    function HighLightCBX(obj) {
        if (obj.checked) {
            document.getElementById(obj).className = "hightlight";
        }
        else {
            document.getElementById(obj).className = "nohightlight";
        }
    }

    function confirmDelete(msg, item) {
        if (item != undefined) {
            if (item.href == "") return false;
            else return confirm(msg);;
        }
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        for (i = 0; i < count; i++) {
            if (elements[i].type == 'checkbox' && elements[i].disabled != true && elements[i].id != 'cbxSelectAll') {
                if (elements[i].checked == true) return confirm(msg);
            }
        }
        alert("You must choose record(s) to delete")
        return false;
    }

    function ShowError(msg) {
        msg = msg != undefined ? msg : "";
        var lblError = document.getElementById('<%=lblError.ClientID%>');
        lblError.innerText = msg;
    }
</script>

