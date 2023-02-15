<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSPRODUCTPROMOTIONAPP_Controls_Widget" %>
<%@ Register Src="~/Controls/Formula/FormulaDevExpress.ascx" TagPrefix="uc1" TagName="FormulaDevExpress" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleProduct" runat="server"></asp:Label>
            </h1>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
        <asp:HiddenField ID="hdfstatus" runat="server" />
        <asp:HiddenField runat="server" ID="hdfFormularID" />
        <asp:Panel ID="pnDiscountInfo" runat="server">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.promotioninfor%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.promotionname %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID ="txtPromotionName" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.productname %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlProduct" CssClass="form-control select2 infinity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFeeType_OnChange">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12"></div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.TransactionName %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlTransactionType" CssClass="form-control select2 infinity" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.promotionside %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlPromotionSide" CssClass="form-control select2 infinity" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12"></div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.contractlevel %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlContractLevel" CssClass="form-control select2 infinity" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.status %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlStatus" Enabled="False" CssClass="form-control select2 infinity" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12"></div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.promotiontype %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlpromotiontype" OnSelectedIndexChanged="ddlpromotiontype_SelectedIndexChanged" AutoPostBack="true" Enabled="False" CssClass="form-control select2 infinity" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.usecount %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID ="txtUseCount" runat="server" CssClass="form-control" onkeypress=" return isNumberKeyNumer(event)" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class ="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.BeneficiarySide %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlBeneficiarySide" CssClass="form-control select2" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.promotiondesc %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtdiscountdes" CssClass="form-control" MaxLength="22" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 control-label"><%=Resources.labels.kieudatlich %></label>
                                            <div class="col-sm-10 col-xs-12">
                                                <asp:RadioButtonList ID="radSchedule" CssClass="custom-control" Width="100%" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="radSchedule_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnDaily" runat="server" Visible="False">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.hangngay%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tungay %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtFromD" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.denngay %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtToD" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.fromtime %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlhourDFrom" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.gio %></label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlminuteDFrom" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.phut %></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.totime %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlhourDTo" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.gio %></label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlminuteDTo" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.phut %></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnWeekly" runat="server" Visible="False">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.hangtuan%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tungay %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtFromW" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.denngay %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtToW" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.fromtime %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlhourWFrom" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.gio %></label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlminuteWFrom" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.phut %></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.totime %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlhourWTo" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.gio %></label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlminuteWTo" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.phut %></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 control-label required"><%=Resources.labels.chonthu %></label>
                                            <div class="col-sm-10 col-xs-12">
                                                <asp:CheckBoxList ID="cblThu" runat="server" CssClass="custom-control" Width="100%" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Text="<%$ Resources:labels, chunhat %>"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="<%$ Resources:labels, thuhai %>"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="<%$ Resources:labels, thuba %>"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="<%$ Resources:labels, thutu %>"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="<%$ Resources:labels, thunam %>"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="<%$ Resources:labels, thusau %>"></asp:ListItem>
                                                    <asp:ListItem Value="7" Text="<%$ Resources:labels, thubay %>"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnMonthly" runat="server" Visible="True">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.hangthang%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tungay %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtFromM" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.denngay %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtToM" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.fromtime %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlhourMFrom" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.gio %></label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlminuteMFrom" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.phut %></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.totime %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlhourMTo" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.gio %></label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlminuteMTo" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.phut %></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 control-label required"><%=Resources.labels.chonngay %></label>
                                            <div class="col-sm-10 col-xs-12">
                                                <asp:CheckBoxList ID="cblThuM" runat="server" CssClass="custom-control" Width="100%" RepeatDirection="Horizontal" RepeatColumns="16">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnOnetime" runat="server" Visible="False">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.motlan%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <label class="col-sm-2 col-xs-12 control-label required"><%=Resources.labels.thoidiem %></label>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-6 col-xs-12">
                                                <asp:TextBox ID="txtDateO" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 col-xs-12"></div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.fromtime %></label>
                                            <div class="col-sm-2 col-xs-12">
                                                <asp:DropDownList ID="ddlHourFrom" CssClass="form-control select2" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-sm-1 control-label"><%=Resources.labels.gio %></label>
                                            <div class="col-sm-2 col-xs-12">
                                                <asp:DropDownList ID="ddlMinuteFrom" CssClass="form-control select2" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-sm-1 control-label"><%=Resources.labels.phut %></label>
                                        </div>
                                    </div>

                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.totime %></label>
                                            <div class="col-sm-2 col-xs-12">
                                                <asp:DropDownList ID="ddlHourTo" CssClass="form-control select2" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-sm-1 control-label"><%=Resources.labels.gio %></label>
                                            <div class="col-sm-2 col-xs-12">
                                                <asp:DropDownList ID="ddlMinuteTo" CssClass="form-control select2" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-sm-1 control-label"><%=Resources.labels.phut %></label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <!--Formula-->
        <asp:Panel ID="pnFormula" runat="server">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.formula %>
                            </h2>
                        </div>

                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <uc1:FormulaDevExpress runat="server" ID="FormulaDevExpress" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnDiscountDetail">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.promotiondetail%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-6 col-xs-12 control-label required"><%=Resources.labels.from %></label>
                                            <div class="col-sm-6 col-xs-12">
                                                <asp:TextBox CssClass="form-control" runat="server" ID="txtFromDiscount"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.to %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox CssClass="form-control" runat="server" ID="txtToDiscount"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-1 col-xs-12 martop9">
                                                <asp:RadioButton runat="server" ID="rdFixed" OnCheckedChanged="rdFixed_OnCheckedChanged" AutoPostBack="True" GroupName="rdDiscountdtl" />
                                            </div>
                                            <label class="col-sm-5 col-xs-12 control-label required "><%=Resources.labels.fixedPromotion %></label>
                                            <div class="col-sm-6 col-xs-12">
                                                <asp:TextBox CssClass="form-control" runat="server" ID="txtfixedDiscount"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-1 col-xs-12 martop9">
                                                <asp:RadioButton runat="server" OnCheckedChanged="rdRate_OnCheckedChanged" ID="rdRate" AutoPostBack="True" GroupName="rdDiscountdtl" />
                                            </div>
                                            <label class="col-sm-5 col-xs-12 control-label required"><%=Resources.labels.rate %> %</label>
                                            <div class="col-sm-6 col-xs-12">
                                                <asp:TextBox CssClass="form-control" runat="server" ID="txtRateDetails"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required "><%=Resources.labels.maxamountpromotion %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox CssClass="form-control" runat="server" ID="txtMaxamount"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-12 col-xs-12">
                                                <asp:Button ID="Button1" runat="server" Text="<%$ Resources:labels, them %>" CssClass="btn btn-primary" OnClick="btnSaveDetails_Click" OnClientClick="return validate();" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="pnGV" runat="server" Visible="True">
                            <div id="divResult">
                                <asp:GridView ID="gvDiscountDetails" CssClass="table table-hover" runat="server" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                    Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                                    OnRowDataBound="gvDiscountDetails_OnRowDataBound" PageSize="200"
                                    OnPageIndexChanging="gvDiscountDetails_PageIndexChanging"
                                    OnRowDeleting="gvDiscountDetails_OnRowDeletingeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="DiscountId" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDiscountId" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, tu %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfrom" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, den %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblto" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, fixedPromotion %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfixedDiscount" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, promotionrate %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldiscountrate" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, maxpromotion %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmaxdiscount" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbDeleteDiscountdtl" runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' OnClientClick="Loading(); return Confirm();">Delete</asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                                </asp:GridView>
                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                            </div>
                        </asp:Panel>
                    </div>

                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnPaySide" runat="server" Visible="true">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.mpupayside%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <%--<asp:RadioButton runat="server" ID="rdpaysidefixamount" Text="<%$ Resources:labels, fixedamount %>" GroupName="rdpayside" />--%>
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.payside %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlPayside" CssClass="form-control select2" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <%--<asp:RadioButton runat="server" ID="rdpaysidePercent" Text="<%$ Resources:labels, Percentage %>" GroupName="rdpayside" />--%>
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.ShareType %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlShareType" CssClass="form-control select2" Width="100%" AutoPostBack="True" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-12 col-xs-12">
                                                <asp:Button ID="btnsaveconnect" runat="server" Text="<%$ Resources:labels, them %>" CssClass="btn btn-primary" OnClientClick="return validate();" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.FlatAmount %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtFlatAmount" MaxLength="24" CssClass="form-control" onkeypress="return FlatNumberFixed(this,event,24)" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 col-xs-12" runat="server">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.Percentage%></label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtPercentage" MaxLength="6" CssClass="form-control" onkeypress="return FormatPercent2(this,event,6)" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1" style="padding-left: 6px; padding-top: 7px">
                                                <label class="fa fa-percent">
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.Priority %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPriority" CssClass="form-control" Style="width: 100%;" MaxLength="2" onKeyPress="return onlyDotsAndNumbers(this, event,3);" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 col-xs-12" runat="server">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="Panel1" runat="server" Visible="True">
                            <div id="divResult">
                                <asp:GridView ID="gvDiscountCollect" CssClass="table table-hover" runat="server" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                    Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                                    OnRowDataBound="gvDiscountCollect_OnRowDataBound" PageSize="200"
                                    OnPageIndexChanging="gvDiscountCollect_OnPageIndexChanging"
                                    OnRowDeleting="gvDiscountCollect_OnRowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="DiscountId" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDiscountId" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, payside %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPayside" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="<%$ Resources:labels, ShareType %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShareType" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, FlatAmount %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFlatAmount" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, Percentage %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPercentage" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, Priority %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriority" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbDeleteDiscountdtl" runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' OnClientClick="Loading(); return Confirm();">Delete</asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                                </asp:GridView>
                                <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlrejectReason">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.rejectreason%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <label class="col-sm-2 col-xs-12 control-label required"><%=Resources.labels.rejectreason %></label>
                                    <div class="col-sm-10 col-xs-12">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtRejectReson" TextMode="MultiLine" CssClass="form-control " runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
            <asp:Button ID="btnApprove" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, approve %>" OnClientClick="Loading()" OnClick="btnApprove_OnClick" />
            <asp:Button ID="btnReject" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, reject %>" OnClientClick="Loading();" OnClick="btnReject_OnClick" />
            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" OnClientClick="Loading();" />
        </div>



    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function validate() {

        return true;
    }

    function validate1() {

        return true;
    }

    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }
    function isNumberKeyNumer(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    };
    function Confirm() {
        return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
    }

    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    function SelectCbx(obj) {
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != '') {
                    elements[i].checked = true;
                    //elements[i].parentNode.parentNode.className="hightlight";
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != '') {
                    elements[i].checked = false;
                    //elements[i].parentNode.parentNode.className="nohightlight";
                }
            }
        }
    }


</script>
<style>
    .paddingright0 {
        padding-right: 0;
        padding-left: 13px;
    }

    .rdmargin input {
        margin-right: 10px;
    }

    .martop9 input {
        margin-top: 9px;
    }
</style>
