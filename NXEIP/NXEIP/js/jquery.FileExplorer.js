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
   
        

   
    

    $(_setting.fileDeleteButton).bind("click",delFile);

    $(_setting.fileMoveButton).bind("click",OpenMoveTree);
    $(_setting.fileCopyButton).bind("click",OpenCopyTree);
    $(_setting.permissionButton).bind("click",openPermissionDialog);


    function test(){
       
        //alert($.jstree._focused().get_selected());
    }

   function initButton(){
     $(_setting.fileDeleteButton).show();
     $(_setting.fileMoveButton).show();
     $(_setting.permissionButton).show();
     $(_setting.fileUploadButton).show();
      $(_setting.filePublicButton).show();
   }

   //檔案權限
   function openPermissionDialog(){
     //$.log("permission");
        
        var s;
        s = jQuery(_setting.fileDiv).jqGrid('getGridParam', 'selarrrow');

        if(s.length==0){
         alert("請選擇檔案");
         return;
        }

        if(s.length>1){
         alert("只可設定一個檔案");
         return;
        }

        //寫到cookies去

        $.cookie("permissionFiles",s);


        //顯示權限視窗
        var t = this.title || this.name || null;
	    var a = this.href || this.alt;
	    var g = this.rel || false;
	    
       
        
        
        tb_show(t,a,g);
	    this.blur();
        return false;

   }



    //檔案搬移
    function OpenMoveTree(){
        //$.log("MoveTree");
        
        var s;
        s = jQuery(_setting.fileDiv).jqGrid('getGridParam', 'selarrrow');

        if(s.length==0){
         alert("請選擇檔案");
         return;
        }



        showHandleTree();

        
        //建立樹狀
        $(_setting.dialog).dialog({
         modal:true,
         title:"檔案搬移",
         autoOpen:false,
         buttons: {
         "取消":function(){$(_setting.dialog).dialog('close')},
         "搬移":function(){
            node=$.jstree._focused().get_selected();
            moveFile(node);
            $(_setting.dialog).dialog("close");
         }}


        });


        $(_setting.dialog).dialog("open");
        //改變確定紐


    }

     //檔案複製
    function OpenCopyTree(){
       // $.log("MoveTree");
        //jQuery(_setting.handleTree).show();
         var s;
        s = jQuery(_setting.fileDiv).jqGrid('getGridParam', 'selarrrow');

        if(s.length==0){
         alert("請選擇檔案");
         return;
        }


         showHandleTree();

        
        //建立樹狀
        $(_setting.dialog).dialog({
         modal:true,
         title:"檔案複製",
         autoOpen:false,
         buttons: {
         "取消":function(){$(_setting.dialog).dialog('close')},
         "複製":function(){
            node=$.jstree._focused().get_selected();
            copyFile(node);
            $(_setting.dialog).dialog("close");
         }}


        });


        $(_setting.dialog).dialog("open");


    }



    //檔案刪除
    function delFile(){
         //$.log("DelFile");
        //取GRID的SELECTED
        var s;
        s = jQuery(_setting.fileDiv).jqGrid('getGridParam', 'selarrrow');

        if(s.length==0){
        alert("請選擇檔案");
         return ;
        }

        if(confirm("確定要刪除?")){

        var data = { 
            "handle":"delete",
            "files": s

        };
        var url = "FileHandle.ashx";
        var jsonData = JSON.stringify(data) ;
        AjaxHandle(url, ""+jsonData, success);
        }
        //讀取 FILE 傳入 FOLDER
        //檢查節點
        function success(data){
          if(data.msg=="success"){
            $( _setting.fileDiv).trigger("reloadGrid");
          }else{
            alert(data.msg);
          }

        };        

    };


    




        //顯示根目錄
        
        
        
        showFile("0");


        $(_setting.treeDiv).jstree({

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
            "crrm": {
                move: {
                    default_position: "inside",
                    check_move: function (m) {

                        //需要判斷是不是同一個父代
                        if (m.op.attr("id") == m.np.attr("id")) { return false };

                        if (m.np.attr("tagName") == "DIV") { return false; }



                        if (m.p === "before" || m.p === "after") { return false; }
                        //  var p = this._get_parent(m.o);
                        //     if(!p) return false;
                        //      p = p == -1 ? this.get_container() : p;
                        //      if(p === m.np) return true;
                        //  if(p[0] && m.np[0] && p[0] === m.np[0]) return true;


                        return true;
                    }

                }
            },

            "dnd": {
                "drag_target": false, //"tr[aria-selected='true']", 


                "drop_finish": function () {
                    alert("DROP");
                },
                "drag_check": function (data) {
                    if (data.r.attr("id") == "userFolder") {
                        alert("NO");
                        return false;
                    }
                    return {
                        after: false,
                        before: false,
                        inside: true
                    };
                },
                "drag_finish": function () {
                    alert("DRAG OK");
                }
            },
            "contextmenu": {
                items: {
                    "create": {
                        "label": "新增資料夾"
                    },
                    "rename": {

                        "label": "更名"

                    },
                    "remove": {



                        "label": "刪除"

                    }

                }
            },



            "plugins": ["themes", "json_data", "core", "ui", "dnd", "crrm", "contextmenu", "unique", "cookies"]





        })


               .bind("select_node.jstree", reloadNodeFile)
               .bind("move_node.jstree", moveNode) //移動目錄
               .bind("create_node.jstree", addNode) //建立目錄
               .bind("rename_node.jstree", renameNode) //變更名稱
               .bind("delete_node.jstree", deleteNode) //刪除節點


        // JSTREE 設定
    


     //重新整理檔案
    function reloadNodeFile(event, data) {
      
        id=data.rslt.obj.attr("id");
        depid=data.rslt.obj.attr("depid");
        folderType=data.rslt.obj.attr("folderType");
        
        reloadFile(id,depid,folderType);

        //屬性寫入COOKIES
        $.cookie("depid",depid);
        $.cookie("folderType",folderType);

    };

    var reloadFile=function(id,depid,folderType) {
        
       
        $( _setting.fileDiv).setGridParam({ 
        loadComplete:loadComplete,
        url: "FilesGrid.ashx?id=" + id+"&depid="+depid+"&folderType="+folderType 
        
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

            loadComplete: loadComplete,

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
                    

                    be = "<a class='edit imageButton' href='#' alt='版本' title='版本' ><span>版本</span></a>";
                    
                    dl = "<a class='download imageButton' target='_blank' href='"+dlUrl+"' alt='下載' title='下載'><span>下載</span></a>";
                    
                    del = "<a class='fileDelete imageButton' href='#' alt='刪除' title='刪除'><span>刪除</span></a>";
                    jQuery(_setting.fileDiv).setRowData(ids[i], { act: be +dl+del })
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


   

    function addNode(obj, position, js, callback, is_loaded) {
        //alert("New");

        url = "FolderHandle.ashx"
        data = { handle: "create",
            //id: position.rslt.obj.attr("id"),
            pid: position.rslt.parent.attr("id"),
            depid:position.rslt.parent.attr("depid"),
            folderType:position.rslt.parent.attr("folderType")
        };
        AjaxHandle(url, data, handleAddNode);

        function handleAddNode(data) {
            //alert(position.rslt.obj);
            //寫入新ID
            position.rslt.obj.attr("id", data.id);
            position.rslt.obj.attr("depid", position.rslt.parent.attr("depid"));
            position.rslt.obj.attr("folderType", position.rslt.parent.attr("folderType"));

        }

    };

    function renameNode(obj, val) {
        //alert("New");

        url = "FolderHandle.ashx"
        data = { handle: "rename",
            //id: position.rslt.obj.attr("id"),
            "id": val.rslt.obj.attr("id"),
            "name": val.rslt.name
        };
        AjaxHandle(url, data, $.noop());


    };


      function moveNode(obj, ref, position, is_copy, is_prepared, skip_check) {

        //目錄搬移

       
        //alert(ref.rslt.o.attr("id")+" MOVE TO "+ref.rslt.r.attr("id")); 


        url = "FolderHandle.ashx"
        data = { handle: "move",
            id: ref.rslt.o.attr("id"),
            pid: ref.rslt.r.attr("id"),
            depid:ref.rslt.r.attr("depid"),
            folderType:ref.rslt.r.attr("folderType")
        };
        AjaxHandle(url, data,  $.noop(), $.noop());

       //目錄搬移後 更新舊節點的部門編號跟目錄樣式

       

    };


    function deleteNode(obj, val) {
        //alert("New");

      


        if (val.rslt.obj.attr("id") == 0) {
            return false;
        }



        url = "FolderHandle.ashx"
        data = { handle: "delete",
            //id: position.rslt.obj.attr("id"),
            "id": val.rslt.obj.attr("id")

        };
        AjaxHandle(url, data, function(){ alert("刪除成功!")});

        
        

    };


    function moveFile(obj) {
        //設定要搬移的目的地
        var folder_id = $(obj).attr("id");
        var depid=$(obj).attr("depid");
        var folderType=$(obj).attr("folderType");
        var s;
        s = jQuery(_setting.fileDiv).jqGrid('getGridParam', 'selarrrow');
        var data = { 
            "handle":"move",
            "folderId": folder_id,
            "depid":depid,
            "folderType":folderType,
            "files": s

        };
        var url = "FileHandle.ashx";
        var jsonData = JSON.stringify(data) ;
        AjaxHandle(url, ""+jsonData, moveSuccess);

        //讀取 FILE 傳入 FOLDER
        //檢查節點
        function moveSuccess(data){
          
          
          if(data.msg=="success"){
          $( _setting.fileDiv).trigger("reloadGrid");
          alert("搬移成功");
          }else{
           alert(data.msg);
          }

        }

    };

    function copyFile(obj) {
        //設定要搬移的目的地
        var folder_id = $(obj).attr("id");
        var depid=$(obj).attr("depid");
        var folderType=$(obj).attr("folderType");

        var s;
        s = jQuery(_setting.fileDiv).jqGrid('getGridParam', 'selarrrow');
        var data = { 
            "handle":"copy",
            "folderId": folder_id,
            "depid":depid,
            "folderType":folderType,
            "files": s

        };
        var url = "FileHandle.ashx";
        var jsonData = JSON.stringify(data) ;
        AjaxHandle(url, ""+jsonData, success);

        //讀取 FILE 傳入 FOLDER
        //檢查節點
        function success(data){
          if(data.msg=="success"){
            $( _setting.fileDiv).trigger("reloadGrid");
            alert("複製成功!");
          }else{
            alert(data.msg);
          }

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


    $.fn.fileManager=function(settings){
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