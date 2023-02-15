<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCustFXLimit_Control_Widget" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<link href="widgets/SEMSCustFXLimit/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSCustFXLimit/JS/ajax.js" type="text/javascript"></script>
<script src="widgets/SEMSCorpApproveWorkflow/JS/mask.js"></script>
<script src="JS/SEMSCorpApproveWorkflow/mask_CR.js" type="text/javascript"></script>
<script src="JS/mask_CR.js" type="text/javascript"></script>
<script src="widgets/SEMSCustFXLimit/JS/commonjs.js" type="text/javascript"></script>
<script src="widgets/SEMSCustFXLimit/JS/tab-view.js" type="text/javascript"></script>
<script src="widgets/SEMSCustFXLimit/JS/common.js" type="text/javascript"></script>
<link href="widgets/SEMSCustFXLimit/css/style.css" rel="stylesheet" type="text/css">
<link href="widgets/SEMSCustFXLimit/css/subModal.css" rel="stylesheet" type="text/css">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<br />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <contenttemplate>
<div id="divCustHeader">
    <asp:Image ID="imgLoGo" runat="server" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" /> 
    <asp:Label ID="lblTitleProduct" runat="server"></asp:Label>
</div>

<div id="divError">
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" runat="server">
<ProgressTemplate>
<img alt="" src="widgets/SEMSCustFXLimit/Images/loading.gif" style="width: 16px; height: 16px;" />
</ProgressTemplate>
</asp:UpdateProgress>
<asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
</div>

<asp:Panel ID="pnSearch"  runat="server"  DefaultButton="btnSearch">
    <div class="divGetInfoCust">
        <div class="divHeaderStyle">
            Contract search
        </div>
        <table class="style1" cellpadding="3">
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, contractno %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtContractSearch" runat="server" ></asp:TextBox>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, fullname %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtFullNameSearch" runat="server" ></asp:TextBox>
                </td>
                <td class="thbtn">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"></asp:Button>
                </td>
            </tr>
        </table>  
    </div>
    <div id="divResult">
        <asp:GridView ID="gvContractList" runat="server" BackColor="White" 
            BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
            Width="100%" AllowPaging="True" AutoGenerateColumns="False" 
            onrowdatabound="gvContractList_RowDataBound" PageSize="15" 
            onpageindexchanging="gvContractList_PageIndexChanging" 
            AllowSorting="True">
            <RowStyle ForeColor="#000000" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:RadioButton ID="colRbContractNo" runat="server" AutoPostBack="true" GroupName="GrContract" OnCheckedChanged="colRbContractNo_CheckedChanged"/>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, contractno %>">
                    <ItemTemplate>
                        <asp:Label ID="colContractNo" runat="server" Text='<%# Eval("CONTRACTNO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, fullname %>">
                    <ItemTemplate>
                        <asp:Label ID="colFullname" runat="server" Text='<%# Eval("FULLNAME") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle CssClass="gvFooterStyle" />
            <PagerStyle HorizontalAlign="Center" CssClass="pager" />
            <SelectedRowStyle />
            <HeaderStyle CssClass="gvHeader" />
        </asp:GridView>
        <br />
        <asp:Literal ID="litPager" runat="server"></asp:Literal>
    </div>	 
</asp:Panel>

<asp:Panel ID="pnAdd" runat="server"  onkeydown = "return (event.keyCode!=13)" class="divAddInfoPro">
    <div class="divGetInfoCust">
    <div class="divHeaderStyle">
      <%=Resources.labels.thongtinhanmucsanpham %>
    </div>
    <table class="style1" cellpadding="3">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, fullname %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtContractNo" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbllimit" runat="server" Text="<%$ Resources:labels, hanmuc %>"></asp:Label> *
            </td>
            <td>
                <asp:TextBox ID="txtlimit" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblTotalLimitDay" runat="server" Text="<%$ Resources:labels, tonghanmucngay %>"></asp:Label> *
            </td>
            <td>
                <asp:TextBox ID="txtTotalLimitDay" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                 <asp:Label ID="lblCountLimit" runat="server" Text="<%$ Resources:labels, solangiaodichngay%>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCountLimit" runat="server"></asp:TextBox>
            </td>
			<td>
                <asp:Label ID="Label6" runat="server"  Text="<%$ Resources:labels, chophepngoaihoirabenngoai%>"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="cbFX" runat="server"></asp:CheckBox>
            </td>   
        </tr>
       
        <tr>
            <td>
                <asp:Label ID="lblCCYID" runat="server" Text="<%$ Resources:labels, FromCCY %>"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlFromCCYID" runat="server">
                <asp:ListItem Value="LAK" Text="<%$ Resources:labels, lak %>"></asp:ListItem>
                
                    <asp:ListItem>USD</asp:ListItem>
                
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, ToCCY %>"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlToCCYID" runat="server">
                <asp:ListItem Value="LAK" Text="<%$ Resources:labels, lak %>"></asp:ListItem>
                
                    <asp:ListItem>USD</asp:ListItem>
                
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnAdd" Text="<%$ Resources:labels, add %>" runat="server" OnClick="btnAdd_Click" OnClientClick="return validate();"/>
            </td>
        </tr>
    </table>  	   
    </div>
