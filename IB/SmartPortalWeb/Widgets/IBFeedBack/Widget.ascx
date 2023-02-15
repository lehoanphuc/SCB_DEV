<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBFeedBack_Widget" %>
<style type="text/css">
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
    .hightlight
    {
    	background-color:#EAFAFF;color:#003366;
    }
    .nohightlight
    {
    	background-color:White;
    }
    .thtds
    {
    	padding:10px 5px 10px 4px;
    }
        #divToolbar
    {
    	background-color:#F8F8F8;
    	border:solid 1px #B9BFC1;
    	margin:20px 0px 5px 0px;
    	padding:5px 5px 5px 5px;
    	width:98.5%;
    }
        .al
    {
	    font-weight:bold;
	    padding-left:5px;
	    padding-top:10px;
	    padding-bottom:10px;
    }
</style>
<link href="widgets/IBTransactionHistory1/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/IBTransactionHistory1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/IBTransactionHistory1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<br />
<div class="al">
<span><%=Resources.labels.trasoatgiaodich %></span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>   
<div>
<!--thong tin tai khoan DD-->
<asp:Panel ID="pnDD" runat="server">
    <div class="block1">            	 
            	     <div class="handle">                    	
                    	<asp:Label ID="Label19" runat="server" Text="<%$ Resources:labels, timkiem %>"></asp:Label>
                    </div>                    
                    <div class="content">
                        <table style="width:100%;" cellspacing="0" cellpadding="0"> 
                            <tr >
                                <td class="thtds">
                                    <asp:Label ID="Label5" Font-Bold="True" runat="server" 
                                        Text="<%$ Resources:labels, sogiaodich %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="txtsgd" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    
                                    <asp:Label ID="Label7" Font-Bold="True" runat="server" 
                                        Text="<%$ Resources:labels, trangthai %>"></asp:Label>
                                    
                                </td>
                                <td class="thtds">
                                    
                                    <asp:DropDownList ID="ddlstatus" runat="server" ><%--SkinID="extDDL1"--%>
                                    <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
                                    <asp:ListItem Value="N" Text="<%$ Resources:labels, chuaxuly %>"></asp:ListItem>
                                     <asp:ListItem Value="Y" Text="<%$ Resources:labels, daxuly %>"></asp:ListItem>
                                    </asp:DropDownList>
                                    
                                </td>
                                <td class="thtds">
                                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, timkiem %>" onclick="btnSearch_Click" 
                                       />
                                </td>
                            </tr> 
                            <tr>
                             <td class="thtds">
                                    <asp:Label ID="Label1" Font-Bold="True" runat="server" 
                                        Text="<%$ Resources:labels, tieude %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="txttieude" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                                                                                
                            </table>
                    
                    </div> 
                    <asp:Panel ID="pndetails" runat="server"> 
                     <div id="divToolbar">
                    <%--<div style="padding-left:5px; padding-top:25px; padding-bottom:5px;">--%>
                       <asp:Button ID="btnnew" runat="server" Text="<%$ Resources:labels, guitrasoat %>" onclick="btnnew_Click" 
                             />
                    &nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:labels, huy %>" 
                             Width="52px" onclick="btnDelete_Click"  />
             <%-- </div>--%>
                    </div>
                    <div class="handle">                    	
                    	<%=Resources.labels.danhsachtrasoat %>
                    </div>                    
                    <div class="content">
                    <asp:Label runat="server" ID="lblAlert" ForeColor="Red"></asp:Label>
                        <asp:GridView ID="gvSTV" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="3" Width="100%" 
                            onrowdatabound="gvSTV_RowDataBound">
                            <RowStyle ForeColor="#000066" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbxSelect" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FEEDID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFeedid" runat="server" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:labels, sogiaodich %>">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hpTranCode" runat="server"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:labels, tieude %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:labels, noidung %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcontent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>     
                               <asp:TemplateField HeaderText="<%$ Resources:labels, comment %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcomment" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>                          
                                <asp:TemplateField HeaderText="<%$ Resources:labels, huy %>">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#009CD4" Font-Bold="True"  />
                        </asp:GridView>
                        
                    </div>  
                    </asp:Panel>              
                 
    </div>
    <br />
    <asp:Literal ID="litPager" runat="server"></asp:Literal>
</asp:Panel>
<!--end-->

</div>
<script>
 function SelectCbx(obj)
    {   
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked)
        {
            for(i=0;i < count;i++)
            {
                if(elements[i].type == 'checkbox'&& elements[i].id!='ctl00_ctl14_gvSTV_ctl01_cbxSelectAll')
                {
                    elements[i].checked = true;
                    //elements[i].parentNode.parentNode.className="hightlight";
                }
            }
            
        }else
        {
            for(i=0;i < count;i++)
            {
                if(elements[i].type == 'checkbox'&& elements[i].id!='ctl00_ctl14_gvSTV_ctl01_cbxSelectAll')
                {
                    elements[i].checked = false;                     
                    //elements[i].parentNode.parentNode.className="nohightlight";
                }
            }
        }
    }
</script>
