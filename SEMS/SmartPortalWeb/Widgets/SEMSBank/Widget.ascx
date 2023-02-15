<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSBank_Widget" %>
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
    	padding:5px 5px 5px 5px;
    }
    .gvHeader
    {
    	text-align:left;
    }
    #divProductHeader
    {   
    	width:100%;	
    	font-weight:bold;
    	padding:5px 5px 5px 5px;
    }
    #divError
    {   
    	width:100%;	
    	
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
<script src="widgets/SEMSProduct/JS/common.js" type="text/javascript"></script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<br />
<div id="divProductHeader">
    <img alt="" src="widgets/SEMSBank/Images/Bank.png" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" /> <%=Resources.labels.danhsachnganhang %>

</div>
<div id="divError">
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
<ProgressTemplate>
<img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" /> <%=Resources.labels.loading %></ProgressTemplate>
</asp:UpdateProgress>
</div>

<div id="divSearch">
<asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
    <table class="style1">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, nganhang %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtbank" runat="server"></asp:TextBox>
            </td>
            <td>
                
            </td>
            <td>
               
            </td>
            <td width="10%">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, timkiem %>" 
                    onclick="btnSearch_Click" />
            </td>
        </tr>

    </table>
</asp:Panel>
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div>
<div id="divToolbar">
    <asp:Button ID="btnAddNew" runat="server" Text="<%$ Resources:labels, themmoi %>" 
        onclick="btnAddNew_Click" />
&nbsp;
    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:labels, delete %>" Width="73px" onclick="btnDelete_Click" />
&nbsp;
    
</div>
<div id="divResult">
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
            <asp:TemplateField HeaderText="" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, nganhang%>" SortExpression="Bankname">
                <ItemTemplate>
                    <asp:HyperLink ID="hpbankname" runat="server"></asp:HyperLink>
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
        <FooterStyle CssClass="gvFooterStyle" />
            <PagerStyle HorizontalAlign="Center" CssClass="pager" />
            <SelectedRowStyle />
            <HeaderStyle CssClass="gvHeader" />
    </asp:GridView>
    <br />
    <asp:Literal ID="litPager" runat="server"></asp:Literal>
</div>

</ContentTemplate>
</asp:UpdatePanel>
<script language="javascript">
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