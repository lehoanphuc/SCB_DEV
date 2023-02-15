<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBBillPayment_Widget" %>
<style type="text/css">
    .style1
    {
        width: 100%; 
        background-color:#EAEDD8;       
    }
        .style2
    {
        width: 100%; 
        background-color:#EAEDD8;       
    }
    .tibtd
    {
    	width: 25%; 
    }
    .tibtdh
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    }
    #divResult
    {
    	 background-color:#EAEDD8;  
    }
    #divError
    {   
    	width:100%;	
    	
    	height:10px;
    	text-align:center;
    	padding:0px 5px 5px 5px;
    }
       .gvHeader
    {
    	text-align:left;
    }
    .style3
    {
        background-color: #009CD4;
        font-weight: bold;
        height: 31px;
    }
    .style4
    {
        width: 14%;
    }
    .style5
    {
        width: 18%;
    }
</style>
<script src="widgets/IBTransferInBank1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/lang/en.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/mask.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/common.js" type="text/javascript"></script>

<link href="widgets/IBTransferInBank1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSProduct/JS/common.js" type="text/javascript"></script>

<div style="text-align:center; color:Red;">
<asp:Label runat="server" ID="lblTextError"></asp:Label></div>

<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%>

<asp:Panel ID="pnService" runat="server">
<div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.thanhtoanhoadon %>
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <%=Resources.labels.thongtintaikhoan %>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, dichvu %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlservice" runat="server" AutoPostBack="true" 
                                        onselectedindexchanged="ddlservice_SelectedIndexChanged">

                                    </asp:DropDownList>
                                &nbsp;</td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, nhacungcap %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlprovider" runat="server" AutoPostBack="true">

                                    </asp:DropDownList>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, sodanhba %>"></asp:Label>
                                </td>
                                <td >   
                                    <asp:TextBox ID="txtCustCode" runat="server"></asp:TextBox>
                                    <asp:HiddenField ID="hdFee" runat="server" />
                            </tr>
                            
                            </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="btnNext" runat="server" OnClientClick="return validate();" 
                         Text="<%$ Resources:labels, next %>" onclick="btnNext_Click"  />
                 </div>
                  
    </div>
    

</asp:Panel>
 <!--end-->
 <!--confirm-->
 <asp:Panel ID="pnPayment" runat="server">
 <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    <%=Resources.labels.thanhtoanhoadon %>
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="5" border=1>
                            <tr>
                                <td colspan="4" class="style3">
                                    <%=Resources.labels.thongtinhoadon %>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, dichvu %>"></asp:Label>
                                </td>
                                <td style="width:20%">
                                    <asp:Label ID="lblService" runat="server"></asp:Label>
                                </td> 
                              <td class="style5">
                                    <asp:Label ID="Label17" runat="server" Text="<%$ Resources:labels, nhacungcap %>"></asp:Label>
                                </td>
                               <td style="width:50%">
                                    <asp:Label ID="lblprovider" runat="server"></asp:Label>
                                </td>                                 
                            </tr>
                            <tr>
                                <td class="style4">
                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, sodanhba %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblCustCode" runat="server"></asp:Label>
                                </td>  
                                <td class="style5">
                                    <asp:Label ID="Label21" runat="server" Text="<%$ Resources:labels, tenkhachhang %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblcustname" runat="server"></asp:Label>
                                </td>                               
                            </tr>
                                                        
                            <tr>
                                <td class="style4">
                                    <asp:Label ID="Label46" runat="server" Text="<%$ Resources:labels, diachi %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lbladdress" runat="server"></asp:Label>
                                </td>
                               <td class="style5">
                                    <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, duong %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblstreet" runat="server"></asp:Label>
                                </td>                                
                            </tr>
                             </table>
                        <%--  <div class="content"> --%>  <div id="divResult">
