
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="rptTransactionHistory_Widget" %>
<style type="text/css">
    #divSearch
    {
    	background-color:#F8F8F8;
    	border:solid 1px #B9BFC1;
    	margin:5px 5px 5px 5px;
    	padding:5px 5px 5px 5px;
    }
    #divToolbar
    {
    	background-color:#F8F8F8;
    	border:solid 1px #B9BFC1;
    	margin:0px 0px 0px 0px;
    	padding:5px 5px 5px 5px;
    }
     #divLetter
    {
    	background-color:#F8F8F8;
    	border:solid 1px #B9BFC1;
    	margin:10px 5px 5px 5px;
    	padding:5px 5px 5px 5px;
    }
    #divResult
    {
    	
    	margin:20px 5px 5px 5px;
    	padding:0px 0px 0px 0px;
    }
    .gvHeader
    {
    	text-align:left;
    }
    #divCustHeader
    {   
    	width:100%;	
    	font-weight:bold;
    	padding:0px 5px 0px 5px;
    }
    #divError
    {   
    	width:100%;	    	
    	height:10px;
    	text-align:center;
    	padding:5px 5px 5px 5px;
    }
    .hightlight
    {
    	background-color:#EAFAFF;color:#003366;
    }
    .nohightlight
    {
    	background-color:White;
    }
    
    .style6
    {
        width: 265px;
    }
    
</style>
<link href="widgets/SEMSContractList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractList/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/SEMSContractList/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/SEMSContractList/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/SEMSContractList/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractList/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractList/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/rptAllReport/Images/report.png" style="width: 40px; height: 32px; margin-bottom:10px;" align="mirpte" /> 
    <%=Resources.labels.thamsobaocao %>

