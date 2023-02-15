<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCorpList_Add_Widget" %>


<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>
<script type="text/javascript" src="widgets/SEMSCustomerList/js/common.js"> </script>
<script type="text/javascript" src="widgets/SEMSCustomerList/js/commonjs.js"> </script>
<script type="text/javascript" src="widgets/SEMSCustomerListCorp/js/tabber.js"></script>
<script type="text/javascript" src="js/Validate.js"> </script>
<script src="/JS/Common.js"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<br />
<asp:UpdatePanel ID="UpdatePanel9" runat="server">
    <ContentTemplate>
        <div id="divCustHeader">
            <img alt="" src="widgets/SEMSCustomerList/Images/messenger.png" style="width: 32px; height: 32px; margin-bottom: 10px;" />
            <%=Resources.labels.themmoikhachangdoanhnghiep %>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <br />
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="0" AssociatedUpdatePanelID="pnCustInfo" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress6" DisplayAfter="0" AssociatedUpdatePanelID="pnCorp" runat="server">
                <ProgressTemplate>
                    <%--<img src="Images/tenor.gif" style="width: 32px; height: 32px;" />--%>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel runat="server" ID="pnCustInfo">
    <ContentTemplate>
        <div class="row" runat="server" id="Div7">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>
                        <%=Resources.labels.laythongtinkhachhangtucorebanking%>
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="Panel4" runat="server">
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.makhachhang %> </label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtPersioner" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-5"></div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, xemchitiet %>" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="CustomerInfor">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>
                        <%=Resources.labels.thongtinkhachhang%>
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="Panel6" runat="server">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.makhachhang %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtCustCodeInfo" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.tendaydu %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtFullName" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.tenviettat %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtShortName" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.loaikhachhang %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCustType" runat="server" Enabled="False" CssClass="form-control select2 infinity">
                                                <asp:ListItem Text="<%$ Resources:labels, doanhnghiep %>" Value="O" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="<%$ Resources:labels, linkage %>" Value="J"></asp:ListItem>
                                                <asp:ListItem Text="<%$ Resources:labels, canhan %>" Value="P"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.phone %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtMobi" onkeypress="return isNumberKeyNumer(event)" CssClass="form-control" runat="server" Enabled="false" OnTextChanged="OntextChangeMobi_Click" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Email</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.address %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtResidentAddr" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.ngaythanhlap%></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtBirth" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.gpkd %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtIF" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label "><%=Resources.labels.sofax %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtFax" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.ghichu %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtDesc" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.chinhanh %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlBranch" runat="server" Enabled="False" CssClass="form-control select2 infinity">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.region %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control select2"
                                                Width="100%" OnSelectedIndexChanged="ddlRegion_OnSelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.Townshipname %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlTownship" runat="server" CssClass="form-control select2" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                              <%-- <div class="col-sm-12" runat="server">
								<div class="form-group">
									<label class="col-sm-2 control-label"><%=Resources.labels.transactionalert %></label>
									<div class="col-sm-10">
										<div class="row">
											<div class="form-group custom-control">
												<div class="col-sm-3">
													<asp:CheckBox ID="cbSMS" runat="server" Text="<%$ Resources:labels, sms %>" Enabled="false" />
												</div>
												<div class="col-sm-3">
													<asp:CheckBox ID="cbWAPP" runat="server" Text="<%$ Resources:labels, whatsapp %>" Enabled="false" />
												</div>
												<div class="col-sm-3">
													<asp:CheckBox ID="cbLINE" runat="server" Text="<%$ Resources:labels, line %>" Enabled="false" />
												</div>
												<div class="col-sm-3">
													<asp:CheckBox ID="cbTELE" runat="server" Text="<%$ Resources:labels, telegram %>" Enabled="false" />
												</div>
											</div>
										</div>
									</div>
								</div>
                               </div>--%>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="ContractInfor">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>
                        <%=Resources.labels.thongtinhopdong%>
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="Panel7" runat="server">
                            <div class="row">
                                <div class="col-sm-6" style="display:none;">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.mahopdong %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtContractNo" CssClass="form-control" Enabled="True" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.loaihopdong %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlContractType" runat="server" Enabled="False" CssClass="form-control select2 infinity">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.ngayhieuluc %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtStartDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.ngayhethan %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtEndDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.loaihinhsanpham %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlProduct" runat="server" Enabled="True" CssClass="form-control select2 infinity" AutoPostBack="true" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.loaidanhnghiep %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCorpType" runat="server" CssClass="form-control select2 infinity">
                                                <asp:ListItem Value="<%$ Resources:labels, CONTRACTCORPMATRIX %>">Matrix</asp:ListItem>
                                                <asp:ListItem Value="<%$ Resources:labels, CONTRACTCORPADVANCE %>" >Advanced</asp:ListItem>
                                                <asp:ListItem Value="<%$ Resources:labels, CONTRACTCORPSIMPLE %>" Selected="True">Simple</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.autorenewlabel %></label>
                                        <div class="col-sm-8">
                                            <asp:CheckBox ID="chkRenew" runat="server" Checked="True" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="Div10">
            <asp:Panel runat="server" ID="pnbtnNext">
                <div id="divToolbar" style="text-align: center">
                    &nbsp;<asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="<%$ Resources:labels, next %>" CssClass="btn btn-primary" />
                    &nbsp;<asp:Button ID="Button4" runat="server" Text="<%$ Resources:labels, lamlai %>" CssClass="btn btn-secondary" Visible="false" />&nbsp
                </div>
            </asp:Panel>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnNext" />
    </Triggers>
