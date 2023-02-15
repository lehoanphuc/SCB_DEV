<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSinternational_Control_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<style>
    .panel {
        margin-bottom: 0.5rem;
    }
</style>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div id="divCustHeader">
            INTERNATIONAL TRANSFER INFORMATION
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <br />
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div>
            <asp:HiddenField ID="hdfstatus" runat="server" />
        </div>
        <div class="row" runat="server" id="divConsumber">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>Sender information
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="Panel4" runat="server">
                            <div class="row">
                                <div class="col-sm-4">
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Payment Reference</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtReference" CssClass="form-control" runat="server" MaxLength="15" onkeyup="this.value=this.value.replace(/[^0-9.]/g, '')" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.senderamount %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblSenderAmount" runat="server" class="control-label"></asp:Label>
                                            <asp:Label ID="lblSenderCCYIDAmount" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.TransactionDate %></label>
                                        <div class="col-xs-5 col-md-4">
                                            <asp:TextBox ID="txtTrandate" OnTextChanged="txtTrandate_TextChanged" AutoPostBack="true" autocomplete="off" type="date"  runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label" style="font-weight: bold;"><%=Resources.labels.sendtype%> </label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlsendtype" CssClass="form-control select2 infinity" Width="100%" runat="server" OnSelectedIndexChanged="ddlsendtype_SelectedIndexChanged">
                                                <asp:ListItem Value="EMAIL" Text="<%$ Resources:labels,  email%>"></asp:ListItem>
                                                <asp:ListItem Value="PHONE" Text="<%$ Resources:labels,  phone%>"></asp:ListItem>
                                                <asp:ListItem Value="BOTH" Text="Both sms and email"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.hotennguoitratien %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblSenderName" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.idtype %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblSenderIDType" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.idnumber %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblSenderIDNumber" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.country %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblSenderCountry" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.expdate %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblExpDate" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.issdate %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblIssDate" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.address %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblSenderAddress" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.PhoneNumber %> :</label>
                                        <div class="col-sm-7">
                                            <asp:Label ID="lblSenderPhone" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="divEnterprise">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>Sender information
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="Panel3" runat="server">
                            <div class="row">
                                <div class="col-sm-4">
                                </div>                              
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Payment Reference</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtEnterReference" CssClass="form-control" runat="server" MaxLength="15" onkeyup="this.value=this.value.replace(/[^0-9.]/g, '')" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.senderamount %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblEnterSenderAmount" runat="server" class="control-label"></asp:Label>
                                            <asp:Label ID="lblEnterSenderCCYIDAmount" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.TransactionDate %></label>
                                        <div class="col-xs-5 col-md-4">
                                            <asp:TextBox OnTextChanged="txtTrandate_TextChanged" AutoPostBack="true" ID="txtEnterTrandate" autocomplete="off" type="date" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label" ><%=Resources.labels.sendtype%> </label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlEntersendtype" CssClass="form-control select2 infinity" Width="100%" runat="server"  OnSelectedIndexChanged="ddlEntersendtype_SelectedIndexChanged">                                            
                                                <asp:ListItem Value="EMAIL" Text="<%$ Resources:labels,  email%>"></asp:ListItem>
                                                <asp:ListItem Value="PHONE" Text="<%$ Resources:labels,  phone%>"></asp:ListItem>
                                                <asp:ListItem Value="BOTH" Text="Both sms and email"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.enterprisename %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblEntername" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.address %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblEnterAddress" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.PhoneNumber %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblEnterPhone" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.gpkd %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblEnterLicense" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.taxcode %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblEnterTaxcode" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="Div2">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>RECEIVER information
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="row">
                                <div class="col-sm-4">
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.BankName %>:</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblBankName" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.swiftcode %>:</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblSwiftCode" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.tennguoithuhuong %>:</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblBenName" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Beneficiary account number:</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblBenaccount" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.address %>:</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblBenAddress" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.PhoneNumber %>:</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblBenPhone" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.email %>:</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblBenEmail" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="Div3">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>Transfer content
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="Panel2" runat="server">
                            <div class="row">
                                <div class="col-sm-4">
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.taikhoanghino %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblDebitAccount" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label left"><%=Resources.labels.sotien %> :</label>
                                        <div class="col-sm-7 control-label right">
                                            <asp:Label ID="lblAmount" runat="server" class="control-label"></asp:Label>
                                            <asp:Label ID="lblCCYIDAmount" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.loaiphi %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblFeeType" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.phi %> :</label>
                                        <div class="col-sm-7">
                                            <asp:Label ID="lblFee" runat="server" class="control-label"></asp:Label>
                                            <asp:Label ID="lblCCYIDFEE" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><%=Resources.labels.tongtien %> :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblTotalAmount" runat="server" class="control-label"></asp:Label>
                                            <asp:Label ID="lblCCYIDTotalAmount" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Purpose Of Remittance :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblPurPose" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Detail Of Remittance :</label>
                                        <div class="col-sm-7 control-label">
                                            <asp:Label ID="lblDetailPurPose" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <!--TrungTQ-->
                        <asp:Panel ID="pnlDocument" runat="server" Visible="false">
                            <asp:Repeater runat="server" ID="rptDocument" OnItemCommand="rptDocument_ItemCommand" OnItemDataBound="rptDocument_ItemDataBound">
                                <HeaderTemplate>
                                    <div class="pane">
                                        <div class="table-responsive">
                                            <table class="table table-hover footable c_list">
                                                <thead style="background-color: #7A58BF; color: #FFF;">
                                                    <tr>
                                                        <th class="title-repeater">Document Name</th>
                                                        <th class="title-repeater">Document Type</th>
                                                        <th class="title-repeater">View</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="title-repeater">
                                            <%#Eval("DOCUMENTNAME") %>
                                        </td>
                                        <td class="title-repeater">
                                            <%#Eval("DOCUMENTTYPE") %>
                                        </td>
                                        <td class="title-repeater">
                                            <asp:LinkButton ID="lblDownload" OnClientClick="aspnetForm.target ='_blank';" runat="server" CommandName="Download">View</asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                        </table>                                       
                                </FooterTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-container">
            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                <asp:Button ID="btsend" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, Send %>" OnClick="btsave_Click" Style="height: 34px;" Visible ="false"/>
                <asp:Button ID="btresend" CssClass="btn btn-primary" runat="server" Text="ReSend" OnClick="btsave_Click" Style="height: 34px;" />               
                <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Back %>" OnClick="btback_Click" Style="height: 34px;" />
                <asp:Button ID="btnPrint" CssClass="btn btn-warning" runat="server" Text="Print" OnClick="btnPrint_Click" OnClientClick="return poponload()" Style="height: 34px;" />
            </div>
        </div>
        </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function replaceAll(str, from, to) {
        var idx = str.indexOf(from);
        while (idx > -1) {
            str = str.replace(from, to);
            idx = str.indexOf(from);
        }
        return str;
    }

    function ntt(sNumber, idDisplay, event) {
        executeComma(sNumber, event);
        if (document.getElementById(sNumber).value == "") {
            document.getElementById(idDisplay).innerHTML = "";
            return;
        }
        document.getElementById(idDisplay).innerHTML = "(" + number2text(replaceAll(document.getElementById(sNumber).value, ",", ""), '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>') + ")";
    }

    function poponload() {
        testwindow = window.open("widgets/SEMSinternational/print.aspx?pt=P&cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=800,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
</script>

