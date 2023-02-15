<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_Group_Controls_Widget" %>
<script src="JS/treeview.js"></script>
<asp:ScriptManager runat="server"></asp:ScriptManager>
<asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hdID" />
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitle" runat="server"></asp:Label>
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
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.grouptab%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnGroupInfo" runat="server">
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label"><%=Resources.labels.loaihinhdichvu %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList ID="ddlServiceID" CssClass="form-control select2 infinity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnChooseServices">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label"><%=Resources.labels.loainguoidung %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList ID="ddlUserType" CssClass="form-control select2 infinity" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label"><%=Resources.labels.roletype %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList ID="ddlRoleType" CssClass="form-control select2 infinity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRoleType_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label required"><%=Resources.labels.rolename %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox ID="txtGroupName" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label"><%=Resources.labels.roledesc %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label"><%=Resources.labels.status %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList ID="ddlStatus" CssClass="form-control select2 infinity" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="Loading(); return checkValidation()" OnClick="btnSave_Click" />
                            <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClientClick="Loading();" OnClick="btnClear_Click" />
                            <asp:Button ID="btnBack" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClientClick="Loading();" OnClick="btnBack_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel runat="server" ID="pnRole">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.role%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <asp:TreeView ID="tvPage" runat="server" ImageSet="Simple" ShowLines="True">
                                            <ParentNodeStyle Font-Bold="False" />
                                            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                            <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                                            <NodeStyle CssClass="p-l-10" ForeColor="Black" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                                        </asp:TreeView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br/>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnPermission">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.setroleforgroup%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <asp:GridView ID="gvSource" CssClass="table" runat="server" AllowPaging="True"
                                            AllowSorting="True" AutoGenerateColumns="false" CellPadding="4"
                                            ForeColor="#333333" GridLines="Both" OnPageIndexChanging="gvSource_PageIndexChanging"
                                            OnRowDataBound="gvSource_RowDataBound"
                                            PageSize="100" Width="100%">
                                            <PagerStyle HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnReport">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.setroleforreport%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <asp:Repeater runat="server" ID="rptRole">
                                        <ItemTemplate>
                                            <div class="col-sm-4 col-xs-12 custom-control">
                                                <asp:HiddenField runat="server" ID="hdRole" Value='<%# DataBinder.Eval(Container.DataItem, "rptID") %>' />
                                                <asp:CheckBox runat="server" ID="cbRole" Text='<%# DataBinder.Eval(Container.DataItem, "rptName") %>' Checked='<%# DataBinder.Eval(Container.DataItem, "hasRole").ToString().Equals("1") %>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <br/>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function AllCell(obj, rowIndex) {
        rowIndex = rowIndex + 1;
        var elements = document.getElementById('<%=gvSource.ClientID %>').rows[rowIndex];
        var count = elements.cells.length;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements.cells[i].childNodes[0].type == 'checkbox') {
                    elements.cells[i].childNodes[0].checked = true;
                }
            }
        } else {
            for (i = 0; i < count; i++) {
                if (elements.cells[i].childNodes[0].type == 'checkbox') {
                    elements.cells[i].childNodes[0].checked = false;
                }
            }
        }
    }
    function ChildCell(obj, rowIndex) {
        rowIndex = rowIndex + 1;
        var elements = document.getElementById('<%=gvSource.ClientID %>').rows[rowIndex];
        var count = elements.cells.length;
        var countcell = 0;
        var countcheck = 0;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements.cells[i].childNodes[0].type == 'checkbox') {
                    countcell++;
                }
            }
            for (i = 0; i < count; i++) {
                if (elements.cells[i].childNodes[0].type == 'checkbox') {
                    if (elements.cells[i].childNodes[0].checked == true) {
                        countcheck++;
                    }
                }
            }
            if (countcheck == 0) {
                for (i = 0; i < count; i++) {
                    if (elements.cells[i].childNodes[0].type == 'checkbox') {
                        elements.cells[0].children[0].checked = false;
                    }
                }
            } else if (countcheck == countcell - 1) {
                for (i = 0; i < count; i++) {
                    if (elements.cells[i].childNodes[0].type == 'checkbox') {
                        elements.cells[i].childNodes[0].checked = true;
                    }
                }
            }
        } else {
            var index = 0;
            for (i = 0; i < count; i++) {
                if (elements.cells[i].childNodes[0].type == 'checkbox') {
                    index++;
                    if (index == 1) {
                        elements.cells[i].childNodes[0].checked = false;
                    }
                }
            }
        }
    }
    function checkValidation() {
        if (document.getElementById('<%=txtGroupName.ClientID %>').value.trim() == '') {
            alert('<%=Resources.labels.groupnamecannotbeempty %>');
            document.getElementById('<%=txtGroupName.ClientID %>').focus();
            return false;
        }
        return true;
    }
    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }
</script>
