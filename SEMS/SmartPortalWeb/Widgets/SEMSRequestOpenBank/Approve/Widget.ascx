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
                        <h2>User Information
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnRegion" Enabled="false" runat="server">
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label ">Contract No</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtContractNo" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label ">Customer Type</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtCustomerType" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label ">Phone</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.fullname %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtFullName" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label ">Currency</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtccyid" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label ">Account Type</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtAccountType" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.PaperType %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlPaperType" Visible="false" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlKycLevel" CssClass="form-control select2" OnTextChanged="OnTextChanged_KYCLevel" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.PaperNumber %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPaperNumber" CssClass="form-control " runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%" runat="server" visible="false">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.IssueDate %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtIssueDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
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
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.birthday %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtBirthday" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
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
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.nationality %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlNationality" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.address %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Branch</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtbranchName" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">City</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtCityName" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <%--<label class="col-sm-4 control-label required"><%=Resources.labels.Kycname %></label>--%>
                                            <label class="col-sm-4 control-label "><%=Resources.labels.WalletLevel %></label>
                                            <div class="col-sm-8">
                                                <%--<asp:DropDownList ID="ddlKycLevel" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>--%>
                                                <asp:DropDownList ID="ddlWalletLevel" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <%--<label class="col-sm-4 control-label required"><%=Resources.labels.WalletLevel %></label>--%>
                                            <label class="col-sm-4 control-label "><%=Resources.labels.status %></label>
                                            <div class="col-sm-8">
                                                <%--<asp:DropDownList ID="ddlWalletLevel" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>--%>
                                                <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <%--<label class="col-sm-4 control-label required"><%=Resources.labels.status %></label>--%>
                                            <div class="col-sm-8">
                                                <%--<asp:DropDownList ID="ddlStatus" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%" runat="server" id="create">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.createddate %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtcreatedate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
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
                                <div class="row" style="margin-left: 2%" runat="server" id="Div1">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.noidung %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ScrollBars="Auto" runat="server">
                                <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand1">
                                    <HeaderTemplate>
                                        <div class="pane">
                                            <div class="table-responsive">
                                                <table class="table table-hover footable c_list">
                                                    <thead class="thead-light repeater-table">
                                                        <tr>
                                                            <th class="title-repeater"></th>
                                                            <th class="title-repeater"><%=Resources.labels.DocumentName%></th>
                                                            <th class="title-repeater"><%=Resources.labels.view%></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tr-boder item-center"><%#Eval("NO") %></td>
                                            <td class="tr-boder"><%#Eval("DocumentName") %></td>
                                            <td class="td-no-action tr-boder">
                                                <asp:LinkButton ID="lbtnViewFile" runat="server" CommandArgument='<%#Eval("FILE") %>' CommandName='<%#IPC.ACTIONPAGE.REVIEW %>'>
                                                    <asp:Image ID="ImageView" Style="max-width: 150px" runat="server" src='<%#Eval("FILE") %>' data-toggle="tooltip" title="Show image" />
                                                </asp:LinkButton>
                                                <asp:UpdatePanel ID="updatepanel2" runat="server">
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
                                    </FooterTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                            <div class="divbutton" style="padding-top: 10px">
                                <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                    <asp:Button ID="btnApprove" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, approve %>" OnClick="btnApprove_Click" />
                                    <asp:Button ID="btReject" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, conreject %>" OnClick="btReject_Click" />
                                    <asp:Button ID="btBack" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btBack_Click" />
                                </div>
                            </div>

                        </div>


                    </div>
                </div>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<script src="/JS/Common.js"></script>
