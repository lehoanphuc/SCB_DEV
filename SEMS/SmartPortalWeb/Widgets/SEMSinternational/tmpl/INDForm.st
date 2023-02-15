<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
  <head>
    <title>Untitled Page</title>
  </head>
  <body>

    <table style="width:100%;">
      <tr>
        <td colspan="2"
                  style="font-weight: bold;">
          <img src = '../../../Images/logo.png' style='height:70px;' />
          <br/>
          <br/>
        </td>
      </tr>
      <tr>
        <td align="center" colspan="2"
                  style="font-weight: bold; font-size:14pt;">
          INTERNATIONAL TRANSFER<br/><br/>
        </td>
      </tr>
      <tr>
      <tr>
			<td>Reference Payment</td>
			<td>$MTCNCODE$</td>
      </tr>
        <td>Date time</td>
        <td>$tranDate$</td>
      </tr>
      <tr>
        <td>Transaction No</td>
        <td>$tranID$</td>
      </tr>
      <tr>
        <td style="height:5px;">

        </td>
      </tr>
      <tr>
        <td colspan="2"
            style="font-weight: bold; font-size:10pt;">
          SENDER INFORMATION<hr />
        </td>
      </tr>
      <tr>
        <td>Sender name</td>
        <td>$senderName$</td>
      </tr>
      <tr>
        <td>ID Type</td>
        <td>$idType$</td>
      </tr>
      <tr>
        <td>ID Number</td>
        <td>$idNumber$</td>
      </tr>
      <tr>
        <td>Country Of Issue</td>
        <td>$senderCountry$</td>
      </tr>
      <tr>
        <td>Expired Date</td>
        <td>$expiredDate$</td>
      </tr>
      <tr>
        <td>Issue Date</td>
        <td>$issueDate$</td>
      </tr>
      <tr>
        <td>Address</td>
        <td>$senderAddress$</td>
      </tr>
      <tr>
        <td>Phone Number</td>
        <td>$senderPhone$</td>
      </tr>
      <tr>
        <td style="height:5px;">

        </td>
      </tr>
      <tr>
        <td colspan="2"
            style="font-weight: bold; font-size:10pt;">
          BENEFICIARY INFORMATION<hr />
        </td>
      </tr>
      <tr>
        <td>Bank Name</td>
        <td>$bankName$</td>
      </tr>
      <tr>
        <td>Swift Code</td>
        <td>$swiftCode$</td>
      </tr>
      <tr>
        <td>Beneficiary Name</td>
        <td>$benName$</td>
      </tr>
      <tr>
        <td>Beneficiary Account Number</td>
        <td>$benAccount$</td>
      </tr>
      <tr>
        <td>Address</td>
        <td>$benAddress$</td>
      </tr>
      <tr>
        <td>Phone Number</td>
        <td>$benPhone$</td>
      </tr>
      <tr>
        <td>Email</td>
        <td>$benEmail$</td>
      </tr>
      <tr>
        <td style="height:5px;">

        </td>
      </tr>
      <tr>
        <td colspan="2"
            style="font-weight: bold; font-size:10pt;">
          TRANSFER CONTENT<hr />
        </td>
      </tr>
      <tr>
        <td>Debit Account</td>
        <td>$senderAccount$</td>
      </tr>

      <tr>
        <td>Amount</td>
        <td>$amount$ $ccyid$</td>
      </tr>
      <tr>
        <td>Fee Type</td>
        <td>$feeType$</td>
      </tr>
      <tr>
        <td>Fee</td>
        <td>$fee$ $ccyid$</td>
      </tr>
      <tr>
        <td>Total amount</td>
        <td>$totalAmount$ $ccyid$</td>
      </tr>
      <tr>
        <td>Purpose Of Remittance</td>
        <td>$purpose$</td>
      </tr>
      <tr>
        <td>Detail Of Remittance</td>
        <td>$purposeDetail$</td>
      </tr>

      <tr>
        <td>Link to Tracking</td>
        <td><a href='$linktracking$' target='blank'>$linktracking$</a></td>
      </tr>
      <tr>
        <td colspan="2">
          <br/>
          <hr/>
          <span style='font-size:8pt;'>
            Respond from the bank<br/>
            Transaction no: $tranID$, status: $status$, at: $tranDate$
          </span>
          <p>
            <a href='https://www.scb.co.th/en/personal-banking.html' target='blank'></a>
          </p>
          <span style='font-weight:bold;'>
            Thank you for using ebanking services of Siam Commercial Bank!
          </span>
        </td>
      </tr>
    </table>

  </body>
</html>
