<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FormulaDevExpress.ascx.cs" Inherits="Controls_Formula_Formula" %>
<%--<%@ Register Assembly="DevExpress.Web.ASPxFilterControl.v21.2, Version=21.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>--%>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%--<link href="../../CSS/formula/style.css" rel="stylesheet" />--%>
<style>
    #overlay {
        display: none;
        position: absolute;
        top: 0;
        bottom: 0;
        background: #999;
        width: 100%;
        height: 100%;
        opacity: 0.8;
        z-index: 100;
    }

    #popup {
        display: none;
        position: absolute;
        top: 50%;
        left: 50%;
        background: #fff;
        width: 1000px;
        min-height: 300px;
        transform: translateX(-50%) translateY(-50%);
        z-index: 200;
    }

    #popupclose {
        float: right;
        padding: 10px;
        cursor: pointer;
    }

    .popupcontent {
        padding: 10px;
    }

    #button {
        cursor: pointer;
    }
</style>
<asp:UpdatePanel ID="updatepnl" runat="server">
    <ContentTemplate>
        <asp:Panel ID="Panel1" runat="server">
            <div class="row">
                <div class="col-sm-5 col-xs-12 filter">
                    <div class="form-group">
                        <label class="col-sm-4 col-xs-12 control-label">Formula Name</label>
                        <div class="col-sm-8 col-xs-12">
                            <asp:TextBox runat="server" ID="txtName" CssClass="form-control required" />
                        </div>
                    </div>
                    <dx:ASPxFilterControl ID="filter" runat="server" SkinID="" OnOperationVisibility="filter_FilterOperationVisibility"
                        ClientInstanceName="filter" ViewMode="Visual">
                        <%--    <Columns>
                            <dx:FilterControlCheckColumn ColumnType="Boolean" DisplayName="Birthday" PropertyName="BIRTHDAY" />
                            <dx:FilterControlCheckColumn ColumnType="Boolean" DisplayName="Birthmonth" PropertyName="BIRTHMONTH" />
                        </Columns>--%>
                    </dx:ASPxFilterControl>

                    <div class="button-group">
                        <dx:ASPxButton runat="server" ID="btnApply" Text="Apply" OnClick="btnApply_Click"
                            UseSubmitBehavior="false" CssClass="btn btn-primary">
                        </dx:ASPxButton>
                        <br />
                        <br />
                    </div>
                </div>
                <div class="col-sm-7 col-xs-12">
                    <asp:Panel runat="server" ID="pnFieldName" Visible="true" CssClass="">
                        <div class="form-horizontal">
                            <div class="subheader">
                                <h1 class="subheader-title">Add more condition</h1>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label">Field Name</label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:DropDownList ID="ddlFieldName" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                <asp:ListItem Text="First Transaction" Value="FIRSTTRAN" />
                                                <asp:ListItem Text="First Receiver" Value="FIRSTRCV" />
                                                <asp:ListItem Text="Total Transaction" Value="TOTALTRAN" />
                                                <asp:ListItem Text="Total Amount" Value="TOTALAMT" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnAdd" CssClass="btn btn-primary" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="gvFieldName" CssClass="table table-hover" runat="server"
                                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%"
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField HeaderText="Key" DataField="ID" />
                                    <asp:BoundField HeaderText="Name" DataField="DISPLAYNAME" />
                                    <asp:BoundField HeaderText="Description" DataField="DESC" />
                                    <asp:BoundField HeaderText="Expression" DataField="EXPRESSION" />
                                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                                        <ItemTemplate>
                                            <input type="button" onclick="editExpression('<%#Eval("ID")%>')" class="btn btn-primary" value="Edit" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                                        <ItemTemplate>
                                            <input type="button" onclick="deleteExpression('<%#Eval("ID")%>')" class="btn btn-secondary" value="Delete" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                            <asp:Button runat="server" ID="btnEditFieldName" ClientIDMode="Static" Text="Edit" OnClick="btnEditFieldName_Click" CssClass="hidden" />

                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12" style="margin-bottom:20px">
                    <asp:Label runat="server" ID="lblFilter" ForeColor="Red" />
                </div>
            </div>
        </asp:Panel>



        <div id="overlay" onclick="closePopup();"></div>
        <div id="popup">
            <div class="popupcontrols">
                <span id="popupclose" onclick="closePopup();">X</span>
            </div>
            <div class="popupcontent">

                <asp:Label runat="server" ID="lblPopupError" ForeColor="Red" />
                <div class="row">
                    <div class="col-sm-5 col-xs-12">
                        <div class="form-group">
                            <label class="col-sm-4 col-xs-12 control-label">Field Name</label>
                            <div class="col-sm-8 col-xs-12">
                                <asp:TextBox runat="server" ID="txtSubName" CssClass="form-control required" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-5 col-xs-12">
                        <div class="form-group">
                            <label class="col-sm-4 col-xs-12 control-label">Description</label>
                            <div class="col-sm-8 col-xs-12">
                                <asp:TextBox runat="server" ID="txtDesc" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-1 col-xs-12">
                        <div class="form-group">
                            <dx:ASPxButton runat="server" ID="btnApplyFieldName" Text="Apply" OnClick="btnApplyFieldName_Click"
                                UseSubmitBehavior="false" Width="80px" CssClass="btn btn-primary">
                                <%--<ClientSideEvents Click="function() { condition.Apply();return closePopup(); }" />--%>
                            </dx:ASPxButton>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12">
                        <dx:ASPxFilterControl ID="condition" runat="server" ClientInstanceName="condition" OnOperationVisibility="filter_FilterOperationVisibility"
                            ViewMode="Visual">
                            <Columns>
                                <%--  <dx:FilterControlComboBoxColumn DisplayName="Transaction Type" PropertyName="IPCTRANCODE" />
                                <dx:FilterControlColumn DisplayName="Peroid Time" PropertyName="PERIODTIME" />
                                <dx:FilterControlColumn DisplayName="Biller" PropertyName="BILLERID" />--%>
                            </Columns>
                            <ClientSideEvents Applied="function(s,e){alert(e.filterExpression)}" />
                        </dx:ASPxFilterControl>
                    </div>
                </div>
                <%--<input type="button" id="btnApplySub" value="Apply" onclick="filterSubApply();" />--%>
            </div>
            <asp:HiddenField runat="server" ID="lblFieldName" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="hdfCheck" ClientIDMode="Static" />
            <%--<asp:Label runat="server" ID="lblFieldName" ClientIDMode="Static" CssClass="" />--%>
        </div>
        <script type="text/javascript">

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        showPopup();
                    }
                });
            };

            function editExpression(fieldName) {
                document.getElementById("lblFieldName").value = fieldName;
                document.getElementById("btnEditFieldName").click();
            }
            // Close Popup Event
            function closePopup(replaceIndex, fieldName) {
                document.getElementById("hdfCheck").value = 'Notshowing';
                overlay.style.display = 'none';
                popup.style.display = 'none';
            };

            function showPopup() {
                var overlay = document.getElementById("overlay");
                var popup = document.getElementById("popup");
                var fieldName = document.getElementById("lblFieldName").value;
                var check = document.getElementById("hdfCheck").value;

                if ((fieldName.startsWith("FIRSTTRAN") || fieldName.startsWith("TOTALTRAN")
                    || fieldName.startsWith("TOTALAMT") || fieldName.startsWith("FIRSTRCV")
                    || fieldName.startsWith("TOTALAMTRCV") || fieldName.startsWith("TOTALTRANRCV"))
                    && check === 'Show') {
                    overlay.style.display = 'block';
                    popup.style.display = 'block';
                }
            }
        </script>
    </ContentTemplate>
</asp:UpdatePanel>




