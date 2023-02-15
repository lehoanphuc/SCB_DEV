<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSTellerApproveTrans_Widget" %>
<%@ Register TagPrefix="uc1" TagName="GridViewPaging" Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="subheader">
    <h1 class="subheader-title">
        <%=Resources.labels.thietlapquitrinhduyet %>
    </h1>
</div>
<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>
<div class="row">
    <div class="col-sm-12 col-xs-12">
        <div class="panel">
            <div class="panel-container">
                <div class="panel-content form-horizontal p-b-0">
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                        <div class="row">
                            <div class="col-sm-10 col-xs-12">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.maquitrinh %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtapptranID" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tensanpham %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlRole" CssClass="form-control select2" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.sotien %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtAmount" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tiente %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlCCYID" CssClass="form-control select2 infinity" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.giaodich %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddltrans" CssClass="form-control select2 infinity" runat="server">
                                                    <asp:ListItem Value="P" Text="<%$ Resources:labels, canhan %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12"></div>
                                </div>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, timkiem %>" OnClientClick="Loading();" OnClick="btnSearch_Click" />
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
    <asp:Button ID="btnExport" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, exporttofile %>" OnClick="btnExport_Click" OnClientClick="Loading();" />
</div>
<div id="divResult">
    <asp:Literal runat="server" ID="ltrError"></asp:Literal>
    <asp:GridView ID="gvProcessList" CssClass="table table-hover" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
        Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvProcessList_RowDataBound" PageSize="15" OnRowCommand="gvProcessList_RowCommand" OnRowDeleting="gvProcessList_RowDeleting">
        <RowStyle ForeColor="#000000" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbxSelect" runat="server" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, maquitrinh%>">
                <ItemTemplate>
                    <asp:LinkButton ID="lbProcessid" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("APPTRANID")%>' OnClientClick="Loading();"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, sanpham %>">
                <ItemTemplate>
                    <asp:Label ID="lblRole" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, loaigiaodich %>">
                <ItemTemplate>
                    <asp:Label ID="lblTrans" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, tiente %>">
                <ItemTemplate>
                    <asp:Label ID="lblCCYID" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, tu %>">
                <ItemTemplate>
                    <asp:Label ID="lblFrom" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, den %>">
                <ItemTemplate>
                    <asp:Label ID="lblTo" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                <ItemTemplate>
                    <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-primary" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CommandArgument='<%#Eval("APPTRANID")%>' OnClientClick="Loading();">Edit</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                <ItemTemplate>
                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CommandArgument='<%#Eval("APPTRANID")%>' OnClientClick="Loading(); return Confirm();">Delete</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
    <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
    <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
</div>
<script type="text/javascript">
    function SelectCbx(obj) {
        Counter = 0;
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);
        var count = document.getElementById('<%=gvProcessList.ClientID %>').rows.length;
        var elements = document.getElementById('<%=gvProcessList.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvProcessList_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvProcessList_ctl01_cbxSelectAll') {
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

        var grid = document.getElementById('<%= gvProcessList.ClientID %>');
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

    function executeComma(id) {
        var so = document.getElementById(id).value;
        so = so.toString().replace(/\$|\,/g, '');
        if (so != "") {
            if (isNaN(so)) {
                so = "";
            } else {
                so = Math.floor(so * 100 + 0.50000000001);
                var cents;
                cents = so % 100;
                so = Math.floor(so / 100).toString();
                if (cents < 10)
                    cents = "0" + cents;
                for (var i = 0; i < Math.floor((so.length - (1 + i)) / 3); i++)
                    so = so.substring(0, so.length - (4 * i + 3)) + ',' +
                        so.substring(so.length - (4 * i + 3));
            }
        }
        document.getElementById(id).value = so;
    }
</script>

