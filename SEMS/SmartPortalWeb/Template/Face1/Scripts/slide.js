jQuery13(document).ready(function() {
	
	// Expand Panel
	jQuery13("#open").click(function(){
		jQuery13("div#panel").slideDown("slow");
	
	});	
	
	// Collapse Panel
	jQuery13("#close").click(function(){
		jQuery13("div#panel").slideUp("slow");	
	});		
	
	// Switch buttons from "Log In | Register" to "Close Panel" on click
	jQuery13("#toggle a").click(function () {
		jQuery13("#toggle a").toggle();
	});		
		
});