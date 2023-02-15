<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoadCity.ascx.cs" Inherits="Controls_SearchTextBox_City" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<style>
    .border-faded {
        border-top: none !important;
    }
</style>
<div class="dropdown-search">
    <asp:UpdatePanel runat="server">

        <ContentTemplate>
            <asp:TextBox ID="txtCity" runat="server" Width="86%" ReadOnly="true" style="padding-left: 8px; outline:none" BorderStyle="Solid"  BorderColor="#E5E5E5" ></asp:TextBox>
            <asp:HiddenField runat="server" ID="hdID" />
            <asp:HiddenField runat="server" ID="hdfCitySearchValue" />
            <asp:HiddenField runat="server" ID="hdfCitySearchText" />
            <asp:HiddenField runat="server" ID="hdfCityNameSearchText" />

            <button type="button" runat="server" id="btnPopup" class="search-popup fa fa-search" style="background: transparent; border: none; box-shadow: none"
                data-toggle="modal" data-target="#CityCode">
            </button>
        </ContentTemplate>
    </asp:UpdatePanel>

</div>
<asp:Panel runat="server" class="modal fade" ID="CityCode" data-backdrop="static" role="dialog">
    <div class="modal-dialog <%=Size %>">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title"><%=Resources.labels.thanhpho %></h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span></button>
            </div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        <div class="row">
                            <div class="panel-container">
                                <div class="panel-content form-horizontal p-b-0">
                                    <%-- Condition search --%>
                                    <div class="form-group">
                                        <div class="row col-sm-12">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-5 control-label"><%=Resources.labels.cityid%></label>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="txtCityCode" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-5 control-label"><%=Resources.labels.cityname%></label>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="txtcityname" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                                <asp:Button ID="btnSearch" runat="server" data-close="<%CityCode.ClientID%>" CssClass="btn btn-primary" Text="<%$ Resources:labels, timkiem %>"
                                                    OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <%-- Result notifycation --%>
                                    <div class="result">
                                        <div class="error">
                                            <asp:Label runat="server" ID="lblError" />
                                        </div>
                                    </div>
                                    <%-- Table view result --%>
                                    <asp:Repeater runat="server" ID="rptData">
                                        <HeaderTemplate>
                                            <div class="pane">
                                                <div class="table-responsive">
                                                    <table class="table table-hover footable c_list">
                                                        <thead class="thead-light">
                                                            <tr>
                                                                <th>
                                                                    <input name='<%="item"+CityCode.ClientID%>' data-control='<%=hdID.ClientID %>' data-display='<%=hdfCitySearchText.ClientID %>' data-name='<%=hdfCityNameSearchText.ClientID %>' style="visibility: hidden" type="checkbox" onclick="CheckboxAll(this)">
                                                                </th>
                                                                <th><%=Resources.labels.cityid%></th>
                                                                <th><%=Resources.labels.cityname%></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="tdcheck">
                                                    <input name='<%="item"+CityCode.ClientID%>' class="check keepvalue" value="<%#Eval("CityId") %>" data-text="<%#Eval("CityId") %>" data-name="<%#Eval("CityName") %>" onclick="ConfigRatioPlus(this)" type="radio">
                                                </td>
                                                <td><%#Eval("CityId") %></td>
                                                <td><%#Eval("CityName") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                        </table>
                                        </div> </div>
                                         <div class="margintop20 button-group panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                             <asp:Button runat="server" class="btn btn-primary" data-check="itemBranch111" ID="btnOK" OnClick="btnOK_Click" data-close="<%=CityCode.ClientID%>" Text='<%$Resources:labels,ok %>' />
                                             <button type="button" class="btn btn-primary" data-dismiss="modal"><%=Resources.labels.cancel %></button>
                                         </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <uc1:GridViewPaging ID="gidview" runat="server"></uc1:GridViewPaging>
                                </div>
                            </div>
                            <%-- Button --%>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Panel>
<script>

</script>
