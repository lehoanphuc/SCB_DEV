<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_EBASYSVAR_Control_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lbltitleValuelist" runat="server">
                           <%=Resources.labels.EBASYSVAREDIT %>
                </asp:Label>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.EBASYSVAREDIT %>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.Key %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtValueName" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.type %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlType" AutoPostBack="True" OnSelectedIndexChanged="changeType" CssClass="form-control select2 infinity" runat="server">
                                                            <asp:ListItem Value="String" Text="String"></asp:ListItem>
                                                            <asp:ListItem Value="Number" Text="Number"></asp:ListItem>
                                                            <asp:ListItem Value="Hidden" Text="Hidden"></asp:ListItem>
                                                            <asp:ListItem Value="Datetime" Text="Datetime"></asp:ListItem>
                                                            <asp:ListItem Value="Combobox" Text="Combobox"></asp:ListItem>
                                                            <asp:ListItem Value="Email" Text="Email"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                                 <div class="form-group">
                                            <asp:Label cssClass="col-sm-4 control-label " ID="Label1" runat="server">
                                              <%=Resources.labels.VarName %>
                                            </asp:Label>
                                            <div class="col-sm-8 col-xs-12">
                                                 <asp:DropDownList AutoPostBack="True" OnSelectedIndexChanged="changevalue" ID="ddlReference" CssClass="form-control select2 infinity" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <asp:Label runat="server" />
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.ValueName %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtValueCode" onblur="check()" CssClass="form-control " TextMode="MultiLine" MaxLength="500" runat="server"></asp:TextBox>
                                               
                                                 <asp:TextBox ID="vlcombobox" CssClass="form-control " MaxLength="50" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtEffectiveDate" CssClass="form-control igtxtTuNgay" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:Label runat="server" />
                                        </div>
                               
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group" id="hidensv"  runat="server">
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.desc %> </label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtvardesc" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate()" OnClick="btnAddNew_Click" />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" PostBackUrl="javascript:history.go(-1);" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function validate() {
        if (!validateEmpty('<%=txtValueName.ClientID %>', '<%=Resources.labels.bancannhapvaluename %>')) {
            document.getElementById('<%=txtValueName.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtValueCode.ClientID %>', '<%=Resources.labels.bancannhapvaluecode %>')) {
            document.getElementById('<%=txtValueCode.ClientID %>').focus();
            return false;
        }

        return true;
    }
    function check() {
        var type = document.getElementById('<%=ddlType.ClientID%>').value;
        var value = document.getElementById('<%=txtValueCode.ClientID%>').value;
        if (!CheckInt(value) && type == "Int" && value != "") {
            document.getElementById('<%=txtValueCode.ClientID %>').value = "";
            document.getElementById('<%=txtValueCode.ClientID %>').focus();
            alert('<%=Resources.labels.bancannhapdinhdangso %>')
        }
    }
    function CheckInt(value) {
        if (/^[-+]?(\d+|Infinity)$/.test(value)) {
            return true;
        } else {
            return false;
        }
    }
</script>
