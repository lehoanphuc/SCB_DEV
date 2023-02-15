<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MoneySourceLinkage.ascx.cs" Inherits="Widgets_SEMSConsumerProfile_Controls_MoneySourceLinkage" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<div class="panel-container" id="BigpnMoney">
    <div class="panel-content form-horizontal p-b-0">

        <asp:Panel ID="pnCard" runat="server">
            <div class="collapse in" id="collapseExample3">
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
                <a class="btn" data-toggle="collapse" href="#collapseExample3" role="button" aria-expanded="false" aria-controls="collapseExample3">
                    <em class="fa fa-angle-up"></em>
                </a>
            </div>
        </asp:Panel>


    </div>

    <asp:Panel ScrollBars="Auto" runat="server">
        <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
            <HeaderTemplate>
                <div class="pane">
                    <div class="table-responsive">
                        <table class="table table-hover footable c_list">
                            <thead class="thead-light repeater-table">
                                <tr>
                                    <th class="title-repeater"><%=Resources.labels.MoneySourceNumber%></th>
                                    <th class="title-repeater"><%=Resources.labels.MoneySourceType%></th>
                                    <th class="title-repeater"><%=Resources.labels.BankCode%></th>
                                    <th class="title-repeater"><%=Resources.labels.BankName%></th>
                                    <th class="title-repeater"><%=Resources.labels.EffectiveDate%></th>
                                    <th class="title-repeater"><%=Resources.labels.ExpiryDate%></th>
                                    <th class="title-repeater"><%=Resources.labels.Default%></th>

                                </tr>
                            </thead>
                            <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>

                    <td class="tr-boder"><%#Eval("AcctNo") %></td>
                    <td class="tr-boder"><%#Eval("CAPTION") %></td>
                    <td class="tr-boder"><%#Eval("BANKID") %></td>
                    <td class="tr-boder"><%#Eval("BANKNAME")%></td>
                    <td class="tr-boder"><%#Resources.labels.None %></td>
                    <td class="tr-boder"><%#Resources.labels.None%></td>
                    <td class="tr-boder"><%#Resources.labels.None %></td>

                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                        </table>
                        </div> </div>
            </FooterTemplate>
        </asp:Repeater>
        <asp:HiddenField runat="server" ID="hdCLMS_SCO_SCO_PRODUCT" />

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
        &nbsp;<asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, back %>" OnClick="btnBack_Click" />
          &nbsp;<asp:Button ID="btnSearch"  CssClass="btn btn-primary" Style="display: none" runat="server" Text="<%$ Resources:labels, search %>" OnClick="btnSearch_Click" />
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