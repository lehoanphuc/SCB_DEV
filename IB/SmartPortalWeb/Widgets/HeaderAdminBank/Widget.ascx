<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_HeaderAdminBank_Widget" %>
<div id="header">            
            <div id="logo"><img src="https://upload.wikimedia.org/wikipedia/en/thumb/4/47/Siam_Commercial_Bank_%28logo%29.svg/345px-Siam_Commercial_Bank_%28logo%29.svg.png" style=" margin-left:0px;" /></div>
            <div id="lang"><a href='<%= SmartPortal.Common.Utilities.Utility.LinkMultiLang(true)+"l=en-US" %> '><%Response.Write(Resources.labels.english); %></a></div>           
                          
        </div>

  