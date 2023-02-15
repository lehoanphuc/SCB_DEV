<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCustomerList_Add_Widget" %>
<%@ Register Src="~/Controls/PreviewImage/PreviewImage.ascx" TagPrefix="uc1" TagName="PreviewImage" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>
<script type="text/javascript" src="widgets/SEMSCustomerList/js/common.js"> </script>
<script type="text/javascript" src="widgets/SEMSCustomerList/js/commonjs.js"> </script>
<script type="text/javascript" src="widgets/SEMSCustomerListCorp/js/tabber.js"></script>
<script type="text/javascript" src="js/Validate.js"> </script>
<script src="/JS/Common.js"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divCustHeader">
            <img alt="" src="widgets/SEMSCustomerList/Images/messenger.png" style="width: 32px; height: 32px; margin-bottom: 10px;" />
            <%=Resources.labels.themmoikhachhang %>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <br />
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="0" AssociatedUpdatePanelID="pnCustInfo" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="0" AssociatedUpdatePanelID="pnPersonal" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="pnCustInfo" runat="server">
    <ContentTemplate>
        <div class="row" runat="server" id="Div1">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>
                        <%=Resources.labels.laythongtinkhachhangtucorebanking%>
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="Panel3" runat="server">
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.makhachhang %> </label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtCustCode" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-5"></div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, xemchitiet %>" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="PhoneWallet">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>
                        <%=Resources.labels.laythongtinkhachhang%>
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="Panel4" runat="server">
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.phone %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtPhoneWL" onkeypress="return isNumberKeyNumer(event)" CssClass="form-control" runat="server" OnTextChanged="btnSearchWL_Click" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-5">
                                    <asp:Label ID="lblLog" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btnSearchWL" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, xemchitiet %>" OnClick="btnSearchWL_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="CustomerInfor">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>
                        <%=Resources.labels.thongtinkhachhang%>
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="Panel5" runat="server">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.makhachhang %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtCustCodeInfo" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.tendaydu %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtFullName" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.tenviettat %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtShortName" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.loaikhachhang %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCustType" runat="server" Enabled="False" CssClass="form-control select2 infinity">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.birthday %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtBirth" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.gender %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlGender" runat="server" Enabled="False" CssClass="form-control select2 infinity">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.chinhanh %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Value="0">Chi nhánh Sài Gòn</asp:ListItem>
                                                <asp:ListItem Value="0">Chi nhánh Hà Nội</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.email%></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.region %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control select2"
                                                Width="100%" OnSelectedIndexChanged="ddlRegion_OnSelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.Townshipname %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlTownship" runat="server" CssClass="form-control select2" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.diachithuongtru %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtResidentAddr" CssClass="form-control" TextMode="MultiLine" onkeyup="ValidateLimit(this,'250');" onkeyDown="ValidateLimit(this,'250');" onpaste="ValidateLimit(this,'250');" onChange="ValidateLimit(this,'250');" onmousedown="ValidateLimit(this,'250');" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.diachitamtru%> </label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtTempAddress" CssClass="form-control" TextMode="MultiLine" onkeyup="ValidateLimit(this,'250');" onkeyDown="ValidateLimit(this,'250');" onpaste="ValidateLimit(this,'250');" onChange="ValidateLimit(this,'250');" onmousedown="ValidateLimit(this,'250');" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.Cardid %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtIF" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.IssueDate%></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtIssueDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label "><%=Resources.labels.noicap %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtIssuePlace" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.quocgia %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlNation" runat="server" CssClass="form-control select2 infinity">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" visible="false">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.sofax %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtFax" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.nghenghiep %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtJob" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" visible="false">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.diachilamviec %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtOfficeAddr" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.dienthoaicoquan %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtCompanyPhone" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6" runat="server" visible="false">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.ghichu %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtDesc" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6" runat="server" visible="false">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.kyctype %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlkycType" runat="server" CssClass="form-control select2 infinity">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12" runat="server">
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label"><%=Resources.labels.transactionalert %></label>
                                        <div class="col-sm-10">
                                            <div class="row">
                                                <div class="form-group custom-control">
                                                    <div class="col-sm-3">
                                                        <asp:CheckBox ID="cbSMS" runat="server" Text="<%$ Resources:labels, sms %>" />
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:CheckBox ID="cbWAPP" runat="server" Text="<%$ Resources:labels, whatsapp %>" />
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:CheckBox ID="cbLINE" runat="server" Text="<%$ Resources:labels, line %>" />
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:CheckBox ID="cbTELE" runat="server" Text="<%$ Resources:labels, telegram %>" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" runat="server" id="KYCInFor">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>
                        <%=Resources.labels.kycinformation %>
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="Panel8" runat="server">
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="col-sm-12 control-label required">PP</label>
                                    </div>
                                </div>
                                <div class="relative col-sm-3">
                                    <div class="form-group relative">
                                        <asp:DropDownList ID="ddlKYCName" CssClass="form-control infinity" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2 right">
                                    <div class="form-group">
                                        <asp:Button ID="btnCheckKYC" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, kiemtra %>" OnClick="btnCheckKYC_Click" Visible="False" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <div class="row">
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <label class="col-sm-12 control-label required">PP Number</label>
                                </div>
                            </div>
                            <div class="relative">
                                <div class="col-sm-2">
                                    <div class="form-group relative">
                                        <asp:TextBox ID="txtNRICNewNumber" CssClass="form-control" placeholder="PP number" MaxLength="18" runat="server" Width="150%" OnTextChanged="btnCheckKYC_Click"></asp:TextBox>
                                    </div>
                                </div>
                                <div id="errorMessageNRIC" style="margin-left: 10px; color: red;" hidden="hidden"></div>
                            </div>
                            <div>
                            </div>
                        </div>
                        <div>
                            <asp:Panel ID="pnNewNRIC" runat="server">

                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-3">
                                            <label class="col-sm-12 control-label required">PP Front</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <label class="col-sm-12 control-label required">PP Back</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <label class="col-sm-12 control-label">Selfie With PP</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="padding-bottom: 15px">
                                    <div class="col-sm-3">
                                        <div class="col-sm-12">
                                            <asp:FileUpload ID="FUNRICFontNew" runat="server" ClientIDMode="Static" accept=".png,.jpg,.jpeg,.gif" />
                                            <br />
                                            <asp:Label ID="lblNRICFontNew" runat="server" Visible="false" />

                                            <a data-toggle="modal" data-target="#ViewNRICFontNew">
                                                <asp:Image runat="server" ID="ImgNRICFontNew" Style="max-width: 150px; max-height: 150px; width: 100%" />
                                            </a>

                                            <asp:Panel ID="pannelModal" runat="server">
                                                <!-- The Modal -->
                                                <div class="modal" id="ViewNRICFontNew">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">

                                                            <!-- Modal Header -->
                                                            <div class="modal-header">
                                                                <h4 class="modal-title" style="text-align: left!important">View Document</h4>
                                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                            </div>

                                                            <!-- Modal body -->
                                                            <div class="divlog" style="color: red">
                                                                <%-- <label id="lblErrorPopup" runat="server"></label>--%>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="panel-container">
                                                                    <div class="panel-content form-horizontal p-b-0">
                                                                        <div class="view-image" style="text-align: center; width: 100%; height: auto">
                                                                            <asp:Image ID="PopupImgNRICFontNew" runat="server" Style="width: 100%; height: auto" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!-- Modal footer -->
                                                            <div class="modal-footer" style="text-align: center!important">
                                                                <button type="button" class="btn btn-secondary" data-dismiss="modal"><%=Resources.labels.cancel %></button>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="col-sm-12">
                                            <asp:FileUpload ID="FUNRICBackNew" runat="server" ClientIDMode="Static" accept=".png,.jpg,.jpeg,.gif" />
                                            <br />
                                            <asp:Label ID="lblNRICBackNew" runat="server" Visible="false" />
                                            <a data-toggle="modal" data-target="#ViewNRICBackNew">
                                                <asp:Image runat="server" ID="ImgNRICBackNew" Style="max-width: 150px; max-height: 150px; width: 100%" />
                                            </a>

                                            <asp:Panel ID="Panel9" runat="server">
                                                <!-- The Modal -->
                                                <div class="modal" id="ViewNRICBackNew">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">

                                                            <!-- Modal Header -->
                                                            <div class="modal-header">
                                                                <h4 class="modal-title" style="text-align: left!important">View Document</h4>
                                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                            </div>

                                                            <!-- Modal body -->
                                                            <div class="divlog" style="color: red">
                                                                <label id="Label1" runat="server"></label>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="panel-container">
                                                                    <div class="panel-content form-horizontal p-b-0">
                                                                        <div class="view-image" style="text-align: center; width: 100%; height: auto">
                                                                            <asp:Image ID="PopupImgNRICBackNew" runat="server" Style="width: 100%; height: auto" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!-- Modal footer -->
                                                            <div class="modal-footer" style="text-align: center!important">
                                                                <button type="button" class="btn btn-secondary" data-dismiss="modal"><%=Resources.labels.cancel %></button>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="col-sm-12">
                                            <asp:FileUpload ID="FUSelfieNRIC" runat="server" ClientIDMode="Static" accept=".png,.jpg,.jpeg,.gif" />
                                            <br />
                                            <asp:Label ID="lblSelfieNRIC" runat="server" Visible="false" />
                                            <a data-toggle="modal" data-target="#ViewSelfieNRIC">
                                                <asp:Image runat="server" ID="ImgSelfieNRIC" Style="max-width: 150px; max-height: 150px; width: 100%" />

                                            </a>
                                            <asp:Panel ID="Panel10" runat="server">
                                                <!-- The Modal -->
                                                <div class="modal" id="ViewSelfieNRIC">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">

                                                            <!-- Modal Header -->
                                                            <div class="modal-header">
                                                                <h4 class="modal-title" style="text-align: left!important">View Document</h4>
                                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                            </div>

                                                            <!-- Modal body -->
                                                            <div class="divlog" style="color: red">
                                                                <label id="Label4" runat="server"></label>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="panel-container">
                                                                    <div class="panel-content form-horizontal p-b-0">
                                                                        <div class="view-image" style="text-align: center; width: 100%; height: auto">
                                                                            <asp:Image ID="PopupImgSelfieNRIC" runat="server" Style="width: 100%; height: auto" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!-- Modal footer -->
                                                            <div class="modal-footer" style="text-align: center!important">
                                                                <button type="button" class="btn btn-secondary" data-dismiss="modal"><%=Resources.labels.cancel %></button>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="col-sm-12">
                                            <asp:Button ID="btnUpLoadFile" runat="server" Text="Upload" CssClass="btn btn-primary" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="Div2">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>
                        <%=Resources.labels.thongtinhopdong%>
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.mahopdong %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtContractNo" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.kieunguoidung %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlUserType" CssClass="form-control select2 infinity" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.ngayhieuluc %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtStartDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.ngayhethan %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtEndDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.masanpham %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlProduct" CssClass="form-control select2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.contractaLevel %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlContractLevel" CssClass="form-control select2 infinity" runat="server" AutoPostBack="True" Enabled="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group custom-control" style="margin-left: 1px">
                                        <asp:CheckBox ID="chkRenew" runat="server" Checked="True" Text="<%$ Resources:labels, autorenewlabel %>" />
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="Div10">
            <asp:Panel runat="server" ID="pnbtnNext">
                <div id="divToolbar" style="text-align: center">
                    &nbsp;<asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" OnClientClick="return validate();"
                        Text="<%$ Resources:labels, next %>" CssClass="btn btn-primary" />
                    &nbsp;
        <asp:Button ID="Button4" runat="server" Text="<%$ Resources:labels, lamlai %>" CssClass="btn btn-secondary" Visible="false" />
                    &nbsp
                </div>
            </asp:Panel>
        </div>

        <asp:Panel class="row" ID="pnDocument" runat="server">
            <div class="pane">
                <div class="table-responsive">

                    <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_OnItemDataBound" OnItemCommand="rptData_ItemCommand1">
                        <HeaderTemplate>
                            <table class="table table-hover footable">
                                <thead class="thead-light repeater-table">
                                    <tr>
                                        <th class="title-repeater"></th>
                                        <th class="title-repeater"><%=Resources.labels.DocumentName%></th>
                                        <th class="title-repeater"><%=Resources.labels.view%></th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="tr-boder item-center"><%#Eval("NO") %></td>
                                <td class="tr-boder"><%#Eval("DocumentName") %></td>
                                <td class="td-no-action tr-boder">
                                    <asp:LinkButton ID="lbtnViewFile" runat="server" CommandArgument='<%#Eval("NO") %>'>
                                        <asp:Image ID="ImageView" Style="max-width: 1000px" runat="server" src='<%# "data:image/jpg;base64," + Eval("FILE") %>' data-toggle="tooltip" title="Show image" />
                                    </asp:LinkButton>
                                    <asp:UpdatePanel ID="updatepanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox Visible="false" ID="txtImage" runat="server" Width="10%" ReadOnly="true"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hdID" />
                                            <button type="button" visible="false" runat="server" id="btnPopup" class="search-popup">
                                            </button>
                                            <asp:Panel runat="server" class="modal fade" ID="Image" data-backdrop="static" role="dialog">
                                                <div class="modal-dialog">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h4 class="modal-title" style="text-align: left!important">Document</h4>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">×</span></button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </asp:Panel>

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnUpLoadFile" />
        <%--        <asp:PostBackTrigger ControlID="btnUploadPP" />--%>
        <%--<asp:PostBackTrigger ControlID="btnUploadLC" />--%>
        <asp:PostBackTrigger ControlID="btnNext" />
    </Triggers>
