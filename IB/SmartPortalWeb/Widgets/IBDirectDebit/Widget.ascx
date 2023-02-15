<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBDirectDebit_Widget" %>
<style>
    .row {
        /*margin-bottom: 10px;*/
    }
</style>
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
<div class="th">
    <div style="float: left">
        <span><%=Resources.labels.directdebit %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
    </div>
    <div style="text-align: right; float: right;">
        <a href="TemplateDownload/Direct Debit.xls"><%= Resources.labels.downloadddtemplate %></a>
    </div>
</div>
<div class="clearfix"></div>
<asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

        <div class="divError" style="text-align: center">
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </div>

        <asp:Panel ID="pnPT" runat="server">
            <figure class="divcontent">
                <%--<legend class="handle"><%=Resources.labels.chitietgiaodich %></legend>--%>

                <div class="handle">
                    <label class="bold"><%= Resources.labels.thongtingiaodich %></label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right bold">
                            <label><%= Resources.labels.tendoanhnghiep %></label>
                        </div>
                        <div class="col-xs-7 col-md-4 line30">
                            <asp:DropDownList runat="server" ID="ddlCorp" Enabled="false"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right bold">
                            <label><%= Resources.labels.madoanhnghiep %></label>
                        </div>
                        <div class="col-xs-7 col-md-4 line30">
                            <asp:TextBox runat="server" ID="txtCorpID" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right bold">
                            <label><%= Resources.labels.dichvu %></label>
                        </div>
                        <div class="col-xs-7 col-md-4 line30">
                            <asp:DropDownList runat="server" ID="ddlService" AutoPostBack="true" OnSelectedIndexChanged="ddlService_Changed"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right bold">
                            <label><%= Resources.labels.serviceid %></label>
                        </div>
                        <div class="col-xs-7 col-md-4 line30">
                            <asp:TextBox runat="server" ID="txtServiceID" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right bold">
                            <label><%= Resources.labels.chonfile %></label>
                        </div>
                         <div class="col-md-4 col-sm-4 col-xs-4">
                                <input id="uploadFile" type="text" class="form-control" placeholder="Choose File" disabled="disabled" />
                            </div>
                        <div class="col-xs-3 col-md-4 line30 upload-btn-wrapper">
                            <button class="btn btn-success">Upload file</button>
                            <asp:FileUpload ID="fuTransfer" runat="server" />
                            <asp:HiddenField runat="server" ID="hdfFileUri" />
                        </div>
                    </div>
                </div>
                <!--Button next-->
                <div style="text-align: center; margin-top: 10px;">
                    <asp:Button ID="btnLNext" runat="server" CssClass="btn btn-primary"
                        Text="<%$ Resources:labels, tieptuc %>" OnClick="btnLNext_Click" OnClientClick="return vali();" />
                </div>
            </figure>

        </asp:Panel>
        <!--end-->

        <!--confirm-->
        <asp:Panel ID="pnConfirm" runat="server">
            <figure class="divcontent">
                <%--<legend class="handle"><%=Resources.labels.chitietgiaodich %></legend>--%>

                <div class="handle">
                    <label class="bold"><%= Resources.labels.corporateinformation %></label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right bold">
                            <label><%= Resources.labels.tendoanhnghiep %></label>
                        </div>
                        <div class="col-xs-7 col-md-4 line30 height30">
                            <asp:Label ID="lblCorpNameCF" runat="server" Font-Bold="False"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right bold">
                            <label><%= Resources.labels.madoanhnghiep %></label>
                        </div>
                        <div class="col-xs-7 col-md-4 line30 height30">
                            <asp:Label ID="lblCorpIDCF" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right bold">
                            <label><%= Resources.labels.dichvu %></label>
                        </div>
                        <div class="col-xs-7 col-md-4 line30 height30">
                            <asp:Label ID="lblServiceCF" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right bold">
                            <label><%= Resources.labels.serviceid %></label>
                        </div>
                        <div class="col-xs-7 col-md-4 line30 height30">
                            <asp:Label ID="lblServiceIDCF" runat="server"></asp:Label>
                        </div>
                    </div>
                    <asp:GridView ID="gvConfirm" runat="server" AutoGenerateColumns="True"
                        CssClass="table footable" CellPadding="3" Width="100%">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    </asp:GridView>
                    <div style="text-align: center; margin-top: 10px; margin-bottom: 10px; width: 100%">
                         <asp:Button ID="btnBack2" runat="server" CssClass="btn btn-warning" OnClick="btnBack2_Click"
                            Text="<%$ Resources:labels, quaylai %>" />
                        <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click"
                            Text="<%$ Resources:labels, confirm %>" CssClass="btn btn-primary" />
                       
                            <div class="clearfix"></div> 
                    </div>
                </div>
            </figure>
        </asp:Panel>
        <!--end-->
        <!--token-->
        <asp:Panel ID="pnOTP" runat="server">
            <figure class="divcontent">
                <%--<legend class="handle"><%=Resources.labels.chitietgiaodich %></legend>--%>

                <div class="handle">
                    <label class="bold"><%= Resources.labels.xacthucgiaodich %></label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right bold">
                            <label><%= Resources.labels.loaixacthuc %></label>
                        </div>
                        <div class="col-xs-7 col-md-4 line30">
                            <asp:DropDownList ID="ddlAuthenType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListOTP_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Button ID="btnSendOTP" CssClass="btn btn-primary" runat="server" OnClick="btnSendOTP_Click" Text="Send" Visible="False" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right bold">
                            <label><%= Resources.labels.maxacthuc %></label>
                        </div>
                        <div class="col-xs-7 col-md-4 line30">
                            <asp:TextBox ID="txtAuthenCode" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4 col-md-4"></div>
                        <div class="col-xs-7 col-md-4 line30">
                            <img alt="" src="widgets/IBTransferInBank1/Images/otp.gif" style="width: 100px; height: 60px" />
                        </div>
                    </div>
                    <div style="text-align: center; margin-top: 10px;">
                        <asp:Button ID="btnBackA" runat="server" OnClick="btnBackA_Click" CssClass="btn btn-warning"
                            Text="<%$ Resources:labels, quaylai %>" />
                        <asp:Button ID="btnOTP" runat="server" OnClick="btnOTP_Click" CssClass="btn btn-primary"
                            Text="<%$ Resources:labels, thuchien %>" />
                      <div class="clearfix"></div> 
                     </div>
                </div>
            </figure>
        </asp:Panel>
        <!--end-->
        <asp:Panel ID="pnResult" runat="server">
            <figure class="divcontent">
                <%--<legend class="handle"><%=Resources.labels.ketquagiaodich %></legend>--%>

                <div class="handle">
                    <label class="bold"><%= Resources.labels.ketquagiaodich %></label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right bold">
                            <label><%= Resources.labels.magiaodich %></label>
                        </div>
                        <div class="col-xs-7 col-md-4 line30">
                            <asp:Label ID="lblTranIDFN" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right bold">
                            <label><%= Resources.labels.thoidiem %></label>
                        </div>
                        <div class="col-xs-7 col-md-4 line30">
                            <asp:Label ID="lblDateTimeFN" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right bold">
                            <label>Import file number</label>
                        </div>
                        <div class="col-xs-7 col-md-4 line30">
                            <asp:Label ID="lblServerTimeFN" runat="server"></asp:Label>
                        </div>
                    </div>

                </div>
                <div class="handle">
                    <label class="bold"><%= Resources.labels.corporateinformation %></label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right bold">
                            <label><%= Resources.labels.tendoanhnghiep %></label>
                        </div>
                        <div class="col-xs-7 col-md-4 line30">
                            <asp:Label ID="lblCorpNameFN" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right bold">
                            <label><%= Resources.labels.madoanhnghiep %></label>
                        </div>
                        <div class="col-xs-7 col-md-4 line30">
                            <asp:Label ID="lblCorpIDFN" runat="server"></asp:Label>
                        </div>
                    </div>
                    <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="True"
                        CssClass="table footable" CellPadding="3" Width="100%">
                        <RowStyle ForeColor="#000066" />
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
                <!--Button next-->
                <div style="text-align: center; margin-top: 10px;">
                    <asp:Button ID="btnExit" runat="server" CssClass="btn btn-warning"
                        Text="<%$ Resources:labels, thoat %>" OnClick="btnExit_Click" />
                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-success" OnClientClick="javascript:return poponloadview()"
                        Text="<%$ Resources:labels, viewphieuin %>" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-info" OnClientClick="javascript:return poponload()"
                        Text="<%$ Resources:labels, inketqua %>" />
                      <div class="clearfix"></div> 
                </div>
            </figure>
        </asp:Panel>
        <!--end-->
        <script>
            document.getElementById("<%=fuTransfer.ClientID%>").onchange = function () {
                document.getElementById("uploadFile").value = this.files[0].name;
            };
            function poponload() {
                testwindow = window.open("widgets/IBCLGD/print.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
                    "menubar=1,scrollbars=1,width=500,height=650");
                testwindow.moveTo(0, 0);
                return false;
            }
            function poponloadview() {
                testwindow = window.open("widgets/IBCLGD/viewprint.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
                    "menubar=1,scrollbars=1,width=500,height=650");
                testwindow.moveTo(0, 0);
                return false;
            }

            function resetTS() {
                document.getElementById("ctl00_ctl15_txtTS").value = "";
                document.getElementById("ctl00_ctl15_txtTS").disabled = true;
            }
            function enableTS() {
                document.getElementById("ctl00_ctl15_txtTS").disabled = false;
            }
            function resetTS1() {
                document.getElementById("ctl00_ctl15_txtTS1").value = "";
                document.getElementById("ctl00_ctl15_txtTS1").disabled = true;
            }
            function enableTS1() {
                document.getElementById("ctl00_ctl15_txtTS1").disabled = false;
            }
            function enableOTP() {
                document.getElementById("ctl00_ctl12_txtOTP").disabled = false;
                document.getElementById("ctl00_ctl12_txtOTPBSMS").value = "";
                document.getElementById("ctl00_ctl12_txtOTPBSMS").disabled = true;
            }
            function enableSMSOTP() {
                document.getElementById("ctl00_ctl12_txtOTP").disabled = true;
                document.getElementById("ctl00_ctl12_txtOTPBSMS").disabled = false;
                document.getElementById("ctl00_ctl12_txtOTP").value = "";
            }
            function ntt(sNumber, event) {

                executeComma(sNumber, event);


            }

            function ValidateLimit(obj, maxchar) {
                replaceSQLChar(obj);
                if (this.id) obj = this;
                var remaningChar = maxchar - obj.value.length;

                if (remaningChar <= 0) {
                    obj.value = obj.value.substring(maxchar, 0);
                }
            }
        </script>
    </ContentTemplate>
</asp:UpdatePanel>
