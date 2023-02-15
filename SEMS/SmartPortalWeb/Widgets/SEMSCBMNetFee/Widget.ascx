<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCBMNetFee_Widget" %>
<%@ Register TagPrefix="uc1" TagName="GridViewPaging" Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.interbankfee%>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.detailinterbankfee%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnFocus" runat="server" DefaultButton="btnSave">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-offset-2 control-label"><%=Resources.labels.sotien %> *</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtAmount" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4"></div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate()" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divResult" runat="server">
            <asp:Literal ID="litError" runat="server"></asp:Literal>
            <asp:GridView ID="gvHisChange" CssClass="table table-hover" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvHisChange_RowDataBound" PageSize="15">
                <Columns>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, ngaysuadoi %>">
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, valueold %>">
                        <ItemTemplate>
                            <asp:Label ID="lblValueOld" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, valuenew %>">
                        <ItemTemplate>
                            <asp:Label ID="lblValueNew" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, nguoisuadoi %>">
                        <ItemTemplate>
                            <asp:Label ID="lblUserName" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                </Columns>
                <SelectedRowStyle />
            </asp:GridView>
            <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
            <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
            <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="Widgets/SEMSCBMNetFee/JS/jquery.min.js"></script>
<script src="Widgets/SEMSCBMNetFee/JS/jquery.inputmask.bundle.js"></script>
<script type="text/javascript">
    Inputmask.extendAliases({
        pesos: {
            prefix: "",
            groupSeparator: ".",
            alias: "numeric",
            placeholder: "0",
            autoGroup: !0,
            digits: 2,
            digitsOptional: !1,
            clearMaskOnLostFocus: !1
        }
    });

    $("#<%= txtAmount.ClientID %>").inputmask({
        alias: "pesos"
    });

    function validate() {
        if (document.getElementById('<%=txtAmount.ClientID%>').value == '') {
            alert('<%=Resources.labels.tienphikhongrong %>');
            document.getElementById('<%=txtAmount.ClientID %>').focus();
            return false;
        }
        return true;
    }
</script>
