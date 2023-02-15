<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PreviewImage.ascx.cs" Inherits="Controls_SearchTextBox_Reason" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<style>
    .border-faded{
         border-top:none !important;
    }
</style>
<div class="dropdown-search">
    <asp:UpdatePanel runat="server">

        <ContentTemplate>
                <asp:TextBox Visible="false" ID="txtImage" runat="server" Width="50%" ReadOnly="true"></asp:TextBox>
                <asp:HiddenField runat="server" ID="hdID" />
                <button type="button" visible="false" runat="server" id="btnPopup" class="search-popup">
                </button>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</div>
<asp:Panel runat="server" class="modal fade" ID="Image" ScrollBars="Auto" data-backdrop="static" role="dialog">
    <div class="modal-dialog <%=Size %>">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" style="float:left"><%=Resources.labels.image %></h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span></button>
            </div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        <div class="view-image" style="text-align:center">
                            <asp:Image ID="ImageView" runat="server" style="width: 100%; height: 100%;"/>
                        </div>
                  </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        </div>
</asp:Panel>
<%--<script type="text/javascript">
    $(document).ready(function showModal () {
        $("#Image").modal("show");
    });
</script>--%>