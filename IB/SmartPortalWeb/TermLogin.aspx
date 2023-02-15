<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TermLogin.aspx.cs" Inherits="ibLoginTerm" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <style>
        table, th, td {
            border: 1px solid black;
            border-collapse: collapse;
        }

        body {
            padding-top: 5px;
            padding-bottom: 50px;
            padding-right: 50px;
            padding-left: 50px;
            max-width: 100% !important;
        }
        a{
        color: blue !important;

        }
    </style>
    <title><%=Resources.labels.iblogintitle %></title>
    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <script src="JS/sha256.js"></script>
</head>
<body>
    <div>
        <asp:Literal ID="ltrPrint" runat="server"></asp:Literal>
    </div>
    <main style="background: none">
        <div class="content">
            <div class="row">
                <div class="col-md-6 right">
                    <form class="wrap-login1" runat="server">
                        <div class="box-login1">
                            <div class="form-input">
                                <div class="button-login1">
                                    <asp:Button CssClass="smbutton1 " type="button" ID="btnLogin1" runat="server" Text="I Agree" OnClick="btnLogin_Click"
                                        TabIndex="3" />
                                      <asp:Button CssClass="smbutton1 " type="button" ID="Button1" runat="server" Text="Back" OnClick="btnBack_Click"
                                        TabIndex="3" />
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </main>
</body>
</html>
<style>
    .smbutton1 {
        padding: 5px 20px;
        background: #28a745;
        border: 1px solid #238339;
        border-radius: 10px;
        color: #fff;
    }
</style>

<script type="text/javascript">
    function poponload1() {
        testwindow = window.open("TermLogin.aspx", "Term",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
</script>
