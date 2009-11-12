
function buttonPick(xUrl, xId, xDisplayId){
	if (xUrl.indexOf("?",0) > 0) {
		xUrl += "&xId=" + xId + "&xDisplayId=" + xDisplayId;
	}
	else {
		xUrl += "?xId=" + xId + "&xDisplayId=" + xDisplayId;
	}
	window.open(xUrl ,null,"height=400,width=600, scrollbars=yes");
}

function delGovcat(xId, delCode, trId) {
	var codeText = $(xId).value;

	var codeText1 = codeText.replace(","+delCode, "");
	if (codeText1.length==codeText.length)
		codeText = codeText.replace(delCode, "");
	else
		codeText = codeText1;
		
	if (codeText.charAt(0)==',')
		codeText = codeText.substr(1, codeText.length-1);
		
	$(xId).value = codeText;

	var node=$(trId);
    node.parentNode.removeChild(node);
	
}