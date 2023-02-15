<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBWesternUnion_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<link href="CSS/css.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBRequestExportStatement/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<script src="JS/mask.js" type="text/javascript"></script>
<script src="JS/docso.js" type="text/javascript"></script>
<link href="CSS/bootstrap-timepicker.min.css" rel="stylesheet" />
<script src="JS/bootstrap-timepicker.min.js"></script>
<script src="JS/jquery.inputmask.js"></script>
<script src="JS/jquery.inputmask.date.extensions.js"></script>
<script src="JS/jquery.inputmask.extensions.js"></script>
<script src="Widgets/IBScheduleTransfer/JS/common.js"></script>
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
    <span><%=Resources.labels.westernunion %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div id="divError">
            <asp:Label runat="server" ID="lblTextError" Font-Bold="true" ForeColor="Red" Text=""></asp:Label>
        </div>
        <div>
            <asp:HiddenField ID="hdTrancode" runat="server" />
            <asp:HiddenField ID="hdTypeID" runat="server" />
            <asp:HiddenField ID="hdActTypeSender" runat="server" />
            <asp:HiddenField ID="hdavailablebalan" runat="server" />
            <asp:HiddenField ID="hdSenderAccount" runat="server" />
            <asp:HiddenField ID="hdSenderName" runat="server" />
            <asp:HiddenField ID="hdSenderBranch" runat="server" />
            <asp:HiddenField ID="hdAmount" runat="server" />
            <asp:HiddenField ID="hdTotalAmount" runat="server" />
            <asp:HiddenField ID="hdfee" runat="server" />
            <asp:HiddenField ID="hdCountryPay" runat="server" />
            <asp:HiddenField ID="hdexchangerate" runat="server" />
            <asp:HiddenField ID="hdexchangeratecciyd" runat="server" />
            <asp:HiddenField ID="hdamountreceiver" runat="server" />
        </div>

        <%--Main screen--%>
        <asp:Panel ID="pnTIB" runat="server" class="divcontent">
            <figure>
                <div class="handle">
                    <asp:Label runat="server" Text='<%$ Resources:labels, senderinfor %>'></asp:Label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="lbdebit" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4 line30">
                            <asp:DropDownList ID="ddlSenderAccount" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="lbavailable" runat="server" Text="<%$ Resources:labels, availablebalance %>"></asp:Label>
                        </div>
                        <div class="col-xs-5 col-md-4 line30">
                            <asp:Label ID="lblAvailableBal" runat="server"></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblAvailableBalCCYID" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, fisrtname %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4 line30">
                            <asp:TextBox ID="txtsenderfirstname" runat="server" CssClass="form-control" Text="" onkeyup="this.value=this.value.replace(/[^a-zA-Z, ]/g , '')">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label33" runat="server" Text='<%$ Resources:labels, lastname %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4 line30">
                            <asp:TextBox ID="txtsenderlastname" runat="server" CssClass="form-control" Text="" onkeyup="this.value=this.value.replace(/[^a-zA-Z, ]/g , '')">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, senderccy %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4 line30" runat="server">
                            <asp:Label ID="lblSenderCCYID" runat="server">USD</asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label3" runat="server" Text='<%$ Resources:labels, payoutcountry %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4 line30">
                            <asp:DropDownList ID="ddlCountryPay" CssClass="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountryPay_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="lbaddress" runat="server" Text='<%$ Resources:labels, amountWU %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:TextBox ID="txtamountsend" runat="server" CssClass="amount" AutoPostBack="true" OnTextChanged="txtamountsend_TextChanged" onkeyup="this.value=this.value.replace(/[^0-9.]/g, '')"></asp:TextBox>
                            <asp:Label ID="lbamountsendccyid" runat="server" Text='USD'></asp:Label>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label7" runat="server" Text='<%$ Resources:labels, fee %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4 line30" runat="server">
                            <asp:Label ID="lbsendfee" runat="server">0.00</asp:Label>
                            <asp:Label ID="Label6" runat="server" Text='USD'></asp:Label>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label9" runat="server" Text='<%$ Resources:labels, tongtien %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4 line30" runat="server">
                            <asp:Label ID="lbtotalAmount" runat="server" Text="0.00"></asp:Label>
                            <asp:Label ID="Label10" runat="server" Text='USD'></asp:Label>
                        </div>
                    </div>
                    <div class="row" runat="server">

                        <div class="col-xs-5 col-md-4 form-control" style="text-align: center; color: #2e6da4">
                            <asp:Label ID="Label40" runat="server" Text='The exchange rate and total amount sent to receiver along with MTCN number will be emailed to you after successful processing of your Western Union Request'></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="handle">
                    <asp:Label runat="server" Text='<%$ Resources:labels, senderinfordetails %>'></asp:Label>
                </div>
                <div class="content_table">
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="lbunit" runat="server" Text='<%$ Resources:labels, temporaryunit %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-sm-4" runat="server">
                            <asp:TextBox ID="txtSenderUnit" runat="server" CssClass="form-control" Text="">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Labledistrict" runat="server" Text='<%$ Resources:labels, temporarydistrict %>'></asp:Label>
                            &nbsp;
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:TextBox ID="txtSenderdistrict" runat="server" CssClass="form-control" Text="">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="lablecity" runat="server" Text='<%$ Resources:labels, thanhpho %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:TextBox ID="txtSenderCity" runat="server" CssClass="form-control" Text="" onkeyup="this.value=this.value.replace(/[^a-zA-Z0-9, ]/g , '')">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="lableprovince" runat="server" Text='<%$ Resources:labels, province %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:TextBox ID="txtProvince" runat="server" CssClass="form-control" Text="" onkeyup="this.value=this.value.replace(/[^a-zA-Z0-9, ]/g , '')">
                            </asp:TextBox>
                        </div>
                    </div>

                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label12" runat="server" Text='<%$ Resources:labels, postcode %>'></asp:Label>
                            &nbsp;
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:TextBox ID="txtpostcode" runat="server" CssClass="form-control" Text="" onkeyup="this.value=this.value.replace(/[^a-zA-Z0-9, ]/g , '')">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label13" runat="server" Text='<%$ Resources:labels, country %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:DropDownList ID="ddlSenderCountry" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label5" runat="server" Text='<%$ Resources:labels, countrycode %>'></asp:Label>
                            &nbsp;
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:DropDownList ID="ddlCountryCode" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label8" runat="server" Text='<%$ Resources:labels, telephonenum %>'></asp:Label>
                            &nbsp;
                        </div>
                        <div class="col-xs-5 col-md-4 line30">
                            <asp:TextBox ID="txttelephone" runat="server" MaxLength="12" CssClass="form-control" onkeyup="this.value=this.value.replace(/[^0-9.]/g, '')"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label43" runat="server" Text='<%$ Resources:labels, mbcountrycode %>'></asp:Label>
                            &nbsp;
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:DropDownList ID="ddlmobilecode" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label15" runat="server" Text='<%$ Resources:labels, mobilephone %>'></asp:Label>
                            &nbsp;
                        </div>
                        <div class="col-xs-5 col-md-4 line30">
                            <asp:TextBox ID="txtmobilephone" runat="server" AutoPostBack="true" CssClass="form-control" MaxLength="12" onkeyup="this.value=this.value.replace(/[^0-9.]/g, '')"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 form-control center" style="text-align: center; color: #2e6da4">
                            <asp:Label ID="Label35" runat="server" Text='<%$ Resources:labels, mobilephonewu %>'></asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label39" runat="server" Text='<%$ Resources:labels, email %>'></asp:Label>
                            &nbsp;
                        </div>
                        <div class="col-xs-5 col-md-4 line30">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </figure>
            <div class="row" style="text-align: center; padding-top: 10px;">
                <asp:Button Width="100px" ID="btncontinute" runat="server" CssClass="btn btn-primary" Text='<%$ Resources:labels, tieptuc %>' OnClientClick="return validate1();" OnClick="btncontinute_Click" />
            </div>
        </asp:Panel>

        <%--Pannel 2------------------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
        <asp:Panel ID="pnTIB1" runat="server" class="divcontent" Visible="false">
            <figure>
                <div class="handle">
                    <asp:Label runat="server" Text='<%$ Resources:labels, senderiddetails %>'></asp:Label>
                </div>
                <div class="content_table">
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="Label16" runat="server" Text='<%$ Resources:labels, idtype %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-sm-4" runat="server">
                            <asp:DropDownList ID="ddlKYC" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label17" runat="server" Text='<%$ Resources:labels, idnumber %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:TextBox ID="txtIDnumber" runat="server" CssClass="form-control" Text="">
                            </asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label18" runat="server" Text='<%$ Resources:labels, countryofissue %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:Label ID="Label32" runat="server" Text='Laos'></asp:Label>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label19" runat="server" Text='<%$ Resources:labels, expdate %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:TextBox ID="txtexpitydate" autocomplete="off" type="date" runat="server">
                            </asp:TextBox>
                            <%--<asp:TextBox ID="txtexpitydate" runat="server" autocomplete="off" CssClass="dateselect form-control" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask></asp:TextBox>--%>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label20" runat="server" Text='<%$ Resources:labels, issdate %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:TextBox ID="txtissuedate" autocomplete="off" type="date" runat="server">
                            </asp:TextBox>
                            <%--<asp:TextBox ID="txtissuedate" runat="server" CssClass="form-control pull-right dateselect1" autocomplete="off" data-name="date1" data-level="0"></asp:TextBox>--%>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label21" runat="server" Text='<%$ Resources:labels, DOB %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:TextBox ID="txtdateofbirth" autocomplete="off" type="date" runat="server">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label14" runat="server" Text='<%$ Resources:labels, officephone %>'></asp:Label>
                            &nbsp;
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:TextBox ID="txtOfficephone" runat="server" CssClass="form-control" Text="" onkeyup="this.value=this.value.replace(/[^0-9, ]/g , '')">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label22" runat="server" Text='<%$ Resources:labels, occupation %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:DropDownList ID="dllOccupation" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label23" runat="server" Text='<%$ Resources:labels, purpose %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:DropDownList ID="ddlPurpose" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label24" runat="server" Text='<%$ Resources:labels, sourceoffund %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:DropDownList ID="dllSourceFund" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label25" runat="server" Text='<%$ Resources:labels, permanentunit %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:TextBox ID="txtIDunit" runat="server" CssClass="form-control" Text="" onkeyup="this.value=this.value.replace(/[^a-zA-Z0-9, ]/g , '')">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label26" runat="server" Text='<%$ Resources:labels, permanentdistrict %>'></asp:Label>
                            &nbsp;
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:TextBox ID="txtIDdistrict" runat="server" CssClass="form-control" Text="" onkeyup="this.value=this.value.replace(/[^a-zA-Z0-9, ]/g , '')">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label27" runat="server" Text='<%$ Resources:labels, permanentcity %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:TextBox ID="txtIDProvince" runat="server" CssClass="form-control" Text="" onkeyup="this.value=this.value.replace(/[^a-zA-Z0-9, ]/g , '')">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label28" runat="server" Text='<%$ Resources:labels, permanentcountry %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:DropDownList ID="dllCountryPermanent" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label29" runat="server" Text='<%$ Resources:labels, countryofbirth %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:DropDownList ID="ddlcountryofbirth" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label30" runat="server" Text='<%$ Resources:labels, nationality %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:DropDownList ID="ddlNationality" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label31" runat="server" Text='<%$ Resources:labels, gender %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:DropDownList ID="ddlGender" CssClass="form-control" runat="server">
                                <asp:ListItem Value="M" Text="<%$ Resources:labels, male %>"></asp:ListItem>
                                <asp:ListItem Value="F" Text="<%$ Resources:labels, female %>"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label34" runat="server" Text='<%$ Resources:labels, comment %>'></asp:Label>
                            &nbsp;
                        </div>
                        <div class="col-xs-5 col-md-4" runat="server">
                            <asp:TextBox ID="txtcomment" runat="server" CssClass="form-control" Text="" onkeyup="this.value=this.value.replace(/[^a-zA-Z0-9, ]/g , '')">
                            </asp:TextBox>
                        </div>
                    </div>
                    <%--<div class="form-group">
                        <div class="row" style="padding-bottom: 15px">
                            <div class="col-xs-5 col-md-4 right" style="MARGIN-LEFT: 24%;">
                                    <asp:FileUpload ID="FUNRICFontNew" runat="server" AutoPostBack="true" ClientIDMode="Static" accept=".png,.jpg,.jpeg" />
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
                            <div class="col-xs-5 col-md-3">
                                <div class="col-sm-12">
                                    <asp:Button ID="btnUpLoadFile" runat="server" Text="Upload" CssClass="btn btn-primary"/>
                                </div>
                            </div>
                        </div>
                    </div>--%>

                    <div class="row form-group">
                        <div class="col-xs-3 col-sm-4 right">
                            <asp:Label ID="LblDocument" runat="server" Text=""><%= Resources.labels.document %>&nbsp;*</asp:Label>
                        </div>
                        <div class="col-xs-4 col-md-4 col-sm-6 line30">
                            <asp:FileUpload ID="FUDocument" AllowMultiple="false" runat="server" ClientIDMode="Static" accept=".pdf,.png,.jpg,.jpeg" />
                        </div>
                        <div class="col-xs-5 col-md-4 col-sm-6 line30">
                            <asp:Label ID="LblDocumentExpalainion" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666" Text='<%$ Resources:labels, uploadlimit1MB %>'></asp:Label>
                        </div>
                    </div>
                </div>
            </figure>
            <figure>
                <div class="handle">
                    <asp:Label runat="server" Text='<%$ Resources:labels, receiverdetails %>'></asp:Label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label36" runat="server" Text='<%$ Resources:labels, fisrtname %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4 line30">
                            <asp:TextBox ID="txtReceiverfirstname" runat="server" CssClass="form-control" Text="" onkeyup="this.value=this.value.replace(/[^a-zA-Z, ]/g , '')">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-md-4 right">
                            <asp:Label ID="Label37" runat="server" Text='<%$ Resources:labels, lastname %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-5 col-md-4 line30">
                            <asp:TextBox ID="txtReceiverlastname" runat="server" CssClass="form-control" Text="" onkeyup="this.value=this.value.replace(/[^a-zA-Z, ]/g , '')">
                            </asp:TextBox>
                        </div>
                    </div>
                </div>
            </figure>
            <div style="text-align: center; margin-top: 10px;">
                <asp:Button Width="100px" ID="btnback1" runat="server" CssClass="btn btn-danger" Text='<%$ Resources:labels, back %>' OnClick="btnback1_Click" />
                <asp:Button ID="btnRequest" runat="server" CssClass="btn btn-primary" OnClick="btnRequest_Click" OnClientClick="return validate();" Text='<%$ Resources:labels, sendrequest %>' />
            </div>
        </asp:Panel>

        <%--Confirm---------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
        <asp:Panel ID="pnConfirm" runat="server" class="divcontent" Visible="false">
            <div class="handle">
                <span><%=Resources.labels.thongtinnguoitratien %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.fisrtname %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblSenderFirstName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.lastname %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblSenderLastName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.debitaccount %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblSenderAccount" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.availablebalance %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblBalanceSender" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblBalanceccyidSender" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.payoutcountry %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbpayoutcountry" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.temporaryunit %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbSenderUnit" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.temporarydistrict %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbSenderdistrict" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.thanhpho %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbsendercity" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.province %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbsenderProvince" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.postcode %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblpostcode" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.country %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbSendercountry" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.countrycode %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblContrycode" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.telephonenum %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblTelephone" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.mbcountrycode %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbmobiphonecoode" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.mobilephone %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblMobile" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.email %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblemail" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <span>ID information</span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.idtype %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbidtype" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.idnumber %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbIDnumber" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.countryofissue %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="Label42" runat="server">Laos</asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.expdate %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbexpiry" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.issdate %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbissuedate" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.DOB %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbdateodbirth" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.officephone %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblofficephone" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.occupation %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lboccupation" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.purpose %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbpurpose" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.sourceoffund %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbsourceoffund" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.temporaryunit %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lnicunit" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.temporarydistrict %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbicdistrict" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.thanhpho %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lciccity" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.country %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbCountry" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.countryofbirth %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblicCoutryofbirth" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.nationality %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbnation" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.gender %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbgender" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.comment %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblcoment" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span>File Upload</span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbfileupload" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <span><%=Resources.labels.thongtinnguoinhantien %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.firstname %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbReceiverfistname" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.lastname %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbReceiverlastname" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <asp:Label runat="server" Text='Summary'></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label64" runat="server" Text='<%$ Resources:labels, transamount %>'></asp:Label>
                        &nbsp;
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbtranferamount" runat="server"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text='USD'></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label66" runat="server" Text='<%$ Resources:labels, transfee %>'></asp:Label>
                        &nbsp;
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbtransferfee" runat="server"></asp:Label>
                        <asp:Label ID="Label41" runat="server" Text='USD'></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <asp:Label ID="Label70" runat="server" Text='<%$ Resources:labels, transtotal %>'></asp:Label>
                        &nbsp;
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbtransfertotal" runat="server"></asp:Label>
                        <asp:Label ID="Label11" runat="server">USD</asp:Label>
                    </div>
                </div>
            </div>
            <div class="button-group">
                <asp:Button ID="btnBackTRF" runat="server" OnClick="btnBackTRF_Click" CssClass="btn btn-warning" Text='<%$ Resources:labels, quaylai %>' />
                <asp:Button ID="btnApply" runat="server" CssClass="btn btn-primary" OnClick="btnApply_Click" Text='<%$ Resources:labels, xacnhan %>' />
            </div>
        </asp:Panel>

        <asp:Panel ID="pnOTP" runat="server" CssClass="divcontent" Visible="false">
            <div class="handle">
                <span><%=Resources.labels.xacthucgiaodich %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <label class="bold"><%=Resources.labels.loaixacthuc %></label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:DropDownList ID="ddlLoaiXacThuc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLoaiXacThuc_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-5 col-sm-4 left">
                        <asp:Panel ID="pnSendOTP" runat="server">
                            <asp:Button ID="btnSendOTP" runat="server" CssClass="btn btn-primary btnSendOTP " OnClick="btnSendOTP_Click" Text="<%$ Resources:labels, resend %>" />
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
                <asp:Button ID="btnBackConfirm" runat="server" CssClass="btn btn-warning" OnClick="btnBackConfirm_Click" Text="<%$ Resources:labels, quaylai %>" />
                <asp:Button ID="btnAction" runat="server" CssClass="btn btn-primary" OnClick="btnAction_Click" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" Text="<%$ Resources:labels, thuchien %>" />
            </div>
        </asp:Panel>

        <asp:Panel ID="pnResult" runat="server" class="divcontent" Visible="false">
            <div class="handle">
                <span><%=Resources.labels.thongtinnguoitratien %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.firstname %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndSenderName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.lastname %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndSenderLastName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.debitaccount %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndSenderAccount" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.availablebalance %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndBalanceSender" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblBalanceCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.payoutcountry %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendpaycountry" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.temporaryunit %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendunit" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.temporarydistrict %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbenddistrict" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.thanhpho %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendCity" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.province %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendprovince" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.postcode %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblendpostcode" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.country %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendcountry" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.countrycode %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblendcountrycode" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.telephonenum %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendtelephone" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.mbcountrycode %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblendmobilephonecode" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.mobilephone %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendmobilephone" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.email %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendemail" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="handle">
                <span>ID information</span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.idtype %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendidtype" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.idnumber %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendidnumber" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.countryofissue %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="Label45" runat="server">Laos</asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.expdate %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendexdate" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.issdate %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendissuedate" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.DOB %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbenddob" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.officephone %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblendoffcice" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.occupation %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendocc" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.purpose %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendpurpose" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.sourceoffund %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendsource" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.permanentunit %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblendicunit" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.permanentdistrict %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblendicdistrict" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.permanentcity %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblendiccity" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.permanentcountry %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblendiccountry" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.countryofbirth %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbliccountryofbirth" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.nationality %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendnation" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.gender %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendgender" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.comment %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblendiccomment" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span>File Upload</span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendfileupload" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="handle">
                <span><%=Resources.labels.thongtinnguoinhantien %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.firstname %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndReceiverFirstName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.lastname %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndReceiverLastName" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <span><%=Resources.labels.noidungchuyenkhoan %></span>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.sogiaodich %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndTransactionNo" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.thoidiem %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndDateTime" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.sotien %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblEndAmountCCYID" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.nguoitraphi %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndPhi" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <span><%=Resources.labels.sotienphi %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lblEndPhiAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblEndFeeCCYID" runat="server">USD</asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-6 right">
                        <span><%=Resources.labels.tongtien %></span>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbendtotal" runat="server"></asp:Label>
                        <asp:Label ID="Label38" runat="server">USD</asp:Label>
                    </div>
                </div>
            </div>
            <div class="button-group">
                <asp:Button ID="btnView" runat="server" OnClientClick="javascript:return poponloadview();" Text="<%$ Resources:labels, viewphieuin %>" CssClass="btn btn-primary" />
                <asp:Button ID="btnPrint" runat="server" OnClientClick="javascript:return poponload();" Text="<%$ Resources:labels, inketqua %>" CssClass="btn btn-warning" />
                <asp:Button ID="btnNew" runat="server" Text='<%$ Resources:labels, lammoi %>' OnClick="btnNew_Click" CssClass="btn btn-success" />
            </div>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnRequest" />
        <asp:PostBackTrigger ControlID="btncontinute" />
    </Triggers>
