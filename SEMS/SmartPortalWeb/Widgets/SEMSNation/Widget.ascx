<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSNation_Widget" %>
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
    #divNationHeader
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
<link href="widgets/SEMSNation/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/SEMSNation/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/SEMSNation/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/SEMSNation/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSNation/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSNation/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSNation/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<br />
<div id="divNationHeader">
    <img alt="" src="widgets/SEMSNation/Images/nation.png" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" />
     <%=Resources.labels.danhsachquocgia %>

</div>
<div id="divError">
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
<ProgressTemplate>
<img alt="" src="widgets/SEMSNation/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
</ProgressTemplate>
</asp:UpdateProgress>
</div>
<div id="divSearch">
<asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
    <table class="style1">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, maquocgia %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, tenquocgia %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
            <td width="10%">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, timkiem %>" />
            </td>
        </tr>
    </table>
</asp:Panel>
</div>
<div id="divToolbar">
    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:labels, themmoi %>" />
&nbsp;
    <asp:Button ID="Button2" runat="server" Text="<%$ Resources:labels, delete %>" Width="73px" />
&nbsp;
    <asp:Button ID="Button3" runat="server" Text="<%$ Resources:labels, exporttofile %>" OnClientClick="showPopWin('http://v-index.com', 400, 200, null);return false;" />
</div>
<div>
<div id="divLetter">
    <uc1:LetterSearch ID="LetterSearch1" runat="server" />
</div>
<div id="divResult">
    <asp:GridView ID="gvNationList" runat="server" BackColor="White" 
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Width="100%" AllowPaging="True" AutoGenerateColumns="False" 
        onrowdatabound="gvNationList_RowDataBound" PageSize="15" 
        onpageindexchanging="gvNationList_PageIndexChanging" 
        onsorting="gvNationList_Sorting" AllowSorting="True">
        <RowStyle ForeColor="#000000" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbxSelect" runat="server" />                   
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeaderCB" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, maquocgia %>" SortExpression="Maquocgia">
                <ItemTemplate>
                    <asp:Label ID="lblMaquocgia" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="left" />
                 <HeaderStyle HorizontalAlign="left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, tenquocgia %>">
                <ItemTemplate>
                    <asp:Label ID="lblTenquocgia" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, thudo %>">
                <ItemTemplate>
                    <asp:Label ID="lblTenthudo" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, edit %>">
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
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" CssClass="pager" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#E0ECFF" Font-Bold="True" ForeColor="Black" />
    </asp:GridView>
</div>
</div>

</ContentTemplate>
</asp:UpdatePanel>
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