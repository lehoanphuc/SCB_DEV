<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_UserInRole_Widget" %>

<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSCustomerList/Images/messenger.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.setroleforuser %>
</div>
<div id="divError"></div>
<div class="divGetInfoCust">
    <div class="divHeaderStyle">
        <asp:Label ID="lblUserName" runat="server"></asp:Label>
    </div>
    <table class="style1" cellspacing="0" cellpadding="5">
        <tr>
            <td>
                <asp:GridView ID="gvRoles" runat="server" AutoGenerateColumns="False" ShowHeader="False" BorderWidth="0px" GridLines="None" onrowdatabound="gvRoles_RowDataBound">
                    <RowStyle Height="25px" />                     
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblRoleID" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbRoleName" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>
<div style="text-align:center; padding-top:10px;">
    &nbsp;
    <asp:Button ID="Button1" runat="server" Text='<%$ Resources:labels, save %>' onclick="Button1_Click" />
    &nbsp;
    <asp:Button ID="btnBack" runat="server" Text="<%$ Resources:labels, back %>" PostBackUrl="javascript:history.go(-1)" />
</div>