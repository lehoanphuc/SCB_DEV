<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Controls_IconSlideRight_Widget" %>
<table cellspacing="5">
    <tr>
        <td>
             <img src="App_Themes/Bank2/images/theme.gif" />
		     <asp:HyperLink ID="slidepage" runat="server" Text='<%$ Resources:labels, theme %>'></asp:HyperLink>		            
             <br />
              <br />
               
        </td>
         <td>
            <img src="App_Themes/Bank2/images/language.gif" />
            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%$ Resources:labels, language %>'></asp:HyperLink>		           
            <br />
              <br />
         </td>
          <td>
             <img src="App_Themes/Bank2/images/setting.gif" />
             <asp:HyperLink ID="HyperLink2" runat="server"  Text='<%$ Resources:labels, setting %>'></asp:HyperLink>		           
            <br />
              <br />
          </td>
    </tr>
    <tr>
        <td>
             <img src="App_Themes/Bank2/images/user.gif" />
             <asp:HyperLink ID="HyperLink3" runat="server"  Text='<%$ Resources:labels, user %>'></asp:HyperLink>		           
            <br />
              <br />
        </td>
         <td>
             <img src="App_Themes/Bank2/images/group.gif" />
             <asp:HyperLink ID="HyperLink4" runat="server"  Text='<%$ Resources:labels, group %>'></asp:HyperLink>		           
                 <br />
              <br />
         </td>
          <td></td>
    </tr>
     <tr>
        <td>
            
        </td>
         <td>
            
         </td>
          <td></td>
    </tr>
</table>