<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSProductLimit_Controls_Widget" %>
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
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.thongtinhanmucsanpham%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tensanpham %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlProductType" CssClass="form-control select2" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlProductType_OnSelectedIndexChanged" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.giaodich %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlTrans" CssClass="form-control select2" Width="100%" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlTransaction_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tiente %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlCCYID" CssClass="form-control select2" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
									 <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.hanmucmotgiaodich %></label>
                                            <div class="col-sm-6 col-xs-9">
                                                <asp:TextBox ID="txtlimit" CssClass="form-control" MaxLength="21" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:CheckBox ID="checktxtlimit" OnCheckedChanged="checktxtlimit_OnCheckedChanged" AutoPostBack="True" runat="server" />
                                            Unlimit
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tonghanmucngay %></label>
                                            <div class="col-sm-6 col-xs-9">
                                                <asp:TextBox ID="txtTotalLimit" CssClass="form-control" MaxLength="21" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:CheckBox ID="checktotallimit" OnCheckedChanged="checktotallimit_OnCheckedChanged" AutoPostBack="True" runat="server" />
                                            Unlimit
                                        </div>
                                    </div>
									 <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.kieuhanmuc %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddllimittype" AutoPostBack="True" OnSelectedIndexChanged="ddllimittype_OnSelectedIndexChanged" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                    <asp:ListItem Value="NOR"> Normal </asp:ListItem>
                                                    <asp:ListItem Value="DEB"> Receiver </asp:ListItem>
                                                    <asp:ListItem Value="BAT"> Batch transactions</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.numberoftransaction %></label>
                                            <div class="col-sm-6 col-xs-9">
                                                <asp:TextBox ID="txtCountLimit" CssClass="form-control" MaxLength="13" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:CheckBox ID="checkcountlimit" OnCheckedChanged="checkcountlimit_OnCheckedChanged" AutoPostBack="True" runat="server" />
                                            Unlimit
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12" runat="server" id="divLimitBIO" visible="false">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.biometriclimit %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtAmountbiometric" CssClass="form-control" MaxLength="21" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.mota %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtDesc" CssClass="form-control" TextMode="MultiLine" MaxLength="250" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12 hidden">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.unittype %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlUnitType" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                    <asp:ListItem Text="<%$ Resources:labels, daily %>" Value="D" runat="server"></asp:ListItem>
                                                    <asp:ListItem Text="<%$ Resources:labels, hangtuan %>" Value="W" runat="server"></asp:ListItem>
                                                    <asp:ListItem Text="<%$ Resources:labels, monthly %>" Value="M" runat="server"></asp:ListItem>
                                                    <asp:ListItem Text="<%$ Resources:labels, quarterly %>" Value="Q" runat="server"></asp:ListItem>
                                                    <asp:ListItem Text="<%$ Resources:labels, yearly %>" Value="Y" runat="server"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="Loading(); return validate()" OnClick="btsave_Click" />
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

    function validate() {
        var checklimit = document.getElementById('<%=checktxtlimit.ClientID %>').checked;
        var checklimittotal = document.getElementById('<%=checktotallimit.ClientID %>').checked;
        var checklimitcount = document.getElementById('<%=checkcountlimit.ClientID %>').checked;
        if (!checklimit) {
            if (!validateEmpty('<%=txtlimit.ClientID %>', '<%=Resources.labels.hanmucsanphamkhongrong %>')) {
                document.getElementById('<%=txtlimit.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtlimit.ClientID %>').value.trim() == "0") {
                alert('<%=Resources.labels.hanmucsanphaminvalid %>');
                document.getElementById('<%=txtlimit.ClientID %>').focus();
                return false;
            }
        }
        if (!checklimittotal) {
            if (!validateEmpty('<%=txtTotalLimit.ClientID %>', '<%=Resources.labels.tonghanmuctrenngaycuahanmucsanphamkhongrong %>')) {
                document.getElementById('<%=txtTotalLimit.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtTotalLimit.ClientID %>').value.trim() == "0") {
                alert('<%=Resources.labels.tonghanmuctrenngaycuahanmucsanphaminvalid %>');
                document.getElementById('<%=txtTotalLimit.ClientID %>').focus();
                return false;
            }
            if (!validateFormTo('<%=txtlimit.ClientID %>', '<%=txtTotalLimit.ClientID %>', '<%=Resources.labels.sotientonggioihanphailonhonsotiengioihan %>')) {
                document.getElementById('<%=txtlimit.ClientID %>').focus();
                return false;
            }
        }
        if (!checklimitcount) {
            if (!validateEmpty('<%=txtCountLimit.ClientID %>', '<%=Resources.labels.numberoftransactionkhongrong %>')) {
                document.getElementById('<%=txtCountLimit.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtCountLimit.ClientID %>').value.trim() == "0") {
                alert('<%=Resources.labels.numberoftransactioninvalid %>');
                document.getElementById('<%=txtCountLimit.ClientID %>').focus();
                return false;
            }
        }

        return true;
    }
    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }
</script>
