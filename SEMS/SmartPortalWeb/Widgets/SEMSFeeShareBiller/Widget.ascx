<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSREGIONFEE_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divSearch">
            <div class="subheader">
                <h1 class="subheader-title">
                    <%=Resources.labels.feeShareDetail %>
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
                            <span>
                                <em class="fa fa-angle-down"></em>
                                <span id="toggleDetail" data-toggle="collapse" href="#AdvanceSearch" role="button" aria-expanded="false" aria-controls="collapseExample">
                                    <label for="collapsible" class="lbl-toggle"><%=Resources.labels.AdvanceSearch%></label>
                                    <span style="display: block;" class="downarrowclass"></span>
                                </span>
                                <div class="collapse in" id="AdvanceSearch">
                                    <div class="panel-content form-horizontal p-b-0" style="display: block;">
                                        <div class="row" style="margin-left: 2%">
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.FeeShareCode %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtFeeShareCode"  MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.billerid %></label>
                                                    <div class="col-sm-8">
                                                         <asp:DropDownList ID="ddBiller" CssClass="form-control select2" Style="width: 100%;"  runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-left: 2%">
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label "><%=Resources.labels.phibacthang%> </label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlIsLadder" CssClass="form-control select2" Style="width: 100%;"  runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-1">
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
        <panel runat="server" visible="false" id="pnResult">
            <%-- Table View --%>
            <div id="divResult" style="overflow-x: hidden;">
                <asp:Panel ScrollBars="Auto" runat="server">
                    <%--<div style="overflow-x:hidden; overflow-y:scroll;">--%>
                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                        <HeaderTemplate>
                            <div class="pane">
                                <div class="table-responsive">
                                    <table class="table table-hover footable c_list">
                                        <thead class="thead-light repeater-table">
                                            <tr>
                                                <th class="title-repeater">
                                                    <input name="item1" data-control='<%=hdCLMS_SCO_SCO_PRODUCT.ClientID %>' type="checkbox" onclick="CheckboxAll(this)">
                                                </th>
                                                <th class="title-repeater"><%=Resources.labels.FeeShareCode%></th>
                                                 <th class="title-repeater"><%=Resources.labels.FeeShareName%></th>
                                                <th class="title-repeater"><%=Resources.labels.billerid%></th>
                                                <th class="title-repeater"><%=Resources.labels.phibacthang%></th>
                                                <th class="title-repeater"><%=Resources.labels.tu%></th>
                                                <th class="title-repeater"><%=Resources.labels.den%></th>
                                                <th class="title-repeater"><%=Resources.labels.edit%></th>
                                                <th class="title-repeater"><%=Resources.labels.delete%></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="tr-boder">
                                    <input name="item1" class="check" value="<%#Eval("FeeShareTypeID") + "+" + Eval("FromLimitShow").ToString() + "+" + Eval("ToLimitShow").ToString()%>" onclick="ConfigCheckbox(this)" type="checkbox">
                                </td>
                                <td class="action tr-boder">
                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("FeeShareTypeID") + "+" + Eval("FromLimitShow").ToString() + "+" + Eval("ToLimitShow").ToString()%>' CommandName='<%#IPC.ACTIONPAGE.DETAILS%>' OnClientClick="Loading();"><%#Eval("FeeShareCode") %></asp:LinkButton>
                                </td>
                                <td class="tr-boder"><%#Eval("FeeShareName") %></td>
                                <td class="tr-boder"><%#Eval("BiilerID") %></td>
                                <td class="tr-boder"><%#Eval("IsLadderShow") %></td>
                                <td class="tr-boder" style="text-align: right"><%#Eval("FromLimitShow")%></td>
                                <td class="tr-boder" style="text-align: right"><%#Eval("ToLimitShow")%></td>
                                <td class="tr-boder">
                                    <asp:LinkButton runat="server" class="btn btn-primary" CommandArgument='<%#Eval("FeeShareTypeID") + "+" + Eval("FromLimitShow").ToString() + "+" + Eval("ToLimitShow").ToString() %>' CommandName='<%#IPC.ACTIONPAGE.EDIT%>'>Edit</asp:LinkButton>
                                </td>
                                <td class="tr-boder">
                                    <asp:LinkButton runat="server" class="btn btn-secondary" CommandArgument='<%#Eval("FeeShareTypeID") + "+" + Eval("FromLimitShow").ToString() + "+" + Eval("ToLimitShow").ToString()%>' CommandName='<%#IPC.ACTIONPAGE.DELETE%>' OnClientClick="return ConfirmDelete('Are you sure you want to delete this entry?');">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                            </table>
                            </div> </div>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:HiddenField runat="server" ID="hdCLMS_SCO_SCO_PRODUCT" />
                    <%--</div>--%>
                    <uc1:GridViewPaging ID="GridViewPaging" runat="server"></uc1:GridViewPaging>
                </asp:Panel>
            </div>
        </panel>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>

<script>
    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }

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

