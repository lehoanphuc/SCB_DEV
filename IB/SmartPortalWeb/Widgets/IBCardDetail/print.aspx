<%@ Page Language="C#" AutoEventWireup="true" CodeFile="print.aspx.cs" Inherits="Widgets_IBTransactionHistory1_print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="widgets/IBTransactionHistory1/CSS/css.css" rel="stylesheet" type="text/css">
    <style type="text/css">
        body
        {
            font-family: Tahoma, Verdana, Arial, Helvetica, sans-serif;
            font-size: 9pt;    
        }
        .style1
        {
            width: 100%;        
        }
        /*.style2
        {
    	    margin:5px 0px 10px 0px;
        }*/
        .thtdbold
        {
    	    background-color:#EAFAFF;color:#003366;
    	    font-weight:bold;
    	    border-left:solid 1px #b9bfc1;
    	    border-top:solid 1px #b9bfc1;
        }
        .thtd
        {
    	    border-left:solid 1px #b9bfc1;
    	    border-top:solid 1px #b9bfc1;
        }
        .thtdf
        {
    	    background-color:#EAFAFF;color:#003366;
    	    font-weight:bold;
    	    border-top:solid 1px #b9bfc1;
        }
        .thtdff
        {  	
    	
    	    border-top:solid 1px #b9bfc1;
        }
        .thtr
        {
    	    background-color:#EAFAFF;color:#003366;
    	    font-weight:bold;
        }
        td
        {
    	    padding:2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <img src='https://upload.wikimedia.org/wikipedia/en/thumb/4/47/Siam_Commercial_Bank_%28logo%29.svg/345px-Siam_Commercial_Bank_%28logo%29.svg.png' style='height: 70px;' />
            <div style="float:right;">
                <strong><%=Resources.labels.printdate %>: <%=DateTime.Now.ToString("dd/MM/yyyy HH:mm") %></strong>
            </div>
        </div>
        <div style='font-weight: 18pt; color: #a02226; font-size: 28px; font-family: Tahoma, Verdana, Arial, Helvetica, sans-serif;
            text-align: center;'>
            <strong><%=Resources.labels.ibankingstatement %></strong>
        </div>
        <br />
        <div>
            <table>
                <tbody>
                    <tr>
                        <td><%=Resources.labels.sotaikhoan%>: 
                            <asp:Label ID="lbAccountNo" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><%=Resources.labels.tiente %>:
                            <asp:Label ID="lbCurrency" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><%=Resources.labels.accountname %>:
                            <asp:Label ID="lbAccountName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><%=Resources.labels.ngaymo%>:
                            <asp:Label ID="lbOpenDate" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><%=Resources.labels.branch%>:
                            <asp:Label ID="lbBranch" runat="server"></asp:Label></td>
                    </tr>
                </tbody>
            </table>
        </div>
        <br />
        <asp:Literal ID="ltrStatement" runat="server"></asp:Literal>
        <div>
            <br/>
            <p>
                <a href='https://www.scb.co.th/en/personal-banking.html' target='blank'>www.scb.co.th/en/personal-banking</a>
            </p>
            <span style='font-weight:bold;'><%=Resources.labels.camonquykhachdasudungdichvucuabank %></span>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        window.print();
    </script>
</body>
</html>