</asp:Panel>

</br>
<div>
<asp:Panel ID="pnAddDetail" runat="server" onkeydown = "return (event.keyCode!=13)">
     <div id="divAddDetail" style="padding: 5px;">
        <asp:GridView ID="gvAddDetail" runat="server" BackColor="White" 
            BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
            Width="100%" AllowPaging="True" AutoGenerateColumns="False" 
            PageSize="15" 
            onpageindexchanging="gvAddDetail_PageIndexChanging" 
            AllowSorting="True">
            <RowStyle ForeColor="#000000" />
            <Columns>
                <asp:TemplateField HeaderText="<%$ Resources:labels, giaodich %>" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblTranCode" runat="server" Text='<%# Eval("TRANDCODE") %>' ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center"/>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="<%$ Resources:labels, giaodich %>" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblTranCodeName" runat="server" Text='<%# Eval("TRANDCODENAME") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, FromCCY %>">
                    <ItemTemplate>
                        <asp:Label ID="lblFromCCY" runat="server" Text='<%# Eval("FROMCCYID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, ToCCY %>">
                    <ItemTemplate>
                        <asp:Label ID="lblToCCY" runat="server" Text='<%# Eval("TOCCYID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, hanmucmotgiaodich %>">
                    <ItemTemplate>
                        <asp:Label ID="lblLimitTran" runat="server" Text='<%# Eval("LIMITTRAN") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Number Transaction / Day">
                    <ItemTemplate>
                        <asp:Label ID="lblCountTran" runat="server" Text='<%# Eval("COUNTTRAN") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, tonghanmucngay %>">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalLimit" runat="server" Text='<%# Eval("TOTALLIMIT") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, chophepngoaihoirabenngoai %>">
                <ItemTemplate>
                    <asp:Label ID="lblfx" runat="server" Text='<%# Eval("ALLOWFOREIGN") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                    <ItemTemplate>
                        <asp:LinkButton ID="colDelete" runat="server" OnClick="colDelete_Click"
                        CommandArgument='<%#Eval("TRANDCODE")+"#"+Eval("FROMCCYID")+"#"+Eval("TOCCYID")%>' Text="<%$ Resources:labels, delete %>">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center"/>
                </asp:TemplateField>
            </Columns>
            <FooterStyle CssClass="gvFooterStyle" />
            <PagerStyle HorizontalAlign="Center" CssClass="pager" />
            <SelectedRowStyle />
            <HeaderStyle CssClass="gvHeader" />
        </asp:GridView>
        <br />
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </div>
</asp:Panel> 
</div>

<div style="text-align: center; margin-top: 10px;">
    &nbsp;
    <asp:Button ID="btsave" runat="server" Text="<%$ Resources:labels, save %>"
        OnClientClick="return validate();" OnClick="btsave_Click" />&nbsp;
    <asp:Button ID="btback" runat="server" Text="<%$ Resources:labels, back %>" OnClientClick="return confirm('Are you sure you want to back?')"
        OnClick="btback_Click" />&nbsp;
    <asp:Button ID="btbackHome" runat="server" Text="<%$ Resources:labels, back %>" Visible="false"
        OnClick="btbackHome_Click" />&nbsp;
    <asp:Button ID="btnNext" runat="server" Text="<%$ Resources:labels, next %>"
        OnClick="btnNext_Click" />                       
</div>
</contenttemplate>
</asp:UpdatePanel>
<script language="javascript">
    function validate() {
        if (!validateEmpty('<%=txtlimit.ClientID %>', '<%=Resources.labels.hanmucsanphamkhongrong %>')) {
            document.getElementById('<%=txtlimit.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtTotalLimitDay.ClientID %>', '<%=Resources.labels.tonghanmuctrenngaycuahanmucsanphamkhongrong %>')) {
            document.getElementById('<%=txtTotalLimitDay.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtCountLimit.ClientID %>', '<%=Resources.labels.sogiaodichngaykhongrong %>')) {
            document.getElementById('<%=txtCountLimit.ClientID %>').focus();
            return false;
        }
    }
</script>
