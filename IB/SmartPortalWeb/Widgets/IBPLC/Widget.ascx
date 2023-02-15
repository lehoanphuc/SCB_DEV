<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBPLC_Widget" %>

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
    	border-bottom:solid 1px #b9bfc1;
    	
    }
    .thtdf
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    	border-top:solid 1px #b9bfc1;
    }
    .thtdff
    {  	
    	
    	border-left:solid 1px #b9bfc1;
    	border-top:solid 1px #b9bfc1;
    	border-bottom:solid 1px #b9bfc1;
    }
    .thtdfff
    {  	
    	
    	border-left:solid 1px #b9bfc1;
    	border-top:solid 1px #b9bfc1;
    	border-bottom:solid 1px #b9bfc1;
    	border-right:solid 1px #b9bfc1;
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
</style>
                      
<div style="width:100%;">
    <asp:Literal ID="ltrTH" runat="server"></asp:Literal><br />
    <asp:Literal ID="ltrL" runat="server"></asp:Literal>
    <div style="padding-left:5px; margin-top:10px; text-align:center">
            <asp:Button ID="Button3" runat="server" Text="Quay lại" onclick="Button3_Click1" 
                />
    </div>
</div>
                   