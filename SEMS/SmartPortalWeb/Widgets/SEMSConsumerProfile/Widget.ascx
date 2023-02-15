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
        <div id="divSearch">
            <div class="subheader">
                <h1 class="subheader-title">
                    <%=Resources.labels.ConsumerProfile %>
                </h1>
            </div>
            <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
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
                                <em class="fa fa-angle-up"></em>
                                <span id="toggleDetail" data-toggle="collapse" href="#AdvanceSearch" role="button" aria-expanded="false" aria-controls="collapseExample">
                                    <label for="collapsible" class="lbl-toggle"><%=Resources.labels.AdvanceSearch%></label>
                                    <span style="display: block;" class="downarrowclass"></span>
                                </span>
                                <div class="collapse in" id="AdvanceSearch">
                                    <div class="panel-content form-horizontal p-b-0" style="display: block;">
                                        <div class="row" style="margin-left: 2%">
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.PhoneNumber %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.gender %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlGender" CssClass="form-control select2" Style="width: 100%;" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-left: 2%">
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.PaperNumber %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtPaperNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.nationality %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlNationality" CssClass="form-control select2" Style="width: 100%;" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-left: 2%">
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.fullname %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtFullName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.status %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" Style="width: 100%;" runat="server"></asp:DropDownList>
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
            </asp:Panel>
        </div>

        <%-- Table View --%>
        <asp:Panel runat="server" ID="pnResult"   Visible="false">
            <div id="divResult"  style="overflow-x: hidden;">
                <asp:Panel runat="server">
                    <%--<div style="overflow-x:hidden; overflow-y:scroll;">--%>
                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                        <HeaderTemplate>
                            <div class="table-responsive">
                                <table class="table table-bordered" style="border: 1px solid rgba(0, 0, 0, 0.1); border-style: Solid;">
                                    <thead style="background-color: #7A58BF; color: #FFF;">
                                        <tr>

                                            <th class="title-repeater"><%=Resources.labels.PhoneNumber%></th>
                                            <th class="title-repeater"><%=Resources.labels.ConsumerCode%></th>
                                            <th class="title-repeater"><%=Resources.labels.fullname%></th>
                                            <th class="title-repeater"><%=Resources.labels.gender%></th>
                                            <th class="title-repeater"><%=Resources.labels.PaperNumber%></th>
                                            <th class="title-repeater"><%=Resources.labels.KycLevel%></th>
                                            <th class="title-repeater"><%=Resources.labels.nationality%></th>
                                            <th class="title-repeater"><%=Resources.labels.status%></th>
                                            <th class="title-repeater"><%=Resources.labels.Action%></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="action tr-boder">
                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("CUSTID")%>' CommandName='<%#IPC.ACTIONPAGE.DETAILS%>' OnClientClick="Loading();"><%#Eval("TEL") %></asp:LinkButton>
                                </td>
                                <td class="tr-boder"><%#Eval("CUSTID") %></td>
                                <td class="tr-boder"><%#Eval("FULLNAME") %></td>
                                <td class="tr-boder"><%#Eval("GENDER")%></td>
                                <td class="tr-boder"><%#Eval("LICENSEID") %></td>
                                <td class="tr-boder"><%#Eval("KYCNAME") %></td>
                                <td class="tr-boder"><%#Eval("CountryName")%></td>
                                <td class="tr-boder"><%#Eval("STATUSCAPTION") %></td>
                                <td class="action tr-boder item-center">
                                    <asp:LinkButton runat="server" class="btn btn-primary" CommandArgument='<%#Eval("CUSTID")%>' CommandName='<%#IPC.ACTIONPAGE.EDIT%>'>Edit </asp:LinkButton>
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

