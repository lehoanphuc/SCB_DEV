<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSFeedBack_Delete_Widget" %>
<style type="text/css">
    .style1
    {
        width: 100%;
    }
    .divGetInfoCust
   {
   	    background-color:#F8F8F8;
    	border:solid 1px #B9BFC1;
    	margin:2px 5px 5px 5px;
    	padding:0px 0px 0px 0px;
    	
    	
   }
   .divHeaderStyle
   {
   	    background-color:#EAFAFF;color:#003366;   	    	   
   	    font-weight:bold;
   	    margin:0px;
   	    line-height:20px;
   	    padding:5px;
   }
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
    	margin:20px 5px 5px 5px;
    	padding:5px 5px 5px 5px;
    }
     #divLetter
    {
    	background-color:#F8F8F8;
    	border:solid 1px #B9BFC1;
    	margin:20px 5px 5px 5px;
    	padding:5px 5px 5px 5px;
    }
    #divResult
    {
    	
    	margin:20px 5px 5px 5px;
    	padding:0px 5px 5px 5px;
    }
    .gvHeader
    {
    	text-align:left;
    }
    .gvHeaderCB
    {
    	width:3%;
    	text-align:left;
    }
       #divDate
    {
    	text-align:right;
    	padding-right:10px;
    	font-weight:bold;
    }
    #divProvinceHeader
    {   
    	width:100%;	
    	font-weight:bold;
    	padding:5px 5px 5px 5px;
    }
    #divError
    {   
    	width:100%;	
    	font-weight:bold;
    	height:10px;
    	text-align:center;
    	padding:0px 5px 5px 5px;
    }
    .hightlight
    {
    	background-color:#EAFAFF;color:#003366;
    }
    .nohightlight
    {
    	background-color:White;
    }
     .style3
   {    	   
   	    font-weight:bold;

   }
</style>

<link href="widgets/SEMSProvince/CSS/css.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSProvince/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/SEMSProvince/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/SEMSProvince/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSProvince/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSProvince/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSProvince/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<div id="divProvinceHeader">
    <img alt="" src="widgets/SEMSProvince/Images/branch.png" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" /><%=Resources.labels.xoatinhthanh %>
</div>


<asp:Panel runat="server" ID="pnDel">
<div class="divGetInfoCust">
    <div class="divHeaderStyle">
      <%=Resources.labels.thongtinxacnhan %>
    </div>
    <table class="style1" cellspacing="0" cellpadding="5">
        <tr>
            <td align="center" class="style2">
                <asp:Label ID="lblConfirm" runat="server" 
                    Text="<%$ Resources:labels, banchacchanmuonxoatinhthanhkhong %>"></asp:Label>
            </td>
           
        </tr>
        </table>
</div>
</asp:Panel>
<asp:Panel runat="server" ID="pnresult">
<div class="divGetInfoCust">
    <div class="divHeaderStyle">
      <%=Resources.labels.thongtinketqua %>
    </div>
    <table class="style1" cellspacing="0" cellpadding="5">
        <tr>
            <td>

                <div id="divError">
                <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
                </div>
            </td>
           
        </tr>
        </table>
</div>
</asp:Panel>
<div style="text-align:center; padding-top:10px;">
    <asp:Button ID="btsaveandcont" runat="server" onclick="btsaveandcont_Click" 
        Text="<%$ Resources:labels, xoa %>" Width="71px" />
&nbsp;
    <asp:Button ID="btback" runat="server" 
        Text="<%$ Resources:labels, quaylai %>" onclick="btback_Click" />
    &nbsp; &nbsp;
    </div>
