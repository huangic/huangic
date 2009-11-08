//TABLE OF CONTENTS TREE SCRIPT
//last updated: 4/07/05 2:03 PM

//warning: any element in html document that has its id == TOC_CONTAINER_ID
//	and is not a ul will be replaced

/*
constants or default values
*/

//depth of XML returned from the server via a XML request
//can be set by a public method
var DEPTH_PER_XML = 2

//when entry in TOC is this number away from maximum depth,
//an XML request is sent to the server, requesting more info
//make sure MIN_DEPTH_LEFT_PER_XML < DEPTH_PER_XML
var MIN_DEPTH_LEFT_PER_XML = 1

//url of xml servlet
//can be set by a public method
var SERVLET_URL = 'http://localhost:8080/toc/servlet/com.hyweb.gip.maian.DBToXMLServlet'

//id of root of TOC tree
var TREE_ROOT_ID = 'treeRoot'

//id of TOC tree root container
var TOC_CONTAINER_ID = 'toc'

//if true, whole TOC is requested recursively
//if false, only the first level of TOC is requested
var bLOAD_ALL = false

//whenever a TOC category is opened and a XML request needs to be sent:
//	if true, displays category before and during XML request
//	if false, displays category after XML request
//no effect if bLOAD_ALL is true
var bOPEN_CATEGORY_ASYNC_WITH_XML_REQUEST = true

//functions that define what happens when you mouseover and mouseout a line in the toc
//this is necessary since for some stupid reason IE doesn't support :hover in css well
var origStyleColor
function MOUSEOVER_FUNC() {
	//this.style.backgroundColor = 'yellow'
	origStyleColor = this.style.color
	this.style.color = 'blue'
}
function MOUSEOUT_FUNC() {
	//this.style.backgroundColor = 'transparent'
	this.style.color = origStyleColor
}

//if true, displays loading status above the TOC
var bDISPLAY_STATUS = false

//image sources
var IMG_SRC = {
	CATEGORY_OPEN: absoluteURL('image/ftv2folderopen.gif'),
	CATEGORY_CLOSED: absoluteURL('image/ftv2folderclosed.gif'),
	DOCUMENT: absoluteURL('image/ftv2doc.gif'),
	VERTICAL_LINE: absoluteURL('image/ftv2vertline.gif'),
	NODE: absoluteURL('image/ftv2node.gif'),
	NODE_PLUS: absoluteURL('image/ftv2pnode.gif'),
	NODE_MINUS: absoluteURL('image/ftv2mnode.gif'),
	LAST_NODE: absoluteURL('image/ftv2lastnode.gif'),
	LAST_NODE_PLUS: absoluteURL('image/ftv2plastnode.gif'),
	LAST_NODE_MINUS: absoluteURL('image/ftv2mlastnode.gif'),
	BLANK: absoluteURL('image/ftv2blank.gif')
}

//images
//will be set in window.onload
var IMG = {}

/*
public methods
*/

function setDepthPerXML (depth) {
	DEPTH_PER_XML = depth
	//make sure MIN_DEPTH_LEFT_PER_XML is always less than DEPTH_PER_XML
	if (MIN_DEPTH_LEFT_PER_XML >= depth)
		setMinDepthLeftPerXML(depth - 1)
}

function setMinDepthLeftPerXML(minDepthLeft) {
	MIN_DEPTH_LEFT_PER_XML = minDepthLeft
	if (minDepthLeft == 0)
		bOPEN_CATEGORY_ASYNC_WITH_XML_REQUEST = false
}

function setCategoryOpenImgSrc(imgSrc) { IMG_SRC.CATEGORY_OPEN = absoluteURL(imgSrc) }
function setCategoryClosedImgSrc(imgSrc) { IMG_SRC.CATEGORY_CLOSED = absoluteURL(imgSrc) }
function setDocumentImgSrc(imgSrc) { IMG_SRC.DOCUMENT = absoluteURL(imgSrc) }
function setVerticalLineImgSrc(imgSrc) { IMG_SRC.VERTICAL_LINE = absoluteURL(imgSrc) }
function setNodeImgSrc(imgSrc) { IMG_SRC.NODE = absoluteURL(imgSrc) }
function setNodePlusImgSrc(imgSrc) { IMG_SRC.NODE_PLUS = absoluteURL(imgSrc) }
function setNodeMinusImgSrc(imgSrc) { IMG_SRC.NODE_MINUS = absoluteURL(imgSrc) }
function setLastNodeImgSrc(imgSrc) { IMG_SRC.LAST_NODE = absoluteURL(imgSrc) }
function setLastNodePlusImgSrc(imgSrc) { IMG_SRC.LAST_NODE_PLUS = absoluteURL(imgSrc) }
function setLastNodeMinusImgSrc(imgSrc) { IMG_SRC.LAST_NODE_MINUS = absoluteURL(imgSrc) }
function setBlankImgSrc(imgSrc) { IMG_SRC.BLANK = absoluteURL(imgSrc) }
function setServletURL(url) { SERVLET_URL = absoluteURL(url) }

