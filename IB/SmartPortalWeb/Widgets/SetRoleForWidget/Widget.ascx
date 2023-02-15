<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SetRoleForWidget_Widget" %>


<div style="padding:5px 0px 5px 5px; text-align:center;">
   
    <img alt="" src="widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="LinkButton1" runat="server" Text='<%$ Resources:labels, save %>' 
                onclick="Button1_Click" /> 
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="LinkButton2" runat="server" Text='<%$ Resources:labels, exit %>' 
                onclick="Button2_Click1" />  
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A1" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
      <hr />
</div>

<table style=" margin:5px auto 5px auto;">
    <tr>
        <td>
            <asp:GridView ID="gvRoles" runat="server" AutoGenerateColumns="False" 
                ShowHeader="False" BorderWidth="0px" GridLines="None" 
                onrowdatabound="gvRoles_RowDataBound">
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
    <tr>
        <td>        
           
            &nbsp; 
                  
        </td>
    </tr>
</table>
<div style="padding:5px 0px 5px 5px; text-align:center;">
   <hr />
    <img alt="" src="widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="Button1" runat="server" Text='<%$ Resources:labels, save %>' 
                onclick="Button1_Click" /> 
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="Button2" runat="server" Text='<%$ Resources:labels, exit %>' 
                onclick="Button2_Click1" />  
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A2" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
      
</div>

    

