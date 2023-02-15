<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_ActiveEBAcct_Widget" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register src="../../Controls/LetterSearch/LetterSearch.ascx" tagname="LetterSearch" tagprefix="uc1" %>
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
       #divDate
    {
    	text-align:right;
    	padding-right:10px;
    	font-weight:bold;
    }
    #divExchangeHeader
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
    .style2
    {
        height: 26px;
    }
</style>
<link href="widgets/SEMSContractList/CSS/css.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSContractList/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/SEMSContractList/JS/lang/en.js" type="text/javascript"></script>
<link href="widgets/SEMSContractList/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractList/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractList/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<br />
<div id="divExchangeHeader">
    <img alt="" src="widgets/SEMSExchangeRate/Images/exhangerate.png" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" /> DANH 
    SÁCH TÀI KHOẢN CHƯA ĐƯỢC ĐĂNG KÝ EBANKING

</div>
<br/>
<div id="divSearch">
<asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
    <table class="style1">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, contractno %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txContractNo" runat="server"></asp:TextBox>
            </td>
             
            <td>
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, custcode %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCustID" runat="server"></asp:TextBox>
            </td>
            <td width="10%">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, search %>" 
                    onclick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, customercodecore %>"></asp:Label>
            </td>
           <td class="style2">
                <asp:TextBox ID="txtCustCode" runat="server"></asp:TextBox>
            </td>
            <td class="style2">
                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, accountname %>"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="txtFullName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
         <td class="style2">
                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, CreateDateContract %>"></asp:Label>
            </td>
        <td>
                <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
            </td>                    
        </tr>
    </table>
    <script type="text/javascript">    //<![CDATA[

    var cal = Calendar.setup({
        onSelect: function(cal) { cal.hide() }
    });
    cal.manageFields("<%=txtDate.ClientID %>", "<%=txtDate.ClientID %>", "%d/%m/%Y");
    
    //]]></script>
</asp:Panel>
</div>
<br />
 <div style="height:500px;overflow:auto;">
     
    <asp:GridView ID="gvInactiveAcct" runat="server" BackColor="White" 
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Width="100%" AutoGenerateColumns="False" 
        onrowdatabound="gvInactiveAcct_RowDataBound" PageSize="15" 
        onpageindexchanging="gvInactiveAcct_PageIndexChanging" 
        onsorting="gvInactiveAcct_Sorting" AllowSorting="True">
        <RowStyle ForeColor="#000000" />
        <Columns>
            <asp:TemplateField HeaderText="<%$ Resources:labels, contractno %>" >
                <ItemTemplate>
                    <asp:Label ID="lblContractNo" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                 <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, custcode %>">
                <ItemTemplate>
                    <asp:Label ID="lblCustID" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                 <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, accountnumber %>">
                <ItemTemplate>
                    <asp:Label ID="lblAcctNo" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, accountname %>" SortExpression="Matiente">
                <ItemTemplate>
                    <asp:Label ID="lblFullName" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, customercodecore %>">
                <ItemTemplate>
                    <asp:Label ID="lblCustCode" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, custtype %>">
                <ItemTemplate>
                    <asp:Label ID="lblCFType" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="<%$ Resources:labels, chinhanhphonggiaodich %>">
                <ItemTemplate>
                    <asp:Label ID="lblBranch" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            
        </Columns>
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" CssClass="pager" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#E0ECFF" Font-Bold="True" ForeColor="Black" />
    </asp:GridView>
    </div>


<script>
    
    function HighLightCBX(obj,obj1)
    {   
        //var obj2=document.getElementById(obj1);
        if(obj1.checked)
        {
            document.getElementById(obj).className="hightlight";
        }        
        else
        {
             document.getElementById(obj).className="nohightlight";
        }
    }
    
    
</script>
