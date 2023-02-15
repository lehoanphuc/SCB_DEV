<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContracLevel_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.ListContractLevel %>
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
            <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
                <div class="row">
                        <div class="col-sm-10 col-xs-12">
                            <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.ContractLevelCode %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:TextBox ID="txtContractLevelCode" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.ContractLevelName %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:TextBox ID="txtContractLevelName" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.status %></label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:DropDownList ID="ddStatus" CssClass="form-control select2 infinity" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, search %>"
                                OnClick="btnSearch_Click" />
                        </div>
                    </div>
            </asp:Panel>
        </div>
        <div id="divToolbar">
            <asp:Button ID="btnAdd_New" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" />
            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click"  OnClientClick="return ConfirmDelete2();"/>
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
                        <div class="table-responsive" style="overflow-x:hidden!important">
                            <table class="table table-hover footable c_list" style="margin-bottom: 0px">
                                <thead class="thead-light repeater-table">
                                    <tr>
                                        <th class="title-repeater">
                                            <input name="item1" data-control='<%=hdContractLevel.ClientID %>' type="checkbox" onclick="CheckboxAll(this)">
                                        </th>
                                        <th class="title-repeater"><%=Resources.labels.ContractLevelCode%></th>
                                        <th class="title-repeater"><%=Resources.labels.ContractLevelName%></th>
                                        <th class="title-repeater"><%=Resources.labels.status%></th>
                                        <th class="title-repeater" style="width:5%"><%=Resources.labels.edit%></th>
                                        <th class="title-repeater" style="width:5%"><%=Resources.labels.delete%></th>
                                    </tr>
                                </thead>
                                <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="tdcheck tr-boder item-center">
                            <input name="item1" class="check" value="<%#Eval("CONTRACT_LEVEL_ID")%>" onclick="ConfigCheckbox(this)" type="checkbox">
                        </td>
                        <td class="tr-boder"> <asp:LinkButton runat="server" CommandArgument='<%#Eval("CONTRACT_LEVEL_ID")%>' CommandName='<%#IPC.ACTIONPAGE.DETAILS%>'><%#Eval("CONTRACT_LEVEL_CODE")%></asp:LinkButton></td>
                        <td class="tr-boder"><%#Eval("CONTRACT_LEVEL_NAME") %></td>
                        <td class="tr-boder"><%#Eval("STATUS") %></td>
                        <td class="tr-boder item-center">
                            <asp:LinkButton runat="server" class="btn btn-primary" CommandArgument='<%#Eval("CONTRACT_LEVEL_ID")%>' CommandName='<%#IPC.ACTIONPAGE.EDIT%>'><%=Resources.labels.edit%></asp:LinkButton>
                        </td>
                        <td class="tr-boder item-center">
                            <asp:LinkButton runat="server" class="btn btn-secondary" CommandArgument='<%#Eval("CONTRACT_LEVEL_ID")%>' CommandName='<%#IPC.ACTIONPAGE.DELETE%>' OnClientClick="return ConfirmDelete('Are you sure you want to delete this entry?');"><%=Resources.labels.delete%></asp:LinkButton>
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
            <asp:HiddenField runat="server" ID="hdContractLevel" />
        </asp:Panel>
            <uc1:GridViewPaging ID="GridViewPaging" Visible="false" runat="server"></uc1:GridViewPaging>
        </div>
    </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<script>
    function ConfirmDelete2() {
        var hdf = document.getElementById("<%= hdContractLevel.ClientID %>");
        if (hdf.value == 0) {
            alert('<%=Resources.labels.pleaseselectbeforedeleting %>');
            return false;
        } else {
            return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
        }
    }
</script>
