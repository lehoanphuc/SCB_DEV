<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_AddWidget_Widget" %>

<div class="row form-group">
    <div class="col-xs-3">
        <span><%= Resources.labels.widgets %></span>
    </div>
    <div class="col-xs-9">
        <asp:DropDownList ID="ddlWidgets" runat="server" CssClass="form-control select2" Width="100%">
        </asp:DropDownList>
    </div>
</div>
<div class="row form-group">
    <div class="col-xs-3">
        <span><%= Resources.labels.position %></span>
    </div>
    <div class="col-xs-9">
        <asp:DropDownList ID="ddlPosition" runat="server" CssClass="form-control" Width="100%">
        </asp:DropDownList>
    </div>
</div>
<div class="row form-group">
    <div class="col-xs-3">
        <span><%= Resources.labels.order %></span>
    </div>
    <div class="col-xs-9">
        <asp:DropDownList ID="ddlOrder" runat="server" CssClass="form-control" Width="100%">
            <asp:ListItem>0</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
        </asp:DropDownList>
    </div>
</div>
<div class="row form-group text-center">
    <asp:Button ID="Button1" runat="server" Text='<%$ Resources:labels, save %>' CssClass="btn btn-primary"
        OnClick="Button1_Click" />
</div>
<script type="text/javascript">
    var ddlText, ddlValue, ddl, lblMesg;
    function CacheItems() {
        ddlText = new Array();
        ddlValue = new Array();
        ddl = document.getElementById("<%=ddlWidgets.ClientID %>");
        for (var i = 0; i < ddl.options.length; i++) {
            ddlText[ddlText.length] = ddl.options[i].text;
            ddlValue[ddlValue.length] = ddl.options[i].value;
        }
    }
    window.onload = CacheItems;

    function FilterItems(value) {
        ddl.options.length = 0;
        for (var i = 0; i < ddlText.length; i++) {
            if (ddlText[i].toLowerCase().indexOf(value) != -1) {
                AddItem(ddlText[i], ddlValue[i]);
            }
        }
        lblMesg.innerHTML = ddl.options.length;
        if (ddl.options.length == 0) {
            AddItem("No items found.", "");
        }
    }

    function AddItem(text, value) {
        var opt = document.createElement("option");
        opt.text = text;
        opt.value = value;
        ddl.options.add(opt);
    }
</script>