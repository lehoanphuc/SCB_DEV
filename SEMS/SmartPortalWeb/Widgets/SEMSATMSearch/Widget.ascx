<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSATMSearch_Widget" %>
<%@ Register src="~/Controls/GirdViewPaging/GridViewPaging.ascx" tagName="GridViewPaging" tagPrefix="control"%>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:Panel ID="UpdatePanel1" runat="server">
    <div class="subheader">
        <h1 class="subheader-title">
            <%= Resources.labels.danhsachatm %>
        </h1>
    </div>
    <div class="loading">
        <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" runat="server">
            <ProgressTemplate>
                <img src="Images/tenor.gif" style="width: 32px; height: 32px;"/>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div id="divError">
        <asp:Label ID="lblError" runat="server"/>
    </div>
    <div id="divSearch">
        <div class="panel-container">
            <div class="panel-content form-horizontal p-b-0">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btAdvanceSearch">
                    <div class="panel-container">

                        <div class="panel-content form-horizontal p-b-0" style="display: block;">
                            <div class="row">
                                <div class="col-sm-10 col-xs-12">
                                    <div class="row">
                                        <div class="form-group col-sm-6 col-xs-12">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.atmid %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtATMid" CssClass="form-control" runat="server" MaxLength="50" onkeypress="return isNumberKey(event)"/>
                                            </div>
                                        </div>
                                        <div class="form-group col-sm-6 col-xs-12">
                                            <label class="col-sm-4  col-xs-12 control-label"><%= Resources.labels.address %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtATMAdd" CssClass="form-control" runat="server"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-6 col-xs-12">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.country %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList Width="100%" ID="ddlCountry" CssClass="form-control select2" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group col-sm-6 col-xs-12">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.tinhthanhpho %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList Width="100%" ID="ddlCity" CssClass="form-control select2" runat="server" >
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-6 col-xs-12">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.quanhuyen %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList Width="100%" ID="ddlDistrict" CssClass="form-control select2" runat="server" >
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group col-sm-6 col-xs-12">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.chinhanh %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList Width="100%" ID="ddlBranch" CssClass="form-control select2" runat="server" >
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <asp:Button ID="btAdvanceSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, search %>" OnClick="btnSearch_Click"/>
                                </div>
                            </div>
                        </div>

                    </div>

                </asp:Panel>
            </div>
        </div>
    </div>
    <div id="divToolbar">
        <asp:Button ID="btnAddNew" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" OnClientClick="Loading();"/>
        <asp:Button ID="btnDelete" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" OnClientClick="Loading(); return ConfirmDelete2();"/>
        <asp:Button ID="Button3" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, exporttofile %>" OnClick="bt_export_Click" OnClientClick="Loading();"/>
    </div>
    <div id="divResult" runat="server">
        <asp:Literal ID="litError" runat="server"/>
        <asp:GridView ID="gvATMList" runat="server" BackColor="White" CssClass="table table-hover" PageSize="15" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvATMList_RowDataBound"
                      OnRowCommand="gvATMList_RowCommand" OnRowDeleting="gvATMList_RowDeleting">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbxSelect" runat="server"/>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, atmid %>">
                    <ItemTemplate>
                        <asp:LinkButton ID="lblATMCode" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("ATMID") %>' OnClientClick="Loading();"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, address %>">
                    <ItemTemplate>
                        <asp:Label ID="lblAddress" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, country %>">
                    <ItemTemplate>
                        <asp:Label ID="lblCountry" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, thanhpho %>">
                    <ItemTemplate>
                        <asp:Label ID="lblCity" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, quanhuyen %>">
                    <ItemTemplate>
                        <asp:Label ID="lblDistrict" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, chinhanh %>">
                    <ItemTemplate>
                        <asp:Label ID="lblBranch" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-primary" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CommandArgument='<%#Eval("ATMID") %>' OnClientClick="Loading();">Edit</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CommandArgument='<%#Eval("ATMID") %>' OnClientClick="Loading(); return Confirm();">Delete</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"/>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <control:GridViewPaging runat="server" ID="GridViewPaging"/>
        <asp:HiddenField ID="hdCounter" Value="0" runat="server"/>
        <asp:HiddenField ID="hdPageSize" Value="15" runat="server"/>
    </div>
</asp:Panel>

<script src="/JS/Common.js"></script>
<script>
    function SelectCbx(obj) {
        Counter = 0;
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        TotalChkBx = parseInt(document.getElementById('<%= hdPageSize.ClientID %>').value);
        var count = document.getElementById('<%= gvATMList.ClientID %>').rows.length;
        var elements = document.getElementById('<%= gvATMList.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvATMList_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvATMList_ctl01_cbxSelectAll') {
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

        var grid = document.getElementById('<%= gvATMList.ClientID %>');
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
        if (hdf.value == 0) {
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