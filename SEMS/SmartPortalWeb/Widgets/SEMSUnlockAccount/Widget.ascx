<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSUnlockAccount_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.mokhoataikhoan%>
            </h1>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.mokhoataikhoan%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnFocus" runat="server" DefaultButton="btsaveandcont">
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label required"><%=Resources.labels.dichvu %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList ID="ddlService" CssClass="form-control select2 infinity" Width="100%" OnSelectedIndexChanged="ddlService_OnSelectedIndexChanged" AutoPostBack="True" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label required"><%=Resources.labels.loainguoidung %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList ID="ddlUserType" CssClass="form-control select2 infinity"  OnSelectedIndexChanged="ddlUserType_OnSelectedIndexChanged" AutoPostBack="True" runat="server">
                                                    <asp:ListItem Value="CCO" Selected="True">Corporate</asp:ListItem>
                                                    <asp:ListItem Value="IND">Individual</asp:ListItem>
                                                    <asp:ListItem Value="TELLER">Bank staff</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblUserName" CssClass="col-sm-2 col-xs-12 col-sm-offset-2 control-label required" runat="server"></asp:Label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox ID="txtusername" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsaveandcont" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, mokhoa %>" OnClientClick="Loading(); return validate();" OnClick="btsaveandcont_Click" />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" PostBackUrl="javascript:history.go(-1)" Text="<%$ Resources:labels, back %>" />
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
        if (!validateEmpty('<%=txtusername.ClientID %>','<%=Resources.labels.bancannhaptendangnhaphoacsodienthoai %>')) {
            document.getElementById('<%=txtusername.ClientID %>').focus();
            return false;
        }
        return true;
    }
    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }
</script>



