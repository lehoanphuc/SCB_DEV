<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSAgentBank_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Register Src="~/Controls/SearchTextBox/LoadCountry.ascx" TagPrefix="uc1" TagName="LoadCountry" %>

<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<script>



</script>
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
                    <%=Resources.labels.agentbanksearch %>
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
                                <div class="panel-content form-horizontal p-b-0" style="display: block; margin-left: 2%">

                                    <div class="row">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.BankCode %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox Width="86%" ID="txtBankCode" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-1"></div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.banktype%></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList Width="86%" ID="ddBanktype" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.BankName %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox Width="86%" ID="txtBankName" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-1"></div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.status %></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList Width="86%" ID="ddStatus" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.shortname %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox Width="86%" ID="txtShortName" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-1"></div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.countrycode %></label>
                                                <div class="col-sm-8">
                                                    <uc1:LoadCountry Width="86%" runat="server" CssClass="form-control" ID="txtCountryCode" />
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
                                <table class="table table-hover footable c_list">
                                    <thead class="thead-light repeater-table">
                                        <tr>
                                            <th class="title-repeater">
                                                <input name="item1" data-control='<%=hdCLMS_SCO_SCO_PRODUCT.ClientID %>' type="checkbox" onclick="CheckboxAll(this)">
                                            </th>
                                            <th class="title-repeater"><%=Resources.labels.BankCode%></th>
                                            <th class="title-repeater"><%=Resources.labels.BankName%></th>
                                            <th class="title-repeater"><%=Resources.labels.shortname%></th>
                                            <th class="title-repeater"><%=Resources.labels.banktype%></th>
                                            <th class="title-repeater"><%=Resources.labels.countrycode%></th>
                                            <th class="title-repeater"><%=Resources.labels.countryname%></th>
                                            <th class="title-repeater"><%=Resources.labels.status%></th>
                                            <th class="title-repeater"><%=Resources.labels.edit%></th>
                                            <th class="title-repeater"><%=Resources.labels.delete%></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="tr-boder" style="text-align: center">
                                <input name="item1" class="check" value="<%#Eval("BankID")%>" onclick="ConfigCheckbox(this)" type="checkbox">
                            </td>
                            <td class="tr-boder">
                                <asp:LinkButton runat="server" CommandArgument='<%#Eval("BankID")%>' CommandName='<%#IPC.ACTIONPAGE.DETAILS%>' OnClientClick="Loading();"><%#Eval("BankCode")%>
                                </asp:LinkButton>
                            </td>
                            <td class="tr-boder"><%#Eval("BankName") %></td>
                            <td class="tr-boder"><%#Eval("ShortName")%></td>
                            <td class="tr-boder"><%#Eval("BankType")%></td>
                            <td class="tr-boder"><%#Eval("CountryCode")%></td>
                            <td class="tr-boder"><%#Eval("CountryName")%></td>
                            <td class="tr-boder"><%#Eval("Status")%></td>
                            <td class="tr-boder" style="text-align: center">
                                <asp:LinkButton runat="server" class="btn btn-primary" CommandArgument='<%#Eval("BankID")%>' CommandName='<%#IPC.ACTIONPAGE.EDIT%>' OnClientClick="Loading();">Edit
                                </asp:LinkButton>
                            </td>

                            <td class="tr-boder" style="text-align: center">
                                <asp:LinkButton runat="server" class="btn btn-secondary" CommandArgument='<%#Eval("BankID")%>' CommandName='<%#IPC.ACTIONPAGE.DELETE%>' OnClientClick="return ConfirmDelete('Are you sure you want to delete this entry?');">Delete
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

