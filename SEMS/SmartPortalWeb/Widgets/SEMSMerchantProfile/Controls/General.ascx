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
                <div class="col-sm-2">
                    <div class="image" style="text-align:center">
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
                                    <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" Enabled="false" runat="server">
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
                                    <asp:DropDownList ID="ddlKyclevel" CssClass="form-control select2" Enabled="false" runat="server">
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
                                    <asp:DropDownList ID="ddWalletLevel" CssClass="form-control select2" Enabled="false" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                        </div>
                    </div>


                 </div>
            </div>
        </div>
              </div>
            </div>

            <div style="text-align:right">
              <a class="btn" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">
                <em class="fa fa-angle-up"></em>
              </a>
            </div>
                    <div class="row">
                        <div class="col-sm-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.MerchantCode %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtMerchantCode_Collapse" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-1"></div>
                        <div class="col-sm-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.PhoneNumber %></label>
                                <div class="col-sm-8">
                                        <asp:TextBox ID="txtPhoneNumber_Collapse" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.MerchantName %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtMerchantName_Collapse" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-1"></div>
                        <div class="col-sm-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.PaperType %></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddPaperType_Collapse" style="width:100%!important" Enabled="false" CssClass="form-control select2" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.PaperNumber %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtlPaperNumber_Collapse" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-1"></div>
                        <div class="col-sm-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label"><%=Resources.labels.IssueDate %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtIssueDate_Collapse" CssClass="form-control datetimepicker" placeholder="DD/MM/YYY" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label"><%=Resources.labels.ExpiryDate %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtExpiryDate_Collapse" CssClass="form-control datetimepicker " placeholder="DD/MM/YYY" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-1"></div>
                        <div class="col-sm-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label"><%=Resources.labels.nationality %></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddNationality_Collapse" style="width:100%!important" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label required"><%=Resources.labels.address %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtAddress_Collapse"  style="width:100%" CssClass="form-control" TextMode="MultiLine" onkeyup="ValidateLimit(this,'300');" onkeyDown="ValidateLimit(this,'300');" onpaste="ValidateLimit(this,'300');" onChange="ValidateLimit(this,'300');" onmousedown="ValidateLimit(this,'300');" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-1"></div>
                        <div class="col-sm-5">
                            <div class="form-group">
                                <label class="col-sm-4 control-label"><%=Resources.labels.email %></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtemail_Collapse" CssClass="form-control email-valid" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                


        </asp:Panel>
    </div>
    <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
        <asp:Button ID="btSaveGeneral" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClick="btSave_Click" />
        <asp:Button ID="btnBackGeneral" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, cancel %>" OnClick="btnBack_Click" />
    </div>

</div>
<script src="/JS/Common.js"></script>
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