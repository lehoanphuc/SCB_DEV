<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSREGIONFEE_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div id="divSearch">
            <div class="subheader">
                <h1 class="subheader-title">
                    <%=Resources.labels.accountdefinition %>
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
                    <div class="row">
                        <div class="container">
                            <span>
                                <em class="fa fa-angle-down"></em>
                                <span id="toggleDetail" data-toggle="collapse" href="#AdvanceSearch" role="button" aria-expanded="false" aria-controls="collapseExample">
                                    <label for="collapsible" class="lbl-toggle"><%=Resources.labels.AdvanceSearch%></label>
                                    <span style="display: block;" class="downarrowclass"></span>
                                </span>
                                <div class="collapse in" id="AdvanceSearch">
                                    <div class="panel-content form-horizontal p-b-0" style="display: block;">
                                        <div class="row" style="margin-left: 2%">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.accountnumber %></label>
                                                    <div class="col-sm-8" >
                                                        <asp:TextBox ID="txtAccNumber" CssClass="form-control" runat="server" Width ="82%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.balanceSide %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlBalanceSide" CssClass="form-control select2" Style="width: 100%;" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-left: 2%">

                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.Baseaccountnumber %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtBacno" CssClass="form-control" runat="server" Width ="82%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.group1 %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlGroup" CssClass="form-control select2" Style="width: 100%;" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-left: 2%">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.accountname %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtAccName" CssClass="form-control" runat="server" Width ="82%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.AccountLevel %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlAccountLevel" CssClass="form-control select2" Style="width: 100%;" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-left: 2%">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.classification %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlClassification" CssClass="form-control select2" Style="width: 100%;" AutoPostBack="true" runat="server" Width ="82%"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                    <asp:Button ID="btAdvanceSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, search %>" OnClick="btnAdvanceSearch_click" />

                                    <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_Click" />
                                </div>
                        </div>
        </div>
        </div>
                </div>
            </div>
        </div>
        <div id="divToolbar">
            <asp:Button ID="btnAdd_New" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAdd_New_Click" OnClientClick="Loading();" />
            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" OnClientClick="return ConfirmDelete2();" />
        </div>
        <%-- Table View --%>
        <asp:Panel runat="server" ID="pnResult" Visible="false">
            <div id="divResult" style="overflow-x: hidden;">
                <asp:Panel ScrollBars="Auto" runat="server">
                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                        <HeaderTemplate>
                            <div class="table-responsive">
                                <table class="table table-bordered" style="border: 1px solid rgba(0, 0, 0, 0.1); border-style: Solid;">
                                    <thead style="background-color: #7A58BF; color: #FFF;">
                                        <tr>
                                            <th class="title-repeater">
                                                <input name="item1" data-control='<%=hdCLMS_SCO_SCO_PRODUCT.ClientID %>' type="checkbox" onclick="CheckboxAll(this)">
                                            </th>
                                            <th class="title-repeater"><%=Resources.labels.accountnumber%></th>
                                            <th class="title-repeater"><%=Resources.labels.Baseaccountnumber%></th>
                                            <th class="title-repeater"><%=Resources.labels.accountname%></th>
                                            <th class="title-repeater"><%=Resources.labels.Balance%></th>
                                            <th class="title-repeater"><%=Resources.labels.AccountLevel%></th>
                                            <th class="title-repeater"><%=Resources.labels.classification%></th>
                                            <th class="title-repeater"><%=Resources.labels.balanceSide%></th>
                                            <th class="title-repeater"><%=Resources.labels.group1%></th>
                                            <th class="title-repeater"><%=Resources.labels.edit%></th>
                                            <th class="title-repeater"><%=Resources.labels.delete%></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>

                            <tr>
                                <td class="tdcheck">
                                    <input name="item1" class="check" value="<%#Eval("Acno")%>" onclick="ConfigCheckbox(this)" type="checkbox">
                                </td>
                                <td class="action">
                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Acno")%>' CommandName='<%#IPC.ACTIONPAGE.DETAILS%>' OnClientClick="Loading();"><%#Eval("Acno") %></asp:LinkButton>
                                </td>
                                <td><%#Eval("BAcno") %></td>
                                <td><%#Eval("ACName") %></td>
                                <td style="text-align: right"><%#Eval("BALANCE") %></td>
                                <td style="text-align: center"><%#Eval("ACLevel") %></td>
                                <td><%#Eval("C_AC_CLS") %></td>
                                <td><%#Eval("C_BALANCE_SIDE")%></td>
                                <td><%#Eval("C_AC_GRP") %></td>
                                <td class="action" style="text-align: center">
                                    <asp:LinkButton runat="server" class="btn btn-primary" CommandArgument='<%#Eval("Acno")%>' CommandName='<%#IPC.ACTIONPAGE.EDIT%>'>Edit</asp:LinkButton>
                                </td>
                                <td class="action" style="text-align: center">
                                    <asp:LinkButton runat="server" class="btn btn-secondary" CommandArgument='<%#Eval("Acno")%>' CommandName='<%#IPC.ACTIONPAGE.DELETE%>' OnClientClick="return ConfirmDelete('Are you sure you want to delete this entry?');">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                        </table>
                        </div> 
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:HiddenField runat="server" ID="hdCLMS_SCO_SCO_PRODUCT" />
                    <%--</div>--%>
                </asp:Panel>
                <uc1:GridViewPaging ID="GridViewPaging" runat="server"></uc1:GridViewPaging>
            </div>
        </asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>
<script>
    function ConfirmDelete2() {
        var hdf = document.getElementById("<%= hdCLMS_SCO_SCO_PRODUCT.ClientID %>");
        if (hdf.value == 0) {
            alert('<%=Resources.labels.pleaseselectbeforedeleting %>');
            return false;
        } else {
            return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
        }
    }
</script>
