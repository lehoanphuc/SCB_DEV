<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContracLevel_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.ReversalTranLimit %>
            </h1>
        </div>
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
            <asp:Panel ID="pnSearch" runat="server" class="panel-content form-horizontal p-b-0" DefaultButton="btnSearch">

                <div class="row" style="margin-left: 2%">
                    <div class="col-sm-5 col-xs-12">
                        <div class="form-group">
                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.loaigiaodich %></label>
                            <div class="col-sm-8 col-xs-12">
                                <asp:DropDownList ID="ddlTransactionType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </div>

                        </div>
                    </div>
                    <div class="col-sm-5 col-xs-12">
                        <div class="form-group">
                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.unittype %></label>
                            <div class="col-sm-8 col-xs-12">
                                <asp:DropDownList ID="ddlUnit" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                    <%--<asp:ListItem Value="H" Text="<%$ Resources:labels, gio %>"></asp:ListItem>--%>
                                    <asp:ListItem Value="D" Text="<%$ Resources:labels, ngaysegui %>"></asp:ListItem>
                                    <%--<asp:ListItem Value="M" Text="<%$ Resources:labels, month %>"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-2 col-xs-12">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, search %>"
                            OnClick="btnSearch_Click" />
                    </div>
                </div>
                <div class="row" style="margin-left: 2%">
                    <div class="col-sm-5 col-xs-12">
                        <div class="form-group">
                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.hanmuc %></label>
                            <div class="col-sm-6 ">
                                <asp:TextBox ID="txtLimit" MaxLength="4" onkeypress="return isNumberKey(event)" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <asp:CheckBox ID="checktxtlimit" OnCheckedChanged="checktxtlimit_OnCheckedChanged" AutoPostBack="True" runat="server" />
                            Unlimit
                        </div>
                    </div>
                </div>

            </asp:Panel>
        </div>
        <div id="divToolbar">
            <asp:Button ID="btnAdd_New" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" />
            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" OnClientClick="return ConfirmDelete2();" />
        </div>
        <asp:Panel ID="pnResult" Visible="false" runat="server">
            <div id="divResult" style="overflow-x: hidden">
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <%-- Table View --%>
                <asp:Panel runat="server">
                    <asp:Literal ID="LiteralErr" runat="server"></asp:Literal>
                    <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound" OnItemCommand="rptData_ItemCommand">
                        <HeaderTemplate>
                            <div class="pane">
                                <div class="table-responsive" style="overflow-x: hidden!important">
                                    <table class="table table-hover footable c_list" style="margin-bottom: 0px">
                                        <thead class="thead-light repeater-table">
                                            <tr>
                                                <th class="title-repeater">
                                                    <input name="item1" data-control='<%=hdReversalLimit.ClientID %>' type="checkbox" onclick="CheckboxAll(this)">
                                                </th>
                                                <th class="title-repeater"><%=Resources.labels.loaigiaodich%></th>
                                                <th class="title-repeater"><%=Resources.labels.hanmuc%></th>
                                                <th class="title-repeater"><%=Resources.labels.unittype%></th>
                                                <th class="title-repeater"><%=Resources.labels.reversal%></th>
                                                <th class="title-repeater" style="width: 5%"><%=Resources.labels.edit%></th>
                                                <th class="title-repeater" style="width: 5%"><%=Resources.labels.delete%></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="tdcheck tr-boder item-center">
                                    <input name="item1" class="check" value="<%#Eval("ipctrancode")%>" onclick="ConfigCheckbox(this)" type="checkbox">
                                </td>
                                <td class="tr-boder">
                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ipctrancode")%>' CommandName='<%#IPC.ACTIONPAGE.DETAILS%>'><%#Eval("pagename")%></asp:LinkButton></td>
                                <%--<td class="tr-boder"><%#Eval("pagename") %></td>--%>
                                <td class="tr-boder"><%#Eval("LimitRR") %></td>
                                <td class="tr-boder"><%#Eval("Unit") %></td>
                                <td class="tr-boder"><%#Eval("IsReversal") %></td>
                                <td class="tr-boder item-center">
                                    <asp:LinkButton runat="server" class="btn btn-primary" CommandArgument='<%#Eval("ipctrancode")%>' CommandName='<%#IPC.ACTIONPAGE.EDIT%>'><%=Resources.labels.edit%></asp:LinkButton>
                                </td>
                                <td class="tr-boder item-center">
                                    <asp:LinkButton runat="server" class="btn btn-secondary" CommandArgument='<%#Eval("ipctrancode")%>' CommandName='<%#IPC.ACTIONPAGE.DELETE%>' OnClientClick="return ConfirmDelete('Are you sure you want to delete this entry?');"><%=Resources.labels.delete%></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                        </table>
                        </div> </div>
                    <%-- Label used for showing Error Message --%>
                            <asp:Label ID="lblErrorMsg" runat="server" CssClass="errMsg" Text="Data not found" Visible="false">
                            </asp:Label>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:HiddenField runat="server" ID="hdReversalLimit" />
                </asp:Panel>
                <uc1:GridViewPaging ID="GridViewPaging" Visible="false" runat="server"></uc1:GridViewPaging>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script>
    function ConfirmDelete2() {
        var hdf = document.getElementById("<%= hdReversalLimit.ClientID %>");
        if (hdf.value == 0) {
            alert('<%=Resources.labels.pleaseselectbeforedeleting %>');
            return false;
        } else {
            return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
        }
    }
</script>
