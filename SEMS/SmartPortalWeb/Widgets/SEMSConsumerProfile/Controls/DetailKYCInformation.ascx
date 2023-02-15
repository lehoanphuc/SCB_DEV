<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DetailKYCInformation.ascx.cs" Inherits="Widgets_SEMSConsumerProfile_Controls_DetailKYCInformation" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>

<div id="myModal3" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title"><%=Resources.labels.image %></h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span></button>
            </div>
            <div class="modal-body">
                <div class="view-img" style="text-align: -webkit-center; width: 100%; height: auto">
                <asp:Image ID="ImageFrontModel3" runat="server" class="img-responsive" />
                </div>
            </div>
        </div>
    </div>
</div>
<div id="myModal4" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title"><%=Resources.labels.image %></h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span></button>
            </div>
            <div class="modal-body">
                <div class="view-img" style="text-align: -webkit-center; width: 100%; height: auto">
                <asp:Image ID="ImageBackModel4" runat="server" class="img-responsive" />
                    </div>
            </div>
        </div>
    </div>
</div>
<div id="myModal5" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title"><%=Resources.labels.image %></h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span></button>
            </div>
            <div class="modal-body">
                <div class="view-img" style="text-align: -webkit-center; width: 100%; height: auto">
                <asp:Image ID="ImageSelfieModel5" runat="server" class="img-responsive" />
                </div>
            </div>
        </div>
    </div>
</div>
<div class="panel-container" id="bigpnDetail">
    <div class="panel-content form-horizontal p-b-0">
        <asp:Panel ID="pnCard" runat="server">
            <div class="collapse in" id="collapseExample2">
                <div class="card card-body">
                    <div class="panel-content form-horizontal p-b-0" style="display: block;">
                        <div class="row">

                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.ConsumerCode %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtConsumerCode" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.DateCreate %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDateCreate" CssClass="form-control" Enabled="false" placeholder="DD/MM/YYY" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.PhoneNumber %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.LastModifiedDate %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtLastModifiedate" CssClass="form-control datetimepicker" Enabled="false" placeholder="DD/MM/YYY" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.status %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlStatus" Style="width: 100%!important" CssClass="form-control select2" Enabled="false" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.CreatedBy %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtCreateBy" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.ApprovedBy %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtApproveBy" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.WalletLevel %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlWalletLevel" Style="width: 100%!important" CssClass="form-control select2" Enabled="false" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
            </div>
            <div style="text-align: right">
                <a class="btn" data-toggle="collapse" href="#collapseExample2" role="button" aria-expanded="false" aria-controls="collapseExample2">
                    <em class="fa fa-angle-up"></em>
                </a>
            </div>
        </asp:Panel>

    </div>

    <asp:Panel ID="pnViewImg" Visible="false" runat="server">
        <table class="table table-hover footable c_list">
            <thead class="thead-light repeater-table">
                <tr>
                    <th>
                        <label class="control-label"><%=Resources.labels.NRCfrontside%></label></th>
                    <th>
                        <label class="control-label"><%=Resources.labels.NRCbackside%></label></th>
                    <th>
                        <label class="control-label">Selfie with NRC</label></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:Image ID="imgFrontSide" runat="server" Style="width: 100%; max-width: 300px; height: auto;" Height="200" Width="300" data-toggle="modal" data-target="#myModal3" />
                    </td>
                    <td>
                        <asp:Image ID="imgBackSide" runat="server" Style="width: 100%; max-width: 300px; height: auto;" Height="200" Width="300" data-toggle="modal" data-target="#myModal4" />
                    </td>
                    <td>
                        <asp:Image ID="imgSelfie" runat="server" Style="width: 100%; max-width: 300px; height: auto;" Height="200" Width="300" data-toggle="modal" data-target="#myModal3" />
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="row" style="margin: 2%">
        </div>
    </asp:Panel>
    <asp:Panel ScrollBars="Auto" runat="server">
        <%--<div style="overflow-x:hidden; overflow-y:scroll;">--%>
        <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
            <HeaderTemplate>
                <div class="pane">
                    <div class="table-responsive">
                        <table class="table table-hover footable c_list">
                            <thead class="thead-light repeater-table">
                                <tr>

                                    <th class="title-repeater"><%=Resources.labels.RequestNo%></th>
                                    <th class="title-repeater"><%=Resources.labels.PaperNumber%></th>
                                    <th class="title-repeater"><%=Resources.labels.PhoneNumber%></th>
                                    <th class="title-repeater"><%=Resources.labels.status%></th>
                                    <th class="title-repeater"><%=Resources.labels.DateCreate%></th>
                                    <th class="title-repeater"><%=Resources.labels.CreatedBy%></th>
                                    <th class="title-repeater"><%=Resources.labels.ApprovedBy%></th>
                                    <th class="title-repeater"><%=Resources.labels.Action%></th>
                                </tr>
                            </thead>
                            <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class="tr-boder"><%#Eval("RequestID") %></td>
                    <td class="tr-boder"><%#Eval("PaperNO") %></td>
                    <td class="tr-boder"><%#Eval("PHONE") %></td>
                    <td class="tr-boder"><%#Eval("StatusCaption")%></td>
                    <td class="tr-boder"><%#Eval("DateCreateFormat") %></td>
                    <td class="tr-boder"><%#Eval("UserCreated")%></td>
                    <td class="tr-boder"><%#Eval("UserApproved") %></td>

                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                        <ContentTemplate>
                            <td class="action tr-boder" style="text-align: center;">
                                <asp:LinkButton ID="linkeViewImageKYC" runat="server" class="btn btn-primary" CommandArgument='<%#Eval("RequestID") %>' CommandName='<%#IPC.ACTIONPAGE.DETAILS%>'>View Image </span> </asp:LinkButton>
                            </td>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="linkeViewImageKYC" />
                        </Triggers>
                    </asp:UpdatePanel>
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
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanelGridView" UpdateMode="Always" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <uc1:GridViewPaging ID="GridViewPaging" runat="server"></uc1:GridViewPaging>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GridViewPaging" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
        &nbsp;<asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, back %>" OnClick="btnBack_Click" />
        &nbsp;<asp:Button ID="btnSearch" Style="display: none" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, search %>" OnClick="btnSearch_Click" />
    </div>

</div>
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
