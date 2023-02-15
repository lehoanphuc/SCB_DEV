<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Document.ascx.cs" Inherits="Widgets_SEMSConsumerProfile_Controls_Document" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>
<div class="panel-container" id="BigPnDocument">
    <div class="panel-content form-horizontal p-b-0">
        <asp:Panel ID="pnDocument" runat="server">
            <div class="collapse in" id="collapseExample1">
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
                <a class="btn" data-toggle="collapse" href="#collapseExample1" role="button" aria-expanded="false" aria-controls="collapseExample1">
                    <em class="fa fa-angle-up"></em>
                </a>
            </div>

        </asp:Panel>

    </div>

    <%--<asp:Panel ID="Panel1" runat="server">
        <div class="row" style="margin: 2%">
            <div class="col-sm-6">
                <div class="form-group">
                    <label class="col-sm-6 control-label"><%=Resources.labels.NRCfrontside + " | Orther"%></label>

                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-group">
                    <label class="col-sm-6 control-label"><%=Resources.labels.NRCbackside%></label>

                </div>
            </div>
        </div>
        <div class="row" style="margin: 2%">
            <div class="col-sm-6">
                <div class="form-group">
                    <div class="col-sm-8">
                        <asp:Image ID="imgFrontSide" runat="server" style="width: 100%; max-width: 350px; height: auto;" Height="300"  Width="350" data-toggle="modal" data-target="#myModal1" />
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-group">
                    <div class="col-sm-8">
                        <asp:Image ID="imgBackSide" runat="server" style="width: 100%; max-width: 350px; height: auto;" Height="300"  Width="350" data-toggle="modal" data-target="#myModal2" />
                    </div>
                </div>
            </div>

        </div>
    </asp:Panel>--%>
    <asp:Panel ScrollBars="Auto" runat="server">
        <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_OnItemDataBound" OnItemCommand="rptData_ItemCommand">
            <HeaderTemplate>
                <div class="pane">
                    <div class="table-responsive">
                        <table class="table table-hover footable c_list">
                            <thead class="thead-light repeater-table">
                                <tr>

                                    <th class="title-repeater"><%=Resources.labels.DocumentCode%></th>
                                    <th class="title-repeater"><%=Resources.labels.DocumentName%></th>
                                    <th class="title-repeater"><%=Resources.labels.UploadDate%></th>
                                    <th class="title-repeater"><%=Resources.labels.CreatedBy%></th>
                                    <th class="title-repeater"><%=Resources.labels.status%></th>
                                    <th class="title-repeater"><%=Resources.labels.Action%></th>
                                </tr>
                            </thead>
                            <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class="tr-boder"><%#Eval("DOCUMENTCODE") %></td>
                    <td class="tr-boder"><%#Eval("DOCUMENTNAME") %></td>
                    <td class="tr-boder"><%#Eval("DateCreateFormat") %></td>
                    <td class="tr-boder"><%#Eval("USERCREATED")%></td>
                    <td class="tr-boder"><%#Eval("StatusCaption") %></td>

                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                        <ContentTemplate>
                            <td class="action tr-boder" style="text-align: center">
                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#Modal<%#Eval("DOCUMENTID") %>"><%=Resources.labels.view%></button>
                                <div id="Modal<%#Eval("DOCUMENTID") %>" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                    <div class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h4 class="modal-title"><%=Resources.labels.image %></h4>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">×</span></button>
                                            </div>
                                            <div class="modal-body">
                                                <asp:Image ID="imgDocument" src='<%# "data:image/jpg;base64," + Eval("FILE") %>' runat="server" class="img-responsive" />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:LinkButton runat="server" ID="txtdown_file1" class="btn btn-primary" CommandArgument='<%#Eval("DOCUMENTCODE") + "---" + Eval("FILE") %>' CommandName='<%#IPC.ACTIONPAGE.EXPORT %>'>Download</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lbtnDeleteFile" class="btn btn-secondary" CommandArgument='<%#Eval("DOCUMENTID")%>' CommandName='<%#IPC.ACTIONPAGE.DELETE%>' OnClientClick="return ConfirmDelete('Are you sure you want to delete this entry?');">Delete </asp:LinkButton>
                            </td>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="txtdown_file1" />
                            <asp:PostBackTrigger ControlID="lbtnDeleteFile" />
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
    <asp:UpdatePanel ID="UpdatePanelHongNT" UpdateMode="Always" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <uc1:GridViewPaging ID="GridViewPaging" runat="server"></uc1:GridViewPaging>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GridViewPaging" />
        </Triggers>
    </asp:UpdatePanel>


    <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
        &nbsp;
            <asp:FileUpload ID="documentUpload" runat="server" accept=".png,.jpg,.jpeg,.gif" Width="348px" Height="27px" />

        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server" RenderMode="Inline">
            <ContentTemplate>
                &nbsp;<asp:Button ID="btnImportFile" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, Importfile %>" OnClick="btnImportFile_Click" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnImportFile" />
            </Triggers>
        </asp:UpdatePanel>
        &nbsp;<asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, back %>" OnClick="btnBack_Click" />

        <asp:Button ID="btnSearch" CssClass="btn btn-secondary" Style="display: none" runat="server" Text="<%$ Resources:labels, search %>" OnClick="btnSearch_Click" />
        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server" RenderMode="Inline">
            <ContentTemplate>
                &nbsp;<asp:Button runat="server" ID="txtdown_file2" CssClass="btn btn-secondary" Text="<%$ Resources:labels, DownloadAllFile %>" OnClick="btnDownloadAll_Click1" />

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="txtdown_file2" />
            </Triggers>
        </asp:UpdatePanel>
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
