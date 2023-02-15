<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSBillPayment_Widget" %>
<style>
 #divSearch
    {
    	background-color:#F8F8F8;
    	border:solid 1px #B9BFC1;
    	margin:5px 5px 5px 5px;
    	padding:5px 5px 5px 5px;
    }
  #divProvinceHeader
    {   
    	width:100%;	
    	font-weight:bold;
    	padding:5px 5px 5px 5px;
    }
</style>
<br />
<div id="divProvinceHeader">
    <img alt="" src="widgets/SEMSFeedBack/Images/feedback.png" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" /> 
    <%=Resources.labels.tracuuthongtinhoadon %>
</div>
<div style="text-align:center; margin-bottom:10px;">
    <asp:Label ID="lblTextError" runat="server" Font-Bold="True" Font-Italic="True" 
        ForeColor="Red"></asp:Label>
</div>
<div id="divSearch">
    <table style="width:100%;">
        <tr>
            <td>
                                    <asp:Label ID="Label4" runat="server" 
                    Text="<%$ Resources:labels, dichvu %>"></asp:Label>
                                </td>
            <td>
                                    <asp:DropDownList ID="ddlservice" runat="server" AutoPostBack="true" 
                                        
                    onselectedindexchanged="ddlservice_SelectedIndexChanged">

                                    </asp:DropDownList>
                                </td>
            <td>
                                    <asp:Label ID="Label5" runat="server" 
                                        Text="<%$ Resources:labels, nhacungcap %>"></asp:Label>
                                </td>
            <td>
                                    <asp:DropDownList ID="ddlprovider" runat="server" 
                    AutoPostBack="true">

                                    </asp:DropDownList>
                                </td>
            <td>
                <asp:Button ID="btnXemHoaDon" OnClientClick="return validate();"  
                    runat="server" Text="<%$ Resources:labels, xemchitiet %>" 
                    onclick="btnXemHoaDon_Click" />
            </td>
        </tr>
        <tr>
            <td>
                                    <asp:Label ID="Label1" runat="server" 
                    Text="<%$ Resources:labels, sodanhba %>"></asp:Label>
                                </td>
            <td>   
                                    <asp:TextBox ID="txtCustCode" runat="server"></asp:TextBox>
                                    </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</div>
<br />
<asp:Panel runat="server" ID="pnResult">
<table style="width:100%;" cellspacing="0" cellpadding="5">
                            <tr>
                                <td style="width:25%">
                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, dichvu %>"></asp:Label>
                                </td>
                                <td style="width:25%">
                                    <asp:Label ID="lblService" runat="server" Text="Trả tiền nước"></asp:Label>
                                </td> 
                              <td style="width:25%">
                                    <asp:Label ID="Label17" runat="server" Text="<%$ Resources:labels, nhacungcap %>"></asp:Label>
                                </td>
                               <td style="width:25%">
                                    <asp:Label ID="lblprovider" runat="server" Text="Công ty nước"></asp:Label>
                                </td>                                 
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, sodanhba %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblCustCode" runat="server" Text="KH000138474"></asp:Label>
                                </td>  
                                <td class="tibtd">
                                    <asp:Label ID="Label21" runat="server" Text="<%$ Resources:labels, tenkhachhang %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblcustname" runat="server" Text="A Tũn"></asp:Label>
                                </td>                               
                            </tr>
                                                        
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label46" runat="server" Text="<%$ Resources:labels, diachi %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lbladdress" runat="server" Text="168"></asp:Label>
                                </td>
                               <td class="tibtd">
                                    <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, duong %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblstreet" runat="server" Text="Vô Lý Thường Kiệt"></asp:Label>
                                </td>                                
                            </tr>
                             </table>
                        <%--  <div class="content"> --%>  <div id="divResult">
<asp:Literal runat="server" ID="ltrError"></asp:Literal>
    <asp:GridView ID="gvProductList" runat="server" BackColor="White" 
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Width="100%" AllowPaging="True" AutoGenerateColumns="False" 
        onrowdatabound="gvProductList_RowDataBound" PageSize="15" 
        onpageindexchanging="gvProductList_PageIndexChanging" 
        >
        <RowStyle ForeColor="#000000" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:CheckBox ID="cbxSelect" runat="server" />                   
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ID" Visible="false" >
                <ItemTemplate>
                    <asp:Label ID="lbID" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, sohoadon %>" ><%--SortExpression="ProductName"--%>
                <ItemTemplate>
                    <asp:Label ID="lbsohoadon" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, namhoadon %>" >
                <ItemTemplate>
                   <asp:Label ID="lblnamhoadon" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, kyhoadon %>" >
                <ItemTemplate>
                    <asp:Label ID="lbkyhoadon" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, tienphi %>" >
                <ItemTemplate>
                    <asp:Label ID="lbltienphi" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, tienthue %>">
                <ItemTemplate>
                    <asp:Label ID="lbtienthue" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="<%$ Resources:labels, bieuphi %>" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lbbieuphi" runat="server" Visible="false"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, dinhmuc %>" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lbdinhmuc" runat="server" Visible="false"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>           
            <asp:TemplateField HeaderText="<%$ Resources:labels, giaban %>" >
                <ItemTemplate>
                    <asp:Label ID="lbgiaban" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="<%$ Resources:labels, tongcong %>">
                <ItemTemplate>
                    <asp:Label ID="lbtongcong" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
<%--            
            <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
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
            </asp:TemplateField>--%>
        </Columns>
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" CssClass="pager" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#E0ECFF" Font-Bold="True" ForeColor="Black" />
    </asp:GridView>
    <br />
    <%--<asp:Literal ID="litPager" runat="server"></asp:Literal>--%>
</div>
<div style="text-align:right;">
    Tổng tiền :
    <asp:Label ID="lblSum" runat="server" Text="<%$ Resources:labels, lak %>"></asp:Label>
</div>
</asp:Panel>
<script>
    function validate() {

        if (document.getElementById('<%=txtCustCode.ClientID %>').value=='') {
            alert('Bạn cần nhập mã khách hàng');
            document.getElementById('<%=txtCustCode.ClientID %>').focus();
            return false;
            
        }
        else {
            return true;
        }


    }
</script>