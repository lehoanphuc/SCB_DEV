<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSService_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<link href="CSS/smallBoostrap.css" rel="stylesheet" />

<asp:ScriptManager ID="Script1" runat="server"></asp:ScriptManager>
<div class="al">
    <%=Resources.labels.danhsachservice %><br />
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label ID="lblError" ForeColor="Red" Font-Bold="true" runat="server" />
        </div>
        <div class="block1">
            <div class="content">
                <asp:Panel runat="server" ID="pnSearch" DefaultButton="btnSeach">
                    <table class="style1">
                        <tr>
                            <td class="thlb">
                                <asp:Label runat="server" Text="<%$ Resources:labels, serviceid %>"></asp:Label>
                            </td>
                            <td class="thtds">
                                <asp:TextBox ID="txtServiceID" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                            </td>
                            <td class="thlb">
                                <asp:Label runat="server" Text="<%$ Resources:labels, shortname %>"></asp:Label>
                            </td>
                            <td class="thtds">
                                <asp:TextBox ID="txtServiceCode" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td class="thbtn">
                                <asp:Button ID="btnSeach" runat="server" Text="<%$ Resources:labels, timkiem %>" OnClick="btnSeach_Click" CssClass="btn btn-submit" />
                            </td>
                        </tr>
                        <tr>
                            <td class="thlb">
                                <asp:Label runat="server" Text="<%$ Resources:labels, Servicename %>"></asp:Label>
                            </td>
                            <td class="thtds">
                                <asp:TextBox ID="txtServiceName" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td class="thlb">
                                <asp:Label runat="server" Text="<%$ Resources:labels, corpname %>"></asp:Label>
                            </td>
                            <td class="thtds">
                                <asp:DropDownList ID="ddlCorp" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </td>
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
            <asp:GridView ID="gvService" CssClass="tablecustom" runat="server" GridLines="None" CellPadding="10" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvService_RowDataBound" PageSize="15" AllowSorting="True" OnRowCommand="gvService_RowCommand" OnRowDeleting="gvServiceList_RowDeleting">
                <AlternatingRowStyle BackColor="#F5F5F5" CssClass="odd" />
                <RowStyle BackColor="#FFFFFF" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxSelect" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, serviceid %>'>
                        <ItemTemplate>
                            <asp:LinkButton ID="lblServiceID" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("ServiceID") %>' OnClientClick="Loading();">[lblServiceID]</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, shortname %>'>
                        <ItemTemplate>
                            <asp:Label ID="lbServiceCode" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, Servicename %>'>
                        <ItemTemplate>
                            <asp:Label ID="lblServiceName" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, corpname %>'>
                        <ItemTemplate>
                            <asp:Label ID="lblCorpName" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, catalogname %>'>
                        <ItemTemplate>
                            <asp:Label ID="lblCATNAME" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, status %>'>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, desc %>' Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbEdit" runat="server" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CommandArgument='<%#Eval("ServiceID") %>' CssClass="btn btn-warning" OnClientClick="Loading();">Edit</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CommandArgument='<%#Eval("ServiceID") %>' CssClass="btn btn-warning" OnClientClick="Loading();return Confirm();">Delete</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="gvFooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                <SelectedRowStyle />
                <HeaderStyle CssClass="gvHeader" />
            </asp:GridView>
        </div>
        <uc1:GridViewPaging runat="server" ID="GridViewPagingControl" />
        <asp:Literal ID="litPager" runat="server"></asp:Literal>
        <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
        <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<script>
    function SelectCbx(obj) {
        Counter = 0;
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);
        var count = document.getElementById('<%=gvService.ClientID %>').rows.length;
        var elements = document.getElementById('<%=gvService.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl15_gvService_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl15_gvService_ctl01_cbxSelectAll') {
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

        var grid = document.getElementById('<%= gvService.ClientID %>');
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
        //var obj2=document.getElementById(obj1);
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

