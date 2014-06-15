/*
 * bookview sample
*/

function addPage(page, book, pics) {

	var id, pages = book.turn('pages');

	// Create a new element for this page
	var element = $('<div />', {});

	// Add the page to the flipbook
	if (book.turn('addPage', element, page)) {

		// Add the initial HTML
		// It will contain a loader indicator and a gradient
		element.html('<div class="gradient"></div><div class="loader"></div>');
        
		// Load the page
		loadPage(page, element, pics);
	}

}

function loadPage(page, pageElement, pics) {

	// Create an image element

	var img = $('<img />');

	img.mousedown(function(e) {
		e.preventDefault();
	});

	img.load(function() {
		
		// Set the size
		$(this).css({width: '100%', height: '100%'});

		// Add the image to the page after loaded

		$(this).appendTo(pageElement);

		// Remove the loader indicator
		
		pageElement.find('.loader').remove();
	});

	// Load the page

//img.attr('src', 'pages/' + page + '.jpg');
//if (pics && pics.length && pics.length >= page) {
//    img.attr('src', pics[page - 1].BPicUrl); 
//}
img.attr('src', pics[page - 1].BPicUrl); 
	//loadRegions(page, pageElement);
//addRegion(pics[page - 1], pageElement)
}

// Load regions

function loadRegions(page, element) {

	$.getJSON('pages/'+page+'-regions.json').
		done(function(data) {

			$.each(data, function(key, region) {
				addRegion(region, element);
			});
		});
}

// Add region

function addRegion(region, pageElement) {
	
	var reg = $('<div />', {'class': 'region  ' + region['class']}),
		options = $('.bookview').turn('options'),
		pageWidth = options.width/2,
		pageHeight = options.height;

	reg.css({
		top: Math.round(0/pageHeight*100)+'%',
		left: Math.round(0/pageWidth*100)+'%',
		width: Math.round(region.PicWidth / pageWidth * 100) + '%',
		height: Math.round(region.PicHeight / pageHeight * 100) + '%'
	}).attr('region-data', $.param(region.data||''));


	reg.appendTo(pageElement);
}

function regionClick(event) {

	var region = $(event.target);
	if (region.hasClass('region')) {
		
		var regionType = $.trim(region.attr('class').replace('region', ''));

		return processRegion(region, regionType);
	}

}

function processRegion(region, regionType) {

	data = decodeParams(region.attr('region-data'));

	switch (regionType) {
		case 'link' :

			window.open(data.url);

		break;
		case 'zoom' :

			var regionOffset = region.offset(),
				viewportOffset = $('.bookview-viewport').offset(),
				pos = {
					x: regionOffset.left-viewportOffset.left,
					y: regionOffset.top-viewportOffset.top
				};

			$('.bookview-viewport').zoom('zoomIn', pos);

		break;
		case 'to-page' :

			$('.bookview').turn('page', data.page);

		break;
	}

}

// Load large page

function loadLargePage(page, pageElement) {
	
	var img = $('<img />');

	img.load(function() {

		var prevImg = pageElement.find('img');
		$(this).css({width: '100%', height: '100%'});
		$(this).appendTo(pageElement);
		prevImg.remove();
		
	});

	// Loadnew page
	
	img.attr('src', 'pages/' +  page + '-large.jpg');
}

// Load small page

function loadSmallPage(page, pageElement) {
	
	var img = pageElement.find('img');

	img.css({width: '100%', height: '100%'});

	img.unbind('load');
	// Loadnew page

	img.attr('src', 'pages/' +  page + '.jpg');
}

// http://code.google.com/p/chromium/issues/detail?id=128488

function isChrome() {

	return navigator.userAgent.indexOf('Chrome')!=-1;

}

function disableControls(page) {
		if (page==1)
			$('.previous-button').hide();
		else
			$('.previous-button').show();
					
		if (page==$('.bookview').turn('pages'))
			$('.next-button').hide();
		else
			$('.next-button').show();
}

// Set the width and height for the viewport

function resizeViewport() {

	var width = $(window).width(),
		height = $(window).height(),
		options = $('.bookview').turn('options');

	$('.bookview').removeClass('animated');

	$('.bookview-viewport').css({
	    width: width,
	    height: height
	}); //.zoom('resize');


	if ($('.bookview').turn('zoom')==1) {
		var bound = calculateBound({
			width: options.width,
			height: options.height,
			boundWidth: Math.min(options.width, width),
			boundHeight: Math.min(options.height, height)
		});

		if (bound.width%2!==0)
			bound.width-=1;

			
		if (bound.width!=$('.bookview').width() || bound.height!=$('.bookview').height()) {

			$('.bookview').turn('size', bound.width, bound.height);

			if ($('.bookview').turn('page')==1)
				$('.bookview').turn('peel', 'br');

			$('.next-button').css({height: bound.height, backgroundPosition: '-38px '+(bound.height/2-32/2)+'px'});
			$('.previous-button').css({height: bound.height, backgroundPosition: '-4px '+(bound.height/2-32/2)+'px'});
		}

		$('.bookview').css({top: -bound.height/2, left: -bound.width/2});
	}

	var bookviewOffset = $('.bookview').offset(),
		boundH = height - bookviewOffset.top - $('.bookview').height(),
		marginTop = (boundH - $('.thumbnails > div').height()) / 2;

	if (marginTop<0) {
		$('.thumbnails').css({height:1});
	} else {
		$('.thumbnails').css({height: boundH});
		$('.thumbnails > div').css({marginTop: marginTop});
	}

	if (bookviewOffset.top<$('.made').height())
		$('.made').hide();
	else
		$('.made').show();

	$('.bookview').addClass('animated');
	
}

// Width of the flipbook when zoomed in

function largebookviewWidth() {
	
	return 2214;

}

// decode URL Parameters

function decodeParams(data) {

	var parts = data.split('&'), d, obj = {};

	for (var i =0; i<parts.length; i++) {
		d = parts[i].split('=');
		obj[decodeURIComponent(d[0])] = decodeURIComponent(d[1]);
	}

	return obj;
}

// Calculate the width and height of a square within another square

function calculateBound(d) {
	
	var bound = {width: d.width, height: d.height};

	if (bound.width>d.boundWidth || bound.height>d.boundHeight) {
		
		var rel = bound.width/bound.height;

		if (d.boundWidth/rel>d.boundHeight && d.boundHeight*rel<=d.boundWidth) {
			
			bound.width = Math.round(d.boundHeight*rel);
			bound.height = d.boundHeight;

		} else {
			
			bound.width = d.boundWidth;
			bound.height = Math.round(d.boundWidth/rel);
		
		}
	}
		
	return bound;
}