$(() => {
	window.Bar = Bar;
	function Bar(options) {
		var thatBar = this;
		Panel.call(thatBar, options);
		thatBar.$elem.appendTo(options.parent);
	}
})