/*
init
*/

window.onload = function () {
	//create images once so they can be cloned later
	//	hopefully this will force images to be cached
	for (var prop in IMG_SRC) {
		var imgSrc = IMG_SRC[prop]
		var img = new Image()
		img.src = imgSrc
		IMG[prop] = img		//so IMG.NODE works
		IMG[imgSrc] = img	//so IMG[<imgsrc>] and IMG[IMG_SRC.NODE] works
	}
	if (bDISPLAY_STATUS) {
		var p = document.getElementById('status')
		if (p == null) {
			p = document.createElement('p')
			p.id = 'status'
			p.appendChild(document.createTextNode('Status: '))
			var toc = document.getElementById('toc')
			toc.parentNode.insertBefore(p, toc)
		}
		statusText = document.createTextNode('')
		p.appendChild(statusText)
		p.style.display = 'list-item'
	} else {
		var p = document.getElementById('status')
		if (p != null)
			p.style.display = 'none'
	}
	requestXML(function () {
		var li = document.getElementById(TREE_ROOT_ID)
		click(li)
	}, [TREE_ROOT_ID])
}

/*
XML request functions
*/

var statusText
var xmlHttpReqQueue = new Array()

//func: function called after XML is loaded
//arguments[1+]: IDs
function requestXML(func, ids) {
	var idstr = ids.join(',')
	var xmlHttpReq
	//if Mozilla
	if (window.XMLHttpRequest) {
		xmlHttpReq = new XMLHttpRequest()
		/*xmlHttpReq.onload = function () {
			onloadFunc(xmlHttpReq, func)
		}*/
	// if IE
	} else if (window.ActiveXObject) {
		xmlHttpReq = new ActiveXObject('Msxml2.XMLHTTP')
 	} else {
		alert('error: your browser cannot handle this script')
		return
	}
	var url = SERVLET_URL
	if (url.indexOf('?') == -1)
		url += '?'
	else
		url += '&'
	url += 'id=' + idstr +
		'&depth=' + DEPTH_PER_XML
	xmlHttpReq.open('GET', url, true)
	xmlHttpReq.onreadystatechange = function () {
		if (xmlHttpReq.readyState != 4)
			return
		onloadFunc(xmlHttpReq, func)
	}
	xmlHttpReqQueue.push(xmlHttpReq)
	//if queue was previously empty, there is no request being sent
	//so send this request immediately
	if (xmlHttpReqQueue.length == 1) {
		if (bDISPLAY_STATUS)
			statusText.data = "loading [" + url + "]..."
		xmlHttpReq.send(null)
	}
}

function onloadFunc(xmlHttpReq, func) {
	if (xmlHttpReq.status == 200) {
		buildBaseNodes(xmlHttpReq.responseXML)
		if (func != null) {
			func()
		}
		xmlHttpReqQueue.shift()
		//send the next request if queue isn't empty
		if (xmlHttpReqQueue.length > 0) {
			xmlHttpReqQueue[0].send(null)
		} else {
			if (bDISPLAY_STATUS)
				statusText.data += "done"
		}
	} else
		alert(xmlHttpReq.status + ': ' + xmlHttpReq.statusText)
}

/*
build functions
*/

function NODE_STYLE() {}
NODE_STYLE.NO_NODE = 0
NODE_STYLE.NODE = 1
NODE_STYLE.LAST_NODE = 2

