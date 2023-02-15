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
			<td style="height:5px;">
			</td>
        </tr>
        <tr>
            <td colspan="2" 
                style="font-weight: bold; font-size:10pt;">
                PAYMENT INFORMATION<hr /></td>
        </tr>
		<tr>
			<td>Service</td>
			<td>$serviceName$</td>
        </tr>        
		 <tr style="display: $showlblRef1Verf$">
			<td>$lblRef1Verf$</td>
			<td>$refval01$</td>
        </tr>
		 <tr style="display: $showlblRef2Verf$">
			<td>$lblRef2Verf$</td>
			<td>$refval02$</td>
        </tr>
		 <tr style="display: $showlblRef3Verf$">
			<td>$lblRef3Verf$</td>
			<td>$refval03$</td>
        </tr>
		 <tr style="display: $showlblRef4Verf$">
			<td>$lblRef4Verf$</td>
			<td>refval04</td>
        </tr>
		 <tr style="display: $showlblRef5Verf$">
			<td>$lblRef5Verf$</td>
			<td>$refval05$</td>
        </tr>
		 <tr style="display: $showlblBill01Name$">
			<td>$lblBill01Name$</td>
			<td>$lblBill01Value$</td>
        </tr>
		 <tr style="display: $showlblBill02Name$">
			<td>$lblBill02Name$</td>
			<td>$lblBill02Value$</td>
        </tr>
		 <tr style="display: $showlblBill03Name$">
			<td>$lblBill03Name$</td>
			<td>$lblBill03Value$</td>
        </tr>
		 <tr style="display: $showlblBill04Name$">
			<td>$lblBill04Name$</td>
			<td>$lblBill04Value$</td>
        </tr>
		 <tr style="display: $showlblBill05Name$">
			<td>$lblBill05Name$</td>
			<td>$lblBill05Value$</td>
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
			<td>Payment Amount</td>
			<td>$amount$ $ccyid$</td>
        </tr>
        <tr>
			<td>Amount in words</td>
			<td>$amountchu$ $ccyid$</td>
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
			<td>Discount</td>
			<td>$discount$ $ccyid$</td>
        </tr>
        <tr>
			<td>Payment content</td>
			<td>$desc$</td>
        </tr>
        <!--<tr>
			<td colspan="2">
				<table style="width:100%;margin-bottom:30px;margin-top:15px;">
					<tr>
						
					</tr>
				</table>
			</td>
		</tr>-->
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
