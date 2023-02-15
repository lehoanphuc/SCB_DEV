<%@ Page Language="C#" AutoEventWireup="true" CodeFile="print.aspx.cs" Inherits="Widgets_IBTransferBAC1_print" %>

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
            <img src='../../images/logo.png' />
            <div style="float:right;">
                <strong><%=Resources.labels.printdate %>: <%=DateTime.Now.ToString("dd/MM/yyyy HH:mm") %></strong>
            </div>
        </div>
        <br />
        <div style='font-weight: 18pt; color: #a02226; font-size: 28px; font-family: Tahoma, Verdana, Arial, Helvetica, sans-serif;
            text-align: center;'>
            <strong><%=Resources.labels.corporateuserlimit %></strong>
        </div>
         <div style='font-weight: 10pt;  font-size: 14px; font-family: Tahoma, Verdana, Arial, Helvetica, sans-serif;
            text-align: center;'>
            <strong>
                <asp:Label ID="lblsearchdate" runat="server"></asp:Label></strong>
        </div>
        <br />
        <%--<div>
            <table>
                <tbody>
                    <tr>
                        <td><%=Resources.labels.userid%>: 
                            <asp:Label ID="lblUserID" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><%=Resources.labels.fullname %>:
                            <asp:Label ID="lblUserName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><%=Resources.labels.transaction %>:
                            <asp:Label ID="lblTransaction" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><%=Resources.labels.hanmucmotgiaodich%>:
                            <asp:Label ID="lblLimitPerTrans" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><%=Resources.labels.tonghanmucngay%>:
                            <asp:Label ID="lblLimitPerDay" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><%=Resources.labels.sogiaodichtrenngay%>:
                            <asp:Label ID="lblTransPerDay" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><%=Resources.labels.tiente%>:
                            <asp:Label ID="lblCurrency" runat="server"></asp:Label></td>
                    </tr>
                </tbody>
            </table>
        </div>--%>
        <br />
        <asp:Literal ID="ltrStatement" runat="server"></asp:Literal>
        <div>
            <p>
                <a href='https://www.scb.co.th/en/personal-banking.html' target='blank'></a>
            </p>
            <span style='font-weight:bold;'><%=Resources.labels.camonquykhachdasudungdichvucuaAYA_Bank + "!" %></span>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        window.print();
    </script>
</body>
</html>