<asp:Literal runat="server" ID="ltrError"></asp:Literal>
    <asp:GridView ID="gvProductList" runat="server" BackColor="White" 
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Width="100%" AllowPaging="True" AutoGenerateColumns="False" 
        onrowdatabound="gvProductList_RowDataBound" PageSize="15" 
        onpageindexchanging="gvProductList_PageIndexChanging" 
        onsorting="gvProductList_Sorting" AllowSorting="True">
        <RowStyle ForeColor="#000000" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbxSelect" runat="server" />                   
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ID" Visible="false" >
                <ItemTemplate>
                    <asp:Label ID="lbID" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, sohoadon %>" ><%--SortExpression="ProductName"--%>
                <ItemTemplate>
                    <asp:Label ID="lbsohoadon" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, namhoadon %>" >
                <ItemTemplate>
                   <asp:Label ID="lblnamhoadon" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, kyhoadon %>" >
                <ItemTemplate>
                    <asp:Label ID="lbkyhoadon" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, tienphi %>" >
                <ItemTemplate>
                    <asp:Label ID="lbltienphi" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, tienthue %>">
                <ItemTemplate>
                    <asp:Label ID="lbtienthue" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="<%$ Resources:labels, bieuphi %>" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lbbieuphi" runat="server" Visible="false"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, dinhmuc %>" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lbdinhmuc" runat="server" Visible="false"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>           
            <asp:TemplateField HeaderText="<%$ Resources:labels, giaban %>" >
                <ItemTemplate>
                    <asp:Label ID="lbgiaban" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="<%$ Resources:labels, tongcong %>">
                <ItemTemplate>
                    <asp:Label ID="lbtongcong" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
<%--            
            <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                <ItemTemplate>
                    <asp:HyperLink ID="hpEdit" runat="server">[hpEdit]</asp:HyperLink>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                <ItemTemplate>
                    <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>--%>
        </Columns>
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" CssClass="pager" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#E0ECFF" Font-Bold="True" ForeColor="Black" />
    </asp:GridView>
    <br />
    <%--<asp:Literal ID="litPager" runat="server"></asp:Literal>--%>
</div>
                             <table class="style1" cellspacing="0" cellpadding="5">                           
                            <tr>
                                <td colspan="2" class="tibtdh">
                                   <%=Resources.labels.thongtinthanhtoan %>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label51" runat="server" Text="<%$ Resources:labels, taikhoanthanhtoan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlSenderAccount" runat="server"
                                    onselectedindexchanged="ddlSenderAccount_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem>00009102004 - Tài khoản thanh toán</asp:ListItem>
                                        <asp:ListItem>00009103003 - Tài khoản thanh toán</asp:ListItem>
                                    </asp:DropDownList>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label26" runat="server" Text="<%$ Resources:labels, sodutaikhoanthanhtoan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblsodutaikhoan" runat="server" Text=""></asp:Label>
                                    &nbsp;<asp:Label ID="lblCCYIDDD" runat="server"></asp:Label></td>                                
                            </tr>
                            <tr>
  <%--                              <td class="tibtd">
                                    <asp:Label ID="Label45" runat="server" Text="<%$ Resources:labels, sotien %>"></asp:Label>*
                                </td>--%>
                                <td >
                               <%-- onkeyup="ntt('ctl00_ctl13_txtAmount','ctl00_ctl13_lblText',event);"--%>
<%--                                    <asp:TextBox ID="txtAmount" runat="server"  ></asp:TextBox>
                                      &nbsp;<asp:Label ID="lblText" runat="server" Font-Size="7pt" Font-Bold="True" 
                                        Font-Italic="True" ForeColor="#0066FF" Width="200px"></asp:Label>--%>
                                </td>                                
                            </tr>
<%--                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label3" runat="server" Text="Hình thức "></asp:Label>
                                </td>
                                <td >
                                    <asp:RadioButton ID="radTS1" runat="server" GroupName="TIB" 
                                        Text="Chuyển ngay" Checked="True" onclick="resetTS();" />
                                    <br />
                                    <asp:RadioButton ID="radTS" runat="server" GroupName="TIB" 
                                        Text="Chuyển vào ngày (dd/mm/yyyy)" onclick="enableTS();" />
