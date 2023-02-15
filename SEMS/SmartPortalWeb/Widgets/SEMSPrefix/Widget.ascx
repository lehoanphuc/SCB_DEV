<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSPrefix_Widget" %>
<script type="text/javascript" src="widgets/SEMSPrefix/js/mask.js"> </script>
<%@ Import Namespace="SmartPortal.Constant" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSCashcodemanager/Images/tax.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />

</div>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.PREFIXMANAGEMENT %>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
        </div>
        <div id="divSearch" class="" runat="server">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
                                    <div class="row">
                                        <div class=" col-sm-10 col-xs-12">
                                            <div class="col-sm-6 hidden">
                                                <div class="form-group">
                                                    <label class="col-sm-4  control-label "><%=Resources.labels.SupplierID %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlSupplierID" runat="server" CssClass="form-control select2 infinity">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.Prefix %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtPrefix" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.groupid %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtGroupID" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.CountryPrefix %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtCountryPrefix" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Button CssClass="btn btn-primary" ID="btnSearch" runat="server" Text="<%$ Resources:labels, search %>" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div id="divToolbar" runat="server" class="divToolbar">
            <asp:Button ID="btnAddNew" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" />
            <asp:Button ID="btnDelete" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" OnClientClick="Loading(); return ConfirmDelete2();" />
        </div>
        <div id="divResult" runat="server" class="divResult">
            <asp:Literal ID="litError" runat="server"></asp:Literal>
            <asp:GridView ID="gvPrefix" runat="server" BackColor="White" CssClass="table table-hover"
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" PageSize="15"
                Width="100%" AllowPaging="True" AutoGenerateColumns="False" OnRowDeleting="delete_Click" OnRowCommand="gvPrefix_RowCommand"
                OnRowDataBound="gvPrefix_RowDataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-CssClass="gvCenter">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxSelect" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" <%$ Resources:labels, TelcoName %>">
                        <ItemTemplate>
                            <asp:Label ID="lbltelconame" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, Prefix %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblprefix" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("PREFIX") %>' OnClientClick="Loading();">Edit</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, CountryPrefix %>">
                        <ItemTemplate>
                            <asp:Label ID="lblcountryprefix" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, groupid %>">
                        <ItemTemplate>
                            <asp:Label ID="lblgroupid" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, PhoneLength %>">
                        <ItemTemplate>
                            <asp:Label ID="lblphoenlen" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbEditpre" runat="server" CssClass="btn btn-primary" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CommandArgument='<%#Eval("PREFIX") %>' OnClientClick="Loading();">Edit</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, delete %> ">
                        <ItemTemplate>
                            <asp:LinkButton CssClass="btn btn-secondary" ID="hpDelete" runat="server" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' OnClientClick="Loading(); return Confirm();">Delete</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" CssClass="pager hidden" />
                <SelectedRowStyle />
            </asp:GridView>
            <uc1:gridviewpaging runat="server" id="GridViewPaging" />
            <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
            <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
        </div>
        <p class="auto-style1">
            <div style="text-align: center; margin-top: 10px;">
                <asp:Button ID="Button8" CssClass="btn btn-secondary btnGeneral" runat="server" Text="<%$ Resources:labels, back %>" OnClick="Button8_Click1" />
            </div>
        </p>
    </ContentTemplate>
</asp:UpdatePanel>

<script>
    function SelectCbx(obj) {
        Counter = 0;
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);
        var count = document.getElementById('<%=gvPrefix.ClientID %>').rows.length;
        var elements = document.getElementById('<%=gvPrefix.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvPrefix_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvPrefix_ctl01_cbxSelectAll') {
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

        var grid = document.getElementById('<%= gvPrefix.ClientID %>');
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

    function ConfirmDelete() {
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        if (hdf.value == 0) {
            alert('<%=Resources.labels.pleaseselectbeforedeleting %>');
            return false;
        } else {
            return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
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
