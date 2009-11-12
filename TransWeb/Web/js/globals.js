/**
 *
 * The Globale Variables
 */

if (!window.Node) {
  var Node = {            // If there is no Node object, define one
    ELEMENT_NODE: 1,    // with the following properties and values.
    ATTRIBUTE_NODE: 2,  // Note that these are HTML node types only.
    TEXT_NODE: 3,       // For XML-specific nodes, you need to add
    COMMENT_NODE: 8,    // other constants here.
    DOCUMENT_NODE: 9,
    DOCUMENT_FRAGMENT_NODE: 11
  }
} 


var KEY_PAGEUP   = 33;
var KEY_PAGEDOWN = 34;
var KEY_END      = 35;
var KEY_HOME     = 36;

var KEY_LEFT     = 37;
var KEY_UP       = 38;
var KEY_RIGHT    = 39;
var KEY_DOWN     = 40;

var KEY_SPACE    = 32;
var KEY_TAB      = 9;

var KEY_BACKSPACE = 8;
var KEY_DELETE    = 46;
var KEY_ENTER     = 13;
var KEY_INSERT    = 45;
var KEY_ESCAPE    = 27;
var KEY_ALT       = 18; 

var NS_XHTML = "http://www.w3.org/1999/xhtml"
var NS_STATE = "http://www.w3.org/2005/07/aaa";

// **********************************************
// *
// * Commonly used helper functions
// *
// **********************************************

/**
 *
 * nextSiblingElement
 * 
 * @contructor
 */
 
function nextSiblingElement( node ) {

  var next_node = node.nextSibling;

  while( next_node
		&& (next_node.nodeType != Node.ELEMENT_NODE) ) {
	  next_node = next_node.nextSibling;
  }  // endwhile

  return next_node;
  
}

/**
 *
 * previousSiblingElement 
 * 
 * @param ( node ) node object for which you are looking for the next sibling element node
 *
 * @return ( node) next sibling or "null"
 */
 
function previousSiblingElement( node ) {

  var next_node = node.previousSibling;

  while( next_node
		&& (next_node.nodeType != Node.ELEMENT_NODE) ) {
	  next_node = next_node.previousSibling;
  }  // endwhile

  return next_node;
  
}

/**
 *
 * firstChildElement 
 * 
 * @param ( node ) node object for which you are looking for the first child element node
 *
 * @return ( node) next sibling or "null"
 */
 
function firstChildElement( node ) {

  var next_node = node.firstChild;

  while( next_node
		&& (next_node.nodeType != Node.ELEMENT_NODE) ) {
	  next_node = next_node.nextSibling;
  }  // endwhile


  return next_node;
  
}

/**
 *
 * getTextContentOfNode
 * 
 * @contructor
 */
 
function getTextContentOfNode( node ) {

  var next_node = node.firstChild;
  var str = "";

  while( next_node ) {
		
	  if( (next_node.nodeType == Node.TEXT_NODE ) &&
		  (next_node.length > 0 )
		 )
	    str += next_node.data;
	  
	  
	  next_node = next_node.nextSibling;
	  
  }  // endwhile

  return str;
  
}

/**
 *
 * setTextContentOfNode
 * 
 * @contructor
 */
 
function setTextContentOfNode( node, text ) {

   // Generate a new text node with the text value
    var text_node = document.createTextNode(text);
  
    // Remove child nodes to remove text
    while (node.firstChild) {
      node.removeChild(node.firstChild);
    } // while

    // Append new text to the container element
    node.appendChild( text_node );

}
