<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBViewLogTransactions_ViewDetails_Widget" %>
<style type="text/css">
    .style1 {
        width: 100%;
        background-color: #EAEDD8;
    }

    .tibtd {
    }

    .tibtdh {
        background-color: #EAFAFF;
        color: #003366;
        font-weight: bold;
    }

    .al {
        font-weight: bold;
        padding-left: 5px;
        padding-top: 10px;
        padding-bottom: 10px;
    }

    .thtdf {
        background-color: #EAFAFF;
        color: #003366;
        font-weight: bold;
        border-top: solid 1px #b9bfc1;
    }

    .thtdbold {
        background-color: #EAFAFF;
        color: #003366;
        font-weight: bold;
        border-left: solid 1px #b9bfc1;
        border-top: solid 1px #b9bfc1;
    }

    .thtd {
        border-left: solid 1px #b9bfc1;
        border-top: solid 1px #b9bfc1;
    }

    .style3 {
        border-left: solid 1px #b9bfc1;
        border-top: solid 1px #b9bfc1;
    }

    .divHeaderStyle {
        background-color: #EAFAFF;
        color: #003366;
        font-weight: bold;
        margin: 0px;
        line-height: 20px;
        padding: 5px;
    }

    .gvHeader {
        text-align: center;
    }
</style>

<div class="al">
    <span><%=Resources.labels.nhatkygiaodich %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<div id="divError" style="text-align: center; color: Red;">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>

<asp:Panel runat="server" ID="pnDefault">
    <figure id="Ownerdiv" runat="server">
        <legend class="handle"><%=Resources.labels.chitietgiaodich %></legend>
        <div class="content display-label">
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.sogiaodich %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblTransID" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.ngaygiogiaodich %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.debitaccount %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblAccount" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.hotennguoitratien %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-3 col-xs-5"><%= Resources.labels.sotien %></label>
                <div class="col-md-9 col-xs-7">
                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                    <asp:Label ID="lblCCYID" runat="server"></asp:Label>
                </div>
                <label class="col-md-3 col-xs-5"><%= Resources.labels.sotienphi %></label>
                <div class="col-md-9 col-xs-7">
                    <asp:Label ID="lblFee" runat="server"></asp:Label>
                    &nbsp;<asp:Label ID="lblCCYIDPhi" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.laiduochuong %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblLDH" runat="server"></asp:Label>
                    &nbsp;<asp:Label ID="lblCCYIDLDH" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.vat %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblVAT" runat="server"></asp:Label>
                    &nbsp;<asp:Label ID="lblCCYIDVAT" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-3 col-xs-5"><%= Resources.labels.mota %></label>
                <div class="col-md-9 col-xs-7">
                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                </div>
            </div>

            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.taikhoanbaoco %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblAccountNo" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.hotennguoinhantien %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblReceiverName" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.nganhang %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblBank" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.socmnd %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblLicense" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.ngaycap %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblIssueDate" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.noicap %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblIssuePlace" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.diachinguoinhantien %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblReceiverAdd" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.ngayduyet %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblApproveDate_0" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.trangthai %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.nguoiduyet %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblAppSts" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.nguoithuchien %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblUserCreate" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.ketqua %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblResult" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </figure>

    <asp:Panel ID="pnBil" runat="server" Visible="false">
        <figure id="Figure1" runat="server">
            <legend class="handle"><%=Resources.labels.chitiethoadon %></legend>

            <div class="content">
                <asp:GridView ID="gvLTWA" runat="server" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                    CellPadding="5" Width="100%" OnRowDataBound="gvLTWA_RowDataBound"
                    AllowPaging="True">
                    <RowStyle ForeColor="#000066" />
                    <Columns>
                        <asp:TemplateField HeaderText='<%$ Resources:labels, sohoadon %>'>
                            <ItemTemplate>
                                <asp:Label ID="lblsohoadon" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText='<%$ Resources:labels, sotien %>'>
                            <ItemTemplate>
                                <asp:Label ID="lblsotien" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText='<%$ Resources:labels, loaitien %>'>
                            <ItemTemplate>
                                <asp:Label ID="lblloaitien" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText='<%$ Resources:labels, trangthai %>'>
                            <ItemTemplate>
                                <asp:Label ID="lbltrangthai" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#009CD4" Font-Bold="True" />
                </asp:GridView>
                <asp:HiddenField ID="hdsodanhba" runat="server" />
                <asp:HiddenField ID="hddebitbranch" runat="server" />
                <asp:HiddenField ID="hdcreditbranch" runat="server" />
                <asp:HiddenField ID="hdaddresscholon" runat="server" />
                <asp:HiddenField ID="hdsotienbangchu" runat="server" />
            </div>
            <div class="button-group">
                <asp:Button ID="btnPrint" runat="server" Text='<%$ Resources:labels, inketqua %>' OnClientClick="javascript:return poponload()" />
                &nbsp;
                    <asp:Button ID="Button3" runat="server"
                        Text='<%$ Resources:labels, quaylai %>' PostBackUrl="javascript:history.go(-1)" />
            </div>
        </figure>
    </asp:Panel>
