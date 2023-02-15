<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSSwiftApprove_Widget" %>
<%@ Register src="../../../Controls/LetterSearch/LetterSearch.ascx" tagname="LetterSearch" tagprefix="uc1" %>
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
    	text-align:right;
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
    	padding:5px 5px 5px 5px;
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
</style>
<link href="widgets/SEMSContractApprove/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractApprove/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/SEMSContractApprove/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/SEMSContractApprove/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/SEMSContractApprove/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractApprove/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractApprove/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%>
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSTransactionsApprove/Images/icon_transactions.jpg" style="width: 40px; height: 32px; margin-bottom:10px;" align="middle" /> 
    TẠO ĐIỆN

    CHUYỂN TIỀN NGOÀI HỆ THỐNG

</div>
<div id="divError">
<%--<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" runat="server">
<ProgressTemplate>
<img alt="" src="widgets/SEMScontractList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
</ProgressTemplate>
</asp:UpdateProgress>--%>
<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</div>

<div id="divSearch">
    <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Mã giao dịch"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTranID" runat="server"></asp:TextBox>
                </td>
                 <td>
                     <asp:Label ID="Label5" runat="server" Text="Ngân hàng nhận"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlBank" runat="server" SkinID="extDDL1">
                </asp:DropDownList>
                </td>
                <td width="10%">
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, xemchitiet %>" 
                    onclick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, tungay %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, denngay %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                </td>

                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
</div>

<div>
<asp:Panel ID="pnbutton" runat="server">
    <div id="divToolbar">
        <asp:Button ID="btnApprove" runat="server" Text="Tạo điện" onclick="btnApprove_Click" 
        Width="72px" />
        </div>
    </asp:Panel>
     <%--   <asp:Button ID="Button3" runat="server" Text="<%$ Resources:labels, exporttofile %>" />--%>
<%--<div id="divLetter">
    <uc1:LetterSearch ID="LetterSearch1" runat="server" />
</div>--%>
    <div id="divResult">
        <asp:GridView ID="gvcontractList" runat="server" BackColor="White" 
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Width="100%" AllowPaging="True" AutoGenerateColumns="False" 
        onrowdatabound="gvcontractList_RowDataBound" PageSize="15" 
        onpageindexchanging="gvcontractList_PageIndexChanging" 
        onsorting="gvcontractList_Sorting" AllowSorting="True">
            <RowStyle ForeColor="#000000" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbxSelect" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mã giao dịch">
                    <ItemTemplate>
                        <asp:HyperLink ID="hpTranID" runat="server">[hpDetails]</asp:HyperLink>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ngày giao dịch">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, sotien %>">
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ngân hàng nhận">
                    <ItemTemplate>
                        <asp:Label ID="lblBank" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tỉnh thành">
                    <ItemTemplate>
                        <asp:Label ID="lblCity" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" CssClass="pager" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#E0ECFF" Font-Bold="True" ForeColor="Black" />
        </asp:GridView>
        <br />
    <asp:Literal ID="litPager" runat="server"></asp:Literal>
    </div>
</div>

<%--</ContentTemplate>
</asp:UpdatePanel>--%>
<script type="text/javascript">//<![CDATA[

      var cal = Calendar.setup({
          onSelect: function(cal) { cal.hide() }
      });
      cal.manageFields("<%=txtFrom.ClientID %>", "<%=txtFrom.ClientID %>", "%d/%m/%Y");      
      cal.manageFields("<%=txtTo.ClientID %>", "<%=txtTo.ClientID %>", "%d/%m/%Y");      
    //]]></script>
<script>
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
    
    function pop(obj)
    {
        if(window.confirm(obj))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
</script>