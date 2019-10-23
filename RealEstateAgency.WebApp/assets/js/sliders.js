var revapi26,
	tpj=jQuery;

tpj(document).ready(function() {
	if(tpj("#rev_slider_26_1").revolution == undefined){
		revslider_showDoubleJqueryError("#rev_slider_26_1");
	}else{
		revapi26 = tpj("#rev_slider_26_1").show().revolution({
			sliderType:"standard",
			jsFileLocation:"//tpserver.local/R_5452/wp-content/plugins/revslider/public/assets/js/",
			sliderLayout:"fullwidth",
			dottedOverlay:"none",
			delay:9000,
			navigation: {
				keyboardNavigation:"off",
				keyboard_direction: "horizontal",
				mouseScrollNavigation:"off",
 							mouseScrollReverse:"default",
				onHoverStop:"off",
				arrows: {
					style:"metis",
					enable:true,
					hide_onmobile:false,
					hide_onleave:false,
					tmp:'',
					left: {
									container:"layergrid",
						h_align:"right",
						v_align:"bottom",
						h_offset:61,
						v_offset:1
					},
					right: {
									container:"layergrid",
						h_align:"right",
						v_align:"bottom",
						h_offset:0,
						v_offset:1
					}
				}
			},
			responsiveLevels:[1240,1024,778,480],
			visibilityLevels:[1240,1024,778,480],
			gridwidth:[1240,1024,778,480],
			gridheight:[800,768,960,720],
			lazyType:"single",
			shadow:0,
			spinner:"spinner5",
			stopLoop:"on",
			stopAfterLoops:0,
			stopAtSlide:1,
			shuffle:"off",
			autoHeight:"off",
			disableProgressBar:"on",
			hideThumbsOnMobile:"off",
			hideSliderAtLimit:0,
			hideCaptionAtLimit:0,
			hideAllCaptionAtLilmit:0,
			debugMode:false,
			fallbacks: {
				simplifyAll:"off",
				nextSlideOnWindowFocus:"off",
				disableFocusListener:false,
			}
		});
	}
				/*	jQuery.ajax({
				url : 'http://builder.themepunch.com/wp-admin/admin-ajax.php',
				type : 'post',
				data : {
					action : 'display_slider',
					slider: 'goodnews-testimonials'
				},
				success : function( response ) {
					jQuery("body").append("<div id=new_slider>"+response+"</div>");

				},
				error : function ( response ){

				}
			}); // End Ajax

			*/

    RsAddonPanorama(tpj, revapi26);
});