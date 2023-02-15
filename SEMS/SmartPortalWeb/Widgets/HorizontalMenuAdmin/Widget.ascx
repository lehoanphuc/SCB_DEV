<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_HorizontalMenuAdmin_Widget" %>


<script src="Widgets/HorizontalMenuAdmin/scripts/menu.js" type="text/javascript"></script>

<div id="menu">  
           <table cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td class="menuleft"></td>
                     <td class="menumiddle">
                        <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
                     </td>
                      <td class="menuright">
                      </td>
                </tr>          
                             
            </table>          
          
      </div>

