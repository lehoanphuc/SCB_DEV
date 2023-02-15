<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSKYCMerchant_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
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
        <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
            <div id="divSearch">
                <div class="subheader">
                    <h1 class="subheader-title">
                        <%=Resources.labels.kycConsumer %>
                    </h1>
                </div>
                <div class="panel-container form-horizontal p-b-0">
                    <div class="search_box">
                        <div class="row">
                            <div class="container">
                                <div class="form-group">
                                    <div class="col-sm-1"></div>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" Visible ="false"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, search %>" OnClick="btnSearch_Click" Visible="false"/>
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
                                   <%-- <em class="fa fa-angle-up"></em>--%>
                                   <%-- <span id="toggleDetail" data-toggle="collapse" href="#AdvanceSearch" role="button" aria-expanded="false" aria-controls="collapseExample">
                                        <label for="collapsible" class="lbl-toggle"><%=Resources.labels.AdvanceSearch%></label>
                                        <span style="display: block;" class="downarrowclass"></span>
                                    </span>--%>
                                    <div class="collapse in" id="AdvanceSearch">
                                        <div class="panel-content form-horizontal p-b-0" style="display: block;">
                                            <div class="row">
                                                <div class="col-sm-5">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.PhoneNumber %></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-5">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.KycLevel %></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddKycLevel" CssClass="form-control select2" Style="width: 100%" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.PaperNumber %></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtPaperNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-5">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.status %></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddStatus" CssClass="form-control select2" Style="width: 100%" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.fullname %></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtMerchantName" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-5">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.custcode %></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtCustcodeSearch" CssClass="form-control" runat="server"></asp:TextBox>
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
        </asp:Panel>
        <div id="divToolbar">
            <asp:Button ID="btnAdd_New" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" />
        </div>
        <%-- Table View --%>
        <asp:Panel runat="server" ID="pnResult" Visible="false">
            <div id="divResult" style="overflow-x: hidden;">
                <asp:Panel ScrollBars="Auto" runat="server">
                    <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_OnItemDataBound" OnItemCommand="rptData_ItemCommand">
                        <HeaderTemplate>
                            <div class="table-responsive">
                                <table class="table table-bordered" style="border: 1px solid rgba(0, 0, 0, 0.1); border-style: Solid;">
                                    <thead style="background-color: #7A58BF; color: #FFF;">
                                        <tr>
                                            <th class="title-repeater"><%=Resources.labels.RequestNo%></th>
                                            <th class="title-repeater"><%=Resources.labels.PhoneNumber%></th>
                                            <th class="title-repeater"><%=Resources.labels.KycLevel%></th>
                                            <th class="title-repeater"><%=Resources.labels.PaperNumber%></th>
                                            <th class="title-repeater"><%=Resources.labels.fullname%></th>
                                            <th class="title-repeater"><%=Resources.labels.custcode%></th>
                                            <th class="title-repeater"><%=Resources.labels.ApproveOrRejectStatus%></th>
                                            <th class="title-repeater"><%=Resources.labels.DateCreate%></th>
                                            <th class="title-repeater"><%=Resources.labels.edit%></th>
                                            <th class="title-repeater"><%=Resources.labels.approve%></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="action tr-boder" style="text-align: center;">
                                    <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#Eval("RequestID")%>' CommandName='<%#IPC.ACTIONPAGE.DETAILS%>'><%#Eval("REQUESTID")%></asp:LinkButton>
                                </td>
                                <td class="tr-boder"><%#Eval("PHONE") %></td>
                                <td class="tr-boder"><%#Eval("KYCNAME")%></td>
                                <td class="tr-boder"><%#Eval("PAPERNO") %></td>
                                <td class="tr-boder"><%#Eval("FULLNAME") %></td>
                                <td class="tr-boder"><%#Eval("CUSTCODE")%></td>
                                <td class="tr-boder"><%#Eval("APPROVE_STATUS") %></td>
                                <td class="tr-boder"><%#Eval("DATECREATED") %></td>
                                <td class="action tr-boder" style="text-align: center;">
                                    <asp:LinkButton ID="lbtnEdit" runat="server" class="btn btn-primary" CommandArgument='<%#Eval("RequestID")%>' CommandName='<%#IPC.ACTIONPAGE.EDIT%>'> <%=Resources.labels.edit%> </asp:LinkButton>
                                </td>
                                <td class="action tr-boder" style="text-align: center;">
                                    <asp:LinkButton ID="lbtnApprove" runat="server" class="btn btn-primary" CommandArgument='<%#Eval("RequestID")%>' CommandName='<%#IPC.ACTIONPAGE.APPROVE%>'> <%=Resources.labels.approve%> </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                        </table>
                        </div> 
                        </FooterTemplate>
                    </asp:Repeater>
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
