<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpRole_Delete_Widget" %>

<%--<div style="padding:5px 0px 5px 5px; text-align:center;">
    
    <img alt="" src="widgets/pages/view/images/action_delete.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="LinkButton1" runat="server" 
                Text='<%$ Resources:labels, delete %>' onclick="btnDelete_Click" />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="LinkButton2" runat="server" Text='<%$ Resources:labels, exit %>' 
                onclick="btnExit_Click" />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A2" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     <hr />
</div>--%>
<table style=" margin:5px auto 5px auto; text-align:center;" cellspacing="10">
    <tr>
        <td>
            <asp:Label runat="server" ID="lblDelete" Text='<%$ Resources:labels, areyousuredeletethisrecord %>'>
            </asp:Label>
        </td>
    </tr>
  
</table>
<div style="padding:5px 0px 5px 5px; text-align:center;">
    <hr />
    <asp:Image runat="server" ID="imgDelete" alt="" ImageUrl="~/widgets/pages/view/images/action_delete.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="lbDelete" runat="server" 
                Text='<%$ Resources:labels, delete %>' onclick="btnDelete_Click" />
    &nbsp;
    <asp:Image runat="server" alt="" ImageUrl="~/widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="LinkButton4" runat="server" Text='<%$ Resources:labels, exit %>' 
                onclick="btnExit_Click" />
     &nbsp;
     <asp:Image runat="server" alt="" ImageUrl="~/widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A1" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     
</div>