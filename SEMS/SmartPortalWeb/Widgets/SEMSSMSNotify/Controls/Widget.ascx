<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSSMSNotify_Controls_Widget" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<link href="widgets/SEMSSMSNotify/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSSMSNotify/JS/common.js" type="text/javascript"></script>
<link href="widgets/SEMSSMSNotify/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="JS/mask.js" type="text/javascript"></script>
<script type="text/javascript">
    var timeentry = jQuery;
    jQuery.noConflict(true);
</script>



<script type="text/javascript">
    runcalendar();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                runcalendar();
            }
        });
    };
    function runcalendar() {
        timeentry(document).ready(function () {
            timeentry("input[type=text][id*=txtLastTransTime]").timeentry();
        });
        timeentry(document).ready(function () {
            timeentry("input[type=text][id*=txtDueDateTime]").timeEntry();
        });
    }
</script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSSMSNotify/Images/smsnotification.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.configsmsnotification %>
</div>
<div id="divError">
    <asp:Label runat="server" ID="lblError"></asp:Label>
</div>
<div id="divRole" runat="server">
    <div class="divGetInfoCust">
        <asp:Panel ID="pnFee" runat="server">
            <div class="divHeaderStyle">
                <%=Resources.labels.configinformation %>
            </div>
            <table class="style1" cellpadding="3">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, role %>"></asp:Label>
                        *
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnChooseRole">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, loaigiaodich %>"></asp:Label>
                        *
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTransType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, configname %>"></asp:Label>
                        *
                    </td>
                    <td>
                        <asp:TextBox ID="txtConfigName" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, desc %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div style="text-align: center; margin: 10px;">
        &nbsp;
        <asp:Button ID="btnBackRole" runat="server" OnClick="btnBackRole_Click" Text="<%$ Resources:labels, back %>" />
        &nbsp;
        <asp:Button ID="btnNextRole" runat="server" OnClick="btnNextRole_Click" Text="<%$ Resources:labels, next %>" />
    </div>
</div>
<div id="divConfig" runat="server">
    <div class="divGetInfoCust">
        <asp:Panel ID="Panel1" runat="server">
            <div class="divHeaderStyle">
                <%=Resources.labels.configinformation %>
            </div>
            <asp:HiddenField ID="hdfID" runat="server" />
            <table class="style1" cellpadding="3">
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, role %>" Style="font-weight: bold"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblRoleConfig" runat="server" Style="font-weight: bold; color: steelblue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, loaigiaodich %>" Style="font-weight: bold"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTranTypConfig" runat="server" Style="font-weight: bold; color: steelblue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr id="trMAmt" runat="server">
                    <td id="lblMinAmt" runat="server">
                        <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, minimumamount %>"></asp:Label>
                        *
                    </td>
                    <td>
                        <asp:TextBox ID="txtMinAmt" runat="server"></asp:TextBox>
                    </td>
                    <td id="lblMaxAmt" runat="server">
                        <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, maximumamount %>"></asp:Label>
                        *
                    </td>
                    <td>
                        <asp:TextBox ID="txtMaxAmt" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trCurrency" runat="server">
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="<%$ Resources:labels, currency %>"></asp:Label>
                        *
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCurrency" runat="server"></asp:DropDownList>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnAddAmt" runat="server" Style="float: right; margin-right: 20px;" OnClick="btnAddAmt_Click" Text="<%$ Resources:labels, them %>" />
                    </td>
                </tr>
                <tr id="trDueDate" runat="server">
                    <td>
                        <asp:Label ID="lblFromDate" runat="server" Text="<%$ Resources:labels, timetosend %>"></asp:Label>
                        *
                    </td>
                    <td>
                        <asp:TextBox ID="txtDueDateTime" type="time" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trLastTrans" runat="server">
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, numberoftransaction %>"></asp:Label>
                        *
                    </td>
                    <td>
                        <asp:TextBox ID="txtNumTrans" type="number" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label20" runat="server" Text="<%$ Resources:labels, kieudatlich%>"></asp:Label>
                        *
                    </td>
                    <td>
                        <asp:RadioButton ID="rdDaily" runat="server" Text="<%$ Resources:labels, hangngay%>" GroupName="rdScheduleType" AutoPostBack="true" OnCheckedChanged="OnChooseScheduleType" />
                        <asp:RadioButton ID="rdWeekly" runat="server" Text="<%$ Resources:labels, hangtuan%>" GroupName="rdScheduleType" AutoPostBack="true" OnCheckedChanged="OnChooseScheduleType" />
                        <asp:RadioButton ID="rdMonthly" runat="server" Text="<%$ Resources:labels, hangthang%>" GroupName="rdScheduleType" AutoPostBack="true" OnCheckedChanged="OnChooseScheduleType" />
                    </td>
                </tr>
                <tr id="trWeekly" runat="server">
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, chonthu %>"></asp:Label>
                        *
                    </td>
                    <td colspan="3">
                        <asp:CheckBoxList ID="cblThu" runat="server"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="<%$ Resources:labels, chunhat %>"></asp:ListItem>
                            <asp:ListItem Value="2" Text="<%$ Resources:labels, thuhai %>"></asp:ListItem>
                            <asp:ListItem Value="3" Text="<%$ Resources:labels, thuba %>"></asp:ListItem>
                            <asp:ListItem Value="4" Text="<%$ Resources:labels, thutu %>"></asp:ListItem>
                            <asp:ListItem Value="5" Text="<%$ Resources:labels, thunam %>"></asp:ListItem>
                            <asp:ListItem Value="6" Text="<%$ Resources:labels, thusau %>"></asp:ListItem>
                            <asp:ListItem Value="7" Text="<%$ Resources:labels, thubay %>"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr id="trMonthly" runat="server">
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="<%$ Resources:labels, chonthu %>"></asp:Label>
                        *
                    </td>
                    <td colspan="3">
                        <asp:CheckBoxList ID="cblThuM" runat="server"
                            RepeatDirection="Horizontal" RepeatColumns="16">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr id="trLastTransTime" runat="server">
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, timetosend %>"></asp:Label>
                        *
                    </td>
                    <td>
                        <asp:TextBox ID="txtLastTransTime" type="time" value="11:01:00" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</div>


