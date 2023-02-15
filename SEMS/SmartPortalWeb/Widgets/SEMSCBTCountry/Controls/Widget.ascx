<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCBTCountry_Controls_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleCountry" runat="server"></asp:Label>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.countryinformation%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.countryid %> </label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtCountryID" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="3" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.countrycode %> </label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtCountryCode" CssClass="form-control" MaxLength="25" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.countryname %> </label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtCountryName" CssClass="form-control" MaxLength="200" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.status %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server" Width="100%">
                                                    <asp:ListItem Selected="True" Value="A"> Active </asp:ListItem>
                                                    <asp:ListItem Value="I"> InActive </asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.nostro %> </label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtNostro" CssClass="form-control" MaxLength="5" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.mota %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtDescription" CssClass="form-control" onkeyup="ValidateLimit(this,'250');" onkeyDown="ValidateLimit(this,'250');" onpaste="ValidateLimit(this,'250');" onChange="ValidateLimit(this,'250');" onmousedown="ValidateLimit(this,'250');" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return checkValidation()" OnClick="btsave_Click" />
                            <asp:Button ID="Clear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="Clear_Click" />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" PostBackUrl="javascript:history.go(-1);" OnClick="btback_Click" />
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
        if (!validateEmpty('<%=txtCountryCode.ClientID %>','<%=Resources.labels.validate_countrycode %>')) {
            document.getElementById('<%=txtCountryCode.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtCountryName.ClientID %>','<%=Resources.labels.validate_countryname %>')) {
            document.getElementById('<%=txtCountryName.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtNostro.ClientID %>','<%=Resources.labels.validate_nostro %>')) {
            document.getElementById('<%=txtCountryName.ClientID %>').focus();
            return false;
        }

        return true;
    }
</script>
