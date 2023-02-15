<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="MBService.MasterCard.Result" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <script src="../JS/jquery-3.5.1.min.js"></script>
    <style type="text/css">
        .loader {
            border: 16px solid #f3f3f3; /* Light grey */
            border-top: 16px solid #056838; /* Blue */
            border-radius: 50%;
            width: 35px;
            height: 35px;
            animation: spin 2s linear infinite;
            margin: auto;
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        .wait {
            padding: 30px;
            font-size: 14px;
            color: #056838;
            font-weight: 600;
        }

        .hidden {
            display: none;
        }

        .btn {
            cursor: pointer;
            display: inline-block;
            font-weight: 400;
            text-align: center;
            vertical-align: middle;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-color: transparent;
            border: 1px solid transparent;
            padding: .375rem .75rem;
            font-size: 1rem;
            line-height: 1.5;
            border-radius: .25rem;
            transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out;
            color: #fff;
            background-color: #056838;
            border-color: #056838;
            box-shadow: none;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <asp:HiddenField ID="hdValue" runat="server" />
        <p style="text-align: center;">
            <a href="#">
                <img src="https://c.ap1.content.force.com/servlet/servlet.ImageServer?id=01590000008h62j&oid=00D90000000sUDO" />
            </a>
        </p>
        <tr>
            <td align="right" width="50%"><strong>
                <center>
                    <h4>
                        <asp:Label ID="lbresult" runat="server"></asp:Label></h4>
                </center>
            </strong></td>
        </tr>

        <table width="60%" align="center" cellpadding="5" border="0">
            <tr class="title">
                <td colspan="2" height="25">
                    <center>
                        <h4><strong>&nbsp;Order Details</strong></h4>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="2" height="25">
                    <center>
                        <strong>&nbsp;Merchant:
                            <asp:Label ID="lbmerchant" runat="server"></asp:Label>
                        </strong>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="2" height="25">
                    <center>
                        <strong>&nbsp;Order Amount:
                            <asp:Label ID="lbAmount" runat="server"></asp:Label>
                        </strong>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="2" height="25">
                    <center>
                        <strong>&nbsp;Order Curreny:
                            <asp:Label ID="lbccyid" runat="server"></asp:Label>
                        </strong>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="2" height="25">
                    <center>
                        <strong>&nbsp;Order ID:
                            <asp:Label ID="lbid" runat="server"></asp:Label>
                        </strong>
                    </center>
                </td>
            </tr>
        </table>
        <div style="text-align: center; margin: 20% auto;">
            <div class="loader"></div>
            <div class="wait">Redirecting to PSVB Hi app, please wait...</div>
            <button id="buttonclick" class="btn" type="button" onclick="invokeCSCode($('#hdValue').val())">Redirect to PSVB Hi app</button>
        </div>
        <p class="hidden" id="result">Result:</p>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#buttonclick').click();
                setInterval(showBtn(), 5000);
            })
            function showBtn() {
                $('#buttonclick').removeClass("hidden");
            }
            function log(str) {
                $('#result').text($('#result').text() + " " + str);
            }
            function invokeCSCode(value) {
                log("Sending Data:" + value);
                var data = { event: 'CLICK', data: value };
                try {
                    invokeCSharpAction(JSON.stringify(data));
                }
                catch (err) {
                    invokeCSharpActionAnd(JSON.stringify(data));
                    log(err);
                    //alert(err);
                }
            }
            function invokeCSharpActionAnd(data) { jsBridge.invokeAction(data); };

        </script>
    </form>
</body>
