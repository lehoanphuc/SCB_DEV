<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SMSNOTIFYCATIONFEE_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <br />
        <div id="divCustHeader">
            <img alt="" src="widgets/SMSNOTIFYCATIONFEE/Images/smsicon.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
            <%=Resources.labels.chitietsmsnotifyfee %>
        </div>
        <div id="divError">
            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img alt="" src="widgets/SMSNOTIFYCATIONFEE/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />loading
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>

        <div id="divSearch">
            <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
                <table class="style1">
                    <tr>
                        <td class="thlb">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, marole %>"></asp:Label>
                        </td>
                        <td class="thtds">
                            <asp:TextBox ID="txRoleid" onkeypress="return isNumber(event);" runat="server"></asp:TextBox>
                        </td>
                        <td class="thlb">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, maphi %>"></asp:Label>
                        </td>
                        <td class="thtds">
                            <asp:TextBox ID="txfeeid" onkeypress="return isNumber(event);" runat="server"></asp:TextBox>
                        </td>
                        <td class="thbtn">
                            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                                OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, role %>"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txrolename" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, tenphi %>"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txfeename" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div id="divToolbar">
            &nbsp;<asp:Button ID="btnAdd_New" runat="server" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" />
            &nbsp;<asp:Button ID="btnDelete" Visible="false" runat="server" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click1" />
        </div>
        <div id="divResult">
            <asp:Literal ID="litError" runat="server"></asp:Literal>
            <asp:GridView ID="gvBranchList" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                OnRowDataBound="gvBranchList_RowDataBound" PageSize="15"
                OnPageIndexChanging="gvBranchList_PageIndexChanging"
                OnSorting="gvBranchList_Sorting" AllowSorting="True">
                <RowStyle ForeColor="#000000" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxSelect" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, marole %>">
                        <ItemTemplate>
                            <asp:HyperLink ID="lbroleid" runat="server"></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, maphi %>">
                        <ItemTemplate>
                            <asp:Label ID="lbfeeid" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, role %>">
                        <ItemTemplate>
                            <asp:Label ID="lbrolename" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, tenphi %>">
                        <ItemTemplate>
                            <asp:Label ID="lbfeename" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> " Visible="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpEdit" runat="server">[hpEdit]</asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>" Visible="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
<script>
    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
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
