/*
檔案總管使用
需要
jstree1.0套件
jquery.jqGrid


*/



$(function() {
    $.ajaxSetup({ cache: false });

    $("#accordion").accordion({
        fillSpace: true
    });

    //顯示根目錄
    showFile("0");


    $("#userFolder").jstree({

        "json_data": {
            "ajax": {
                "type": "POST",
                "url": "FileFolder.ashx",
                //"contentType": "application/json; charset=utf-8",
                "dataType": "json",
                "async": "true",
                "data":
                        function(n) {
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
                check_move: function(m) {
                        
                        //需要判斷是不是同一個父代
                        if(m.op.attr("id")==m.np.attr("id")){return false};
                        
                        if(m.np.attr("tagName")=="DIV"){return false;}
                        
                        
                        
                        if(m.p === "before" || m.p === "after"){return false;}
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
            "drop_finish": function() {
                alert("DROP");
            },
            "drag_check": function(data) {
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
            "drag_finish": function() {
                alert("DRAG OK");
            }
        },




        "plugins": ["themes", "json_data", "core", "ui", "dnd","crrm", "contextmenu"]





    })


               .bind("dblclick_node.jstree",reloadNodeFile)
               .bind("move_node.jstree", moveNode)






})



//重新整理檔案
function reloadNodeFile(event, data){
 reloadFile(data.rslt.obj.attr("id"));
}

function reloadFile(id){
 $("#filelist").setGridParam({url:"Files.ashx?id="+id});
 
        $('#filelist').trigger("reloadGrid");

 
}


function moveNode(obj, ref, position, is_copy, is_prepared, skip_check){
 
//目錄搬移
 
 
alert(ref.rslt.o.attr("id")+" MOVE TO "+ref.rslt.r.attr("id")); 


  url="FolderHandle.ashx"
    data={ handle:"move",
            id:ref.rslt.o.attr("id"),
            pid:ref.rslt.r.attr("id")};
  AjaxHandle(url,data,$.noop(),$.noop());

}




function showFile(id) { 

$("#filelist").jqGrid({
           url:"Files.ashx?id="+id,
           datatype:"json",
           width:740,
           height: 350,
           colNames: ['名稱', '修改日期','檔案大小', '類型'],
           colModel:[ {name:'name',index:'name', width:400,align:"left"}, 
                      {name:'date',index:'date', width:100,align:"left"},
                      {name:'size',index:'size', width:100,align:"right"}, 
                      {name:'type',index:'type', width:100, align:"left"}
                      ],
                 
           caption:"檔案清單",
           
           
           });

}

function AjaxHandle(url,data,onSuccess,onError){
     $.ajax({
        type: "POST",
        url: url,
        data: data,
        //contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: onSuccess,
        error: onError
    });
}