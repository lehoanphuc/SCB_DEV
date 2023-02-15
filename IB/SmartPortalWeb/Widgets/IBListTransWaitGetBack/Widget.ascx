<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBListTransWaitGetBack_Widget" %>
<style type="text/css">
    .style1
    {
        width: 100%;        
    }
    .style2
    {
    	margin:5px 0px 10px 0px;
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
<link href="widgets/IBListTransWaitGetBack/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/IBListTransWaitGetBack/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBListTransWaitGetBack/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/IBListTransWaitGetBack/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBListTransWaitGetBack/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBListTransWaitGetBack/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

<div>
<!--chi tiet giao dich -->
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
<span>Danh sách giao dịch dự thu</span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>
    <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	Tìm kiếm giao dịch
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="0"> 
                            <tr >
                                <td class="thtds">
                                    <asp:Label ID="Label5" Font-Bold="True" runat="server" 
                                        Text="Mã giao dịch"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="TextBox1" runat="server" Width="74%"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    
                                    <asp:Label ID="Label7" Font-Bold="True" runat="server" 
                                        Text="Loại giao dịch"></asp:Label>
                                    
                                </td>
                                <td class="thtds">
                                    
                                    <asp:DropDownList ID="ddlTransactionType" runat="server" Width="77%">
                                        <asp:ListItem>Rút tiền</asp:ListItem>
                                        <asp:ListItem>Nạp tiền</asp:ListItem>
                                        <asp:ListItem>Chuyển khoản</asp:ListItem>
                                        <asp:ListItem>Thanh toán online</asp:ListItem>
                                    </asp:DropDownList>
                                    
                                </td>
                                <td class="thtds">
                                <asp:Button ID="Button2" runat="server" Text="<%$ Resources:labels, timkiem %>" />
                                </td>
                            </tr> 
                            <tr >
                                <td class="thtds">
                                    <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="<%$ Resources:labels, fromdate %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="txtFromDate" runat="server" Width="74%"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="<%$ Resources:labels, todate %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="txtToDate" runat="server" Width="74%"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    
                                    
                                    
                                </td>
                            </tr>
                            <tr >
                                <td class="thtds">
                                    <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:DropDownList ID="DDLStatus" runat="server" Width="77%">
                                        <asp:ListItem>Chấp nhận</asp:ListItem>
                                        <asp:ListItem>Từ chối</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                  <td class="thtds">
                                    <asp:Label ID="Label4" Font-Bold="true" runat="server" Text="<%$ Resources:labels, accountnumber %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="txtAccno" runat="server" Width="74%"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    
                                    
                                    
                                </td>
                            </tr>
                                                    
                        </table>
                        <br />
                    
                    </div> 
                    <div style="padding-left:5px; padding-top:5px; padding-bottom:5px;">
                        <asp:Button ID="Button1" runat="server" Text="Approve" 
                            />
                    &nbsp;
                        <asp:Button ID="Button11" runat="server" Text="Delete" Width="52px" />
                    </div>
                    <div class="handle">                    	
                    	Danh sách giao dịch dự thu
                    </div>                    
                    <div class="content">
                        
                        <asp:GridView ID="gvLTWA" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="5" Width="100%" OnRowDataBound="gvLTWA_RowDataBound">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                        <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelect" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            <asp:HyperLinkField DataTextField="Magiaodich" HeaderText="Mã giao dịch" 
                                Text="040-2-7100-0003" NavigateUrl="~/Default.aspx?po=3&p=218">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="Loaigiaodich" HeaderText="Loại giao dịch">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Ngay" HeaderText="Ngày">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Ghico" HeaderText="Ghi có">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Ghino" HeaderText="Ghi nợ">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Mota" HeaderText="Mô tả">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Trangthai" HeaderText="<%$ Resources:labels, trangthai %>">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#009CD4" Font-Bold="True"  />
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
                if(elements[i].type == 'checkbox'&& elements[i].id!='ctl00_ctl12_gvLTWA_ctl01_cbSelectAll')
                {
                    elements[i].checked = true;
                    //elements[i].parentNode.parentNode.className="hightlight";
                }
            }
            
        }else
        {
            for(i=0;i < count;i++)
            {
                if(elements[i].type == 'checkbox'&& elements[i].id!='ctl00_ctl12_gvLTWA_ctl01_cbSelectAll')
                {
                    elements[i].checked = false;                     
                    //elements[i].parentNode.parentNode.className="nohightlight";
                }
            }
        }
    }
</script>
