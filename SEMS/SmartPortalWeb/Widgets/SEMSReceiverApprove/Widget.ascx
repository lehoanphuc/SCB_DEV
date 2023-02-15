<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSReceiverApprove_Widget" %>
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
</style>

<link href="widgets/SEMSContractApprove/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/SEMSContractApprove/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/SEMSContractApprove/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/SEMSContractApprove/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractApprove/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractApprove/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSReceiverApprove/Images/approveReceiver.png" style="width: 40px; height: 32px; margin-bottom:10px;" align="middle" /> 
    <%=Resources.labels.duyetnguoithuhuong %>

</div>
<div id="divError">
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" runat="server">
<ProgressTemplate>
<img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" /> <%=Resources.labels.loading %>
</ProgressTemplate>
</asp:UpdateProgress>
<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</div>

<div id="divSearch">
    <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, nguoithuhuong %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtreceivername" runat="server"></asp:TextBox>
                </td>
                 <td>
                <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, sotaikhoan %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAcctno" runat="server"></asp:TextBox>
            </td>
                <td width="10%">
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, timkiem %>" 
                    onclick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>


<div>
<asp:Panel ID="pnbutton" runat="server">
    <div id="divToolbar">
        <asp:Button ID="btnApprove" runat="server" Text="<%$ Resources:labels, duyet %>" onclick="btnApprove_Click" 
        Width="72px" />
&nbsp;
        <asp:Button ID="btnReject" runat="server" Text="<%$ Resources:labels, khongduyet %>" Width="88px"
        onclick="btnReject_Click" />
&nbsp;

    </div>
    </asp:Panel>
     <%--   <asp:Button ID="Button3" runat="server" Text="<%$ Resources:labels, exporttofile %>" />--%>
<%--<div id="divLetter">
    <uc1:LetterSearch ID="LetterSearch1" runat="server" />
</div>--%>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div id="divResult">
    <asp:Label ID="lblAlert" runat="server" ForeColor="Red"></asp:Label>
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
                <asp:TemplateField HeaderText="<%$ Resources:labels, manguoithuhuong %>" Visible="false" SortExpression="ID">
                    <ItemTemplate>
                        <asp:HyperLink ID="hpID" runat="server" Visible="false" >[hpDetails]</asp:HyperLink>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, nguoithuhuong %>" SortExpression="RECEIVERNAME">
                    <ItemTemplate>
                        <asp:HyperLink ID="hpReceiver" runat="server">[receiver]</asp:HyperLink>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, taikhoan %>" SortExpression="ACCTNO">
                    <ItemTemplate>
                        <asp:Label ID="lblAcctno" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, giaodich %>" SortExpression="PAGENAME">
                    <ItemTemplate>
                        <asp:Label ID="lblTransType" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
               <asp:TemplateField HeaderText="<%$ Resources:labels, khachhang %>" SortExpression="FULLNAME">
                    <ItemTemplate>
                        <asp:Label ID="lblFullName" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, mota %>" SortExpression="DESCRIPTION">
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>" SortExpression="STATUS">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle CssClass="gvFooterStyle" />
            <PagerStyle CssClass="pager" HorizontalAlign="Center" />
            <SelectedRowStyle />
            <HeaderStyle CssClass="gvHeader" />
        </asp:GridView>
        <br />
    <asp:Literal ID="litPager" runat="server"></asp:Literal>
    </div>
</div>
</ContentTemplate>
</asp:UpdatePanel>

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