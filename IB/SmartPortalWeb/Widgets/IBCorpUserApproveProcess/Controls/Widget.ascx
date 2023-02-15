<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUserApproveProcess_Controls_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="" runat="server">
    <ProgressTemplate>
        <div class="cssProgress">
            <div class="progress1">
                <div class="cssProgress-bar cssProgress-active" data-percent="100" style="transition: none; width: 100%;"></div>
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>

<div class="al">
    <asp:Label ID="Label1" runat="server"
        Text="<%$ Resources:labels, thietlapquytrinhduyetgiaodich %>"></asp:Label><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label runat="server" Font-Bold="true" ForeColor="Red" ID="lblError"></asp:Label>
        </div>
        <asp:Panel ID="pnAdd" runat="server">
            <figure>
                <legend class="handle"><%=Resources.labels.thongtinduyet %></legend>
                <div class="content">
                    <div class="row form-group">
                        <label class="col-xs-4 col-sm-2 bold">
                            <%= Resources.labels.loaigiaodich %>
                        </label>
                        <div class="col-xs-8 col-sm-3">
                            <asp:DropDownList ID="ddlTrans" runat="server"></asp:DropDownList>
                        </div>
                        <label class="col-xs-4 col-sm-2 bold">
                            <%= Resources.labels.accountnumber %>
                        </label>
                        <div class="col-xs-8 col-sm-3">
                            <asp:DropDownList ID="ddlAccNumber" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-xs-4 col-sm-2 bold">
                            <%= Resources.labels.tu %>
                        </label>
                        <div class="col-xs-8 col-sm-3">
                            <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                        </div>
                        <label class="col-xs-4 col-sm-2 bold">
                            <%= Resources.labels.den %>
                        </label>
                        <div class="col-xs-8 col-sm-3">
                            <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-xs-4 col-sm-2 bold">
                            <%= Resources.labels.tiente %>
                        </label>
                        <div class="col-xs-8 col-sm-3">
                            <asp:DropDownList ID="ddlCCYID" runat="server"></asp:DropDownList>
                        </div>
                        <label class="col-xs-4 col-sm-2 bold">
                        </label>
                        <div class="col-xs-8 col-sm-5">
                            <asp:CheckBox runat="server" ID="cbIsAOT" CssClass="aspCheckBox" Text="<%$ Resources:labels, allowapproveowntransaction %>" />
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-xs-4 col-sm-2 bold">
                        </label>
                        <div class="col-xs-8 col-sm-10">
                            <asp:CheckBox runat="server" ID="cbNeedApprove" CssClass="aspCheckBox" Checked="true" Text="<%$ Resources:labels, transactionneedtoapprove %>" AutoPostBack="true" OnCheckedChanged="cbNeedApprove_CheckedChanged" />
                        </div>
                    </div>
                </div>
            </figure>
        </asp:Panel>

        <asp:Panel ID="pnGroup" runat="server">
            <figure>
                <legend class="handle"><%=Resources.labels.grouptab %></legend>
                <div class="content">
                    <asp:Repeater runat="server" ID="rptGroup">
                        <HeaderTemplate>
                            <table class="table table-bordered table-hover footable" data-paging="true">
                                <thead>
                                    <tr>
                                        <th><%= Resources.labels.groupid %></th>
                                        <th><%= Resources.labels.groupname %></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("GroupID") %></td>
                                <td><%#Eval("GroupName") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
			</table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </figure>
        </asp:Panel>

        <asp:Panel ID="pnProcessApprove" runat="server">
            <figure>
                <legend class="handle"><%=Resources.labels.quitrinhduyet %></legend>
                <div class="content">
                    <div class="row form-group">
                        <label class="col-xs-4 col-sm-2 bold"><%= Resources.labels.approveformula %></label>
                        <div class="col-xs-8 col-sm-3">
                            <asp:TextBox ID="txtFormula" CssClass="form-control" MaxLength="200" runat="server"></asp:TextBox>
                        </div>
                        <label class="col-xs-4 col-sm-2 bold"><%= Resources.labels.mota %></label>
                        <div class="col-xs-8 col-sm-3">
                            <asp:TextBox ID="txtDesc" CssClass="form-control" Height="50" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="col-xs-8 col-sm-2">
                            <asp:Button ID="btnSaveDetails" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, add %>" OnClientClick="return validateAdd();"
                                OnClick="btnSaveDetails_Click" />
                        </div>
                    </div>
                </div>
            </figure>
        </asp:Panel>

        <asp:Panel ID="pnApproval" runat="server" Style="margin-top: 10px;">
            <figure>
                <div class="content">
                    <asp:Repeater runat="server" ID="gvAppTransDetailsList" OnItemCommand="gvAppTransDetailsList_ItemCommand">
                        <HeaderTemplate>
                            <table class="table table-bordered table-hover footable" data-paging="true">
                                <thead>
                                    <tr>
                                        <th><%= Resources.labels.order %></th>
                                        <th><%= Resources.labels.approveformula %></th>
                                        <th><%= Resources.labels.desc %></th>
                                        <%if(this.IsDelete){ %>
                                        <th><%= Resources.labels.sort %></th>
                                        <th><%= Resources.labels.huy %></th>
                                        <%} %>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("Ord") %></td>
                                <td><%#Eval("Formula") %></td>
                                <td><%#Eval("Desc") %></td>
                                <%if(this.IsDelete){ %>
                                <td>
                                    <asp:LinkButton ID="lbUpArrow" runat="server" CommandName="Up" CommandArgument='<%# Eval("Ord") %>' Visible="<%#VisibleUp(Container.ItemIndex) %>">
                                        <i class="fa fa-chevron-up"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbDownArrow" runat="server" CommandName="Down" CommandArgument='<%# Eval("Ord") %>' Visible="<%#VisibleDown(Container.ItemIndex) %>"> 
                                        <i class="fa fa-chevron-down"></i>
                                    </asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbDelete" runat="server" CommandName='Delete' CommandArgument='<%#Eval("Ord")%>' OnClientClick="Loading(); return sweetAlertConfirm(this);"><%= Resources.labels.huy %></asp:LinkButton>
                                </td>
                                <%} %>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
			</table>
                        </FooterTemplate>
                    </asp:Repeater>

                </div>
            </figure>
        </asp:Panel>
        
        <div class="button-group">
            <asp:Button ID="btback" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, quaylai %>" OnClick="btnBack_OnClick" />
            <asp:Button ID="btsave" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, luu %>" OnClientClick="return validateSave();" OnClick="btsave_Click" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    function validateSave() {
        if (!validateEmpty('<%=txtFrom.ClientID %>', '<%=Resources.labels.sotienkhoidiemkhongrong %>')) {
            return false;
        }
        if (!validateMoney('<%=txtTo.ClientID %>', '<%=Resources.labels.sotienketthuckhongrong %>')) {
            return false;
        }
        if (!validateFormTo('<%=txtFrom.ClientID %>', '<%=txtTo.ClientID %>', '<%=Resources.labels.sotienketthucphailonhonsotienkhoidiem %>')) {
            return false;
        }
        return true;
    }
    function validateAdd() {
        if (!validateEmpty('<%=txtFormula.ClientID %>', '<%=Resources.labels.pleaseinputapproveformula %>')) {
            return false;
        }
        return true;
    }
    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }
</script>
