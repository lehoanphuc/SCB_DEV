<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Controls_HeaderHomeBank_Widget" %>

<div id="header">  
<div id="logo">
<img src="widgets/headerhomebank/images/logo.png" style=" margin-left:20px;" />
</div>
<div id="lang">
    <a href='<%= SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Utilities.Utility.LinkMultiLang(true)+"l=mk") %> '>
        <img alt="<%Response.Write(Resources.labels.myanmar); %>" src="widgets/headerhomebank/images/flag_Eng.gif"/>
    </a> 
    <a href='<%= SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Utilities.Utility.LinkMultiLang(true)+"l=en-US") %> '>
        <img alt="<%Response.Write(Resources.labels.english); %>" src="widgets/headerhomebank/images/flag_Eng.gif"/>
    </a>
</div>  
</div>