</asp:UpdatePanel>

<asp:UpdatePanel runat="server" ID="pnCorp" Visible="false">
    <ContentTemplate>
        <div class="row" runat="server" id="divUserContractList">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>User contract list
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="pnSearchCust" runat="server">
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="form-group">
                                        <label class="col-sm-4">
                                            <asp:RadioButton ID="rdAdd" Checked="true" GroupName="MTAction" runat="server" CssClass=" form-check-input" AutoPostBack="true" OnCheckedChanged="MTAction_onChange" />
                                            New User 
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtMTCuscode" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-5" id="DivExistUser" runat="server" visible="false">
                                    <div class="form-group">
                                        <label class="col-sm-4">
                                            <asp:RadioButton ID="rdEdit" GroupName="MTAction" runat="server" CssClass=" form-check-input" AutoPostBack="true" OnCheckedChanged="MTAction_onChange" />
                                            Exist User 
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlUser" runat="server" Enabled="False" CssClass="form-control select2 infinity">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btnMatrixDetail" CssClass="btn btn-primary" runat="server" OnClick="btnMatrixDetail_Click" Text="<%$ Resources:labels, xemchitiet %>" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" runat="server" id="divUserInformation">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>User Information
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="PnUserInfor" runat="server">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"><%=Resources.labels.fullname %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtMTFullName" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6" runat="server">
                                    <label class="col-sm-4 control-label">Local Full Name</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtLocalFulname" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">

                                <div class="col-sm-6" runat="server">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.birthday %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtMTBirth" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6" runat="server" id="divMTGender">
                                    <label class="col-sm-4 control-label"><%=Resources.labels.gender %></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlMTGender" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                            <asp:ListItem Value="M" Text="<%$ Resources:labels, nam%>"></asp:ListItem>
                                            <asp:ListItem Value="F" Text="<%$ Resources:labels, nu%>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.phone %></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtMTPhone" onkeypress="return isNumberKeyNumer(event)" CssClass="form-control" runat="server" OnTextChanged="btnChangePhone_Click" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                                 <asp:CheckBox ID="cbIsForeign" runat="server" Enabled="true" Visible="false"></asp:CheckBox>
                                                <label class="control-label" style="visibility:hidden;">Foreign phone</label>                                                  
                                            </div>
                                    </div>
                                </div>

                                <div class="col-sm-6" id="divUserType" runat="server">
                                    <label class="col-sm-4 control-label">User Type</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlUserType" CssClass="form-control select2 infinity" AutoPostBack="true" OnSelectedIndexChanged="ddlUserType_TextChanged" Width="100%" runat="server">
                                            <asp:ListItem Value="CK" Text="Checker"></asp:ListItem>
                                            <asp:ListItem Value="MK" Text="Maker"></asp:ListItem>
                                            <asp:ListItem Value="AD" Text="Administrators"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.email %></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtMTEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label class="col-sm-4 control-label">
                                        <%=Resources.labels.address %></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtMTAddress" CssClass="form-control" placeholder="Address" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6" runat="server" visible="false">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.account %></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlIBAccount" CssClass="form-control select2 infinity" Height="28px" Width="100%" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                        <div class="col-sm-6" runat="server">
                                            <div class="form-group">
                                               <label class="col-sm-4 control-label required">Default Account</label>
                                                  <div class="col-sm-8">
                                                      <asp:DropDownList ID="ddlDefaultAccountQT" CssClass="form-control select2 infinity" Width="100%" runat="server" >
                                                         </asp:DropDownList>
                                                  </div>
                                             </div>
                                         </div>                                                       
                             </div>
                            <br />
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="div1">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>Account Information
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="pnAccountInfo" runat="server">
                            <div class="row">
                                <div class="col-sm-6" runat="server">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label required"><%=Resources.labels.account %></label>
                                        <div class="col-sm-8 col-xs-9 pading0">
                                            <asp:ListBox ID="lstAccount" runat="server" CssClass="form-control depchange divAcount"
                                                AutoPostBack="True" OnSelectedIndexChanged="lstAccount_SelectedIndexChanged"></asp:ListBox>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group ">
                                        <div class="col-sm-12">
                                            <div class="custom-control custom-control1 overf_auto">
                                                <asp:TreeView ID="tvRole" runat="server" OnTreeNodeCheckChanged="tvRole_OnSelectedIndexChanged">
                                                    <SelectedNodeStyle Font-Bold="True" Font-Underline="True" />
                                                    <NodeStyle CssClass="p-l-10" />
                                                </asp:TreeView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6" id="div2" runat="server">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label"></label>
                                        <div class="col-sm-8">
                                            <asp:CheckBox ID="isWallet" runat="server" OnCheckedChanged="IsWallet_OnCheckedChaned" AutoPostBack="true" Visible="false"/>
                                        </div>
                                        <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="pnCorp" runat="server">
                                            <ProgressTemplate>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                                <div class="col-sm-6" runat="server" visible="false">
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="divServiceRole">
            <div class="nav-tabs-custom">
                <ul class="nav nav-tabs">
                    <li class="active" id="li2" runat="server"><a href="#tabview_1" data-toggle="tab"><%=Resources.labels.internetbanking %></a></li>