function buildBaseNodes(xmlDoc) {
	var root = xmlDoc.documentElement
	for (var node = root.firstChild; node != null; node = node.nextSibling) {
		//var id = getFirstChildElementByTagName(node, 'id').firstChild.data
		var id = node.getAttribute('id')
		if (id == TREE_ROOT_ID) {
			var toc = document.getElementById(TOC_CONTAINER_ID)
			var ul
			if (toc == null) {	//if not present, create it and append it to HTML body
				ul = document.createElement('ul')
				document.body.appendChild(ul)
				ul.id = TOC_CONTAINER_ID
			} else if (toc.nodeName.toLowerCase() != 'UL') {	//if not ul, make it ul
				ul = document.createElement('ul')
				toc.parentNode.replaceChild(ul, toc)
				ul.id = TOC_CONTAINER_ID
			} else {
				ul = toc
			}
			var drawList = document.createDocumentFragment()
			buildNode(node, ul, drawList, DEPTH_PER_XML, NODE_STYLE.NO_NODE, id)
		} else {
			var li = document.getElementById(id)
			if (li) {
				//get drawList, extract nodeStyle,
				//	then remove last draw node, since it will be replaced in buildNode
				var drawList = getDrawList(li)
				var nodeStyle = getNodeStyle(drawList)
				drawList.removeChild(drawList.lastChild)
				buildNode(node, li.parentNode, drawList, DEPTH_PER_XML, nodeStyle, id)
			}
		}
	}
}

