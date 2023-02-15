<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUserApproveLimit_Controls_Widget" %>



<script src="JS/mask.js" type="text/javascript"></script>

<script src="JS/docso.js" type="text/javascript"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="al">
    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, thietlaphanmucduyetgiaodich %>"></asp:Label><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="divAddInfoPro">

            <asp:Panel ID="pnAdd" runat="server">
                <div style="text-align: center;">
                    <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <figure>
                    <legend class="handle"><%=Resources.labels.thongtinhanmucduyetgiaodich %></legend>
                    <div class="content display-label">
                        <div class="row">
                            <label class="col-xs-5 col-sm-2 bold"><%=Resources.labels.nguoidung %></label>
                            <div class="col-xs-7 col-sm-4">
                                <asp:DropDownList ID="ddlTeller" runat="server" />
                            </div>
                            <label class="col-xs-5 col-sm-2 bold"><%=Resources.labels.loaigiaodich %></label>
                            <div class="col-xs-7 col-sm-4">
                                <asp:DropDownList ID="ddlTrans" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-xs-5 col-sm-2 bold required">Approval Limit</label>
                            <div class="col-xs-7 col-sm-4">
                                <asp:TextBox ID="txtApprovalLimit" runat="server" ></asp:TextBox>
                            </div>
                            <label class="col-xs-5 col-sm-2 bold required"><%=Resources.labels.tonghanmucngay %></label>
                            <div class="col-xs-7 col-sm-4">
                                <asp:TextBox ID="txtTotallimit" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-xs-5 col-sm-2 bold required">Number of transaction</label>
                            <div class="col-xs-7 col-sm-4">
                                <asp:TextBox ID="txtNumberTran" runat="server"></asp:TextBox>
                            </div>
                            <label class="col-xs-5 col-sm-2 bold required"><%=Resources.labels.tiente %></label>
                            <div class="col-xs-7 col-sm-4">
                                <asp:DropDownList ID="ddlCCYID" runat="server">
                                    <asp:ListItem Value="LAK" Text="<%$ Resources:labels, lak %>"></asp:ListItem>
                                    <asp:ListItem Value="USD" Text="USD"></asp:ListItem>
                                    <asp:ListItem Value="THB" Text="THB"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="text-align: center; margin-top: 10px;">
                            <asp:Button ID="btback" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, quaylai %>" Width="70px" OnClick="btnBack_OnClick"  />
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, luu %>"
                                OnClientClick="return validate();" OnClick="btsave_Click" Width="70px" />
                            <asp:Button ID="btnew" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, themmoi %>"
                                        OnClick="btnew_Click" />
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </figure>
            </asp:Panel>
        </div>
    </ContentTemplate>

</asp:UpdatePanel>


<script>




    function ntt(sNumber, idDisplay, event) {

        executeComma(sNumber, event);

        if (document.getElementById(sNumber).value == "") {
            document.getElementById(idDisplay).innerHTML = "";
            return;
        }
        document.getElementById(idDisplay).innerHTML = "(" + number2text(replaceAll(document.getElementById(sNumber).value, ",", ""), '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>') + ")";
    }

    
    function validate() {
        if (!validateEmpty('<%=txtApprovalLimit.ClientID %>', '<%=Resources.labels.duyethanmuckhongrong %>')) {
            document.getElementById('<%=txtApprovalLimit.ClientID %>').focus();

            return false;
        }

        if (!validateEmpty('<%=txtTotallimit.ClientID %>', '<%=Resources.labels.limitperdaycannotbeempty %>')) {
            document.getElementById('<%=txtTotallimit.ClientID %>').focus();
            return false;
        }

        if (!validateEmpty('<%=txtNumberTran.ClientID %>', '<%=Resources.labels.sogiaodichngaykhongrong %>')) {
            document.getElementById('<%=txtNumberTran.ClientID %>').focus();
            return false;
        }

        if (!validateFormTo('<%=txtApprovalLimit.ClientID %>', '<%=txtTotallimit.ClientID %>', '<%=Resources.labels.sotientonggioihanphailonhonsotiengioihan %>')) {
            document.getElementById('<%=txtApprovalLimit.ClientID %>').focus();
            return false;
        }
    }
 
</script>
<style>
    .required::after {
        content: '(*)';
        margin-left: 2px;
        color: red;
        position: absolute;
    }
</style>