</asp:Panel>
<asp:Panel runat="server" ID="pnOTK">
    <figure id="Figure2" runat="server">
        <legend class="handle"><%=Resources.labels.chitietgiaodich %></legend>
        <div class="content display-label">
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.sogiaodich %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblTransID_OTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.ngaygiogiaodich %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblDate_OTK" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.debitaccount %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblAccount_OTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.hotennguoitratien %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblSenderName_OTK" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-3 col-xs-5"><%= Resources.labels.accountname %></label>
                <div class="col-md-9 col-xs-7">
                    <asp:Label ID="lblAccountName_FD" runat="server"></asp:Label>
                </div>
                <label class="col-md-3 col-xs-5"><%= Resources.labels.dateopened %></label>
                <div class="col-md-9 col-xs-7">
                    <asp:Label ID="lblDO_FD" runat="server" Text="11/01/2009"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.interestrate %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblIR_FD" runat="server" Text=""></asp:Label>
                    <asp:Label ID="Label75" runat="server"
                        Text="<%$ Resources:labels, percentyear %>"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.ExpireDate %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblLT_FD" runat="server" Text="11/01/2009"></asp:Label>
                </div>
            </div>


            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.kyhan %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblTerm_OTK" runat="server"></asp:Label>
                    &nbsp;<asp:Label ID="lblRTD" runat="server" Text="<%$ Resources:labels, thang %>"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.branch %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblBranch_DD" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.taikhoanbaoco %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblAccountNo_OTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.hotennguoinhantien %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblReceiverName_OTK" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.sotien %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblAmount_OTK" runat="server"></asp:Label>
                    <asp:Label ID="lblCCYID_OTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.sotienphi %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblFee_OTK" runat="server"></asp:Label>
                    &nbsp;<asp:Label ID="lblCCYIDPhi_OTK" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.sotienbangchu %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblstbc_OTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.vat %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblVAT_OTK" runat="server"></asp:Label>
                    &nbsp;<asp:Label ID="lblCCYIDVAT_OTK" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.mota %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblDesc_OTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.ngayduyet %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblApproveDate" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.nguoiduyetcuoi %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblLastApp_OTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.trangthai %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblStatus_OTK" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.nguoiduyet %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblAppSts_OTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.nguoithuchien %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblUserCreate_OTK" runat="server"></asp:Label>
                    &nbsp;
                                 <asp:Label ID="Label72" runat="server" Text="<%$ Resources:labels, ketqua %>" Visible="False"></asp:Label>
                    <asp:Label ID="lblResult_OTK" runat="server" Visible="False"></asp:Label>
                </div>

            </div>

            <div class="button-group">
                <asp:Button ID="btnPrintOTK" runat="server" Text='<%$ Resources:labels, inketqua %>' OnClientClick="javascript:return poponloadtk()" />
                &nbsp;
                    <asp:Button ID="btnExitOTK" runat="server"
                        Text='<%$ Resources:labels, quaylai %>' PostBackUrl="javascript:history.go(-1)" />
            </div>
        </div>
    </figure>

