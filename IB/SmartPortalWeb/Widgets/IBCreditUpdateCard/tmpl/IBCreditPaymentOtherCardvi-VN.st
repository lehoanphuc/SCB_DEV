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
                ỦY NHIỆM CHI<br/><br/></td>
		</tr>
		<tr>
			<td>Ngày giờ giao dịch</td>
			<td>$tranDate$</td>
        </tr>
        <tr>
			<td>Số giao dịch</td>
			<td>$tranID$</td>
        </tr>
        <tr>
			<td style="height:5px;">
			
			</td>
        </tr>
        <tr>
            <td colspan="2" 
                style="font-weight: bold; font-size:10pt;">
                THÔNG TIN NGƯỜI TRẢ TIỀN<hr /></td>
        </tr>
        <tr>
			<td>Họ tên người trả tiền</td>
			<td>$senderName$</td>
        </tr>
        <tr>
			<td>Tài khoản ghi nợ</td>
			<td>$senderAccount$</td>
        </tr>        
        
        <tr>
			<td>Tại ngân hàng</td>
			<td>$senderBranch$</td>
        </tr>
        <tr>
			<td style="height:5px;">
			
			</td>
        </tr>
        <tr>
            <td colspan="2" 
                style="font-weight: bold; font-size:10pt;">
                THÔNG TIN NGƯỜI NHẬN TIỀN<hr /></td>
        </tr>
        <tr>
			<td>Họ tên người nhận tiền</td>
			<td>$recieverName$</td>
        </tr>
        <tr>
			<td>Tài khoản báo có</td>
			<td>$recieverAccount$</td>
        </tr>       
        
        <tr>
			<td>Tại ngân hàng</td>
			<td>$receiverBranch$</td>
        </tr>
        <tr>
			<td style="height:5px;">
			
			</td>
        </tr>
        <tr>
            <td colspan="2" 
                style="font-weight: bold; font-size:10pt;">
                NỘI DUNG CHUYỂN KHOẢN<hr /></td>
        </tr>
        
        <tr>
			<td>Số tiền</td>
			<td>$amount$ $ccyid$</td>
        </tr>
        <tr>
			<td>Số tiền bằng chữ</td>
			<td>$amountchu$</td>
        </tr>
        <tr>
			<td>Người trả phí</td>
			<td>$feeType$</td>
        </tr>
        <tr>
			<td>Số tiền phí</td>
			<td>$feeAmount$ $ccyid$</td>
        </tr>
        <tr>
			<td>Nội dung thanh toán</td>
			<td>$desc$</td>
        </tr>
        <tr>
			<td colspan="2">
				<table style="width:100%;margin-bottom:30px;margin-top:15px;">
					<tr>
						<td style="width:25%;text-align:center">
						KẾ TOÁN VIÊN
						</td>
						<td style="width:25%;text-align:center">
						KIỂM SOÁT VIÊN
						</td>
						<td style="width:25%;text-align:center">
						CHỦ TÀI KHOẢN
						</td>
						<td style="width:25%;text-align:center">
						NGƯỜI GỬI
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<br/>
				<hr/><span style='font-size:8pt;'>
				Trả lời từ ngân hàng<br/>
				Số giao dịch: $tranID$, trạng thái: $status$, thời điểm: $tranDate$</span>
				<p><a href='https://www.scb.co.th/en/personal-banking.html' target='blank'>www.scb.co.th/en/personal-banking</a></p>
				<span style='font-weight:bold;'>
				Cảm ơn quý khách đã sử dụng dịch vụ ebanking của Phongsavanh!
				</span>
			</td>
		</tr>
    </table>
	
</body>
</html>
