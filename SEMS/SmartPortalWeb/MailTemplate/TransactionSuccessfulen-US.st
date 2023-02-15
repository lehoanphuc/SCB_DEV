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
             <img src = '../../../Images/logo.png' style='height:70px;' /><br/>             
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
                SENDER INFORMATION<hr /></td>
        </tr>
        <tr>
			<td>Sender name</td>
			<td>$senderName$</td>
        </tr>
        <tr>
			<td>Debit account</td>
			<td>$senderAccount$</td>
        </tr>        
        
        <tr>
			<td>Bank</td>
			<td>$senderBranch$</td>
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
			<td>$recieverName$</td>
        </tr>
        <tr>
			<td>Credit account</td>
			<td>$recieverAccount$</td>
        </tr>        
        
        <tr>
			<td>Bank</td>
			<td>$receiverBranch$</td>
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
			<td>Amount</td>
			<td>$amount$ $ccyid$</td>
        </tr>
        <tr>
			<td>Amount in words</td>
			<td>$amountchu$</td>
        </tr>
        <tr>
			<td>Fee payment by</td>
			<td>$feeType$</td>
        </tr>
        <tr>
			<td>Fee amount</td>
			<td>$feeAmount$ $ccyid$</td>
        </tr>
        <tr>
			<td>Payment content</td>
			<td>$desc$</td>
        </tr>
       <tr>
			<td colspan="2">
				<br/>
				<hr/>
				<span style='font-size:8pt;'>
				Respond from the bank<br/>
				Transaction no: $tranID$, status: $status$, at: $tranDate$</span>
				<p><a href=' ' target='blank'></a></p>
				<span style='font-weight:bold;'>
				Thank you for using Siam Commercial Bank
			</td>
		</tr>
    </table>

</body>
</html>
