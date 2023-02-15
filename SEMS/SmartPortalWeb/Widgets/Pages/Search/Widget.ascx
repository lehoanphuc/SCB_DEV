<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_Pages_Search_Widget" %>

<div>
    <table width="100%" style="margin:5px auto 5px auto">
        <tr>
            <td>
                <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:ImageButton ID="btnSearch" 
                                    ImageUrl="~/Widgets/Pages/Search/images/search.gif" runat="server" 
                                    onclick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</div>