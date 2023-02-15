<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCorpMatrixUserGroup_Controls_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.assignusertogroup %>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>


        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2><%=Resources.labels.userlist %></h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnInfo" runat="server">
                                <div class="row">
                                    <div class="col-sm-8 col-xs-12 col-sm-offset-2">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.userid %> / <%=Resources.labels.fullname %></label>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-3 col-xs-12">
                                                <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, search %>" OnClick="btnSearch_Click" OnClientClick="Loading();" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <asp:GridView ID="gvUser" CssClass="table table-hover" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%" AllowPaging="True" AutoGenerateColumns="False" PageSize="15" OnRowDataBound="gvUser_RowDataBound" OnPageIndexChanging="gvUser_OnPageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="cbxSelectAll" runat="server" onclick="SelectAll(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbxSelect" runat="server" onclick="CbxCheck(this);" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gvHeader" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:labels, nguoidung %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gvHeader" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:labels, username %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gvHeader" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:labels, fullname %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFullName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FullName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gvHeader" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2><%=Resources.labels.group1 %></h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <div class="row">
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.group1 %></label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:DropDownList ID="ddlGroup" CssClass="form-control select2" Width="100%" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnAdd" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, add %>" OnClick="btnAdd_Click" OnClientClick="Loading();" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btnNext" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClick="btnNext_onclick" OnClientClick="Loading();" />
                            <asp:Button ID="btnBack" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btnBack_onclick" OnClientClick="Loading();" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divResult">
            <asp:GridView ID="gvUserGroup" runat="server" CssClass="table table-hover" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%" PageSize="15" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvUserGroup_OnPageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="<%$ Resources:labels, nguoidung %>" DataField="UserID" />
                    <asp:BoundField HeaderText="<%$ Resources:labels, tendaydu %>" DataField="FullName" />
                    <asp:BoundField DataField="GroupID" HeaderText="<%$ Resources:labels, group %>" />
                    <asp:BoundField DataField="GroupName" HeaderText="<%$ Resources:labels, groupname %>" />
                    <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="colGroupDelete" CssClass="btn btn-secondary" runat="server" OnClick="colGroupDelete_onclick" CommandArgument='<%# Eval("UserID")+"|"+Eval("GroupID") %>' OnClientClick="Loading(); return Confirm();">Delete</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" CssClass="pager" />
            </asp:GridView>
        </div>

        <asp:HiddenField ID="hdfCountRowsDisabled" runat="server" />
        <asp:HiddenField ID="hdfCountCbxChecked" runat="server" />
        <asp:HiddenField ID="hdfTotalRow" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }
    function Confirm() {
        return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
    }
    function SelectSingleRadiobutton(rdbtnid) {
        var rdBtn = document.getElementById(rdbtnid);
        var rdBtnList = document.getElementsByTagName("input");
        for (i = 0; i < rdBtnList.length; i++) {
            if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id && rdBtnList[i].value == "cbGroup") {
                rdBtnList[i].checked = false;
            }
        }
    }
    function SelectAll(chkbox) {
        var chk = chkbox;
        var grid = document.getElementById("<%= gvUser.ClientID %>");
        var hdf = document.getElementById("<%= hdfCountCbxChecked.ClientID %>");
        var hdfTotalrow = document.getElementById("<%= hdfTotalRow.ClientID %>");
        var countTotal = parseInt(hdfTotalrow.value);
        if (isNaN(countTotal)) countTotal = 0;
        var count = 0;
        debugger;
        if (chk.checked) {
            if (countTotal > 0) {
                for (i = 1; i <= countTotal; i++) {
                    var cbx = grid.rows[i].cells[0].childNodes[1];
                    if (cbx.type == "checkbox" && !cbx.disabled) {
                        cbx.checked = true;
                        count = count === grid.rows.length ? grid.rows.length : count + 1;
                    }
                }
            }
        }
        else {
            if (countTotal > 0) {
                for (i = 1; i <= countTotal; i++) {
                    var cbx = grid.rows[i].cells[0].childNodes[1];
                    if (cbx.type == "checkbox" && !cbx.disabled) {
                        cbx.checked = false;
                        count = count === 0 ? 0 : count - 1;
                    }
                }
            }
        }
        hdf.value = count.toString();
    }

    function CbxCheck(chkbox) {
        var chk = chkbox;
        var grid = document.getElementById("<%= gvUser.ClientID %>");
        var hdf = document.getElementById("<%= hdfCountCbxChecked.ClientID %>");
        var hdfDis = document.getElementById("<%= hdfCountRowsDisabled.ClientID %>");
        var hdfTotalrow = document.getElementById("<%= hdfTotalRow.ClientID %>");
        var cell;
        var count = parseInt(hdf.value);
        var countDis = parseInt(hdfDis.value);
        var countTotal = parseInt(hdfTotalrow.value);
        if (isNaN(count)) count = 0;
        if (isNaN(countDis)) countDis = 0;
        if (isNaN(countTotal)) countTotal = 0;

        if (!chk.checked) {
            if (grid.rows.length > 0) {
                if (grid.rows[0].cells[0].childNodes[1].type == "checkbox") {
                    grid.rows[0].cells[0].childNodes[1].checked = false;
                }
            }

            if (count > 0) {
                count--;
            }
        } else {
            count++;
            grid.rows[0].cells[0].childNodes[1].checked = (count === countTotal - countDis);
        }
        hdf.value = count.toString();
    }

    function IsCheckAll() {
        var grid = document.getElementById("<%= gvUser.ClientID %>");
        var hdf = document.getElementById("<%= hdfCountCbxChecked.ClientID %>");
        var hdfTotalrow = document.getElementById("<%= hdfTotalRow.ClientID %>");
        var hdfDis = document.getElementById("<%= hdfCountRowsDisabled.ClientID %>");
        var countTotal = parseInt(hdfTotalrow.value);
        var countDis = parseInt(hdfDis.value);
        if (isNaN(countTotal)) countTotal = 0;
        if (isNaN(countDis)) countDis = 0;
        var count = 0;
        if (countTotal > 0) {
            for (i = 1; i <= countTotal; i++) {
                var cbx = grid.rows[i].cells[0].childNodes[1];
                if (cbx.type == "checkbox" && !cbx.disabled && cbx.checked) {
                    count++;
                }
            }
            grid.rows[0].cells[0].childNodes[1].checked = (count === countTotal - countDis);
        }
        hdf.value = count.toString();
    }
</script>
