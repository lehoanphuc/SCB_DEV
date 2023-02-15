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
             <img src='https://upload.wikimedia.org/wikipedia/en/thumb/4/47/Siam_Commercial_Bank_%28logo%29.svg/345px-Siam_Commercial_Bank_%28logo%29.svg.png' /><br/>             
             <br/></td>
		</tr>
		<tr>
			<td align="center" colspan="4" 
                style="font-weight: bold; font-size:14pt;">
                CHI TIẾT GIAO DỊCH<br/><br/></td>
		</tr>
		<tr>
			<td style='width:25%'>Số giao dịch</td>
			<td style='width:25%'>$tranID$</td>
			<td style='width:25%'>Ngày giờ giao dịch</td>
			<td style='width:25%'>$date$</td>
        </tr>
        <tr>
			<td colspan="4"><br/>
			<b>THÔNG TIN NGƯỜI GỬI</b><HR/>
			</td>
        </tr>
        <tr>
			<td style='width:25%'>Tài khoản ghi nợ</td>
			<td style='width:25%'>$debitAccount$</td>
			<td style='width:25%'>Họ tên người trả tiền</td>
			<td style='width:25%'>$senderName$</td>
        </tr>
        
        
        <tr>
			<td colspan="4"><br/>
			<b>THÔNG TIN NGƯỜI NHẬN</b><HR/>
			</td>
        </tr>
        <tr>
			<td style='width:25%'>Tài khoản báo có</td>
			<td style='width:25%'>$creditAccount$</td>
			<td style='width:25%'>Họ tên người nhận tiền</td>
			<td style='width:25%'>$receiverName$</td>
        </tr>
        <tr>
			<td style='width:25%'>Ngân hàng</td>
			<td style='width:25%'>$bank$</td>
			<td style='width:25%'>Số CMND</td>
			<td style='width:25%'>$license$</td>
        </tr>
        <tr>
			<td style='width:25%'>Ngày cấp</td>
			<td style='width:25%'>$issueDate$</td>
			<td style='width:25%'>Nơi cấp</td>
			<td style='width:25%'>$issuePlace$</td>
        </tr>
         
        <tr>
			<td style='width:25%'>Địa chỉ người nhận tiền</td>
			<td style='width:25%' colspan='3'>$receiverAdd$</td>
			
        </tr>
        <tr>
			<td colspan="4"><br/>
			<b>CHI TIẾT GIAO DỊCH</b><HR/>
			</td>
        </tr>
        <tr>
			<td style='width:25%'>Số tiền</td>
			<td style='width:25%'>$amount$ $ccyid$</td>
			<td style='width:25%'>Số tiền phí</td>
			<td style='width:25%'>$feeAmount$ $ccyid$</td>
        </tr>
        <tr>
			<td style='width:25%'>Lãi được hưởng</td>
			<td style='width:25%'>$LDH$ $ccyid$</td>
			<td style='width:25%'>VAT</td>
			<td style='width:25%'>$VATAmount$ $ccyid$</td>
        </tr>
        <tr>
			<td style='width:25%'>Mô tả</td>
			<td style='width:25%' colspan='3'>$desc$</td>
			
        </tr>
        <tr>
			<td style='width:25%'>Trạng thái</td>
			<td style='width:25%'>$status$</td>
			<td style='width:25%'>Người duyệt</td>
			<td style='width:25%'>$approver$</td>
        </tr>
        <tr>
			<td style='width:25%'>Người thực hiện</td>
			<td style='width:25%'>$worker$</td>
			<td style='width:25%'></td>
			<td style='width:25%'></td>
        </tr>
        $bill$
        <tr>
			<td colspan="4">
				<table style="width:100%;margin-bottom:50px;margin-top:15px;">
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
			<td colspan="4">
				<br/>
				<span style='font-size:8pt;'>
				Trả lời từ ngân hàng<br/>
				Số giao dịch: $tranID$, trạng thái: $status$, thời điểm: $date$</span>
				<p><a href='https://www.scb.co.th/en/personal-banking.html' target='blank'>www.scb.co.th/en/personal-banking</a></p>
				<span style='font-weight:bold;'>
				Cảm ơn quý khách đã sử dụng dịch vụ ebanking của Phongsavanh!
				</span>
			</td>
		</tr>
    </table>

</body>
</html>
