// JavaScript Document

/**
 * NODE_LAST_FOCUS keeps track of which form element in the tab panel had last focus
 */

var NODE_LAST_FOCUS = null;

/**
 *
 * The TabPanel object is used to maintain information about a Tab Panel widget
 * including the collection of tab objects 
 * 
 * @contructor
 */
 

function TabPanel( id ) {

  this.id = id;
  this.tabs = new Array();
  this.current_tab = 0;


}

/**
 * init is a subclass of TabPanel and is used to initialize the event handlers and 
 * tabs in a tab panel.
 * 
 * @member TabPanel
 * @return nothing
 */
 
TabPanel.prototype.init = function() {

  this.node = document.getElementById(this.id);

  var obj = this;
 
  browser.addEvent(this.node, "click", function(event) { handleTabPanelClickEvent(event, obj); }, false);
  browser.addEvent(this.node, "keydown", function(event) {handleTabPanelKeyDownEvent(event, obj); }, true);
  browser.addEvent(this.node, "keypress", function(event) {handleTabPanelKeyPressEvent(event, obj); }, true);

  for(var i = 0; i < this.tabs.length; i++ )
     this.tabs[i].init();

   selectTabPanel( this, this.current_tab,true);
  
}

/**
 * add is a subclass of TabPanel 
 * The tab obj is added to the list of tabs in a Tab Panel object
 *
 * @member TabPanel
 * @param ( Tab Object) obj
 * @return nothing
 */
 
TabPanel.prototype.add = function(obj) {
  this.tabs[this.tabs.length] = obj;
}


/**
 *
 * The Tab object is used to maintain information about a panel in a Tab Panel widget 
 * including the node of the last control that had focus in the tab panel
 *
 * @params ( IDREF ) tab_id   the id of the tab control container element
 * @params ( IDREF ) panel_id the id of the panel container element
 * @contructor
 */
 
function Tab( tab_id, panel_id ) {
 
  this.tab_id = tab_id;
  this.panel_id = panel_id;
  this.node_last_focus = null;

}

/**
 * init is a subclass of Tab
 * The init function currentlt doesn't do anything right now
 * 
 * @member TabPanel
 * @return nothing
 */
 
Tab.prototype.init = function() {

  
}


/**
 * nextTabPanel moves to the next tab panel 
 * Updates the current tab panel index
 * If panel was selected before in current session focus is moved to last control with focus
 * Used by keyboard event handlers to change currrent tab panel
 *
 * @param ( TabPanel object) tab_panel is the TabPanel object associated with the event
 * @return nothing
 */
 
nextTabPanel = function( tab_panel ) {

   if( tab_panel.current_tab < (tab_panel.tabs.length - 1) ) {

	 new_tab = tab_panel.current_tab + 1;
	 
	 selectTabPanel( tab_panel, new_tab,false );

   } // endif

}


/**
 * previousTabPanel moves to the previous tab panel 
 * Updates the current tab panel index
 * If panel was selected before in current session focus is moved to last control with focus
 * Used by keyboard event handlers to change currrent tab panel
 *
 * @param ( TabPanel object) tab_panel is the TabPanel object associated with the event
 * @return nothing
 */
 
previousTabPanel = function( tab_panel ) {

   if( tab_panel.current_tab > 0 ) {
	 
	 new_tab = tab_panel.current_tab - 1;
	 
	 selectTabPanel( tab_panel, new_tab ,false  );
	   
   } // endif

}


/**
 * selectTabPanel moves to the previous tab panel 
 * Updates the current tab panel index
 * Used by keyboard event handlers to change currrent tab panel
 * If panel was selected before in current session focus is moved to last control with focus
 *
 * @param ( TabPanel object) tab_panel is the TabPanel object associated with the event
 * @param ( TabPanel object) index is the index in the panel list of the selected panel 
 * @return nothing
 */
 
