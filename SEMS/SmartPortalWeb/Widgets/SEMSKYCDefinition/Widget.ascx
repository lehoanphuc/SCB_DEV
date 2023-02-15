<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSKYCDefinition_Widget" %>
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
                    <%=Resources.labels.KYCDefinitionSearch %>
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
                                <span id="toggleDetail" data-toggle="collapse" data-target="#AdvanceSearch" role="button" aria-expanded="false" aria-controls="collapseExample">
                                    <label for="collapsible" class="lbl-toggle"><%=Resources.labels.AdvanceSearch%></label>
                                    <span style="display: block;" class="downarrowclass"></span>
                                </span>
                                <asp:Panel ID="pnGeneral" runat="server">
                                    <div class="collapse in" id="AdvanceSearch">
                                        <div class="panel-content form-horizontal p-b-0" style="display: block;">
                                            <div class="row" style="margin-left: 2%">
                                                <div class="col-sm-5">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.KycCode %></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtKycCode" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-5">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.Kycname %></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtKycName" MaxLength="250" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-left: 2%">
                                                <div class="col-sm-5">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.status %></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" Style="width: 100%;" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-5">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                            <asp:Button ID="btAdvanceSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, search %>" OnClick="btnAdvanceSearch_click" />
                                            <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_Click" />
                                        </div>
                                    </div>
                                </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:panel id="pnToolbar" runat="server">
            <div id="divToolbar" hidden="hidden">
                <asp:Button ID="btnAdd_New" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAdd_New_Click" OnClientClick="Loading();" />
                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" OnClientClick="return ConfirmDelete2();" />
            </div>
        </asp:panel>
        <panel runat="server" visible="false" id="pnResult">
        <%-- Table View --%>
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
                                        <th class="title-repeater"><%=Resources.labels.KycCode%></th>
                                        <th class="title-repeater"><%=Resources.labels.Kycname%></th>
                                        <th class="title-repeater"><%=Resources.labels.status%></th>
                                        <th class="title-repeater td-no-action"><%=Resources.labels.edit%></th>
                                        <th hidden="hidden" class="title-repeater td-no-action"><%=Resources.labels.delete%></th>
                                    </tr>
                                </thead>
                                <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="tdcheck tr-boder item-center">
                                <input name="item1" class="check" value="<%#Eval("KYCID")%>" onclick="ConfigCheckbox(this)" type="checkbox">
                            </td>
                            <td class="action tr-boder">
                                <asp:LinkButton runat="server" CommandArgument='<%#Eval("KYCID")%>' CommandName='<%#IPC.ACTIONPAGE.DETAILS%>'><%#Eval("KYCCODE") %> </asp:LinkButton>

                            </td>
                            <td class="tr-boder"><%#Eval("KYCNAME") %></td>
                            <td class="tr-boder"><%#Eval("CAPTION") %></td>
                            <td class="action td-no-action tr-boder">
                                <asp:LinkButton runat="server" CssClass="btn btn-primary" CommandArgument='<%#Eval("KYCID")%>' CommandName='<%#IPC.ACTIONPAGE.EDIT%>' OnClientClick="Loading();">Edit</asp:LinkButton>
                            </td>
                            <td hidden="hidden" class="action td-no-action tr-boder">
                                <asp:LinkButton runat="server" CssClass="btn btn-secondary" CommandArgument='<%#Eval("KYCID")%>' CommandName='<%#IPC.ACTIONPAGE.DELETE%>' OnClientClick="return Confirm();">Delete</asp:LinkButton>
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
                <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
            </asp:Panel>
        </div>
            </panel>
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

