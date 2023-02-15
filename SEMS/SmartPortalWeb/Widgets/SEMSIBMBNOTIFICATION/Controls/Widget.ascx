<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSProduct_Controls_Widget" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<link href="widgets/SEMSCustomerList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>
<script src="widgets/SEMSCustomerList/JS/tab-view.js" type="text/javascript"></script>
<script src="widgets/SEMSProduct/JS/common.js" type="text/javascript"></script>
<link href="widgets/SEMSProduct/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/css/style.css" rel="stylesheet" type="text/css">
<link href="widgets/SEMSCustomerList/css/subModal.css" rel="stylesheet" type="text/css">
<script src="widgets/IBTransferInBank1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/lang/en.js" type="text/javascript"></script>
<link href="widgets/IBTransferInBank1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<script src="widgets/IBScheduleTransfer/JS/jquery-1.5.js" type="text/javascript"></script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <contenttemplate>
<br />
<div id="divCustHeader">
    <asp:Image ID="imgLoGo" runat="server" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" /> 
    <asp:Label ID="lblTitleProduct" runat="server"></asp:Label>
</div>
<div id="divError">
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
<ProgressTemplate>
<img alt="" src="widgets/SEMSProduct/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
</ProgressTemplate>
</asp:UpdateProgress>
    <asp:Label ID="lbError"  Font-Bold="true" ForeColor="Red" runat="server" Text=""></asp:Label>
</div>
<div>
    
</div>
<div class="divGetInfoCust">

<asp:Panel ID="pnAdd" runat="server">
<div class="divHeaderStyle">
       <%=Resources.labels.thongtinnotifyIBMB %>
    </div>
    <table class="style1" style="margin-left:0px;">
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Service"></asp:Label>
            </td>
            <td>
              <asp:DropDownList ID="ddlService" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlService_SelectedIndexChanged"></asp:DropDownList>

            </td>
           
        </tr>
      <tr>
           <td>
                <asp:Label ID="Label6" runat="server" Text="Type"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlVarname" runat="server"></asp:DropDownList>

            </td>
      </tr>
         
          <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Content"></asp:Label>
            </td>
            <td>
                <asp:TextBox TextMode="MultiLine" ID="txtContent"  runat="server"></asp:TextBox>

            </td>
        </tr>
         
          <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Link"></asp:Label>
            </td>
            <td>
                <asp:TextBox  ID="txtLink" runat="server"></asp:TextBox>

            </td>
        </tr>
        
          <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Loop times"></asp:Label>
            </td>
            <td>
                <asp:TextBox onkeypress="return isNumber(event);" ID="ddlLoop" runat="server"></asp:TextBox>
               <%-- <asp:DropDownList  SkinID="ddl50" ID="ddlLoop" runat="server">
                     <asp:ListItem Text="1" Value="1" />
                    <asp:ListItem Text="2" Value="2" />
                    <asp:ListItem Text="3" Value="3" />
                </asp:DropDownList>--%>

            </td>
        </tr>
         
          <tr id="trdevicetype" runat="server" >
            <td>
                <asp:Label ID="Label3" runat="server" Text="Device type"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDevicetype" runat="server">
                    <asp:ListItem Text="ALL" Value="ALL" />
                    <asp:ListItem Text="AND" Value="Android" />
                    <asp:ListItem Text="IOS" Value="IOS" />
                </asp:DropDownList>

            </td>
        </tr>
          <tr id="trmbver" runat="server" >
            <td>
                <asp:Label ID="Label10" runat="server" Text="MB Version"></asp:Label>
            </td>
            <td> 
              <asp:DropDownList ID="ddlMBversion" runat="server"></asp:DropDownList>-
                <asp:DropDownList ID="ddlVersionupdown" runat="server">
                    <asp:ListItem Text="ALL" Value="ALL" />
                    <asp:ListItem Text="U" Value="U" />
                    <asp:ListItem Text="E" Value="E" />
                    <asp:ListItem Text="D" Value="D" />
                </asp:DropDownList>

            </td>
        </tr>
         
          <tr id="trval5" runat="server">
            <td>
                <asp:Label ID="Label11" runat="server" Text="Value5"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtValue5" runat="server"></asp:TextBox>

            </td>
        </tr>
        
          <tr id="trval6" runat="server">
            <td>
                <asp:Label ID="Label12" runat="server" Text="Value6"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtValue6" runat="server"></asp:TextBox>

            </td>
        </tr>
        
          <tr id="trval7" runat="server">
            <td>
                <asp:Label ID="Label13" runat="server" Text="Value7"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtValue7" runat="server"></asp:TextBox>

            </td>
        </tr>
        
          <tr id="trval8" runat="server">
            <td>
                <asp:Label ID="Label14" runat="server" Text="Value8"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtValue8" runat="server"></asp:TextBox>

            </td>
        </tr>
        
          <tr id="trval9" runat="server">
            <td>
                <asp:Label ID="Label15" runat="server" Text="Value9"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtValue9" runat="server"></asp:TextBox>

            </td>
        </tr>
        
          <tr id="trval10" runat="server">
            <td>
                <asp:Label ID="Label16" runat="server" Text="Value10"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtValue10" runat="server"></asp:TextBox>

            </td>
        </tr>
           <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, ngayhieuluc %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtdatestart"  runat="server"></asp:TextBox>
                <asp:TextBox ID="txttimestart"  runat="server"></asp:TextBox>
                

            </td>
        </tr>
           <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, ngayhethieuluc %>"></asp:Label>
            </td>
            <td>
                
                <asp:TextBox ID="txtdateend" runat="server"></asp:TextBox>
                <asp:TextBox ID="txttimeend" runat="server"></asp:TextBox>
            </td>
        </tr>

