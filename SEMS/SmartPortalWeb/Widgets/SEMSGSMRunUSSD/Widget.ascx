<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSGSMRunUSSD_Widget" %>
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSReportManagement/Images/report.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.GSMRunUSD%>
</div>
<div id="divError">
    <asp:Label runat="server" ID="lblTextError"></asp:Label>
</div>
<!--First-->
<asp:Panel ID="pnUSSD" runat="server">
    <div class="divGetInfoCust">
        <div class="divHeaderStyle">
            <%=Resources.labels.GSMRunUSD%>
        </div>
        <table class="style1" cellspacing="0" cellpadding="3">
            <tr>
                <td style="width: 50%; text-align: right;">
                    <asp:Label ID="Label48" runat="server" Text="<%$ Resources:labels, USSDCode %>"></asp:Label>
                    *
                </td>
                <td>
                    <asp:TextBox ID="txtUSSDCode" runat="server"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" Text=" eg: *124#" ForeColor="#339966"></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 50%; text-align: right;">
                    <asp:Label ID="Label21" runat="server" Text="<%$ Resources:labels, ketqua %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtResult" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    
    <!--Button next-->
    <div style="text-align: center; margin-top: 10px;">
        <asp:Button ID="btnCheck" runat="server" Text="<%$ Resources:labels, kiemtra %>" OnClientClick="return Validate();" OnClick="btnCheck_Click" />
    </div>
</asp:Panel>
<script type="text/javascript">
    function Validate() {
        if (validateEmpty('<%=txtUSSDCode.ClientID %>','<%=Resources.labels.bancannhapussdcode %>')) {
        }
        else {
            document.getElementById('<%=txtUSSDCode.ClientID %>').focus();
            return false;
        }
    }
</script>
