<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSReceiverApprove_ViewDetail_Widget" %>
<%@ Register src="../../../Controls/LetterSearch/LetterSearch.ascx" tagname="LetterSearch" tagprefix="uc1" %>

<link href="widgets/SEMSContractList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script src="widgets/SEMSContractList/JS/ajax.js" type="text/javascript"></script>

<script src="widgets/SEMSContractList/JS/tab-view.js" type="text/javascript"></script>

<style>

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
    .divResult
    {
    	overflow:auto;
    	margin:20px 5px 5px 5px;
    	padding:0px 0px 0px 0px;
    	height:250px;
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
    	width:99%;	
    	font-weight:bold;
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
    .btnGeneral
    {}
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
       .style1
    {
        width: 100%;
    }
</style>
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSReceiverApprove/Images/approveReceiver.png" style="width: 40px; height: 32px; margin-bottom:10px;" align="middle" /> 
    <%=Resources.labels.thongtinnguoithuhuong %>

</div>
<div id="divError">
<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</div>

 <asp:Panel ID="pnConfirm" runat="server">  
<div class="divGetInfoCust">
    <div class="divHeaderStyle">
       <%=Resources.labels.chitietthongtinnguoithuhuong %>
    </div>       
                        <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label88" runat="server" Text="<%$ Resources:labels, loaichuyenkhoan %>"></asp:Label>
                                    <asp:Label ID="lbtemp" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblTranferType" runat="server" Text="<%$ Resources:labels, noibo %>"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, tenkhachhang %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblfullname" runat="server" Text="Trần Anh Tuấn"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label86" runat="server" Text="<%$ Resources:labels, tennguoithuhuong %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblReceiverName" runat="server" Text="Trần Anh Tuấn"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label87" runat="server" Text="<%$ Resources:labels, sotaikhoan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblAcctNo" runat="server"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <%=Resources.labels.cmndmasothue %></td>
                                <td>
                                    <asp:Label ID="lblLicense" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <%=Resources.labels.noicap %></td>
                                <td>
                                    <asp:Label ID="lblIssuePlace" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <%=Resources.labels.ngaycap %></td>
                                <td>
                                    <asp:Label ID="lblIssueDate" runat="server"></asp:Label>
                                </td>
                            </tr>                          
                            <tr>
                                <td class="tibtd">
                                    <%=Resources.labels.ghichu %></td>
                                <td>
                                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>                
                 <!--Button next-->
		<div style="margin-top:10px; text-align:center;">
		
	<asp:Button ID="Button1" runat="server" CssClass="btnGeneral"  Text="<%$ Resources:labels, duyet %>" 
            Width="80px" onclick="btApprove_Click" />	
	&nbsp;	
	<asp:Button ID="Button2" runat="server" CssClass="btnGeneral"  Text="<%$ Resources:labels, khongduyet %>" 
            Width="86px" onclick="btReject_Click1" />	
            &nbsp;	
            <asp:Button ID="Button4" runat="server" CssClass="btnGeneral"  Text="<%$ Resources:labels, back %>" 
                onclick="btnExit_Click" />
	</div>

	</asp:Panel>
 <!--end--> 
 <asp:Panel ID="pnResult" Visible="false" runat="server">
<div class="divGetInfoCust">
    <div class="divHeaderStyle">
      <%=Resources.labels.ketquagiaodich %>
    </div>                  
                    <div class="content">
                      
                        <div style=" padding-top:10px; padding-bottom:10px; text-align:center;">
                      
                        <asp:Label ID="lbresult" runat="server" ForeColor="Red" Font-Bold="true"
                            Text=""></asp:Label>
                        </div>
                        
                      
                    </div> 
                      </div>             
                 <!--Button next-->
                         <div style="text-align:center; margin-top:10px;">
                             <%--onclick="btnNew_Click" --%>
                    &nbsp;
                    <asp:Button ID="Button3" runat="server" 
                         Text="<%$ Resources:labels, thoat %>" onclick="Button3_Click"  /><%--onclick="Button3_Click"--%>
                         </div>

 </asp:Panel>