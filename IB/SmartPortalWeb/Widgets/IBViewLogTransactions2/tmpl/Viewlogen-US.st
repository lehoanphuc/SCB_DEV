<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Untitled Page</title>
	<style type="text/css">
		.style1 {
			width: 100%;
			border: 1px solid rgb(204, 204, 204);
		}
		.thtdf {
			background-color: #7A58BF;
			border: 1px solid #ccc;
			padding: 5px 10px;
		}
		.trtd td {
			border: 1px solid #ccc;
			padding: 5px 10px;
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
                APPROVAL INFORMATION<br/><br/></td>
		</tr>
		<tr>
			<td style='width:25%'>Transaction code</td>
			<td style='width:25%'>$tranID$</td>
			<td style='width:25%'>Date time</td>
			<td style='width:25%'>$date$</td>
        </tr>
		<tr>
			<td style='width:25%'>Transaction type</td>
			<td style='width:25%'>$tranType$</td>
			<td style='width:25%'>Transaction ref</td>
			<td style='width:25%'>$refNo$</td>
        </tr>
		
		<tr>
			<td colspan="4"><br/>
			<b>SENDER INFORMATION</b><hr/>
			</td>
        </tr>	
		<tr>
			<td style='width:25%'>Debit account</td>
			<td style='width:25%'>$debitAccount$</td>
			<td style='width:25%'>Sender name</td>
			<td style='width:25%'>$debitName$</td>
        </tr>
		
		<tr style="display: $isReceiver$;">
			<td colspan="4"><br/>
			<b>RECEIVER INFORMATION</b><hr/>
			</td>
        </tr>	
		<tr style="display: $isReceiver$;">
			<td style='width:25%'>Credit account</td>
			<td style='width:25%'>$creditAccount$</td>
			<td style='width:25%'>Reciever name</td>
			<td style='width:25%'>$creditName$</td>
        </tr>
		<tr style="display: $isBatch$;">
			<td colspan="4"><br/>
			<b>RECEIVER INFORMATION</b><hr/>
			</td>
        </tr>	
		<tr style="display: $isBatch$;">
			<td colspan="4">
				$batchTable$
			</td>
		</tr>
		
		<tr style="display: $isTopup$;">
			<td colspan="4"><br/>
			<b>TOPUP INFORMATION</b><hr/>
			</td>
        </tr>
		<tr style="display: $isTopup$;">
			<td style='width:25%'>Phone</td>
			<td style='width:25%'>$phoneNo$</td>
			<td style='width:25%'>Telecom</td>
			<td style='width:25%'>$telecomName$</td>
        </tr>
		<tr style="display: $isTopup$;">
			<td style='width:25%'>Card Amount</td>
			<td style='width:25%'>$cardAmount$</td>
			<td style='width:25%'></td>
			<td style='width:25%'></td>
        </tr>
		
		<tr style="display: $isBillPayment$;">
			<td colspan="4"><br/>
			<b>BILL INFORMATION</b><hr/>
			</td>
        </tr>
		<tr style="display: $isBillPayment$;">
			<td style='width:25%'>Biller Name</td>
			<td colspan="3">$billerName$</td>
        </tr>
		
        <tr>
			<td colspan="4"><br/>
			<b>TRANSACTION DETAIL</b><hr/>
			</td>
        </tr>
        <tr>
			<td style='width:25%'>Amount</td>
			<td style='width:25%'>$amount$ $ccyid$</td>
			<td style='width:25%'>Fee amount</td>
			<td style='width:25%'>$feeAmount$ $ccyid$</td>
        </tr>
        <tr>
			<td style='width:25%'>Description</td>
			<td colspan='3'>$desc$</td>
			
        </tr>
        <tr>
			<td style='width:25%'>Status</td>
			<td style='width:25%'>$status$</td>
			<td style='width:25%'>Transfer by</td>
			<td style='width:25%'>$worker$</td>
        </tr>
		
		<tr style="display: $isSchedule$;">
			<td colspan="4"><br/>
			<b>SCHEDULE DETAIL</b><hr/>
			</td>
        </tr>
		<tr style="display: $isSchedule$;">
			<td style='width:25%'>Calendar name</td>
			<td style='width:25%'>$scheduleName$</td>
			<td style='width:25%'>Calendar type</td>
			<td style='width:25%'>$scheduleType$</td>
        </tr>
		<tr style="display: $isSchedule$;">
			<td style='width:25%'>From date</td>
			<td style='width:25%'>$fromDate$</td>
			<td style='width:25%'>To date</td>
			<td style='width:25%'>$toDate$</td>
        </tr>
		<tr style="display: $isSchedule$;">
			<td style='width:25%'>Next datetime execute</td>
			<td colspan='3'>$nextExecute$</td>
        </tr>
		
        <tr>
			<td colspan="4">
				<table style="width:100%;margin-top:15px;">
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
