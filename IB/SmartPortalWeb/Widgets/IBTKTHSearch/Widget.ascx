<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTKTHSearch_Widget" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register src="../../Controls/LetterSearch/LetterSearch.ascx" tagname="LetterSearch" tagprefix="uc1" %>
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
    	margin:20px 0px 5px 0px;
    	padding:5px 5px 5px 5px;
    	width:98.5%;
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
        .al
    {
	    font-weight:bold;
	    padding-left:5px;
	    padding-top:0px;
	    padding-bottom:0px;
    }    
    .searchtitle
    {

    	font-weight:bold;
    }
    .divHeaderStyle
   {
   	    background-color:#EAFAFF;color:#003366;   	    	   
   	    font-weight:bold;
   	    margin:0px;
   	    line-height:20px;
   	    padding:10px 0px 0px 0px;
   }
</style>
<script src="widgets/SEMSProduct/JS/common.js" type="text/javascript"></script>
<link href="widgets/SEMSProduct/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<link href="widgets/IBInfoLC/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/IBInfoLC/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBInfoLC/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/IBInfoLC/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBInfoLC/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBInfoLC/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

<link href="widgets/IBTransactionHistory1/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/IBTransactionHistory1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/IBTransactionHistory1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>


<%--<div id="divProductHeader">
    <img alt="" src="widgets/SEMSTellerApproveTrans/Images/processteller.png" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" /> THIẾT LẬP QUI TRÌNH DUYỆT

</div>--%>

<%--<div class="al">
<span>Tạo người thụ hưởng</span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>--%>
<div id="divError">
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
<ProgressTemplate>
<img alt="" src="Images/WidgetImage/ajaxloader.gif" style="width: 16px; height: 16px;" /> <%=Resources.labels.loading %></ProgressTemplate>
</asp:UpdateProgress>
</div>

<div class="block1">   
<asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
                    <div class="handle">                    	
                    	Thông tin tìm kiếm
                    </div>
<div class="content">
 
    <table class="style1">
        <tr>
            <td class="searchtitle" Width="20%">
                <asp:Label ID="Label1" runat="server" Text="Tên người chuyển"></asp:Label>
            </td >
            <td Width="25%">
                <asp:TextBox ID="txtSender" runat="server"></asp:TextBox>
            </td>
             <td class="searchtitle" Width="15%">
                <asp:Label ID="Label4" runat="server" Text="Tài khoản"></asp:Label>
            </td>
            <td Width="30%">
                <asp:TextBox ID="txtAccount" runat="server" ></asp:TextBox>
            </td>
            <td Width="20%">
                <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" 
                    onclick="btnSearch_Click" />
            </td>
        </tr>
        <tr>             

        </tr>
    </table>
    </div>
</asp:Panel>
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div class="block1">   
<div id="divToolbar">
    <asp:Button ID="btnAddNew" runat="server" Text="Tạo mới" 
        onclick="btnAddNew_Click" />
        &nbsp;
   <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:labels, delete %>" Width="73px" 
        onclick="btnDelete_Click" />

 </div>
<%--<div id="divResult">--%>


      <div class="handle">                    	
       Danh sách tài khoản chuyển đến
      </div>
 <div class="content">
     <asp:GridView ID="gvProcessList" runat="server" AutoGenerateColumns="False" 
                           BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                           CellPadding="3" Width="100%" 
                           onrowdatabound="gvProcessList_RowDataBound" PageSize="15" 
                           onpageindexchanging="gvProcessList_PageIndexChanging" 
                           onsorting="gvProcessList_Sorting" AllowSorting="True">
                               <RowStyle ForeColor="#000000" />
                                <Columns>
                                   <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbxSelect" runat="server" />                   
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gvHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mã người chuyển" SortExpression="PROCESSID" Visible="false">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hpsenderid" runat="server" Visible="false"></asp:HyperLink>
                                        </ItemTemplate>
                                         <HeaderStyle CssClass="gvHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tên người chuyển" >
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lblsender" runat="server"></asp:HyperLink>
                                        </ItemTemplate>
                                         <HeaderStyle CssClass="gvHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tài khoản">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaccount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle CssClass="gvHeader" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Ghi chú">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle CssClass="gvHeader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Xóa">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
                                        </ItemTemplate>
                                         <HeaderStyle CssClass="gvHeader" />
                                    </asp:TemplateField>
                                </Columns>
                                   <FooterStyle BackColor="White" ForeColor="#000066" />
                                   <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                   <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                   <HeaderStyle BackColor="#009CD4" Font-Bold="True"/>
      </asp:GridView>
       <asp:Label runat="server" ID="lblAlert" ForeColor="Red"></asp:Label>
</div>
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