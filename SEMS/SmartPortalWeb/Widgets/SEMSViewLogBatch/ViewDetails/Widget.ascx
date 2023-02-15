<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSViewLogBatch_ViewDetails_Widget" %>
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
    .al
    {
	    font-weight:bold;
	    padding-left:5px;
	    padding-top:10px;
	    padding-bottom:10px;
    }
        .thtdf
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    	border-top:solid 1px #b9bfc1;
    }
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
        </style>
<script src="widgets/IBTransferInBank1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/lang/en.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/mask.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/common.js" type="text/javascript"></script>

<link href="widgets/IBTransferInBank1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

<div class="al">
<span><%=Resources.labels.nhatkygiaodichlo %>
</span><br />
<img style="margin-top:5px;" src="widgets/IBTransactionHistory/Images/underline.gif" />
</div>
 <div id="divError" style="text-align:center; color:Red;">
<asp:Label ID="lblError" runat="server"></asp:Label>
</div>
 <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.chitietgiaodich %>

                    </div>                    
                    <div class="content">                    
                         <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label5" runat="server" 
                                        Text="<%$ Resources:labels, magiaodich %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="lblTransID" runat="server"></asp:Label>                                    
                                </td>                                
                            </tr>
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <%=Resources.labels.thongtintaikhoan %>

                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label3" runat="server" 
                                        Text="<%$ Resources:labels, taikhoannguon %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="lblConfirmSenderAcctno" runat="server" Font-Bold="False"></asp:Label>                                    
                                </td>                                
                            </tr>
                            
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label4" runat="server" 
                                        Text="<%$ Resources:labels, tentaikhoan %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="lblConfirmSenderName" runat="server" Font-Bold="False"></asp:Label>                                    
                                </td>                                
                            </tr>
                            
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label6" runat="server" 
                                        Text="<%$ Resources:labels, sodu %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="lblConfirmBalance" runat="server" Font-Bold="False"></asp:Label>                                    
                                &nbsp;<asp:Label ID="lblCCYID" runat="server" Font-Bold="False"></asp:Label>                                    
                                </td>                                
                            </tr>
                             <tr>
                                <td colspan="2" class="tibtdh">
                                    <%=Resources.labels.noidungthanhtoan %>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                                  <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, noidung %>"></asp:Label>
                                                  <br />                    
                                </td>
                                <td >
                                                                  
                                    <asp:Label ID="lblContent" runat="server"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td colspan="2">
                                  <div style="overflow:auto; height:300px; width:100%;">                                  
                                    <asp:GridView ID="gvConfirm" runat="server" AutoGenerateColumns="False" 
                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                        CellPadding="3" Width="100%">
                                        <RowStyle ForeColor="#000066" />
                                        <Columns>
                                            <asp:BoundField DataField="STT" HeaderText="STT">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Account" HeaderText="<%$ Resources:labels, sotaikhoan %>">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="User" HeaderText="<%$ Resources:labels, nguoithuhuong %>">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Amount" HeaderText="<%$ Resources:labels, sotien %>">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Desc" HeaderText="<%$ Resources:labels, diengiai %>">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>    
                                             <asp:BoundField DataField="Status" HeaderText="<%$ Resources:labels, trangthai %>">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>                                          
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#009CD4" Font-Bold="True" ForeColor="Black" />
                                    </asp:GridView>
                                  
                                    </div>
                                </td>
                                                              
                            </tr>
                            <tr>
                                <td>
                                    <%=Resources.labels.tongtien %>: <asp:Label ID="lblTotal" runat="server" Font-Bold="true"></asp:Label>   
                                <asp:Label ID="lblTable" runat="server" Visible ="false" ></asp:Label>
                                </td>
                                <td>
                                    
                                </td>
                            </tr>
                            </table>
                    </div>                
                 <!--Button next-->
                 
                  
    </div>
<br />
<asp:Panel runat="server" ID="pnDesc">
 <div class="block1">
 <div class="handle">                    	
                    	<%=Resources.labels.thongtinduyetgiaodich %></div>                    
                    <div class="content">
                        <table class="style11" cellspacing="0" cellpadding="5">
                          
                                   
                             <tr>
                                 <td class="thtdf" style="width:20%;">
                                     <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, diengiai %>"></asp:Label>
                                 </td>
                                 <td style="width:20%;">
                                     <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" 
                                         SkinID="txtTwoColumn"></asp:TextBox>
                                 </td>
                                 
                                 <td class="thtd" style="width:20%;">
                                     &nbsp;</td>
                             </tr>
                                   
                        </table>
                    </div>  
                    </div>
</asp:Panel>     

<asp:Panel ID = "pn_approve" runat="server">        
<div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="btnPrevious" runat="server" onclick="Button5_Click" 
                        Text="<%$ Resources:labels, giaodichtruoc %>" SkinID="skn120" />
&nbsp;
                    <asp:Button ID="btnApprove" runat="server" onclick="btnApprove_Click" 
                        Text="<%$ Resources:labels, duyet %>" Width="70px" />
&nbsp;
                    <asp:Button ID="btnReject" runat="server" onclick="btnReject_Click" 
                        Text="<%$ Resources:labels, khongduyet %>" Width="78px" OnClientClick="return reject();" />
                    &nbsp;
                    <asp:Button ID="Button3" runat="server" PostBackUrl="javascript:history.go(-1)" 
                        Text="<%$ Resources:labels, back %>" />
&nbsp;
                    <asp:Button ID="btnNext" runat="server" onclick="Button4_Click" 
                        Text="<%$ Resources:labels, giaodichketiep %>" SkinID="skn120" />
                 </div>
</asp:Panel>
<asp:Panel ID = "pn_View" runat="server">
    <table style="width:100%">
        <tr>
            <td align="center">
            <asp:Button ID="btnPrint" runat="server"  Text="<%$ Resources:labels, inthongtin %> " 
                   onclientclick="javascript:return poponload()"  />&nbsp;
            <asp:Button ID="Button1" runat="server" 
                         Text="<%$ Resources:labels, back %>" PostBackUrl="javascript:history.go(-1)"/>
            </td>
        </tr>
    </table>
 </asp:Panel>
 
 
 <script type="text/javascript">
      function poponload()
    {
    testwindow= window.open ("widgets/SEMSViewLogBatch/print.aspx?cul="+'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
        "menubar=1,scrollbars=1,width=700,height=650");
    testwindow.moveTo(0,0);
    return false;
    }
function reject()
{

    if(document.getElementById('<%=txtDesc.ClientID %>').value=='')
    {
        window.alert('<%=Resources.labels.banvuilongnhaplydokhongduyetgiaodichnay %>');
        document.getElementById('<%=txtDesc.ClientID %>').focus();
        return false;
    }
}
</script>