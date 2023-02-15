<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBListTransWaitApprove_ViewDetails_Widget" %>
<link href="/CSS/css.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:HiddenField ID="hdTranCode" runat="server" />
<asp:TextBox ID="txusertosend" Visible="false" runat="server"></asp:TextBox>
<asp:TextBox ID="txsenderbranch" Visible="false" runat="server"></asp:TextBox>
<asp:TextBox ID="txreceiverbranch" Visible="false" runat="server"></asp:TextBox>
<asp:Label ID="lblamountchu" runat="server" Visible="false" Text=""></asp:Label>
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
<div class="al">
    <span><%=Resources.labels.danhsachgiaodichchoduyet %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<div id="divError" style="text-align: center; color: Red;">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>
<figure class="divcontent">
    <div class="content_table_4c_cl display-label">
        <asp:Panel ID="pnInfo" runat="server">
            <legend class="handle"><%=Resources.labels.thongtingiaodich %></legend>
            <div class="row">
                <div class="col-xs-6 col-sm-3 line30">
                    <label class="bold"><%= Resources.labels.magiaodich %></label>
                </div>
                <div class="col-xs-6 col-sm-3 line30">
                    <asp:Label ID="lblTransID" runat="server"></asp:Label>
                </div>
                <div class="col-xs-6 col-sm-3 line30">
                    <label class="bold"><%= Resources.labels.ngaygiogiaodich %></label>
                </div>
                <div class="col-xs-6 col-sm-3 line30">
                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 col-sm-3 line30">
                    <span><%=Resources.labels.loaigiaodich %></span>
                </div>
                <div class="col-xs-12 col-sm-9 line30">
                    <asp:Label ID="tranname" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-5 col-sm-3">
                    <label class="bold"><%= Resources.labels.nguoithuchien %></label>
                </div>
                <div class="col-xs-7 col-sm-3 line30 height30">
                    <asp:Label ID="lblUserCreate" runat="server"></asp:Label>
                </div>
                <div class="col-xs-5 col-sm-3">
                    <label class="bold"><%= Resources.labels.trangthai %></label>
                </div>
                <div class="col-xs-7 col-sm-3 line30 height30">
                    <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnSender" runat="server">
            <legend class="handle"><%=Resources.labels.thongtintaikhoanchuyen %></legend>
            <div class="row">
                <div class="col-xs-5 col-sm-3">
                    <label class="bold"><%= Resources.labels.debitaccount %></label>
                </div>
                <div class="col-xs-7 col-sm-3 line30 height30">
                    <asp:Label ID="lblAccount" runat="server"></asp:Label>
                </div>
                <div class="col-xs-5 col-sm-3">
                    <label class="bold"><%= Resources.labels.hotennguoitratien %></label>
                </div>
                <div class="col-xs-7 col-sm-3 line30 height30">
                    <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnReceiver" runat="server">
            <legend class="handle"><%=Resources.labels.thongtinnguoinhan %></legend>
            <div class="row">
                <div class="col-xs-5 col-sm-3">
                    <label class="bold"><%= Resources.labels.taikhoanbaoco %></label>
                </div>
                <div class="col-xs-7 col-sm-3 line30 height30">
                    <asp:Label ID="lblAccountNo" runat="server"></asp:Label>
                </div>
                <div class="col-xs-5 col-sm-3">
                    <label class="bold"><%= Resources.labels.hotennguoinhantien %></label>
                </div>
                <div class="col-xs-7 col-sm-3 line30 height30">
                    <asp:Label ID="lblReceiverName" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row hidden">
                <div class="col-xs-5 col-sm-3">
                    <label class="bold"><%= Resources.labels.diachinguoinhantien %></label>
                </div>
                <div class="col-xs-7 col-sm-9 line30 height30">
                    <asp:Label ID="lblReceiverAdd" runat="server"></asp:Label>
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

            <div class="row" id="payment2" runat="server" visible="False">
                <div class="col-xs-5 col-sm-3">
                    <asp:Label CssClass="bold" ID="lblRef1Name" runat="server"></asp:Label>
                </div>
                <div class="col-xs-7 col-sm-3 line30 height30">
                    <asp:Label ID="lblRef1" runat="server"></asp:Label>
                </div>
                <div class="col-xs-5 col-sm-3">
                    <asp:Label CssClass="bold" ID="lblRef2Name" runat="server"></asp:Label>
                </div>
                <div class="col-xs-7 col-sm-3 line30 height30">
                    <asp:Label ID="lblRef2" runat="server"></asp:Label>
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
                <div class="col-xs-5 col-sm-3">
                    <label class="bold"><%= Resources.labels.sotien %></label>
                </div>
                <div class="col-xs-7 col-sm-3 line30 height30">
                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                    <asp:Label ID="lblCCYID" runat="server"></asp:Label>
                </div>
                <div class="col-xs-5 col-sm-3">
                    <label class="bold"><%= Resources.labels.sotienphi %></label>
                </div>
                <div class="col-xs-7 col-sm-3 line30 height30">
                    <asp:Label ID="lblFee" runat="server"></asp:Label>
                    <asp:Label ID="lblCCYIDPhi" runat="server"></asp:Label>
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
                <div class="col-xs-5 col-sm-3">
                    <label class="bold"><%= Resources.labels.mota %></label>
                </div>
                <div class="col-xs-7 col-sm-9 line30 height30">
                    <asp:Label ID="lblDesc" runat="server" CssClass="col-xs-7 col-sm-10 line30 height30"></asp:Label>
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

        <div class="row hidden">
            <div class="col-xs-5 col-sm-3">
                <label class="bold"><%= Resources.labels.status %></label>
            </div>
            <div class="col-xs-7 col-sm-3 line30 height30">
                <asp:Label ID="lblResult" runat="server"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-3">
                <label class="bold"><%= Resources.labels.nguoiduyet %></label>
            </div>
            <div class="col-xs-7 col-sm-3 line30 height30">
                <asp:Label ID="lblAppSts" runat="server"></asp:Label>
            </div>
        </div>
    </div>
