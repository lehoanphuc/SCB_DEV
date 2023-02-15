<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_HeaderAdminBank_Widget" %>
<div id="header">            
            <div id="logo"><img src="widgets/headeradminbank/images/Abanklogo.png" style=" margin-left:20px;" /></div>
            <div id="lang"><a  href='<%= SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Utilities.Utility.LinkMultiLang(true)+"l=mk") %> '><%Response.Write(Resources.labels.myanmar); %></a> | <a href='<%= SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Utilities.Utility.LinkMultiLang(true)+"l=en-US") %> '><%Response.Write(Resources.labels.english); %></a></div>           
                          
        </div>

  