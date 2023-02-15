<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUserLevel_Control_Widget" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="" runat="server">
    <ProgressTemplate>
        <div class="cssProgress">
            <div class="progress1">
                <div class="cssProgress-bar cssProgress-active" data-percent="100" style="transition: none; width: 100%;"></div>
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<div class="al">
    <asp:Label ID="Label6" Font-Bold="True" runat="server"
        Text='<%$ Resources:labels, group %>'></asp:Label><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label runat="server" ID="lblError" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>

        <asp:Panel ID="pnConfirm" runat="server">
            <figure>
                <legend class="handle"><%=Resources.labels.grouptab %></legend>
                <div class="content">
                    <div class="row form-group">
                        <label class="col-xs-4 col-md-offset-2 col-md-2 bold"><%=Resources.labels.groupcode %></label>
                        <div class="col-xs-8 col-md-4">
                            <asp:DropDownList ID="ddlGroup" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-xs-4 col-md-offset-2 col-md-2 bold"><%=Resources.labels.groupname %></label>
                        <div class="col-xs-8 col-md-4">
                            <asp:TextBox ID="txtNameGroup" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-xs-4 col-md-offset-2 col-md-2 bold"><%=Resources.labels.groupshortname %></label>
                        <div class="col-xs-8 col-md-4">
                            <asp:TextBox ID="txtShortNameGroup" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-xs-4 col-md-offset-2 col-md-2 bold"><%=Resources.labels.mota %></label>
                        <div class="col-xs-8 col-md-4">
                            <asp:TextBox ID="txtDescGroup" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="button-group">
                        <asp:Button ID="Button1" CssClass="btn btn-warning" runat="server"
                            Text='<%$ Resources:labels, quaylai %>' OnClick="Button1_Click" />
                        <asp:Button ID="btnLuu" CssClass="btn btn-primary" runat="server"
                            Text='<%$ Resources:labels, save %>' OnClick="btnLuu_Click" OnClientClick="return validate();" />
                    </div>
                </div>
            </figure>
        </asp:Panel>

        <asp:Panel ID="pnResult" runat="server">
            <figure>
                <div class="button-group">
                    <asp:Button ID="Button3" runat="server" CssClass="btn btn-warning"
                        Text='<%$ Resources:labels, thoat %>' OnClick="Button3_Click" />
                    <asp:Button ID="btnNew" runat="server" CssClass="btn btn-success"
                        Text='<%$ Resources:labels, themmoi %>' OnClick="btnNew_Click" />
                    <div class="clearfix"></div>
                </div>
            </figure>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    function validate() {
        if (!validateEmpty('<%=txtNameGroup.ClientID %>', '<%=Resources.labels.groupnamecannotbeempty %>')) {
            return false;
        }
        return true;
    }
</script>
