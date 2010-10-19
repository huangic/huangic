/// <reference path="jquery-1.4.1-vsdoc.js" />
/*
檔案總管使用
需要
thickbox
jstree1.0套件
jquery.jqGrid


*/

;(function ($) {

    var _init=function(_setting) {
    tb_init('a.thickbox input.thickbox');

    $.ajaxSetup({ cache: false });
   
        

   function initButton(){
     $(_setting.fileDeleteButton).hide();
     $(_setting.fileMoveButton).hide();
     $(_setting.permissionButton).hide();
     $(_setting.fileUploadButton).hide();
      $(_setting.filePublicButton).hide();
   }
    

    //$(_setting.fileDeleteButton).bind("click",delFile);

    //$(_setting.fileMoveButton).bind("click",OpenMoveTree);
    //$(_setting.fileCopyButton).bind("click",OpenCopyTree);

    //$(_setting.permissionButton).bind("click",openPermissionDialog);


    function test(){
       
        //alert($.jstree._focused().get_selected());
    }

   


        //顯示根目錄
        
        

        $(_setting.treeDiv).jstree({

            "json_data": {
                "ajax": {
                    "type": "POST",
                    "url": "PublicFolderTree.ashx",
                    //"contentType": "application/json; charset=utf-8",
                    "dataType": "json",
                    "async": "true",
                    "data":
                        function (n) {
                            return {
                                "operation": "get_children",
                                "id": n.attr? n.attr("id") : 0,
                                "peoid": n.attr?n.attr("peoid"):"-1"
                            };
                        }
                }
            },
            "core": { strings: {
                "loading": "讀取目錄中……",
                "new_node": "新資料夾"

            }
            },

            "themes": {
                "theme": "classic"
            },
            

           
            



            "plugins": ["themes", "json_data", "core", "ui"]





        })


               .bind("select_node.jstree", reloadNodeFile)
              


        // JSTREE 設定
    


     //重新整理檔案
    function reloadNodeFile(event, data) {
      
        id=data.rslt.obj.attr("id");
        peoid=data.rslt.obj.attr("peo_id");
        
        
        reloadFile(id,peoid);

        //屬性寫入COOKIES
        //$.cookie("depid",depid);
        //$.cookie("folderType",folderType);

    };

    var reloadFile=function(id,peoid) {
        
       
        $( _setting.fileDiv).setGridParam({ 
        loadComplete:loadComplete,
        url: "PermissionFilesGrid.ashx?id=" + id+"&peo_id="+peoid
        
        });

       

        $( _setting.fileDiv).trigger("reloadGrid");

         initButton();
    };

  


  




    function showFile(id) {

        $( _setting.fileDiv).jqGrid({
            url: "FilesGrid.ashx?id=" + id,
            datatype: "json",
            width: 740,
            height: 350,
            colNames: ['名稱', '修改日期', '檔案大小', '類型','編碼', '動作'],
            colModel: [{ name: 'name', index: 'name', width: 400, align: "left", editable: true },
                      { name: 'date', index: 'date', width: 100, align: "left" },
                      { name: 'size', index: 'size', width: 100, align: "right" },
                      { name: 'type', index: 'type', width: 100, align: "left",sortable: false },
                      {name:'code',index:'code',hidden:true},
                      { name: 'act', index: 'act', width: 100, sortable: false },
                      ],
            multiselect: true,

            loadComplete: loadComplate,

            caption: "檔案清單",
            emptyDataText: "<div id='EmptyData'>目前無檔案</div>"

        });

        //$("#filelist").jqGrid('gridDnD', { connectWith: '#userFolder' }); 

    };



        function loadComplete(){
             var ids = jQuery( _setting.fileDiv).getDataIDs();

               

                for (var i = 0; i < ids.length; i++) {
                    var cl = ids[i];

                     //ret就是ROW資料
                     var ret = jQuery( _setting.fileDiv).jqGrid('getRowData',cl); 
                     //alert("id="+ret.id+" code="+ret.code);
                     dlUrl="FileDownload.ashx?code="+ret.code;
                    

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


   
   
    
    
    function showHandleTree(){
    
    $(_setting.handleTree).jstree({

            "json_data": {
                "ajax": {
                    "type": "POST",
                    "url": "FileFolder.ashx",
                    //"contentType": "application/json; charset=utf-8",
                    "dataType": "json",
                    "async": "true",
                    "data":
                        function (n) {
                            return {
                                "operation": "get_children",
                                "id": n.attr ? n.attr("id") : 0
                            };
                        }
                }
            },
            "core": { strings: {
                "loading": "讀取目錄中……",
                "new_node": "新資料夾"

            }
            },

            "themes": {
                "theme": "classic"
            },
           

            
          



            "plugins": ["themes", "json_data", "core", "ui"]





        })
    }
   



    }//_init end


    $.fn.publicfileManager=function(settings){
        var _defaultSettings={
            treeDiv:"#treeDiv",
            publicDiv:"#publicDiv",
            fileDiv:"#fileDiv",
            handleTree:"#handleTree",
            dialog:"#dialog",
            permissionDialog:"#permissionDialog",
            permissionButton:"#permissionButton",
            fileDeleteButton:"#deleteFile",
            fileMoveButton:"#moveFile",
            fileCopyButton:"#copyFile",
            fileUploadButton:"#addFile",
            fileHandleOKButton:"#handleOK",
            filePublicButton:"#publicFile"
        
        };


        var _settings = $.extend(_defaultSettings, settings);
        _init(_settings);
        
    };
})(jQuery); 