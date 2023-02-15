<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSREGIONFEE_Controls_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleBranch" runat="server"></asp:Label>
            </h1>
        </div>
         <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.moduleaccountlistinformation%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0" style="display: block;">
                            <asp:Panel ID="pnRegion" runat="server">
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.modulename %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlModuleName" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.systemaccountname %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtSysAccName" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.mota %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDescription" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate()" OnClick="btsave_Click" />
                            <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_Click" />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
   <%-- function validate() {
        if (!validateEmpty('<%=txRegionName.ClientID %>', '<%=Resources.labels.tenvungphikhongduocdetrong %>')) {
            document.getElementById('<%=txRegionName.ClientID %>').focus();
            return false;
        }
        return true;
    }--%>
    function validateEmpty(id, msg) {
        if (document.getElementById(id).value == "0" || document.getElementById(id).value == "") {
            window.alert(msg);
            return false;
        }
        else {
            return true;
        }
    }
</script>
