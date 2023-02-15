<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCBTransfer_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<script src="JS/docso.js" type="text/javascript"></script>
<asp:ScriptManager runat="server">
</asp:ScriptManager>
<div style="text-align: center; height: 8px">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="" runat="server">
        <ProgressTemplate>
            <div class="cssProgress">
                <div class="progress1">
                    <div class="cssProgress-bar cssProgress-active" data-percent="100" style="transition: none; width: 100%;"></div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="th">
    <span><%=Resources.labels.crossbordertransfer %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label runat="server" ID="lblError" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div>
            <asp:HiddenField runat="server" ID="hdBalanceSender" />
            <asp:HiddenField runat="server" ID="hdTypeID" />
            <asp:HiddenField runat="server" ID="hdTrancode" />
            <asp:HiddenField runat="server" ID="hdActTypeReceiver" />
            <asp:HiddenField runat="server" ID="hdSenderBranch" />
            <asp:HiddenField runat="server" ID="hdFullName" />
            <asp:HiddenField runat="server" ID="hdSelectedBankID" />
            <asp:HiddenField runat="server" ID="hdSelectefCCYID" />
            <asp:HiddenField ID="txtChu" runat="server" />
            <asp:HiddenField ID="hdUserType" runat="server" />
        </div>
        <asp:Panel ID="pnRemitterIND" runat="server" class="divcontent">
            <div class="handle">
                <span><%=Resources.labels.thongtinnguoitratien %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.hotennguoitratien %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtSenderName" onkeyup="this.value=this.value.replace(/[^a-zA-Z ]/g, '')" autocomplete="off" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.idtype %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                       <asp:DropDownList runat="server" ID ="ddlIdType"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.idnumber %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox runat="server" ID="txtIdNumber"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.countryofissue %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:Label ID="lblSenderCountry" runat="server" Text='Laos'></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.expdate %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox runat="server" ID="txtExpDate"  type="date"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.issdate %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox runat="server" ID="txtIssDate" type="date"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.address %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtSenderAddress" TextMode="MultiLine" autocomplete="off" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.PhoneNumber %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtSenderPhone" MaxLength="12" onkeypress=" return isNumberKeyNumer(event)" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnRemitterMTK" runat="server" class="divcontent">
            <div class="handle">
                <span><%=Resources.labels.thongtinnguoitratien %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.enterprisename %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtEnterName" onkeyup="this.value=this.value.replace(/[^a-zA-Z ]/g, '')" autocomplete="off" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.address %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtEnterAddress" autocomplete="false" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.PhoneNumber %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtEnterPhone" onkeypress=" return isNumberKeyNumer(event)" autocomplete="false" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.gpkd %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtEnterLicense"  runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.taxcode %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtEnterTax"  runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnBeneficiary" runat="server" class="divcontent">
            <div class="handle">
                <span><%=Resources.labels.thongtinnguoinhantien %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.BankName %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <%--<asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged">
                        </asp:DropDownList>--%>
                        <asp:TextBox runat="server" ID ="txtBankName" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.swiftcode %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtSwiftCode" runat="server" OnTextChanged="txtSwiftCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                </div>

                 <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.currency %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                       <asp:DropDownList runat="server" ID ="ddlCcyID"></asp:DropDownList>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.tennguoithuhuong %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtReceiverName" onkeyup="this.value=this.value.replace(/[^a-zA-Z ]/g, '')" autocomplete="off" runat="server" Text=""></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.taikhoanthuhuong %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-md-4 line30">
                        <asp:TextBox ID="txtReceiverAcc" autocomplete="off" MaxLength="25" onkeypress=" return isNumberKeyNumer(event)" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.address %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                            <asp:TextBox ID="txtReceiverAdd" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.PhoneNumber %></span>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtReceiverPhone" autocomplete="off" MaxLength="12" onkeypress=" return isNumberKeyNumer(event)" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.email9 %></span>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtReceiverMail" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="button-group">
                    <asp:Button ID="btnNext" runat="server" CssClass="btn btn-primary" OnClientClick=" return validate();" Text='<%$ Resources:labels, tieptuc %>' OnClick="btnNext_Click" />
                </div>
        </asp:Panel>
        <asp:Panel ID="pnPurpose" runat="server" class="divcontent" Visible="false">
            <div class="handle">
                <span><%=Resources.labels.PurposeOfRemittance %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.PurposeOfRemittance %></span>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:DropDownList runat="server" ID="ddlpaymentpurpose" AutoPostBack="true" OnSelectedIndexChanged="ddlpaymentpurpose_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <br />
                <asp:Panel runat="server" ID="pnlReason1" Visible="false">
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <span><%=Resources.labels.idcardpassport %></span>
                        </div>
                        <div class="col-xs-7 col-sm-3 line30">
                            <asp:FileUpload runat="server" ID="fileDocument" AutoPostBack="true" ClientIDMode="Static" accept=".png,.jpg,.jpeg" />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlReason2" Visible="false">
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <span><%=Resources.labels.idcardpassport %></span>
                        </div>
                        <div class="col-xs-7 col-sm-3 line30">
                            <asp:FileUpload runat="server" ID="FileUpload2" AutoPostBack="true" ClientIDMode="Static" accept=".png,.jpg,.jpeg" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <span><%=Resources.labels.invoiceetc %></span>
                        </div>
                        <div class="col-xs-7 col-sm-3 line30">
                            <asp:FileUpload runat="server" ID="FileUpload3" AutoPostBack="true" ClientIDMode="Static" accept=".png,.jpg,.jpeg" />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlReason3" Visible="false">
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <span><%=Resources.labels.idcardpassport %></span>
                        </div>
                        <div class="col-xs-7 col-sm-3 line30">
                            <asp:FileUpload runat="server" ID="FileUpload4" AutoPostBack="true" ClientIDMode="Static" accept=".png,.jpg,.jpeg"/>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <span><%=Resources.labels.enterpriseregis %></span>
                        </div>
                        <div class="col-xs-7 col-sm-3 line30">
                            <asp:FileUpload runat="server" ID="FileUpload5" AutoPostBack="true" ClientIDMode="Static" accept=".png,.jpg,.jpeg"/>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <span><%=Resources.labels.certification %></span>
                        </div>
                        <div class="col-xs-7 col-sm-3 line30">
                            <asp:FileUpload runat="server" ID="FileUpload6" AutoPostBack="true" ClientIDMode="Static" accept=".png,.jpg,.jpeg"/>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <span><%=Resources.labels.InvoicePurchase %></span>
                        </div>
                        <div class="col-xs-7 col-sm-3 line30">
                            <asp:FileUpload runat="server" ID="FileUpload7" AutoPostBack="true" ClientIDMode="Static" accept=".png,.jpg,.jpeg"/>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <span><%=Resources.labels.CustomsDeclaration %></span>
                        </div>
                        <div class="col-xs-7 col-sm-3 line30">
                            <asp:FileUpload runat="server" ID="FileUpload8" AutoPostBack="true" ClientIDMode="Static" accept=".png,.jpg,.jpeg" />
                        </div>
                    </div>
                </asp:Panel>
                <div style="text-align: center; margin-top: 10px;">
                    <asp:Button ID="btnUpLoadFile" runat="server" CssClass="btn btn-primary" OnClick="btnUpLoadFile_Click" Text="UpLoad" />
                </div>
                <br />
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.DetailsOfRemittance %></span>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtDetailRemittance" runat="server" TextMode="MultiLine"></asp:TextBox><br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.choosefeecross %></span>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:DropDownList runat="server" ID="ddlchoosefee"></asp:DropDownList>
                    </div>
                </div>
                <asp:Panel runat="server" ID="pnuploadfileCorp" Visible ="false">
                    <div class="row">
                        <div class="row form-group">
                            <div class="col-xs-3 col-sm-4 right">
                                <asp:Label ID="LblDocument" runat="server" Text=""><%= Resources.labels.document %>&nbsp;*</asp:Label>
                            </div>
                            <div class="col-xs-4 col-md-4 col-sm-6 line30">
                                <asp:FileUpload ID="FUDocument" AutoPostBack="true" runat="server" ClientIDMode="Static" accept=".pdf,.png,.jpg,.jpeg" />
                            </div>
                            <div class="col-xs-5 col-md-4 col-sm-6 line30">
                                <asp:Label ID="LblDocumentExpalainion" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666" Text='<%$ Resources:labels, uploadlimit1MB %>'></asp:Label>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="row">
                    <asp:Repeater runat="server" ID="rptDocument" OnItemCommand="rptDocument_ItemCommand">
                        <HeaderTemplate>
                            <table class="table table-bordered table-hover footable" data-paging="true">
                                <thead>
                                    <tr>
                                        <th><%= Resources.labels.filename %></th>
                                        <th><%= Resources.labels.fileextension %></th>
                                        <th><%= Resources.labels.filesize %></th>
                                        <th><%= Resources.labels.desc%></th>
                                        <th><%= Resources.labels.huy %></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Eval("FileName") %>
                                </td>
                                <td>
                                    <%#Eval("FileExtension") %>
                                </td>
                                <td>
                                    <%#Eval("FileSize") %>
                                </td>
                                <td>
                                    <%#Eval("Description") %></td>
                                <td>
                                    <asp:LinkButton ID="lbDelete" runat="server" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' ForeColor="Red"><%= Resources.labels.huy %></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
			            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div style="text-align: center; margin-top: 10px;">
                    <asp:Button ID="btnBack1" runat="server" Text='<%$ Resources:labels, quaylai %>' OnClick="btnBack1_Click" class="btn btn-warning" />
                    <asp:Button ID="btnNext1" runat="server" Text='<%$ Resources:labels, tieptuc %>' OnClick="btnNext1_Click" class="btn btn-primary" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnpaymentinfor" runat="server" class="divcontent" Visible="false">
            <div class="handle">
                <span><%=Resources.labels.noidungthanhtoan %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.debitaccount %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:DropDownList ID="ddlSenderAccount" runat="server" OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.availablebalance %></span>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:Label ID="lblAvailableBal" runat="server"></asp:Label>&nbsp;
                        <asp:Label ID="lblAvailableBalCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <span><%=Resources.labels.sotien %></span>&nbsp;*
                    </div>
                    <div class="col-xs-7 col-md-4 line30">
                        <asp:TextBox ID="txtAmount" autocomplete="off" runat="server" Text="" MaxLength="15" CssClass="amount"></asp:TextBox>
                        <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        &nbsp;
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:Label ID="lblText" runat="server" Font-Size="7pt" Font-Bold="True" Font-Italic="True" ForeColor="#0066FF" Style="padding-top: 2px; line-height: 23px !important;"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-4 form-control" style="text-align: center; color: #2e6da4">
                        <asp:Label ID="Label40" runat="server" Text='I/We certify that the details given above are correct and in comply with the terms and conditions of international outward remittance specified by Siam Commercial Bank'></asp:Label>
                    </div>
                </div>
                <div style="text-align: center; margin-top: 10px;">
                    <asp:Button ID="btnBack2" runat="server" Text='<%$ Resources:labels, quaylai %>' OnClick="btnBack2_Click" class="btn btn-warning" />
                    <asp:Button ID="btnNext2" runat="server" Text='<%$ Resources:labels, tieptuc %>' OnClick="btnNext2_Click" class="btn btn-primary" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnConfirm" runat="server" class="divcontent" Visible="false">
            <div class="handle">
                <asp:Label ID="Label20" runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
            </div>
            <div class="content_table">
                <asp:Panel ID="pnConfirmIND" runat="server" Visible="false">
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, hotennguoitratien %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label35" runat="server" Text="<%$ Resources:labels, idtype %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblIdType" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label39" runat="server" Text="<%$ Resources:labels, idnumber %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblIdNumber" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label42" runat="server" Text="<%$ Resources:labels, countryofissue %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblCountry" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label44" runat="server" Text="<%$ Resources:labels, expdate %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblExpDate" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label47" runat="server" Text="<%$ Resources:labels, issdate %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblIssDate" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <div id="div1" style=" overflow-y:auto;overflow-x:auto; word-break:break-all; font-weight:400;"  runat="server">
                                <asp:Label ID="lblSenderAddress" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, PhoneNumber %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblSenderPhone" runat="server"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnConfirmMTK" runat="server" Visible="false">
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, enterprisename %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEnterName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <div id="div2" style=" overflow-y:auto;overflow-x:auto; word-break:break-all; font-weight:400;"  runat="server">
                            <asp:Label ID="lblEnterAddress" runat="server"></asp:Label>
                                </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, PhoneNumber %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEnterPhone" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, gpkd %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEnterLicense" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, taxcode %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEnterTaxcode" runat="server"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="handle">
                <span><%=Resources.labels.thongtinnguoinhantien %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, BankName %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblBankName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, swiftcode %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblSwiftCode" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, tennguoithuhuong %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblBenName" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, taikhoanthuhuong %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblBenAccount" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label12" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <div id="div3" style=" overflow-y:auto;overflow-x:auto; word-break:break-all; font-weight:400;"  runat="server">
                        <asp:Label ID="lblBenAddress" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label13" runat="server" Text="<%$ Resources:labels, PhoneNumber %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblBenPhone" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:labels, email %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblBenEmail" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <span><%=Resources.labels.noidungchuyenkhoan %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label15" runat="server" Text='<%$ Resources:labels, debitacc2 %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblSenderAccount" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label16" runat="server" Text='<%$ Resources:labels, sodutruockhighino %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblBalanceSender" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblSenderCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label17" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblFinalAmount" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblFinalFee" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lbCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label48" runat="server" Text='<%$ Resources:labels, loaiphi %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblPhi" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label45" runat="server" Text='<%$ Resources:labels, transactionfee %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblPhiAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblFeeCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                 <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label50" runat="server" Text='<%$ Resources:labels, tongtien %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblCCYIDTotalAmout" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label19" runat="server" Text='<%$ Resources:labels, PurposeOfRemittance %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        &nbsp;<asp:Label ID="lblPupose" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label22" runat="server" Text='<%$ Resources:labels, DetailsOfRemittance %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <div id="div6" style=" overflow-y:auto;overflow-x:auto; word-break:break-all; font-weight:400;"  runat="server">
                                <asp:Label ID="lblDetailPupose" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Repeater runat="server" ID="rptConfirmDocument">
                    <HeaderTemplate>
                        <table class="table table-bordered table-hover footable" data-paging="true">
                            <thead>
                                <tr>
                                    <th><%= Resources.labels.filename %></th>
                                    <th><%= Resources.labels.fileextension %></th>
                                    <th><%= Resources.labels.filesize %></th>
                                    <th><%= Resources.labels.desc%></th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("FileName") %>
                            </td>
                            <td>
                                <%#Eval("FileExtension") %>
                            </td>
                            <td>
                                <%#Eval("FileSize") %>
                            </td>
                            <td>
                                <%#Eval("Description") %>

                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
			            </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="row">
                <div class="col-xs-5 col-md-8">
                    <asp:CheckBox runat="server" ID="checkTerms" AutoPostBack="true" OnCheckedChanged="checkTerms_CheckedChanged"/>
                    <asp:Label runat="server">I have read and agree with the </asp:Label>
                    <a href="https://ib.hellomoney.com.la:9004/Term/SWIFTTerm_EN.html" target="_blank">Terms and Conditions</a>
                    <asp:Label runat="server"> of Siam Commercial Bank</asp:Label>
                </div>
            </div>
            <div style="text-align: center; margin-top: 10px;">
                <asp:Button ID="btnBack3" runat="server" Text='<%$ Resources:labels, quaylai %>' OnClick="btnBack3_Click" class="btn btn-warning" />
                <asp:Button ID="btnApply" runat="server" Text='<%$ Resources:labels, xacnhan %>' OnClick="btnApply_Click" class="btn btn-primary" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnOTP" runat="server" class="divcontent" Visible="false">
            <div class="handle">
                <span><%=Resources.labels.xacthucgiaodich %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <label class="bold"><%=Resources.labels.loaixacthuc %></label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:DropDownList ID="ddlLoaiXacThuc" OnSelectedIndexChanged="ddlLoaiXacThuc_SelectedIndexChanged" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-5 col-sm-4 left">
                        <asp:Panel ID="pnSendOTP" runat="server">
                            <asp:Button ID="btnSendOTP" runat="server" OnClick="btnSendOTP_Click" CssClass="btn btn-primary btnSendOTP " Text="<%$ Resources:labels, resend %>" />
                            <div class="countdown hidden">
                                <span style="font-weight: normal;"><%=Resources.labels.OTPcodeexpiresin %></span>&nbsp;<span class="countdown_time"></span>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <label class="bold"><%=Resources.labels.maxacthuc %></label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtOTP" runat="server" Width="100%" AutoCompleteType="None"></asp:TextBox>
                    </div>
                </div>
                <div class="row" style="text-align: center; color: #7A58BF">
                    <asp:Label ID="lbnotice" runat="server" class="bold" Visible="false">If you do not see the email in your inbox, please check your “junk mail” folder or “spam” folder.</asp:Label>
                </div>
            </div>
            <div class="button-group">
                <asp:Button ID="btnBackConfirm" runat="server" OnClick="btnBackConfirm_Click" CssClass="btn btn-warning" Text="<%$ Resources:labels, quaylai %>" />
                <asp:Button ID="btnAction" runat="server" OnClick="btnAction_Click" CssClass="btn btn-primary" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" Text="<%$ Resources:labels, thuchien %>" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnResultTransaction" runat="server" class="divcontent" Visible="false">
            <div class="handle">
                <asp:Label ID="Label21" runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
            </div>
            <div class="content">
                <asp:Panel ID="pnResultIND" runat="server" Visible="false">
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label24" runat="server" Text="<%$ Resources:labels, hotennguoitratien %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblResSendername" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label31" runat="server" Text="<%$ Resources:labels, idtype %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblResIdtype" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label52" runat="server" Text="<%$ Resources:labels, idnumber %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblResIdNumber" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label55" runat="server" Text="<%$ Resources:labels, countryofissue %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblResCountry" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label57" runat="server" Text="<%$ Resources:labels, expdate %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblResExpDate" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label60" runat="server" Text="<%$ Resources:labels, issdate %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblResIssDate" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label26" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <div id="div4" style=" overflow-y:auto;overflow-x:auto; word-break:break-all; font-weight:400;"  runat="server">
                            <asp:Label ID="lblResSenderAddress" runat="server"></asp:Label>
                                </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label28" runat="server" Text="<%$ Resources:labels, PhoneNumber %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblResSenderPhone" runat="server"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnResultMTK" runat="server" Visible="false">
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label30" runat="server" Text="<%$ Resources:labels, enterprisename %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblResEnterName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label32" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <div id="div5" style=" overflow-y:auto;overflow-x:auto; word-break:break-all; font-weight:400;"  runat="server">
                            <asp:Label ID="lblResEnterAddress" runat="server"></asp:Label>
                                </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label34" runat="server" Text="<%$ Resources:labels, PhoneNumber %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblResEnterPhone" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label36" runat="server" Text="<%$ Resources:labels, gpkd %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblResEnterLicense" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label38" runat="server" Text="<%$ Resources:labels, taxcode %>"></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblResEnterTaxCode" runat="server"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="handle">
                <span><%=Resources.labels.thongtinnguoinhantien %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label25" runat="server" Text="<%$ Resources:labels, BankName %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblResBankName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label29" runat="server" Text="<%$ Resources:labels, swiftcode %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblResSwiftCode" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label33" runat="server" Text="<%$ Resources:labels, tennguoithuhuong %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblResReceiverName" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label37" runat="server" Text="<%$ Resources:labels, taikhoanthuhuong %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblResReceiverAcc" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label41" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblResReceiverAdd" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label43" runat="server" Text="<%$ Resources:labels, PhoneNumber %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblResReceiverPhone" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label46" runat="server" Text="<%$ Resources:labels, email %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblResReceiverEmail" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <span><%=Resources.labels.noidungchuyenkhoan %></span>
            </div>
            <div class="content_table">
                <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="lblendtransaction" runat="server" Text='<%$ Resources:labels, sogiaodich %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEndTransactionNo" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-6 right">
                            <asp:Label ID="Label27" runat="server" Text='<%$ Resources:labels, thoidiem %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-6 line30">
                            <asp:Label ID="lblEndDateTime" runat="server"></asp:Label>
                        </div>
                    </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label49" runat="server" Text='<%$ Resources:labels, debitacc2 %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblResSenderAcc" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label51" runat="server" Text='<%$ Resources:labels, sodutruockhighino %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblResBalance" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblResCCYIDbalance" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label54" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblResAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblResCCYIDamount" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label59" runat="server" Text='<%$ Resources:labels, loaiphi %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblResFeeType" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label61" runat="server" Text='<%$ Resources:labels, transactionfee %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblResFeeAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblResCCYITFee" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label53" runat="server" Text='<%$ Resources:labels, tongtien %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <asp:Label ID="lblResTotalAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblResCCYIDTongtien" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label64" runat="server" Text='<%$ Resources:labels, PurposeOfRemittance %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        &nbsp;<asp:Label ID="lblResPurpo" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label66" runat="server" Text='<%$ Resources:labels, DetailsOfRemittance %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-md-6 line30">
                        <div id="div7" style=" overflow-y:auto;overflow-x:auto; word-break:break-all; font-weight:400;"  runat="server">
                                <asp:Label ID="lblResDetailPurpo" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div style="text-align: center; margin-top: 10px;">
                <asp:Button ID="btnView" runat="server"
                    Text="<%$ Resources:labels, viewphieuin %>" class="btn btn-primary" OnClick="btnView_Click" OnClientClick="javascript:return poponloadview()" />
                &nbsp;
            <asp:Button ID="btnPrint" runat="server" OnClientClick="javascript:return poponload()" OnClick="btnPrint_Click"
                Text="<%$ Resources:labels, inketqua %>" class="btn btn-warning" />
                &nbsp;
            <asp:Button ID="btnNew" runat="server" Text='<%$ Resources:labels, lammoi %>' class="btn btn-success" OnClick="btnNew_Click" />
            </div>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnNext" />
        <asp:PostBackTrigger ControlID="btnUpLoadFile" />
        <asp:PostBackTrigger ControlID="btnNext1"/>
    </Triggers>
