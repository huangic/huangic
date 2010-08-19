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

                    }, "moveFile": {
                        "label": "檔案搬移",
                        "action": moveFile
                    },
                    "copyFile": {
                        "label": "檔案複製",
                        "action": copyFile
                    }

                }
            },



            "plugins": ["themes", "json_data", "core", "ui", "dnd", "crrm", "contextmenu", "unique", "cookies"]





        })


               .bind("select_node.jstree", reloadNodeFile)
               .bind("move_node.jstree", moveNode)
               .bind("create_node.jstree", addNode)
               .bind("rename_node.jstree", renameNode)
               .bind("delete_node.jstree", deleteNode)


        // JSTREE 設定
    


     //重新整理檔案
    function reloadNodeFile(event, data) {
        reloadFile(data.rslt.obj.attr("id"));
    };

    var reloadFile=function(id) {
        
       
        $( _setting.fileDiv).setGridParam({ url: "FilesGrid.ashx?id=" + id });

        $( _setting.fileDiv).trigger("reloadGrid");


    };

  


    function moveNode(obj, ref, position, is_copy, is_prepared, skip_check) {

        //目錄搬移


        //alert(ref.rslt.o.attr("id")+" MOVE TO "+ref.rslt.r.attr("id")); 


        url = "FolderHandle.ashx"
        data = { handle: "move",
            id: ref.rslt.o.attr("id"),
            pid: ref.rslt.r.attr("id")
        };
        AjaxHandle(url, data, $.noop(), $.noop());

    };




    function showFile(id) {

        $( _setting.fileDiv).jqGrid({
            url: "FilesGrid.ashx?id=" + id,
            datatype: "json",
            width: 740,
            height: 350,
            colNames: ['名稱', '修改日期', '檔案大小', '類型', '動作'],
            colModel: [{ name: 'name', index: 'name', width: 400, align: "left", editable: true },
                      { name: 'date', index: 'date', width: 100, align: "left" },
                      { name: 'size', index: 'size', width: 100, align: "right" },
                      { name: 'type', index: 'type', width: 100, align: "left",sortable: false },
                      { name: 'act', index: 'act', width: 100, sortable: false },
                      ],
            multiselect: true,

            loadComplete: function () {
                var ids = jQuery( _setting.fileDiv).getDataIDs();
                for (var i = 0; i < ids.length; i++) {
                    var cl = ids[i];
                    be = "<input class='a-input' type='button' value='修改' onclick=jQuery('#filelist').editRow(" + cl + "); ></ids>";
                    ce = "<input class='a-input' type='button' value='版本' onclick=jQuery('#filelist').editRow(" + cl + "); ></ids>";
                    jQuery("#filelist").setRowData(ids[i], { act: be + ce })
                }

                //無資料寫是空值
                if ($( _setting.fileDiv).getGridParam('records') == 0) // are there any records?
                    DisplayEmptyText(true);
                else
                    DisplayEmptyText(false);


            },

            caption: "檔案清單",
            emptyDataText: "<div id='EmptyData'>目前無檔案</div>"

        });

        //$("#filelist").jqGrid('gridDnD', { connectWith: '#userFolder' }); 

    };


     function AjaxHandle(url, data, onSuccess) {
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
            pid: position.rslt.parent.attr("id")
        };
        AjaxHandle(url, data, handleAddNode);

        function handleAddNode(data) {
            //alert(position.rslt.obj);
            //寫入新ID
            position.rslt.obj.attr("id", data.id);
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
        AjaxHandle(url, data, $.noop());


    };


    function moveFile(obj) {
        //設定要搬移的目的地
        var folder_id = $(obj).attr("id");
        var s;
        s = jQuery("#filelist").jqGrid('getGridParam', 'selarrrow');
        var data = { 
            "handle":"move",
            "folderId": folder_id,
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
          }else{
           alert(data.msg);
          }

        }

    };

    function copyFile(obj) {
        //設定要搬移的目的地
        var folder_id = $(obj).attr("id");
        var s;
        s = jQuery("#filelist").jqGrid('getGridParam', 'selarrrow');
        var data = { 
            "handle":"copy",
            "folderId": folder_id,
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
          }else{
           alert(data.msg);
          }

        }

    };





    }//_init end


    $.fn.fileManager=function(settings){
        var _defaultSettings={
            treeDiv:"#treeDiv",
            fileDiv:"#fileDiv"
           
        };


        var _settings = $.extend(_defaultSettings, settings);

        _init(_settings);
    };
})(jQuery); 