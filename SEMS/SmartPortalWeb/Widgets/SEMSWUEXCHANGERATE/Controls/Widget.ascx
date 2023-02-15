<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSEXCHANGERATE_Controls_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleProduct" runat="server"></asp:Label>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            Exchange rate
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label">Exchange ID</label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtExchangeID" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label">Exchange Name</label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtExchangeName" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.country %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlCountry" CssClass="form-control select2 " Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tiente %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlCCYID" CssClass="form-control select2 " AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlCCYID_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label">Amount</label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtAmount" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnccyid" runat="server" Visible="false">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label">Currency</label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtCcyid" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                        </asp:Panel>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClick="btsave_Click" OnClientClick="return validate();"  />
                            <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_Click" OnClientClick="Loading();"  />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" OnClientClick="Loading();"/>
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

    function validate() {
        if (!validateEmpty('<%=txtExchangeName.ClientID %>', 'Please input Exchange name !')) {
            return false;
        }
        if (!validateEmpty('<%=txtCcyid.ClientID %>', 'Please input Currency !')) {
            return false;
        }
    }
    function isNumberK(evt) {
        var so = document.getElementById(evt.target.id).value;
        so = so.toString().replace(/\$|\,/g, '');
        if (so != "" && so != "-") {
            so = Math.floor(so * 100 + 0.50000000001);
            so = Math.floor(so / 100).toString();
            if (Number(so) < 0) {
                for (var i = 0; i < Math.floor((so.length - 1 - (1 + i)) / 3); i++) {
                    so = so.substring(0, so.length - (4 * i + 3)) + ',' +
                        so.substring(so.length - (4 * i + 3));
                }
            }
            else {
                document.getElementById(evt.target.id).setAttribute("MaxLength", "21");
                for (var i = 0; i < Math.floor((so.length - (1 + i)) / 3); i++) {
                    if (Math.floor((so.length - (1 + i)) / 3) <= 5) {
                        so = so.substring(0, so.length - (4 * i + 3)) + ',' +
                            so.substring(so.length - (4 * i + 3));
                    }
                }
            }
        }
        document.getElementById(evt.target.id).value = so

    }
</script>
