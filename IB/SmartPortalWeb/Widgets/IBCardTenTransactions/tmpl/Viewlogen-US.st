<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Untitled Page</title>
    <style>
		.tblIn
		{
			border:1px solid gray;
		}
		.tblIn td
		{
			border:1px solid gray;
		}
    </style>
</head>
<body>

    <table style="width:100%;" cellspacing="5">
		<tr>
			<td colspan="4" 
                style="font-weight: bold;">
             <img src='https://upload.wikimedia.org/wikipedia/en/thumb/4/47/Siam_Commercial_Bank_%28logo%29.svg/345px-Siam_Commercial_Bank_%28logo%29.svg.png' style='height:70px;' /><br/>             
             <br/></td>
		</tr>
		<tr>
			<td align="center" colspan="4" 
                style="font-weight: bold; font-size:14pt;">
                TRANSACTION DETAIL<br/><br/></td>
		</tr>
		<tr>
			<td style='width:25%'>Transaction No</td>
			<td style='width:25%'>$tranID$</td>
			<td style='width:25%'>Date time</td>
			<td style='width:25%'>$date$</td>
        </tr>
        <tr>
			<td colspan="4"><br/>
			<b>SENDER INFORMATION</b><HR/>
			</td>
        </tr>
        <tr>
			<td style='width:25%'>Debit account</td>
			<td style='width:25%'>$debitAccount$</td>
			<td style='width:25%'>Sender name</td>
			<td style='width:25%'>$senderName$</td>
        </tr>
        <tr>
			<td colspan="4"><br/>
			<b>RECIEVER INFOMATION</b><HR/>
			</td>
        </tr>
        
        <tr>
			<td style='width:25%'>Credit account</td>
			<td style='width:25%'>$creditAccount$</td>
			<td style='width:25%'>Receiver name</td>
			<td style='width:25%'>$receiverName$</td>
        </tr>
        <tr>
			<td style='width:25%'>Bank</td>
			<td style='width:25%'>$bank$</td>
			<td style='width:25%'>Identification number</td>
			<td style='width:25%'>$license$</td>
        </tr>
        <tr>
			<td style='width:25%'>Issue date</td>
			<td style='width:25%'>$issueDate$</td>
			<td style='width:25%'>Issue place</td>
			<td style='width:25%'>$issuePlace$</td>
        </tr>
         
        <tr>
			<td style='width:25%'>Receiver address</td>
			<td style='width:25%' colspan='3'>$receiverAdd$</td>
			
        </tr>
        <tr>
			<td colspan="4"><br/>
			<b>TRANSACTION DETAIL</b><HR/>
			</td>
        </tr>
        <tr>
			<td style='width:25%'>Amount</td>
			<td style='width:25%'>$amount$ $ccyid$</td>
			<td style='width:25%'>Fee amount</td>
			<td style='width:25%'>$feeAmount$ $ccyid$</td>
        </tr>
        <tr>
			<td style='width:25%'>Interest</td>
			<td style='width:25%'>$LDH$ $ccyid$</td>
			<td style='width:25%'>VAT</td>
			<td style='width:25%'>$VATAmount$ $ccyid$</td>
        </tr>
        <tr>
			<td style='width:25%'>Description</td>
			<td style='width:25%' colspan='3'>$desc$</td>
			
        </tr>
        <tr>
			<td style='width:25%'>Status</td>
			<td style='width:25%'>$status$</td>
			<td style='width:25%'>Approver</td>
			<td style='width:25%'>$approver$</td>
        </tr>
        <tr>
			<td style='width:25%'>Transfer by</td>
			<td style='width:25%'>$worker$</td>
			<td style='width:25%'></td>
			<td style='width:25%'></td>
        </tr>
        <tr>
			<td colspan="4">
				<table style="width:100%;margin-bottom:50px;margin-top:15px;">
					<tr>
						
					</tr>
				</table>
			</td>
		</tr>
       <tr>
			<td colspan="4">
				<br/>
				<span style='font-size:8pt;'>
				Respond from the bank<br/>
				Transaction no: $tranID$, status: $status$, at: $date$</span>
				<p><a href='https://www.scb.co.th/en/personal-banking.html' target='blank'>www.scb.co.th/en/personal-banking</a></p>
				<span style='font-weight:bold;'>
				Thank you for using Siam Commercial Bank!
				</span>
			</td>
		</tr>
    </table>

</body>
</html>
