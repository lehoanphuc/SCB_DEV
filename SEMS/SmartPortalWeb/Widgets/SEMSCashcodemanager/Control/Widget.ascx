<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCashcodemanager_Control_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleProduct" runat="server"></asp:Label>
            </h1>
        </div>
        <div>
            <asp:HiddenField runat="server" ID="hdFee" />
            <asp:HiddenField runat="server" ID="hdAmount" />
            <asp:HiddenField runat="server" ID="hdCCYID" />
            <asp:HiddenField runat="server" ID="hdContractNo" />
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%= Resources.labels.cashcodeinformation %>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnDetail" runat="server">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.sogiaodich %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtTransID" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.ngaytao %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtDateCreate" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group col-sm-12 bold"><%=Resources.labels.thongtinnguoichuyen %></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.senderphone %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtSenderPhone" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.hotennguoitratien %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtSenderName" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.PaperNumber %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtPaperNumber" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.senderaddress %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtSenderAddress" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.taikhoannguon %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtSourceMoney" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.sendtype %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtSourceType" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group col-sm-12 bold"><%=Resources.labels.thongtinnguoinhan %></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.receiverphone %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtReceiverPhone" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.tennguoinhan %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtReceiverName" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.PaperNumber %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtReceiverPaperNumber" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.diachinguoinhan %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtReceiverAddress" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group col-sm-12 bold"><%=Resources.labels.noidungthanhtoan %></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.senderamount %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtSenderAmount" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.paidamount %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtPaidAmount" CssClass="form-control" runat="server" />
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
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.trangthai %></label>
                                            <div class="col-sm-8 col-xs-12 custom-control">
                                                <asp:DropDownList ID="ddlStatus" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                    <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
                                                    <asp:ListItem Value="N" Text="<%$ Resources:labels, notyetpaid %>"></asp:ListItem>
                                                    <asp:ListItem Value="P" Text="<%$ Resources:labels, partialpaid %>"></asp:ListItem>
                                                    <asp:ListItem Value="F" Text="<%$ Resources:labels, fullypaid %>"></asp:ListItem>
                                                    <asp:ListItem Value="D" Text="<%$ Resources:labels, condelete %>"></asp:ListItem>
                                                    <asp:ListItem Value="E" Text="<%$ Resources:labels, Expried %>"></asp:ListItem>
                                                    <asp:ListItem Value="L" Text="<%$ Resources:labels, locked %>"></asp:ListItem>
                                                    <asp:ListItem Value="C" Text="<%$ Resources:labels, canceled %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.desc %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtDesc" CssClass="form-control" TextMode="MultiLine" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div id="divDetail" class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted" runat="server">
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="Button8_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="pnCancel" runat="server">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%= Resources.labels.cancelcashcode %>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-12  col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label"><%=Resources.labels.hinhthucnhan %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList CssClass="form-control select2 infinity" Width="100%" ID="ddlReceiveType" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divFeeShare" class="row" visible="False" runat="server">
                                    <div class="col-sm-12  col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label"><%=Resources.labels.FeeShareForSender %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList CssClass="form-control select2 infinity" Width="100%" ID="ddlFeeShareSender" runat="server">
                                                    <asp:ListItem Value="" Text="&nbsp;"></asp:ListItem>
                                                    <asp:ListItem Value="Y" Text="<%$ Resources:labels, co %>"></asp:ListItem>
                                                    <asp:ListItem Value="N" Text="<%$ Resources:labels, khong %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12  col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label"><%=Resources.labels.sotien %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:Label ID="lblAmount" CssClass="control-label" runat="server"></asp:Label>&nbsp;<asp:Label ID="lblCurrencyAmount" CssClass="control-label" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12  col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label"><%=Resources.labels.sotienphi %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:Label ID="lblFeeAmount" CssClass="control-label" runat="server"></asp:Label>&nbsp;<asp:Label ID="lblCurrencyFeeAmount" CssClass="control-label" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12  col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label"><%=Resources.labels.desc %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox ID="txtMota" CssClass="form-control" TextMode="MultiLine" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                <%-- Hong modify : allow click many times--%>
                                <asp:Button ID="btnConfirm" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, xacnhan %>" OnClick="btnConfirm_OnClick" />
                                <%-- <asp:Button ID="btnConfirm" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, xacnhan %>" OnClick="btnConfirm_OnClick" OnClientClick="return checkdoubleclick()" />--%>
                                <asp:Button ID="btnPrint" Enabled="False" CssClass="btn btn-primary" runat="server" OnClientClick="javascript:return poponload()" Text="<%$ Resources:labels, inketqua %>" OnClick="btnPrint_OnClick" />
                                <asp:Button ID="btnBack" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="Button8_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnResend" runat="server">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-container">
                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                <asp:Button ID="btnResend" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, resend %>" OnClick="btnResend_OnClick" OnClientClick="return checkdoubleclick()" />
                                <asp:Button ID="Button3" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="Button8_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">

    var click = 0;
    function checkdoubleclick() {
        if (++click > 1) {
            return false;
        }
    }

    function poponload() {
        testwindow = window.open("widgets/SEMSCashcodemanager/print.aspx", "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
</script>


