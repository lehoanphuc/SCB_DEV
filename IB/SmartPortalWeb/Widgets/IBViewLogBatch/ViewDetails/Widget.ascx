<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBListTransWaitApprove_ViewDetails_Widget" %>

<link href="CSS/css.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager ID="ScriptManager1" runat="server">
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
<div class="al">
    <span>Batch transfer information</span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError" style="text-align: center; color: Red; margin-top: 10px">
            <asp:Label ID="lblError" CssClass="bold" runat="server"></asp:Label>
        </div>
        <figure>
            <legend class="handle"><%=Resources.labels.chitietgiaodich %></legend>
            <div class="content">
                <div class="row">
                    <label class="bold col-xs-4 col-sm-2"><%=Resources.labels.magiaodich %></label>
                    <asp:Label ID="lblTransID" runat="server" CssClass="col-xs-8 col-sm-10 line30"></asp:Label>
                </div>
                <div class="row">
                    <div class="header-title">
                        <label class="bold"><%=Resources.labels.thongtintaikhoan %></label>
                    </div>
                    <div class="row">
                        <label class="bold col-xs-4 col-sm-2"><%=Resources.labels.taikhoanghino %></label>
                        <asp:Label ID="lblConfirmSenderAcctno" runat="server" Font-Bold="False" CssClass="col-xs-8 col-sm-10 line30"></asp:Label>

                    </div>
                    <div class="row">
                        <label class="bold col-xs-4 col-sm-2"><%=Resources.labels.tentaikhoan %></label>
                        <asp:Label ID="lblConfirmSenderName" runat="server" Font-Bold="False" CssClass="col-xs-8 col-sm-10 line30"></asp:Label>

                    </div>
                    <div class="row" id="amount" runat="server">
                        <label class="bold col-xs-4 col-sm-2"><%=Resources.labels.sodu %></label>
                        <div class="col-xs-8 col-sm-10 line30">
                            <asp:Label ID="lblConfirmBalance" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblCCYID" runat="server"></asp:Label>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="header-title">
                        <label class="bold"><%=Resources.labels.noidungthanhtoan %></label>
                    </div>
                    <div class="row">
                        <label class="bold col-xs-4 col-sm-2"><%=Resources.labels.noidung %></label>
                        <asp:Label ID="lblContent" runat="server" Font-Bold="False" CssClass="col-xs-8 col-sm-10 line30"></asp:Label>
                    </div>
                    <div class="row">
                        <asp:Repeater runat="server" ID="rptConfirm">
                            <HeaderTemplate>
                                <table class="table table-bordered table-hover footable" data-paging="true" style="background-color: white; border-color: rgb(204, 204, 204); border-width: 1px; border-style: none; border-collapse: collapse; max-width: 1000px;">
                                    <thead>
                                        <tr style="background-color: #009CD4; font-weight: bold;">
                                            <th><%= Resources.labels.stt %></th>
                                            <th data-breakpoints="xs"><%= Resources.labels.sotaikhoan %></th>
                                            <th><%= Resources.labels.nguoithuhuong %></th>
                                            <th data-breakpoints="xs sm"><%= Resources.labels.sotien %></th>
                                            <th data-breakpoints="xs sm"><%= Resources.labels.mota %></th>
                                            <th><%= Resources.labels.trangthai %></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("STT") %></td>
                                    <td><%#Eval("Account") %></td>
                                    <td><%#Eval("User") %></td>
                                    <td><%#Eval("Amount") %></td>
                                    <td><%#Eval("Desc") %></td>
                                    <td><%#Eval("Status") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
            </table>
                            </FooterTemplate>
                        </asp:Repeater>

                    </div  
                        <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <label class="bold "><%=Resources.labels.document %> : </label>
                            <asp:Label ID="Label1" runat="server" Font-Bold="true"><asp:HyperLink runat="server" ID="lblDocumentLink" Target="_blank"></asp:HyperLink></asp:Label>
                           
                        </div> 
                     </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <label class="bold "><%=Resources.labels.tongphi %> : </label>
                            <asp:Label ID="lblTotalFee" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="col-xs-12 col-sm-10">
                            <label class="bold "><%=Resources.labels.tongtien %> : </label>
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblTable" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </figure>

        <asp:Panel ID="pn_approve" runat="server">
            <asp:Panel runat="server" ID="pnToken">
                <figure>
                    <legend class="handle"><%=Resources.labels.thongtinxacthuc %></legend>
                    <div class="content">
                        <div class="row">
                            <div class="col-xs-5 col-sm-2">
                                <label class="bold"><%=Resources.labels.loaixacthuc %></label>
                            </div>
                            <%--<div class="col-xs-7 col-sm-4">
                                <asp:DropDownList ID="ddlAuthenType" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-5 col-sm-2">
                                <label class="bold"><%=Resources.labels.maxacthuc %></label>
                            </div>
                            <div class="col-xs-7 col-sm-3">
                                <asp:TextBox ID="txtAuthenCode" runat="server"></asp:TextBox>
                            </div>--%>
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
                        <div class="row" style="text-align:center; color:#7A58BF">
                            <asp:Label ID="lbnotice" runat="server" class="bold" Visible="false">If you do not see the email in your inbox, please check your “junk mail” folder or “spam” folder.</asp:Label>
                        </div>
                    </div>
                </figure>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnDesc">
                <figure>
                    <legend class="handle"><%=Resources.labels.thongtinxacthuc %></legend>
                    <div class="content">
                        <div class="col-xs-5 col-sm-3">
                            <label class="bold"><%=Resources.labels.diengiai %></label>
                        </div>
                        <div class="col-xs-7 col-sm-9">
                            <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </div>
                    </div>
                </figure>
            </asp:Panel>

            <!-- PostBackUrl="javascript:history.go(-1)"-->
            <div style="text-align: center; margin-top: 10px;">
                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click"  CssClass="btn btn-warning"
                    Text="<%$ Resources:labels, quaylai %>" />
                <asp:Button ID="btnPrevious" runat="server" OnClick="btnPrevious_Click" CssClass="btn btn-info"
                    Text="<%$ Resources:labels, giaodichtruoc %>" />
                <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" CssClass="btn btn-primary"
                    Text="<%$ Resources:labels, duyet %>" />
                <asp:Button ID="btnReject" runat="server" OnClick="btnReject_Click" CssClass="btn btn-danger"
                    OnClientClick="return reject();" Text="<%$ Resources:labels, khongduyet %>"
                    Width="74px" />
                <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" CssClass="btn btn-success"
                    Text="<%$ Resources:labels, giaodichketiep %>" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pn_View" runat="server">
            <asp:Button ID="btnPrint" runat="server" Visible="false" Text="<%$ Resources:labels, inthongtin %> "
                OnClientClick="javascript:return poponload()" CssClass="btn btn-info" />
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning"
                Text="<%$ Resources:labels, quaylai %>" OnClick="Button1_Click" />

        </asp:Panel>

        <script type="text/javascript">
            function poponload() {
                testwindow = window.open("widgets/SEMSViewLogBatch/print.aspx?cul=" +'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
                    "menubar=1,scrollbars=1,width=700,height=650");
                testwindow.moveTo(0, 0);
                return false;
            }
            function reject() {
                if (document.getElementById('<%=txtDesc.ClientID %>').value == '') {
                    window.alert('<%=Resources.labels.banvuilongnhaplydokhongduyetgiaodichnay %>');
                    document.getElementById('<%=txtDesc.ClientID %>').focus();
                    return false;
                }
            }
        </script>
    </ContentTemplate>
</asp:UpdatePanel>
