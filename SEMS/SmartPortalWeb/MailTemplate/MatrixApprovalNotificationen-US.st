<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Untitled Page</title>
</head>
<body>

    <table style="width: 100%;">
        <tr>
            <td colspan="2"
                style="font-weight: bold;">
                <img src = '../../../Images/logo.png' style='height: 70px;' /><br />
                <br />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2"
                style="font-weight: bold; font-size: 14pt;">APPROVAL NOTIFICATION<br />
                <br />
            </td>
        </tr>
        <tr>
			<td colspan="2"
                style="font-weight: bold; font-size: 12pt;">$message$<hr />
            </td>
			<br />
        </tr>
        <tr>
            <td>Transaction name</td>
            <td>$tranName$</td>
        </tr>
        <tr>
            <td>Transaction No</td>
            <td>$tranID$</td>
        </tr>
        <tr>
            <td>Date time</td>
            <td>$tranDate$</td>
        </tr>
        <tr>
            <td style="height: 5px;"></td>
        </tr>
        <tr>
            <td colspan="2"
                style="font-weight: bold; font-size: 10pt;">PAYMENT INFORMATION<hr />
            </td>
        </tr>
        <tr>
            <td>Debit account</td>
            <td>$senderAccount$</td>
        </tr>
        <tr>
            <td>Amount</td>
            <td>$amount$ $ccyid$</td>
        </tr>
        <tr>
            <td>Amount in words</td>
            <td>$amountchu$</td>
        </tr>
        <tr>
            <td>Payment content</td>
            <td>$desc$</td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
                <hr />
                <!--<span style='font-size: 8pt;'>Respond from the bank<br />
                    Transaction no: $tranID$, status: Successful, at: $tranDate$</span>-->
                <p><a href=' ' target='blank'></a></p>
                <span style='font-weight: bold;'>Thank you for using Siam Commercial Bank!</span>
            </td>
        </tr>
    </table>

</body>
</html>