selectTabPanel = function( tab_panel, index ,isInit ) {


 
   if( (index >= 0 ) && ( index < tab_panel.tabs.length ) ) {
   
    	for(var i = 0; i < tab_panel.tabs.length; i++ ) {
	 
	   if(i!=index)
	    {
		// Change the selected state of the current tab  
		var node_tab = document.getElementById(tab_panel.tabs[i].tab_id);

		node_tab.setAttribute("aria-selected","false");

		// change tab styling to indicate not selected
		node_tab.className = "tab";

		// Hide the current tab	 
		document.getElementById(tab_panel.tabs[i].panel_id).style.display = "none";

		// Set ARIA hidden attribute	 
		document.getElementById(tab_panel.tabs[i].panel_id).setAttribute( "aria-hidden", "true");

		// Put tab in list in the tabindex order
		document.getElementById(tab_panel.tabs[i].tab_id).tabIndex = -1;
	   } 
	}  
	
  

     // Move to the next tab
     tab_panel.current_tab = index;
	 
     // Change the selected state of the current tab  
     var node_tab = document.getElementById(tab_panel.tabs[tab_panel.current_tab].tab_id);

     node_tab.setAttribute("aria-selected","true");

     // change tab styling to indicate not selected
     node_tab.className = "tab selected";

     // Show the current tab	 
     document.getElementById(tab_panel.tabs[tab_panel.current_tab].panel_id).style.display = "block";

     // Set ARIA hidden attribute	 
     document.getElementById(tab_panel.tabs[tab_panel.current_tab].panel_id).setAttribute("aria-hidden", "false");


     // Put tab in list in the tabindex order
     document.getElementById(tab_panel.tabs[tab_panel.current_tab].tab_id).tabIndex = 0;

     // Get control with last focus
     if( tab_panel.tabs[tab_panel.current_tab].node_last_focus )
       tab_panel.tabs[tab_panel.current_tab].node_last_focus.focus();
     else   
        if(isInit!=true)     
           document.getElementById(tab_panel.tabs[tab_panel.current_tab].tab_id).focus();

   } // endif

}

/**
 * nextTab moves to the next tab in a tab panel 
 * Updates the current tab panel index
 * Used by keyboard event handlers to change currrent tab panel
 * Keyboard focus moves to the new tab 
 *
 * @param ( DOM node ) node_target of the element the event occured on
 * @param ( TabPanel object) tab_panel is the TabPanel object associated with the event
 * @return nothing
 */

nextTab = function( node_target, tab_panel ) {

   var index = indexOfNodeInTabList( node_target, tab_panel);

   if( (index >= 0 ) && (  index < (tab_panel.tabs.length - 1) ) ) {
	 
	 index++;
	 
	 selectTabPanel( tab_panel, index ,false );
	   	   
     document.getElementById(tab_panel.tabs[index].tab_id).focus();
	 
   } // endif

}

/**
 * previousTab moves to the previous tab in a tab panel 
 * Updates the current tab panel index
 * Used by keyboard event handlers to change currrent tab panel
 * Keyboard focus moves to the new tab 
 *
 * @param ( DOM node ) node_target of the element the event occured on
 * @param ( TabPanel object) tab_panel is the TabPanel object associated with the event
 * @return nothing
 */

previousTab = function( node_target, tab_panel ) {

   var index = indexOfNodeInTabList( node_target, tab_panel);

   if( index > 0 ) {
	 
	 index--;
	 
	 selectTabPanel( tab_panel, index ,false );
	   	   
     document.getElementById( tab_panel.tabs[index].tab_id).focus();

   } // endif

}

/**
 * indexNodeInTabList checcks to see if the node the event ocurred on is in the list of tab in the tab panel
 *
 * @param ( DOM node ) node_target of the element the event occured on
 * @param ( TabPanel object) tab_panel is the TabPanel object associated with the event
 * @return index of tab panel of current event target; -1 if node target is not in list
 */

indexOfNodeInTabList = function( node_target, tab_panel ) {
 
  var flag = -1;
  
  for(var i = 0; i < tab_panel.tabs.length; i++ ) {
       if( node_target == document.getElementById(tab_panel.tabs[i].tab_id) || node_target == document.getElementById( 's'+tab_panel.tabs[i].tab_id)) {
		  flag = i;
	  }  // endif

   }  // endfor

  return flag;
  
}

/**
 * isNodeInTabList checcks to see if the node the event ocurred on is in the list of tabs in the tab panel
 *
 * @param ( DOM node ) node_target of the element the event occured on
 * @param ( TabPanel object) tab_panel is the TabPanel object associated with the event
 * @return boolean true if the node is in the tab list, otherwise false
 */

isNodeInTabList = function( node_target, tab_panel ) {

  var flag = false;
  
  for(var i = 0; !flag &&  (i < tab_panel.tabs.length); i++ ) {
	  
	  if( node_target == document.getElementById( tab_panel.tabs[i].tab_id) ) {
		  flag = true;
	  } //endif
	  
  }  // endfor

//  alert(flag);
  return flag;
  
}