&nbsp;<asp:TextBox ID="txtTS" runat="server"></asp:TextBox>
                                </td>                                
                            </tr>--%>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label44" runat="server" Text="<%$ Resources:labels, noidungthanhtoan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="351px" 
                                        Height="68px"></asp:TextBox>
                                </td>                                
                            </tr>
                                                        
                        </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">

                
                    <asp:Button ID="BtnConfirm" runat="server" OnClientClick="return Validate1();"
                         Text="<%$ Resources:labels, xacnhan %>" onclick="BtnConfirm_Click" />
                          &nbsp;
                   <asp:Button ID="btnBack" runat="server" 
                         Text="<%$ Resources:labels, back %>" onclick="btnBack_Click" />
                 </div>
                  
    </div>
<%--    <script type="text/javascript">//<![CDATA[

      var cal = Calendar.setup({
          onSelect: function(cal) { cal.hide() }
      });
      
      cal.manageFields("ctl00_ctl13_txtTS", "ctl00_ctl13_txtTS", "%d/%m/%Y");     
            
           
    //]]></script>--%></asp:Panel>

<asp:Panel ID="pnConfirm" runat="server">
 <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    <%=Resources.labels.thongtinxacnhan %>
                    </div>                    
                    <div class="content">
                         <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="4" class="tibtdh">
                                    <%=Resources.labels.thongtinhoadon %>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:25%">
                                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, dichvu %>"></asp:Label>
                                </td>
                                <td style="width:25%">
                                    <asp:Label ID="lbldichvu" runat="server" Text="Trả tiền nước"></asp:Label>
                                </td> 
                              <td style="width:25%">
                                    <asp:Label ID="Label22" runat="server" Text="<%$ Resources:labels, nhacungcap %>"></asp:Label>
                                </td>
                               <td style="width:25%">
                                    <asp:Label ID="lblnhacungcap" runat="server" Text="Công ty nước"></asp:Label>
                                </td>                                 
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label30" runat="server" Text="<%$ Resources:labels, sodanhba %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblmakhachhang" runat="server" Text="KH000138474"></asp:Label>
                                </td>  
                                <td class="tibtd">
                                    <asp:Label ID="Label32" runat="server" Text="<%$ Resources:labels, tenkhachhang %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lbltenkhachhang" runat="server" Text="A Tũn"></asp:Label>
                                </td>                               
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, diachi %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lbldiachi" runat="server" Text="168"></asp:Label>
                                </td>
                               <td class="tibtd">
                                    <asp:Label ID="Label14" runat="server" Text="<%$ Resources:labels, duong %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblduong" runat="server" Text="Vô Lý Thường Kiệt"></asp:Label>
                                </td>                                
                            </tr>
                             </table>
                         
                         <table class="style2" cellspacing="0" cellpadding="5" border="1">