<%--                    <li id="li1" runat="server"><a href="#tabview_2" data-toggle="tab"><%=Resources.labels.mobilebanking %></a></li>--%>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="tabview_1">
                        <div class="panel" id="Div11" runat="server">
                            <div class="panel-container">
                                <div class="panel-content form-horizontal p-b-0">
                                    <asp:Panel ID="PnIB" runat="server">
                                        <div class="row">
                                            <div class="col-sm-12" style="background-color: #F5F5F5">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <asp:Label class="col-sm-4 control-label" runat="server"><%=Resources.labels.thongtindangnhap %></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6" style="background-color: #F5F5F5; color: #38277c;" runat="server" visible="false">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.quyensudung %></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label required"><%=Resources.labels.tendangnhap %></label>
                                                    <div class="col-sm-8">
                                                        <div class="form-group">
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtIBTypeUserName" runat="server" CssClass="form-control" onkeypress="return isKey(event)" OnTextChanged="ChangeUserName_TextChanged" MaxLength="50" AutoPostBack="true" placeholder="User Name"></asp:TextBox>
                                                            </div>
                                                            <asp:LinkButton ID="lbIBGenerate" runat="server" OnClick="CreateUserName_Click">Generate</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label required"><%=Resources.labels.dungpolicy %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlPolicyIB" runat="server" Width="100%" CssClass="form-control select2 infinity" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6" rowspan="4" runat="server" visible="false">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <div class="custom-control custom-control overf_auto">
                                                            <asp:TreeView ID="tvIBRole" runat="server">
                                                                <SelectedNodeStyle Font-Bold="True" Font-Underline="True" />
                                                                <NodeStyle CssClass="p-l-10" />
                                                            </asp:TreeView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="tab-pane" id="tabview_2">
                        <div class="panel" id="Div9" runat="server">
                            <div class="panel-container">
                                <div class="panel-content form-horizontal p-b-0">
                                    <asp:Panel ID="pnMobile" runat="server">
                                        <div class="row">
                                            <div class="col-sm-12" style="background-color: #F5F5F5; color: #38277c;">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <asp:Label class="col-sm-4 control-label" runat="server"><%=Resources.labels.thongtindangnhap %></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6" style="background-color: #F5F5F5; color: #38277c;" runat="server" visible="false">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.quyensudung %></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label required"><%=Resources.labels.username %></label>
                                                    <div class="col-sm-8">
                                                        <div class="form-group">
                                                            <div class="col-sm-12">
                                                                <asp:TextBox ID="txtUserNameMB" runat="server" CssClass="form-control" MaxLength="20" AutoPostBack="true" placeholder="User Name"></asp:TextBox>
                                                            </div>
                                                            <asp:LinkButton ID="lbMBGenerate" runat="server" OnClick="CreateUserName_Click">Generate</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label required"><%=Resources.labels.phone %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtPhoneMB" runat="server" CssClass="form-control" MaxLength="10" AutoPostBack="true" placeholder="Phone Numbner" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label required"><%=Resources.labels.loginmethod %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlLoginMethod" CssClass="form-control select2 infinity" Width="100%" runat="server" Enabled="false">
                                                            <asp:ListItem Value="USERNAME" Text="User Name" Selected="True"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group" runat="server" visible="false">
                                                    <label class="col-sm-4 control-label required"><%=Resources.labels.authentype %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlauthenType" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                            <asp:ListItem Value="PASSWORD" Text="Password"></asp:ListItem>
                                                            <asp:ListItem Value="PINCODE" Text="Pincode"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label required"><%=Resources.labels.dungpolicy %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlPolicyMB" runat="server" CssClass="form-control select2 infinity" Width="100%" AutoPostBack="true" value="1"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 custom-control" rowspan="4" runat="server" visible="false">
                                                <div class="form-group ">
                                                    <div class="col-sm-12">
                                                        <div class="custom-control custom-control">
                                                            <asp:TreeView ID="tvMB" runat="server">
                                                                <SelectedNodeStyle Font-Bold="True" Font-Underline="True" />
                                                                <NodeStyle CssClass="p-l-10" />
                                                            </asp:TreeView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" runat="server" id="div12" style="margin-top: 10px">
                <div class="col-sm-12">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanelAddUser">
                        <ProgressTemplate>
                            <div style="text-align: center">
                                <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                                <%=Resources.labels.loading %>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanelAddUser" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnAddNewUser" />
                            <asp:PostBackTrigger ControlID="btnAddUserMT" />
                            <asp:PostBackTrigger ControlID="btnMTCancel" />
                        </Triggers>
                        <ContentTemplate>
                            <div style="text-align: right;">
                                <asp:Button ID="btnAddNewUser" runat="server" Text="Add New User" OnClick="btnAddNewUser_Click" CssClass="btn btn-primary" />
                                &nbsp;
                                            <asp:Button ID="btnAddUserMT" runat="server" Text="Save Current User" CssClass="btn btn-primary" OnClick="btnAddUserMT_Click" />
                                &nbsp;
                                            <asp:Button ID="btnMTCancel" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, delete %>" OnClick="btndelete_Click" />
                                &nbsp;
                            </div>
                            <div id="div8" style="margin: 20px 5px 5px 5px; height: 150px; overflow: auto;" runat="server" visible="false">
                                <asp:GridView ID="gvMTUser" CssClass="table table-hover" runat="server" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" Width="100%">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField HeaderText="<%$ Resources:labels, tendaydu %>" DataField="colFullName" />
                                        <asp:BoundField DataField="colIBUserName" HeaderText="<%$ Resources:labels, username %>" />
                                        <asp:BoundField HeaderText="<%$ Resources:labels, taikhoan %>" DataField="colAccount" />
                                        <asp:BoundField HeaderText="<%$ Resources:labels, quyensudung %>" DataField="colRole" />
                                    </Columns>
                                    <FooterStyle CssClass="gvFooterStyle" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                                    <SelectedRowStyle />
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="rdEdit" />
        <asp:AsyncPostBackTrigger ControlID="rdAdd" />
        <asp:AsyncPostBackTrigger ControlID="ddlUserType" />
    </Triggers>