</asp:UpdatePanel>

<asp:UpdatePanel runat="server" ID="pnPersonal">
    <ContentTemplate>
        <div class="row" runat="server" id="divTab">
            <div class="col-sm-12">
                <div class="nav-tabs-custom">
                    <ul class="nav nav-tabs">
                        <li class="active" id="liTabCTK" runat="server"><a href="#tab_1" data-toggle="tab"><%=Resources.labels.thongtinnguoidung %></a></li>
                        <li id="liTabDCTK" runat="server" visible="false"><a href="#tab_2" data-toggle="tab"><%=Resources.labels.thongtinnguoidongsohuu %></a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active" id="tab_1">
                            <div class="tabbertab" runat="server" id="TabCTK">
                                <div class="row" runat="server" id="AccountInfor">
                                    <div class="col-sm-12">
                                        <div class="panel">
                                            <div class="panel-container">
                                                <div class="panel-content form-horizontal p-b-0">
                                                    <asp:Panel ID="Panel6" runat="server">
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label">
                                                                        <%=Resources.labels.tendaydu %></label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtReFullName" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6" runat="server">
                                                                <label class="col-sm-4 control-label"><%=Resources.labels.qrcodename %></label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtLocalFullName" CssClass="form-control" placeholder="<%$ Resources:labels, qrcodename %>" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label">
                                                                        <%=Resources.labels.phone %></label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtReMobi" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <label class="col-sm-4 control-label">
                                                                    <%=Resources.labels.gender %></label>
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList ID="ddlReGender" runat="server" Enabled="False" Width="100%" CssClass="form-control select2 infinity">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label">
                                                                        <%=Resources.labels.birthday %></label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtReBirth" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <label class="col-sm-4 control-label">
                                                                    <%=Resources.labels.address %></label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtReAddress" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6" runat="server">
                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label required"><%=Resources.labels.account %></label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlAccountMB" CssClass="form-control select2 infinity" Width="100%" runat="server" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="ddlAccountMB_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <label class="col-sm-4 control-label">
                                                                    Email</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtReEmail" CssClass="form-control" placeholder="Email" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6" runat="server">
                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label required">Default Account</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlDefaultAccountMB" CssClass="form-control select2 infinity" Width="100%" runat="server">
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
                                <div class="row" runat="server" id="divAccount">
                                    <div class="col-sm-12">
                                        <div class="nav-tabs-custom">
                                            <ul class="nav nav-tabs">
                                                <%   if (TabCustomerInfoHelperConsumer.TabMobileVisibility == 1)
                                                    { %>

                                                <li class="active" id="li1" runat="server"><a href="#tabview_1" data-toggle="tab"><%=Resources.labels.mobilebanking %></a></li>
                                                <%}
                                                    if (TabCustomerInfoHelperConsumer.TabWalletVisibility == 1)
                                                    { %>

                                                <li id="li2" runat="server"><a href="#tabview_2" data-toggle="tab"><%=Resources.labels.walletbanking %></a></li>
                                                <%}
                                                    if (TabCustomerInfoHelperConsumer.TabSMSVisibility == 1)
                                                    { %>

                                                <li id="liTabSMS" runat="server"><a href="#tab_3" data-toggle="tab"><%=Resources.labels.smsbanking %></a></li>
                                                <%}
                                                    if (TabCustomerInfoHelperConsumer.TabIBVisibility == 1)
                                                    { %>

                                                <li id="liTabIB" runat="server"><a href="#tab_4" data-toggle="tab"><%=Resources.labels.internetbanking %></a></li>
                                                <%}
                                                %>
                                            </ul>
                                            <div class="tab-content">
                                                <div class="tab-pane active" id="tabview_1">
                                                    <div class="panel" id="tblMB" runat="server">
                                                        <div class="panel-container">
                                                            <div class="panel-content form-horizontal p-b-0">
                                                                <asp:Panel ID="divSearch" runat="server">
                                                                    <div class="row">
                                                                        <div class="col-sm-6" style="background-color: #F5F5F5; color: #38277c;">
                                                                            <div class="form-group">
                                                                                <div class="col-sm-12">
                                                                                    <asp:Label class="col-sm-4 control-label" runat="server"><%=Resources.labels.thongtindangnhap %></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6" style="background-color: #F5F5F5; color: #38277c;">
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label"><%=Resources.labels.quyensudung %></label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin-top: 5px">
                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label required"><%=Resources.labels.username %></label>
                                                                                <div class="col-sm-8">
                                                                                    <div class="form-group">
                                                                                        <div class="col-sm-10">
                                                                                            <asp:TextBox ID="txtUserNameMB" runat="server" CssClass="form-control" onkeypress="return isKey(event)" OnTextChanged="ChangeUserName_TextChanged" MaxLength="50" AutoPostBack="true" placeholder="User Name"></asp:TextBox>
                                                                                        </div>
                                                                                        <asp:LinkButton ID="lbCreateusername" OnClick="CreateUserName_Click" runat="server">Generate</asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label required"><%=Resources.labels.phone %></label>
                                                                                <div class="col-sm-8">
                                                                                    <div class="form-group">
                                                                                        <div class="col-sm-12">
                                                                                            <asp:TextBox ID="txtPhoneMB" onkeypress="return isNumberKeyNumer(event)" CssClass="form-control" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                                                        </div>

                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label required"><%=Resources.labels.loginmethod %></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlLoginMethod" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                                                        <asp:ListItem Value="USERNAME" Text="User Name"></asp:ListItem>
                                                                                        <asp:ListItem Value="PHONENO" Text="Phone"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group" runat="server" visible="false">
                                                                                <label class="col-sm-4 control-label required"><%=Resources.labels.authentype %></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlauthenType" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                                                        <asp:ListItem Value="PASSWORD" Text="Password"></asp:ListItem>
                                                                                        <asp:ListItem Value="PINCODE" Text="Pincode"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label required"><%=Resources.labels.dungpolicy %></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlpolicyMB" runat="server" CssClass="form-control select2 infinity" AutoPostBack="true" OnSelectedIndexChanged="ddlpolicyIB_SelectedIndexChanged"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6 custom-control" rowspan="4">
                                                                            <div class="form-group ">
                                                                                <div class="col-sm-12">
                                                                                    <div class="custom-control custom-control">
                                                                                        <asp:TreeView ID="tvMB" runat="server">
                                                                                            <SelectedNodeStyle Font-Bold="True" Font-Underline="True" />
                                                                                            <NodeStyle CssClass="p-l-10" />
                                                                                        </asp:TreeView>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tab-pane" id="tabview_2">
                                                    <div class="panel" id="tblWL" runat="server">
                                                        <div class="panel-container">
                                                            <div class="panel-content form-horizontal p-b-0">
                                                                <asp:Panel ID="Panel7" runat="server">
                                                                    <div class="row">
                                                                        <div class="col-sm-6" style="background-color: #F5F5F5">
                                                                            <div class="form-group">
                                                                                <div class="col-sm-12">
                                                                                    <asp:Label class="col-sm-4 control-label" runat="server"><%=Resources.labels.thongtindangnhap %></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6" style="background-color: #F5F5F5; color: #38277c;">
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label"><%=Resources.labels.quyensudung %></label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin-top: 5px">
                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label required"><%=Resources.labels.phone %></label>
                                                                                <div class="col-sm-8">
                                                                                    <div>
                                                                                        <asp:TextBox ID="txtWLPhoneNo" CssClass="form-control" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6" rowspan="4">
                                                                            <div class="form-group">
                                                                                <div class="col-sm-12">
                                                                                    <div class="custom-control custom-control">
                                                                                        <asp:TreeView ID="tvWL" runat="server">
                                                                                            <SelectedNodeStyle Font-Bold="True" Font-Underline="True" />
                                                                                            <NodeStyle CssClass="p-l-10" />
                                                                                        </asp:TreeView>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tab-pane" id="tab_3">
                                                    <table id="tblSMS" class="style1" cellspacing="0" cellpadding="4">
                                                        <tr>
                                                            <td colspan="2" style="background-color: #F5F5F5; color: #003366; width: 50%;">
                                                                <asp:Label ID="Label35" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                                            </td>
                                                            <td style="background-color: #F5F5F5; color: #003366; width: 50%;">
                                                                <asp:Label ID="Label271" runat="server" Font-Bold="True" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25%;" valign="top">
                                                                <asp:Label ID="Label36" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                                            </td>
                                                            <td style="width: 25%;" valign="top">
                                                                <asp:TextBox ID="txtSMSPhoneNo" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td rowspan="5" style="width: 50%;">
                                                                <div style="width: 100%; height: 150px; overflow: auto;">
                                                                    <asp:TreeView ID="tvSMS" runat="server">
                                                                    </asp:TreeView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;" valign="top">
                                                                <asp:Label ID="Label45" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:DropDownList ID="ddlSMSDefaultAcctno" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;" valign="top">
                                                                <asp:Label ID="Label46" runat="server" Text="<%$ Resources:labels, language %>"></asp:Label>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:DropDownList ID="ddlLanguage" runat="server">
                                                                    <asp:ListItem Value="M" Text="<%$Resources:labels, myanmar %>"></asp:ListItem>
                                                                    <asp:ListItem Value="E" Text="<%$Resources:labels, english %>"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25%;" valign="top">
                                                                <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                                                <%=Resources.labels.dungpolicy %>
                                                                <%--<asp:Label ID="Label61" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                                            </td>
                                                            <td valign="top" style="width: 25%;">
                                                                <asp:DropDownList ID="ddlpolicySMS" runat="server"></asp:DropDownList>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style="width: 20%;" valign="top">
                                                                <asp:CheckBox ID="cbIsDefault" runat="server" Text="<%$ Resources:labels, sudungmacdinh %>"
                                                                    Checked="True" />
                                                            </td>
                                                            <td valign="top">
                                                                <asp:Label ID="Label60" runat="server" Text="<%$ Resources:labels, warningoneaccountononesmsrole %>" Style="color: red"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="tab-pane" id="tab_4">
                                                    <div class="panel" id="tblIB" runat="server">
                                                        <div class="panel-container">
                                                            <div class="panel-content form-horizontal p-b-0">
                                                                <asp:Panel ID="pn16" runat="server">
                                                                    <div class="row">
                                                                        <div class="col-sm-6" style="background-color: #F5F5F5">
                                                                            <div class="form-group">
                                                                                <div class="col-sm-12">
                                                                                    <asp:Label class="col-sm-4 control-label" runat="server"><%=Resources.labels.thongtindangnhap %></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6" style="background-color: #F5F5F5; color: #38277c;">
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label"><%=Resources.labels.quyensudung %></label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin-top: 5px">
                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label required"><%=Resources.labels.tendangnhap %></label>
                                                                                <div class="col-sm-8">
                                                                                    <div>
                                                                                        <asp:TextBox ID="txtUsernameIB" CssClass="form-control" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                                                    </div>
                                                                                    <asp:RadioButton ID="rbGenerate" runat="server" GroupName="rbUserNameIB" Checked="True"
                                                                                        ClientIDMode="Static" Visible="false" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-4 control-label"><%=Resources.labels.dungpolicy %></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlpolicyIB" runat="server" Width="100%" CssClass="form-control select2 infinity" AutoPostBack="true" OnSelectedIndexChanged="ddlpolicyIB_SelectedIndexChanged"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6" rowspan="4">
                                                                            <div class="form-group">
                                                                                <div class="col-sm-12">
                                                                                    <div class="custom-control custom-control">
                                                                                        <asp:TreeView ID="tvIB" runat="server">
                                                                                            <SelectedNodeStyle Font-Bold="True" Font-Underline="True" />
                                                                                            <NodeStyle CssClass="p-l-10" />
                                                                                        </asp:TreeView>
                                                                                    </div>
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
                                        </div>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="div3">
                                    <div class="col-sm-12">
                                        <asp:UpdateProgress runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel2">
                                            <ProgressTemplate>
                                                <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                                                <%=Resources.labels.loading %>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="div5" style="margin-top: 10px">
                                    <div class="col-sm-12">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnThemChuTaiKhoan" />
                                                <asp:AsyncPostBackTrigger ControlID="btnHuy" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <div style="text-align: right;">
                                                    &nbsp;
                                                            <asp:Button ID="btnThemChuTaiKhoan" runat="server" Text='<%$ Resources:labels, them %>'
                                                                OnClick="btnThemChuTaiKhoan_Click" OnClientClick="return validate1();" CssClass="btn btn-primary" />
                                                    &nbsp;
                                                            <asp:Button ID="btnHuy" runat="server" CssClass="btn btn-secondary" OnClick="btnHuy_Click" Text="<%$ Resources:labels, delete %>" />
                                                    &nbsp;
                                                </div>
                                                &nbsp;&nbsp;<asp:Label runat="server" ID="lblAlert" ForeColor="Red"></asp:Label>
                                                <div id="div8" style="margin: 20px 5px 5px 5px; height: 150px; overflow: auto;">
                                                    <asp:GridView ID="gvResultChuTaiKhoan" CssClass="table table-hover" runat="server" AutoGenerateColumns="False"
                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                        CellPadding="3" Width="100%" OnPageIndexChanging="gvResultChuTaiKhoan_PageIndexChanging"
                                                        OnRowDeleting="gvResultChuTaiKhoan_RowDeleting">
                                                        <RowStyle ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="<%$ Resources:labels, tendaydu %>" DataField="colFullName" />
                                                            <asp:BoundField DataField="colPhone" HeaderText="<%$ Resources:labels, phone %>" />
                                                            <asp:BoundField HeaderText="<%$ Resources:labels, taikhoan %>" DataField="colAccount" />
                                                            <asp:BoundField HeaderText="<%$ Resources:labels, quyensudung %>" DataField="colRole" />
                                                        </Columns>
                                                        <FooterStyle CssClass="gvFooterStyle" />
                                                        <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                                                        <SelectedRowStyle />
                                                        <HeaderStyle CssClass="gvHeader" />
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" id="tab_2">
                            <div class="row" runat="server" id="Div4">
                                <div class="col-sm-12">
                                    <div class="panel">
                                        <div class="panel-container">
                                            <div class="panel-content form-horizontal p-b-0">
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">
                                                                    <%=Resources.labels.coownercode %></label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtCoownerCode" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <asp:Button ID="btnCoreownerDetail" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, xemchitiet %>"
                                                                OnClick="btnCoownerDetail_Click" />
                                                            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedPanelID="Panel2"
                                                                runat="server">
                                                                <ProgressTemplate>
                                                                    <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                                                                    <%=Resources.labels.loading %>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">
                                                                    <%=Resources.labels.tendaydu %></label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtFullnameNguoiUyQuyen" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <label class="col-sm-4 control-label">Email</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtEmailNguoiUyQuyen" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">
                                                                    <%=Resources.labels.phone %></label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtPhoneNguoiUyQuyen" CssClass="form-control" runat="server" OnTextChanged="txtPhoneNguoiUyQuyen_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <label class="col-sm-4 control-label">
                                                                <%=Resources.labels.gender %></label>
                                                            <div class="col-sm-8">
                                                                <asp:DropDownList ID="ddlGenderNguoiUyQuyen" runat="server" Enabled="False" Width="180px" CssClass="form-control select2">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">
                                                                    <%=Resources.labels.birthday %></label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtBirthNguoiUyQuyen" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <label class="col-sm-4 control-label">
                                                                <%=Resources.labels.address %></label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtAddressNguoiUyQuyen" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6" runat="server" visible="false">
                                                            <label class="col-sm-4 control-label"><%=Resources.labels.capbac %></label>
                                                            <div class="col-sm-8">
                                                                <asp:Label ID="lblLevelNguoiUyQuyen" runat="server" Text="1"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6"></div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="div6">
                                <div class="col-sm-12">
                                    <div class="nav-tabs-custom">
                                        <ul class="nav nav-tabs">
                                            <%   if (TabCustomerInfoHelperCoOwner.TabMobileVisibility == 1)
                                                { %>

                                            <li class="active" id="li3" runat="server"><a href="#tabNUQ_1" data-toggle="tab"><%=Resources.labels.mobilebanking %></a></li>
                                            <%}
                                                if (TabCustomerInfoHelperCoOwner.TabWalletVisibility == 1)
                                                { %>

                                            <li id="li4" runat="server"><a href="#tabNUQ_2" data-toggle="tab"><%=Resources.labels.walletbanking %></a></li>
                                            <%}
                                                if (TabCustomerInfoHelperCoOwner.TabSMSVisibility == 1)
                                                { %>

                                            <li id="li5" runat="server"><a href="#tabNUQ_3" data-toggle="tab"><%=Resources.labels.smsbanking %></a></li>
                                            <%}
                                                if (TabCustomerInfoHelperCoOwner.TabIBVisibility == 1)
                                                { %>

                                            <li id="li6" runat="server"><a href="#tabNUQ_4" data-toggle="tab"><%=Resources.labels.internetbanking %></a></li>
                                            <%}
                                            %>
                                        </ul>
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tabNUQ_1">
                                                <table id="Table5" class="style1" cellspacing="0" cellpadding="4">
                                                    <tr>
                                                        <td colspan="2" style="background-color: #F5F5F5; color: #38277c;">
                                                            <asp:Label ID="lbMB10" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                                        </td>
                                                        <td style="background-color: #F5F5F5; color: #38277c;">
                                                            <asp:Label ID="lbMB11" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; height: 25px" valign="top">
                                                            <asp:Label ID="Label65" runat="server" Text="<%$ Resources:labels, username %>"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <div class="col-sm-2">
                                                                <asp:RadioButton ID="rbMBGenerateNguoiUyQuyen" runat="server" GroupName="rbUserNameNguoiUyQuyen"
                                                                    Checked="True" ClientIDMode="Static" onclick="DisabledTextbox(true,'txtMBGenUserNameNguoiUyQuyen')" />
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtMBPhoneNguoiUyQuyen" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <br />
                                                            <br />
                                                            <div class="col-sm-2">
                                                                <asp:RadioButton ID="rbMBTypeNguoiUyQuyen" runat="server"
                                                                    GroupName="rbUserNameNguoiUyQuyen" onclick="DisabledTextbox(false,'txtMBGenUserNameNguoiUyQuyen')"
                                                                    ClientIDMode="Static" />
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtMBGenUserNameNguoiUyQuyen" CssClass="form-control" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                        <td rowspan="5" style="width: 50%;">
                                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                                <asp:TreeView ID="tvMBUyQuyen" runat="server">
                                                                </asp:TreeView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>

                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlAccUyQuyen" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlpolicyMBco" Width="100%" runat="server" CssClass="form-control select2 infinity" AutoPostBack="true" OnSelectedIndexChanged="ddlpolicyMBco_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </div>
                                            <div class="tab-pane" id="tabNUQ_2">
                                                <table id="Table3" class="style1" cellspacing="0" cellpadding="4">
                                                    <tr>
                                                        <td colspan="2" style="background-color: #F5F5F5; color: #38277c;">
                                                            <asp:Label ID="Label47" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                                        </td>
                                                        <td style="background-color: #F5F5F5; color: #38277c;">
                                                            <asp:Label ID="Label48" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; height: 25px" valign="top">
                                                            <asp:Label ID="Label49" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                                        </td>
                                                        <td style="width: 25%;" valign="top">
                                                            <asp:TextBox ID="txtWLNguoiUyQuyen" runat="server" Enabled="False"></asp:TextBox>
                                                        </td>
                                                        <td rowspan="4" style="width: 50%;">
                                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                                <asp:TreeView ID="tvWLUyQuyen" runat="server">
                                                                </asp:TreeView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>
                                                        </td>
                                                        <td class="form-group">
                                                            <div>
                                                                <asp:DropDownList ID="ddlpolicyWLco" runat="server" CssClass="form-control select2 infinity"></asp:DropDownList>
                                                            </div>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>

                                                </table>
                                            </div>
                                            <div class="tab-pane" id="tabNUQ_3">
                                                <table id="Table2" class="style1" cellspacing="0" cellpadding="4">
                                                    <tr>
                                                        <td colspan="2" style="background-color: #F5F5F5; color: #38277c; width: 50%;">
                                                            <asp:Label ID="Label341" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                                        </td>
                                                        <td style="background-color: #F5F5F5; color: #38277c; width: 50%;">
                                                            <asp:Label ID="Label37" runat="server" Font-Bold="True" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%;" valign="top">
                                                            <asp:Label ID="Label38" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                                                        </td>
                                                        <td style="width: 25%;" valign="top">
                                                            <asp:TextBox ID="txtSMSPhoneNguoiUyQuyen" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td rowspan="5" style="width: 50%;">
                                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                                <asp:TreeView ID="tvSMSUyQuyen" runat="server">
                                                                </asp:TreeView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 20%;" valign="top">
                                                            <asp:Label ID="Label41" runat="server" Text="<%$ Resources:labels, taikhoanmacdinh %>"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="ddlSMSDefaultAcctnoUyQuyen" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 20%;" valign="top">
                                                            <asp:Label ID="Label44" runat="server" Text="<%$ Resources:labels, language %>"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="ddlDefaultLanguageNguoiUyQuyen" runat="server">
                                                                <asp:ListItem Value="E">English</asp:ListItem>
                                                                <asp:ListItem Value="M" Text="<%$Resources:labels, myanmar %>"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%;" valign="top">
                                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                                            <%=Resources.labels.dungpolicy %>
                                                            <%--<asp:Label ID="Label64" Font-Bold="true" ForeColor="Red" runat="server" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                                        </td>
                                                        <td valign="top" style="width: 25%;">
                                                            <asp:DropDownList ID="ddlpolicySMSco" runat="server"></asp:DropDownList>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="width: 20%;" valign="top">
                                                            <asp:CheckBox ID="cbCTKTKMD" runat="server" Text="<%$ Resources:labels, sudungmacdinh %>"
                                                                Checked="True" />
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Label ID="Label61" runat="server" Text="<%$ Resources:labels, warningoneaccountononesmsrole %>" Style="color: red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="tab-pane" id="tabNUQ_4">
                                                <table id="Table1" class="style1" cellspacing="0" cellpadding="4">
                                                    <tr>
                                                        <td colspan="2" style="background-color: #F5F5F5; color: #38277c;">
                                                            <asp:Label ID="Label311" runat="server" Font-Bold="true" Text="<%$ Resources:labels, thongtindangnhap %>"></asp:Label>
                                                        </td>
                                                        <td style="background-color: #F5F5F5; color: #38277c;">
                                                            <asp:Label ID="Label321" runat="server" Font-Bold="true" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; height: 50px" valign="top">
                                                            <asp:Label ID="Label331" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                                                        </td>
                                                        <td valign="top" style="width: 25%;">
                                                            <asp:RadioButton ID="rbGenerateNguoiUyQuyen" runat="server" GroupName="rbUserNameNguoiUyQuyenIB"
                                                                Checked="True" ClientIDMode="Static" onclick="DisabledTextbox(true,'txtIBTypeUserNameNguoiUyQuyen')" />
                                                            <asp:TextBox ID="txtUsernameIBNguoiUyQuyen" runat="server"></asp:TextBox>
                                                            <br />
                                                            <asp:RadioButton ID="rbTypeNguoiUyQuyen" runat="server" GroupName="rbUserNameNguoiUyQuyenIB" onclick="DisabledTextbox(false,'txtIBTypeUserNameNguoiUyQuyen')"
                                                                ClientIDMode="Static" />
                                                            <asp:TextBox ID="txtIBTypeUserNameNguoiUyQuyen" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                            <asp:HiddenField ID="hdfIBUserNameNguoiUyQuyen" runat="server" />
                                                        </td>
                                                        <td rowspan="2" style="width: 50%;">
                                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                                <asp:TreeView ID="tvIBUyQuyen" runat="server">
                                                                </asp:TreeView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%;" valign="top">
                                                            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 25px; height: 25px; margin-bottom: 10px;" align="middle" />
                                                            <%=Resources.labels.dungpolicy %>
                                                            <%--<asp:Label ID="Label63" runat="server" Font-Bold="true" ForeColor="Red" Text="<%$ Resources:labels, dungpolicy %>"></asp:Label>--%>
                                                        </td>
                                                        <td valign="top" style="width: 25%; padding-left: 25px">
                                                            <asp:DropDownList ID="ddlpolicyIBco" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpolicyIBco_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="Div7">
                                <div class="col-sm-12">

                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upNUY">
                                        <ProgressTemplate>
                                            <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                                            <%=Resources.labels.loading %>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

                                </div>
                            </div>
                            <div class="row" runat="server" id="Div9">
                                <div class="col-sm-12">
                                    <asp:UpdatePanel runat="server" ID="upNUY">
                                        <ContentTemplate>
                                            <div style="text-align: right;">
                                                <asp:Button ID="btnThemNguoiUyQuyen" runat="server" CssClass="btn btn-primary" OnClick="btnThemNguoiUyQuyen_Click"
                                                    Text='<%$ Resources:labels, them %>' />
                                                &nbsp;
                                <asp:Button ID="btnHuyDSH" runat="server" OnClick="btnHuyDSH_Click" CssClass="btn btn-secondary" Text="<%$ Resources:labels, delete %>" />
                                                &nbsp;
                                <asp:Button ID="btnResetNguoiUyQuyen" runat="server" CssClass="btn btn-primary" OnClick="btnResetNguoiUyQuyen_Click"
                                    Text="<%$ Resources:labels, themmoi %>" />
                                                &nbsp;
                                            </div>
                                            &nbsp;&nbsp;<asp:Label runat="server" ID="lblAlertDSH" ForeColor="Red"></asp:Label>
                                            <div id="divResultNguoiUyQuyen" style="margin-top: 20px; height: 150px; overflow: auto;">
                                                <asp:GridView ID="gvResultNguoiUyQuyen" CssClass="table table-hover" runat="server" AutoGenerateColumns="False"
                                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                    CellPadding="3" Width="100%" OnPageIndexChanging="gvResultNguoiUyQuyen_PageIndexChanging"
                                                    OnRowDeleting="gvResultNguoiUyQuyen_RowDeleting">
                                                    <RowStyle ForeColor="#000066" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="<%$ Resources:labels, tennguoidung1 %>" DataField="colFullName" />
                                                        <asp:BoundField DataField="colPhone" HeaderText="<%$ Resources:labels, username %>" />
                                                        <asp:BoundField HeaderText="<%$ Resources:labels, taikhoan %>" DataField="colAccount" />
                                                        <asp:BoundField HeaderText="<%$ Resources:labels, quyensudung %>" DataField="colRole" />
                                                    </Columns>
                                                    <FooterStyle CssClass="gvFooterStyle" />
                                                    <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                                                    <SelectedRowStyle />
                                                    <HeaderStyle CssClass="gvHeader" />
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="pnLuu">
            <div style="text-align: center; padding-top: 10px;">
                <asp:Button ID="btnCustSave" runat="server" OnClick="btnCustSave_Click" OnClientClick="return validate1();"
                    Text="<%$ Resources:labels, save %>" Width="69px" CssClass="btn btn-primary" />
                &nbsp;
            <asp:Button ID="btnBack" runat="server" OnClick="Button2_Click" CssClass="btn btn-secondary" Text="<%$ Resources:labels, back %>" />
                &nbsp; &nbsp;
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnCustSave" />
        <asp:PostBackTrigger ControlID="btnBack" />
    </Triggers>
