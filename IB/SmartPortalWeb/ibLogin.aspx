<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ibLogin.aspx.cs" Inherits="ibLogin" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title><%=Resources.labels.iblogintitle %></title>
    <meta name="viewport" content="width=device-width, maximum-scale=1.0, initial-scale=1, user-scalable=0">
    <link rel="icon" href="/icon.png" type="image/png">
    <link href="CSS/font-awesome-4.6.3/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="shortcut icon" href="~/favicon.ico" type="image/x-icon" />
    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <script src="JS/sha256.js"></script>

    <style>
        .box-login .tite {
            text-align: left;
            font-size: 34px;
            color: #ffffff;
        }

        .pic{
            float: left;
            display: block; 
            align-content:center;
            width: 150px;
            height: auto;
        }
		
		.login-pic {
			width: 316px;
			height: auto;
			margin-right: 400px;
            position:relative;
		}
    </style>
</head>
<body class="ibLogin">
    <nav class="navbar">
        <!-- Left navbar links -->
        <%--<ul class="navbar-nav">
            <li class="nav-item">
                <i class="fa fa-map-marker"><a style="ma"><%=Resources.labels.locatenearestbranch %></a></i> 
                <a class="nav-link" href="#"></a>
            </li>
        </ul>--%>
        <!-- Right navbar links -->
        <ul class="navbar-nav nav-right">
            <li class="nav-item">
                <a class="nav-link" href="ibLogin.aspx?l=en-US">
                    <img src="App_Themes/InternetBanking/images/flag-english.png" alt="us_flag"> <span class="language">English</span>
                </a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="ibLogin.aspx?l=en-US">
                    <img src="App_Themes/InternetBanking/images/flag-thailand.png" alt="tl_flag"> <span class="language">ThaiLand</span>
                </a>
            </li>
        </ul>
    </nav>
    <main>
        <div class="content">
            <div class="row">
                <div class="col-md-6 left hidden-xs hidden-sm">
                    <div class="text-center">
                        <img class="login-pic" src="Images/logo.png" />
                    </div>
            </div>
            <div class="col-md-6 right">
                <form class="wrap-login" runat="server"> 
                    <div class="box-login">
                            <div class="form-input">
                                <p class="title"  style="text-align:center"><%=Resources.labels.webcomebphi %></p>
                                <div class="row">
                                    <img class="login-pic" style="display: block; margin-left: auto; margin-right: auto; width: 80%;" src="Images/logo.png" />                                  </div>
                                <div class="title-login">
                                    <p class="title-login"><%=Resources.labels.pleaseenteryourusernamelg %> & <%=Resources.labels.passwordtologin %></p>
                                </div>
                                <div class="wranning">
                                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblInfoDetail" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtUser" runat="server" class="form-control" TabIndex="1" autocomplete="off" Placeholder="<%$ Resources:labels, tendangnhap %>"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtPass" runat="server" class="form-control" TabIndex="2" TextMode="Password" autocomplete="off"
                                        AutoCompleteType="Disabled" Placeholder="<%$ Resources:labels, matkhau %>"></asp:TextBox>
                                </div>
                                <div class="form-validate">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Captcha/imgsecuritycode.aspx" CssClass="imgValidate" />
                                    <div class="divtext">
                                        <asp:TextBox ID="txtValidateCode" runat="server" class="form-control" TabIndex="3" autocomplete="off" Placeholder="<%$ Resources:labels, kytubaomat %>"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="button-login">
                                    <asp:Button CssClass="smbutton" ID="btnLogin2" OnClientClick="clientValidate()" runat="server" Text="<%$ Resources:labels, dangnhapthuong %>" OnClick="btnLogin_Click"
                                        TabIndex="3" />
                                </div>
                            </div>
                        </div>
                </form>
            </div>
        </div>
    </div>
    </main>
    
    <footer class="main-footer">
        <ul class="ul-footer">
            <li style="margin-right:80%">
                <a class="footer-link" href="#"><%=Resources.labels.ibankingguide1 %></a>
            </li>
        </ul>
    </footer>
</body>
</html>
    