</asp:Panel>
<asp:Panel runat="server" ID="pnCTK">
    <figure id="Figure3" runat="server">
        <legend class="handle"><%=Resources.labels.chitietgiaodich %></legend>
        <div class="content display-label">
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.sogiaodich %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblTransID_CTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.ngaygiogiaodich %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblDate_CTK" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.debitaccount %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblAccount_CTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.hotennguoitratien %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblSenderName_CTK" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-3 col-xs-5"><%= Resources.labels.accountname %></label>
                <div class="col-md-9 col-xs-7">
                    <asp:Label ID="lblAccountName_CTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-3 col-xs-5"><%= Resources.labels.dateopened %></label>
                <div class="col-md-9 col-xs-7">
                    <asp:Label ID="lblDO_CTK" runat="server" Text="11/01/2009"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.interestrate %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblIR_CTK" runat="server"></asp:Label>
                    <asp:Label ID="Label33" runat="server"
                        Text="<%$ Resources:labels, percentyear %>"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.ExpireDate %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblED_CTK" runat="server" Text="11/01/2009"></asp:Label>
                </div>
            </div>


            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.laiduochuong %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblLDH_CTK" runat="server"></asp:Label>
                    &nbsp;<asp:Label ID="lblCCYIDLDH_CTK" runat="server"
                        Text=""></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.branch %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblBranch_CTK" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.taikhoanbaoco %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblAccountNo_CTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.hotennguoinhantien %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblReceiverName_CTK" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.sotien %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblAmount_CTK" runat="server"></asp:Label>
                    <asp:Label ID="lblCCYID_CTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.sotienphi %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblFee_CTK" runat="server"></asp:Label>
                    &nbsp;<asp:Label ID="lblCCYIDPhi_CTK" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.sotienbangchu %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblstbc_CTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.vat %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblVAT_CTK" runat="server"></asp:Label>
                    &nbsp;<asp:Label ID="lblCCYIDVAT_CTK" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.mota %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblDesc_CTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.ngayduyet %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblApproveDate_1" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.nguoiduyetcuoi %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblLastApp_CTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.trangthai %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblStatus_CTK" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-md-2 col-xs-5"><%= Resources.labels.nguoiduyet %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblAppSts_CTK" runat="server"></asp:Label>
                </div>
                <label class="col-md-2 col-xs-5"><%= Resources.labels.nguoithuchien %></label>
                <div class="col-md-4 col-xs-7">
                    <asp:Label ID="lblUserCreate_CTK" runat="server"></asp:Label>
                    &nbsp;
                                 <asp:Label ID="Label67" runat="server" Text="<%$ Resources:labels, ketqua %>" Visible="False"></asp:Label>
                    <asp:Label ID="lblResult_CTK" runat="server" Visible="False"></asp:Label>
                </div>

            </div>

        </div>
        <div class="button-group">
            <asp:Button ID="btnPrintCTK" runat="server" Text='<%$ Resources:labels, inketqua %>' OnClientClick="javascript:return poponloadctk()" />
            &nbsp;
                    <asp:Button ID="btnExitCTK" runat="server"
                        Text='<%$ Resources:labels, quaylai %>' PostBackUrl="javascript:history.go(-1)" />
        </div>
    </figure>

</asp:Panel>

<script type="text/javascript">
    function poponload() {
        testwindow = window.open("widgets/IBViewLogTransactions/print.aspx?pt=P&cul=" +'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=800,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
    function poponloadtk() {
        testwindow = window.open("widgets/IBCKVTKTKCKH/print.aspx?pt=P&cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
    function poponloadctk() {
        testwindow = window.open("widgets/SEMSViewLogTransactions/printctk.aspx?pt=P&cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=800,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
</script>
