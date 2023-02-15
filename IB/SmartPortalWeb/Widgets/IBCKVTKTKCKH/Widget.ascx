<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCKVTKTKCKH_Widget" %>


<script src="JS/mask.js" type="text/javascript"></script>

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
<div class="al">
    <%=Resources.labels.momoitaikhoantietkiemcokyhan %><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div style="text-align: center; color: Red; margin: 10px 0;">
            <asp:Label runat="server" Font-Bold="true" ID="lblTextError"></asp:Label>
        </div>
        <asp:Panel ID="pnTIB" runat="server">
            <div class="block1">
                <div class="divcontent">
                    <div class="handle">
                        <asp:Label runat="server" Text='<%$ Resources:labels, thongtinmomoi %>'></asp:Label>
                    </div>
                    <div class="content_table">
                        <div class="row">
                            <div class="col-xs-5 col-md-4 right">
                                <asp:Label ID="Label11" runat="server" Text='<%$ Resources:labels, chinhanh %>'></asp:Label>&nbsp;*
                            </div>
                            <div class="col-xs-7 col-md-5 line30">
                                <asp:DropDownList ID="ddlBranch" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-md-4 right">
                                <asp:Label ID="Label12" runat="server" Text='<%$ Resources:labels, sanpham %>'></asp:Label>&nbsp;*
                            </div>
                            <div class="col-xs-7 col-md-5 line30">
                                <asp:DropDownList ID="ddlFDProduct" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFDProduct_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:HyperLink ID="HyperLink1" runat="server" Text="<%$ Resources:labels, laisuatthamkhao %>"
                                    NavigateUrl="https://www.phongsavanhbank.com/" Target="_blank" Visible="false"></asp:HyperLink>
                                <a href="javascript:alert('Please goto the website for more information')">
                                    <%=Resources.labels.laisuatthamkhao %></a>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-md-4 right">
                                <asp:Label ID="Label3" runat="server" Text='<%$ Resources:labels, kyhan %>'></asp:Label>
                            </div>
                            <div class="col-xs-7 col-md-5 line30">
                                <asp:Label ID="lblKyHan" runat="server"></asp:Label>
                                &nbsp;<asp:Label ID="lblTermDes" runat="server" Text="<%$ Resources:labels, thang %>"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-md-4 right">
                                <asp:Label ID="Label14" runat="server" Text='<%$ Resources:labels, trichtutaikhoan %>'></asp:Label>&nbsp;*
                            </div>
                            <div class="col-xs-7 col-md-5 line30">
                                <asp:DropDownList ID="ddlSenderAccount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-md-4 right">
                                <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, sodu %>'></asp:Label>
                            </div>
                            <div class="col-xs-7 col-md-5 line30">
                                <asp:Label ID="lblBalance" Font-Bold="False" runat="server"></asp:Label>
                                &nbsp;<asp:Label ID="lblCCYID1" runat="server"></asp:Label>
                                <asp:HiddenField ID="hidChu" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-md-4 right">
                                <asp:Label ID="Label6" runat="server" Text='<%$ Resources:labels, sotienguitietkiem %>'></asp:Label>&nbsp;*
                            </div>
                            <div class="col-xs-7 col-md-5 line30">
                                <asp:TextBox ID="txtAmount" autocomplete="off" runat="server" onkeyup="ntt('ctl00_ctl13_txtAmount','ctl00_ctl13_lblText',event);"></asp:TextBox>
                                <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                                <asp:Label ID="lblText" runat="server" Font-Bold="True" Font-Italic="True"
                                    Font-Size="7pt" ForeColor="#0066FF" Width="200px"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5 col-md-4 right">
                                <asp:Label ID="Label15" runat="server" Text='<%$ Resources:labels, khidenhan %>'></asp:Label>&nbsp;*
                            </div>
                            <div class="col-xs-7 col-md-8 radio">
                                <span class="radio-button">
                                    <asp:RadioButton ID="rad1" runat="server" Text="<%$ Resources:labels, khidenhanvonvalaisangkyhanmoituongduongkyhanbandau %>"
                                        GroupName="TKCKH" Checked="True" /><br />
                                    <asp:RadioButton ID="rad2" runat="server" Text="<%$ Resources:labels, tattoanvachuyenvonvalaisangtaikhoanthanhtoan %> "
                                        ValidationGroup="TKCKH" GroupName="TKCKH" />
                                    <asp:Label ID="lblACCTNOTT" runat="server"></asp:Label>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <!--Button next-->
                <div style="text-align: center; margin-top: 10px;">
                    <asp:Button ID="btnTIBNext" CssClass="btn btn-primary" runat="server" OnClientClick="return validate();" Text="<%$ Resources:labels, tieptuc %>"
                        OnClick="btnTIBNext_Click" />
                </div>
            </div>
        </asp:Panel>
        <!--end-->
        <!--confirm-->
        <asp:Panel ID="pnConfirm" runat="server">
            <div class="divcontent">
                <div class="handle">
                    <asp:Label runat="server" Text='<%$ Resources:labels, thongtinxacnhan %>'></asp:Label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label2" runat="server" Text='<%$ Resources:labels, chinhanh %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">
                            <asp:Label ID="lblCFChiNhanh" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label5" runat="server" Text='<%$ Resources:labels, sanpham %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">
                            <asp:Label ID="lblConfirmFDProduct" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label9" runat="server" Text='<%$ Resources:labels, kyhandachon %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">
                            <asp:Label ID="lblCFKyHan" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblCFTermDes" runat="server" Text="<%$ Resources:labels, thang %>"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label16" runat="server" Text='<%$ Resources:labels, trichtutaikhoan %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">

                            <asp:Label ID="lblCFAccount" runat="server"></asp:Label>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label8" runat="server" Text='<%$ Resources:labels, sodu %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">

                            <asp:Label ID="lblCFBalance" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblCFCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label17" runat="server" Text='<%$ Resources:labels, sotienguitietkiem %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">
                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblCFCCYID1" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div style="text-align: center; margin-top: 10px;">
                    <asp:Button ID="Button3" CssClass="btn btn-warning" runat="server" OnClick="Button3_Click" Text="<%$ Resources:labels, quaylai %>" />

                    <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" OnClick="Button2_Click" Text="<%$ Resources:labels, xacnhan %>" />

                    <div class="clearfix"></div>
                </div>
            </div>

        </asp:Panel>
        <!--end-->
        <!--token-->
        <asp:Panel ID="pnOTP" runat="server">
            <div class="divcontent">
                <div class="handle">
                    <asp:Label runat="server" Text='<%$ Resources:labels, xacthucgiaodich %>'></asp:Label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label18" runat="server" Text='<%$ Resources:labels, loaixacthuc %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 ">
                            <asp:DropDownList ID="ddlLoaiXacThuc" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label19" runat="server" Text='<%$ Resources:labels, maxacthuc %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 ">
                            <asp:TextBox ID="txtOTP" runat="server" AutoCompleteType="None"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4"></div>
                        <div class="col-xs-7 col-md-5" style="text-align: center; margin: 10px 0;">
                            <img alt="" src="Images/otp.png" style="width: 100px;" />
                        </div>
                    </div>
                </div>
                <div style="text-align: center; margin-top: 10px;">
                    <asp:Button ID="Button4" CssClass="btn btn-warning" runat="server" OnClick="Button4_Click" Text="<%$ Resources:labels, quaylai %>" />

                    <asp:Button ID="Button5" CssClass="btn btn-primary" runat="server" OnClick="Button5_Click" Text="<%$ Resources:labels, thuchien %>" />

                    <div class="clearfix"></div>
                </div>
            </div>
        </asp:Panel>
        <!--end-->
        <!--sao ke-->
        <asp:Panel ID="pnResultTransaction" runat="server">
            <div class="divcontent">
                <div class="handle">
                    <asp:Label runat="server" Text='<%$ Resources:labels, thongtintaikhoantietkiem %>'></asp:Label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label20" runat="server" Text='<%$ Resources:labels, chinhanh %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">
                            <asp:Label ID="lblRSChiNhanh" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label21" runat="server" Text='<%$ Resources:labels, taikhoantietkiemcokyhan %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">
                            <asp:Label ID="lblAccountTK" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label22" runat="server" Text='<%$ Resources:labels, sanpham %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">
                            <asp:Label ID="lblRSanPham" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label24" runat="server" Text='<%$ Resources:labels, kyhandachon %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">
                            <asp:Label ID="lblRKyHan" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblRTD" runat="server" Text="<%$ Resources:labels, thang %>"></asp:Label>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label25" runat="server" Text='<%$ Resources:labels, laisuat %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">
                            <asp:Label ID="lblRLaiSuat" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="Label70" runat="server" Text="<%$ Resources:labels, percentyear %>"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, ngaymo %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">
                            <asp:Label ID="lblOpenDate" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label7" runat="server" Text='<%$ Resources:labels, ngaydenhan %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">
                            <asp:Label ID="lblExpireDate" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label26" runat="server" Text='<%$ Resources:labels, sodutietkiem %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">
                            <asp:Label ID="lblFDBalance" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblRCCYID2" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label27" runat="server" Text='<%$ Resources:labels, trichtutaikhoan %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">
                            <asp:Label ID="lblRAccount" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label10" runat="server" Text='<%$ Resources:labels, sodutaikhoanthanhtoan %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">
                            <asp:Label ID="lblRBalance" runat="server" Font-Bold="False"></asp:Label>
                            &nbsp;<asp:Label ID="lblRCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label28" runat="server" Text='<%$ Resources:labels, sotienguitietkiem %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-md-5 line30">
                            <asp:Label ID="lblRAmount" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblRCCYID1" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div style="text-align: center; margin-top: 10px;">
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-info" OnClientClick="javascript:return poponload()"
                        Text="<%$ Resources:labels, inketqua %>" />

                    <asp:Button ID="btnViewPrint" CssClass="btn btn-warning" OnClientClick="javascript:return poponloadview()" runat="server"
                        Text="<%$ Resources:labels, viewphieuin %>" />

                    <asp:Button ID="Button6" runat="server" CssClass="btn btn-success" OnClick="Button6_Click" Text="<%$ Resources:labels, lammoi %>" />

                    <asp:Button ID="btnPreview" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, thongtintaikhoan %>"
                        OnClick="btnPreview_Click" />
                    <div class="clearfix"></div>
                </div>
            </div>
        </asp:Panel>
        <!--end-->

        <script type="text/javascript">
            function resetTS() {
                document.getElementById("ctl00_ctFl15_txtTS").value = "";
                document.getElementById("ctl00_ctl15_txtTS").disabled = true;
            }
            function enableTS() {
                document.getElementById("ctl00_ctl15_txtTS").disabled = false;
            }

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
                document.getElementById('<%=hidChu.ClientID %>').value = "(" + number2text(replaceAll(document.getElementById(sNumber).value, ",", ""), '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>') + ")";
            }
            function replaceAll(str, from, to) {
                var idx = str.indexOf(from);


                while (idx > -1) {
                    str = str.replace(from, to);
                    idx = str.indexOf(from);
                }

                return str;
            }
            function validate() {

                if (validateMoney('<%=txtAmount.ClientID %>', '<%=Resources.labels.bancannhapsotien %>')) {

            //                if (parseInt(replaceAll(document.getElementById('<%=txtAmount.ClientID %>').value,",","")) < parseInt('1000000')) {
            //                    alert('<%=Resources.labels.sotienguitietkiemtoithieumottrieudong %>');
            //                    document.getElementById('<%=txtAmount.ClientID %>').focus();
                    //                    return false;
                    //                }
                }
                else {
                    document.getElementById('<%=txtAmount.ClientID %>').focus();
                    return false;
                }


            }
            function poponload() {
                testwindow = window.open("widgets/IBCKVTKTKCKH/print.aspx?pt=P&cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
                    "menubar=1,scrollbars=1,width=500,height=650");
                testwindow.moveTo(0, 0);
                return false;
            }

            function poponloadview() {
                testwindow = window.open("widgets/IBCKVTKTKCKH/print.aspx?pt=WP&cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
                    "menubar=1,scrollbars=1,width=500,height=650");
                testwindow.moveTo(0, 0);
                return false;
            }
        </script>
    </ContentTemplate>
</asp:UpdatePanel>
