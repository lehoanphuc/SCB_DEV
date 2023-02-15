<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSFeedBack_Widget" %>
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
</style>
<link href="widgets/SEMSProvince/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/SEMSProvince/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/SEMSProvince/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/SEMSProvince/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSProvince/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSProvince/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<br />
<div id="divProvinceHeader">
    <img alt="" src="widgets/SEMSFeedBack/Images/feedback.png" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" /> 
    <%=Resources.labels.trasoatgiaodich %>
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
<asp:Label runat="server" ID="lblAlert" ForeColor="Red"></asp:Label>
</div>

<div id="divSearch">
<asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
    <table class="style1">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, sogiaodich %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtsgd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
            </td>
            <td>
                                     <asp:DropDownList ID="ddlstatus" runat="server" ><%--SkinID="extDDL1"--%>
                                    <asp:ListItem Value="N" Text="<%$ Resources:labels, chuaxuly %>"></asp:ListItem>
                                     <asp:ListItem Value="Y" Text="<%$ Resources:labels, daxuly %>"></asp:ListItem>
                                    </asp:DropDownList>
            </td>
            <td width="10%">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, timkiem %>" 
                    onclick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
                    <td>
                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, mahopdong %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtcontractno" runat="server"></asp:TextBox>
            </td>
                                <td>
                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, tieude %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txttieude" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Panel>
</div>
<%--<div id="divToolbar">
    <asp:Button ID="btn_Add" runat="server" Text="<%$ Resources:labels, themmoi %>" onclick="Button1_Click" 
        style="height: 26px" />
&nbsp;
    <asp:Button ID="btn_Del" runat="server" Text="<%$ Resources:labels, delete %>" Width="73px" 
        onclick="btn_Del_Click" />
&nbsp;
    </div>--%>
<asp:Panel ID="PNRESULT" runat="server">
<div id="divResult">
    <asp:GridView ID="gvProvinceList" runat="server" BackColor="White" 
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Width="100%" AllowPaging="True" AutoGenerateColumns="False" 
        onrowdatabound="gvProvinceList_RowDataBound" PageSize="15" 
        onpageindexchanging="gvProvinceList_PageIndexChanging" 
        onsorting="gvProvinceList_Sorting" AllowSorting="True">
        <RowStyle ForeColor="#000000" />
        <Columns>
<%--                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbxSelect" runat="server"/>
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="<%$ Resources:labels, sogiaodich %>">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hpTranCode" runat="server"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gvHeader" />
                                    <%--<ItemStyle HorizontalAlign="Center" />--%>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:labels, mahopdong %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcontractno" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gvHeader" />
                                   <%-- <ItemStyle HorizontalAlign="Center" />--%>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Feedid" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFeedid" runat="server" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:labels, tieude %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gvHeader" />
                                    <%--<ItemStyle HorizontalAlign="Center" />--%>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:labels, noidung %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcontent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gvHeader" />
                                    <%--<ItemStyle HorizontalAlign="Center" />--%>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gvHeader" />
                                    <%--<ItemStyle HorizontalAlign="Center" />--%>
                                </asp:TemplateField>     
                               <asp:TemplateField HeaderText="<%$ Resources:labels, comment %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcomment" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gvHeader" />
                                    <%--<ItemStyle HorizontalAlign="Center" />--%>
                                </asp:TemplateField>                          
        </Columns>
        <FooterStyle CssClass="gvFooterStyle" />
            <PagerStyle HorizontalAlign="Center" CssClass="pager" />
            <SelectedRowStyle />
            <HeaderStyle CssClass="gvHeader" />
    </asp:GridView>
</div>
</asp:Panel>
    <asp:Literal ID="litPager" runat="server"></asp:Literal>

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