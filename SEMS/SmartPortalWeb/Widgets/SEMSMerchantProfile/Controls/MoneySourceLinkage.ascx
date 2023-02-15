<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MoneySourceLinkage.ascx.cs" Inherits="Widgets_SEMSConsumerProfile_Controls_MoneySourceLinkage" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<div class="panel-container" id="BigpnMoney">
    <div class="panel-content form-horizontal p-b-0">
        <div class="collapse in" id="collapseExample3">
              <div class="card card-body">
                 <div class="panel-content form-horizontal p-b-0" style="display: block;">
                <asp:Panel ID="pnCard" runat="server">
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
                                            <asp:DropDownList ID="ddlStatus" style="width:100%!important" CssClass="form-control select2" Enabled="false" runat="server">
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
                                            <asp:DropDownList ID="ddlKyclevel" style="width:100%!important" CssClass="form-control select2" Enabled="false" runat="server">
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
                                            <asp:DropDownList ID="ddWalletLevel" style="width:100%!important" CssClass="form-control select2" Enabled="false" runat="server"></asp:DropDownList>
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
        <div style="text-align:right">
              <a class="btn" data-toggle="collapse" href="#collapseExample3" role="button" aria-expanded="false" aria-controls="collapseExample3">
                <em class="fa fa-angle-up"></em>
              </a>
            </div>
        
                     <asp:Panel ScrollBars="Auto" runat="server">
                    <%--<div style="overflow-x:hidden; overflow-y:scroll;">--%>
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
                                      
                                                </tr>
                                            </thead>
                                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                        
                                    <td class="tr-boder"><%#Eval("AcctNo") %></t>
                                    <td class="tr-boder"><%#Eval("BankAcctype") %></td>
                                    <td class="tr-boder"><%#Eval("BankId") %></td>
                                    <td class="tr-boder"><%#Eval("BANKNAME")%></td>
                                    <td class="tr-boder"><%#Resources.labels.None %></td>
                                    <td class="tr-boder"><%#Resources.labels.None%></td>
                        
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
                          <asp:UpdatePanel ID="UpdatePanelGrid" UpdateMode="Always" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <uc1:GridViewPaging ID="GridViewPaging" runat="server"></uc1:GridViewPaging>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="GridViewPaging" />
            </Triggers>
        </asp:UpdatePanel>
                 
           <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                <asp:Button ID="btSave" CssClass="btn btn-primary" Visible="false" runat="server" Text="<%$ Resources:labels, save %>" OnClick="btSave_Click" />
                <asp:Button ID="btnCancel" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, cancel %>" OnClick="btnCancel_Click" />
            </div>
    </div>
</div>
