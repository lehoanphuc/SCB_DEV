<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSREGIONFEE_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

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
        <div id="divSearch">
            <div class="subheader">
                <h1 class="subheader-title">
                    <%=Resources.labels.updateworkingdate%>
                </h1>
            </div>
            <div class="panel-container form-horizontal p-b-0">
                <div class="wrap-collabsible">
                    <div class="SearchAdvance">
                        <div class="panel-container">
                           
                                    <div class="panel-content form-horizontal p-b-0" style="display: block;">
                                        <div class="row" style="margin-left: 2%">
                                            <div class="col-sm-7">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.currentworkingdate%></label>
                                                    <div class="col-sm-8 control-label">
                                                        <asp:Label ID="lbCurentWorkingdate" runat="server"></asp:Label>  
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-5">
                                                <asp:Button ID="btUpdate" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, capnhat %>" OnClick="btUpdate_Click" />
                                            </div>
                                        </div>
                                        
                                    </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
      
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>

<script>
    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }
</script>

