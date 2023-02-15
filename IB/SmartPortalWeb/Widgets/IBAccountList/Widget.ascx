<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBAccountList_Widget" %>

<link rel="stylesheet" type="text/css" href="/Widgets/IBAccountList/css.css?version=1.0.0" />
<asp:ScriptManager runat="server">
</asp:ScriptManager>
<div style="text-align: center; height: 8px">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="" runat="server">
        <ProgressTemplate>
            <div class="cssProgress">
                <div class="progress1">
                    <div class="cssProgress-bar cssProgress-active" data-percent="100" style="transition: none; width: 100%;"></div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="al">
    <span>
        <%=Resources.labels.danhsachtaikhoan %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<div>
    <asp:UpdatePanel runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
            <div style="text-align: center; display: none">
                <asp:Image runat="server" ID="imgLoading" alt="" src="Images/WidgetImage/ajaxloader.gif" Style="width: 16px; height: 16px; color: #0066E1" />
            </div>
            <div class="divAcct">
                <asp:Literal ID="ltrDD" runat="server"></asp:Literal>
                <asp:Timer ID="Timer1" runat="server" Interval="50" OnTick="OnGetAccountListFinish">
                </asp:Timer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<script type="text/javascript">
    function abc(id) {

        var spn = id.replace("btn", "spn");
        var blc = spn.replace("spn", "blc");
        var blch = blc.replace("blc", "blch");
        if (document.getElementById(spn).classList.contains("fa-eye")) {
            document.getElementById(spn).classList.remove("fa-eye");
            document.getElementById(spn).classList.add("fa-eye-slash");
            document.getElementById(blc).style.display = "none";
            document.getElementById(blch).style.display = "contents";
           
        } else {
            document.getElementById(spn).classList.remove("fa-eye-slash");
            document.getElementById(spn).classList.add("fa-eye");
            document.getElementById(blc).style.display = "contents";
            document.getElementById(blch).style.display = "none";
        }
    }
</script>
<style>
    .a2{
        position: absolute;
        top: 0px;
        height: -webkit-fill-available;
        width: -webkit-fill-available;
        color: gainsboro;
        width: 100%;
        height: 100%;
    }
</style>

