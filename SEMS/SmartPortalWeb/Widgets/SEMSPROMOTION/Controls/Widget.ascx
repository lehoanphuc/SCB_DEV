<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSOTCFEE_Controls_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hdID" runat="server"/>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleProduct" runat="server"></asp:Label>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.thongtinpromotion%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.promotioncode %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtPromotionCode" CssClass="form-control" MaxLength="200" style='text-transform:uppercase' runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.ExpireDate %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtExpireDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.status %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlStatus" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="Loading(); return Validate();" OnClick="btsave_Click" />
                            <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClientClick="Loading();" OnClick="btnClear_Click" />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClientClick="Loading();" OnClick="btback_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }
    function Validate() {
        if (!validateEmpty('<%=txtPromotionCode.ClientID %>', '<%=Resources.labels.promotioncodekhongrong %>')) {
            document.getElementById('<%=txtPromotionCode.ClientID %>').focus();
            return false;
        }
        var iChars = "!@#$%^&*()+=-[]\\\';,./{}|\":<>?";
        for (var i = 0; i < document.getElementById('<%=txtPromotionCode.ClientID%>').value.length; i++) {
            if (iChars.indexOf(document.getElementById('<%=txtPromotionCode.ClientID%>').value.charAt(i)) != -1) {
                alert('<%=Resources.labels.promotioncodespecialcharactervalidate %>');
                document.getElementById('<%=txtPromotionCode.ClientID %>').focus();
                return false;
            }
        }
        if (!hasWhiteSpace('<%=txtPromotionCode.ClientID %>', '<%=Resources.labels.promotioncodewhitespace %>')) {
            document.getElementById('<%=txtPromotionCode.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtExpireDate.ClientID %>', '<%=Resources.labels.expiredatekhongrong %>')) {
            document.getElementById('<%=txtExpireDate.ClientID %>').focus();
            return false;
        }
        return true;
    }
</script>
