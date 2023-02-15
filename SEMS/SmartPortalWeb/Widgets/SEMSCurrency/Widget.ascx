<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCurrency_Widget" %>
<%@ Register TagPrefix="control" TagName="GridViewPaging" Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" %>
<%@Import namespace="SmartPortal.Constant" %>

<asp:ScriptManager runat="server" ID="ScriptManager1">
</asp:ScriptManager>

<asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%= Resources.labels.currencylist %>
            </h1>
        </div>
        <div id="divError">
            <asp:Label runat="server" ID="lblError"/>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
                                <div class="row">
                                    <div class="col-sm-10 col-xs-12">
                                        <div class="row">
                                            <div class="form-group col-sm-6 col-xs-12">
                                                <label class="col-sm-4 control-label col-xs-12"><%= Resources.labels.currencyid %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtCCYID" CssClass="form-control" runat="server" MaxLength="10"/>
                                                </div>
                                            </div>
                                            <div class="form-group col-sm-6 col-xs-12">
                                                <label class="col-sm-4 control-label col-xs-12"><%= Resources.labels.currencyname %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtCurrencyName" CssClass="form-control" runat="server"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-sm-6 col-xs-12">
                                                <label class="col-sm-4 control-label col-xs-12"><%= Resources.labels.currencynumber %></label>
                                                <div class=" col-sm-8 col-xs-12">
                                                    <asp:TextBox runat="server" ID="txtCurrencyNumber" CssClass="form-control" MaxLength="5" onkeypress="return isNumberKey(event);"/>
                                                </div>
                                            </div>
                                            <div class="form-group col-sm-6 col-xs-12">
                                                <label class="col-sm-4 control-label col-xs-12"><%= Resources.labels.currencymastername %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox runat="server" ID="txtCurrencyMasterName" CssClass="form-control" MaxLength="250"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, search %>" OnClick="btnSearch_OnClick"/>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divToolbar">
            <asp:Button runat="server" ID="btnAddNew" Text="<%$ Resources:labels, themmoi %>" CssClass="btn btn-primary" OnClientClick="Loading()" OnClick="btnAddNew_OnClick"/>
            <asp:Button runat="server" ID="btnClear" Text="<%$Resources:labels, Clear%>" CssClass="btn btn-secondary" OnClick="btnClear_OnClick"/>
            <asp:Button runat="server" ID="btnDelete" Text="<%$ Resources:labels, xoa%>" CssClass="btn btn-secondary" OnClientClick="Loading();return ConfirmDelete2();" OnClick="btnDelete_Click"/>
        </div>

        <div id="divResult" runat="server">
            <asp:Literal runat="server" ID="litError"/>
            <asp:GridView ID="gvCurrencyList" CssClass="table table-hover" runat="server" BackColor="White"
                          BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                          Width="100%"
                          AutoGenerateColumns="False"
                          OnRowDataBound="gvCurrencyList_OnRowDataBound"
                          PageSize="15"
                          OnRowCommand="gvCurrencyList_OnRowCommand"
                          OnRowDeleting="gvCurrencyList_OnRowDeleting">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxSelect" runat="server"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, currencyid %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbCCYID" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("CCYID") %>' OnClientClick="Loading();"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, scurrencyid %>">
                        <ItemTemplate>
                            <asp:Label ID="lblSCurrencyId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, currencynumber %>">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCurrencyNumber"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, currencyname %>">
                        <ItemTemplate>
                            <asp:Label ID="lblCurrencyName" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, currencymastername%>">
                        <ItemTemplate>
                            <asp:Label ID="lblMasterName" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-primary" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CommandArgument='<%#Eval("CCYID") %>' OnClientClick="Loading();">Edit</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CommandArgument='<%#Eval("CCYID") %>' OnClientClick="Loading(); return Confirm();">Delete</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"/>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" CssClass="pager"/>
            </asp:GridView>
            <control:GridViewPaging runat="server" ID="GridViewPaging"/>
            <asp:HiddenField ID="hdCounter" Value="0" runat="server"/>
            <asp:HiddenField ID="hdPageSize" Value="15" runat="server"/>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script>
    function SelectCbx(obj) {
        Counter = 0;
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        TotalChkBx = parseInt(document.getElementById('<%= hdPageSize.ClientID %>').value);
        var count = document.getElementById('<%= gvCurrencyList.ClientID %>').rows.length;
        var elements = document.getElementById('<%= gvCurrencyList.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvCurrencyList_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvCurrencyList_ctl01_cbxSelectAll') {
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
        document.getElementById('<%= hdCounter.ClientID %>').value = '0';
    }

    function ChildClick(CheckBox) {
        Counter = parseInt(document.getElementById('<%= hdCounter.ClientID %>').value);
        TotalChkBx = parseInt(document.getElementById('<%= hdPageSize.ClientID %>').value);

        var grid = document.getElementById('<%= gvCurrencyList.ClientID %>');
        var cbHeader = grid.rows[0].cells[0].childNodes[0];

        if (CheckBox.checked)
            Counter++;
        else if (Counter > 0)
            Counter--;

        if (Counter < TotalChkBx)
            cbHeader.checked = false;
        else if (Counter == TotalChkBx)
            cbHeader.checked = true;
        document.getElementById('<%= hdCounter.ClientID %>').value = Counter.toString();
    }

        function Loading() {
            if (document.getElementById('<%= lblError.ClientID %>').innerHTML != '') {
                document.getElementById('<%= lblError.ClientID %>').innerHTML = '';
            }
        }

    function ConfirmDelete2() {
            var hdf = document.getElementById("<%= hdCounter.ClientID %>");
            if (hdf.value < 1) {
                alert('<%= Resources.labels.pleaseselectbeforedeleting %>');
                   return false;
               } else {
                   return confirm('<%= Resources.labels.banchacchanmuonxoa %>');
            }
        }
    function Confirm() {
        return confirm('Are you sure you want to delete?')
    }
</script>