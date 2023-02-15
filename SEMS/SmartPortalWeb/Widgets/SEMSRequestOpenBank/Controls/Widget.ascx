<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSREGIONFEE_Controls_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Register Src="~/Controls/PreviewImage/PreviewImage.ascx" TagPrefix="uc1" TagName="PreviewImage" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleBranch" runat="server"></asp:Label>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
          <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.KYCConsumerInformation%>
                        </h2>
                    </div>
                    <asp:Panel ID="pnMain" runat="server" DefaultButton="btnSave">
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <asp:Panel ID="pnRegion" runat="server">
                                    <div class="row" style="margin-left: 2%; margin-right: 2%">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.PhoneNumber %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" OnTextChanged="LoadDataConsumerByPhone" AutoPostBack="true" runat="server" MaxLength="50"></asp:TextBox>
                                                    <asp:HiddenField ID="hdfUserID" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.fullname %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtFullName" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-left: 2%; margin-right: 2%">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.PaperType %></label>
                                                <div class="col-sm-8">
                                                    <%--<asp:DropDownList ID="ddlPaperType" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>--%>
                                                    <asp:DropDownList ID="ddlKycLevel" CssClass="form-control select2" OnPreRender="loadKYCLevel" OnSelectedIndexChanged="loadKYCLevel" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.PaperNumber %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtPaperNumber" onchange="onchage_papernum(); CheckLenght();" CssClass="form-control " MaxLength="20" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-left: 2%; margin-right: 2%">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.IssueDate %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtIssueDate" onblur="onchage_issuedate();" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.ExpiryDate %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtExpireDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-left: 2%; margin-right: 2%">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.birthday %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtBirthday" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.gender %></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlGender" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-left: 2%; margin-right: 2%">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.nationality %></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlNationality" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.address %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtAddress" TextMode="MultiLine" CssClass="form-control" onkeyup="ValidateLimit(this,'250');" onkeyDown="ValidateLimit(this,'250');" onpaste="ValidateLimit(this,'250');" onChange="ValidateLimit(this,'250');" onmousedown="ValidateLimit(this,'250');" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-left: 2%; margin-right: 2%">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.WalletLevel %></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlWalletLevel" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <asp:Label id="lbStatusKYC" runat="server" class="col-sm-4 control-label required"><%=Resources.labels.status %></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-left: 2%; margin-right: 2%">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <div class="col-sm-8">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-7">
                                        </div>
                                    </div>


                                    <div class="row" style="margin-left: 2%; margin-right: 2%" runat="server" id="create">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.createddate %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtcreatedate"  CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.nguoithuchien %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtCreateby" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnDocument" ScrollBars="Auto" runat="server">
                                    <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_OnItemDataBound" OnItemCommand="rptData_ItemCommand1">
                                        <HeaderTemplate>
                                            <div class="pane">
                                                <div class="table-responsive">
                                                    <table class="table table-hover footable c_list">
                                                        <thead class="thead-light repeater-table">
                                                            <tr>
                                                                <th class="title-repeater"></th>
                                                                <th class="title-repeater"><%=Resources.labels.DocumentName%></th>
                                                                <th class="title-repeater"><%=Resources.labels.edit%></th>
                                                                <th class="title-repeater"><%=Resources.labels.delete%></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="tr-boder item-center"><%#Eval("NO") %></td>
                                                <td class="tr-boder"><%#Eval("DocumentName") %></td>
                                                <td class="td-no-action tr-boder">
                                                    <asp:LinkButton ID="lbtnViewFile" runat="server" CommandArgument='<%#Eval("NO") %>' CommandName='<%#IPC.ACTIONPAGE.EDIT %>'>
                                                        <asp:Image ID="ImageView" Style="max-width: 150px" runat="server" src='<%# "data:image/jpg;base64," + Eval("FILE") %>' data-toggle="tooltip" title="Show image" />
                                                    </asp:LinkButton>
                                                    <asp:UpdatePanel ID="updatepanel4" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox Visible="false" ID="txtImage" runat="server" Width="50%" ReadOnly="true"></asp:TextBox>
                                                            <asp:HiddenField runat="server" ID="hdID" />
                                                            <button type="button" visible="false" runat="server" id="btnPopup" class="search-popup">
                                                            </button>
                                                            <asp:Panel runat="server" class="modal fade" ID="Image" data-backdrop="static" role="dialog">
                                                                <div class="modal-dialog">
                                                                    <div class="modal-content">
                                                                        <div class="modal-header">
                                                                            <h4 class="modal-title" style="text-align: left!important">Edit Document</h4>
                                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                                <span aria-hidden="true">×</span></button>
                                                                        </div>
                                                                        <asp:UpdatePanel runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:Panel ID="pannelModal" runat="server" DefaultButton="btnOK">
                                                                                    <div class="divlog" style="color: red">
                                                                                        <label id="lblErrorPopup" runat="server"></label>
                                                                                    </div>
                                                                                    <div class="modal-body" style="overflow-y: auto; width: 100%; height: auto;">
                                                                                        <div class="panel-container">
                                                                                            <div class="panel-content form-horizontal p-b-0">
                                                                                                <div class="view-image">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.DocumentName%></label>
                                                                                                        <div class="col-sm-8">
                                                                                                            <asp:TextBox ID="txtDocumentName" MaxLength="250" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.DocumentType%></label>
                                                                                                        <div class="col-sm-8">
                                                                                                            <asp:DropDownList ID="ddlDocumentType" MaxLength="250" runat="server" CssClass="form-control">
                                                                                                            </asp:DropDownList>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="view-image" style="text-align: center; width: 100%; height: auto">
                                                                                                        <asp:Image ID="Image1" runat="server" Style="width: 100%; height: auto;" />
                                                                                                    </div>
                                                                                                    <div class="button" style="text-align: right; padding-bottom: 10px">
                                                                                                        <%--<asp:FileUpload ID="fileUpdate" runat="server" accept=".PNG, .png, .jpg, .JPG, .JPEG, .jpeg, .BMP, .bmp, .PDF, .pdf, .webp, .WEBP" Width="348px" Height="27px" />--%>
                                                                                                        <asp:FileUpload ID="fileUpdate" runat="server" accept=".PNG, .png, .jpg, .JPG, .JPEG, .jpeg, .BMP, .bmp" Width="348px" Height="27px" />
                                                                                                        <asp:UpdatePanel ID="UpdatePanelUpdate" UpdateMode="Always" runat="server">
                                                                                                            <ContentTemplate>
                                                                                                                <asp:Button ID="btnImportUpdate" type="button" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, ImportFile %>" autopostback="false" OnClick="btnImportFileUpdate_Click" />
                                                                                                            </ContentTemplate>
                                                                                                            <Triggers>
                                                                                                                <asp:PostBackTrigger ControlID="btnImportUpdate" />
                                                                                                            </Triggers>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="modal-footer" style="text-align: center!important">
                                                                                        <asp:Button runat="server" class="btn btn-primary" data-check="itemBranch111" ID="btnOK" OnClick="btnOK_Click" data-close="<%=BankCode.ClientID%>" Text='<%$Resources:labels,ok %>' />
                                                                                        <button type="button" class="btn btn-secondary" data-dismiss="modal"><%=Resources.labels.cancel %></button>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td class="tr-boder item-center" style="width: 10%">
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" class="btn btn-secondary" data-toggle="tooltip" title="Delete document" CommandArgument='<%#Eval("No")+ "|" +Eval("DocumentID")%>' CommandName='<%#IPC.ACTIONPAGE.DELETE%>' OnClientClick="return ConfirmDelete('Are you sure you want to delete this entry?');"> <%=Resources.labels.delete%> </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                        </table>
                        </div> </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </asp:Panel>

                                <asp:Panel runat="server" ID="pnImportNewDocument">
                                    <div class="panel-container">
                                        <div class="panel-content form-horizontal p-b-0">
                                            <asp:Panel ID="PnPassport" runat="server" GroupingText="Passport">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label required"><%=Resources.labels.DocumentName%></label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtPassportName" Text="Passport" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label required"><%=Resources.labels.nationality%></label>
                                                            <div class="col-sm-8">
                                                                <asp:DropDownList ID="ddlContryPassport" CssClass="form-control select2" runat="server" Width="100%">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label required"><%=Resources.labels.PassportNumber%></label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtPassPortNo" CssClass="form-control" onchange="onchage_papernumimport();" Width="100%" placeholder="Passport No" MaxLength="20" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label"><%=Resources.labels.Datetimepassport%></label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtDatetimePassport" onblur="onchage_issuedateimport();" Width="100%" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnNewNRIC" runat="server" GroupingText="National Registration Identity Card">
                                                <div class="row">
                                                    <div class="NRIC">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.DocumentName%></label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtDocumentNameImport" placeholder="Document name" MaxLength="250" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.NRICNumber%></label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtPaperNumberImport" placeholder="NRIC number" onchange="onchage_papernumimport(); CheckLenght();" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label" style="float: left"><%=Resources.labels.IssueDate%></label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtIssueDateImport" onblur="onchage_issuedateimport();" MaxLength="250" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.DocumentType%></label>
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList ID="ddlDocumentTypeImport" MaxLength="250" Width="100%" runat="server" OnTextChanged="DocumentTypeImport_OnTextChanged" AutoPostBack="true" CssClass="form-control select2">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnLicense" runat="server" GroupingText="License">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.DocumentName%></label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtLicenseName" Text="License" placeholder="License name" MaxLength="250" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label required">License Number</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtLicenseNumber" CssClass="form-control" onchange="onchage_papernumimport();" placeholder="License number" runat="server" MaxLength="20"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:FileUpload ID="documentUpload" runat="server" accept=".PNG, .png, .jpg, .JPG, .JPEG, .jpeg, .BMP, .bmp" Width="348px" Height="27px" />
                                            <div class="button" style="text-align: right; padding-bottom: 10px">
                                                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnImport" type="button" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, ImportFile %>" autopostback="false" OnClick="btnImportFile_Click" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnImport" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>

                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="divbutton" style="padding-top: 10px">
                                    <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_click" />
                                        <asp:Button ID="btBack" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btBack_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
