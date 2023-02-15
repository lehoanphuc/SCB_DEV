<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCorporate_Controls_Widget" %>
<link href="CSS/bootstrap.min.css" rel="stylesheet" />
<link href="CSS/smallBoostrap.css" rel="stylesheet" />
<link href="widgets/SEMSHeader/css/style.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <div class="al">
            <asp:Image ID="imgLoGo" runat="server" Style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
            <asp:Label ID="lblTitle" runat="server"></asp:Label>
        </div>
        <div id="divError">
            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img alt="" src="widgets/SEMSKRCBank/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:Label ID="lblError" ForeColor="Red" Font-Bold="true" runat="server" />
        </div>
        <div class="col-sm-12">
            <div class="row">
                <div class="col-sm-1">
                    &nbsp;
                </div>
                <div class="col-sm-10">
                    <div class="content form-horizontal">
                        <asp:Panel ID="pnAdd" runat="server" DefaultButton="btsave">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-4 control-label">
                                            <label style="color: red; font-weight: bold">
                                                <%=Resources.labels.corpid %>*
                                            </label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtCorpID" onkeyup="this.value=this.value.toUpperCase()" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-4 control-label">
                                            <label style="color: red; font-weight: bold">
                                                <%=Resources.labels.corpname %>*
                                            </label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtCorpName" runat="server" CssClass="form-control" MaxLength="250"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-4 control-label">
                                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, catalogname%>"></asp:Label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCatalog" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-4 control-label">
                                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, desc%>"></asp:Label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtDesc" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control textarea1"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-4 control-label">
                                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, status%>"></asp:Label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2 infinity">
                                                <asp:ListItem Value="A" Text="<%$ Resources:labels, active%>" />
                                                <asp:ListItem Value="N" Text="<%$ Resources:labels, inactive%>" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="row center p-tb-15">
                        <asp:Button ID="btsave" runat="server" CssClass="btn btn-submit" Text="<%$ Resources:labels, save %>"
                            OnClientClick="Loading();" OnClick="btsave_Click" />
                        &nbsp;&nbsp;
            <asp:Button ID="btback" runat="server" CssClass="btn" Text="<%$ Resources:labels, back %>"
                OnClick="btback_Click" OnClientClick="Loading();return Confirm();" />
                    </div>
                </div>
                <div class="col-sm-1">
                    &nbsp;
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script language="javascript">
    function Loading() {
        if ($('<%=lblError.ClientID%>').html() != '') {
            $('<%=lblError.ClientID%>').html('');
        }
    }

    function Confirm() {
        return confirm('Are you sure you want to exit?');
    }
</script>