<%--                            <tr>
                                <td colspan="4" class="tibtdh">
                                    <%=Resources.labels.thongtinthanhtoan %>
                                </td>
                            </tr>--%>
                            <tr>
                                <td style="width:25%">
                                    <asp:Label ID="rdKHD" runat="server" Text="<%$ Resources:labels, sohoadon %>" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width:25%">
                                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:labels, namhoadon %>" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width:25%">
                                    <asp:Label ID="Label35" runat="server" Text="<%$ Resources:labels, kyhoadon %>" Font-Bold="true"></asp:Label>
                                </td>
                               <td style="width:25%">
                                    <asp:Label ID="Label29" runat="server" Text="<%$ Resources:labels, sotien %>" Font-Bold="true"></asp:Label>
                                </td>          
                            </tr>
                            
                            <tr>
                            <asp:Label ID="lblcontent" runat="server" Text=""></asp:Label>
                            </tr>

                             </table>
                             <table class="style2" cellspacing="0" cellpadding="5">
                                 <tr>
                                 <td style="width:20%">
                                 </td>   
                                 <td style="width:18%">
                                 </td>  
                                 <td style="width:23%">
                                 </td>  
                                 <td style="width:11%">
                                        <asp:Label ID="Label36" runat="server" Text="<%$ Resources:labels, tongcong %>"></asp:Label>
                                 </td>
                                 <td style="width:24%">
                                        <asp:Label ID="lblsumall" runat="server" Text="300,000"></asp:Label> <%Response.Write(Resources.labels.lak); %>
                                        <asp:HiddenField ID="hdsumall" runat="server"></asp:HiddenField>
                                 </td>                           
                                </tr>
                             </table>
                         
                         <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <%=Resources.labels.thongtinthanhtoan %>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:25%">
                                    <asp:Label ID="Label20" runat="server" Text="<%$ Resources:labels, taikhoanthanhtoan %>"></asp:Label>
                                </td>
                                <td style="width:25%">
                                    <asp:Label ID="lbltaikhoanthanhtoan" runat="server" Text="12618268"></asp:Label>
                                </td>
                            </tr> 
                             <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label28" runat="server" Text="<%$ Resources:labels, sodutaikhoanthanhtoan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblsdtk" runat="server" Text=""></asp:Label>
                                    &nbsp;<asp:Label ID="lblCCYIDDDC" runat="server"></asp:Label></td>                                
                            </tr>
                           <tr> 
                              <td style="width:25%">
                                    <asp:Label ID="Label31" runat="server" Text="<%$ Resources:labels, sotien %>"></asp:Label>
                                </td>
                               <td style="width:25%">
                                    <asp:Label ID="lblsotien" runat="server" Text="151,000"></asp:Label> &nbsp;<asp:Label 
                                        ID="lblCCYIDDDC1" runat="server"></asp:Label>&nbsp;<asp:HiddenField ID="txtChu" runat="server" />
                                </td>                                 
                            </tr>
                             <tr> 
                              <td style="width:25%">
                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, phi %>"></asp:Label>
                                </td>
                               <td style="width:25%">
                                    <asp:Label ID="lblPhi" runat="server" Text="0"></asp:Label> <%Response.Write(Resources.labels.lak); %>
                                </td>                                 
                            </tr>
                            
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label34" runat="server" Text="<%$ Resources:labels, noidungthanhtoan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblnoidungthanhtoan" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblSenderAccount" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblReceiverAccount" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblSenderName" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblBalanceSender" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblReceiverName" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblSenderBranch" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblReceiverBranch" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblSenderCCYID" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblCurrency" runat="server" Text="" Visible="false"></asp:Label>

                                </td>                              
                            </tr>
                             </table>
                            
              
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">

              <%--  OnClientClick="return Validate1();"--%>
                    <asp:Button ID="btnxacnhanOTP" runat="server" 
                         Text="<%$ Resources:labels, xacnhan %>" onclick="btnxacnhanOTP_Click" 
                         onclientclick="hienthiso();" />
                          &nbsp;
                   <asp:Button ID="Button8" runat="server" 
                         Text="<%$ Resources:labels, back %>" onclick="Button3_Click" />
                 </div>
                  
    </div>
<%--    <script type="text/javascript">//<![CDATA[

      var cal = Calendar.setup({
          onSelect: function(cal) { cal.hide() }
      });
      
      cal.manageFields("ctl00_ctl13_txtTS", "ctl00_ctl13_txtTS", "%d/%m/%Y");     
            
           
    //]]></script>--%></asp:Panel>
 <!--end--> 
 <!--token-->
 <asp:Panel ID="pnOTP" runat="server">
 <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.chitietgiaodich %>
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <%=Resources.labels.xacthucgiaodich %>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label58" runat="server" Text="<%$ Resources:labels, loaixacthuc %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlLoaiXacThuc" runat="server">
                                    </asp:DropDownList>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label59" runat="server" Text="<%$ Resources:labels, maxacthuc %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtOTP" runat="server" AutoCompleteType="None"></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    &nbsp;</td>
                                <td >
                                    <img alt="" src="widgets/IBTransferInBank1/Images/otp.gif" style="width: 100px; height: 60px" /></td>                                
                            </tr>
                                                        
                        </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                     <asp:Button ID="btnAction" runat="server" onclick="btnAction_Click" 
                         Text="<%$ Resources:labels, thuchien %>" />
                 &nbsp;
                     <asp:Button ID="btnBackOTP" runat="server" onclick="btnBackOTP_Click" 
                         Text="<%$ Resources:labels, quaylai %>" />