</figure>
<div class="al">
    <span><%=Resources.labels.chitietduyetgiaodich %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<div style="text-align: center; margin-top: 10px;">
    <asp:Repeater runat="server" ID="rptLTWA">
        <HeaderTemplate>
            <table class="table table-bordered table-hover footable" data-paging="true" style="background-color: white; border-color: rgb(204, 204, 204); border-width: 1px; margin-bottom: 0;">
                <thead>
                    <tr>
                        <th><%= Resources.labels.order %></th>
                        <th><%= Resources.labels.ngayduyet %></th>
                        <th><%= Resources.labels.manhanvien %></th>
                        <th><%= Resources.labels.tennhanvien %></th>
                        <th><%= Resources.labels.kieunhanvien %></th>
                        <th>Staff group</th>
                        <th><%= Resources.labels.status %></th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%#(((RepeaterItem)Container).ItemIndex+1).ToString() %></td>
                <td><%#Eval("IPCTRANSDATE") %></td>
                <td><%#Eval("USERNAME") %></td>
                <td><%#Eval("FULLNAME") %></td>
                <td><%#Eval("USERTYPE") %></td>
                <td><%#Eval("GROUPNAME") %></td>
                <td><%#Eval("APPRSTS").ToString() == SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED ? Resources.labels.daduyet : Eval("APPRSTS").ToString() == SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED ? Resources.labels.reject : "" %></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>
