<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_rptAllReport_Widget" %>
<div class="subheader">
    <h1 class="subheader-title">
        <%=Resources.labels.danhsachbaocao %>
    </h1>
</div>
<div id="divResult">
    <asp:GridView ID="gvAllReport" CssClass="table table-hover" runat="server" BackColor="White"
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
        Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvAllReport_RowDataBound">
        <RowStyle ForeColor="#000000" />
        <Columns>
            <asp:TemplateField HeaderText="<%$ Resources:labels, mabaocao %>">
                <ItemTemplate>
                    <asp:HyperLink ID="lblReportID" runat="server"></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, tenbaocao %>">
                <ItemTemplate>
                    <asp:Label ID="lblReportName" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="gvHeader" />
    </asp:GridView>
</div>
