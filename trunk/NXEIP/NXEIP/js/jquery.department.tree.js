//部門樹
/// <reference path="jquery-1.4.2-vsdoc.js" />

;(function ($) { 
     
   
    //列舉直
    var Enum={

    "NodeType":{"All":0,"Child":1,"SameLevel":2,"Auth":3},
    "LeafType":{"Department":0,"People":1},
    "SelectMode":{"Multi":0,"Single":1},
    "PeopleStatus":{"All":0,"OnJob":1, "StopJob":3},
    "PeopleColumn":{"Name":1,"Title":2,"Ext":4},
    "PeopleType":{"General":0,"Contract":1}
    }


     selectNode=function(event,data){
       //alert(settings.postBackEnum.selectMode);
       
        
       listboxId='#'+settings.listBoxID;

       //判斷多選單選
        if(settings.params.SelectMode==Enum.SelectMode.Single){
            $(listboxId).find('option').remove();
        }

          //try{  
            itemA = $(this).parent();
            id = $(itemA).attr("id");
            nodeType = $(itemA).attr("rel");
            nodeName = $(itemA).attr("nName");

            
            if(settings.params.LeafType!=Enum.LeafType.Department){
                if (nodeType=="depart") {
                 return;
                }
               }

            itemReduplicate = false;
              //id = $(this).parent().attr("id");

            $(listboxId).children().each(function () {
                //判斷重負
                // alert($(this).val());

                if (id == $(this).val() ) {

                    itemReduplicate = true;
                }


            });

            if (!itemReduplicate) {
                $(listboxId).append($("<option/>").attr("value", id).text(nodeName));
                save();
            }
      
      //}catch(err){
      //  alert(err)
      //}
   }




     var settings={
                params:{ 
                    "TreeType":Enum.NodeType.All,
                    "LeafType":Enum.LeafType.Department,
                    "SelectMode":Enum.SelectMode.Mutil,
                    "PeopleStatus":Enum.PeopleStatus.All,
                    "PeopleType":Enum.PeopleStatus
                },
                "rootUrl":"lib/tree/DepartTreeMethod.ashx",
                
                "selectHandle":selectNode,
                "listBoxID":"",
                "peopleImg":"../image/v05.gif",
                "id":"",
                "parentSessionID":""
                };

//Entry Point
    $.fn.DepartTree=function(user_settings){
            //預設值
             var _default=settings;
               
              


             settings = $.extend(_default, user_settings);

             return new _init(this,settings);
    }


    _del=function(event){
        $("#"+settings.listBoxID+" option:selected").remove();
        save();
    
    }


    _parentSave=function(event){
        //呼叫AJAX 介接SESSION
         $.ajax({
                type: "POST",
                url: settings.rootUrl+"?session="+settings.id+"&parentSession="+settings.parentSessionID+"&mode=saveSession",
                data: "{\"para\":\"\"}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                complete: function() {

                //更新父容器

                if(settings.params.SelectMode==Enum.SelectMode.Multi){
                    try{
                    //parent.$("#"+settings.parentSessionID).children().remove().end().append($("#"+settings.listBoxID).children());
                    $(window.parent.document).find("#"+settings.parentSessionID).children().remove().end().append($("#"+settings.listBoxID).html());
                    //$("#"+settings.parentSessionID, top.document).children().remove().end().append($("#"+settings.listBoxID).children());
                             
                   
                  




                    }catch(ex){
                        //alert(ex);
                    }

                }else{
                    //parent.$("#"+settings.parentSessionID).val(
                    $(window.parent.document).find("#"+settings.parentSessionID).val(

                    $.trim($("#"+settings.listBoxID).html())
                    
                    );
                
                }

                  
                   self.parent.tb_remove();
                }
            });


    }

    function getOptions() {
            options = new Array();

            

            $("#"+settings.listBoxID).children().each(function(e) {
                options.push({value: $(this).val(), text: $(this).html() });

            });
            return options;
        
        }



     save=function() {


            //alert(self.parent.tb_ClientID);




            $.ajax({
                type: "POST",
                url: settings.rootUrl+"?session="+settings.id+"&mode=save",
                data: "{\"para\":" + JSON.stringify(getOptions()) + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                complete: function() {

                    //self.parent.updateClientID("test");

                //self.parent.__doPostBack(self.parent.tb_ClientID, '')
                //    self.parent.tb_remove();
                }
            });


        }



    _init=function(element,settings){
      //init


      $('.TreeDel').bind('click',_del);
      $('.TreeSave').bind('click',_parentSave);


       $.ajaxSetup({ "cache": false }); 
        //載入樹狀
            $(element).jstree({
                "types": {
                    "types": {
                        "people": {
                            "icon": {
                                "image": settings.peopleImg
                            }

                        }
                    }
                },
                "json_data": {
                    "ajax": {
                        "type": "POST",
                        "url": settings.rootUrl + "?mode=select",
                        //"contentType": "application/json; charset=utf-8",
                        "dataType": "json",
                        "async": "true",
                        "data":
                        function (n) {
                            //合併參數
                            _param={
                                "operation": "get_children",
                                "id": n.attr ? n.attr("id") : 0
                            };

                           _param = $.extend(settings.params, _param);

                            return _param;
                        }
                    }
                },
                "core": { strings: {
                    "loading": "讀取中……",
                    "new_node": "新資料夾"

                }
                },

                "themes": {
                    "theme": "classic"
                },

               


                "plugins": ["json_data", "core", "themes", "ui", "types"]





            })


            //.bind("select_node.jstree", select_node); // 選擇節點

               .delegate("a", "click", settings.selectHandle);

    } //treeInit

   


})(jQuery);