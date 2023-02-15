<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridViewPaging.ascx.cs" Inherits="Controls_WidgetHTML_GridViewPaging" %>
<style type="text/css">
      .navigationButton {
      -webkit-box-shadow: rgba(0,0,0,0.2) 0 1px 0 0;
      -moz-box-shadow: rgba(0,0,0,0.2) 0 1px 0 0;
      box-shadow: rgba(0,0,0,0.2) 0 1px 0 0;
      border-bottom-color: #333;
      border: 1px solid #ffffff;
      background-color: #EAF2D3;
      border-radius: 5px;
      -moz-border-radius: 5px;
      -webkit-border-radius: 5px;
      color: #0d76c3;
      font-family: 'Verdana',Arial,sans-serif;
      font-size: 14px;
      text-shadow: #b2e2f5 0 1px 0;
      padding: 5px;
      cursor: pointer;
 }
.tablePaging {
      font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
      width: 100%;
      border-collapse: collapse;
 }
.tablePaging td {
      font-size: 1em;
      border: 1px solid #ffffff;
      padding: 3px 7px 2px 7px;
      background-color: #EAFAFF;
      font-size: 10pt;
 } 
</style>
<asp:HiddenField ID="hdfCurrentPage" runat="server" />
<table class="tablePaging">
 <tr>
     <td style="width: 15%; text-align: center;">
     <asp:Label ID="lblPageSize" runat="server" Text="Page Size: "></asp:Label>
        <asp:Label ID="Label1" runat="server" Text="15"></asp:Label>
     <asp:DropDownList ID="PageRowSize" AutoPostBack="true" Visible="false" runat="server" style="width:50px!important" se OnSelectedIndexChanged="PageRowSize_onclick">
          <asp:ListItem Selected="True">15</asp:ListItem>
          <asp:ListItem>30</asp:ListItem>
          <asp:ListItem>50</asp:ListItem>
          <asp:ListItem>100</asp:ListItem>
    </asp:DropDownList>
 </td>
 <td style="width: 25%; text-align: center;">
     <asp:Label ID="RecordDisplaySummary" runat="server"></asp:Label></td>
 <td style="width: 20%; text-align: center;">
     <asp:Label ID="PageDisplaySummary" runat="server"></asp:Label></td>
 <td style="width: 40%; text-align: center ;">

     <asp:Button ID="First" runat="server" Text="<<" OnClick="First_Click" CssClass="btnGeneral" style="width:40px!important;font-weight: bold;"/>&nbsp;

     <asp:Button ID="Previous" runat="server" Text="<" OnClick="Previous_Click" CssClass="btnGeneral" style="width:40px!important;font-weight: bold;"/>&nbsp;

     <asp:TextBox ID="SelectedPageNo" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
     Font-Names="Verdana" Font-Size="Small" OnTextChanged="SelectedPageNo_TextChanged" AutoPostBack="True" MaxLength="8"
     style="width:50px!important; text-align:center"></asp:TextBox>&nbsp;

     <asp:Button ID="Next" runat="server" Text=">" OnClick="Next_Click" CssClass="btnGeneral" style="width:40px!important;font-weight: bold;"/>&nbsp;

     <asp:Button ID="Last" runat="server" Text=">>" OnClick="Last_Click" CssClass="btnGeneral" style="width:40px!important;font-weight: bold;"/>&nbsp; 
 </td>
 </tr>
 <tr id="trErrorMessage" runat="server" visible="false">
     <td colspan="4" style="background-color: #e9e1e1;">
     <asp:Label ID="GridViewPagingError" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="Red"></asp:Label>
     <asp:HiddenField ID="TotalRows" runat="server" Value="0" />
     </td>
 </tr>
</table>