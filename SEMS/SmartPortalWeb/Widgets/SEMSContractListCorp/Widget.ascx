<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContractListCorp_Widget" %>
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
<link href="widgets/SEMSContractList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractList/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/SEMSContractList/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/SEMSContractList/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/SEMSContractList/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractList/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractList/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSContractList/Images/handshake.png" style="width: 40px; height: 32px; margin-bottom:10px;" align="middle" /> 
    <%=Resources.labels.danhsachhopdong %>

</div>
<div id="divError">
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" runat="server">
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
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, mahopdong %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtContractCode" runat="server"></asp:TextBox>
            </td>
             <td>
                <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, tenkhachhang %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCustName" runat="server"></asp:TextBox>
            </td>
            <td>
               
            </td>
            <td width="10%">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, xemchitiet %>" 
                    onclick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, ngaymo %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtOpenDate" runat="server"></asp:TextBox>
            </td>
             <td>
                 <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, ngayhethan %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
            </td>

        </tr>
        <tr>

            <td>
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, nguoimo %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtopenPer" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, loaihopdong %>"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlContractType" runat="server">
                    <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>" ></asp:ListItem>
                    <asp:ListItem Value="PCO" Text="<%$ Resources:labels, canhan %>" ></asp:ListItem>
                    <asp:ListItem Value="CCO" Text="<%$ Resources:labels, doanhnghiep %>" ></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList2" runat="server">
                    <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>" ></asp:ListItem>
                    <asp:ListItem Value="N" Text="<%$ Resources:labels, moi %>"></asp:ListItem>
                    <asp:ListItem Value="A" Text="<%$ Resources:labels, hoatdong %>"></asp:ListItem>
                    <asp:ListItem Value="D" Text="<%$ Resources:labels, xoa %>"></asp:ListItem>
                </asp:DropDownList>
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
    <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:labels, themmoi %>" onclick="btnAdd_Click" />
&nbsp;
    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:labels, donghopdong %>" Width="109px" 
        onclick="btnDelete_Click" />
&nbsp;
    <asp:Button ID="Button3" runat="server" Text="<%$ Resources:labels, exporttofile %>" />
</div>
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
            <asp:TemplateField HeaderText="<%$ Resources:labels, mahopdong %>" SortExpression="CONTRACTNO">
                <ItemTemplate>
                    <asp:HyperLink ID="hpcontractCode" runat="server">[hpDetails]</asp:HyperLink>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, khachhang %>" SortExpression="FULLNAME">
                <ItemTemplate>
                    <asp:Label ID="lblcustName" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, nguoimo %>" SortExpression="USERCREATE">
                <ItemTemplate>
                    <asp:Label ID="lblOpen" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, ngaymo %>" SortExpression="CREATEDATE">
                <ItemTemplate>
                    <asp:Label ID="lblOpendate" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, ngayhethan %>" SortExpression="ENDDATE">
                <ItemTemplate>
                    <asp:Label ID="lblClosedate" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, loaihopdong %>" SortExpression="USERTYPE">
                <ItemTemplate>
                    <asp:Label ID="lblContractType" runat="server"></asp:Label>
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
            
            <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                <ItemTemplate>
                    <asp:HyperLink ID="hpEdit" runat="server">[hpEdit]</asp:HyperLink>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                 <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, close %>">
                <ItemTemplate>
                    <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                 <ItemStyle HorizontalAlign="Center" />
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


<script type="text/javascript">//<![CDATA[

      var cal = Calendar.setup({
          onSelect: function(cal) { cal.hide() }
      });
      cal.manageFields("<%=txtOpenDate.ClientID %>", "<%=txtOpenDate.ClientID %>", "%d/%m/%Y");      
      cal.manageFields("<%=txtEndDate.ClientID %>", "<%=txtEndDate.ClientID %>", "%d/%m/%Y");      
    //]]></script>
</ContentTemplate>
</asp:UpdatePanel>


<script>
function SelectCbx(obj) {
    var count = document.getElementById('aspnetForm').elements.length;
    var elements = document.getElementById('aspnetForm').elements;
    if (obj.checked) {
        for (i = 0; i < count; i++) {
            if (elements[i].type == 'checkbox' &&elements[i].disabled!=true&& elements[i].id != 'ctl00_ContentPlaceHolder_Content_ctl00_ctl00_ctl00_gvCustomerView_ctl01_cbxSelectAll') {
                elements[i].checked = true;
                //elements[i].parentNode.parentNode.className = "hightlight";
            }
        }

    } else {
        for (i = 0; i < count; i++) {
            if (elements[i].type == 'checkbox' && elements[i].id != 'ctl00_ContentPlaceHolder_Content_ctl00_ctl00_ctl00_gvCustomerView_ctl01_cbxSelectAll') {
                elements[i].checked = false;
                //elements[i].parentNode.parentNode.className = "nohightlight";
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

function checkColor(obj, obj1) {
    var obj2 = document.getElementById(obj);
    if (obj2.checked) {
        obj1.className = "hightlight";
    }
    else {
        obj1.className = "nohightlight";
    }
}
</script>