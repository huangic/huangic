// GIPDeleteCommand
var GIPDeleteCommand = function()
{
    this.Name = 'GIPDelete';
};

GIPDeleteCommand.prototype =
{
    Execute : function()
    {
        FCK.ExecuteNamedCommand('Delete') ;
    },

    GetState : function()
    {
        return FCK.EditMode != FCK_EDITMODE_WYSIWYG ?
            FCK_TRISTATE_DISABLED :
            FCK.GetNamedCommandState('GIPDelete') ;
    }
};
FCKCommands.RegisterCommand('GIPDelete', new GIPDeleteCommand());

// Create the "GIPDelete" toolbar button.
var oDeleteItem = new FCKToolbarButton('GIPDelete', FCKLang['GIPDelete']);
oDeleteItem.IconPath = FCKPlugins.Items['gip'].Path + 'icon_delete.gif';
FCKToolbarItems.RegisterItem('GIPDelete', oDeleteItem);

// GIPBreakTagCommand
var GIPBreakTagCommand = function()
{
    this.Name = 'GIPBreakTag' ;
};

GIPBreakTagCommand.prototype =
{
    Execute : function()
    {
        FCKUndo.SaveUndoStep() ;
        FCK.InsertElement('br') ;
    },

    GetState : function()
    {
        return FCK.EditMode != FCK_EDITMODE_WYSIWYG ?
            FCK_TRISTATE_DISABLED :
            FCK.GetNamedCommandState('GIPBreakTag') ;
    }
};
FCKCommands.RegisterCommand('GIPBreakTag', new GIPBreakTagCommand());

// Create the "GIPBreakTag" toolbar button.
var oBreakTagItem = new FCKToolbarButton('GIPBreakTag', FCKLang['GIPBreakTag']);
oBreakTagItem.IconPath = FCKPlugins.Items['gip'].Path + 'icon_br.gif';
FCKToolbarItems.RegisterItem('GIPBreakTag', oBreakTagItem);