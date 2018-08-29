$(() => {
	window.Panel = Panel;
	function Panel(options) {
		//var tagName = options.tagName ? options.tagName : this.constructor.name;
		this.$elem = $(`<${this.constructor.name} />`);
		this.$elem.appendTo(options.$parent);
	}
})