</asp:UpdatePanel>
<div class="row" runat="server" id="btnConfirm" visible="false">
    <asp:Panel runat="server" ID="pnLuu">
        <div style="text-align: center; padding-top: 10px;">
            <asp:Button ID="btnCustSave" runat="server" Text="<%$ Resources:labels, save %>" CssClass="btn btn-primary" OnClick="btnCustSave_Click" />
            &nbsp;
            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="<%$ Resources:labels, back %>" CssClass="btn btn-secondary" />
            &nbsp; &nbsp;
        </div>
    </asp:Panel>
</div>
<script>
    function enableMTEdit() {
        document.getElementById("<%=txtMTCuscode.ClientID %>").disabled = true;
        document.getElementById("<%=ddlUser.ClientID %>").disabled = false;
    }
    function isKey(evt) {
        var regex = new RegExp("[A-Za-z0-9]");
        var key = String.fromCharCode(event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    }
</script>

<script type="text/javascript">
    function postBackByObject() {
        var o = window.event.srcElement;
        debugger;
        if (o.tagName == "INPUT" && o.type == "checkbox") {
            __doPostBack("<%=pnAccountInfo.ClientID %>", "");
        }
    }
</script>

<style>
    .depchange option {
        padding: 5px;
    }

    .depchange {
        padding: 0;
    }

    .overf_auto {
        max-height: 150px;
        overflow: auto;
    }

    .divAcount {
        min-height: 180px;
    }
</style>
