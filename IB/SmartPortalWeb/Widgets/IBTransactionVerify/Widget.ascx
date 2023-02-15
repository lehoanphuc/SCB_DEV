<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTransactionVerify_Widget" %>
<style type="text/css">
    .style11
    {
        width: 100%; 
        background-color:#EAEDD8;       
    }
    .tibtd
    {
    	
    }
    .tibtdh
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    }
        .quyen
    {
    	font-weight:bold;
    	text-align:center;
    }
    .al
{
	font-weight:bold;
	padding-left:5px;
	padding-top:10px;
	padding-bottom:10px;
}
    #txtarea
    {
        width: 518px;
        height: 128px;
    }
    </style>
<script src="widgets/IBTransferInBank1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/lang/en.js" type="text/javascript"></script>
<script src="JS/mask.js" type="text/javascript"></script>
<script src="JS/docso.js" type="text/javascript"></script>
<script src="widgets/IBTransferOutBank1/JS/common.js" type="text/javascript"></script>

<link href="widgets/IBTransferInBank1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<%--<asp:ScriptManager runat="server"></asp:ScriptManager>--%>
<br />
<div class="al">
<span><%=Resources.labels.trasoatgiaodich %></span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>   
<asp:Panel ID="pnVerify" runat="server">
<div class="block1"> 
                <div class="quyen">
                   <asp:Label ID="lberror" runat="server" Text="" ForeColor="Red"/>     
                </div>           	 
      	            
                     <div class="handle">                    	
                    	<%=Resources.labels.taothongtintrasoatgiaodich %>
                    </div>              
                    <div class="content">
                     
                        <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <asp:Label ID="Label9" runat="server" 
                                        Text="<%$ Resources:labels, thongtintrasoatgiaodich %>"></asp:Label> </td>
                            </tr>
                            <%--<tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label1" runat="server" Text="Mã người thụ hưởng *"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtNTHCode" runat="server" Width="237px"></asp:TextBox>
                                </td>                                
                            </tr>--%>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, tieude %>"></asp:Label>
                                &nbsp;*</td>
                                <td >
                                    <asp:TextBox ID="txttitle" runat="server" Width="237px"></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, noidung %>"></asp:Label>
                                &nbsp;*</td>
                                <td >
                                    <textarea ID="txtarea" runat="server" rows="10" cols="50"></textarea>
                                </td>                                
                            </tr>                             
                            </table>
                    </div>     
                    </asp:Panel>           
                     <asp:Panel ID="pnfeedback" runat="server">
                         <div class="block1">
                             <div class="handle">
                                 <asp:Label ID="Label4" runat="server" 7
                                     Text="<%$ Resources:labels, ketquagiaodich %>"></asp:Label>
                             </div>
                             <div class="content">
                                 <div style=" padding-top:10px; padding-bottom:10px; text-align:center;">
                                     <asp:Label ID="Label5" runat="server" ForeColor="Red" 
                                         Text="Tra soát giao dịch thành công"></asp:Label>
                                 </div>
                             </div>
                         </div>
                     </asp:Panel>
                  <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="bntSend" runat="server"  
                         Text="<%$ Resources:labels, send %>" OnClientClick="return validate();" 
                         onclick="bntSend_Click" />
                    &nbsp;
                    <asp:Button ID="btback" runat="server" 
                         Text="<%$ Resources:labels, quaylai %>" onclick="btback_Click"/>
                                             &nbsp;
                    <asp:Button ID="btn_exit" runat="server" 
                         Text="<%$ Resources:labels, thoat %>" onclick="btn_exit_Click"/>
                 </div>
  



 <script type="text/javascript">
	function validate()
    {  
      if(validateEmpty('<%=txttitle.ClientID %>','Bạn cần nhập tiêu đề'))
         {
               if(validateEmpty('<%=txtarea.ClientID %>','Bạn cần nhập nội dung'))
                 {
                 }
              else
                 {
                    document.getElementById('<%=txtarea.ClientID %>').focus();
                    return false;
                 }
         }
      else
         {
            document.getElementById('<%=txttitle.ClientID %>').focus();
            return false;
         }
    }
      
</script>

 

    