<script>
    function CheckLenght() {
        if (!(document.getElementById("<%=txtPaperNumber.ClientID %>").value.length >= 17 && document.getElementById("<%=txtPaperNumber.ClientID %>").value.length <= 20) ||
            !(document.getElementById("<%=txtPaperNumber.ClientID %>").value.length >= 10 && document.getElementById("<%=txtPaperNumber.ClientID %>").value.length <= 12)) {

        }
        else {
            alert('<%=Resources.labels.nrcmustcontainonlylettersnumbersandspecialcharactersandbebetween%>');
            document.getElementById('<%=txtPaperNumber.ClientID %>').focus();
            return false;
        }
        if ((document.getElementById("<%=txtPaperNumberImport.ClientID %>").value.length >= 17 && document.getElementById("<%=txtPaperNumberImport.ClientID %>").value.length <= 20) ||
            (document.getElementById("<%=txtPaperNumberImport.ClientID %>").value.length >= 10 && document.getElementById("<%=txtPaperNumberImport.ClientID %>").value.length <= 12)) {
         
        }
        else {
            alert('<%=Resources.labels.nrcmustcontainonlylettersnumbersandspecialcharactersandbebetween%>');
            document.getElementById('<%=txtPaperNumberImport.ClientID %>').focus();
            return false;
        }
    }
    function onchage_papernum() {
        var select = document.getElementById('<%=ddlKycLevel.ClientID %>');
        var string = select.options[select.selectedIndex].value;
        if (string == 'NRIC') {
            var textbox = document.getElementById('<%=txtPaperNumberImport.ClientID %>');
            var textbox2 = document.getElementById('<%=txtPaperNumber.ClientID %>');
            textbox.value = textbox2.value;
        }
        if (string == 'PASSPORT') {
            var textbox = document.getElementById('<%=txtPassPortNo.ClientID %>');
            var textbox2 = document.getElementById('<%=txtPaperNumber.ClientID %>');
            textbox.value = textbox2.value;
        }
        if (string == 'LICENSE') {
            var textbox = document.getElementById('<%=txtLicenseNumber.ClientID %>');
            var textbox2 = document.getElementById('<%=txtPaperNumber.ClientID %>');
            textbox.value = textbox2.value;
        }
        
    }

    function onchage_papernumimport() {
        var select = document.getElementById('<%=ddlKycLevel.ClientID %>');
        var string = select.options[select.selectedIndex].value;
        if (string == 'NRIC') {
            var textbox = document.getElementById('<%=txtPaperNumberImport.ClientID %>');
            var textbox2 = document.getElementById('<%=txtPaperNumber.ClientID %>');
            textbox2.value = textbox.value;
        }
        if (string == 'PASSPORT') {
            var textbox = document.getElementById('<%=txtPassPortNo.ClientID %>');
            var textbox2 = document.getElementById('<%=txtPaperNumber.ClientID %>');
            textbox2.value = textbox.value;
        }
        if (string == 'LICENSE') {
            var textbox = document.getElementById('<%=txtLicenseNumber.ClientID %>');
            var textbox2 = document.getElementById('<%=txtPaperNumber.ClientID %>');
            textbox2.value = textbox.value;
        }
    }

    function onchage_issuedate() {
        var select = document.getElementById('<%=ddlKycLevel.ClientID %>');
        var string = select.options[select.selectedIndex].value;
        if (string == 'NRIC') {
            var textbox = document.getElementById('<%=txtIssueDateImport.ClientID %>');
            var textbox2 = document.getElementById('<%=txtIssueDate.ClientID %>');
            textbox.value = textbox2.value;
        }
        if (string == 'PASSPORT') {
            var textbox = document.getElementById('<%=txtDatetimePassport.ClientID %>');
            var textbox2 = document.getElementById('<%=txtIssueDate.ClientID %>');
            textbox.value = textbox2.value;
        }
        if (string == 'LICENSE') {
            <%--var textbox = document.getElementById('<%=txtLicenseNumber.ClientID %>');
            var textbox2 = document.getElementById('<%=txtIssueDate.ClientID %>');
            textbox.value = textbox2.value;--%>
        }
        
    }

    function onchage_issuedateimport() {
        var select = document.getElementById('<%=ddlKycLevel.ClientID %>');
        var string = select.options[select.selectedIndex].value;
        if (string == 'NRIC') {
            var textbox = document.getElementById('<%=txtIssueDateImport.ClientID %>');
            var textbox2 = document.getElementById('<%=txtIssueDate.ClientID %>');
            textbox2.value = textbox.value;
        }
        if (string == 'PASSPORT') {
            var textbox = document.getElementById('<%=txtDatetimePassport.ClientID %>');
            var textbox2 = document.getElementById('<%=txtIssueDate.ClientID %>');
            textbox2.value = textbox.value;
        }
        if (string == 'LICENSE') {
            <%--var textbox = document.getElementById('<%=txtLicenseNumber.ClientID %>');
            var textbox2 = document.getElementById('<%=txtIssueDate.ClientID %>');
            textbox2.value = textbox.value;--%>
        }
    }

</script>

<script src="/JS/Common.js"></script>