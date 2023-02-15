<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="ibRegister" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title><%=Resources.labels.iblogintitle %></title>
    <meta http-equiv="Content-Security-Policy" content="upgrade-insecure-requests">
    <meta name="viewport" content="width=device-width, maximum-scale=1.0, initial-scale=1, user-scalable=0">
    <link rel="icon" href="/icon.png" type="image/png">
    <link href="CSS/font-awesome-4.6.3/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="shortcut icon" href="~/favicon.ico" type="image/x-icon" />
    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="css/register.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/InternetBanking/Portal.css" rel="stylesheet" type="text/css" />
    <script src="JS/sha256.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="JS/common.js"> </script>
    <script type="text/javascript" src="js/Validate.js"> </script>
    <script src="JS/common.js"></script>
    <style>
        .box-login .tite {
            text-align: left;
            font-size: 34px;
            color: #ffffff;
        }

        .pic {
            float: left;
            display: block;
            align-content: center;
            width: 150px;
            height: auto;
        }

        .login-pic {
            width: 259px;
            height: 100%;
            margin-left: 50px;
            position: relative;
        }

        .icon-pic {
            height: 100%;
            width: 75%;
            position: relative;
        }

        .register {
            text-align: center;
            font-style: initial;
        }

        .handle {
            color: #fbaf1e;
        }

        .form-control {
            font-family: 'Roboto', Arial, sans-serif;
            font-size: inherit;
            line-height: inherit;
        }

        th{
            text-align:center;
            background-color: #0a8045;
            color: #fff;
        }
    </style>
