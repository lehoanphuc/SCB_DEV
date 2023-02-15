<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCorporate_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<link href="CSS/smallBoostrap.css" rel="stylesheet" />

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="al">
            <img alt="" src="widgets/SEMSCorporate/Images/Bank.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
            <%=Resources.labels.corporateservicelist %>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" ForeColor="Red" Font-Bold="true" runat="server" />
        </div>
        <div class="block1" style="padding: 5px 5px 5px 5px;">

            <div class="handle">
                <asp:Label ID="Label6" Font-Bold="True" runat="server" Text='Corporate Search'></asp:Label>
            </div>
            <div class="content">
                <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
                    <table class="style1">
                        <tr>
                            <td class="thlb">
                                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, corpid %>"></asp:Label>
                            </td>
                            <td class="thtds">
                                <asp:TextBox ID="txtCorpID" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                            </td>
                            <td class="thlb">
                                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, corpname %>"></asp:Label>
                            </td>
                            <td class="thtds">
                                <asp:TextBox ID="txtCorpName" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td class="thbtn">
                                <asp:Button ID="btnSearch" CssClass="btn btn-submit" runat="server" Text="<%$ Resources:labels, timkiem %>"
                                    OnClick="btnSearch_Click" OnClientClick="Loading();" />
                            </td>
                        </tr>
                        <tr>
                            <td class="thlb">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, catalogname %>"></asp:Label>
                            </td>
                            <td class="thtds">
                                <asp:DropDownList ID="ddlCatalog" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </td>
                            <td class="thlb"></td>
                            <td class="thtds"></td>
                            <td class="thbtn"></td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
        <div class="btn-group">
            <asp:Button ID="btnAddNew" CssClass="btn btn-submit" runat="server" Text="<%$ Resources:labels, add %>"
                OnClick="btnAddNew_Click" OnClientClick="Loading();" />
            &nbsp;&nbsp;
            <asp:Button ID="btnDelete" CssClass="btn" runat="server" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click"
                OnClientClick="Loading();return Confirm('Are you sure want to delete these records?');" />
        </div>

        <div class="divResult">
            <asp:Literal runat="server" ID="ltrError"></asp:Literal>
            <asp:GridView ID="gridView" CssClass="tablecustom" runat="server" GridLines="None" CellPadding="10"
                Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                OnRowDataBound="gridView_RowDataBound" PageSize="15"
                OnPageIndexChanging="gridView_PageIndexChanging" AllowSorting="True"
                OnRowCommand="gridView_RowCommand" OnRowDeleting="gridView_RowDeleting">
                <AlternatingRowStyle BackColor="#F5F5F5" CssClass="odd" />
                <RowStyle BackColor="#FFFFFF" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxSelect" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, corpid%>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbCorpID" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("CORPID") %>' OnClientClick="Loading();"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, corpname%>">
                        <ItemTemplate>
                            <asp:Label ID="lblCorpName" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, catalogname%>">
                        <ItemTemplate>
                            <asp:Label ID="lblCatalogName" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, status %>'>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-warning" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CommandArgument='<%#Eval(IPC.CORPID) %>' OnClientClick="Loading();">Edit</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader center" />
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn btn-warning" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CommandArgument='<%#Eval(IPC.CORPID) %>' OnClientClick="Loading();return Confirm();">Delete</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader center" />
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="gvFooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                <SelectedRowStyle />
                <HeaderStyle CssClass="gvHeader" />
            </asp:GridView>
            <uc1:GridViewPaging runat="server" ID="GridViewPagingControl" />
            <asp:HiddenField ID="hdCounter" runat="server" />
            <asp:HiddenField ID="hdPageSize" runat="server" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script language="javascript">
    function SelectCbx(obj) {
        Counter = 0;
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);
        var count = document.getElementById('<%=gridView.ClientID %>').rows.length;
        var elements = document.getElementById('<%=gridView.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl14_gridView_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl14_gridView_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = false;
                    if (Counter > 0)
                        Counter--;
                }
            }
        }
        hdf.value = Counter.toString();
    }

    var TotalChkBx;
    var Counter;

    window.onload = function () {
        document.getElementById('<%=hdCounter.ClientID %>').value = '0';
    }

    function ChildClick(CheckBox) {
        Counter = parseInt(document.getElementById('<%=hdCounter.ClientID %>').value);
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);

        var grid = document.getElementById('<%= gridView.ClientID %>');
        var cbHeader = grid.rows[0].cells[0].childNodes[0];

        if (CheckBox.checked)
            Counter++;
        else if (Counter > 0)
            Counter--;

        if (Counter < TotalChkBx)
            cbHeader.checked = false;
        else if (Counter == TotalChkBx)
            cbHeader.checked = true;
        document.getElementById('<%=hdCounter.ClientID %>').value = Counter.toString();
    }

    function HighLightCBX(obj, obj1) {
        if (obj1.checked) {
            document.getElementById(obj).className = "hightlight";
        }
        else {
            document.getElementById(obj).className = "nohightlight";
        }
    }

    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }

    function Confirm() {
        return confirm('Are you sure you want to delete?');
    }
</script>
