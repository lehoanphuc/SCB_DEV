<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBResetPasswords_Widget" %>
<link href="Widgets/ChangePassword/CSS/Default.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTKCKHNewAccount/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTKCKHNewAccount/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTKCKHNewAccount/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<script src="widgets/IBTransactionHistory1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/lang/en.js" type="text/javascript"></script>


<link href="widgets/IBTKCKHNewAccount/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTKCKHNewAccount/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTKCKHNewAccount/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="widgets/SEMSCustomerList/JS/commonjs.js"> </script> 
<style type="text/css">
   .al
    {
	    font-weight:bold;
	    padding-left:5px;
	    padding-top:10px;
	    padding-bottom:10px;
    }
   
</style>
<link href="widgets/IBTransactionHistory1/CSS/jscal2.css" rel="stylesheet" type="text/css" />


<%--<div class="al">
<span>Lấy lại mật khẩu</span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>--%>
<div id="login">
<div style=" text-align:center; margin:5px 1px 5px 1px; color:Red; font-weight:bold;">
<asp:Label ID="lblError" runat="server" Font-Bold="False" SkinID="lblImportant"></asp:Label>
</div>

<div class="block1" style="width:500px; margin:10px auto 10px auto;">            	 
<asp:Panel ID="pnFocus" runat="server">            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.laylaimatkhau %>
                    </div>                    
                    <div class="content" style="background-color:#EAEDD8;">
<table class="tblLogin" style="background-color:#EAEDD8; width:100%;">
    <tr>
        <td class="tdleft">
            <asp:Label ID="lblUserName" runat="server" text='<%$ Resources:labels, username %>'> *</asp:Label>
            &nbsp;*</td>
        <td>
            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="lblPassword" runat="server" text='<%$ Resources:labels, email %>'> *</asp:Label>
            &nbsp;*</td>
        <td>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        </td>
    </tr>
    
</table>
    </asp:Panel>

 </div>
</div>

 <asp:Panel ID="pnResult" runat="server">
 <div class="block1" style="width:500px; margin:10px auto 10px auto;">            	 
            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.ketquagiaodich %>
                    </div>                    
                    <div class="content">
                        <br />
                        <div style=" text-align:center; margin:1px 1px 1px 1px; color:Red; font-weight:bold;">
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" SkinID="lblImportant"  Text="<%$ Resources:labels, laylaimatkhauthanhcongmatkhaumoiduocguiquaemailcuaban %>" ></asp:Label>
                        </div>
                        <br />
                      
                    </div>                
                 <!--Button next-->

</div>
</asp:Panel>
      <div style="text-align:center; margin-top:10px;">
            <asp:Button ID="btnChange" runat="server" onclick="btnChange_Click" 
                OnClientClick="return validate();" Text="<%$ Resources:labels, thaydoi %>" />
            &nbsp;
            <asp:Button ID="btnReset" runat="server" onclick="btnReset_Click" 
                Text="<%$ Resources:labels, lamlai %>" />&nbsp;
            <asp:Button ID="Button3" runat="server" 
              Text="<%$ Resources:labels, thoat %>" onclick="Button3_Click" />
        </div>
<script>
function validate() {
        //validate username
        if (document.getElementById('<%=txtUsername.ClientID%>').value == '') 
        {
            alert('<%=Resources.labels.vuilongnhaptendangnhap %>');
            document.getElementById('<%=txtUsername.ClientID %>').focus();
            return false;
        }
        else 
        {
            //validate email
             if (document.getElementById('<%=txtEmail.ClientID%>').value == '') 
            {
                alert('<%=Resources.labels.vuilongnhapemail %>');
                document.getElementById('<%=txtEmail.ClientID %>').focus();
                return false;
            }
             
              else
            {
             if(checkEmail('<%=txtEmail.ClientID %>','<%=Resources.labels.emailkhongdungdinhdang %>')==false)
                {
                document.getElementById('<%=txtEmail.ClientID %>').focus();
                return false;                   
                }
                
            }
           
                         
         }
       }                                
                                 

    </script>