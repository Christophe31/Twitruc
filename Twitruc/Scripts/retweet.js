function retweet(but, id) {
	$.ajax({
		url: 'Tweet/apiRetweet/'+id,
		success: function (data) {
			console.log(data);
			$(but).attr("disabled", "true");
		}
	});
}