<div id="divResult" runat="server">
    <div class="divGetInfoCust">
        <asp:Panel ID="pnResult" runat="server">
            <div class="divHeaderStyle">
                <%=Resources.labels.configinformation %>
            </div>
            <table class="style1" cellpadding="3">
                <tr>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:labels, role %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblRoleCF" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label16" runat="server" Text="<%$ Resources:labels, loaigiaodich %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTranTypeCF" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label15" runat="server" Text="<%$ Resources:labels, configname %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblConfigNameCF" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label17" runat="server" Text="<%$ Resources:labels, desc %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDescCF" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trTimeCF" runat="server">
                    <td>
                        <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, timetosend %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTimeCF" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trScheduleTypeCF" runat="server">
                    <td>
                        <asp:Label ID="Label19" runat="server" Text="<%$ Resources:labels, loailich %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblScheduleTypeCF" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trScheduleDayCF" runat="server">
                    <td>
                        <asp:Label ID="Label21" runat="server" Text="<%$ Resources:labels, ngaysegui %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblScheduleDayCF" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trNumTranCF" runat="server">
                    <td>
                        <asp:Label ID="Label22" runat="server" Text="<%$ Resources:labels, numberoftransaction %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblNumTranCF" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>

    </div>
</div>
<div id="divGrid" runat="server" style="padding-left: 5px; padding-right: 5px; padding-top: 5px">
    <asp:Panel ID="pnGV" runat="server">
        <asp:GridView ID="gvConfigDetails" runat="server" BackColor="White"
            BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
            Width="100%" AllowPaging="True" AutoGenerateColumns="False"
            OnRowDataBound="gvConfigDetails_RowDataBound" PageSize="15"
            OnPageIndexChanging="gvConfigDetails_PageIndexChanging"
            OnSorting="gvConfigDetails_Sorting" AllowSorting="True"
            OnRowDeleting="gvConfigDetails_RowDeleting">
            <RowStyle ForeColor="#000000" />
            <Columns>
                <%--            <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbxSelect" runat="server" />                   
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Config ID" SortExpression="CFID" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblCFID" runat="server" Visible="true"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FkID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblFkID" runat="server" Visible="false"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, minimumamount %>">
                    <ItemTemplate>
                        <asp:Label ID="lblmin" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, maximumamount %>"><%--SortExpression="TOLIMIT"--%>
                    <ItemTemplate>
                        <asp:Label ID="lblmax" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, currency %>"><%--SortExpression="MINAMOUNT"--%>
                    <ItemTemplate>
                        <asp:Label ID="lblccyid" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:CommandField DeleteText="<%$ Resources:labels, huy %>" HeaderText="<%$ Resources:labels, huy %>" ShowDeleteButton="True">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" CssClass="pager" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#E0ECFF" Font-Bold="True" ForeColor="Black" />
        </asp:GridView>
        <br />
        <asp:Literal ID="litPager" runat="server"></asp:Literal>
    </asp:Panel>
</div>

<div style="text-align: center; margin-top: 10px;" id="divbtnAmt" runat="server">
    <asp:Button ID="btnBackMAmt" runat="server" OnClick="btnBackMAmt_Click" Text="<%$ Resources:labels, back %>" />
    &nbsp;
        <asp:Button ID="btnNextMAmt" runat="server" OnClick="btnNextMAmt_Click" Text="<%$ Resources:labels, next %>" />
</div>
<div style="text-align: center; margin-top: 10px;" id="divbtnCF" runat="server">
    <asp:Button ID="btnBackCF" runat="server" OnClick="btnBackCF_Click" Text="<%$ Resources:labels, back %>" />
    &nbsp;
        <asp:Button ID="btnSaveCF" runat="server" OnClick="btnSaveCF_Click" Text="<%$ Resources:labels, luu %>" />
    &nbsp;
        <asp:Button ID="btnExit" runat="server" OnClick="btnExitCF_Click" Text="<%$ Resources:labels, thoat %>" />
</div>
