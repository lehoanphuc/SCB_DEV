<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSDepartment_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Register Src="~/Controls/SearchTextBox/LoadBranch.ascx" TagPrefix="uc1" TagName="LoadBranch" %>


<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>

        <div id="divSearch">
            <div class="subheader">
                <h1 class="subheader-title">
                    <%=Resources.labels.departmentsearch%>
                </h1>
            </div>
            <div class="panel-container form-horizontal p-b-0">

                <div class="search_box">
                    <div class="row">
                        <div class="container">
                            <div class="form-group">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, search %>" OnClick="btnSearch_Click" />
                                </div>
                                <div class="col-sm-1"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="wrap-collabsible">
                    <div class="SearchAdvance">
                        <div class="panel-container">

                            <span id="toggleDetail" data-toggle="collapse" href="#Advanced" role="button" aria-expanded="false" aria-controls="collapseExample">
                                <em class="fa fa-angle-up"></em>
                                <label for="collapsible" class="lbl-toggle"><%=Resources.labels.AdvanceSearch%></label>
                                <span style="display: block;" class="downarrowclass"></span>
                            </span>

                            <div class="collapse in" id="Advanced">
                                <div class="panel-content form-horizontal p-b-0" style="display: block;">

                                    <div class="row">
                                        <div class="col-sm-1"></div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.departmentcode %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox Width="76%" ID="txtDepartmentCode" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.branchcode%></label>
                                                <div class="col-sm-8">
                                                    <uc1:LoadBranch ID="txtBranch" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-1"></div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.departmentname %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox Width="76%" ID="txtDepartmentName" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.status %></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList Width="76%" runat="server" CssClass="form-control" ID="ddStatus" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                    <asp:Button ID="btAdvanceSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, search %>" OnClick="btnAdvanceSearch_click" />
                                    <asp:Button ID="btBack" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_click" />

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divToolbar">
            <asp:Button ID="btnAdd_New" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" OnClientClick="Loading();" />
            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" OnClientClick="Loading(); return d();" />
        </div>
        <%-- Table View --%>
        <asp:Panel runat="server" Visible="false" ID="p">
            <asp:Panel ScrollBars="Auto" runat="server">
                <%--<div style="overflow-x:hidden; overflow-y:scroll;">--%>
                <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand" OnItemDataBound="rptData_OnItemDataBound">
                    <HeaderTemplate>
                        <div class="pane">
                            <div class="table-responsive">
                                <table class="table table-bordered" style="border: 1px solid rgba(0, 0, 0, 0.1); border-style: Solid;">
                                    <thead class="thead-light repeater-table">
                                        <tr>
                                            <th class="title-repeater">
                                                <input name="item1" data-control='<%=hdCLMS_SCO_SCO_PRODUCT.ClientID %>' type="checkbox" onclick="CheckboxAll(this)">
                                            </th>
                                            <th class="title-repeater"><%=Resources.labels.departmentcode%></th>
                                            <th class="title-repeater"><%=Resources.labels.departmentname%></th>
                                            <th class="title-repeater"><%=Resources.labels.branchcode%></th>
                                            <th class="title-repeater"><%=Resources.labels.branchname%></th>
                                            <th class="title-repeater"><%=Resources.labels.status%></th>
                                            <th class="title-repeater"><%=Resources.labels.edit%></th>
                                            <th class="title-repeater"><%=Resources.labels.delete%></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="tdcheck" style="text-align: center">
                                <input name="item1" class="check" value="<%#Eval("DeprtID")%>" onclick="ConfigCheckbox(this)" type="checkbox">
                            </td>
                            <td>
                                <asp:LinkButton runat="server" CommandArgument='<%#Eval("DeprtID")%>' CommandName='<%#IPC.ACTIONPAGE.DETAILS%>' OnClientClick="Loading();"><%#Eval("DeprtCode") %>
                                </asp:LinkButton>
                            </td>
                            <td><%#Eval("DeprtName") %></td>
                            <td><%#Eval("BranchID")%></td>
                            <td><%#Eval("BranchName")%></td>
                            <td><%#Eval("Status")%></td>
                            <td class="action" style="text-align: center">
                                <asp:LinkButton runat="server" class="btn btn-primary" CommandArgument='<%#Eval("DeprtID")%>' CommandName='<%#IPC.ACTIONPAGE.EDIT%>' OnClientClick="Loading();">Edit
                                </asp:LinkButton>
                            </td>
                            <td class="action" style="text-align: center">
                                <asp:LinkButton runat="server" class="btn btn-secondary" CommandArgument='<%#Eval("DeprtID")%>' CommandName='<%#IPC.ACTIONPAGE.DELETE%>' OnClientClick="return ConfirmDelete('Are you sure you want to delete this entry?');">Delete
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                        </div> </div>
                     <%-- Label used for showing Error Message --%>
                        <asp:Label ID="lblErrorMsg" runat="server" CssClass="errMsg" Text="Sorry, no item is there to show." Visible="false">
                        </asp:Label>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:HiddenField runat="server" ID="hdCLMS_SCO_SCO_PRODUCT" />
                <%--</div>--%>
            </asp:Panel>
            <uc1:GridViewPaging ID="GridViewPaging" runat="server"></uc1:GridViewPaging>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<script>
    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }

    function d() {
        var hdf = document.getElementById("<%= hdCLMS_SCO_SCO_PRODUCT.ClientID %>");
        if (hdf.value == 0) {
            alert('<%=Resources.labels.pleaseselectbeforedeleting %>');
            return false;
        } else {
            return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
        }
    }
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $('.collapse').on('shown.bs.collapse', function () {
            $(this).parent().find('.fa-angle-down')
                .removeClass('fa-angle-down')
                .addClass('fa-angle-up');
        }).on('hidden.bs.collapse', function () {
            $(this).parent().find(".fa-angle-up")
                .removeClass("fa-angle-up")
                .addClass("fa-angle-down");
        });
    });
</script>
