$(() => {
	window.ToolBar = ToolBar;
	function ToolBar(options) {
		var that = this;

		Bar.call(that, options);

		that.$divs = [];
		var $firstRow = $('<div />'),
			$secondRow = $('<div />');
		$firstRow.addClass('firstRow');
		$secondRow.addClass('secondRow');
		for (var i = 0; i < 17; i++) {
			that.$divs[i] = $('<span />');
			$firstRow.append(that.$divs[i]);
		}
		for (var i = 0; i < 13; i++) {
			that.$divs[i] = $('<span />');
			$secondRow.append(that.$divs[i]);
		}
		that.$elem.append($firstRow);
		that.$elem.append($secondRow);
		//options.$parent = that.$elem;
	}
})
