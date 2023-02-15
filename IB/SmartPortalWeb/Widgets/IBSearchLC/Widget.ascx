<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBSearchLC_Widget" %>
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
    .quyen3
    {
    	width:35%;
    	border-left:solid 1px #b9bfc1;
    	border-bottom:solid 1px #b9bfc1;
    }
    .quyen1
    {
    	
    	border-left:solid 1px #b9bfc1;
    	border-right:solid 1px #b9bfc1;
    	border-bottom:solid 1px #b9bfc1;
    }
    .thtdf
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    	border-top:solid 1px #b9bfc1;
    }
    .quyen
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    	width:35%;
    	border-left:solid 1px #b9bfc1;
    	border-top:solid 1px #b9bfc1;
    }
        .quyen2
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    	width:35%;
    	border-left:solid 1px #b9bfc1;
    	border-right:solid 1px #b9bfc1;
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
    	padding:10px 5px 10px 5px;
    }
    .searchtitle
    {

    	font-weight:bold;
    }
        .searchcontent
    {
    	width:25%;
    	border-top:solid 1px #b9bfc1;
    	border-right: solid 1px #b9bfc1;
    }
    .btsearch
    {
    	width:25%;
    	border-top:solid 1px #b9bfc1;
    }
    </style>
<link href="widgets/IBInfoLC/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/IBInfoLC/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBInfoLC/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/IBInfoLC/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBInfoLC/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBInfoLC/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

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
<span>Tìm kiếm L/C</span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>
<!--thong tin tai khoan DD-->
<asp:Panel ID="pnDD" runat="server">
    <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	Tìm Kiếm L/C
                    </div>   
                    <div class="content">
                            <table class="style1" cellspacing="5" cellpadding="0">
                            <tr>
                                <td class="searchtitle">
                                    <asp:Label ID="Label33" runat="server" Text="Số tham chiếu"></asp:Label>
                                </td>
                                <td>
                                    
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    
                                </td>
                                <td class="searchtitle">
                                    <asp:Label ID="Label39" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
                                </td>
                                <td >
                                    
                                    <asp:DropDownList ID="DropDownList1" runat="server" Width="170">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
                                    
                                </td>
                                <td>
                                    
                                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:labels, timkiem %>" />
                                    
                                </td>

                            </tr>
                            <tr>
                                <td class="searchtitle">
                                    <asp:Label ID="Label36" runat="server" Text="<%$ Resources:labels, mahopdong %>"></asp:Label>
                                </td>
                                <td >
                                    
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    
                                </td>
                                <td class="searchtitle">
                                    <asp:Label ID="Label40" runat="server" Text="Loại hình"></asp:Label>
                                </td>
                                 <td >
                                   
                                     <asp:DropDownList ID="DropDownList2" runat="server" Width="170">
                                         <asp:ListItem>In Wward</asp:ListItem>
                                         <asp:ListItem>Out Wward</asp:ListItem>
                                     </asp:DropDownList>
                                   
                                </td>
                            </tr>  
                            </table>
                            </div>
                    <br />
                    <div class="handle">                    	
                    	Kết Quả Tìm Kiếm L/C
                    </div> 
                    <div class="content">
                    <asp:GridView ID="GridViewSearchLC" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="5" Width="100%">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:HyperLinkField DataTextField="Sothamchieu" HeaderText="Số tham chiếu" 
                                Text="040-2-7100-0003" NavigateUrl="~/Default.aspx?po=3&p=128">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="Sohopdong" HeaderText="<%$ Resources:labels, mahopdong %>">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Tiente" HeaderText="Tiền tệ">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Loaihinh" HeaderText="Loại tiền">
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
                    <br />  
          </div>           
</asp:Panel>
<!--end-->
</div>