//last argument is put in for efficiency reasons
//it can be omitted
function buildNode(node, ul, drawList, depthLeft, nodeStyle, id) {
	var li, nodeType
	if (id == null)
		//id = getFirstChildElementByTagName(node, 'id').firstChild.data
		id = node.getAttribute('id')
	//nodeType = getFirstChildElementByTagName(node, 'nodeType').firstChild.data
	nodeType = node.getAttribute('nodeType')
	if (nodeType == 'U') {	//is a document
		li = document.getElementById(id)
		//create description row if it doesn't exist
		if (li == null) {
			var newDrawList = drawList.cloneNode(true)
			if (nodeStyle == NODE_STYLE.NODE)
				newDrawList.appendChild(createImg('NODE'))
			else if (nodeStyle == NODE_STYLE.LAST_NODE)
				newDrawList.appendChild(createImg('LAST_NODE'))
			newDrawList.appendChild(createImg('DOCUMENT'))
			li = buildDocumentRow(node, ul, newDrawList, id)
			//for some stupid reason, IE doesn't support :hover in css well,
			//	so I have to script it
			li.onmouseover = MOUSEOVER_FUNC
			li.onmouseout = MOUSEOUT_FUNC
		//make sure node is updated correctly
		//(e.g. in the case that this node is no longer the last node)
		} else {
			/*if (nodeStyle != NODE_STYLE.NO_NODE) {
				var img = getLastChildElementByTagName(li, 'IMG')
				img.previousSibling.setAttribute('src', IMG_SRC.NODE)
			}*/
		}
	} else if (nodeType == 'C') {	//is a category
		var contents = getFirstChildElementByTagName(node, 'contents')
		li = document.getElementById(id)
		//create category header row if it doesn't exist
		if (li == null) {
			//determine how drawList is to be adjusted
			//then adjust it and create the header row
			if (contents && contents.hasChildNodes()) {
				//if there are contents, after the row is created,
				//	adjust drawList for content and sibling nodes
				if (nodeStyle == NODE_STYLE.NODE) {
					var img = createImg('NODE_PLUS')
					drawList.appendChild(img)
					var newDrawList = drawList.cloneNode(true)
					newDrawList.appendChild(createImg(contents == null && depthLeft > 0 ? 'CATEGORY_OPEN' : 'CATEGORY_CLOSED'))
					li = buildCategoryHeaderRow(node, ul, newDrawList, id,
						[newDrawList.lastChild, newDrawList.lastChild.previousSibling])
					img.setAttribute('src', IMG_SRC.VERTICAL_LINE)
				} else if (nodeStyle == NODE_STYLE.LAST_NODE) {
					var img = createImg('LAST_NODE_PLUS')
					drawList.appendChild(img)
					var newDrawList = drawList.cloneNode(true)
					newDrawList.appendChild(createImg(contents == null && depthLeft > 0 ? 'CATEGORY_OPEN' : 'CATEGORY_CLOSED'))
					li = buildCategoryHeaderRow(node, ul, newDrawList, id,
						[newDrawList.lastChild, newDrawList.lastChild.previousSibling])
					img.setAttribute('src', IMG_SRC.BLANK)
				} else {	//NODE_STYLE.NO_NODE
					var newDrawList = drawList.cloneNode(true)
					newDrawList.appendChild(createImg(contents == null && depthLeft > 0 ? 'CATEGORY_OPEN' : 'CATEGORY_CLOSED'))
					li = buildCategoryHeaderRow(node, ul, newDrawList, id,
						[newDrawList.lastChild])
				}
			} else {
				if (nodeStyle == NODE_STYLE.NODE) {
					var newDrawList = drawList.cloneNode(true)
					newDrawList.appendChild(createImg(depthLeft == 0 ? 'NODE_PLUS' : 'NODE'))
					newDrawList.appendChild(createImg(contents == null && depthLeft > 0 ? 'CATEGORY_OPEN' : 'CATEGORY_CLOSED'))
					li = buildCategoryHeaderRow(node, ul, newDrawList, id,
						[newDrawList.lastChild, newDrawList.lastChild.previousSibling])
				} else if (nodeStyle == NODE_STYLE.LAST_NODE) {
					var newDrawList = drawList.cloneNode(true)
					newDrawList.appendChild(createImg(depthLeft == 0 ? 'LAST_NODE_PLUS' : 'LAST_NODE'))
					newDrawList.appendChild(createImg(contents == null && depthLeft > 0 ? 'CATEGORY_OPEN' : 'CATEGORY_CLOSED'))
					li = buildCategoryHeaderRow(node, ul, newDrawList, id,
						[newDrawList.lastChild, newDrawList.lastChild.previousSibling])
				} else {	//NODE_STYLE.NO_NODE
					var newDrawList = drawList.cloneNode(true)
					newDrawList.appendChild(createImg(contents == null && depthLeft > 0 ? 'CATEGORY_OPEN' : 'CATEGORY_CLOSED'))
					li = buildCategoryHeaderRow(node, ul, newDrawList, id,
						[newDrawList.lastChild])
				}
			}
			//for some stupid reason, IE doesn't support :hover in css well,
			//	so I have to script it
			li.onmouseover = MOUSEOVER_FUNC
			li.onmouseout = MOUSEOUT_FUNC
		} else {
			if (contents && contents.hasChildNodes()) {
				//adjust drawList for content nodes
				if (nodeStyle == NODE_STYLE.NODE) {
					drawList.appendChild(createImg('VERTICAL_LINE'))
				} else if (nodeStyle == NODE_STYLE.LAST_NODE) {
					drawList.appendChild(createImg('BLANK'))
				}	//else NODE_STYLE.NO_NODE
			} else {
				//there is a case where an empty category show as if they did have content nodes
				//	(see "benefit of doubt" case near end of function)
				//so update it accordingly and remove its onclick events
				var img = getLastChildElementByTagName(li, 'IMG')
				img.setAttribute('src', IMG_SRC.CATEGORY_OPEN)
				img = img.previousSibling
				if (img.getAttribute('src') == IMG_SRC.NODE_PLUS) {
					img.setAttribute('src', IMG_SRC.NODE)
				} else if (img.getAttribute('src') == IMG_SRC.LAST_NODE_PLUS) {
					img.setAttribute('src', IMG_SRC.LAST_NODE)
				}
				setOnclick(li, null)
			}
		}
		//category may be empty, so test if there are content nodes
		if (contents && contents.hasChildNodes()) {
			var newUl
			//create list of category's contents and the row that will contain it for child nodes if it doesn't exist
			if (li.contents == null) {
				newUl = document.createElement('ul')
				var newLi = document.createElement('li')
				//li.contents primary purpose is to easily show/hide the list of category's contents
				//however, setting ul's display to none won't work,
				//	since IE will display the li that contains it as a blank line,
				//	so li.contents must be the containing li,
				//	and this li's display will be set to none
				li.contents = newLi
				newLi.appendChild(newUl)
				if (li == ul.lastChild) {
					ul.appendChild(newLi)
				} else {
					ul.insertBefore(newLi, li.nextSibling)
				}
				//assign event handlers now that li.contents exists
				li.contents.style.display = 'none'
				var func
				if (depthLeft <= MIN_DEPTH_LEFT_PER_XML && !bLOAD_ALL) {	//need to send a XML request
					if (bOPEN_CATEGORY_ASYNC_WITH_XML_REQUEST && MIN_DEPTH_LEFT_PER_XML > 0) {
						func = function () {
							openCategory(li, nodeStyle)
							requestXML(null, [li.id])
						}
					} else {
						func = function () {
							requestXML(function () {
								openCategory(li, nodeStyle)
							}, [li.id])
						}
					}
				} else {	//don't need to send a XML request
					func = function () {
						openCategory(li, nodeStyle)
					}
				}
				setOnclick(li, func)
			} else {
				//li.contents is the li containing the contents list (ul)
				newUl = li.contents.firstChild
			}
			//recurse through child nodes
			var child = contents.firstChild, childLi
			for (i = 0; child != contents.lastChild; child = child.nextSibling, i++) {
				childLi = buildNode(child, newUl, drawList, depthLeft - 1, NODE_STYLE.NODE)
				childLi.up = li
			}
			childLi = buildNode(child, newUl, drawList, depthLeft - 1, NODE_STYLE.LAST_NODE)
			childLi.up = li
			//if there were contents, an img was appended to drawList,
			//	so remove last draw img to remove it
			if (nodeStyle != NODE_STYLE.NO_NODE) {
				drawList.removeChild(drawList.lastChild)
			}
		} else {
			//if 0 depthLeft, give benefit of doubt,
			//	and assume that once an XML request is sent,
			//	they will have contents
			//if they actually don't have contents, they will be updated accordingly
			if (depthLeft == 0) {
				if (bLOAD_ALL) {
					requestXML(null, [li.id])
					func = function () {
						openCategory(li, nodeStyle)
					}
				} else {
					if (bOPEN_CATEGORY_ASYNC_WITH_XML_REQUEST) {
						func = function () {
							openCategory(li, nodeStyle)
							requestXML(null, [li.id])
						}
					} else {
						func = function () {
							requestXML(function () {
								openCategory(li, nodeStyle)
							}, [li.id])
						}
					}
				}
				setOnclick(li, func)
			}
		}
	}
	return li
}

