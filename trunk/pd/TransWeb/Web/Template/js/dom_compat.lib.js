//DOM COMPATIBILITY SCRIPT
//last updated: 3/31/05 11:13 PM

//Moz must use appendChild and range.createContextualFragment
//	to replicate IE's insertAdjacentHTML functionality
if (document.createRange) {
	var oSerializerRange = document.createRange();
	//set bogus range position (just needs to be set)
	oSerializerRange.setStart(document.documentElement, 0);
}

//since insertAdjacentHTML is so useful and most browsers already support innerHTML
//	add support to Moz
if (window.HTMLElement) {
	if (!HTMLElement.prototype.insertAdjacentHTML && oSerializerRange && oSerializerRange.createContextualFragment) {
		HTMLElement.prototype.insertAdjacentHTML = function(sWhere, sHTML) {
			var docFrag = oSerializerRange.createContextualFragment(sHTML)
			switch (sWhere.toLowerCase()) {
				case 'beforebegin':
					this.parentNode.insertBefore(docFrag, this);
					break;
				case 'afterbegin':
					this.insertBefore(docFrag, this.firstChild);
					break;
				case 'beforeend':
					this.appendChild(docFrag);
					break;
				case 'afterend':
					this.parentNode.insertBefore(docFrag, this.nextSibling);
					break;
			}
		}
	}
}

if (!window.Node) {
	var Node = {
		ELEMENT_NODE: 1,
		ATTRIBUTE_NODE: 2,
		TEXT_NODE: 3,
		CDATA_SECTION_NODE: 4,
		ENTITY_REFERENCE_NODE: 5,
		ENTITY_NODE: 6,
		PROCESSING_INSTRUCTION_NODE: 7,
		COMMENT_NODE: 8,
		DOCUMENT_NODE: 9,
		DOCUMENT_TYPE_NODE: 10,
		DOCUMENT_FRAGMENT_NODE: 11,
		NOTATION_NODE: 12
	}
}
