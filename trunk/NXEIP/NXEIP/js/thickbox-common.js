/// <reference path="jquery-1.3.2-vsdoc.js" />

// This is the initial jQuery starting point.  The 'main' function is run once the DOM of every page is established.

  $(document).ready(function() {
    main();
  });

  function main() {
    // Nothing to do at this time
  }


  // This function must be called with all modal dialog boxes which have fields that
  // require validation.  It performs a call to the ASP.Net function, "Page_ClientValidate()",
  // and then continues on calling "CloseModalDialog()" if the validation is okay.
  // Note: Surprisingly, companion functions like "ValidatorValidate()" do not work, 
  // perhaps due to the way ThickBox rearranges DOM elements.
  function CheckValidationBeforeClose(element) {
    if (Page_ClientValidate())
      CloseModalDialog(element);
  }


  // Used to close a ThickBox modal dialog and then force a postback,
  // which a server-side control is seemingly unable to do on its own
  // when fired from within the confines of a ThickBox object.
  function CloseModalDialog(element) {
   // tb_remove();
    setTimeout('__doPostBack(\'' + element.name + '\',\'\')', 500);  // 500ms seems to give ThickBox enough time to remove itself
  }


  // Used to close a ThickBox modal dialog without causing a postback.
  function CancelModalDialog() {
    tb_remove();
  }



  // Prepares all textboxes such that the background changes to a distinct
  // color upon focus and then returns to white after focus is lost.
  function PrepareDefaultEventHandlers() {
    $(":text").focus(textboxHighlight).blur(textboxRemoveHighlight);
    $(":password").focus(textboxHighlight).blur(textboxRemoveHighlight);
    $("textarea").focus(textboxHighlight).blur(textboxRemoveHighlight);
  }

  function textboxHighlight() {
    $("#" + this.id).css({ 'background-color': '#ffff40' });
  }

  function textboxRemoveHighlight() {
    $("#" + this.id).css({ 'background-color': 'white' });
  }