//TODO: fix function to make compatible with Mozilla
function buildDocumentRow(node, ul, drawList, id) {
	var li = document.createElement('li')
	li.id = id
	//append drawList
	li.appendChild(drawList)
	var desc = getFirstChildElementByTagName(node, 'desc')
	//note: following not Mozilla compliant
	//li.insertAdjacentHTML("beforeEnd", desc.firstChild.xml)
	appendXMLNode(li, desc.childNodes)
	ul.appendChild(li)
	return li
}

function buildCategoryHeaderRow(node, ul, drawList, id, onclickList) {
	var li = document.createElement('li')
	li.id = id
	//append drawList
	li.appendChild(drawList)
	var desc = getFirstChildElementByTagName(node, 'desc')
	//note: following not Mozilla compliant
	//li.insertAdjacentHTML("beforeEnd", desc.firstChild.xml)
	appendXMLNode(li, desc.childNodes)
	//sets the list of elements that will have their onclick funcs set
	//does not set those onclick funcs yet
	li.onclickList = onclickList
	ul.appendChild(li)
	return li
}

/*
event functions
*/

function setOnclick(li, func) {
	for (var i = 0; i < li.onclickList.length; i++)
		li.onclickList[i].onclick = func
}

function click(li) {
	if (li.contents) {
		li.onclickList[0].onclick()
	}
}

function closeCategory(li, nodeStyle) {
	if (!li.contents) {
		setOnclick(li, null)
		return
	}
	var func = function () {
		openCategory(li, nodeStyle)
	}
	setOnclick(li, func)
	li.contents.style.display = 'none'
	//change last img
	var img = li.lastChild
	while (img != null && img.nodeName != 'IMG')
		img = img.previousSibling
	img.setAttribute('src', IMG_SRC.CATEGORY_CLOSED)
	//change second last img if it exists
	img = img.previousSibling
	if (img) {
		if (nodeStyle == NODE_STYLE.NODE)
			img.setAttribute('src', IMG_SRC.NODE_PLUS)
		else if (nodeStyle == NODE_STYLE.LAST_NODE)
			img.setAttribute('src', IMG_SRC.LAST_NODE_PLUS)
	}
}

function openCategory(li, nodeStyle) {
	if (!li.contents) {
		setOnclick(li, null)
		return
	}
	var func = function () {
		closeCategory(li, nodeStyle)
	}
	setOnclick(li, func)
	li.contents.style.display = 'list-item'
	//change last img
	var img = li.lastChild
	while (img != null && img.nodeName != 'IMG')
		img = img.previousSibling
	img.setAttribute('src', IMG_SRC.CATEGORY_OPEN)
	//change second last img if it exists
	img = img.previousSibling
	if (img) {
		if (nodeStyle == NODE_STYLE.NODE)
			img.setAttribute('src', IMG_SRC.NODE_MINUS)
		else if (nodeStyle == NODE_STYLE.LAST_NODE)
			img.setAttribute('src', IMG_SRC.LAST_NODE_MINUS)
	}
}

