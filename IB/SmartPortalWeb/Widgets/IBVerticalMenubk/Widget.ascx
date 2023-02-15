<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBVerticalMenu_Widget" %>
<link href="widgets/IBVerticalMenu/CSS/menu.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="widgets/IBHorizontalMenu/menu.css" />
<link href="widgets/IBVerticalMenu/CSS/css.css" rel="stylesheet" type="text/css" />
<!--[if lt IE 9]>
<link href="widgets/IBVerticalMenu/CSS/css-ie8.css" rel="stylesheet" type="text/css" />
<![endif]-->



<script type="text/javascript" src="widgets/IBVerticalMenu/ddaccordion.js">
/***********************************************
* Accordion Content script- (c) Dynamic Drive DHTML code library (www.dynamicdrive.com)
* Visit http://www.dynamicDrive.com for hundreds of DHTML scripts
* This notice must stay intact for legal use
***********************************************/


</script>

<script type="text/javascript">

    ddaccordion.init({ //top level headers initialization

        headerclass: "expandable", //Shared CSS class name of headers group that are expandable
        contentclass: "categoryitems", //Shared CSS class name of contents group
        revealtype: "mouseover", //Reveal content when user clicks or onmouseover the header? Valid value: "click", "clickgo", or "mouseover"
        mouseoverdelay: 0, //if revealtype="mouseover", set delay in milliseconds before header expands onMouseover
        collapseprev: true, //Collapse previous content (so only one open at any time)? true/false 
        defaultexpanded: [], //index of content(s) open by default [index1, index2, etc]. [] denotes no content
        onemustopen: false, //Specify whether at least one header should be open always (so never all headers closed)
        animatedefault: true, //Should contents open by default be animated into view?
        persiststate: true, //persist state of opened contents within browser session?
        toggleclass: ["", "openheader"], //Two CSS classes to be applied to the header when it's collapsed and expanded, respectively ["class1", "class2"]
        togglehtml: ["prefix", "", ""], //Additional HTML added to the header when it's collapsed and expanded, respectively  ["position", "html1", "html2"] (see docs)
        animatespeed: "normal", //speed of animation: integer in milliseconds (ie: 200), or keywords "fast", "normal", or "slow"
        oninit: function (headers, expandedindices) { //custom code to run when headers have initalized
            //do nothing
        },
        onopenclose: function (header, index, state, isuseractivated) { //custom code to run whenever a header is opened or closed
            //do nothing
        }


    })


</script>
<asp:Literal runat="server" ID="ltrContent"></asp:Literal>

<%--<div class='left_content'>
    <div class='left_content_menu'>
        <div class='left_content_menu_header'>
            <div class='middle_left_content_menu_header'>
                <div class='left_menu_icon' style='background-image: url("../Images/MenuIcon/menu_inquiry_icon.png")'></div>
                Information Inquiry
            </div>
        </div>
        <div class='arrowlistmenu'>
            <div class='menuheader expandable crazy'>Account Information</div>
            <ul class='categoryitems'>
                <li><a href='?po=3&p=86'>Accounts list</a></li>
                <li><a href='?po=3&p=87'>Transaction details</a></li>
            </ul>
        </div>
        <div class='arrowlistmenu'>
            <div class='menuheader expandable crazy'>User information</div>
            <ul class='categoryitems'>
                <li><a href='?po=3&p=88'>User information</a></li>
                <li><a href='?po=3&p=115'>Login history</a></li>
            </ul>
        </div>
        <div class='arrowlistmenu'>
            <div class='menuheader expandable crazy'>Current account</div>
            <ul class='categoryitems'>
                <li><a href='?po=3&p=92'>Accounts list</a></li>
            </ul>
        </div>
        <div class='arrowlistmenu'>
            <div class='menuheader expandable crazy'>Demand deposit</div>
            <ul class='categoryitems'>
                <li><a href='?po=3&p=94'>Accounts list</a></li>
            </ul>
        </div>
        <div class='arrowlistmenu'>
            <div class='menuheader expandable crazy'>Term deposit</div>
            <ul class='categoryitems'>
                <li><a href='?po=3&p=93'>Accounts list</a></li>
            </ul>
        </div>
        <div class='arrowlistmenu'>
            <div class='menuheader expandable crazy'>Loan</div>
            <ul class='categoryitems'>
                <li><a href='?po=3&p=95'>Accounts list</a></li>
            </ul>
        </div>
    </div>
