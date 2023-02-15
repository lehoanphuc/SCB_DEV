<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSDeposit_Widget" %>
<style type="text/css">
    .style1
    {
        width: 100%;        
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
    .thtds
    {
    	padding:5px 5px 5px 5px;
    }
</style>
<link href="widgets/IBListTransWaitApprove/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/IBListTransWaitApprove/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBListTransWaitApprove/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/IBListTransWaitApprove/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBListTransWaitApprove/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBListTransWaitApprove/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

<div>
<style>
.al
{
	font-weight:bold;
	padding-left:5px;
	padding-top:10px;
	padding-bottom:10px;
}
</style>
<div class="al">
<img alt="" src="widgets/SEMSTransactionsApprove/Images/icon_transactions.jpg" style="width: 40px; height: 32px; margin-bottom:10px;" align="middle" /> 
    <%=Resources.labels.hoantragiaodichchuyenkhoanngoaihethong%><br />

</div>
<div id="divError" style="text-align:center;">
<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</div>
<!--chi tiet giao dich -->
    <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<asp:Label ID="Label6" Font-Bold="True" runat="server" 
                                        Text="<%$ Resources:labels, timkiemgiaodich %>"></asp:Label>
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="0"> 
                            <tr >
                                <td class="thtds">
                                    <asp:Label ID="Label5" Font-Bold="True" runat="server" 
                                        Text="<%$ Resources:labels, sogiaodich %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="txtTranID" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    
                                    <asp:Label ID="Label4" Font-Bold="true" runat="server" Text="<%$ Resources:labels, debitaccount %>"></asp:Label>
                                    
                                </td>
                                <td class="thtds">
                                    
                                    <asp:TextBox ID="txtAccno" runat="server"></asp:TextBox>
                                    
                                </td>
                                <td class="thtds">
                                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, timkiem %>"
                                        onclick="btnSearch_Click" />
                                </td>
                            </tr> 
                            <tr >
                                <td class="thtds">
                                    <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="<%$ Resources:labels, tungay %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="<%$ Resources:labels, denngay %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    
                                    
                                    
                                </td>
                            </tr>
                                                    
                            <tr >
                                <td class="thtds">
                                    <asp:Label ID="Label10" runat="server" Font-Bold="True" Text="<%$ Resources:labels, checknumber %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="txtCheckNo" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    &nbsp;</td>
                                <td class="thtds">
                                    &nbsp;</td>
                                <td class="thtds">
                                    
                                    
                                    
                                    &nbsp;</td>
                            </tr>
                                                    
                            <tr >
                                <td class="thtds">
                                    
                                    <asp:Label ID="Label8" Font-Bold="True" runat="server" Text="<%$ Resources:labels, trangthai %>"
                                        Visible="False"></asp:Label>
                                    
                                </td>
                                <td class="thtds" colspan="2">
                                    
                                    <asp:DropDownList ID="ddlStatus" runat="server" Visible="False">
                                        <asp:ListItem Value="1">Hoàn thành</asp:ListItem>
                                        <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>" ></asp:ListItem>
                                        <asp:ListItem Value="0">Đang xử lý</asp:ListItem>
                                        <asp:ListItem Value="2">Lỗi</asp:ListItem>
                                        <asp:ListItem Value="3">Chờ duyệt</asp:ListItem>
                                    </asp:DropDownList>
                                    
                                    <asp:Label ID="Label9" Font-Bold="True" runat="server" 
                                        Text="<%$ Resources:labels, malo %>" Visible="False"></asp:Label>
                                    
                                    <asp:TextBox ID="txtBatchRef" runat="server" Visible="False"></asp:TextBox>
                                    
                                    
                                    
                                    <asp:CheckBox ID="ckbIsBatch" runat="server" Font-Bold="True" 
                                        Text="Giao dịch lô" Visible="False" />
                                    
                                </td>
                                <td class="thtds">
                                    
                                    
                                    
                                    <asp:Label ID="Label1" Font-Bold="True" runat="server" Text="Kết quả" 
                                        Visible="False"></asp:Label>
                                    
                                    
                                    
                                    <asp:CheckBox ID="ckbIsDelete" runat="server" Font-Bold="True" Text="Deleted" 
                                        Visible="False" />
                                </td>
                                <td class="thtds">
                                    
                                    
                                    
                                    <asp:DropDownList ID="DDLAppSta" runat="server" Visible="False">
                                        <asp:ListItem Value="3">Approve</asp:ListItem>
                                        <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>" ></asp:ListItem>
                                        <asp:ListItem Value="0">Processing</asp:ListItem>
                                        <asp:ListItem Value="1">Pending to approve</asp:ListItem>                    
                                        <asp:ListItem Value="4">Delete</asp:ListItem>
                                    </asp:DropDownList>
                                    
                                </td>
                            </tr>
                            
                            </table>
                    
                    </div> 
                    <div style="margin-top:20px; margin-bottom:15px; padding-left:2px; text-align:left;">
                    <asp:Button runat="server" ID="btnDeposit" Text="<%$ Resources:labels, hoantra %>" 
                            onclick="btnDeposit_Click" />
                    </div>
                   
                        <asp:Literal runat="server" ID="ltrError"></asp:Literal>
                       <div style="height:600px; overflow:auto;">
                       
                        <asp:GridView ID="gvLTWA" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="5" Width="1500px" OnRowDataBound="gvLTWA_RowDataBound" 
                            AllowPaging="True" onpageindexchanging="gvLTWA_PageIndexChanging" 
                        PageSize="20">
                        <RowStyle ForeColor="#000066" />
                         <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbxSelect" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gvHeader" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, ngaygiogiaodich %>">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gvHeader" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="<%$ Resources:labels, checknumber %>">
                                 <ItemTemplate>
                                     <asp:Label ID="lblCheckNo" runat="server"></asp:Label>
                                 </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Center" />
                             </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, sogiaodich %>">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hpTranID" runat="server">[hpDetails]</asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gvHeader" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, debitaccount %>">
                                <ItemTemplate>
                                    <asp:Label ID="lblAccount" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gvHeader" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, sotien %>">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                    &nbsp;<asp:Label ID="lblCCYID" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gvHeader" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, mota %>">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gvHeader" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Kết quả" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblResult" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="<%$ Resources:labels, hoantra %>">
                                 <ItemTemplate>
                                     <asp:HyperLink ID="hpDeposit" runat="server">[hpDeposit]</asp:HyperLink>
                                 </ItemTemplate>
                             </asp:TemplateField>
                        </Columns>
                       <FooterStyle CssClass="gvFooterStyle" />
            <PagerStyle HorizontalAlign="Center" CssClass="pager" />
            <SelectedRowStyle />
            <HeaderStyle CssClass="gvHeader" />
                    </asp:GridView>
                        
                      </div>     
                 
    </div>    
<!--end-->
</div>

<script type="text/javascript">//<![CDATA[

      var cal = Calendar.setup({
          onSelect: function(cal) { cal.hide() }
      });
      cal.manageFields("<%=txtFromDate.ClientID %>", "<%=txtFromDate.ClientID %>", "%d/%m/%Y");      
      cal.manageFields("<%=txtToDate.ClientID %>", "<%=txtToDate.ClientID %>", "%d/%m/%Y");      
    //]]></script>
    <script>
 function SelectCbx(obj)
    {   
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked)
        {
            for(i=0;i < count;i++)
            {
                if(elements[i].type == 'checkbox'&& elements[i].id!='ctl00_ctl12_gvLTWA_ctl01_cbSelectAll' && elements[i].id!='<%=ckbIsDelete.ClientID%>' && elements[i].id!='<%=ckbIsBatch.ClientID%>')
                {
                    elements[i].checked = true;
                    //elements[i].parentNode.parentNode.className="hightlight";
                }
            }
            
        }else
        {
            for(i=0;i < count;i++)
            {
                if(elements[i].type == 'checkbox'&& elements[i].id!='ctl00_ctl12_gvLTWA_ctl01_cbSelectAll' && elements[i].id!='<%=ckbIsDelete.ClientID%>' && elements[i].id!='<%=ckbIsBatch.ClientID%>')
                {
                    elements[i].checked = false;                     
                    //elements[i].parentNode.parentNode.className="nohightlight";
                }
            }
        }
    }
</script>
