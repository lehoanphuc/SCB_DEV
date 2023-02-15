<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCashback_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.cashbacklist%>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div>
            <div class="col-sm-12" style="text-align: right">
                <a href="Widgets/SEMSCashBack/File/CashBackTemplate.xls">Download Cash Back Template</a>
            </div>
        </div>
        <div id="divToolbar" class="col-sm-12">
            <div class="form-group">
                <div class="col-sm-1"></div>
                <div class="col-sm-3">
                    <div class="input-group">
                        <div class="custom-file">
                            <asp:FileUpload ID="FileUpload1" CssClass="custom-file-input" runat="server" accept=".csv,.xlsx,.xls" />
                            <label class="custom-file-label"></label>
                        </div>
                        <label id="filename"></label>
                    </div>
                </div>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnImport" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Button ID="btnImport" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, ImportFile %>" autopostback="false" OnClick="btnImport_click" OnClientClick="myFunction()" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-2">
                    <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="Clear All" OnClick="btnClearing_Click" />
                </div>
            </div>
        </div>
        <%-- Table View --%>

        <div id="divResult" class="table-responsive">
            <asp:Label runat="server" ID="ltrError"></asp:Label>
            <asp:GridView ID="GVCashCode" runat="server" AutoGenerateColumns="False" CssClass="table table-hover"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                Width="100%" OnRowCancelingEdit="GVCashCode_RowCancelingEdit" OnRowDataBound="GVCashCode_RowDataBound"
                OnRowEditing="GVCashCode_RowEditing" OnRowUpdating="GVCashCode_RowUpdating">
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="lbl_No" runat="server" Text='<%#Eval("colNo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transaction No" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_TransactionNumber" runat="server" Text='<%#Eval("colTransactionNumber") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_TransactionNumber" Enabled="false" runat="server" Text='<%#Eval("colTransactionNumber") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Time" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="colDateTime" runat="server" Text='<%#Eval("colDateTime") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_DateTime" runat="server" Text='<%#Eval("colDateTime") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tran Name">
                        <ItemTemplate>
                            <asp:Label ID="colTran" runat="server" Text='<%#Eval("colTranName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Tran" runat="server" Text='<%#Eval("colTranName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone">
                        <ItemTemplate>
                            <asp:Label ID="lbl_RegisterPhone" runat="server" Text='<%#Eval("colRegisterPhone") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_RegisterPhone" runat="server" Text='<%#Eval("colRegisterPhone") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Full Name">
                        <ItemTemplate>
                            <asp:Label ID="lbl_RegisterFullName" runat="server" Text='<%#Eval("colRegisterFullName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_RegisterFullName" runat="server" Text='<%#Eval("colRegisterFullName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invitation code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Invitationcode" runat="server" Text='<%#Eval("colInvitationCode") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Invitationcode" runat="server" Text='<%#Eval("colInvitationCode") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Amount" runat="server" Text='<%#Eval("colAmount") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Amount" runat="server" Text='<%#Eval("colAmount") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Desc" runat="server" Text='<%#Eval("colDesc") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Error Description">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Status" runat="server" Text='<%#Eval("colStatus") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %>" Visible="false">
                        <ItemTemplate>
                            <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" CssClass="btn btn-primary" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" CssClass="btn btn-primary" />
                            <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn btn-secondary" />
                        </EditItemTemplate>
                        <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>" Visible="False">
                        <ItemTemplate>
                            <asp:Button ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete" CssClass="btn btn-secondary" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
            <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
            <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
        </div>
        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, Submit %>" OnClientClick="return checkValidation()" OnClick="btsave_Click" />
            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script>
    function myFunction() {
        confirm("Are you sure to do this transaction ? ");
    }

</script>

