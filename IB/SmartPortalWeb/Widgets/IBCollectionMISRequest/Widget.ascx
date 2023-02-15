<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_CollectionMISRequest_Widget" %>



<script src="JS/mask.js" type="text/javascript"></script>

<script src="JS/docso.js" type="text/javascript"></script>
<div class="al">
    <span>Collection MIS Request</span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<asp:Panel ID="pnTIB" runat="server">
    <div style="text-align: center; color: Red;">
        <asp:Label runat="server" Font-Bold="true" ID="lblTextError"></asp:Label>
    </div>
    <div class="divcontent">
        <div class="handle">
            <label class="bold"><%= Resources.labels.chitietgiaodich %></label>
        </div>
        <div class="content_table">
            <div class="row">
                <label class="col-sm-2 col-xs-5 bold"><%= Resources.labels.corporates %></label>
                <div class="col-sm-4 col-xs-7">
                    <asp:TextBox ID="txtCorpName" Enabled="false" runat="server"></asp:TextBox>

                    <asp:HiddenField ID="hdfCorpID" runat="server" />
                </div>
                <label class="col-sm-2 col-xs-5 bold"><%= Resources.labels.dichvu %></label>
                <div class="col-sm-4 col-xs-7">
                    <asp:DropDownList ID="ddlservice" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <label class="col-sm-2 col-xs-5 bold"><%= Resources.labels.email %></label>
                <div class="col-sm-4 col-xs-7">
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <label class="col-sm-2 col-xs-5 bold"><%= Resources.labels.fromdate %></label>
                <div class="col-sm-4 col-xs-7">
                    <asp:TextBox ID="txtFromDateLN" CssClass="dateselect" runat="server"></asp:TextBox>
                </div>
                <label class="col-sm-2 col-xs-5 bold"><%= Resources.labels.todate %></label>
                <div class="col-sm-4 col-xs-7">
                    <asp:TextBox ID="txtToDateLN" CssClass="dateselect" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <div style="text-align: center; margin-top: 10px;">
            <asp:Button ID="btnRequest" CssClass="btn btn-primary" runat="server" OnClientClick="return validate();" Text='<%$ Resources:labels, yeucau %>'
                OnClick="btnRequest_Click" />
        </div>
    </div>
    <script type="text/javascript">    //<![CDATA[
        Onload();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            Onload();
        }

        function Onload() {
            $('.dateselect').datepicker({
                autoclose: true,
                format: 'dd/mm/yyyy',
                language: 'en',
                todayBtn: "linked"
            });
        }


        function validate() {
            if (validateEmpty('<%=txtEmail.ClientID %>', '<%=Resources.labels.emailkhongdinhdang %>')) {
                if (validateEmpty('<%=txtFromDateLN.ClientID %>', '<%=Resources.labels.bannhapngaybatdau %>')) {
                    if (validateEmpty('<%=txtToDateLN.ClientID %>', '<%=Resources.labels.bannhapngayketthuc %>')) {
                        if (IsDateGreater('<%=txtToDateLN.ClientID %>', '<%=txtFromDateLN.ClientID %>', '<%=Resources.labels.ngayketthucphailonhonngaybatdau %>')) {
                            if (IsDateGreaterDay('<%=txtToDateLN.ClientID %>', '<%=txtFromDateLN.ClientID %>', 7, '<%=Resources.labels.thoigianhieuluctoida7ngay %>')) {

                            }
                            else {
                                return false;
                            }
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        document.getElementById('<%=txtToDateLN.ClientID %>').focus();
                        return false;
                    }
                }
                else {
                    document.getElementById('<%=txtFromDateLN.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtEmail.ClientID %>').focus();
                return false;
            }

        }
        function IsDateGreater(DateValue1, DateValue2, aler) {

            var DaysDiff;

            DateValue1 = document.getElementById(DateValue1).value;
            DateValue2 = document.getElementById(DateValue2).value;
            var dt1 = DateValue1.substring(0, 2);
            var mon1 = DateValue1.substring(3, 5);
            var yr1 = DateValue1.substring(6, 10);
            var dt2 = DateValue2.substring(0, 2);
            var mon2 = DateValue2.substring(3, 5);
            var yr2 = DateValue2.substring(6, 10);

            DateValue1 = mon1 + "/" + dt1 + "/" + yr1;
            DateValue2 = mon2 + "/" + dt2 + "/" + yr2;

            Date1 = new Date(DateValue1);
            Date2 = new Date(DateValue2);
            DaysDiff = Math.floor((Date1.getTime() - Date2.getTime()) / (1000 * 60 * 60 * 24));
            //window.alert(DaysDiff);
            //return;
            if (DaysDiff >= 0)
                return true;
            else
                window.alert(aler);
            return false;
        }
        function IsDateGreaterDay(DateValue1, DateValue2, day, aler) {

            var DaysDiff;

            DateValue1 = document.getElementById(DateValue1).value;
            DateValue2 = document.getElementById(DateValue2).value;

            var dt1 = DateValue1.substring(0, 2);
            var mon1 = DateValue1.substring(3, 5);
            var yr1 = DateValue1.substring(6, 10);
            var dt2 = DateValue2.substring(0, 2);
            var mon2 = DateValue2.substring(3, 5);
            var yr2 = DateValue2.substring(6, 10);
            DateValue1 = mon1 + "/" + dt1 + "/" + yr1;
            DateValue2 = mon2 + "/" + dt2 + "/" + yr2;

            Date1 = new Date(DateValue1);
            Date2 = new Date(DateValue2);
            DaysDiff = Math.floor((Date1.getTime() - Date2.getTime()) / (1000 * 60 * 60 * 24));
            //window.alert(DaysDiff);
            //return;


            if (DaysDiff <= day)
                return true;
            else
                window.alert(aler);
            return false;
        }
    </script>

</asp:Panel>
