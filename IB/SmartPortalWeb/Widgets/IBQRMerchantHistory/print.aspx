<%@ Page Language="C#" AutoEventWireup="true" CodeFile="print.aspx.cs" Inherits="Widgets_IBTransactionHistory1_print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=Resources.labels.ibankingstatement %></title>
    <link href="../../CSS/bootstrap.css" rel="stylesheet" />
    <link href="../../CSS/css.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 10px;">
            <div>
                <img src='https://upload.wikimedia.org/wikipedia/en/thumb/4/47/Siam_Commercial_Bank_%28logo%29.svg/345px-Siam_Commercial_Bank_%28logo%29.svg.png' style='height: 70px;' />
                <div style="float: right;">
                    <strong><%=Resources.labels.printdate %>: <%=DateTime.Now.ToString("dd/MM/yyyy HH:mm") %></strong>
                </div>
            </div>
            <div style='font-size: 18pt; color: #e5882d; font-size: 28px; text-align: center;'>
                <strong><%=Resources.labels.ibankingstatement %></strong>
            </div>
            <div style='font-size: 10pt; font-size: 14px; text-align: center;'>
                <strong>
                    <asp:Label ID="lblsearchdate" runat="server" Text=""></asp:Label></strong>
            </div>
            <br />
            <div class="content display-label">
                <div class="row text-left">
                    <div class="form-group">
                        <label><%=Resources.labels.sotaikhoan%> : </label>
                        <asp:Label ID="lblAccountNumber_DD" runat="server"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label><%=Resources.labels.tiente %> : </label>
                        <asp:Label ID="lblCurrency_DD" runat="server"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label><%=Resources.labels.accountname %> : </label>
                        <asp:Label ID="lblAccountName_DD" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <br />
            <asp:Literal ID="ltrStatement" runat="server"></asp:Literal>
            <div>
                <br />
                <p>
                    <a href='https://www.scb.co.th/en/personal-banking.html' target='blank'>www.scb.co.th/en/personal-banking</a>
                </p>
                <span style='font-weight: bold;'><%=Resources.labels.camonquykhachdasudungdichvucuabank %></span>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        window.print();
    </script>
</body>
</html>
