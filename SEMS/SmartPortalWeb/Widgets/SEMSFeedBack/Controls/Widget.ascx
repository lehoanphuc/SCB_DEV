<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSFeedBack_Controls_Widget" %>
<style type="text/css">
    .style1
    {
        width: 100%;
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
           .divHeaderStyle
   {
   	    background-color:#EAFAFF;color:#003366;   	    	   
   	    font-weight:bold;
   	    margin:0px;
   	    line-height:20px;
   	    padding:5px;
   }
          .divAddInfoPro
   {
   	    background-color:#F8F8F8;
    	border:solid 1px #B9BFC1;
    	margin:2px 5px 5px 5px;
    	padding:0px 0px 0px 2px;
   }
    .style2
    {
        height: 256px;
    }
</style>


<link href="widgets/SEMSProvince/CSS/css.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSProvince/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/SEMSProvince/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/SEMSProvince/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSProvince/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSProvince/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSProvince/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>
<script src="widgets/SEMSCustomerList/JS/tab-view.js" type="text/javascript"></script>
<script src="widgets/SEMSProduct/JS/common.js" type="text/javascript"></script>


<%--<asp:ScriptManager runat="server" ID="ScriptManager1">
</asp:ScriptManager>--%>
<br />
<div id="divProvinceHeader">
    <img alt="" src="widgets/SEMSFeedBack/Images/Feedback.png" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" />
    <asp:Label ID="lblProvinceHeader" runat="server"></asp:Label>
</div>


<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
<ContentTemplate>
<div id="divError">
<asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
</div>
   
<br />


<div  class="divAddInfoPro" >
<asp:Panel ID="pnResult" runat="server">
<div class="divHeaderStyle">
<%=Resources.labels.ketquathuchien %>
</div>
<div style="text-align:center;font-weight:bold">
<br />
<asp:Label runat="server" ID="lbResult" ForeColor="Red"></asp:Label>
<br />
<br />
</div>

</asp:Panel>
<asp:Panel ID="pnAdd" runat="server">
<div class="divHeaderStyle">
      <%=Resources.labels.thongtintinhthanh %>
    </div>
    <table class="style1" cellpadding="3">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, tieude %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txttitle" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
          
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, noidung %>"></asp:Label>
            </td>
            <td class="style2">
               <textarea id="areacontent" runat="server" rows="15" cols="80"></textarea>
            </td>  
        </tr>
         </table>
        <div class="divHeaderStyle">
            <%=Resources.labels.phanhoitunganhang %>
        </div>
        <table class="style1" cellpadding="9">
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, comment %>"></asp:Label>
            </td>
            <td>
                <textarea id="areacomment" runat="server" rows="15" cols="80"></textarea>
            </td>         
        </tr>
    </table>
</asp:Panel>
</div>

<%--</ContentTemplate>
</asp:UpdatePanel>--%>

<div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="btsave" runat="server"  Text="<%$ Resources:labels, save %>" 
                         onclick="btsave_Click" OnClientClick="return validate();" />
&nbsp;
                         <asp:Button ID="btnPrint" runat="server"  Text="<%$ Resources:labels, inthongtin %> " 
                            onclientclick="javascript:return poponload()"  />&nbsp;
                    <asp:Button ID="btback" runat="server"  Text="<%$ Resources:labels, back %>" 
                        onclick="btback_Click" />
                 &nbsp;
                    </div>
                    
<script type="text/javascript">
	      function poponload()
    {
    testwindow= window.open ("widgets/IBFeedBack/print.aspx?cul="+'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
        "menubar=1,scrollbars=1,width=700,height=650");
    testwindow.moveTo(0,0);
    return false;
    }
      function validate() {
          if (validateEmpty('<%=areacomment.ClientID %>', 'Bạn cần nhập trả lời')) 
             {

              }
              else 
              {
                    document.getElementById('<%=areacomment.ClientID %>').focus();
                    return false;
              }
        }  
</script>