</asp:UpdatePanel>
<script type="text/javascript">
    function validate() {
        var value = document.getElementById('hdUserType').value;
        if (value == 'IND') {
            if (validateEmpty('<%=txtSenderName.ClientID %>', '<%=Resources.labels.taikhoandichkhongrong %>', '<%=lblError.ClientID %>')) {
                if (validateEmpty('<%=txtSenderAddress.ClientID %>', '<%=Resources.labels.taikhoandichkhongrong %>', '<%=lblError.ClientID %>')) {
                    if (validateEmpty('<%=txtSenderPhone.ClientID %>', '<%=Resources.labels.taikhoandichkhongrong %>', '<%=lblError.ClientID %>')) {
                    }
                    else {
                        document.getElementById('<%=txtSenderPhone.ClientID %>').focus();
                        return false;
                    }
                }
                else {
                    document.getElementById('<%=txtSenderAddress.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtSenderName.ClientID %>').focus();
                return false;
            }
        }
        else {
            if (validateEmpty('<%=txtEnterName.ClientID %>', '<%=Resources.labels.taikhoandichkhongrong %>', '<%=lblError.ClientID %>')) {
                if (validateEmpty('<%=txtEnterPhone.ClientID %>', '<%=Resources.labels.taikhoandichkhongrong %>', '<%=lblError.ClientID %>')) {
                    if (validateEmpty('<%=txtEnterAddress.ClientID %>', '<%=Resources.labels.taikhoandichkhongrong %>', '<%=lblError.ClientID %>')) {
                        if (validateEmpty('<%=txtEnterLicense.ClientID %>', '<%=Resources.labels.taikhoandichkhongrong %>', '<%=lblError.ClientID %>')) {
                            if (validateEmpty('<%=txtEnterTax.ClientID %>', '<%=Resources.labels.taikhoandichkhongrong %>', '<%=lblError.ClientID %>')) {
                            }
                            else {
                                document.getElementById('<%=txtEnterTax.ClientID %>').focus();
                                return false;
                            }
                        } else {
                            document.getElementById('<%=txtEnterLicense.ClientID %>').focus();
                            return false;
                        }
                    }
                    else {
                        document.getElementById('<%=txtEnterAddress.ClientID %>').focus();
                        return false;
                    }
                }
                else {
                    document.getElementById('<%=txtEnterPhone.ClientID %>').focus();
                    return false;
                }
            } else {
                document.getElementById('<%=txtEnterName.ClientID %>').focus();
                return false;
            }
        }
        if (validateEmpty('<%=txtReceiverName.ClientID %>', '<%=Resources.labels.taikhoandichkhongrong %>', '<%=lblError.ClientID %>')) {
            if (validateEmpty('<%=txtReceiverAcc.ClientID %>', '<%=Resources.labels.taikhoandichkhongrong %>', '<%=lblError.ClientID %>')) {
                if (validateEmpty('<%=txtReceiverPhone.ClientID %>', '<%=Resources.labels.taikhoandichkhongrong %>', '<%=lblError.ClientID %>')) {
                    if (validateEmpty('<%=txtReceiverAdd.ClientID %>', '<%=Resources.labels.taikhoandichkhongrong %>', '<%=lblError.ClientID %>')) {
                        if (validateEmpty('<%=txtReceiverMail.ClientID %>', '<%=Resources.labels.taikhoandichkhongrong %>', '<%=lblError.ClientID %>')) {

                        } else {
                            document.getElementById('<%=txtReceiverMail.ClientID %>').focus();
                            return false;
                        }
                    } else {
                             document.getElementById('<%=txtReceiverAdd.ClientID %>').focus();
                        return false;
                    }
                }
                else {
                    document.getElementById('<%=txtReceiverPhone.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtReceiverAcc.ClientID %>').focus();
                return false;
            }
        } else {
            document.getElementById('<%=txtReceiverName.ClientID %>').focus();
            return false;
        }
    };
    function poponload() {
        testwindow = window.open("widgets/IBCBTransfer/print.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    };
    function poponloadview() {
        testwindow = window.open("widgets/IBCBTransfer/viewprint.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    };
    function ValidateFileLimit() {
        var fileCount = document.getElementById('fileDocument').files.length;
        if (fileCount > 10) {
            alert("Please select only 10 files!!!");
            return false;
        }
        else if (fileCount <= 0) {
            alert("Please select at least 1 file!!!");
            return false;
        }
        return true;
    };
    function isNumberKeyNumer(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    };
    function replaceAll(str, from, to) {
        var idx = str.indexOf(from);
        while (idx > -1) {
            str = str.replace(from, to);
            idx = str.indexOf(from);
        }
        return str;
    };
    function ntt(sNumber, idDisplay, event) {

        executeComma(sNumber, event);

        if (document.getElementById(sNumber).value == "") {
            document.getElementById(idDisplay).innerHTML = "";
            return;
        }

        document.getElementById(idDisplay).innerHTML = "(" + number2text(replaceAll(document.getElementById(sNumber).value, ",", ""), '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>') + ")";
        document.getElementById('<%=txtChu.ClientID %>').value = number2text(replaceAll(document.getElementById(sNumber).value, ",", ""), '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>');
    };
</script>

