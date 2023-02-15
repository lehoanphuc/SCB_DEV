<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridViewPaging.ascx.cs" Inherits="Controls_WidgetHTML_GridViewPaging" %>
<asp:HiddenField ID="hdfCurrentPage" runat="server" />
<asp:HiddenField ID="TotalRows" runat="server" Value="0" />
<div class="col-xs-12">
    <div class="col-sm-3 col-xs-12 " style="padding-top: 10px;">
        <div class="form-group">
            <div class="row">
                <label class="col-sm-7 col-xs-5 control-label right"><%=Resources.labels.pagesize %></label>
                <div class="col-sm-5 col-xs-7" style="padding-left: 0px">
                    <asp:UpdatePanel runat="server">
                        <Triggers></Triggers>
                        <ContentTemplate>
                            <asp:DropDownList ID="PageRowSize" AutoPostBack="true" runat="server" CssClass="form-control select2 infinity" OnSelectedIndexChanged="PageRowSize_onclick">
                                <asp:ListItem Selected="True">15</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-4 col-xs-12 center" style="padding-top: 15px">
        <div class="form-group">
            <asp:Label ID="RecordDisplaySummary" runat="server"></asp:Label>
            <asp:Label ID="PageDisplaySummary" Visible="False" CssClass="control-label" runat="server"></asp:Label>
        </div>
    </div>
    <div class="col-sm-5 col-xs-12 mb-center" style="padding-top: 10px">
        <div class="row">
            <div class="col-sm-5 col-xs-5">
                <div class="form-group right">
                    <asp:LinkButton ID="First" CssClass="btn btn-secondary btn-paging" OnClick="First_Click" runat="server">
                        <span class="fa fa-arrow-left"></span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="Previous" CssClass="btn btn-secondary btn-paging" OnClick="Previous_Click" runat="server">
                        <span class="fa fa-chevron-left"></span>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="col-sm-2 col-xs-2 center">
                <div class="form-group">
                    <asp:TextBox ID="SelectedPageNo" oncopy="return false" onpaste="return false" oncut="return false" runat="server" CssClass="form-control center" OnTextChanged="SelectedPageNo_TextChanged" onKeyPress="return onlyDotsAndNumbers(this, event,3);" AutoPostBack="True" MaxLength="8"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-5 col-xs-5">
                <div class="form-group">
                    <asp:LinkButton ID="Next" CssClass="btn btn-secondary btn-paging" OnClick="Next_Click" runat="server">
                        <span class="fa fa-chevron-right"></span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="Last" CssClass="btn btn-secondary btn-paging" OnClick="Last_Click" runat="server">
                        <span class="fa fa-arrow-right"></span>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</div>
<div>
    <div class="col-sm-12" style="padding-top: 10px">
        <asp:Label ID="GridViewPagingError" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="Red"></asp:Label>
    </div>
</div>


