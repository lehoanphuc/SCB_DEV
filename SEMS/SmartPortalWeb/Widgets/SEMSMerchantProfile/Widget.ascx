<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSKYCMerchant_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<script>
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel DefaultButton="btnSearch" runat="server">
            <div id="divError">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </div>

            <div id="divSearch">
                <div class="subheader">
                    <h1 class="subheader-title">
                        <%=Resources.labels.MerchantProfile %>
                    </h1>
                </div>
                <div id="divButton" style="padding-bottom: 10px">
                    <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, Register %>" OnClick="btnRegister_Click" />
                </div>
                <asp:Panel ID="panel" runat="server" DefaultButton="btnSearch">
                    <div class="panel-container form-horizontal p-b-0">
                        <div class="search_box">
                            <div class="row">
                                <div class="container">
                                    <div class="form-group">
                                        <div class="col-sm-1"></div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtSearch" CssClass="form-control" MaxLength="250" runat="server"></asp:TextBox>
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
                                            <%--<span style="display: block;" class="downarrowclass"></span>--%>
                                        </span>
                                        <%--    <span id="collapsible" data-target="#SearchAdvence" data-toggle="collapse">
                                      
                                    </span>--%>
                                        <div class="collapse in" id="AdvanceSearch">
                                            <div class="panel-content form-horizontal p-b-0" style="display: block;">
                                                <div class="row">
                                                    <div class="col-sm-5">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label"><%=Resources.labels.PhoneNumber %></label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtPhoneNumber" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2"></div>
                                                    <div class="col-sm-5">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label"><%=Resources.labels.nationality %></label>
                                                            <div class="col-sm-8">
                                                                <asp:DropDownList ID="ddNationality" Style="width: 100% !important" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-5">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label"><%=Resources.labels.MerchantName %></label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtMerchantName" MaxLength="255" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2"></div>
                                                    <div class="col-sm-5">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label"><%=Resources.labels.status %></label>
                                                            <div class="col-sm-8">
                                                                <asp:DropDownList ID="ddStatus" Style="width: 100% !important" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-5">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label"><%=Resources.labels.PaperNumber %></label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtPaperNumber" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2"></div>
                                                    <div class="col-sm-5">
                                                        <div class="form-group">
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
                </asp:Panel>
            </div>
            <asp:Panel ID="pnResult" Visible="false" runat="server">
            <div class="divResult">
                <%-- Table View --%>
                <asp:Panel ScrollBars="Auto" runat="server">
                    <asp:Literal ID="litError" runat="server"></asp:Literal>
                    <%--<div style="overflow-x:hidden; overflow-y:scroll;">--%>
                    <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_OnItemDataBound" OnItemCommand="rptData_ItemCommand">
                        <HeaderTemplate>
                            <div class="pane">
                                <div class="table-responsive" <%--style="overflow-x:hidden!important"--%>>
                                    <table class="table table-hover footable c_list">
                                        <thead class="thead-light repeater-table">
                                            <tr>
                                                <th class="title-repeater"><%=Resources.labels.PhoneNumber%></th>
                                                <th class="title-repeater"><%=Resources.labels.MerchantCode%></th>
                                                <th class="title-repeater"><%=Resources.labels.MerchantName%></th>
                                                <th class="title-repeater"><%=Resources.labels.PaperNumber%></th>
                                                <th class="title-repeater"><%=Resources.labels.nationality%></th>
                                                <th class="title-repeater"><%=Resources.labels.status%></th>
                                                <th class="title-repeater"><%=Resources.labels.DateCreate%></th>
                                                <th class="title-repeater"><%=Resources.labels.edit%></th>
                                                <th class="title-repeater"><%=Resources.labels.approve%></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <%--<td class="tdcheck">
                            <input name='<%="item"+UserCode.ClientID%>' class="check keepvalue" value="<%#Eval("UserCode") %>" data-text="<%#Eval("UserCode") %>" onclick="ConfigRatio(this)" type="radio">
                        </td>--%>
                                <td class="tr-boder">
                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("MERCHANT_CODE")%>' CommandName='<%#IPC.ACTIONPAGE.DETAILS%>'><%#Eval("PHONE_NUMBER") %></asp:LinkButton>
                                </td>
                                <td class="tr-boder"><%#Eval("MERCHANT_CODE") %></td>
                                <td class="tr-boder"><%#Eval("MERCHANT_NAME") %></td>
                                <td class="tr-boder"><%#Eval("PAPER_NUMBER")%></td>
                                <td class="tr-boder"><%#Eval("NATION") %></td>
                                <td class="tr-boder">
                                    <label id="lbStatus" runat="server"><%#Eval("STATUS") %></label>
                                </td>
                                <td class="tr-boder"><%#Eval("DATECREATED") %></td>
                                <td class="action tr-boder item-center">
                                    <asp:LinkButton ID="btnlinkEdit" runat="server" class="btn btn-primary" CommandArgument='<%#Eval("MERCHANT_CODE")%>' CommandName='<%#IPC.ACTIONPAGE.EDIT%>'><%=Resources.labels.edit%></asp:LinkButton>
                                </td>
                                <td class="action tr-boder item-center">
                                    <asp:LinkButton ID="btnlinkApprove" runat="server" class="btn btn-secondary" CommandArgument='<%#Eval("MERCHANT_CODE")%>' CommandName='<%#IPC.ACTIONPAGE.APPROVE%>'><%=Resources.labels.approve%></asp:LinkButton>
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
                    <%--</div>--%>
                </asp:Panel>
                <uc1:GridViewPaging ID="GridViewPaging" runat="server"></uc1:GridViewPaging>
            </div>
        </asp:Panel>
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
