<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSKYCMerchantProfile_ApproveOrReject_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
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
<asp:UpdatePanel ID="UpdatePanel1"  UpdateMode="Conditional" RenderMode="Block" runat="server">
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
                                    <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" Enabled="false" IsRequired="true" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.MerchantName %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtMerchantName" CssClass="form-control" Enabled="false" IsRequired="true" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.PaperType %></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddPaperType" CssClass="form-control select2" Enabled="false" IsRequired="true" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.PaperNumber %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtPaperNumber" CssClass="form-control" Enabled="false" IsRequired="true" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label"><%=Resources.labels.IssueDate %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtIssueDate" CssClass="form-control datetimepicker" Enabled="false" placeholder="DD/MM/YYY" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label"><%=Resources.labels.ExpiryDate %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtExpiryDate" CssClass="form-control datetimepicker" Enabled="false" placeholder="DD/MM/YYY" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label"><%=Resources.labels.nationality %></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddNationality" CssClass="form-control select2" Enabled="false" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.address %></label>
                                <div class="col-sm-8">
                                    <textarea ID="txtAddress" CssClass="textarea-control" style="width:100%" disabled="disabled" IsRequired="true" Enabled="false" runat="server"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.email %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtEmail" CssClass="form-control email-valid" Enabled="false" placeholder="example@gmail.com" IsRequired="true" onblur="return CheckEmailFormat(this);" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label"><%=Resources.labels.KycLevel %></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddKYCLevel" CssClass="form-control select2" Enabled="false" IsRequired="true" runat="server"></asp:DropDownList>
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
                                    <asp:DropDownList ID="ddMoneySourceType" CssClass="form-control select2" Enabled="false" IsRequired="true" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.BankName %></label>
                                <div class="col-sm-8">
                                    <uc1:Bank ID="txtBankName" runat="server"></uc1:Bank>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.MoneySourceNumber %></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtMoneySourceNumber" CssClass="form-control" Enabled="false" IsRequired="true" OnPreRender="loadDataMoneySource" OnTextChanged="loadDataMoneySource" runat="server"></asp:TextBox>
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
                                <label class="col-sm-4 control-label"><%=Resources.labels.DateCreate %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtCreateDate" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-4 control-label"><%=Resources.labels.CreatedBy %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtCreateBy" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            
                        </div>
                    </div>
                </asp:Panel>
                </div>
                </div>
            
                <%-- Table View --%>
                <asp:Panel ScrollBars="Auto" runat="server">
                <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound" OnItemCommand="rptData_ItemCommand">
                    <HeaderTemplate>
                    <div class="pane">
                        <div class="table-responsive">
                            <table class="table table-hover footable c_list">
                                <thead class="thead-light repeater-table">
                                    <tr>
                                        <th class="td-no-action title-repeater"></th>
                                        <th class="title-repeater"><%=Resources.labels.DocumentName%></th>
                                        <th class="td-no-action title-repeater"><%=Resources.labels.view%></th>
                                    </tr>
                                </thead>
                                <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <tr>
                        <td class="td-no-action tr-boder"><%#Eval("No") %></td>
                        <td class="tr-boder"><%#Eval("DocumentName") %></td>
                        <td class="td-no-action action tr-boder">
                            <asp:LinkButton ID="lbtnViewFile" runat="server" class="btn btn-info" CommandArgument='<%#Eval("FILE") %>' CommandName='<%#IPC.ACTIONPAGE.DETAILS %>'> <%=Resources.labels.view%> </span>
                            </asp:LinkButton>
                            <asp:UpdatePanel id="updatepanel2" runat="server">
                                <ContentTemplate>
                                    <uc1:PreviewImage ID="PreviewImage" runat="server"></uc1:PreviewImage>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, approve %>" OnClick="btnApprove_click" />
                    <asp:Button ID="btnReject" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Reject %>" OnClick="btnReject_click" />
                    <asp:Button ID="btnCancel" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, cancel %>" OnClick="btnCancel_click" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
