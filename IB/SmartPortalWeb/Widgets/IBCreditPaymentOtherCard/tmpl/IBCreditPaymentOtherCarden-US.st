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
             <img src='https://upload.wikimedia.org/wikipedia/en/thumb/4/47/Siam_Commercial_Bank_%28logo%29.svg/345px-Siam_Commercial_Bank_%28logo%29.svg.png' style='height:70px;' /><br/>             
             <br/></td>
		</tr>
		<tr>
			<td align="center" colspan="2" 
                style="font-weight: bold; font-size:14pt;">
                CREDIT CARD PAYMENT<br/><br/></td>
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
			<td>Sender information</td>
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
                CREDIT CARD INFOMATION<hr /></td>
        </tr>
       <tr>
			<td>Credit Card Number</td>
			<td>$cardNo$</td>
        </tr>     
<tr>
			<td>Card holder name</td>
			<td>$cardholdername$</td>
        </tr> 
<tr>
			<td>Outstanding Amount</td>
			<td>$outstandingamount$</td>
        </tr> 		
        <tr>
			<td style="height:5px;">
			
			</td>
        </tr>
        <tr>
            <td colspan="2" 
                style="font-weight: bold; font-size:10pt;">
                TRANSACTION INFOMATION<hr /></td>
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
			<td>Fee amount</td>
			<td>$feeAmount$ $ccyid$</td>
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
				<br/>
				<hr/><span style='font-size:8pt;'>
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
