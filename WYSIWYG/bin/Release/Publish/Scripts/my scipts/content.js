$(() => {
	window.Content = Content;
	function Content(options) {
		var thatContent = this;
		//this.$elem = $('<content />');
		Bar.call(thatContent, options);
		thatContent.$elem.attr('contenteditable', 'true');
	}
});