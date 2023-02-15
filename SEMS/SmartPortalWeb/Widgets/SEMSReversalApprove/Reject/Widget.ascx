<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSReversalApprove_reject_Widget" %>

<asp:ScriptManager runat="server" ID="ScriptManager1">
</asp:ScriptManager>

<br />
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div id="divSearch"  style="padding-left: 2%">
            <div class="subheader">
                <h1 class="subheader-title">
                    <%=Resources.labels.ART%>
                </h1>
            </div>
            <br />
            <br />
            <div class="panel-container form-horizontal p-b-0" style="margin-left: 2%">
                <div class="row">
                    <div class="col-sm-11">
                        <div class="form-group">
                            <label class="col-sm-4 control-label"><%=Resources.labels.ReasonRejectReversal%></label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtReasonReject" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
             
                <div class="row">
                    <div class="col-sm-11" style="text-align: center; margin:1%">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, Confirm%>" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, Cancel%>" OnClick="btncancel_click" />
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>




