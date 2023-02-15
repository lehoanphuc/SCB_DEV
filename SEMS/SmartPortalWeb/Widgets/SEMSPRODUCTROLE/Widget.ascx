<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSPRODUCTROLE_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.danhsachquyensanpham %>
            </h1>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.masanpham %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtmasp" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.tensanpham %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txttensp" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, timkiem %>" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.contractlevel %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlContractLevel" CssClass="form-control select2" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divToolbar">
            <asp:Button ID="btnAddNew" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" OnClientClick="Loading();" />
            <asp:Button ID="btnDelete" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" OnClientClick="Loading(); return ConfirmDelete2();" />
        </div>
        <div id="divResult">
            <asp:Literal ID="litError" runat="server"></asp:Literal>
            <asp:GridView ID="gvProductList" CssClass="table table-hover" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                OnRowDataBound="gvProductList_RowDataBound" PageSize="15" OnRowCommand="gvProductList_RowCommand" OnRowDeleting="gvProductList_RowDeleting"
                OnPageIndexChanging="gvProductList_PageIndexChanging">
                <RowStyle ForeColor="#000000" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxSelect" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, masanpham%>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblProductCode" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("ProductID")+"|"+ Eval("CONTRACT_LEVEL_ID")%>' OnClientClick="Loading();"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, tensanpham %>">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, loaikhachhang %>">
                        <ItemTemplate>
                            <asp:Label ID="lblCustType" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, loaihinhsanpham %>">
                        <ItemTemplate>
                            <asp:Label ID="lblProductType" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, contractlevel %>">
                        <ItemTemplate>
                            <asp:Label ID="lblContractLevelName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-primary" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CommandArgument='<%#Eval("ProductID")+"|"+ Eval("CONTRACT_LEVEL_ID")%>' OnClientClick="Loading();">Edit</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CommandArgument='<%#Eval("ProductID")+"|"+ Eval("CONTRACT_LEVEL_ID")%>' OnClientClick="Loading(); return Confirm();">Delete</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                <SelectedRowStyle />
            </asp:GridView>
            <asp:Literal ID="litPager" runat="server"></asp:Literal>
            <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
            <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script language="javascript">
    function SelectCbx(obj) {
        Counter = 0;
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);
        var count = document.getElementById('<%=gvProductList.ClientID %>').rows.length;
        var elements = document.getElementById('<%=gvProductList.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvProductList_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvProductList_ctl01_cbxSelectAll') {
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

        var grid = document.getElementById('<%= gvProductList.ClientID %>');
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

    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }

    function ConfirmDelete2() {
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        if (hdf.value == 0) {
            alert('<%=Resources.labels.pleaseselectbeforedeleting %>');
            return false;
        } else {
            return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
        }
    }

    function Confirm() {
        return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
    }
</script>