</head>
<body class="ibRegister" style="background-color: #dadada;">
    <div class="banner">
        <div class="container">
            <div class="nav-logo">
                <img class="login-pic" src="Widgets/IBHeader/Images/img_individual.png" />
            </div>
        </div>
    </div>
    <main>
        <div class="content">
            <div class="row">
                <div class="col-md-12">
                    <form class="wrap-login" runat="server">
                        <div class="box-login">
                            <div class="form-input">
                                <p class="title" style="text-align: center; color: #7A58BF">Register your Hi Banking Service</p>
                                <div class="wranning" style="color: red; text-align: center">
                                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                                </div>
                                <asp:Panel ID="pn1" runat="server">
                                    <figure>
                                        <div class="handle">
                                            <asp:Label runat="server" Text='Personal information'></asp:Label>
                                        </div>
                                        <div class="content_table">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label1" runat="server" Text="Full Name"></asp:Label>&nbsp;*
                                                    <asp:TextBox ID="txtFullname" runat="server" onkeyup="this.value=this.value.replace(/[^a-zA-Z ]/g, '')" class="form-control" TabIndex="1" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label3" runat="server" Text="Date of birth"></asp:Label>&nbsp;*
                                            <asp:TextBox ID="txtDOB" runat="server" class="form-control" type="date" Height="30px"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label12" runat="server" Text="Gender"></asp:Label>&nbsp;*
                                            <asp:DropDownList ID="ddlGender" CssClass="form-control" runat="server">
                                                <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                                <asp:ListItem Text="FeMale" Value="F"></asp:ListItem>
                                            </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label13" runat="server" Text="Nation"></asp:Label>&nbsp;*
                                                    <asp:DropDownList ID="ddlNation" CssClass="form-control" runat="server">
                                                        <asp:ListItem Text="Laos" Value="LA"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </figure>
                                    <figure>
                                        <div class="handle">
                                            <asp:Label runat="server" Text='Paper information'></asp:Label>
                                        </div>
                                        <div class="content_table">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label15" runat="server" Text="Type of Identify Document"></asp:Label>&nbsp;*
                                                    <asp:DropDownList ID="ddlTypeID" CssClass="form-control" runat="server">
                                                        <asp:ListItem Text="National ID Card" Value="NRIC"></asp:ListItem>
                                                        <asp:ListItem Text="Passport" Value="PASSPORT"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label4" runat="server" Text="ID No"></asp:Label>&nbsp;*
                                                    <asp:TextBox ID="txtID" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label2" runat="server" Text="Issue date"></asp:Label>&nbsp;*
                                                    <asp:TextBox ID="txtIssueDate" runat="server" class="form-control" type="date" Height="30px"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label14" runat="server" Text="Expiry date"></asp:Label>&nbsp;*
                                                    <asp:TextBox ID="txtExpiryDate" runat="server" class="form-control" type="date" Height="30px"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label9" runat="server" Text="Paper front"></asp:Label>&nbsp;*
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label10" runat="server" Text="Paper Back"></asp:Label>&nbsp;*
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label11" runat="server" Text="Selfie with Paper"></asp:Label>&nbsp;*
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row" style="padding-bottom: 15px">
                                                    <div class="col-sm-4">
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
                                                    <div class="col-sm-4">
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
                                                                                <label id="Label25" runat="server"></label>
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
                                                    <div class="col-sm-4">
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
                                                                                <label id="Label26" runat="server"></label>
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
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-12" style="text-align: center">
                                                            <asp:Button ID="btnUpLoadFile" runat="server" Text="Upload" CssClass="btn btn-primary" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </figure>
                                    <figure>
                                        <div class="handle">
                                            <asp:Label runat="server" Text='Address'></asp:Label>
                                        </div>
                                        <div class="content_table">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label5" runat="server" Text="House No"></asp:Label>&nbsp;
                                                    <asp:TextBox ID="txtHouseNo" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label16" runat="server" Text="Unit"></asp:Label>&nbsp;
                                                    <asp:TextBox ID="txtUnit" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label17" runat="server" Text="Village"></asp:Label>&nbsp;
                                                    <asp:TextBox ID="txtVillage" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label6" runat="server" Text="District"></asp:Label>&nbsp;*
                                                        <asp:TextBox ID="txtDistrict" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label18" runat="server" Text="Province"></asp:Label>&nbsp;*
                                                         <asp:DropDownList ID="ddlprovince" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlprovince_SelectedIndexChanged">
                                                         </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </figure>
                                    <figure>
                                        <div class="handle">
                                            <asp:Label runat="server" Text='Contact information'></asp:Label>
                                        </div>
                                        <div class="content_table">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label7" runat="server" Text="Mobile"></asp:Label>&nbsp;*
                                                    <asp:TextBox ID="txtphonenumber" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtphonenumber_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label8" runat="server" Text="E-Mail"></asp:Label>&nbsp;*
                                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </figure>
                                    <figure>
                                        <div class="handle">
                                            <asp:Label runat="server" Text='Account detail'></asp:Label>
                                        </div>
                                        <div class="content_table">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label19" runat="server" Text="Account name"></asp:Label>&nbsp;
                                                        <asp:TextBox ID="txtAccname" runat="server" class="form-control" onkeyup="this.value=this.value.replace(/[^a-zA-Z ]/g, '')" MaxLength="16"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-5">
                                                        <asp:Label ID="Label20" runat="server" Text="Account number"></asp:Label>&nbsp;
                                                        <asp:TextBox ID="txtaccnumber" runat="server" type="money" class="form-control" MaxLength="16" onkeyup="this.value=this.value.replace(/[^0-9-]/g, '')"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:Label ID="lblcc" runat="server" Text="Currency"></asp:Label>&nbsp;
                                                        <asp:DropDownList ID="ddlcciyd" CssClass="form-control" runat="server">
                                                            <asp:ListItem Value="LAK" Text="LAK"></asp:ListItem>
                                                            <asp:ListItem Value="USD" Text="USD"></asp:ListItem>
                                                            <asp:ListItem Value="THB" Text="THB"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2" style="text-align: center; margin-top: 10px">
                                                        <br />
                                                        <asp:Button CssClass="btn btn-primary" ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" />
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="form-group">
                                                <asp:Panel ID="pnGV" runat="server" Visible="False">
                                                    <div id="divResult">
                                                        <asp:GridView ID="gvACC" CssClass="table table-hover" runat="server" BackColor="White"
                                                            BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                                            Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                                                            OnRowDataBound="gvACC_RowDataBound" PageSize="15"
                                                            OnPageIndexChanging="gvACC_PageIndexChanging"
                                                            OnRowDeleting="gvACC_RowDeleting">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Account number">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblaccno" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Currency">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblccyid" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton BackColor="#c0391e" ID="lbDelete" runat="server" CssClass="btn btn-secondary" CommandName='DELETE'>Delete</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                                                        </asp:GridView>
                                                        <asp:Literal ID="litPager" runat="server"></asp:Literal>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </figure>
                                    <figure>
                                        <div class="handle">
                                            <asp:Label runat="server" Text='Login information'></asp:Label>
                                        </div>
                                        <div class="content_table">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label23" runat="server" Text="Preferred Login Name :"></asp:Label>&nbsp;
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtloginname" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label24" runat="server" Text="Phone number to receiver OTP:"></asp:Label>&nbsp;
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TextBox27" runat="server" class="form-control" Text="+ (856) 20" Enabled="false" Height="34px"></asp:TextBox>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtphoneOTP" runat="server" class="form-control" AutoPostBack="true" MaxLength="8" OnTextChanged="txtphoneOTP_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </figure>
                                    <figure>
                                        <div class="form-validate">
                                            <div class="col-md-4">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Captcha/imgsecuritycode.aspx" CssClass="imgValidate" />
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtValidateCode" runat="server" class="form-control" TabIndex="3" autocomplete="off" Placeholder="<%$ Resources:labels, kytubaomat %>"></asp:TextBox>
                                            </div>
                                        </div>
                                    </figure>
                                    <div class="button-login">
                                        <asp:Button CssClass="smbutton" ID="btnsubmit" runat="server" Text="Register" OnClick="btnsubmit_Click" TabIndex="3" />
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
                                                    <asp:ListItem Value="ESMSOTP" Text="Authen by OTP SMS"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-xs-5 col-sm-4 left">
                                                <asp:Button ID="btnSendOTP" runat="server" CssClass="btn btn-primary btnSendOTP" OnClick="btnSendOTP_Click" Text="<%$ Resources:labels, resend %>" />
                                                <asp:Panel ID="pnSendOTP" runat="server">
                                                    <div class="countdown">
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
                                    </div>
                                    <div class="button-group">
                                        <asp:Button ID="btnBackConfirm" runat="server" CssClass="btn btn-warning" OnClick="btnBackConfirm_Click" Text="<%$ Resources:labels, quaylai %>" />
                                        <asp:Button ID="btnAction" runat="server" CssClass="btn btn-primary" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="btnAction_Click" Text="<%$ Resources:labels, thuchien %>" />
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnresult" runat="server" CssClass="divcontent" Visible="false">
                                    <div class="row" style="text-align: center; font-size: 28px; font-weight: bold; color: #0a8045">
                                        <label id="lblresult" runat="server">Registration Successful !</label>
                                    </div>
                                    <div class="handle">
                                        <span>Result register</span>
                                    </div>
                                    <div class="content_table">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:Label ID="Label21" runat="server" Text="Contract No :"></asp:Label>&nbsp;
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtContract" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:Label ID="Label22" runat="server" Text="Phone no :"></asp:Label>&nbsp;
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtPhoneno" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <br />
                                <div class="form-group register">
                                    <a runat='server' style="color: #7A58BF" href='/ibLogin.aspx'>Login by another account</a>
                                </div>
                            </div>
                        </div>
                        <br />
                        <asp:Panel ID="pninfor" runat="server" Visible="false">
                            <div class="row" style="text-align: center; font-size: 15px; font-weight: bold; color: #0a8045">
                                <asp:Label ID="lblpush" runat="server">Enjoy using Hi App! You will have (Individual wallet only in config) per day transfer limit.</asp:Label>
                            </div>
                            <div class="row" style="text-align: center; font-size: 15px; font-weight: bold; color: #0a8045">
                                <asp:Label ID="Label27" runat="server">If you want to increase this limit, you can link your Siam Commercial Bank account by visiting any of our branches.</asp:Label>
                            </div>
                            <br />
                            <br />
                            <div class="row" style="text-align: center; margin-left: 32%">
                                <div class="col-md-2">
                                    <a href="play.google.com/store/apps/details?id=la.com.psvbhi.hiapp"><img class="icon-pic" src="Images/icongoogle.png"/></a>
                                </div>
                                <div class="col-md-2">
                                    <a href="apps.apple.com/vn/app/psvb-hi/id1596039134?l=en"><img class="icon-pic" src="Images/icon_appstore.png" /></a>
                                </div>
                                <div class="col-md-2">
                                    <a href="#"><img class="icon-pic" src="Images/icon_appgallery.png" style="width: 84%; height: 100%" /></a>
                                </div>
                            </div>
                            <%--<br />
                            <br />
                            <div class="row" style="text-align: center">
                                <div id="devtranlimit" style="width:50%; margin:auto">
                                    <asp:GridView ID="gvtran" CssClass="table table-hover" runat="server" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                        Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                                        OnRowDataBound="gvtran_RowDataBound" PageSize="15"
                                        OnPageIndexChanging="gvtran_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Transaction Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbtrantype" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Limit per day (LAK)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblimitday" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Limit per transfer (LAK)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblimittran" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField >
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                                    </asp:GridView>
                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                </div>
                            </div>--%>
                        </asp:Panel>
                    </form>
                </div>
            </div>
        </div>
    </main>
    <div class="reg-footer">
    </div>
</body>
</html>


