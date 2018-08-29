$(() => {
	window.TitleBar = TitleBar;
	function TitleBar(options) {
		var that = this;
		options.tagName = that.constructor.name;
		that.$buttons = [];
		Bar.call(that, options);
		for (let i = 0; i < 3; i++) {
			//debugger
			var $button = $('<button/>');
			that.$buttons[i] = $button;
			that.$elem.append($button);
			$button.attr('id', 'titlebarButton' + i);
		}
		var $close = $('#titlebarButton0'),
			$fullScreen = $('#titlebarButton1'),
			$min_max = $('#titlebarButton2');

		$close.click(function () {
			$('#popup').remove();
		});

		$min_max.click(function () {
			$('#popup').toggleClass('hide');
			$('#box').toggleClass('hide');
			$('#bottom').toggleClass('show');

			if (!$('#box').hasClass('hide')) {
				$('titlebar').prependTo('#box')
					.off('mousemove')
					.off('mouseup');
			} else {
				$('titlebar').appendTo('#bottom')
					.off('mousemove')
					.on('mouseup');
			}
		});

		$fullScreen.get(0).addEventListener('click', function (e) {
			e.stopPropagation();
			$('#popup').toggleClass('fullScreen');
			$('titlebar').off('mousemove')
				.on('mouseup');
		}, false);
	}
});