/**
 * handleTabPanelKeyPressEvent
 * This function helps stop TAB, PAGEDOWN and PAFEUP propagation
 *
 * @param ( event object ) event
 * @param ( TabPanel object) tab_panel is the TabPanel object associated with the event
 * @return boolean false if the event was used by Tab Panel, otherwisee true
 */

function handleTabPanelKeyPressEvent(event, tab_panel) {
    var e = event || window.event;

    switch( e.keyCode ) {

      case KEY_PAGEUP:
      case KEY_PAGEDOWN:
           if( e.ctrlKey && !e.altKey && !e.shiftKey ) {
             return browser.stopPropagation(e);
           } // endif
           break;

      case KEY_TAB:
           if( e.ctrlKey && !e.altKey ) {
             return browser.stopPropagation(e);
           } // endif
           break;

    }

}


/**
 * handleTabPanelKeyDownEvent 
 * Keyboard commands for the Tab Panel
 *
 * @param ( event object ) event 
 * @param ( TabPanel object) tab_panel is the TabPanel object associated with the event
 * @return boolean false if the event was used by Tab Panel, otherwisee true
 */

function handleTabPanelKeyDownEvent(event, tab_panel) {
    var e = event || window.event;
 
    // Save information about a modifier key being pressed
		// May want to ignore keyboard events that include modifier keys 
    var no_modifier_pressed_flag = !e.ctrlKey && !e.shiftKey;	
    var control_modifier_pressed_flag = e.ctrlKey && !e.shiftKey && !e.altKey;

    switch( e.keyCode ) {
		
      case KEY_LEFT:
	  case KEY_UP:	
	       if( isNodeInTabList( browser.target(e), tab_panel ) && no_modifier_pressed_flag ) {
             previousTab( browser.target(e), tab_panel);
             return browser.stopPropagation(event);
           } // endif
           break;

      case KEY_RIGHT:
	  case KEY_DOWN:	 
	       if( isNodeInTabList( browser.target(e), tab_panel ) && no_modifier_pressed_flag  ) {
             nextTab( browser.target(e), tab_panel);
             return browser.stopPropagation(e);
           } // endif
           break;

      case KEY_PAGEDOWN:
           if( control_modifier_pressed_flag ) {
             nextTabPanel( tab_panel );			 
             return browser.stopPropagation(e);
           }
           break;

      case KEY_PAGEUP:
           if( control_modifier_pressed_flag ) {			   
             previousTabPanel( tab_panel );
             return browser.stopPropagation(e);
           }
           break;

      case KEY_HOME:
      case KEY_ALT:	   
	     if( no_modifier_pressed_flag ) {
	     selectTabPanel( tab_panel, 0 ,false );
             return browser.stopPropagation(e);			   
		   }

      case KEY_END:
	       if( no_modifier_pressed_flag ) {
             selectTabPanel( tab_panel, (tab_panel.tabs.length - 1) ,false  );
             return browser.stopPropagation(e);			   
		   }

      case KEY_TAB:
           if( e.ctrlKey && !e.altKey ) {

             if( e.shiftKey ) 
			 {
                previousTabPanel( tab_panel );	
				// previousTab( browser.target(e), tab_panel);				
			}
             else 
			 {
                nextTabPanel( tab_panel );
              //  nextTab( browser.target(e), tab_panel);				
			}
             return browser.stopPropagation(e);
			 
	         }
 			
				
          break;

	}
  
  return true;
   
}

/**
 * handleTabPanelClickEvent
 * handlers changing tab panels when the user uses the mouse to select a tab
 *
 * @param ( event object ) event 
 * @param ( TabPanel object) tab_panel is the TabPanel object associated with the event
 * @return boolean false if the event was used by Tab Panel, otherwisee true
 */

handleTabPanelClickEvent = function(event, tab_panel) {

  var e = event || window.event;
	 
   var index = indexOfNodeInTabList( browser.target(e), tab_panel);
  
   
    if( index >= 0 ) {

	 selectTabPanel(tab_panel, index,false  );	   	   
     document.getElementById(tab_panel.tabs[index].tab_id).focus();

     return false;

   } // endif

  return true;
   
}

/**
 * handleInputFocusEvent
 * handlers keeping track of which HTML input control last had focus
 * Used by tab panels to restore keyboard focus when using some keyboard commands
 *
 * @param ( event object ) event 
 * @param ( TabPanel object) tab is the Tab object associated with the event
 */

handleInputFocusEvent = function(event, tab) {
   var e = event || window.event;

   tab.node_last_focus = browser.target(e);
   
}

 
