<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Untitled Page</title>
    <style>
		.tblIn
		{
			border:1px dotted gray;
		}
		.tblIn td
		{
			border:1px dotted gray;
		}
    </style>
</head>
<body>

    <table style="width:100%;" cellspacing="5">
		<tr>
			<td colspan="4" 
                style="font-weight: bold;">
             <img src = '../../../Images/logo.png' style='height:70px;' /><br/>             
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
			<td style='width:25%'>Date Time</td>
			<td style='width:25%'>$date$</td>
        </tr>
         <tr>
			<td colspan="4"><br/>
			<b>SENDER INFOMATION</b><HR/>
			</td>
        </tr>
        <tr>
			<td style='width:25%'>Debit Account</td>
			<td style='width:25%'>$debitAccount$</td>
			<td style='width:25%'>Sender Name</td>
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
			<td style='width:25%'>$receiverAdd$</td>
			<td style='width:25%'>Last user approve</td>
			<td style='width:25%'>$lastapp$</td>
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
			<td style='width:25%'>Words</td>
			<td style='width:25%'>$sotienbangchu$ </td>
			<td style='width:25%'>Interest</td>
			<td style='width:25%'>$LDH$ $ccyid$</td>
        </tr>
        <tr>
			<td style='width:25%'>Description</td>
			<td style='width:25%'>$desc$</td>
			<td style='width:25%'>VAT</td>
			<td style='width:25%'>$VATAmount$ $ccyid$</td>
        </tr>
        <tr>
			<td style='width:25%'>Status</td>
			<td style='width:25%'>$status$</td>
			<td style='width:25%'>Approver</td>
			<td style='width:25%'>$approver$</td>
        </tr>
        <tr>
			<td style='width:25%'>Created by</td>
			<td style='width:25%'>$worker$</td>
			<td style='width:25%'></td>
			<td style='width:25%'></td>
        </tr>
        <tr>
			<td colspan="4">
				<table style="width:100%;margin-bottom:50px;margin-top:15px;">
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
				</table>
			</td>
		</tr>
       <tr>
			<td colspan="4">
				<br/>
				<span style='font-size:8pt;'>
				Respond from the bank<br/>
				Transaction no: $tranID$, status: $status$, at: $date$</span>
				<p><a href='https://www.scb.co.th/en/personal-banking.html' target='blank'></a></p>
				<span style='font-weight:bold;'>
				Thank you for using Siam Commercial eBanking Services!
				</span>
			</td>
		</tr>
    </table>

</body>
</html>
