function retweet(but, id) {
	$.ajax({
		url: 'Tweet/apiRetweet/'+id,
		success: function (data) {
			console.log(data);
			alert('Load was performed.');
		}
	});
	$(but).attr("disabled", "true");
}