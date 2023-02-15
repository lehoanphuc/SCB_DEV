<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSKYCMerchantProfile_Register_Widget" %>
<%@ Register Src="~/Controls/SearchTextBox/Bank.ascx" TagPrefix="uc1" TagName="Bank" %>
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

        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div id="divSearch">
            <div class="subheader">
                <asp:Label class="subheader-title" runat="server" id="lbTitle">
                </asp:Label>
            </div>
            <div class="panel-container">
            <div class="panel-container form-horizontal p-b-0">
                <asp:Panel ID="panel" runat="server">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.MerchantCode %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtMerchantCode" CssClass="form-control" Enabled="false" IsRequired="true" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.PhoneNumber %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" MaxLength="50" IsRequired="true" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                 <label class="col-sm-4 control-label required"><%=Resources.labels.MerchantName %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtMerchantName" CssClass="form-control" MaxLength="255" IsRequired="true" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.PaperType %></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddPaperType" CssClass="form-control select2" IsRequired="true" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.PaperNumber %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtPaperNumber" CssClass="form-control" MaxLength="50" IsRequired="true" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label"><%=Resources.labels.IssueDate %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtIssueDate" CssClass="form-control datetimepicker" placeholder="DD/MM/YYY" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label"><%=Resources.labels.ExpiryDate %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtExpiryDate" CssClass="form-control datetimepicker" placeholder="DD/MM/YYY" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label"><%=Resources.labels.nationality %></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddNationality" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.address %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtAddress" CssClass="form-control" TextMode="MultiLine" onkeyup="ValidateLimit(this,'300');" onkeyDown="ValidateLimit(this,'300');" onpaste="ValidateLimit(this,'300');" onChange="ValidateLimit(this,'300');" onmousedown="ValidateLimit(this,'300');" IsRequired="true" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.email %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtEmail" CssClass="form-control email-valid" placeholder="example@gmail.com" MaxLength="100" IsRequired="true" onblur="return CheckEmailFormat(this);" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.KycLevel %></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddKYCLevel" CssClass="form-control select2" IsRequired="true" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.WalletLevel %></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddWalletLevel" CssClass="form-control select2" Enabled="false" IsRequired="true" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.LinkToMoneySourceType%></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddMoneySourceType" CssClass="form-control select2" IsRequired="true" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.BankName %></label>
                                <div class="col-sm-8">
                                    <uc1:Bank ID="txtBankName" class="Pretextbox" runat="server"></uc1:Bank>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.MoneySourceNumber %></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtMoneySourceNumber" CssClass="form-control" IsRequired="true" MaxLength="50" OnPreRender="loadDataMoneySource" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Label id ="lbCurrency" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label"><%=Resources.labels.EffectiveDate %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtEffectiveDate" CssClass="form-control datetimepicker" placeholder="DD/MM/YYY" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label"><%=Resources.labels.ExpiryDate %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtExpiryDate2" CssClass="form-control datetimepicker" placeholder="DD/MM/YYY" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                </div>
                </div>
                <%-- Table View --%>
                <asp:Panel ScrollBars="Auto" runat="server">
                <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                    <HeaderTemplate>
                    <div class="pane">
                        <div class="table-responsive">
                            <table class="table table-hover footable c_list">
                                <thead class="thead-light repeater-table">
                                    <tr>
                                        <th class="td-no-action title-repeater"></th>
                                        <th class="title-repeater"><%=Resources.labels.DocumentName%></th>
                                        <th class="td-no-action title-repeater"><%=Resources.labels.edit%></th>
                                        <th class="td-no-action title-repeater"><%=Resources.labels.delete%></th>
                                    </tr>
                                </thead>
                                <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <tr>
                        <td class="tr-boder item-center"><%#Eval("No") %></td>
                        <td class="tr-boder"><%#Eval("Documentname") %></td>
                        <td class="tr-boder item-center">
                            <asp:LinkButton ID="lbtnDocument" runat="server" CssClass="btn btn-primary" CommandArgument='<%#Eval("No")%>'  CommandName='<%#IPC.ACTIONPAGE.EDIT%>' > <%=Resources.labels.edit%>
                            </asp:LinkButton>
                            <asp:UpdatePanel id="updatepanel4" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox Visible="false" ID="txtImage" runat="server" Width="50%" ReadOnly="true"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hdID" />
                                            <button type="button" visible="false" runat="server" id="btnPopup" class="search-popup">
                                            </button>
                               <asp:Panel runat="server" class="modal fade" ID="Image" data-backdrop="static" role="dialog">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class="modal-title" style="text-align:left!important">Edit Document</h4>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">×</span></button>
                                        </div>
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:Panel id="pannelModal" runat="server" DefaultButton="btnOK">
                                                <div class="divlog" style="color: red">
                                                    <label id="lblErrorPopup" runat="server"></label>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="view-image">
                                                        <div class="form-group">
                                                        <label class="control-label required" style="float: left"><%=Resources.labels.DocumentName%></label>
                                                        <asp:TextBox ID="txtDocumentName" MaxLength="250" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="view-image" style="text-align:center" overflow: scroll;>
                                                            <asp:Image ID="ImageView" runat="server" style="width: 100%; height: auto;"/>
                                                        </div>
                                                    </div>
                                              </div>
                                            <div class="modal-footer" style="text-align:center!important">
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
                        <td class="td-no-action tr-boder">
                            <asp:LinkButton runat="server" class="btn btn-secondary" CommandArgument='<%#Eval("No")%>'  CommandName='<%#IPC.ACTIONPAGE.DELETE%>' > <%=Resources.labels.delete%> </asp:LinkButton>
                        </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                        </div> </div>
                    </FooterTemplate>
                </asp:Repeater>
                <%--</div>--%>
            </asp:Panel>
            <div class="panel-content" style="text-align:right">
                <%--<uc1:ImportImage ID="ImportImage" runat="server"></uc1:ImportImage>--%>
                <asp:FileUpload ID="documentUpload" accept=".PNG, .png, .jpg, .JPG, .JPEG, .jpeg, .BMP, .bmp, .PDF, .pdf, .webp, .WEBP" runat="server" Width="348px" Height="27px"/>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                    <Triggers>

                    <asp:PostBackTrigger ControlID="btnImport" />

                    </Triggers>
                    <ContentTemplate>
                        <asp:Button ID="btnImport" type="button" CssClass="btn btn-primary" runat="server" autopostback="false" Text="<%$ Resources:labels, ImportFile %>"  OnClick="btnImport_click"/>
                    </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, Submit %>" OnClick="btnSubmit_click" />
                    <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_click" />
                    <asp:Button ID="btnCancel" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, cancel %>" OnClick="btnCancel_click" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script src="/JS/Common.js"></script>