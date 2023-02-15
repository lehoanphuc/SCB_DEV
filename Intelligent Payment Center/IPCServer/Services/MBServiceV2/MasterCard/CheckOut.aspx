<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckOut.aspx.cs" Inherits="MBService.MasterCard.CheckOut" %>

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
        <p style="text-align: center;">
            <a href="#">
                <img src="data:image/png;base64, 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" />
            </a>
        </p>
        <asp:HiddenField ID="hdAmount" runat="server" />
        <asp:HiddenField ID="hdCcyid" runat="server" />
        <asp:HiddenField ID="hdTransID" runat="server" />
        <asp:HiddenField ID="hdsession" runat="server" />
        <asp:HiddenField ID="hduserid" runat="server" />
        <asp:HiddenField ID="hdmerchant" runat="server" />
        <asp:HiddenField ID="hdTranCode" runat="server" />
        <asp:HiddenField ID="hdSourceID" runat="server" />

        <h3 align="center"><u>Order Summary</u></h3>
        <p style="text-align: center;">
            <asp:Label ID="lbcontentamount" runat="server" Visible="false">Order Amount :</asp:Label>
            <asp:Label ID="lbAmount" runat="server"></asp:Label>
        </p>
        <p style="text-align: center;"><asp:Label ID="lbcontentccyid" runat="server" Visible="false">Currency :</asp:Label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:<asp:Label ID="lbccyid" runat="server"></asp:Label></p>

        <p style="text-align: center;">
            <p style="text-align: center;">
                <input class="btn" id="linkid" type="button" value="Pay with Lightbox" onclick="Checkout.showLightbox();" style="display: none;" />
            </p>
            <p style="text-align: center;">
                <p style="text-align: center;">
                    <input class="btn" type="button" value="Pay with Payment Page" onclick="Checkout.showPaymentPage();" style="display: none;" />
                </p>

                <p style="text-align: center;">
                    <br />
                    <br />
                    <input type="button" class="btn" value="Redirect to PSVB Hi app" onclick="invokeCSCode()" />
                </p>
    </form>

    <script src="<%=System.Configuration.ConfigurationManager.AppSettings["LinkCashin"]%>" data-error="errorCallback" data-cancel="cancelCallback" data-complete="complete">
    </script>
    <script type="text/javascript">

        function complete() {
            window.location = "Result.aspx?TRANSID=" + document.getElementById('hdTransID').value + "&?USERID=" + document.getElementById('hduserid').value + "&?SESSIONID=" + document.getElementById('hdsession').value + "&?TRANCODE=" + document.getElementById('hdTranCode').value + "&?SOURCEID=" + document.getElementById('hdSourceID').value;
        }

        function errorCallback(error) {
            alert(JSON.stringify(error));
        }

        function completeCallback(resultIndicator, sessionVersion) {
            alert("Result Indicator");
            alert(JSON.stringify(resultIndicator));
            alert("Session Version:");
            alert(JSON.stringify(sessionVersion));
            alert("Successful Payment");
        }

        function cancelCallback() {
            alert('Payment cancelled');
        }

        Checkout.configure({
            merchant: document.getElementById('hdmerchant').value,
            order: {
                amount: document.getElementById('hdAmount').value,
                currency: document.getElementById('hdCcyid').value,
                description: 'Phongsavanh Bank',
                id: document.getElementById('hdTransID').value,
                item: {
                    brand: 'Mastercard',
                    description: 'Phongsavanh Bank',
                    name: 'HostedCheckoutItem',
                    quantity: '1',
                    unitPrice: '1.00',
                    unitTaxAmount: '1.00'
                }
            },
            billing: {
                address: {
                    street: 'Kaysone Phomvihan Road, Phakao Village, Xaythany District,Vientiane Capital',
                    stateProvince: 'Vientiane',
                    city: 'Vientiane',
                    company: 'Phongsavanh Bank',
                    postcodeZip: '01170',
                    country: 'LAO'
                }
            },
            customer: {
                email: ''
            },
            interaction: {
                merchant: {
                    name: 'Phongsavanh Bank',
                    address: {
                        line1: 'Kaysone Phomvihan Road, Phakao Village, Xaythany District,Vientiane Capital',
                    },
                    logo: 'https://www.phongsavanhbank.com/psv/allpages/images/psvblogo.png'
                },
                displayControl: {
                    billingAddress: 'HIDE',
                    customerEmail: 'HIDE',
                    shipping: 'HIDE'
                }
            },
            session: {
                id: document.getElementById('hdsession').value
            }
        });

        function invokeCSCode() {
            var data = { event: 'CLICK', data: '' };
            try {
                invokeCSharpAction(JSON.stringify(data));
            }
            catch (err) {
                invokeCSharpActionAnd(JSON.stringify(data));
            }
        }

        jQuery(function () {
            jQuery('#linkid').click();
        });
    </script>
</body>