<%--        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, masanpham%>"></asp:Label> *
            </td>
            <td>
                <asp:TextBox ID="txtmasp" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, tensanpham %>"></asp:Label> *
            </td>
            <td>
                <asp:TextBox ID="txttensp" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, loaikhachhang %>"></asp:Label>
            </td>
            <td>
               <asp:DropDownList ID="DropDownList" runat="server" Width="57%">
                    <asp:ListItem Value="P" Text="<%$ Resources:labels, canhan %>" ></asp:ListItem>
                    <asp:ListItem Value="O" Text="<%$ Resources:labels, doanhnghiep %>" ></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, mota%>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtdesc" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>--%>
    </table>  	   
     <script src="widgets/IBScheduleTransfer/JS/jquery.maskedinput-1.3.min.js"
        type="text/javascript"></script>
<script type="text/javascript">
    var maskedinput = jQuery;
    jQuery.noConflict(true);
    </script>

         <script type="text/javascript">//<![CDATA[
             runcalendar();
             function isNumber(evt) {
                 evt = (evt) ? evt : window.event;
                 var charCode = (evt.which) ? evt.which : evt.keyCode;
                 if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                     return false;
                 }
                 return true;
             }
             function runcalendar() {
                 maskedinput(document).ready(function () {
                     maskedinput("input[type=text][id*=txttimestart]").mask("99:99:00");
                 });
                 maskedinput(document).ready(function () {
                     maskedinput("input[type=text][id*=txttimeend]").mask("99:99:00");
                 });



                 var cal = Calendar.setup({
                     onSelect: function (cal) { cal.hide() }
                 });
                 cal.manageFields("<%=txtdatestart.ClientID %>", "<%=txtdatestart.ClientID %>", "%d/%m/%Y");
                 cal.manageFields("<%=txtdateend.ClientID %>", "<%=txtdateend.ClientID %>", "%d/%m/%Y");
             }
             //]]></script>   
</asp:Panel>
   </div>   
 <%-- <div id="divAccount" style="height:250px;">


 
<script type="text/javascript">
    

</script>
</div>--%>
</contenttemplate>

</asp:UpdatePanel>
<div style="text-align: center; margin-top: 10px;">
    <asp:Button ID="btsave" runat="server" Text="<%$ Resources:labels, save %>"
        OnClientClick="return validate();" OnClick="btsave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btback" runat="server" Text="<%$ Resources:labels, back %>" PostBackUrl="javascript:history.go(-1)" OnClick="btback_Click" />


</div>

<script language="javascript">
    function validate() {
        if (validateEmpty('<%=txtContent.ClientID %>', '<%=Resources.labels.youneedtypecontent %>')) {

        }
        else {
            document.getElementById('<%=txtContent.ClientID %>').focus();
            return false;
        }
    }
    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    //On UpdatePanel Refresh
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                runcalendar();
                //hidepwdcpdetail();
                //hidetimelogindetail()


            }
        });
    };

</script>
