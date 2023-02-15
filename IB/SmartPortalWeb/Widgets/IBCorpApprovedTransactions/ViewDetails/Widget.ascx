<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpApprovedTransactions_ViewDetails_Widget" %>
<%@ Import Namespace="SmartPortal.Common.Utilities" %>

<asp:Label ID="lblTable" runat="server" Visible="false"></asp:Label>
<div class="al">
    Approved transactions list<br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<div id="divError">
    <asp:Label runat="server" ID="lblError" Font-Bold="true" ForeColor="Red"></asp:Label>
</div>
<div class="divcontent" id="pnDefault" runat="server">
    <div class="content_table_4c_cl">
        <asp:Panel ID="pnInfo" runat="server">
            <legend class="handle"><%=Resources.labels.thongtingiaodich %></legend>
            <div class="row">
                <div class="col-xs-6 col-sm-3 line30 ">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, magiaodich %>"></asp:Label>
                </div>
                <div class="col-xs-6 col-sm-3 line30">
                    <asp:Label ID="lblTransID" runat="server"></asp:Label>
                </div>
                <div class="col-xs-6 col-sm-3 line30 ">
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, ngaygiogiaodich %>"></asp:Label>
                </div>
                <div class="col-xs-6 col-sm-3 line30">
                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 col-sm-3 line30 ">
                    <span><%=Resources.labels.loaigiaodich %></span>
                </div>
                <div class="col-xs-6 col-sm-3 line30">
                    <asp:Label ID="lblPagename" runat="server"></asp:Label>
                </div>
                <div class="col-xs-6 col-sm-3 line30 ">
                    <span><%=Resources.labels.sogiaodichcore %></span>
                </div>
                <div class="col-xs-6 col-sm-3 line30">
                    <asp:Label ID="lblReftype" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 col-sm-3 line30 ">
                    <asp:Label ID="Label54" runat="server" Text="<%$ Resources:labels, nguoithuchien %>"></asp:Label>
                </div>
                <div class="col-xs-6 col-sm-3 line30">
                    <asp:Label ID="lblUserCreate" runat="server"></asp:Label>
                </div>
                <div class="col-xs-6 col-sm-3 line30 ">
                    <asp:Label ID="Label52" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
                </div>
                <div class="col-xs-6 col-sm-3 line30">
                    <asp:Label ID="lblStatus" Font-Bold="true" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnSender" runat="server">
            <legend class="handle"><%=Resources.labels.thongtintaikhoanchuyen %></legend>
            <div class="row">
                <div class="col-xs-6 col-sm-3 line30 ">
                    <asp:Label ID="lblSender" runat="server" Text="<%$ Resources:labels, debitaccount %>"></asp:Label>
                </div>
                <div class="col-xs-6 col-sm-3 line30">
                    <asp:Label ID="lblAccount" runat="server"></asp:Label>
                </div>
                <div class="col-xs-6 col-sm-3 line30 ">
                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:labels, hotennguoitratien %>"></asp:Label>
                </div>
                <div class="col-xs-6 col-sm-3 line30">
                    <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </div>
    <asp:Panel ID="pnBatch" runat="server" Visible="false">
        <figure>
            <legend class="handle"><%=Resources.labels.chitietgiaodich %></legend>
            <div class="row">
                <div style="overflow: auto; height: 300px; width: 100%; padding-top: 10px;">
                    <asp:Repeater runat="server" ID="gvConfirm">
                        <HeaderTemplate>
                            <table class="table table-bordered table-hover footable" data-paging="true">
                                <thead>
                                    <tr>
                                        <th><%=Resources.labels.stt %></th>
                                        <th><%=Resources.labels.sotaikhoan %></th>
                                        <th><%=Resources.labels.nguoithuhuong %></th>
                                        <th><%=Resources.labels.sotien %></th>
                                        <th><%=Resources.labels.mota %></th>
                                        <th><%=Resources.labels.trangthai %></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("STT") %></td>
                                <td><%#Eval("Account") %></td>
                                <td><%#Eval("User") %></td>
                                <td style="text-align: right"><%#Utility.FormatMoney(Eval("Amount").ToString(), "LAK") %></td>
                                <td><%#Eval("Desc") %></td>
                                <td><%#Eval("Status") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
        </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </figure>
    </asp:Panel>
    <div class="content_table_4c_cl">
        <figure>

            <asp:Panel ID="pnReceiver" runat="server">
                <legend class="handle"><%=Resources.labels.thongtinnguoinhan %></legend>
                <div class="row">
                    <div class="col-xs-6 col-sm-3 line30 ">
                        <asp:Label ID="lblReceiver" runat="server" Text="<%$ Resources:labels, taikhoanbaoco %>"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30 height30">
                        <asp:Label ID="lblReceiverAccount" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30 right ">
                        <asp:Label ID="Label16" runat="server" Text="<%$ Resources:labels, hotennguoinhantien %>"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30 height30">
                        <asp:Label ID="lblReceiverName" runat="server"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnOpenBank" runat="server" Visible="false">
                <legend class="handle"><%=Resources.labels.openbankaccount %></legend>

                <div class="row">
                    <div class="col-xs-6 col-sm-3 line30 ">
                        <span><%=Resources.labels.category %> </span>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30">
                        <asp:Label ID="lblTerm" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30 ">
                        <span><%=Resources.labels.interestrate %> </span>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30">
                        <asp:Label ID="lblinterestrate" runat="server"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnBillPayment" runat="server" Visible="false">
                <legend class="handle"><%=Resources.labels.thongtinhoadon %></legend>
                <div class="row">
                    <div class="col-xs-6 col-sm-3 line30 ">
                        <span><%=Resources.labels.billername %></span>
                    </div>
                    <div class="col-xs-6 col-sm-9 line30">
                        <asp:Label ID="lblBillerName" runat="server"></asp:Label>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnTopup" runat="server" Visible="false">
                <legend class="handle"><%=Resources.labels.thongtintopup %></legend>
                <div class="row">
                    <div class="col-xs-6 col-sm-3 line30 ">
                        <span><%=Resources.labels.phone %></span>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30">
                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30 ">
                        <span><%=Resources.labels.telco %></span>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30">
                        <asp:Label ID="lblTelco" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 col-sm-3 line30 ">
                        <span><%=Resources.labels.cardamount %></span>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30">
                        <asp:Label ID="lblCardAmount" runat="server"></asp:Label>
                    </div>
                </div>
            </asp:Panel>


            <asp:Panel runat="server">
                <legend class="handle"><%=Resources.labels.noidungthanhtoan %></legend>
                <div class="row">
                    <div class="col-xs-6 col-sm-3 line30 ">
                        <asp:Label ID="lblSoTien" runat="server" Text="<%$ Resources:labels, sotien %>"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30">
                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                        &nbsp;
                        <asp:Label ID="lblCCYID" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30 ">
                        <asp:Label ID="lblSoTienPhi" runat="server" Text="<%$ Resources:labels, sotienphi %>"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30">
                        <asp:Label ID="lblFee" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblCCYIDPhi" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 col-sm-3 line30 ">
                        <span><%=Resources.labels.sotienbangchu %></span>
                    </div>
                    <div class="col-xs-6 col-sm-9 line30">
                        <asp:Label ID="lblstbc" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 col-sm-3 line30">
                        <asp:Label ID="Label15" runat="server" Text="<%$ Resources:labels, mota %>"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-9 line30">
                        <asp:Label ID="lblDesc" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-3 col-sm-3">
                        <label class="bold"><%= Resources.labels.document %></label>
                    </div>
                    <div class="col-xs-3 col-sm-3 line30 height30">
                        <asp:HyperLink Target="_blank" runat="server" Visible="false" Text="Click to View Details" ID="lblDocumentLink" CssClass="col-xs-3 col-sm-10 line30 height30"></asp:HyperLink>
                    </div>
                </div>
                <div class="row hidden">
                    <div class="col-xs-6 col-sm-3 line30 ">
                        <asp:Label ID="Label58" runat="server" Text="<%$ Resources:labels, ketqua %>"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-9 line30">
                        <asp:Label ID="lblResult" runat="server"></asp:Label>
                    </div>
                </div>
            </asp:Panel>


            <asp:Panel ID="pnScheduleInfor" runat="server" Visible="false">
                <legend class="handle"><%= Resources.labels.thongtinlich %></legend>
                <div class="row">
                    <div class="col-xs-6 col-sm-3 line30">
                        <span><%= Resources.labels.tenlich %></span>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30">
                        <asp:Label ID="lblScheduleName" runat="server"></asp:Label>&nbsp;
                    </div>
                    <div class="col-xs-6 col-sm-3 line30">
                        <span><%= Resources.labels.kieulich %></span>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30">
                        <asp:Label ID="lblScheduleType" runat="server"></asp:Label>&nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 col-sm-3 line30">
                        <span><%= Resources.labels.tungay %></span>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30">
                        <asp:Label ID="lblFromDate" runat="server"></asp:Label>&nbsp;
                    </div>
                    <div class="col-xs-6 col-sm-3 line30">
                        <span><%= Resources.labels.denngay %></span>
                    </div>
                    <div class="col-xs-6 col-sm-3 line30">
                        <asp:Label ID="lblToDate" runat="server"></asp:Label>&nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 col-sm-3 line30">
                        <span><%= Resources.labels.thoidiemthuchienketiep %></span>
                    </div>
                    <div class="col-xs-6 col-sm-9 line30">
                        <asp:Label ID="lblNextExecute" runat="server"></asp:Label>&nbsp;
                    </div>
                </div>
            </asp:Panel>
        </figure>
    </div>

    <asp:Panel ID="pnWF" runat="server">
        <figure>
            <legend class="handle"><%=Resources.labels.approvalworkflowinfo %></legend>
            <div class="">
                <div style="text-align: center; margin-top: 10px;">
                    <asp:Literal ID="ltrWF" runat="server"></asp:Literal>
                </div>
            </div>
        </figure>
    </asp:Panel>
    <asp:Panel ID="pnDetail" runat="server">
        <figure>
            <legend class="handle"><%=Resources.labels.chitietduyetgiaodich %></legend>
            <div class="">
                <div style="text-align: center; margin-top: 10px;">
                    <asp:Literal ID="ltrTH" runat="server"></asp:Literal>
                </div>
            </div>
        </figure>
    </asp:Panel>
    <asp:Panel ID="pnGroup" runat="server">
        <figure>
            <legend class="handle"><%=Resources.labels.listuserwaitingforapprove %></legend>
            <div class="">
                <div style="text-align: center; margin-top: 10px;">
                    <asp:Literal ID="ltrGR" runat="server"></asp:Literal>
                </div>
            </div>
        </figure>
    </asp:Panel>
    <asp:Panel ID="pnDocument" Visible="false" runat="server">
        <figure>
            <legend class="handle"><%=Resources.labels.document %></legend>
            <div class="">
                <div style="text-align: center; margin-top: 10px;">
                    <asp:Repeater runat="server" ID="rptDocument" OnItemCommand="rptDocument_ItemCommand">
                        <HeaderTemplate>
                            <div class="pane">
                                <div class="table-responsive">
                                    <table class="table table-hover footable c_list">
                                        <thead style="background-color: #7A58BF; color: #FFF;">
                                            <tr>
                                                <th class="title-repeater"><%=Resources.labels.DocumentName %></th>
                                                <th class="title-repeater"><%=Resources.labels.DocumentType %></th>
                                                <th class="title-repeater"><%=Resources.labels.view %></th>
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
                </div>
            </div>
        </figure>
    </asp:Panel>
    <asp:Panel ID="pnButton" runat="server">
        <figure>
            <div class="button-group">
                <asp:Button ID="Button3" runat="server" CssClass="btn btn-warning" Text='<%$ Resources:labels, quaylai %>' OnClick="btnBack_OnClick" />
                <asp:Button ID="btnPrint" runat="server" OnClientClick="javascript:return poponload()" CssClass="btn btn-warning" Text="<%$ Resources:labels, inketqua %>" />
            </div>
        </figure>
    </asp:Panel>
</div>

<script type="text/javascript">
    function poponload() {
        testwindow = window.open("widgets/IBCorpApprovedTransactions/print.aspx?pt=P&cul=" +'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=800,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
</script>