<br />
<asp:Label ID="lblnguoiduyet" runat="server" Visible="false" Text=""></asp:Label>
<asp:Panel ID="pnToken" runat="server">
    <figure>
        <legend class="handle"><%=Resources.labels.thongtinduyetgiaodich %></legend>
        <div class="content">
            <div class="row">
                <div class="col-xs-5 col-sm-2">
                    <label class="bold"><%=Resources.labels.loaixacthuc %></label>
                </div>
                <div class="col-xs-7 col-sm-3">
                    <asp:DropDownList ID="ddlAuthenType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAuthenType_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-offset-1 col-xs-5 col-sm-1">
                    <label class="bold"><%=Resources.labels.maxacthuc %></label>
                </div>
                <div class="col-xs-7 col-sm-3">
                    <asp:TextBox ID="txtAuthenCode" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-5 col-sm-2">
                    <asp:Button ID="btnSendOTP" runat="server" Text="Send OTP" OnClick="btnSendOTP_Click" CssClass="btn btn-primary btnSendOTP"></asp:Button>
                    <div class="countdown hidden">
                        <span style="font-weight: normal;"><%=Resources.labels.OTPcodeexpiresin %></span>&nbsp;<span class="countdown_time"></span>
                    </div>
                </div>
            </div>
            <div class="row" style="text-align: center; color: #7A58BF">
                <asp:Label ID="lbnotice" runat="server" class="bold" Visible="false">If you do not see the email in your inbox, please check your “junk mail” folder or “spam” folder.</asp:Label>
            </div>
        </div>
    </figure>
</asp:Panel>
<asp:Panel ID="Panel1" runat="server">
    <figure>
        <legend class="handle"><%=Resources.labels.noidung %></legend>
        <div class="content">
            <div class="row">
                <div class="col-xs-5 col-sm-2">
                    <label class="bold"><%= Resources.labels.diengiai %></label>
                </div>
                <div class="col-xs-7 col-sm-9">
                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="417px"></asp:TextBox>
                </div>
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
                                <asp:LinkButton ID="lblDownload" OnClientClick=" return PostToNewWindow();" runat="server" CommandName="Download">View</asp:LinkButton>
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
<div style="text-align: center; margin: 10px;">
    <asp:Button ID="Button3" CssClass="btn btn-warning" runat="server" Text="<%$ Resources:labels, quaylai %>" OnClick="Button3_Click" />
    <asp:Button ID="btnPrevious" runat="server" OnClick="btnPrevious_Click" CssClass="btn btn-primary"
        Text="<%$ Resources:labels, giaodichtruoc %>" OnClientClick="Loading();" />
    <asp:Button ID="btnApprove" CssClass="btn btn-success" runat="server" OnClick="btnApprove_Click" Text="<%$ Resources:labels, duyet %>" />
    <asp:Button ID="btnReject" CssClass="btn btn-danger" runat="server" Text="<%$ Resources:labels, khongduyet %>" OnClick="btnReject_Click" OnClientClick="Loading(); return ConfirmReject(this);" />
    <asp:Button ID="btnNext" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, giaodichketiep %>" OnClick="btnNext_Click" OnClientClick="Loading();" />
    <div class="clearfix"></div>
</div>
<!--end-->
<script type="text/javascript">
    function ConfirmReject(btnReject) {
        if (document.getElementById('<%=txtDesc.ClientID %>').value == '') {
                    swalWarning('<%=Resources.labels.banvuilongnhaplydokhongduyetgiaodichnay %>');
            return false;
        } else {
            return sweetAlertConfirm(btnReject);
        }
    }

    function sweetAlertConfirm(btnReject) {
        if (btnReject.dataset.confirmed) {
            btnReject.dataset.confirmed = false;
            return true;
        } else {
            event.preventDefault();
            Swal.fire({
                title: 'Are you sure to reject?',
                text: "You won't be able to revert this!",
                type: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes, reject it!',
                confirmButtonColor: '#337ab7',
                cancelButtonColor: '#d9534f',
            })
                .then((result) => {
                    if (result.isConfirmed) {
                        btnReject.dataset.confirmed = true;
                        btnReject.click();
                    }
                });
        }
    }

    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
                    document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }
    function PostToNewWindow() {
        originalTarget = document.forms[0].target;
        document.forms[0].target = '_blank';
        window.setTimeout("document.forms[0].target=originalTarget;", 300);
        return true;
    }
</script>
