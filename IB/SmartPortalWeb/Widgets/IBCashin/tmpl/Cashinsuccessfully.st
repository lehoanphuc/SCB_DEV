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
             <img src='https://ib.hellomoney.com.la:9004/Images/logo.png' style='height:70px;' /><br/>             
             <br/></td>
		</tr>
		<tr>
			<td align="center" colspan="2" 
                style="font-weight: bold; font-size:14pt;">
                ORDER FOR PAYMENT<br/><br/></td>
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
                CARD INFORMATION<hr /></td>
        </tr>
        <tr>
			<td>Name on card</td>
			<td>$senderName$</td>
        </tr>
        <tr>
			<td>Card number</td>
			<td>$senderAccount$</td>
        </tr>        

        <tr>
			<td style="height:5px;">

			</td>
        </tr>
        <tr>
            <td colspan="2" 
                style="font-weight: bold; font-size:10pt;">
                RECEIVER INFORMATION<hr /></td>
        </tr>
        <tr>
			<td>Receiver name</td>
			<td>$ReceiverName$</td>
        </tr>
        <tr>
			<td>Receiver account</td>
			<td>$receiverAccount$</td>
        </tr>
        <tr>
			<td style="height:5px;">
			
			</td>
        </tr>
        <tr>
            <td colspan="2" 
                style="font-weight: bold; font-size:10pt;">
                PAYMENT CONTENT<hr /></td>
        </tr>
        <tr>
			<td>Invoice Number</td>
			<td>$invoiceNumber$</td>
        </tr>
        <tr>
			<td>Transfer amount</td>
			<td>$amount$ $ccyid$</td>
        </tr>
        <tr>
			<td>Fee amount</td>
			<td>$feeAmount$ $ccyid$</td>
        </tr>
        <tr>
			<td>Cash In Amount</td>
			<td>$totalAmount$ $ccyid$</td>
        </tr>
		
       <tr>
			<td colspan="2">
				<br/>
				<hr/>
				<span style='font-size:8pt;'>
				Respond from the bank<br/>
				Transaction no: $tranID$, status: $status$, at: $tranDate$</span>
				<p><a href='https://www.scb.co.th/en/personal-banking.html' target='blank'>www.scb.co.th/en/personal-banking</a></p>
				<span style='font-weight:bold;'>
				Thank you for using Siam Commercial Bank!
				</span>
			</td>
		</tr>
    </table>

</body>
</html>
