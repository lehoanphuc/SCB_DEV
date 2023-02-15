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
				<img src='https://upload.wikimedia.org/wikipedia/en/thumb/4/47/Siam_Commercial_Bank_%28logo%29.svg/345px-Siam_Commercial_Bank_%28logo%29.svg.png' style='height:70px;' /><br />
				<br />
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2"
				style="font-weight: bold; font-size:14pt;">
				ORDER FOR PAYMENT<br /><br />
			</td>
		</tr>
		<tr>
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
			<td>$country$</td>
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
			<td>Debit account</td>
			<td>$senderAccount$</td>
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
			<td>Beneficiary name</td>
			<td>$recieverName$</td>
		</tr>
		<tr>
			<td>Beneficiary account</td>
			<td>$recieverAccount$</td>
        </tr>
		<tr>
			<td>Beneficiary Bank</td>
			<td>$recieverBank$</td>
        </tr>
        <tr>
			<td>Beneficiary SwiftCode</td>
			<td>$swiftCode$</td>
        </tr>
		<tr>
			<td>Beneficiary Phone</td>
			<td>$recieverPhone$</td>
        </tr>
        <tr>
			<td>Beneficiary Email</td>
			<td>$recieverEmail$</td>
        </tr>
        <tr>
			<td>Beneficiary Address</td>
			<td>$recieverAddress$</td>
        </tr>
		<tr>
			<td style="height:5px;">
			</td>
		</tr>
		<tr>
			<td colspan="2" style="font-weight: bold; font-size:10pt;">
				TRANSFER CONTENT<hr />
			</td>
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
			<td>Fee type</td>
			<td>$feeType$</td>
		</tr>
		<tr>
			<td>Fee amount</td>
			<td>$feeAmount$ $ccyid$</td>
		</tr>
		<tr>
			<td>Total Amount</td>
			<td>$totalAmount$ $ccyid$</td>
		</tr>
        <tr>
			<td>Purpose Of Remittance</td>
			<td>$purPose$</td>
		</tr>
        <tr>
			<td>Details Of Remittance</td>
			<td>$DetailpurPose$</td>
		</tr>
		<tr>
			<td>Payment content</td>
			<td>$desc$</td>
		</tr>
		<tr>
			<td colspan="2">
				<table style="width:100%;margin-bottom:30px;margin-top:15px;">
				</table>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<br />
				<hr /><span style='font-size:8pt;'>
					Respond from the bank<br />
					Transaction no: $tranID$, status: $status$, at: $tranDate$
				</span>
				<p><a href='https://www.scb.co.th/en/personal-banking.html' target='blank'>www.scb.co.th/en/personal-banking</a></p>
				<span style='font-weight:bold;'>
					Thank you for using Siam Commercial Bank!
				</span>
			</td>
		</tr>
	</table>

</body>
</html>
