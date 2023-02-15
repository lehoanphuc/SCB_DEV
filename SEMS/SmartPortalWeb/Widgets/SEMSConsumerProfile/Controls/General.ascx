<%@ Control Language="C#" AutoEventWireup="true" CodeFile="General.ascx.cs" Inherits="Widgets_SEMSConsumerProfile_Controls_General" %>
<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>
<div class="panel-container" id="BigPannelGeneral">
    <div class="panel-content form-horizontal p-b-0">
        <asp:Panel ID="pnGeneral" runat="server">
            
            <div class="collapse in" id="collapseExample">
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
                                                <asp:TextBox ID="txtDateCreate" CssClass="form-control" Enabled="false" placeholder="DD/MM/YYYY" runat="server"></asp:TextBox>
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
                                                <asp:TextBox ID="txtLastModifiedate" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
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
                    <hr />
                    </div>
                </div>
            </div>
            <div style="text-align: right">
                <a class="btn" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">
                    <em class="fa fa-angle-up"></em>
                </a>
            </div>

            <div class="row">
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="col-sm-4 control-label"><%=Resources.labels.fullname %></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtFullName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-1"></div>
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="col-sm-4 control-label"><%=Resources.labels.fisrtname %></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="col-sm-4 control-label"><%=Resources.labels.middlename %></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtMiddleName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-1"></div>
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="col-sm-4 control-label"><%=Resources.labels.lastname %></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtLastname" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="col-sm-4 control-label"><%=Resources.labels.PaperType %></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlPaperType" Style="width: 100%!important" CssClass="form-control select2" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-1"></div>
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="col-sm-4 control-label"><%=Resources.labels.PaperNumber %></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtPaperNumber" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="col-sm-4 control-label"><%=Resources.labels.IssueDate %></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtIssuedate" CssClass="form-control datetimepicker " placeholder="DD/MM/YYYY" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-1"></div>
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="col-sm-4 control-label"><%=Resources.labels.ExpiryDate %></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtExpirydate" CssClass="form-control datetimepicker " placeholder="DD/MM/YYYY" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="col-sm-4 control-label"><%=Resources.labels.birthday %></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtBirsthday" CssClass="form-control datetimepicker " placeholder="DD/MM/YYYY" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-1"></div>
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="col-sm-4 control-label"><%=Resources.labels.gender %></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlGender" Style="width: 100%!important" CssClass="form-control select2" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="col-sm-4 control-label"><%=Resources.labels.nationality %></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlNationality" Style="width: 100%!important" CssClass="form-control select2" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-1"></div>
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="col-sm-4 control-label"><%=Resources.labels.address %></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtAddress" CssClass="form-control " runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="col-sm-4 control-label"><%=Resources.labels.email %></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtEmail" CssClass="form-control " runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-1">
                </div>
                <div class="col-sm-5">
                </div>
            </div>

        </asp:Panel>
    </div>
    <div style="margin-top: 10px; text-align: center;">
        &nbsp;<asp:Button ID="btSaveGeneral" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, save %>" OnClick="btSave_Click" />
         &nbsp;<asp:Button ID="btnClear" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, clear %>" OnClick="btnClear_Click" />
        &nbsp;<asp:Button ID="btnBackGeneral" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, back %>" OnClick="btnBack_Click" />
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