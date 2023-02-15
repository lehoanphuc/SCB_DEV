<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSREGION_Controls_Widget" %>
<%@ Register src="~/Controls/GirdViewPaging/GridViewPaging.ascx" tagName="GridViewPaging" tagPrefix="control"%>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleBranch" runat="server"/>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"/>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%= Resources.labels.thongtinvungphi %>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnRegion" runat="server">
                                <div class="row">
                                    <div class="form-group col-xs-12">
                                        <label hidden="" class="col-sm-2 col-xs-12 col-sm-offset-2 control-label"><%= Resources.labels.mavungphi %> </label>
                                        <div class="col-sm-4 col-xs-12">
                                            <asp:TextBox ID="txRegionID" Enabled="False" Visible="False" CssClass="form-control" runat="server"/>
                                        </div>
                                        <div class="col-sm-4 col-xs-12"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-xs-12">
                                        <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label required"><%= Resources.labels.tenmavungphi %></label>
                                        <div class="col-sm-4 col-xs-12">
                                            <asp:TextBox ID="txRegionName" CssClass="form-control" runat="server" MaxLength="200" />
                                        </div>
                                        <div class="col-sm-4 col-xs-12"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-xs-12">
                                        <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label required"><%= Resources.labels.RegionSpecial %> </label>
                                        <div class="col-sm-4 col-xs-12">
                                            <asp:DropDownList ID="ddlRegionSpecial" CssClass="form-control select2" Width="100%" runat="server"/>
                                        </div>

                                        <div class="col-sm-4 col-xs-12"></div>
                                    </div>
                                </div>
                           
                                <div class="row">
                                    <div class="form-group col-xs-12">
                                        <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label"><%= Resources.labels.desc %></label>
                                        <div class="col-sm-4 col-xs-12">
                                            <asp:TextBox ID="txDescription" CssClass="form-control" TextMode="MultiLine" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');" runat="server"/>
                                        </div>
                                        <div class="col-sm-4 col-xs-12"></div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate()" OnClick="btsave_Click"/>
                            <asp:Button runat="server" ID="btnClear" CssClass="btn btn-secondary" Text="<%$ Resources:labels, Clear%>" OnClick="btnClear_OnClick"/>
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" id="divpnbranchinfo" runat="server">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%= Resources.labels.danhsachbranchtrongregionfee %>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel runat="server" ID="pnbranchinfor">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gvBranchList" CssClass="table table-hover" runat="server" BackColor="White"
                                                      BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                                      Width="100%" AutoGenerateColumns="False"
                                                      OnRowDataBound="gvBranchList_RowDataBound" PageSize="15">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:labels, machinhanh %>">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lblBranchID" runat="server"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:labels, bankname %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBankName" runat="server"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:labels, branch %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranchName" runat="server"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <control:GridViewPaging ID="GridViewPaging" runat="server"/>
                                        <asp:Literal runat="server" ID="litError"/>
                                        <asp:HiddenField ID="hdCounter" Value="0" runat="server"/>
                                        <asp:HiddenField ID="hdPageSize" Value="15" runat="server"/>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    var regionFeeName = '<%= txRegionName.ClientID %>';

    function validate() {
      if(!validateEmpty(regionFeeName, '<%= Resources.labels.tenvungphikhongduocdetrong %>')){
           document.getElementById(regionFeeName).focus();
           return false;
      }
      return true;
    }
</script>