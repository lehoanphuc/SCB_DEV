<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCustomerListCorp_Widget" %>
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
    	padding:0px 0px 0px 0px;
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
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="widgets/SEMSCustomerList/js/common.js"> </script> 
<script type="text/javascript" src="widgets/SEMSCustomerList/js/subModal.js"> </script> 

<link href="widgets/SEMSCustomerList/css/style.css" rel="stylesheet" type="text/css"> 
<!-- Add this to have a specific theme--> 
<link href="widgets/SEMSCustomerList/css/subModal.css" rel="stylesheet" type="text/css"> 




<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSCustomerList/Images/messenger.png" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" /> 
<%=Resources.labels.danhsachkhachhang %>

</div>
<div id="divError">
<asp:Label ID="lblError" runat="server"></asp:Label>
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
<ProgressTemplate>
<img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" /> <%=Resources.labels.loading %>
</ProgressTemplate>
</asp:UpdateProgress>
</div>

<div id="divSearch">
<asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
    <table class="style1">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, makhachhang %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCustCode" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, tenkhachhang %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFullName" runat="server"></asp:TextBox>
            </td>
            <td width="10%">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, xemchitiet %>" 
                    onclick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, sochungminh %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtLicenseID" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblct" runat="server" Text="<%$ Resources:labels, loaikhachhang %>"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCustType" runat="server">
                    <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>" ></asp:ListItem>
                    <asp:ListItem Value="P" Text="<%$ Resources:labels, canhan %>" ></asp:ListItem>
                    <asp:ListItem Value="O" Text="<%$ Resources:labels, doanhnghiep %>" ></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <%--<asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>--%>
            </td>
            <td>
                <%--<asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>" ></asp:ListItem>
                    <asp:ListItem Value="N">Mới</asp:ListItem>
                    <asp:ListItem Value="A">Hoạt động</asp:ListItem>
                    <asp:ListItem Value="D">Xóa</asp:ListItem>
                </asp:DropDownList>--%>
            </td>
            <td>
                &nbsp;</td>
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
    <%--<asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:labels, delete %>" Width="73px" onclick="btnDelete_Click"  />
&nbsp;--%>
    <asp:Button ID="Button3" runat="server" Text="<%$ Resources:labels, exporttofile %>" OnClientClick="showPopWin('widgets/SEMSCustomerList/ExportToFile.aspx', 170, 160, null);setTitle(this);return false;" />
</div>
<div id="divLetter">
    <uc1:LetterSearch ID="LetterSearch" runat="server" />
</div>
<div id="divResult">
    <asp:GridView ID="gvCustomerList" runat="server" BackColor="White" 
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Width="100%" AllowPaging="True" AutoGenerateColumns="False" 
        onrowdatabound="gvCustomerList_RowDataBound" PageSize="15" 
        onpageindexchanging="gvCustomerList_PageIndexChanging" 
        onsorting="gvCustomerList_Sorting" AllowSorting="True">
        <RowStyle ForeColor="#000000" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbxSelect" runat="server" />                   
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, makhachhang %>" SortExpression="CUSTID">
                <ItemTemplate>
                    <asp:HyperLink ID="lblCustCode" runat="server"></asp:HyperLink>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, tenkhachhang %>" SortExpression="FULLNAME">
                <ItemTemplate>
                    <asp:Label ID="lblCustName" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, phone %>" SortExpression="TEL">
                <ItemTemplate>
                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, sochungminh %>" SortExpression="LICENSEID">
                <ItemTemplate>
                    <asp:Label ID="lblIdentify" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, loaikhachhang %>" SortExpression="CFTYPE">
                <ItemTemplate>
                    <asp:Label ID="lblCustType" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="<%$ Resources:labels, edit %>">
                <ItemTemplate>
                    <asp:HyperLink ID="hpEdit" runat="server">[hpEdit]</asp:HyperLink>
                </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Center" />
                 <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                <ItemTemplate>
                    <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>--%>
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
    
    
</script>

