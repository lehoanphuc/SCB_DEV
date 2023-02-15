<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Document.ascx.cs" Inherits="Widgets_SEMSConsumerProfile_Controls_Document" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Register Src="~/Controls/PreviewImage/PreviewImage.ascx" TagPrefix="uc1" TagName="PreviewImage" %>
<%@ Register Src="~/Controls/SearchTextBox/Bank.ascx" TagPrefix="uc1" TagName="Bank" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<script>

    function openWindow() {
        //Change your pagename here and also the width and height as per your desgin
        window.open('page.aspx', 'open_window', ' width=150, height=250, left=0, top=0');
    }
</script>
<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>
<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" RenderMode="Block" runat="server">
    <ContentTemplate>
        <div class="panel-container" id="BigPnDocument">
            <div class="panel-content form-horizontal p-b-0">

                <div class="collapse in" id="collapseExample1">
                    <div class="card card-body">
                        <div class="panel-content form-horizontal p-b-0" style="display: block;">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <div class="image" style="text-align: center">
                                            <img src="http://placehold.it/380x500" alt="" class="img-rounded img-responsive" />
                                        </div>
                                    </div>
                                    <div class="col-sm-10">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label required"><%=Resources.labels.MerchantCode %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtMerchantCode" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
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
                                                    <label class="col-sm-4 control-label required"><%=Resources.labels.KycLevel %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlKyclevel" Style="width: 100%!important" CssClass="form-control select2" Enabled="false" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.ApprovedBy %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtApproveBy" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label required"><%=Resources.labels.WalletLevel %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddWalletLevel" Style="width: 100%!important" CssClass="form-control select2" Enabled="false" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div style="text-align: right">
                    <a class="btn" data-toggle="collapse" href="#collapseExample1" role="button" aria-expanded="false" aria-controls="collapseExample1">
                        <em class="fa fa-angle-up"></em>
                    </a>
                </div>

                <asp:Panel ScrollBars="Auto" runat="server">
                    <%--<div style="overflow-x:hidden; overflow-y:scroll;">--%>
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
                                                <th class="title-repeater"><%=Resources.labels.edit%></th>
                                                <th class="title-repeater"><%=Resources.labels.Download%></th>
                                                <th class="title-repeater"><%=Resources.labels.delete%></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="tr-boder"><%#Eval("DocumentCode") %></td>
                                <td class="tr-boder"><%#Eval("DocumentName") %></td>
                                <td class="tr-boder"><%#Eval("DateCreated") %></td>
                                <td class="tr-boder"><%#Eval("UserCreated")%></td>
                                <td class="tr-boder">
                                    <%#Eval("Status") %>
                                    <label id="lbStatusDocument" visible="false" runat="server"><%#Eval("ValueStatus") %></label>
                                </td>
                                <td class="tr-boder item-center">
                                    <%--<asp:LinkButton ID="lbtnViewFile" runat="server" class="btn btn-info" CommandArgument='<%#Eval("FILE") %>' CommandName='<%#IPC.ACTIONPAGE.DETAILS %>'> <%=Resources.labels.view%>
                            </asp:LinkButton>--%>
                                    <asp:UpdatePanel ID="updatepanel" runat="server">
                                        <ContentTemplate>
                                            <%--<uc1:PreviewImage ID="PreviewImage" runat="server"></uc1:PreviewImage>--%>
                                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#Modal<%#Eval("No") %>"><%=Resources.labels.edit%></button>
                                            <asp:Panel id="pannelModal" runat="server" DefaultButton="btnOK">
                                            <!-- The Modal -->
                                            <div class="modal" id="Modal<%#Eval("No") %>">
                                                <div class="modal-dialog">
                                                    <div class="modal-content">

                                                        <!-- Modal Header -->
                                                        <div class="modal-header">
                                                            <h4 class="modal-title" style="text-align:left!important">Edit Document</h4>
                                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                        </div>

                                                        <!-- Modal body -->
                                                        <div class="divlog" style="color:red">
                                                            <label id="lblErrorPopup" runat="server"></label>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="panel-container">
                                                                <div class="panel-content form-horizontal p-b-0">
                                                                <asp:TextBox ID="txtNo" Visible="false" Text='<%#Eval("No") %>' runat="server"></asp:TextBox>
                                                                <asp:TextBox ID="txtDocname" Visible="false" Text='<%#Eval("DocumentName") %>' runat="server"></asp:TextBox>
                                                                <div class="form-group">
                                                                    <label class="control-label required" style="float: left"><%=Resources.labels.DocumentName%></label>
                                                                    <asp:TextBox ID="txtDocumentName" MaxLength="250" Text='<%#Eval("DocumentName") %>' runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <asp:Image ID="Image1" runat="server" src='<%#  "data:image/jpg;base64," + Eval("FILE") %>' Style="width: 100%; max-width: 500px; height: auto;" Height="400" Width="500" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- Modal footer -->
                                                        <div class="modal-footer" style="text-align:center!important">
                                                            <asp:Button runat="server" class="btn btn-primary" data-check="itemBranch111" ID="btnOK" OnClick="btnOK_Click" data-close="<%=BankCode.ClientID%>" Text='<%$Resources:labels,ok %>' />
                                                            <button type="button" class="btn btn-secondary" data-dismiss="modal"><%=Resources.labels.cancel %></button>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnOK" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="tr-boder item-center">
                                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton runat="server" class="btn btn-info" BackColor="DodgerBlue" ID="txtdown_file1" CommandArgument='<%#Eval("DocumentCode") + "---" + Eval("File") %>' CommandName='<%#IPC.ACTIONPAGE.EXPORT %>'><i class="fa fa-download"></i> Download</asp:LinkButton>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="txtdown_file1" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="tr-boder item-center">
                                    <asp:LinkButton ID="lbtnDelete" runat="server" class="btn btn-secondary" CommandArgument='<%#Eval("DocumentID")%>' CommandName='<%#IPC.ACTIONPAGE.DELETE%>' OnClientClick="return ConfirmDelete('Are you sure you want to delete this entry?');"> <%=Resources.labels.delete%> </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                        </table>
                        </div> </div>
                        </FooterTemplate>
                    </asp:Repeater>
                    <div class="button" style="text-align: right;padding-bottom:10px">
                        <asp:FileUpload ID="documentUpload" runat="server" accept=".PNG, .png, .jpg, .JPG, .JPEG, .jpeg, .BMP, .bmp, .PDF, .pdf, .webp, .WEBP" Width="348px" Height="27px" />
                        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnImport" type="button" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, ImportFile %>" autopostback="false" OnClick="btnImportFile_Click" />
                                <asp:Button ID="btnDowloadAll" type="button" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, DownloadAllFile %>" autopostback="false" OnClick="btnDownloadAll_Click" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnDowloadAll" />
                                <asp:PostBackTrigger ControlID="btnImport" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                    <asp:HiddenField runat="server" ID="hdCLMS_SCO_SCO_PRODUCT" />
                    <%--</div>--%>
                </asp:Panel>

                <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                    <asp:Button ID="btSaveGeneral" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClick="btSave_Click" />
                    <asp:Button ID="btnBackGeneral" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, cancel %>" OnClick="btnBack_Click" />
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btSaveGeneral" />
    </Triggers>
</asp:UpdatePanel>
<script>
    
</script>