</asp:UpdatePanel>
<script type="text/javascript">//<![CDATA[

    function validate() {
        if (validateEmpty('<%=txtCustCodeInfo.ClientID %>', '<%=Resources.labels.makhachhangkhongrong %>')) {
            if (validateMoney('<%=txtFullName.ClientID %>', '<%=Resources.labels.bannhaptenkhachhang %>')) {
                if (validateEmpty('<%=txtContractNo.ClientID %>', '<%=Resources.labels.mahopdongkhongrong %>')) {
                    if (validateEmpty('<%=txtStartDate.ClientID %>', '<%=Resources.labels.bannhapngayhieuluc %>')) {
                        if (validateEmpty('<%=txtEndDate.ClientID %>', '<%=Resources.labels.bannhapngayhethan %>')) {
                            if (IsPhoneNum('<%=txtPhoneWL.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                                //kiem tra ngay hết hạn lớn hơn ngày bắt đầu
                                if (validateEmpty('<%=txtResidentAddr.ClientID %>', '<%=Resources.labels.bancannhapdiachithuongtru %>')) {
                                    if (IsDateGreater('<%=txtEndDate.ClientID %>', '<%=txtStartDate.ClientID %>', '<%=Resources.labels.ngayhethanphailonhonngayhieuluc %>')) {
                                        if (checkEmail('<%=txtEmail.ClientID %>', '<%=Resources.labels.emailkhongdinhdang %>')) { }
                                        else {
                                            document.getElementById('<%=txtEmail.ClientID %>').focus();
                                            return false;
                                        }
                                    }
                                    else {
                                        document.getElementById('<%=txtStartDate.ClientID %>').focus();
                                        return false;
                                    }
                                }
                                else {
                                    document.getElementById('<%=txtResidentAddr.ClientID %>').focus();
                                    return false;
                                }

                            }
                            else {
                                document.getElementById('<%=txtPhoneWL.ClientID %>').focus();
                                return false;
                            }
                        }
                        else {
                            document.getElementById('<%=txtEndDate.ClientID %>').focus();
                            return false;
                        }
                    }
                    else {
                        document.getElementById('<%=txtStartDate.ClientID %>').focus();
                        return false;
                    }
                }
                else {
                    document.getElementById('<%=txtContractNo.ClientID %>').focus();
                    return false;
                }
            }
            else {
                return false;
            }
        }
        else {

            return false;
        }
    }
    function checkEmail(emailID, aler) {
        var email = document.getElementById(emailID);
        var value = document.getElementById(emailID).value;
        if (value != '') {
            var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
            if (!filter.test(email.value)) {
                alert(aler);
                return false;
            }
            return true;
        }
        else {
            return true;
        }
    }
    var cal = Calendar.setup({
        onSelect: function (cal) { cal.hide() }
    });
    cal.manageFields("<%=txtStartDate.ClientID %>", "<%=txtStartDate.ClientID %>", "%d/%m/%Y");
    cal.manageFields("<%=txtEndDate.ClientID %>", "<%=txtEndDate.ClientID %>", "%d/%m/%Y");
    cal.manageFields("<%=txtBirth.ClientID %>", "<%=txtBirth.ClientID %>", "%d/%m/%Y");
    cal.manageFields("<%=txtIssueDate.ClientID %>", "<%=txtIssueDate.ClientID %>", "%d/%m/%Y");
    //]]></script>
<script type="text/javascript">

    function validate1() {
        try {

            if (IsNumeric('<%=txtSMSPhoneNo.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                //edit by vutran 05082014 IB,MB sample user
                if (validateEmpty('<%=txtPhoneMB.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                    if (validateEmpty('<%=txtReFullName.ClientID %>', '<%=Resources.labels.bannhaptennguoidung %>')) {
                        if (IsPhoneNum('<%=txtPhoneWL.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                            if (validateEmpty('<%=txtReMobi.ClientID %>', '<%=Resources.labels.bannhapsodienthoainguoidung %>')) {
                                <%--if (validateEmpty('<%=txtReEmail.ClientID %>', '<%=Resources.labels.bannhapemail %>')) {--%>
                                //kiem tra so
                                if (IsPhoneNum('<%=txtReMobi.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {

                                }
                                else {
                                        //document.getElementById('<%=txtReMobi.ClientID %>').focus();
                                    return false;
                                }
                                <%--}
                                else {
                                    //document.getElementById('<%=txtReEmail.ClientID %>').focus();
                                    return false;
                                }--%>
                            }
                            else {
                                //document.getElementById('<%=txtReMobi.ClientID %>').focus();
                                return false;
                            }
                        }
                        else {
                                //document.getElementById('<%=txtPhoneWL.ClientID %>').focus();
                            return false;
                        }
                    }
                    else {
                            //document.getElementById('<%=txtReFullName.ClientID %>').focus();
                        return false;
                    }

                }
                else {

                    return false;
                }
            }
            else {

                return false;
            }
        }
        catch (err) {
        }
    }

    function validate2() {
        try {
            if (IsNumeric('<%=txtSMSPhoneNguoiUyQuyen.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                //edit by vutran 05082014 IB,MB sample user
                if (validateEmpty('<%=txtMBPhoneNguoiUyQuyen.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                    if (validateEmpty('<%=txtFullnameNguoiUyQuyen.ClientID %>', '<%=Resources.labels.bannhaptennguoidongsohuu %>')) {
                        if (validateEmpty('<%=txtPhoneNguoiUyQuyen.ClientID %>', '<%=Resources.labels.bannhapsodienthoainguoidongsohuu %>')) {
                               <%-- if (validateEmpty('<%=txtEmailNguoiUyQuyen.ClientID %>', '<%=Resources.labels.bannhapemailnguoidongsohuu %>')) {--%>
                            //kiem tra so
                            if (IsPhoneNum('<%=txtPhoneNguoiUyQuyen.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>')) {
                                        //kiem tra email
                                       <%-- if (checkEmail('<%=txtEmailNguoiUyQuyen.ClientID %>', '<%=Resources.labels.emailkhongdinhdang %>')) {--%>
                                if (document.getElementById('<%=rbTypeNguoiUyQuyen.ClientID %>').checked) {
                                    if (validateEmpty('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>', '<%=Resources.labels. bancannhaptendangnhap %>')) {
                                        var minlength = '<%= int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString())%>'
                                        var maxlength = '<%= int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString())%>'
                                        if (document.getElementById('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>').value.length >= minlength && document.getElementById('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>').value.length <= maxlength) {

                                        } else {
                                            alert('<%=string.Format(Resources.labels.usernamemustbetween, ConfigurationManager.AppSettings["minlengthloginname"].ToString(), ConfigurationManager.AppSettings["maxlengthloginname"].ToString()) %>');
                                            return false;
                                        }
                                    } else {
                                        return false;
                                    }
                                }
                                        <%--}
                                        else {
                                            document.getElementById('<%=txtEmailNguoiUyQuyen.ClientID %>').focus();
                                            return false;
                                        }--%>
                            }
                            else {
                                document.getElementById('<%=txtPhoneNguoiUyQuyen.ClientID %>').focus();
                                return false;
                            }
                                <%--}
                                else {
                                    document.getElementById('<%=txtEmailNguoiUyQuyen.ClientID %>').focus();
                                    return false;
                                }--%>
                        }
                        else {
                            document.getElementById('<%=txtPhoneNguoiUyQuyen.ClientID %>').focus();
                            return false;
                        }

                    }
                    else {
                        document.getElementById('<%=txtFullnameNguoiUyQuyen.ClientID %>').focus();
                        return false;
                    }
                }
                else {

                    return false;
                }
            }
            else {

                return false;
            }
        }
        catch (err) {
        }
    }
</script>
<script>
    function SetUserName(custcode, custtype, length) {
        var date = new Date();
        var a = ticks(date).toString().length - 10;
        var b = length - ((custcode + custtype).length);
        var c = 10 - (custcode.length);

        var un = custcode + custtype + ticks(date).toString().substring(a, a + b) + "3";

        var mb = custcode + ticks(date).toString().substring(a, a + c) + "3";
        var pho = custcode + ticks(date).toString().substring(a, a + c) + "3";

        document.getElementById('<%=txtUsernameIBNguoiUyQuyen.ClientID %>').value = un;

        document.getElementById('<%=txtFullnameNguoiUyQuyen.ClientID %>').value = '';
        document.getElementById('<%=txtPhoneNguoiUyQuyen.ClientID %>').value = '';
        document.getElementById('<%=txtEmailNguoiUyQuyen.ClientID %>').value = '';
        document.getElementById('<%=txtBirthNguoiUyQuyen.ClientID %>').value = '';
        document.getElementById('<%=txtAddressNguoiUyQuyen.ClientID %>').value = '';
        document.getElementById('<%=txtSMSPhoneNguoiUyQuyen.ClientID %>').value = '';
        document.getElementById('<%=txtMBPhoneNguoiUyQuyen.ClientID %>').value = mb;

        document.getElementById('<%=txtFullnameNguoiUyQuyen.ClientID %>').focus();
        return false;
    }

    function ticks(date) {

        this.day = date.getDate();
        this.month = date.getMonth() + 1;
        this.year = date.getFullYear();
        this.hour = date.getHours();
        this.minute = date.getMinutes();
        this.second = date.getSeconds();
        this.ms = date.getMilliseconds();

        this.monthToDays = function (year, month) {
            var add = 0;
            var result = 0;
            if ((year % 4 == 0) && ((year % 100 != 0) || ((year % 100 == 0) && (year % 400 == 0)))) add++;

            switch (month) {
                case 0: return 0;
                case 1: result = 31; break;
                case 2: result = 59; break;
                case 3: result = 90; break;
                case 4: result = 120; break;
                case 5: result = 151; break;
                case 6: result = 181; break;
                case 7: result = 212; break;
                case 8: result = 243; break;
                case 9: result = 273; break;
                case 10: result = 304; break;
                case 11: result = 334; break;
                case 12: result = 365; break;
            }
            if (month > 1) result += add;
            return result;
        }

        this.dateToTicks = function (year, month, day) {
            var a = parseInt((year - 1) * 365);
            var b = parseInt((year - 1) / 4);
            var c = parseInt((year - 1) / 100);
            var d = parseInt((a + b) - c);
            var e = parseInt((year - 1) / 400);
            var f = parseInt(d + e);
            var monthDays = this.monthToDays(year, month - 1);
            var g = parseInt((f + monthDays) + day);
            var h = parseInt(g - 1);
            return h * 864000000000;
        }

        this.timeToTicks = function (hour, minute, second) {
            return (((hour * 3600) + minute * 60) + second) * 10000000;
        }

        return this.dateToTicks(this.year, this.month, this.day) + this.timeToTicks(this.hour, this.minute, this.second) + (this.ms * 10000);
    }

</script>

<script type="text/javascript">
    function DisabledTextbox(diabled, txtID) {
        var el = document.getElementById(txtID);
        el.disabled = diabled;
    }

    var minlength = <%= int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString())%>;
    var maxlength = <%= int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString())%>;
    var IBMBSameUser =<%= int.Parse(ConfigurationManager.AppSettings["IBMBSameUser"].ToString())%>;
    //Edit by VuTran 04082014 IB,MB Sample user
    $(document).on("click", "#tabTabdhtmlgoodies_tabView1_2", function () {
        var elem = document.getElementById('')
        var txtType = document.getElementById('')
        var hdf = document.getElementById('')
        var txtType1 = document.getElementById('<%=txtUsernameIB.ClientID %>')
        if (IBMBSameUser == 1) {
            if (elem.checked == true) {
                document.getElementById('<%=txtPhoneMB.ClientID %>').value = txtType.value;
            }
            else {
                document.getElementById('<%=txtPhoneMB.ClientID %>').value = txtType1.value;
            }
        }
    });
    $(document).on("click", "#tabTabdhtmlgoodies_tabView2_2", function () {
        var elem = document.getElementById('<%=rbTypeNguoiUyQuyen.ClientID %>')
        var txtType = document.getElementById('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>')
        var hdf = document.getElementById('<%=hdfIBUserNameNguoiUyQuyen.ClientID %>')
        var txtType1 = document.getElementById('<%=txtUsernameIBNguoiUyQuyen.ClientID %>')
        if (IBMBSameUser == 1) {
            if (elem.checked == true) {
                document.getElementById('<%=txtMBPhoneNguoiUyQuyen.ClientID %>').value = txtType.value;
            }
            else {
                document.getElementById('<%=txtMBPhoneNguoiUyQuyen.ClientID %>').value = txtType1.value;
            }
        }
    });


    $(document).on("click", "#rbType", function () {
        var elem = document.getElementById('')
        var txtType = document.getElementById('')
        var hdf = document.getElementById('')
        if (elem.checked == true) {
            txtType.disabled = false;
            hdf.value = txtType.value;
        }
    });
    $(document).on("click", "#rbGenerate", function () {
        var elem = document.getElementById('<%=rbGenerate.ClientID %>')
        var txtType = document.getElementById('')
        var txtGen = document.getElementById('')
        var hdf = document.getElementById('')
        if (elem.checked == true) {
            txtType.disabled = true;
            hdf.value = txtGen.value;
        }
    });
    $(document).on("change", "", function () {
        var txtType = document.getElementById('');
        if (txtType.value.length < minlength || txtType.value.length > maxlength) {
            alert('<%=string.Format(Resources.labels.usernamemustbetween, ConfigurationManager.AppSettings["minlengthloginname"].ToString(), ConfigurationManager.AppSettings["maxlengthloginname"].ToString()) %>');
        }
        validateCode(txtType.value);

    });

    $(document).on("click", "#rbTypeNguoiUyQuyen", function () {
        var elem = document.getElementById('<%=rbTypeNguoiUyQuyen.ClientID %>')
        var txtType = document.getElementById('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>')
        var hdf = document.getElementById('<%=hdfIBUserNameNguoiUyQuyen.ClientID %>')
        if (elem.checked == true) {
            txtType.disabled = false;
            hdf.value = txtType.value;
        }
    });
    $(document).on("click", "#rbGenerateNguoiUyQuyen", function () {
        var elem = document.getElementById('<%=rbGenerateNguoiUyQuyen.ClientID %>')
        var txtType = document.getElementById('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>')
        var txtGen = document.getElementById('<%=txtUsernameIBNguoiUyQuyen.ClientID %>')
        var hdf = document.getElementById('<%=hdfIBUserNameNguoiUyQuyen.ClientID %>')
        if (elem.checked == true) {
            txtType.disabled = true;
            hdf.value = txtGen.value;
        }
    });
    $(document).on("change", "#<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>", function () {
        var txtType = document.getElementById('<%=txtIBTypeUserNameNguoiUyQuyen.ClientID %>');
        if (txtType.value.length < minlength || txtType.value.length > maxlength) {
            alert('<%=string.Format(Resources.labels.usernamemustbetween, ConfigurationManager.AppSettings["minlengthloginname"].ToString(), ConfigurationManager.AppSettings["maxlengthloginname"].ToString()) %>');

        }
        validateCode(txtType.value);

    });


</script>
<script type="text/javascript">
    function pageLoad(sender, args) {
        $(document).ready(function () {
            if (CheckNRIC('<%=showTabNRIC()%>') == true) {
                LoadHide("block", "none", "none");
            }
            if (CheckLicense('<%=showTabLicense()%>') == true) {
                LoadHide("none", "block", "none");
            }
            if (CheckPassport('<%=showTabPassport()%>') == true) {
                LoadHide("none", "none", "block");
            }
        });
    }
    if (CheckNRIC('<%=showTabNRIC()%>') == true) {
        LoadHide("block", "none", "none");
    }
    if (CheckLicense('<%=showTabLicense()%>') == true) {
        LoadHide("none", "block", "none");
    }
    if (CheckPassport('<%=showTabPassport()%>') == true) {
        LoadHide("none", "none", "block");
    }
    function accordion() {
        debugger;
        if (CheckNRIC('<%=showTabNRIC()%>') == true) {
            LoadHide("block", "none", "none");
        }
        if (CheckLicense('<%=showTabLicense()%>') == true) {
            LoadHide("none", "block", "none");
        }
        if (CheckPassport('<%=showTabPassport()%>') == true) {
            LoadHide("none", "none", "block");
        }
    }
    function LoadHide(nric, license, passport) {
        <%--document.getElementById("<%=PnPassport.ClientID %>").style.display = "none";
        document.getElementById("<%=pnLicense.ClientID %>").style.display = "none";--%>
        document.getElementById("<%=pnNewNRIC.ClientID %>").style.display = "none";
        if ('<%=showTabNRIC()%>' == 'True') {
            document.getElementById("<%=pnNewNRIC.ClientID %>").style.display = nric;
        }
    }
</script>

<script type="text/javascript">
    function isNumberKeyNumer(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }
    function isKey(evt) {
        var regex = new RegExp("[A-Za-z0-9]");
        var key = String.fromCharCode(event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    }
</script>

<style>
    .red {
        border: 1px solid red !important;
    }

    .relative {
        position: relative;
    }

    #errorMessageNRIC {
        position: absolute;
        top: 30px;
        left: 5px;
        margin-left: 0;
        width: 100%;
        font-size: 11px;
    }

    #errorMessagePassport {
        position: absolute;
        top: 30px;
        left: 5px;
        margin-left: 0;
        width: 100%;
        font-size: 11px;
    }
</style>
