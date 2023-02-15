<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSExchangeRate_Widget" %>
<%@ Register src="../../Controls/LetterSearch/LetterSearch.ascx" tagname="LetterSearch" tagprefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
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
</style>
<link href="widgets/SEMSExchangeRate/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/SEMSExchangeRate/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/SEMSExchangeRate/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/SEMSExchangeRate/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSExchangeRate/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSExchangeRate/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%>
<br />
<div id="divExchangeHeader">
    <img alt="" src="widgets/SEMSExchangeRate/Images/exhangerate.png" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" />
     <%=Resources.labels.danhsachtygia %>

</div>
<%--<div id="divError">
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
<ProgressTemplate>
<img alt="" src="widgets/SEMSExchangeRate/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
</ProgressTemplate>
</asp:UpdateProgress>
</div>--%>
<div id="divDate">
    <asp:Label ID="lblngay"  runat="server"><%=Resources.labels.ngay %></asp:Label>
    <asp:TextBox ID="txtDate" runat="server" Width="10%"></asp:TextBox>
    <asp:Button ID="BtView" runat="server" Text="Xem" Width="52px" 
        onclick="BtView_Click" />
</div>
<div>
<div id="divResult">
<asp:Label ID="Label1"  runat="server" Font-Bold="true"><%=Resources.labels.donvi %> : <%=Resources.labels.lak %></asp:Label>
<br />
<br />
    <div style="height:500px;overflow:auto;">
    <asp:GridView ID="gvExchangeList" runat="server" BackColor="White" 
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Width="100%" AutoGenerateColumns="False" 
        onrowdatabound="gvExchangeList_RowDataBound" PageSize="15" 
        onpageindexchanging="gvExchangeList_PageIndexChanging" 
        onsorting="gvExchangeList_Sorting" AllowSorting="True">
        <RowStyle ForeColor="#000000" />
        <Columns>
            <asp:TemplateField HeaderText="<%$ Resources:labels, tiente %>" SortExpression="Matiente">
                <ItemTemplate>
                    <asp:Label ID="lblCCYCD" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                 <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, ngay %>">
                <ItemTemplate>
                    <asp:Label ID="lblExchangeDate" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, muachuyenkhoan %>">
                <ItemTemplate>
                    <asp:Label ID="lblTransB" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, banchuyenkhoan %>">
                <ItemTemplate>
                    <asp:Label ID="lblTransS" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, muatienmat %>">
                <ItemTemplate>
                    <asp:Label ID="lblCashB" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, bantienmat %>">
                <ItemTemplate>
                    <asp:Label ID="lblCashS" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>            
        </Columns>
        <FooterStyle CssClass="gvFooterStyle" />
            <PagerStyle HorizontalAlign="Center" CssClass="pager" />
            <SelectedRowStyle />
            <HeaderStyle CssClass="gvHeader" />
    </asp:GridView>
    </div>
</div>
</div>

<%--</ContentTemplate>
</asp:UpdatePanel>--%>
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
<script type="text/javascript">//<![CDATA[

      var cal = Calendar.setup({
          onSelect: function(cal) { cal.hide() }
      });  
      cal.manageFields("<%=txtDate.ClientID %>", "<%=txtDate.ClientID %>", "%d/%m/%Y");      
    //]]></script>