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
             <img src='../../../Images/logoIB.png' style='height:70px;' /><br/>             
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
			<td>Transaction Type</td>
			<td>$tranType$</td>
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
                PAYMENT CONTENT<hr /></td>
        </tr>
        
        <tr>
			<td>Total Amount</td>
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
			<td>Total Fee amount</td>
			<td>$feeAmount$ $ccyid$</td>
        </tr>
        <tr>
			<td>Payment content</td>
			<td>$desc$</td>
        </tr>
        <tr>
			<td colspan="2">
				<table style="width:100%;margin-bottom:30px;margin-top:15px;">
					<tr>
						<td style="width:25%;text-align:center">
						Accountant
						</td>
						<td style="width:25%;text-align:center">
						Supervisor
						</td>
						<td style="width:25%;text-align:center">
						Acc.Holder
						</td>
						<td style="width:25%;text-align:center">
						Sender
						</td>
					</tr>
					$details$
				</table>
			</td>
		</tr>
       <tr>
			<td colspan="2">
				<br/>
				<hr/>
				<p><a href=''https://www.scb.co.th/en/personal-banking.html' target='blank'>scb.co.th</a></p>
				<span style='font-weight:bold;'>
				Thank you for using Siam Commercial Bank!
				</span>
			</td>
		</tr>
    </table>

</body>
</html>
