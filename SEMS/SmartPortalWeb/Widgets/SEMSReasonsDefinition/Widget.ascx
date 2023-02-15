<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSReasons_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>


<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<script>



</script>
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
                    <%=Resources.labels.reasonsDefinition%>
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
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.ReasonCode %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtReasonCode" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-1"></div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.reasonType%></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddreasontype" CssClass="form-control select2" Style="width: 100%" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.ReasonName %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtReasonName" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-1"></div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels._event%></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList runat="server" CssClass="form-control select2" Style="width: 100%" ID="ddEvent" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.reasonAction%></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddreasonaction" CssClass="form-control select2" Style="width: 100%" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-1"></div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.status%></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList runat="server" CssClass="form-control select2" Style="width: 100%" ID="ddstatus" />
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
            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" OnClientClick="Loading(); return ConfirmDelete2();" />
        </div>
        <%-- Table View --%>
        <asp:Panel Visible="false" ID="p" runat="server">
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
                                            <th class="title-repeater"><%=Resources.labels.ReasonCode%></th>
                                            <th class="title-repeater"><%=Resources.labels.ReasonName%></th>
                                            <th class="title-repeater"><%=Resources.labels.reasonAction%></th>
                                            <th class="title-repeater"><%=Resources.labels.reasonType%></th>
                                            <th class="title-repeater"><%=Resources.labels._event%></th>
                                            <th class="title-repeater"><%=Resources.labels.status%></th>
                                            <th class="title-repeater"><%=Resources.labels.edit%></th>
                                            <th class="title-repeater"><%=Resources.labels.delete%></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="tdcheck tr-boder item-center">
                                <input name="item1" class="check" value="<%#Eval("ReasonID")%>" onclick="ConfigCheckbox(this)" type="checkbox">
                            </td>
                            <td class="tr-boder">
                                <asp:LinkButton runat="server" CommandArgument='<%#Eval("ReasonID")%>' CommandName='<%#IPC.ACTIONPAGE.DETAILS%>' OnClientClick="Loading();"><%#Eval("ReasonCode") %>
                                </asp:LinkButton>
                            </td>
                            <td class="tr-boder"><%#Eval("ReasonName") %></td>
                            <td class="tr-boder"><%#Eval("CAP_ACTION")%></td>
                            <td class="tr-boder"><%#Eval("CAP_TYPE")%></td>
                            <td class="tr-boder"><%#Eval("CAP_EVENT")%></td>
                            <td class="tr-boder"><%#Eval("CAP_STT")%></td>
                            <td class="action tr-boder item-center">
                                <asp:LinkButton runat="server" class="btn btn-primary" CommandArgument='<%#Eval("ReasonID")%>' CommandName='<%#IPC.ACTIONPAGE.EDIT%>' OnClientClick="Loading();">Edit
                                </asp:LinkButton>
                            </td>
                            <td class="action tr-boder item-center">

                                <asp:LinkButton runat="server" class="btn btn-secondary" CommandArgument='<%#Eval("ReasonID")%>' CommandName='<%#IPC.ACTIONPAGE.DELETE%>' OnClientClick="return Confirm();">Delete
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

    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }

    function Confirm() {
        return confirm('Are you sure you want to delete?');
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

