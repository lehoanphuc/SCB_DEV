<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_CategoryApprove_Widget" %>
<link href="Widgets/CategoryApprove/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="Widgets/CategoryApprove/Scripts/JScript.js" type="text/javascript"></script>

<link href="Widgets/CategoryApprove/CSS/SyntaxHighlighter.css" rel="stylesheet" type="text/css" />

<script src="Widgets/CategoryApprove/Scripts/shCore.js" type="text/javascript"></script>

<script src="Widgets/CategoryApprove/Scripts/shBrushJScript.js" type="text/javascript"></script>

<script src="Widgets/CategoryApprove/Scripts/ModalPopups.js" type="text/javascript"></script>

<div style="padding:5px 0px 5px 5px;">
    <img id="imgApprove" runat="server" alt="" src="~/widgets/newsapprove/Images/adminapproval_icon_smll.gif" 
        style="width: 15px; height: 15px" />
    <asp:LinkButton ID="lbAddPage" Text='<%$ Resources:labels, approve %>' 
        runat="server" onclick="lbAddPage_Click" ></asp:LinkButton>
&nbsp;
    <img id="imgUnApprove" runat="server" alt="" src="~/widgets/newsapprove/Images/lock_user_icon.png" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="lbDeleteSelected" 
        Text='<%$ Resources:labels, unapprove %>' runat="server" onclick="lbDeleteSelected_Click" 
         ></asp:LinkButton>
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A1" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     <hr />
</div>

<div style=" text-align:center; margin:5px 1px 5px 1px;">
<asp:Label ID="lblAlert" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
</div>

<div style="text-align:left; padding:5px 5px 5px 5px;">
<table style=" margin:0 auto 0 auto">
    <tr>
        <td>        
        
<asp:Panel runat="server" ID="pnSearch" DefaultButton="ibSearch">
&nbsp;<asp:Panel ID="pnCondition" runat="server" 
        GroupingText="<%$ Resources:labels, filter %>" Width="400px">
        <table>
            <tr>
                <td align="right">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, keyword %>"></asp:Label>
                    :</td>
                <td>
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:ImageButton ID="ibSearch" runat="server" 
                        ImageUrl="~/Widgets/widget/view/images/search.gif" onclick="ibSearch_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, status %>"></asp:Label>
                    :</td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>

        </td>
    </tr>
</table>
</div>

<div>
<table style="margin:5px auto 5px auto; width:100%;">
    <tr>
        <td>
            <asp:GridView ID="gvCategory" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                ForeColor="#333333" GridLines="None" 
                 PageSize="15" Width="100%" 
                onpageindexchanging="gvCategory_PageIndexChanging" 
                onrowdatabound="gvCategory_RowDataBound" onsorting="gvCategory_Sorting" >
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                           <center><asp:CheckBox ID="cbxSelect" runat="server" /></center> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblCatID" runat="server" Visible="False"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, title %>' SortExpression="CatName">
                        <ItemTemplate>
                           <asp:HyperLink ID="hpCatName" runat="server"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>  
                                     
                    <asp:TemplateField HeaderText='<%$ Resources:labels, author %>' SortExpression="UserCreated">
                        <ItemTemplate>
                            <asp:Label ID="lblAuthor" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText='<%$ Resources:labels, datecreated %>' 
                        SortExpression="DateCreated">
                        <ItemTemplate>
                            <asp:Label ID="lblDateCreated" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText='<%$ Resources:labels, parent %>' SortExpression="Parent">
                        <ItemTemplate>
                           <asp:Label ID="lblParent" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>                  
                    
                    
                   
                    
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle CssClass="pager" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#BCDFFB" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </td>
    </tr>
</table>  
</div>
<div style="padding:5px 0px 5px 5px;">
     <hr />
    <img id="imgApprove1" runat="server" alt="" src="~/widgets/newsapprove/Images/adminapproval_icon_smll.gif" 
        style="width: 15px; height: 15px" />
    <asp:LinkButton ID="lbAddPage1" Text='<%$ Resources:labels, approve %>' 
        runat="server" onclick="lbAddPage_Click" ></asp:LinkButton>
&nbsp;
    <img id="imgUnApprove1" runat="server" alt="" src="~/widgets/newsapprove/Images/lock_user_icon.png" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="lbDeleteSelected1" 
        Text='<%$ Resources:labels, unapprove %>' runat="server" onclick="lbDeleteSelected_Click" 
         ></asp:LinkButton>
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A2" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
    
</div>


<script src="Widgets/CategoryApprove/Scripts/shInit.js" type="text/javascript"></script>
<script>

    //ModalPopupsConfirm();
    function ModalPopupsConfirm(un, button) {
        ModalPopups.Confirm("idConfirm1",
        "<%=Resources.labels.authencation %>",
        "<div id='divError' style='color:maroon; font-size:8pt; font-weight:bold; text-align:center;'></div><table><tr><td><%=Resources.labels.password %> :</td><td><input id='txtPassword' type='password' /></td></tr><tr><td colspan='2'><input id='txtUser' type='text' style='visibility:hidden' value='" + un + "' /></td></tr></table>",
        {
            yesButtonText: "<%=Resources.labels.process %>",
            noButtonText: "<%=Resources.labels.exit %>",
            onYes: "ModalPopupsConfirmYes(" + button + ")",
            onNo: "ModalPopupsConfirmNo()",
            shadowSize: 15
        }
    );
        return false;
    }
    function ModalPopupsConfirmYes(button) {
        var pass = document.getElementById('txtPassword').value;
        var user = document.getElementById('txtUser').value;
        _login("AjaxRequest/AjaxRequest.aspx", button, "u=" + user + "&p=" + pass + "&mode=aa&d=" + new Date().getTime());
    }
    function ModalPopupsConfirmNo() {
        ModalPopups.Cancel("idConfirm1");
    }
    //Authencation By AJAX
    function _login(filename, button,param) {
        var objXml = ModalPopupsSupport.getXmlHttp();
        objXml.open("POST", filename, true);
        objXml.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        objXml.setRequestHeader("Content-length", param.length);
        objXml.setRequestHeader("Connection", "close");
        objXml.onreadystatechange = function() {
            if (objXml.readyState == 4) {
                if (objXml.responseText != "0") {
                    ModalPopups.Close("idConfirm1");
                    if (button == '0') {
                        __doPostBack("<%=lbAddPage.ClientID.Replace('_','$')%>", '');
                    }
                    else {
                        __doPostBack("<%=lbDeleteSelected.ClientID.Replace('_','$')%>", '')
                    }
                }

                else {
                    document.getElementById('divError').innerHTML = "<%=Resources.labels.passwordincorrect %>";

                }
            }
        }
        objXml.send(encodeURI(param));
    }
</script>