</asp:UpdatePanel>

<script type="text/javascript">

    Onload();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        Onload();
    }

    function Onload() {
        $("[data-mask]").inputmask();
        $(".timepicker").timepicker({
            showInputs: false,
            minuteStep: 1,
            showMeridian: false,
            showSeconds: true
        });
        var date = new Date();
        date.setDate(date.getDate() + 1)
        $('.dateselect').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy',
            language: 'en',
            todayBtn: "linked",
            startDate: date
        });
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
    }

    function isNumberKeyNumer(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    };


    function validateName(x) {
        if (/^[A-Za-z0-9,/_()-.: ]+$/.test(x))
            return true;
        else
            return false;
    }

    function validate1() {
        if (!validateEmpty('<%=txtProvince.ClientID %>', 'Please input Temporary address - State/Province')) {
            return false;
        }
        if (!validateEmpty('<%=txtSenderUnit.ClientID %>', 'Please input Temporary address - Houseno/unit !')) {
            return false;
        }
        if (!validateEmpty('<%=txtSenderCity.ClientID %>', 'Please input City!')) {
            return false;
        }
        if (!validateEmpty('<%=txtsenderlastname.ClientID %>', 'Please input sender last name !')) {
            return false;
        }
        if (!validateEmpty('<%=txtsenderfirstname.ClientID %>', 'Please input sender first name !')) {
            return false;
        }
    }



    function validate() {
        if (!validateEmpty('<%=txtReceiverfirstname.ClientID %>', 'Please spell Receiver First name correctly')) {
            return false;
        }
        if (!validateEmpty('<%=txtReceiverlastname.ClientID %>', 'Please spell Receiver Last name correctly')) {
            return false;
        }
        if (!validateEmpty('<%=txtIDProvince.ClientID %>', 'Please input Permanent address-City/Province')) {
            return false;
        }
        if (!validateEmpty('<%=txtIDunit.ClientID %>', 'Please input Permanent address-Hoseno/Unit!')) {
            return false;
        }
        if (!validateEmpty('<%=txtIDnumber.ClientID %>', 'Please input ID number !')) {
            return false;
        }
        if (!validateEmpty('<%=txtexpitydate.ClientID %>', 'Please input Expiry date !')) {
            return false;
        }
        if (!validateEmpty('<%=txtissuedate.ClientID %>', 'Please input Issue date !')) {
            return false;
        }
        if (!validateEmpty('<%=txtdateofbirth.ClientID %>', 'Please input Date of birth !')) {
            return false;
        }
    }

    function poponload() {
        testwindow = window.open("widgets/IBWesternUnion/print.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }

    function poponloadview() {
        testwindow = window.open("widgets/IBWesternUnion/viewprint.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }

</script>
