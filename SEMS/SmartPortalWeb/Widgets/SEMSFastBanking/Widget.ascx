<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSFastBanking_Widget" %>
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
    #divATMHeader
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
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

&nbsp;<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<br />
<div id="divATMHeader">
    <img alt="" src="widgets/SEMSFastBanking/Images/fastbank.png" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" /> <%=Resources.labels.danhsachshop%>

</div>
<div id="divError">
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
<ProgressTemplate>
<img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" /> <%=Resources.labels.loading %>
</ProgressTemplate>
</asp:UpdateProgress>
<asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
</div>

<div id="divSearch">
<asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
    <table class="style1">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, contractno %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtContractNo" runat="server"></asp:TextBox>
            </td>
             
            <td>
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, shopid %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtShopID" runat="server"></asp:TextBox>
            </td>
            <td width="10%">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, search %>" 
                    onclick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, shopcode %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtShopCode" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, shopname %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtShopName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server" Width="130px">
                    <asp:ListItem Value="A">Available</asp:ListItem>
                    <asp:ListItem Value="P">Pending</asp:ListItem>
                    <asp:ListItem Value="D">Deleted</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Panel>
</div>

<div>
<div id="divToolbar">
    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:labels, themmoi %>" 
        onclick="Button1_Click" />
&nbsp;
    <asp:Button ID="btnDelete" runat="server" Visible="false" Text="<%$ Resources:labels, delete %>" Width="73px" 
        onclick="btnDelete_Click" />
&nbsp;
    <asp:Button ID="Button3" runat="server" Visible="false" Text="<%$ Resources:labels, exporttofile %>" />
</div>
<div id="divResult">
    <asp:GridView ID="gvShopList" runat="server" BackColor="White" 
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Width="100%" AllowPaging="True" AutoGenerateColumns="False" 
        onrowdatabound="gvShopList_RowDataBound" PageSize="15" 
        onpageindexchanging="gvShopList_PageIndexChanging" 
        onsorting="gvShopList_Sorting">
        <RowStyle ForeColor="#000000" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbxSelect" runat="server" />                   
                </ItemTemplate>
                <ItemStyle Width="15px" />
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, shopid %>">
                <ItemTemplate>
                    <asp:Label ID="lblShopID" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, contractno %>">
                <ItemTemplate>
                    <asp:Label ID="lblContractNo" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, shopname %>">
                <ItemTemplate>
                    <asp:Label ID="lblShopName" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, shopcode %>">
                <ItemTemplate>
                    <asp:Label ID="lblShopCode" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, nguoimo %>">
                <ItemTemplate>
                    <asp:Label ID="lblUserCreate" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, ngaymo %>">
                <ItemTemplate>
                    <asp:Label ID="lblDateCreate" runat="server"></asp:Label>
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
</div>
</div>

</ContentTemplate>
</asp:UpdatePanel>
<script language="javascript" >
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