&nbsp;
                 </div>
                  
    </div>
 </asp:Panel>
 <!--end-->
<!--sao ke-->
<asp:Panel ID="pnResultTransaction" runat="server">
 <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.ketquagiaodich %>
                    </div>                    
                            <div class="content">
                         <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="4" class="tibtdh">
                                    <%=Resources.labels.thongtinhoadon %>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:25%">
                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, dichvu %>"></asp:Label>
                                </td>
                                <td style="width:25%">
                                    <asp:Label ID="lbldichvuRS" runat="server" Text="Trả tiền nước"></asp:Label>
                                </td> 
                              <td style="width:25%">
                                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, nhacungcap %>"></asp:Label>
                                </td>
                               <td style="width:25%">
                                    <asp:Label ID="lblnhacungcapRS" runat="server" Text="Công ty nước"></asp:Label>
                                </td>                                 
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, sodanhba %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblmakhachhangRS" runat="server" Text="KH000138474"></asp:Label>
                                </td>  
                                <td class="tibtd">
                                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:labels, tenkhachhang %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lbltenkhachhangRS" runat="server" Text="A Tũn"></asp:Label>
                                </td>                               
                            </tr>
                             <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label24" runat="server" Text="<%$ Resources:labels, diachi %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lbldiachiRS" runat="server" Text="168"></asp:Label>
                                </td>
                               <td class="tibtd">
                                    <asp:Label ID="Label37" runat="server" Text="<%$ Resources:labels, duong %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblduongRS" runat="server" Text="Vô Lý Thường Kiệt"></asp:Label>
                                </td>                                
                            </tr>
                             </table>
                         
                         <table class="style2" cellspacing="0" cellpadding="5" border="1">
<%--                            <tr>
                                <td colspan="4" class="tibtdh">
                                    <%=Resources.labels.thongtinthanhtoan %>
                                </td>
                            </tr>--%>
                            <tr>
                                <td style="width:25%">
                                    <asp:Label ID="Label15" runat="server" Text="<%$ Resources:labels, sohoadon %>" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width:25%">
                                    <asp:Label ID="Label19" runat="server" Text="<%$ Resources:labels, namhoadon %>" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width:25%">
                                    <asp:Label ID="Label16" runat="server" Text="<%$ Resources:labels, kyhoadon %>" Font-Bold="true"></asp:Label>
                                </td>
                               <td style="width:25%">
                                    <asp:Label ID="Label23" runat="server" Text="<%$ Resources:labels, sotien %>" Font-Bold="true"></asp:Label>
                                </td>          
                            </tr>
                            
                            <tr>
                            <asp:Label ID="lblContentRS" runat="server" Text=""></asp:Label>
                            </tr>

                             </table>
                             <table class="style2" cellspacing="0" cellpadding="5">
                                 <tr>
                                 <td style="width:20%">
                                 </td>   
                                 <td style="width:18%">
                                 </td>  
                                 <td style="width:23%">
                                 </td>  
                                 <td style="width:11%">
                                        <asp:Label ID="Label25" runat="server" Text="<%$ Resources:labels, tongcong %>"></asp:Label>
                                 </td>
                                 <td style="width:24%">
                                        <asp:Label ID="lblsumallRS" runat="server" Text="300,000"></asp:Label> <%Response.Write(Resources.labels.lak); %>
                                 </td>                           
                                </tr>
                             </table>
                         
                         <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <%=Resources.labels.thongtinthanhtoan %>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:25%">
                                    <asp:Label ID="Label27" runat="server" Text="<%$ Resources:labels, taikhoanthanhtoan %>"></asp:Label>
                                </td>
                                <td style="width:25%">
                                    <asp:Label ID="lbltaikhoanthanhtoanRS" runat="server" Text="12618268"></asp:Label>
                                </td> 
                            </tr>
                                                        <tr>
                                <td style="width:25%">
                                    <asp:Label ID="Label39" runat="server" Text="<%$ Resources:labels, sodutaikhoanthanhtoan %>"></asp:Label>
                                </td>
                                <td style="width:25%">
                                    <asp:Label ID="lblsdtk1" runat="server" Text=""></asp:Label>
                                    &nbsp;<asp:Label ID="lblCCYIDDDR" runat="server"></asp:Label></td> 
                            </tr>
                           <tr> 
                              <td style="width:25%">
                                    <asp:Label ID="Label33" runat="server" Text="<%$ Resources:labels, sotien %>"></asp:Label>
                                </td>
                               <td style="width:25%">
                                    <asp:Label ID="lblsotienRS" runat="server" Text="151,000"></asp:Label> &nbsp;<asp:Label 
                                        ID="lblCCYIDDDR1" runat="server"></asp:Label></td>                                 
                            </tr>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label38" runat="server" Text="<%$ Resources:labels, noidungthanhtoan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblnoidungthanhtoanRS" runat="server" Text="Thanh toán tiền nước"></asp:Label>
                                </td>                              
                            </tr>
                             </table>
                            
              
                    </div>     
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                   <asp:Button ID="btnPrint" runat="server" 
                         onclientclick="javascript:return poponload()" 
                         Text="<%$ Resources:labels, inketqua %>" /><%--onclick="btnPrint_Click"--%>
