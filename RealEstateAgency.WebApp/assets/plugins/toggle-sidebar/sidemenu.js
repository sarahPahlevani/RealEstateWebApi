(function () {
	"use strict";

	var slideMenu = $('.side-menu');

	// Toggle Sidebar
	$('[data-toggle="sidebar"]').click(function(event) {
		event.preventDefault();
		$('.app').toggleClass('sidenav-toggled');
	});

	if ( $(window).width() > 739) {     
		$('.app-sidebar').hover(function(event) {
			event.preventDefault();
			$('.app').removeClass('sidenav-toggled');
		});
	} 
	
	//Automatic reloaded Page
	var context;
	var $window = $(window);
	if ($window.width() < 739) {
		context = 'small';
	} 
	$(window).on("resize",function(e) {
		if(($window.width() < 739)) {
			location.reload();
		} 
	});
	
	// Activate sidebar slide toggle
	$("[data-toggle='slide']").click(function(event) {
		event.preventDefault();
		if(!$(this).parent().hasClass('is-expanded')) {
			slideMenu.find("[data-toggle='slide']").parent().removeClass('is-expanded');
		}
		$(this).parent().toggleClass('is-expanded');
	});

	// Set initial active toggle
	$("[data-toggle='slide.'].is-expanded").parent().toggleClass('is-expanded');

	//Activate bootstrip tooltips
	$("[data-toggle='tooltip']").tooltip();

})();