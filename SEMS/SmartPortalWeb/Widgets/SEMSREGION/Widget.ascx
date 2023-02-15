<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSREGION_Widget" %>
<%@ Register src="~/Controls/GirdViewPaging/GridViewPaging.ascx" tagName="GridViewPaging" tagPrefix="control" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%= Resources.labels.danhsachRegion %>
            </h1>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;"/>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"/>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                                <div class="row">
                                    <div class="col-sm-10 col-xs-12">
                                        <div class="row">
                                            <div class="form-group col-sm-6 col-xs-12">
                                                <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.tenvungphi %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txregionname" CssClass="form-control" runat="server" MaxLength="200"/>
                                                </div>
                                            </div>
                                            <div class="form-group col-sm-6 col-xs-12">
                                                <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.mota %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-sm-6 col-xs-12">
                                                <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.RegionSpecial %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:DropDownList runat="server" ID="ddlRegionSpecial" AutoPostBack="True" Width="100%" CssClass="form-control select2" OnSelectedIndexChanged="ddlRegionType_OnSelectedIndexChanged"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, search %>" OnClick="btnSearch_Click"/>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divToolbar">
            <asp:Button ID="btnAdd_New" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" OnClientClick="Loading();"/>
            <%--<asp:Button ID="btnClear" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_Click"/>--%>
            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" OnClientClick="Loading(); return ConfirmDelete2()"/>
        </div>
        <div id="divResult" runat="server">
            <asp:GridView ID="gvRegionList" CssClass="table table-hover" runat="server"
                          BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                          PageSize="15"
                          Width="100%" AutoGenerateColumns="False"
                          OnRowDataBound="gvRegionList_RowDataBound"
                          OnRowCommand="gvRegionList_RowCommand"
                          OnRowDeleting="gvRegionList_RowDeleting">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxSelect" runat="server"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, tenvungphi %>">
                        <ItemTemplate>
                            <asp:Label ID="lblregionid" runat="server" Visible="False" />
                            <asp:LinkButton ID="lbregionname" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("regionid") %>' OnClientClick="Loading();"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- add --%>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, mota %>">
                        <ItemTemplate>
                            <asp:Label ID="lbdescription" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, RegionSpecial %>">
                        <ItemTemplate>
                            <asp:Label ID="lblRegionSpecial" runat="server"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-primary" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CommandArgument='<%#Eval("regionid") %>' OnClientClick="Loading();">Edit</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CommandArgument='<%#Eval("regionid") %>' OnClientClick="Loading(); return Confirm();">Delete</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"/>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" CssClass="pager"/>
            </asp:GridView>
            <asp:Literal ID="litError" runat="server"/>
            <control:GridViewPaging ID="GridViewPaging" runat="server"/>
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
        var count = document.getElementById('<%= gvRegionList.ClientID %>').rows.length;
        var elements = document.getElementById('<%= gvRegionList.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvRegionList_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvRegionList_ctl01_cbxSelectAll') {
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

        var grid = document.getElementById('<%= gvRegionList.ClientID %>');
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
            return confirm('<%= Resources.labels.banchacchanmuonxoa %>');
        }

</script>