&nbsp;
                    <asp:Button ID="btnNew" runat="server" 
                         Text='<%$ Resources:labels, lammoi %>' onclick="btnNew_Click"  /><%--onclick="btnNew_Click"--%>
                 </div>
                  
    </div>
</asp:Panel>
<%--onclick="btnPrint_Click"--%>
<!--end-->   <%--onclick="btnNew_Click"--%> <%--onclick="btnPrint_Click"--%>
                 
<script type="text/javascript">
 function SelectCbx(obj)
    {   
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked)
        {
            for(i=0;i < count;i++)
            {
                if(elements[i].type == 'checkbox'&& elements[i].id!='')
                {
                    elements[i].checked = true;
                    //elements[i].parentNode.parentNode.className="hightlight";
                }
            }
            
        }else
        {
            for(i=0;i < count;i++)
            {
                if(elements[i].type == 'checkbox'&& elements[i].id!='')
                {
                    elements[i].checked = false;                     
                    //elements[i].parentNode.parentNode.className="nohightlight";
                }
            }
        }
    }
     function poponload()
    {
    testwindow= window.open ("widgets/IBBillPayment/print.aspx?cul="+'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
        "menubar=1,scrollbars=1,width=500,height=650");
    testwindow.moveTo(0,0);
    return false;
    }
    function enableOTP()
    {
        document.getElementById("ctl00_ctl15_txtOTP").disabled=false;
        document.getElementById("ctl00_ctl15_txtOTPBSMS").value="";
        document.getElementById("ctl00_ctl15_txtOTPBSMS").disabled=true;
    }
    function enableSMSOTP()
    {
        document.getElementById("ctl00_ctl15_txtOTP").disabled=true;
        document.getElementById("ctl00_ctl15_txtOTPBSMS").disabled=false;
        document.getElementById("ctl00_ctl15_txtOTP").value="";
    }
    function replaceAll( str, from, to ) {
        var idx = str.indexOf( from );


        while ( idx > -1 ) {
            str = str.replace( from, to ); 
            idx = str.indexOf( from );
        }

        return str;
    }

    function ntt(sNumber,idDisplay,event)
    {  
        
       // executeComma(sNumber,event);  
        
//        if(document.getElementById(sNumber).value=="")
//        {       
//            document.getElementById(idDisplay).innerHTML="";
//            return;
//        }  
//ctl00_ctl14_lblamountso
            // alert(document.getElementById(sNumber).innerText); 
        document.getElementById(idDisplay).value=""+number2text(replaceAll(document.getElementById(sNumber).innerText,",",""))+"";
             
    }
    function hienthiso()
    {
     
        ntt('<%=lblsotien.ClientID %>','<%=txtChu.ClientID %>',event);
    }
    function validate()
    {
        
                if(validateEmpty('<%=txtCustCode.ClientID %>','Bạn cần nhập mã khách hàng'))
                {
                   
                }
                else
                {
                    document.getElementById('<%=txtCustCode.ClientID %>').focus();
                    return false;
                }
            
        
    }
    function Validate1()
    {
        
                if(validateEmpty('<%=txtDesc.ClientID %>','Bạn cần nhập thông tin mô tả'))
                    {
                       
                    }
                    else
                    {
                        document.getElementById('<%=txtDesc.ClientID %>').focus();
                        return false;
                    }
        
            
        
    }
    
    
    function number2text(sNumber)
    {    
     var sResult=new String();
     var sTemp=new String(sNumber);
     var arrNumberText=new Array("không","một","hai","ba","bốn","năm","sáu","bẩy","tám","chín");

     for(var i=0;i<sTemp.length;i++)
     {
     var nNum=parseInt(sTemp.charAt(i));
     var sLevel=new String("");
     switch(sTemp.length-i)
     {
     case 16:sLevel="vạn";break;
     case 15:sLevel="trăm";break;
     case 14:sLevel="mươi";break;
     case 13:sLevel="nghìn";break;
     case 12:sLevel="trăm";break;
     case 11:sLevel="mươi";break;
     case 10:sLevel="tỉ";break;
     case 9:sLevel="trăm";break;
     case 8:sLevel="mươi";break;
     case 7:sLevel="triệu";break;
     case 6:sLevel="trăm";break;
     case 5:sLevel="mươi";break;
     case 4:sLevel="nghìn";break;
     case 3:sLevel="trăm";break;
     case 2:sLevel="mươi";break;
     case 1:sLevel="đồng";break;
     }
     sResult+=arrNumberText[nNum]+" "+sLevel+" ";
     }
     //
     //return sResult.charAt(0).toUpperCase() + sResult.slice(1);
     sResult=regReplace(sResult,"không trăm tỉ","lẻ");
     sResult=regReplace(sResult,"không trăm triệu","lẻ");
     sResult=regReplace(sResult,"không trăm nghìn","lẻ");
     sResult=regReplace(sResult,"không trăm đồng","đồng");
     sResult=regReplace(sResult,"không mươi không ","");
     sResult=regReplace(sResult,"mươi không","mươi");
     sResult=regReplace(sResult,"lẻ không trăm ","");
     sResult=regReplace(sResult,"mươi một","mươi mốt");
     sResult=regReplace(sResult,"một mươi","mười");
     sResult=regReplace(sResult,"mười không","mười");
     sResult=regReplace(sResult,"không mươi","lẻ");
     sResult=regReplace(sResult,"mốt tỉ","một tỉ");
     sResult=regReplace(sResult,"mốt nghìn","một nghìn");
     sResult=regReplace(sResult,"mốt đồng","một đồng");
     sResult=regReplace(sResult,"triệu nghìn","triệu lẻ");
     sResult=regReplace(sResult,"tỉ triệu lẻ","tỉ lẻ");
     sResult=regReplace(sResult,"không triệu","triệu");
     sResult=regReplace(sResult,"mươi một","mươi mốt");
     sResult=regReplace(sResult,"mươi năm","mươi lăm");
     sResult=regReplace(sResult,"mười mốt","mười một");
     //
     sResult=regReplace(sResult,"không trăm tỉ","lẻ");
     sResult=regReplace(sResult,"không trăm triệu","lẻ");
     sResult=regReplace(sResult,"không trăm nghìn","lẻ");
     sResult=regReplace(sResult,"không trăm đồng","đồng");
     sResult=regReplace(sResult,"không mươi không ","");

     sResult=regReplace(sResult,"lẻ lẻ","lẻ");
     sResult=regReplace(sResult,"lẻ đồng","đồng");
     return sResult.charAt(0).toUpperCase() + sResult.slice(1);
    }
    function regReplace(sInput,sReg,sNew)
    {
     var re = new RegExp(sReg, "g");
     return sInput.replace(re, sNew);
    } 
</script>