/*
drawList functions
*/

//returns clone of li's drawList, including all img elements except the last
function getDrawList(li) {
	var drawList = document.createDocumentFragment()
	if (li.hasChildNodes()) {
		for (var child = li.firstChild; 
		  //child.nextSibling != null && child.nextSibling.nodeType == 1;
		  child.nextSibling != null && child.nextSibling.nodeName.toUpperCase() == "IMG"; 
		  child = child.nextSibling)
			drawList.appendChild(child.cloneNode(true))
	}
	return drawList
}

function getNodeStyle(drawList) {
	if (drawList.hasChildNodes()) {
		var lastDrawImgSrc = drawList.lastChild.getAttribute('src')
		if (lastDrawImgSrc == IMG_SRC.NODE ||
			lastDrawImgSrc == IMG_SRC.NODE_PLUS ||
			lastDrawImgSrc == IMG_SRC.NODE_MINUS) {
			return NODE_STYLE.NODE
		} else if (lastDrawImgSrc == IMG_SRC.LAST_NODE ||
			lastDrawImgSrc == IMG_SRC.LAST_NODE_PLUS ||
			lastDrawImgSrc == IMG_SRC.LAST_NODE_MINUS) {
			return NODE_STYLE.LAST_NODE
		}
	} else {
		return NODE_STYLE.NO_NODE
	}
}

function createImg(type) {
	return IMG[type].cloneNode(false)
}

/*
misc functions for convenience and compatibility between Mozilla and IE
*/

//note: case-sensitive
function getFirstChildElementByTagName(node, tagName) {
	var child = node.firstChild
	while (child != null && child.tagName != tagName)
		child = child.nextSibling
	return child
}

//note: cast-sensitive
function getLastChildElementByTagName(node, tagName) {
	var child = node.lastChild
	while (child != null && child.tagName != tagName)
		child = child.previousSibling
	return child
}

//IE sometimes automatically converts relative URLs to absolute URLs
//	so I'll ensure they're all absolute URLs
//url must either be an absolute URL or a relative URL that does not start with a slash
function absoluteURL(url) {
	if (!absoluteURL.baseURL)
		absoluteURL.baseURL = location.protocol + '//' + location.host + location.pathname.substr(0, location.pathname.lastIndexOf('/') + 1);
	if (url.indexOf(location.protocol + '//') == -1)
		return absoluteURL.baseURL + url
	else
		return url
}

if (window.XMLSerializer) {
	var oXMLSerializer = new XMLSerializer()
}
function appendXMLNode(htmlNode, xmlNode) {
	if (xmlNode[0]) {	//array or node list
		for (var i = xmlNode.length - 1; i >= 0; i--) {
			appendXMLNode(htmlNode, xmlNode[i])
		}
	} else if (xmlNode.nodeType == Node.DOCUMENT_FRAGMENT_NODE) {	//document fragment
		for (var child = xmlNode.firstChild; child; child = child.nextSibling) {
			appendXMLNode(htmlNode, child)
		}
	} else if (xmlNode.nodeType == Node.TEXT_NODE) {	//text node
		htmlNode.appendChild(htmlNode.ownerDocument.createTextNode(xmlNode.data))
	} else {	//assume element
		//uncomment following code if insertAdjacentHTML support wasn't added to Moz
		/*if (oXMLSerializer) {
			if (oSerializerRange.createContextualFragment) {	//Moz
				htmlNode.appendChild(oSerializerRange.createContextualFragment(oXMLSerializer.serializeToString(xmlNode)))
			} else if (htmlNode.insertAdjacentHTML) {	//Opera
				htmlNode.insertAdjacentHTML('beforeEnd', oXMLSerializer.serializeToString(xmlNode))
			}
		} else if (htmlNode.insertAdjacentHTML && xmlNode.xml) {	//IE
			htmlNode.insertAdjacentHTML('beforeEnd', xmlNode.xml)
		}*/
		if (oXMLSerializer) {	//Moz and Opera
			var sHTML = oXMLSerializer.serializeToString(xmlNode)
		} else if (xmlNode.xml) {	//IE
			var sHTML = xmlNode.xml
		}
		htmlNode.insertAdjacentHTML('beforeEnd', sHTML)
	}
}
