function mainmenu(){
jQuery12(" #nav ul ").css({display: "none"}); // Opera Fix
jQuery12(" #nav li").hover(function(){
		jQuery12(this).find('ul:first').css({visibility: "visible",display: "none"}).show(400);
		},function(){
		jQuery12(this).find('ul:first').css({visibility: "hidden"});
		});
}

 
 
 jQuery12(document).ready(function(){					
	mainmenu();
});