/// <reference path="jquery-1.4.1-vsdoc.js" />
/*
檔案總管使用
需要
thickbox
jstree1.0套件
jquery.jqGrid


*/

;(function ($) {

    var init=function(element,_setting) {
    tb_init('a.thickbox input.thickbox');

    $.ajaxSetup({ cache: false });
   
    //alert(element); 

   function initButton(){
     $(_setting.fileDeleteButton).hide();
     $(_setting.fileMoveButton).hide();
     $(_setting.permissionButton).hide();
     $(_setting.fileUploadButton).hide();
     $(_setting.filePublicButton).hide();
     $(_setting.fileCopyButton).hide();
   }
     

   


   

   
   $(element).click(function(){
     //alert($(element).attr("id"));
     
     
     n=$("#searchName").val();
     c=$("#searchContext").val();
     
     
     reloadFile(n,c);
     
   });

    


    

    var reloadFile=function(name,context) {
        
       
        $( _setting.fileDiv).setGridParam({ 
        loadComplete:loadComplete,
        url: "100105/SearchFilesGrid.ashx?n=" + name+"&c="+context
        
        });

       

        $( _setting.fileDiv).trigger("reloadGrid");

         initButton();
    };

  


  




   



        function loadComplete(){
             var ids = jQuery( _setting.fileDiv).getDataIDs();

               

                for (var i = 0; i < ids.length; i++) {
                    var cl = ids[i];

                     //ret就是ROW資料
                     var ret = jQuery( _setting.fileDiv).jqGrid('getRowData',cl); 
                     //alert("id="+ret.id+" code="+ret.code);
                     dlUrl="100105/FileDownload.ashx?code="+ret.code;
                    

                    //be = "<a class='edit imageButton' href='#' alt='版本' title='版本' ><span>版本</span></a>";
                    
                    dl = "<a class='download imageButton' target='_blank' href='"+dlUrl+"' alt='下載' title='下載'><span>下載</span></a>";
                    
                    //del = "<a class='fileDelete imageButton' href='#' alt='刪除' title='刪除'><span>刪除</span></a>";
                    jQuery(_setting.fileDiv).setRowData(ids[i], { act: dl })
                }

                //無資料寫是空值
                if ($( _setting.fileDiv).getGridParam('records') == 0) // are there any records?
                    DisplayEmptyText(true);
                else
                    DisplayEmptyText(false);
        }


     function AjaxHandle(url, data, onSuccess) {
         $.ajaxSetup({ cache: false });
        $.post(url, data, onSuccess, "json");
    };

    function DisplayEmptyText(display) {
        var grid = $( _setting.fileDiv);
        var emptyText = grid.getGridParam('emptyDataText'); // get the empty text
        var container = grid.parents('.ui-jqgrid-view'); // find the grid's container
        if (display) {
            container.find('#EmptyData').remove(); // remove the empty data text
            container.find('.ui-jqgrid-hdiv, .ui-jqgrid-bdiv').hide(); // hide the column headers and the cells below
            container.find('.ui-jqgrid-titlebar').after('' + emptyText + ''); // insert the empty data text
        }
        else {
            container.find('.ui-jqgrid-hdiv, .ui-jqgrid-bdiv').show(); // show the column headers

            container.find('#EmptyData').remove(); // remove the empty data text
        }
    };


   
   
    
    
    
   



    }//_init end


    $.fn.searchfileManager=function(settings){
        var _defaultSettings={
            fileDiv:"#filelist",
            fileDeleteButton:"#delFile",
            fileMoveButton:"#moveFile",
            fileCopyButton:"#copyFile",
            fileUploadButton:"#addFile",
            filePublicButton:"#publicFile",
            permissionButton:"#permissionButton",
            inputName:"searchName",
            inputContext:"seatchContext"
        };


        var _settings = $.extend(_defaultSettings, settings);
        
     
     return new init(this,_settings);   
    };
})(jQuery); 