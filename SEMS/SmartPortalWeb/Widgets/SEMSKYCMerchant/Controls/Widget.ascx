<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSKYCMerchant_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Register Src="~/Controls/PreviewImage/PreviewImage.ascx" TagPrefix="uc1" TagName="PreviewImage" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<script>
    function openWindow() {
        //Change your pagename here and also the width and height as per your desgin
        window.open('page.aspx', 'open_window', ' width=150, height=250, left=0, top=0');
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleBranch" runat="server"></asp:Label>
            </h1>
        </div>
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
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.KYCMerchantInformation%>
                        </h2>
                    </div>
                    <asp:Panel ID="pnCustCode" runat="server" DefaultButton="btnSearch">
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="row" style="margin-left: 2%; margin-right: 2%">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.makhachhang %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtCustCode" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, xemchitiet %>" OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnCustInfo" runat="server">
                                <div class="row" style="margin-left: 2%; margin-right: 2%">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.PhoneNumber %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" OnTextChanged="LoadDataMerchantByPhone" AutoPostBack="true" IsRequired="true" runat="server"></asp:TextBox>
                                                <asp:HiddenField ID="hdfUserID" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.MerchantName %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtMerchantName" CssClass="form-control" IsRequired="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%; margin-right: 2%">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.IssueDate%></label>
                                            <div class="col-sm-8">

                                                <asp:TextBox ID="txtIssueDate" CssClass="form-control datetimepicker" IsRequired="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.ExpiryDate%></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtExpiryDate" CssClass="form-control datetimepicker" IsRequired="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%; margin-right: 2%">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.nationality%></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddNationality" CssClass="form-control select2" IsRequired="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.PaperNumber%></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPaperNumber" CssClass="form-control" IsRequired="true" runat="server" onchange=" onchage_papernum();"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%; margin-right: 2%">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.email%></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtEmail" CssClass="form-control email-valid" placeholder="example@gmail.com" MaxLength="100" IsRequired="true" onblur="return CheckEmailFormat(this);" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.KycLevel%></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlKycType" CssClass="form-control select2" OnPreRender="loadKycType" OnSelectedIndexChanged="loadKycType" AutoPostBack="true" IsRequired="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%; margin-right: 2%">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.WalletLevel%></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddWalletLevel" CssClass="form-control" Enabled="False" IsRequired="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.status%></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddStatus" CssClass="form-control select2" IsRequired="true" runat="server" Enabled="false"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.custcode %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtCustCodeInfo" CssClass="form-control " MaxLength="20" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%; margin-right: 2%">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.birthday %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtBirthday" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.gender %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlGender" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.address%></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtAddress" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnImportNewDocument">
                                <div class="panel-container">
                                    <div class="panel-content form-horizontal p-b-0">
                                        <asp:Panel runat="server" GroupingText="Identity Card">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.PaperType%></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtPaperTypeImport" MaxLength="18" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.PaperNumber%></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtPaperNumberImport" placeholder="PP number" MaxLength="18" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.DocumentName%></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtDocumentNameImport" Text="PP Front" placeholder="Document name" MaxLength="250" runat="server" CssClass="form-control"></asp:TextBox>
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
                                        </asp:Panel>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnDocumentBusiness" runat="server">
                                <div class="panel-container">
                                    <div class="panel-content form-horizontal p-b-0">
                                        <asp:Panel runat="server">
                                            <div class="row">
                                                <div class="col-sm-2" runat="server">
                                                    <div class="form-group custom-control">
                                                        <asp:RadioButton ID="radAgreement" runat="server" GroupName="GETINFO"
                                                            Text="Agreement Contract" AutoPostBack="true" Checked="true" OnPreRender="radAgreement_OnCheckedChanged" OnCheckedChanged="radAgreement_OnCheckedChanged" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-2" runat="server">
                                                    <div class="form-group custom-control">
                                                        <asp:RadioButton ID="radBusiness" runat="server" GroupName="GETINFO"
                                                            Text="Business Document" AutoPostBack="true" OnPreRender="radBusiness_OnCheckedChanged" OnCheckedChanged="radBusiness_OnCheckedChanged" />
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnAgreement" runat="server" GroupingText="Agreement Contract">
                                            <div class="row">
                                                <div class="Agreement">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.DocumentName%></label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtAgreementNameImport" Text="Agreement Contract" placeholder="Agreement name" MaxLength="250" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnBusiness" runat="server" GroupingText="Business Document">
                                            <div class="row">
                                                <div class="Business">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.DocumentName%></label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtBusinessNameImport" Text="Business Document" placeholder="Business name" MaxLength="250" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:FileUpload ID="FileUploadMerchantDocumet" runat="server" accept=".PNG, .png, .jpg, .JPG, .JPEG, .jpeg, .BMP, .bmp" Width="348px" Height="27px" />
                                        <div class="button" style="text-align: right; padding-bottom: 10px">
                                            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnImportMerchantDocument" type="button" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, ImportFile %>" autopostback="false" OnClick="btnMerchantDocumentImportFile_Click" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnImportMerchantDocument" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnDocument" ScrollBars="Auto" runat="server">
                                <asp:Panel ID="rpt_DocumentList" runat="server">
                                    <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_OnItemDataBound" OnItemCommand="rptData_ItemCommand">
                                        <HeaderTemplate>
                                            <div class="pane">
                                                <div class="table-responsive">
                                                    <table id="document" class="table table-hover footable c_list">
                                                        <thead class="thead-light repeater-table">
                                                            <tr>
                                                                <th class="td-no-action title-repeater"></th>
                                                                <th class="title-repeater"><%=Resources.labels.DocumentName%></th>
                                                                <th class="title-repeater"><%=Resources.labels.status%></th>
                                                                <th class="td-no-action title-repeater"><%=Resources.labels.edit%></th>
                                                                <th class="td-no-action title-repeater"><%=Resources.labels.delete%></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="tr-boder item-center"><%#Eval("NO") %></td>
                                                <td class="tr-boder"><%#Eval("DocumentName") %></td>
                                                <td class="tr-boder"><%# GetStatus(Eval("Status")) %></td>
                                                <td class="td-no-action tr-boder">
                                                    <asp:LinkButton ID="lbtnViewFile" runat="server" CommandArgument='<%#Eval("NO") %>' CommandName='<%#IPC.ACTIONPAGE.EDIT %>'>
                                                        <asp:Image ID="ImageView" Style="max-width: 150px" runat="server" src='<%# "data:image/jpg;base64," + Eval("File") %>' data-toggle="tooltip" title="Show image" />
                                                    </asp:LinkButton>
                                                    <asp:UpdatePanel ID="updatepanel4" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox Visible="false" ID="txtImage" runat="server" Width="50%" ReadOnly="true"></asp:TextBox>
                                                            <asp:HiddenField runat="server" ID="hdID" />
                                                            <button type="button" visible="false" runat="server" id="btnPopup" class="search-popup">
                                                            </button>
                                                            <asp:Panel runat="server" class="modal" ID="Image" data-backdrop="static" role="dialog">
                                                                <div class="modal-dialog">
                                                                    <div class="modal-content">
                                                                        <div class="modal-header">
                                                                            <h4 class="modal-title" style="text-align: left!important">Edit Document</h4>
                                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="accordion()">
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
                                                                                                        <asp:FileUpload ID="fileUpdate" runat="server" accept=".PNG, .png, .jpg, .JPG, .JPEG, .jpeg, .BMP, .bmp, .PDF, .pdf, .webp, .WEBP" Width="348px" Height="27px" />
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
                                                                                        <asp:Button runat="server" class="btn btn-primary" data-check="itemBranch111" ID="btnOK" OnClick="btnOK_Click" Text='<%$Resources:labels,ok %>' />
                                                                                        <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="accordion()"><%=Resources.labels.cancel %></button>
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
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" class="btn btn-secondary" data-toggle="tooltip" title="Delete document" CommandArgument='<%# Eval("No") +"|"+ Eval("DocumentID")%>' CommandName='<%#IPC.ACTIONPAGE.DELETE%>' OnClientClick="return ConfirmDelete('Are you sure you want to delete this entry?');"> <%=Resources.labels.delete%> </asp:LinkButton>
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
                            </asp:Panel>
                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                <asp:Button ID="btnApprove" Visible="False" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, approve %>" OnClick="btnApprove_Click" />
                                <asp:Button ID="btnReject" Visible="False" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, reject %>" OnClick="btnReject_Click" />
                                <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClick="btnSave_Click" />
                                <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_click" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script src="/JS/Common.js"></script>

<script>
    function CheckLenght() {
        if (!(document.getElementById("<%=txtPaperNumber.ClientID %>").value.length >= 8 && document.getElementById("<%=txtPaperNumber.ClientID %>").value.length <= 18)) {
            alert('<%=Resources.labels.nrcmustcontainonlylettersnumbersandspecialcharactersandbebetween%>');
            document.getElementById('<%=txtPaperNumber.ClientID %>').focus();
            return false;
        }
        if ((document.getElementById("<%=txtPaperNumberImport.ClientID %>").value.length >= 8 && document.getElementById("<%=txtPaperNumberImport.ClientID %>").value.length <= 18)) {

        }
        else {
            alert('<%=Resources.labels.nrcmustcontainonlylettersnumbersandspecialcharactersandbebetween%>');
            document.getElementById('<%=txtPaperNumberImport.ClientID %>').focus();
            return false;
        }
    }
    function onchage_papernum() {
        var select = document.getElementById('<%=ddlKycType.ClientID %>');
        var string = select.options[select.selectedIndex].value;
        if (string != null) {
            var textbox = document.getElementById('<%=txtPaperNumberImport.ClientID %>');
            var textbox2 = document.getElementById('<%=txtPaperNumber.ClientID %>');
            textbox.value = textbox2.value;
        }
    }
    function onchage_papernumimport() {
        var select = document.getElementById('<%=ddlKycType.ClientID %>');
        var string = select.options[select.selectedIndex].value;

        var textbox = document.getElementById('<%=txtPaperNumberImport.ClientID %>');
        var textbox2 = document.getElementById('<%=txtPaperNumber.ClientID %>');
        textbox2.value = textbox.value;

    }
</script>
