<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Controls_HeaderHomeBank_Widget" %>

<div id="header">  
<div id="logo">
<img src="https://upload.wikimedia.org/wikipedia/en/thumb/4/47/Siam_Commercial_Bank_%28logo%29.svg/345px-Siam_Commercial_Bank_%28logo%29.svg.png" style=" margin-left:20px;" />
</div>
<div id="lang">
    <a href='<%= SmartPortal.Common.Utilities.Utility.LinkMultiLang(true)+"l=mk" %> '>
        <img alt="<%Response.Write(Resources.labels.Myanmar); %>" src="App_Themes/InternetBanking/images/flag-myanmar.png"/>
    </a> 
    <a href='<%= SmartPortal.Common.Utilities.Utility.LinkMultiLang(true)+"l=en-US" %> '>
        <img alt="<%Response.Write(Resources.labels.english); %>" src="App_Themes/InternetBanking/images/flag-english.png"/>
    </a>
</div>  
</div>