</div>
<div style="padding-top:50px;padding-left:100px;">
<asp:Panel runat="server" DefaultButton="btnViewReport">
<table width="800px" style="border:solid 1px #DFDFDF; background-color:#DFDFDF" cellpadding="10px">
    <tr >
                                <td class="thtds">
                                    <asp:Label ID="Label5" Font-Bold="True" runat="server" 
                                        Text='<%$ Resources:labels, sogiaodich %>'></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="rptTranID" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    
                                    <asp:Label ID="Label14" Font-Bold="True" runat="server" Text="<%$ Resources:labels, duyetboi %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    
                                    <asp:TextBox ID="RPTUSERAPP" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                <asp:Button ID="btnViewReport" runat="server" Text="<%$ Resources:labels, timkiem %>" 
                                        onclick="btnViewReport_Click" />
                                </td>
                            </tr>
                                                        <tr >
                                <td class="thtds">
                                    
                                    <asp:Label ID="Label4" Font-Bold="true" runat="server" Text="<%$ Resources:labels, debitaccount %>"></asp:Label>
                                    
                                </td>
                                <td class="thtds">
                                    
                                    <asp:TextBox ID="RPTACCOUNT" runat="server"></asp:TextBox>
                                    
                                </td>
                                <td class="thtds">
                                    <asp:Label ID="Label13" Font-Bold="true" runat="server" 
                                        Text="<%$ Resources:labels, taikhoanbaoco %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="RPTCREACCT" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    
                                    &nbsp;</td>
                            </tr>
                                                        <tr >
                                <td class="thtds">
                                    <asp:Label ID="Label10" Font-Bold="true" runat="server" Text="<%$ Resources:labels, customercodecore %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="RPTCUSTCODECORE" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    <asp:Label ID="Label16" Font-Bold="true" runat="server" Text="<%$ Resources:labels, custtype %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:DropDownList ID="rptCustType" runat="server">
                                        <asp:ListItem Value="" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
                                        <asp:ListItem Value="P" Text="<%$ Resources:labels, canhan %>"></asp:ListItem>
                                        <asp:ListItem Value="O" Text="<%$ Resources:labels, doanhnghiep %>"></asp:ListItem>
                                    </asp:DropDownList>
                                                            </td>
                                <td class="thtds">
                                    
                                </td>
                            </tr>
                             <tr >
                                <td class="thtds">
                                    <asp:Label ID="Label11" Font-Bold="true" runat="server" Text="<%$ Resources:labels, customername %>"></asp:Label>
                                </td>
                                <td class="thtds" colspan="4">
                                    <asp:TextBox ID="rptcustname" runat="server" Width="283px" 
                                        SkinID="txtTwoColumn"></asp:TextBox>
                                </td>
                            </tr> 
                             <tr >
                                <td class="thtds">
                                    <asp:Label ID="Label12" Font-Bold="true" runat="server" Text="<%$ Resources:labels, contractno %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="rptcontractno" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                                                              <asp:Label ID="Label15" Font-Bold="True" runat="server" 
                                        Text='<%$ Resources:labels, socmnd %>'></asp:Label>                             
                                   
                                </td>
                                <td class="thtds">
                                     
                                      <asp:TextBox ID="RPTLICENSEID" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    
                                    
                                    
                                </td>
                            </tr> 
                            <tr >
                                <td class="thtds">
                                    <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="<%$ Resources:labels, tungay %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="rptFromDate" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="<%$ Resources:labels, denngay %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="rptToDate" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    
                                    
                                    
                                </td>
                            </tr>
                                                    
                            <tr >
                                <td class="thtds">
                                    
                                    <asp:Label ID="Label8" Font-Bold="True" runat="server" Text='<%$ Resources:labels, trangthai %>'></asp:Label>
                                    
                                </td>
                                <td class="thtds">
                                    
                                    <asp:DropDownList ID="rptStatus" runat="server">
                                        <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="<%$ Resources:labels, dangxuly %>"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="<%$ Resources:labels, hoanthanh %>"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="<%$ Resources:labels, loi %>"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="<%$ Resources:labels, choduyet %>"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="<%$ Resources:labels, huy %>"></asp:ListItem>
                                    </asp:DropDownList>
                                    
                                </td>
                                <td class="thtds">                                
                                 <asp:Label ID="Label9" Font-Bold="True" runat="server" 
                                                                        Text="<%$ Resources:labels, malo %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                      <asp:TextBox ID="rptBatchRef" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    <asp:CheckBox ID="RPTISDELETED" runat="server" Font-Bold="True" Text="<%$ Resources:labels, daxoa %>" 
                                        Visible="False" />   
                                </td>

                            </tr>
                                                    
                            <tr >
                                <td class="thtds">
                                    
                                    <asp:Label ID="Label7" Font-Bold="True" runat="server" 
                                        Text='<%$ Resources:labels, loaigiaodich %>'></asp:Label>
                                    
                                </td>
                                <td class="thtds" colspan="2">
                                    
                                    <asp:DropDownList ID="rptTrancode" runat="server" SkinID="exttxt1" AutoPostBack ="true"  
                                        onselectedindexchanged="rptTrancode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    
                                </td>
                                <td class="thtds">
                                    
                                         <asp:CheckBox ID="CBXSCD" runat="server" CssClass="nothing" Font-Bold="True" Visible ="true" 
                                     Text="<%$ Resources:labels, giaodichlich %>" />
                                    
                                 
                                </td>
                                <td class="thtds">
                                    
                                    
                                    
                                    &nbsp;</td>
                            </tr>
                            
                            <tr >
                                <td class="thtds">
                                   <asp:CheckBox ID="rptIsBatch" runat="server" Font-Bold="True" Visible ="false" 
                                        Text="<%$ Resources:labels, giaodichlo %>" />
                                    
                               
                                </td>
                                <td class="thtds" colspan="2">
                                    
                                    <asp:Label ID="Label1" Font-Bold="True" runat="server" Text="<%$ Resources:labels, ketqua %>" 
                                        Visible="False"></asp:Label>
                                    
                                    
                                    
                                    <asp:DropDownList ID="RPTAPPRSTS" runat="server" Visible="False">
                                        <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
<%--                                        <asp:ListItem Value="0" Text="<%$ Resources:labels, dangxuly %>"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="<%$ Resources:labels, choduyet %>"></asp:ListItem>                    
                                        <asp:ListItem Value="3" Text="<%$ Resources:labels, duyet %>"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="<%$ Resources:labels, hoantien %>"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="<%$ Resources:labels, huy %>"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    
                                </td>
                                <td class="thtds">
                                    <asp:HiddenField ID = "rptimper" runat="server" />
                                    <asp:HiddenField ID = "RPTUSERID" runat="server" Value="" />
                                    <asp:HiddenField ID = "RPTCHECKNO" runat="server" Value="" />
                                    <asp:HiddenField ID = "rptIsSchedule" runat="server" Value="" />
                                    </td>
                                <td class="thtds">
                                    </td>
                            </tr>
                    
</table>
</asp:Panel>
</div>
            <script type="text/javascript">
//<![CDATA[

      var cal = Calendar.setup({
          onSelect: function(cal) { cal.hide() }
      });
      cal.manageFields("<%=rptFromDate.ClientID %>", "<%=rptFromDate.ClientID %>", "%d/%m/%Y");      
      cal.manageFields("<%=rptToDate.ClientID %>", "<%=rptToDate.ClientID %>", "%d/%m/%Y");      
    //]]></script>