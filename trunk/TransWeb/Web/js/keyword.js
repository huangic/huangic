var SERVLET_URL = '';

function setServletURL(url) { SERVLET_URL = absoluteURL(url) }

function absoluteURL(url) {
	if (!absoluteURL.baseURL)
		absoluteURL.baseURL = location.protocol + '//' + location.host;
	if (url.indexOf(location.protocol + '//') == -1)
		return absoluteURL.baseURL + url
	else
		return url
}

function getkeywords(textfield,content){
	
	var url = SERVLET_URL;
	var param = 'content='+encodeURI(content);
	
	//if Mozilla
	if (window.XMLHttpRequest) {
		xmlHttpReq = new XMLHttpRequest();
		xmlHttpReq.open('post', url, false);
		xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
		xmlHttpReq.send(param);
	// if IE
	} else if (window.ActiveXObject) {
		xmlHttpReq = new ActiveXObject('Msxml2.XMLHTTP');
		xmlHttpReq.open('post', url, false);
		xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
		xmlHttpReq.send(param);
 	} else {
		alert('error: your browser cannot handle this script');
		return;
	}
	
	if(xmlHttpReq){
		if (xmlHttpReq.status == 200) {
				var xDoc = xmlHttpReq.responseXML;
				var root = xDoc.documentElement;
				var keywords = root.getElementsByTagName('keyword');
				var returnLine = '';
				for(var i=0;i<keywords.length;i++){
					var name = keywords[i].getElementsByTagName('name')[0].firstChild.nodeValue;
					var frequence = keywords[i].getElementsByTagName('frequency')[0].firstChild.nodeValue;
					returnLine += name+'*'+frequence+',';
				}
				textfield.value = returnLine;
		}else{
				alert("There was a problem retrieving the XML data:" +xmlHttpReq.statusText);
		}
	}

}