<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSFDProduct_Widget" %>
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
    .style3
    {
        width: 300px;
    }
    .style4
    {
        width: 316px;
    }
    .style5
    {
        width: 219px;
    }
    .style6
    {
        width: 280px;
    }
</style>
<link href="widgets/SEMSDistrict/CSS/css.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<br />
<div id="divProvinceHeader">
    <img alt="" src="widgets/SEMSProvince/Images/branch.png" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" /> 
  <%=Resources.labels.danhsachsanphamTKOnline %>
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<br />

<div id="divError">
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
<ProgressTemplate>
<img alt="" src="widgets/SEMSProvince/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
</ProgressTemplate>
</asp:UpdateProgress>
</div>
<div id="divSearch">
<asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
    <table class="style1">
        <tr>
<%--            <td>
                <asp:Label ID="Label1" runat="server" Text="Mã quận/huyện"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="txtDistCode" runat="server"></asp:TextBox>
            </td>--%>
            <td class="style5">
                <asp:Label ID="FDProductID" runat="server" Text="<%$ Resources:labels, sptkonlineid %>"></asp:Label>
            </td>
            <td class="style3">
                <asp:TextBox ID="txtFDProductID" runat="server"></asp:TextBox>
            </td>
            <td class="style6">
                <asp:Label ID="FDProductName" runat="server" Text="<%$ Resources:labels, sptkonlinename %>"></asp:Label>
            </td>
            <td class="style4">
                <asp:TextBox ID="txtFDProductName" runat="server"></asp:TextBox>
            </td>
            <td width="10%">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, timkiem %>" 
                    onclick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
<%--            <td>
                <asp:Label ID="Label1" runat="server" Text="Mã quận/huyện"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="txtDistCode" runat="server"></asp:TextBox>
            </td>--%>
            <td class="style5">
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, kyhan %>"></asp:Label>
            </td>
            <td class="style3">
                <asp:TextBox ID="txtTerm" runat="server"></asp:TextBox>
            </td>
            <td class="style6">
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, laisuat %>"></asp:Label>
            </td>
            <td class="style4">
                <asp:TextBox ID="txtInterestRate" runat="server"></asp:TextBox>
            </td>
            
        </tr>
         <tr>

            <td class="style5">
                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, ghichu %>"></asp:Label>
            </td>
            <td class="style3">
                <asp:TextBox ID="txtNote" runat="server"></asp:TextBox>
            </td>
            <td class="style6">
                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, diengiai %>"></asp:Label>
            </td>
            <td class="style4">
                <asp:TextBox ID="txtDesc" runat="server"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
<%--            <td>
                <asp:Label ID="Label1" runat="server" Text="Mã quận/huyện"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="txtDistCode" runat="server"></asp:TextBox>
            </td>--%>
            <td class="style5" colspan="2">                       
            
                <asp:CheckBox Text="<%$ Resources:labels, tattoantruochan %>" runat="server" ID="cbIsClose" />
            </td>
            
        </tr>
    </table>
</asp:Panel>
</div>
<div id="divToolbar">
    <asp:Button ID="btn_Add" runat="server" Text="<%$ Resources:labels, themmoi %>" style="height: 26px" 
        onclick="btn_Add_Click" />
&nbsp;
    <asp:Button ID="btn_Del" runat="server" Text="<%$ Resources:labels, delete %>" Width="73px" 
        onclick="btn_Del_Click" />
    &nbsp;</div>
<div>

<div id="divResult">
    <asp:GridView ID="gvFDProductList" runat="server" BackColor="White" 
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Width="100%" AllowPaging="True" AutoGenerateColumns="False" 
        onrowdatabound="gvFDProductList_RowDataBound" 
        onpageindexchanging="gvFDProductList_PageIndexChanging" PageSize="15" > 
        <RowStyle ForeColor="#000000" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbxSelect" runat="server" />                   
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeaderCB" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, sptkonlineid %>">
                <ItemTemplate>
                    <asp:Label ID="lblFDProductID" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="left" />
                 <HeaderStyle HorizontalAlign="left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, sptkonlinename %>" Visible="true">
                <ItemTemplate>
                    <asp:Label ID="lblFDProductName" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, kyhan %>">
                <ItemTemplate>
                        <asp:Label ID="lblTerm" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, laisuat %>">
                <ItemTemplate>
                        <asp:Label ID="lblInterestrate" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, ghichu %>">
                <ItemTemplate>
                        <asp:Label ID="lblNote" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="<%$ Resources:labels, diengiai %>">
                <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server"></asp:Label>
                </ItemTemplate>
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
</div>
</div>

</ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    function SelectCbx(obj) {
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != '') {
                    elements[i].checked = true;
                    //elements[i].parentNode.parentNode.className="hightlight";
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != '') {
                    elements[i].checked = false;
                    //elements[i].parentNode.parentNode.className="nohightlight";
                }
            }
        }
    }

    function HighLightCBX(obj, obj1) {
        //var obj2=document.getElementById(obj1);
        if (obj1.checked) {
            document.getElementById(obj).className = "hightlight";
        }
        else {
            document.getElementById(obj).className = "nohightlight";
        }
    }
    
    
</script>