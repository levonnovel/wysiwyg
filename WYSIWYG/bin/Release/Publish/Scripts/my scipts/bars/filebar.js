$(() => {
	window.FileBar = FileBar;
	function FileBar(options) {
		var that = this;
		Bar.call(that, options);

		$('fileBar').on('click', 'div', (e) => {

			if ($(e.target)[0].tagName !== 'INPUT') {
				var fileName = $(e.target).text();
				$('fileBar > div').removeAttr('id');
				if ($(e.target)[0].tagName !== 'INPUT') {
					$(e.target).attr('id', 'currFile');
				}

				//alert(1);
				//debugger
				$.ajax({
					type: 'POST',
					url: 'ReadFile',
					data: { name: fileName },
					success: (res) => {
						console.log(123);
						$('content').html(res);
					},
					error: () => {
						console.log(321);
					}
				})
				//Ajax -> ReadFile

				for (var i = 0; i < 3; i++) {
					//console.log(that.$buttons[i]);
					$('#currFile').append(that.$buttons[i]);
				}
			}

		});
		var buttonNames = ['save', 'delete', 'download'];

		that.$buttons = [];
		for (var i = 0; i < 3; i++) {
			that.$buttons[i] = $('<input type="button" />').addClass('buttonBarButtons').val(buttonNames[i]);
			console.log(that.$buttons[i]);
			$('#currFile').append(that.$buttons[i]);
		}
		that.$buttons[0].click(() => {
			var fileName = $('#currFile').text(),
				text = $('content').text();
			$.ajax({
				type: 'POST',
				url: 'SaveFile',
				data: { name: fileName, text: text },
				success: (res) => {
					console.log('saved');
				},
				error: () => {
					console.log('something went wrong');
				}
			});
		});

		that.$buttons[1].click(() => {
			var isSure = confirm('Are you sure?'),
				fileName = $('#currFile').text();
			console.log(fileName);

			if (isSure) {
				$.ajax({
					type: 'POST',
					url: 'DeleteFile',
					data: { name: fileName },
					success: () => {
						console.log('removed');
						$('fileBar #currFile').detach();
						$('content').text('');
					},
					error: () => {
						console.log('something went wrong');
					}
				});
			}
		});

		that.$buttons[2].click(() => {
			console.log($('titlebar').text());
			console.log($('#currFile').text());
		});
	}
})
