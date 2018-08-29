$(() => {
	//$('input').click(() => {
	//    document.execCommand('cut');
	//})
	//  var popUp1 = new PopUp({ $parent: $('#main'), tagName: 'popUp' });
	//	console.log(popUp1);
	var $parentTag = $('#main');
	var $popup = new PopUp({ $parent: $('#main'), tagName: 'popUp' });
	var $newPopup = document.createElement
	$('titlebar').on('mousedown', function (e) {

		$('#popup').addClass('active');
		var oTop = e.clientY - $('.active').offset().top,
			oLeft = e.clientX - $('.active').offset().left,
			initialX = $(this).offset().top,
			initialY = $(this).offset().left,
			isBack = false;

		$('titlebar').parents().on('mousemove', function (e) {
			$('.active').offset({
				top: e.pageY - oTop,
				left: e.pageX - oLeft
			})

			//            if($('#popup').offset().top < 30 || ($('#popup').offset().top + $('#popup').height()) > window.innerHeight) {
			//                isBack = true;
			//            }
			//            if($('#popup').offset().left < 0 || ($('#popup').offset().left + $('#popup').width()) > window.innerWidth) {
			//                isBack = true;
			//            }
			$('.active').on('mouseup', function () {
				if ($('#popup').offset().top < 30 || ($('#popup').offset().top + $('#popup').height()) > window.innerHeight) {
					isBack = true;
				}
				if ($('#popup').offset().left < 0 || ($('#popup').offset().left + $('#popup').width()) > window.innerWidth) {
					isBack = true;
				}
				if (isBack) {
					$('.active').offset({
						top: initialX,
						left: initialY
					});
					isBack = false;
				}
				$('#popup').removeClass('active');
			});
		});
		return false;
	});
});