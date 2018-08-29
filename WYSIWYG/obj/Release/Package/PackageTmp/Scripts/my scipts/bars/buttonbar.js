$(() => {
	window.ButtonBar = ButtonBar;
	function ButtonBar(options) {
		var that = this,
			buttonName = 'new';


		Bar.call(that, options);

		that.$button = $('<input type="button" />').addClass('new').val(buttonName);
		that.$elem.append(that.$button);

		that.$button.click(() => {
			var fileName = window.prompt('Enter file name please.');
			$.ajax({
				type: 'POST',
				url: 'CreateFile',
				data: { name: fileName },
				success: (message) => {
					console.log(message);
					(message == "Success") && $('fileBar').append($('<div />').text(fileName).addClass('fileDiv'));
				},
				error: () => {
					console.log(321);
				}
			});
		});

		var upload = $('<input type="file"/>').addClass('uploadButton');
		that.$elem.append(upload);
	}
})