</div>
<nav id='mobile_menu'>
    <ul>
        <li><span style='background-color: #f9e16a; font-weight: bold;'>Information Inquiry</span><ul>
            <li><span>Account Information</span><ul>
                <li><a href='?po=3&p=86'>Accounts list</a></li>
                <li><a href='?po=3&p=87'>Transaction details</a></li>
            </ul>
            </li>
            <li><span>User information</span><ul>
                <li><a href='?po=3&p=88'>User information</a></li>
                <li><a href='?po=3&p=115'>Login history</a></li>
            </ul>
            </li>
            <li><span>Current account</span><ul>
                <li><a href='?po=3&p=92'>Accounts list</a></li>
            </ul>
            </li>
            <li><span>Demand deposit</span><ul>
                <li><a href='?po=3&p=94'>Accounts list</a></li>
            </ul>
            </li>
            <li><span>Term deposit</span><ul>
                <li><a href='?po=3&p=93'>Accounts list</a></li>
            </ul>
            </li>
            <li><span>Loan</span><ul>
                <li><a href='?po=3&p=95'>Accounts list</a></li>
            </ul>
            </li>
        </ul>
        </li>
        <li><span style='background-color: #f9e16a; font-weight: bold;'>Payment</span><ul>
            <li><span>Transfer</span><ul>
                <li><a href='?po=3&p=91'>Transfer between own accounts</a></li>
                <li><a href='?po=3&p=89'>Transfer between others BAY account</a></li>
            </ul>
            </li>
            <li><a href='?po=3&p=256'>iBanking transaction history</a></li>
            <li><span>TopUp</span><ul>
                <li><a href='?po=3&p=436'>Mobile topup</a></li>
            </ul>
            </li>
            <li><span>Payment online</span><ul>
                <li><span>List of pending payments</span></li>
                <li><a href='?po=3&p=121'>Bill payment</a></li>
                <li><span>Setup schedule fund transfer</span></li>
            </ul>
            </li>
            <li><a href='?po=3&p=116'>Batch transfer (upload file)</a></li>
            <li><span>Set schedule transfer</span><ul>
                <li><a href='?po=3&p=117'>Set schedule transfer</a></li>
                <li><a href='?po=3&p=118'>View schedule transfer</a></li>
                <li><a href='?po=3&p=118'>Cancel schedule transfer</a></li>
                <li><a href='?po=3&p=355'>View implementation date</a></li>
            </ul>
            </li>
            <li><a href='?po=3&p=1042'>Direct Debit</a></li>
        </ul>
        </li>
        <li><span style='background-color: #f9e16a; font-weight: bold;'>Credit Card Management</span><ul>
            <li><span>Credit Card Information</span><ul>
                <li><a href='?po=3&p=1045'>Card List</a></li>
                <li><a href='?po=3&p=1046'>Statement</a></li>
                <li><a href='?po=3&p=1047'>Last Ten Transactions</a></li>
            </ul>
            </li>
            <li><span>Credit Card Payment</span><ul>
                <li><a href='?po=3&p=1048'>Payment For Own Card</a></li>
                <li><a href='?po=3&p=1049'>Payment For Other Card</a></li>
            </ul>
            </li>
            <li><span>Card Status</span><ul>
                <li><a href='?po=3&p=1050'>Block/Unblock</a></li>
            </ul>
            </li>
        </ul>
        </li>
        <li><span style='background-color: #f9e16a; font-weight: bold;'>Configuration</span><ul>
            <li><a href='?po=3&p=114'>Beneficiary Management</a></li>
            <li><a href='?po=3&p=134'>Transfer Template Management</a></li>
        </ul>
        </li>
        <li><span style='background-color: #f9e16a; font-weight: bold;'>Support</span><ul>
            <li><span>Contact</span></li>
        </ul>
        </li>
        <li class="active"><a href='?po=3&p=388'>Online saving</a><ul>
            <li><a href='?po=3&p=122'>Open Term Deposit</a></li>
            <li><a href='?po=3&p=102'>Close account</a></li>
        </ul>
        </li>
    </ul>
</nav>--%>
