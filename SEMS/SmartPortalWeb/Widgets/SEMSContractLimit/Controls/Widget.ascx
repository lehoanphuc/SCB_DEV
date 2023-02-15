<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContractLimit_Controls_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleProduct" runat="server"></asp:Label></h1>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError" style="text-align: center">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="row" runat="server" id="pn1" defaultbutton="btnKiemtra">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%= Resources.labels.thongtinhanmuchopdong %>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row" id="HienThi" runat="server" visible="false">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <asp:Label class="col-sm-4 control-label" runat="server" ID="lblTenKH"><%=Resources.labels.tenkhachhang %></asp:Label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lblCustName" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.mahopdong %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtcontractno" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnKiemtra" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, xemchitiet %>" OnClick="btnKiemtra_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.giaodich %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlTrans" runat="server" CssClass="form-control select2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.hanmuc %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtlimit" MaxLength="15" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.tiente %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlCCYID" runat="server" CssClass="form-control select2 infinity">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.solangiaodichngay %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtCountLimit" MaxLength="12"  runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.tonghanmucngay %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtTotalLimitDay" MaxLength="15" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-6" id="cbbUnitype" runat="server">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.unittype %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlUnitType" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                    <asp:ListItem Text="<%$ Resources:labels, daily %>" Value="D" runat="server"></asp:ListItem>
                                                    <asp:ListItem Text="<%$ Resources:labels, hangtuan %>" Value="W" runat="server"></asp:ListItem>
                                                    <asp:ListItem Text="<%$ Resources:labels, monthly %>" Value="M" runat="server"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.kieuhanmuc %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlLimitType" runat="server" CssClass="form-control select2 infinity" OnSelectedIndexChanged="ddlLimitType_IndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="text-align: center; margin-top: 10px;">
    &nbsp;<asp:Button ID="btsave" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, save %> " OnClientClick="return  validate();" OnClick="btsave_Click" />
    &nbsp;<asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, inthongtin %> " OnClientClick="javascript:return poponload();"  Visible="false"/>
    &nbsp;<asp:Button ID="btback" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, back %> " OnClick="btback_Click" />
        &nbsp;<asp:Button ID="btnClean" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, Clear %> " OnClick="btnClean_Click" />
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>

<script type="text/javascript">
    function poponload() {
        testwindow = window.open("widgets/SEMSContractLimit/print.aspx?cul=" +'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
    function validateEmpty(id, msg) {
        if (document.getElementById(id).value == "") {
            window.alert(msg);
            return false;
        }
        else {
            return true;
        }
    }
    function validate() {
        if (document.getElementById('<%=txtlimit.ClientID %>').value.trim() == "0") {
            alert('<%=Resources.labels.hanmucsanphamkhongrong %>');
              document.getElementById('<%=txtlimit.ClientID %>').focus();
              return false;
        }
        if (document.getElementById('<%=txtTotalLimitDay.ClientID %>').value.trim() == "0") {
            alert('<%=Resources.labels.tonghanmucngaycuahopdongkhongrong %>');
              document.getElementById('<%=txtTotalLimitDay.ClientID %>').focus();
              return false;
        }
        if (document.getElementById('<%=txtCountLimit.ClientID %>').value.trim() == "0") {
            alert('<%=Resources.labels.sogiaodichngaykhongrong %>');
              document.getElementById('<%=txtCountLimit.ClientID %>').focus();
              return false;
          }
        if (validateEmpty('<%=txtcontractno.ClientID %>', '<%=Resources.labels.mahopdongkhongrong %>')) {
            if (validateEmpty('<%=txtlimit.ClientID %>', '<%=Resources.labels.hanmuchopdongkhongrong %>')) {
                if (validateEmpty('<%=txtTotalLimitDay.ClientID %>', '<%=Resources.labels.tonghanmucngaycuahopdongkhongrong %>')) {
                    if (validateEmpty('<%=txtCountLimit.ClientID %>', '<%=Resources.labels.sogiaodichngaykhongrong %>')) {
                    }
                    else {
                        document.getElementById('<%=txtCountLimit.ClientID %>').focus();
                        return false;
                    }
                }
                else {
                    document.getElementById('<%=txtTotalLimitDay.ClientID %>').focus();
                    return false;
                }
            }
            else {

                document.getElementById('<%=txtlimit.ClientID %>').focus();
                return false;
            }
        }
        else {
            document.getElementById('<%=txtcontractno.ClientID %>').focus();
            return false;
        }
      
    }
</script>
