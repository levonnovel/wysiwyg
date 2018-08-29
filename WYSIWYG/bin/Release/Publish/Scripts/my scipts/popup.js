$(() => {
	window.PopUp = PopUp;
	function PopUp(options) {
		var thatPopup = this;
		options.tagName = thatPopup.constructor.name;
		Panel.call(thatPopup, options);

		thatPopup.$parent && thatPopup.$parent.append(thatPopup.$element);
		options.parent = thatPopup.$elem;

		thatPopup.$titlebar = new TitleBar(options);
		var $div = $('<div/>');
		$('popup').append($div);
		$div.attr('id', 'box');
		options.parent = $('#box');
		thatPopup.$toolbar = new ToolBar(options)
		thatPopup.$filebar = new FileBar(options)
		thatPopup.$content = new Content(options)
		thatPopup.$buttonbar = new ButtonBar(options)
		thatPopup.$elem.attr('id', 'popup');
		var $div = $('<div/>');
		$('#main').append($div);
		$div.attr('id', 'bottom');
		$div.addClass('hide');
	}
})
