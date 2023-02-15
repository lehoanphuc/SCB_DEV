<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSTelco_ViewCard_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging1" %>


        <asp:GridView ID="gvCardid" runat="server" BackColor="White" CssClass="table table-hover"
            BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
            Width="100%" AllowPaging="True" AutoGenerateColumns="False" 
            OnRowDataBound="gvCardid_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Card ID">
                    <ItemTemplate>
                        <asp:Label ID="lblcardid" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Short Name">
                    <ItemTemplate>
                        <asp:Label ID="lblshortname" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Card Amount">
                    <ItemTemplate>
                        <asp:Label ID="lblcardamount" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Real Money">
                    <ItemTemplate>
                        <asp:Label ID="lblrealmoney" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Currency">
                    <ItemTemplate>
                        <asp:Label ID="lblccyid" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblstatus" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type">
                    <ItemTemplate>
                        <asp:Label ID="lbltype" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle CssClass="gvFooterStyle hidden" />
            <PagerStyle HorizontalAlign="Center" CssClass="pager hidden" />
        </asp:GridView>
         <uc1:GridViewPaging1 runat="server" ID="GridViewPaging1" />
          <asp:HiddenField ID="hdCounter1" Value="0" runat="server" />
          <asp:HiddenField ID="hdPageSize1" Value="15" runat="server" />
