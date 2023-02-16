<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUserLimit_Controls_Widget" %>


<link href="css/css.css" rel="stylesheet" type="text/css" />
<script src="JS/mask.js"></script>
<script src="JS/docso.js"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div id="divError">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
        <ProgressTemplate>
            <img alt="" src="Images/WidgetImage/ajaxloader.gif" style="width: 16px; height: 16px;" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
</div>

<div class="al">
    <asp:Label ID="Label7" runat="server"
        Text="<%$ Resources:labels, thietlaphanmucthuchiengiaodich %>"></asp:Label><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>
        <div class="divAddInfoPro">
            <div style="text-align: center;">
                <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                <div style="text-align: center; margin-top: 10px;">
                    <asp:Button Visible="false" ID="btnOverwrite" CssClass="btn btn-primary" runat="server" Text="Overwrite Transaction" 
                        OnClick="btnOverwrite_Click" />
                    <asp:Button Visible="false" ID="btnAddMissTrans" runat="server" CssClass="btn btn-primary" Text="Add miss Transaction"
                        OnClientClick="return validate();" OnClick="btnAddMissTrans_Click" />
                    <asp:Button Visible="false" ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel"
                        OnClick="btnCancel_Click" />
                </div>
            </div>
            <asp:Panel ID="pnAdd" runat="server">
                <figure>
                    <legend class="handle"><%=Resources.labels.thongtinhanmuc %> </legend>
                    <div class="content">
                        <div class="row form-group">
                            <label class="col-md-2 col-xs-5 bold"><%= Resources.labels.loaigiaodich %></label>
                            <div class="col-md-4 col-xs-7">
                                <asp:DropDownList ID="ddlTrans" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <label class="col-md-2 col-xs-5 bold"><%= Resources.labels.nguoidung %></label>
                            <div class="col-md-4 col-xs-7">
                                <asp:DropDownList ID="ddlTeller" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row form-group">
                            <label class="col-md-2 col-xs-5 bold required"><%= Resources.labels.hanmuctrengiaodich %></label>
                            <div class="col-md-4 col-xs-7">
                                <asp:TextBox ID="txtlimit" runat="server"></asp:TextBox>
                            </div>
                            <label class="col-md-2 col-xs-5 bold required"><%= Resources.labels.tonghanmuctrenngay %></label>
                            <div class="col-md-4 col-xs-7">
                                <asp:TextBox ID="txttotallimit" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-md-2 col-xs-5 bold required"><%= Resources.labels.sogiaodichtrenngay %></label>
                            <div class="col-md-4 col-xs-7">
                                <asp:TextBox ID="txtcountlimit" runat="server"></asp:TextBox>
                            </div>
                            <label class="col-md-2 col-xs-5 bold"><%= Resources.labels.tiente %></label>
                            <div class="col-md-4 col-xs-7">
                                <asp:DropDownList ID="ddlCCYID" runat="server">
                                    <asp:ListItem Value="LAK" Text="<%$ Resources:labels, lak %>"></asp:ListItem>
                                    <asp:ListItem Value="USD" Text="USD"></asp:ListItem>
                                    <asp:ListItem Value="THB" Text="THB"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </figure>
            </asp:Panel>
        </div>
        <%--</ContentTemplate>
</asp:UpdatePanel>--%>
        <div style="text-align: center; margin-top: 10px;">
            <asp:Button ID="btback" CssClass="btn btn-warning" runat="server" Text="<%$ Resources:labels, quaylai %>" OnClick="btnBack_OnClick" />

            <asp:Button ID="btsave" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, luu %>"
                OnClientClick="return validate();" OnClick="btsave_Click" />
            <asp:Button ID="btnew" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, themmoi %>"
                OnClick="btnew_Click" />
            <div class="clearfix"></div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script language="javascript">
    function validate() {

        if (validateEmpty('<%=txtlimit.ClientID %>','<%=Resources.labels.hanmuckhongrong %>')) {
            if (validateEmpty('<%=txttotallimit.ClientID %>','<%=Resources.labels.tonghanmuctrenngaykhongrong %>')) {

            }
            else {
                document.getElementById('<%=txttotallimit.ClientID %>').focus();
                return false;
            }

        }
        else {
            document.getElementById('<%=txtlimit.ClientID %>').focus();
            return false;
        }



    }
    function validate() {
        if (!validateEmpty('<%=txtlimit.ClientID %>', '<%=Resources.labels.limitpertranscannotbeempty %>')) {
            document.getElementById('<%=txtlimit.ClientID %>').focus();

            return false;
        }

        if (!validateEmpty('<%=txttotallimit.ClientID %>', '<%=Resources.labels.limitperdaycannotbeempty %>')) {
            document.getElementById('<%=txttotallimit.ClientID %>').focus();
            return false;
        }

        if (!validateEmpty('<%=txtcountlimit.ClientID %>', '<%=Resources.labels.sogiaodichngaykhongrong %>')) {
            document.getElementById('<%=txtcountlimit.ClientID %>').focus();
            return false;
        }

        if (!validateFormTo('<%=txtlimit.ClientID %>', '<%=txttotallimit.ClientID %>', '<%=Resources.labels.sotientonggioihanphailonhonsotiengioihan %>')) {
            document.getElementById('<%=txtlimit.ClientID %>').focus();
            return false;
        }
    }

</script>
