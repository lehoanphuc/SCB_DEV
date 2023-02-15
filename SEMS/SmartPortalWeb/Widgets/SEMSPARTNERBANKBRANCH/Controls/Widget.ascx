<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSPARTNERBANKBRANCH_Controls_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleBank" runat="server"/>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text=""/>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%= Resources.labels.thongtinchinhanhdoitac %>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12   control-label required"><%= Resources.labels.madoitacchinhanh %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtbranchcode" CssClass="form-control" runat="server" MaxLength="50"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12  control-label required"><%= Resources.labels.tendoitacchinhanh %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtbranchname" CssClass="form-control" runat="server" MaxLength="255"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12   control-label required"><%= Resources.labels.trangthai %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlStatus" Width="100%" CssClass="form-control select2 infinity" runat="server" Enabled="False">
                                                    <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12   control-label required"><%= Resources.labels.tenmavungphi %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlRegion" Width="100%" CssClass="form-control select2 infinity" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                 <%--   <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12  control-label required"><%= Resources.labels.originalbranchcode %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="lbloriginalbranchcode" CssClass="form-control" runat="server" MaxLength="255"/>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12   control-label required"><%= Resources.labels.otherbank %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlBank" Width="100%" CssClass="form-control select2 infinity" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                       
                                    </div>
                                </div>
         
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return checkValidation()" OnClick="btsave_Click"/>
                            <asp:Button runat="server" ID="btnClear" CssClass="btn btn-secondary" Text="<%$ Resources:labels, Clear%>" OnClick="btnClear_OnClick"/>
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function checkValidation() {
        var bankcode = '<%= txtbranchcode.ClientID %>';
        var bankname = '<%= txtbranchname.ClientID %>';
 
        if (!validateEmpty(bankcode,'<%= Resources.labels.bancannhapmadoitac %>')) {
            document.getElementById(bankcode).focus();
            return false;
        }
        if (!validateEmpty(bankname,'<%= Resources.labels.bancannhaptendoitac %>')) {
            document.getElementById(bankname).focus();
            return false;
        }
    }
</script>
