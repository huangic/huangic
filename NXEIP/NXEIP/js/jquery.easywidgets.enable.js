
// DOM ready!

$(function() {

    // Very basic usage
    //init();

});


function widget_init() {
    $.fn.EasyWidgets(

  {
      behaviour: {
          dragRevert: 300
      },

      i18n: {
          editText: "編輯",
          closeText: "關閉",
          extendText: "展開",
          collapseText: "摺疊",
          cancelEditText: "取消"
      },

      callbacks: {
          onCloseQuery: widgetClose,
          onChangePositions: widgetTmpSave
      }


  });

}


function widgetClose(link,widget) {

    //alert(widget);

    if (widget != null) {
        $(widget).remove();
    }
    widgetTmpSave();

}

function widgetTmpSave() {
    widgetSaveSession(getWidgetObj());

}



//物件
function WidgetBlock(widgetId,order) { 
  
  
   
    this.WidgetID=widgetId;
    this.Order==order;
}

function WidgetPlace(name){
    
    this.Block=new Array();
   
   
    this.Name=name;
}

function WidgetObj() {
    this.Place = new Array();
}

function getWidgetObj(){

    var widgetObj=new WidgetObj();
    //取所有的PLACE


    $(".widget-place").each(function() {
        //讀取裡面的所有WIDGET物件

        //如果DISPLAY 為NONE就不管它了

        //if (!$(this).css("display")=="none") {

            var str = this.id;
            var place = new WidgetPlace(str.substring(str.lastIndexOf('_') + 1));


            var widgets = Array();
            var order = 0;
            $(this).children(".widget").each(function() {
                order = order+1;

                //ID 處理
                var str = this.id;
                widgets.push(new WidgetBlock(str.substring(str.lastIndexOf('-') + 1), order));

            });

            place.Block = widgets;

            widgetObj.Place.push(place);
        //}

    });
    
    return widgetObj;

}


function saveLayout(){
alert("SAVE");

var wobj = getWidgetObj();



widgetSave(wobj);
}

function WidgetSaveOnSuccess() {
    alert("存檔成功");
}

function WidgetSaveOnFail() {
     alert("操作失敗");
    //alert(error.get_message());
}

var widgetRemoteUrl = "/NXEIP/widget/WidgetMethod.aspx";
var widgetSetting = {"SessionName": "WidgetObj", "SessionTmpName": "TmpWidgetObj", "Uid": "1", "PageType": "P" };




//Widget 頁面的存檔功能 會呼叫遠端RemoteURL的程式更新
function widgetSave(widgetObj) {
    $.ajax({
        type: "POST",
        url: widgetRemoteUrl+"/SaveLayout",
        data: "{\"widgetObj\":" + JSON.stringify(widgetObj) + ",\"widgetPage\":" + JSON.stringify(widgetSetting) + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: WidgetSaveOnSuccess,
        error: WidgetSaveOnFail
    });
   // alert("AJAX");

}

function widgetSaveSession(widgetObj) {
    $.ajax({
        type: "POST",
        url: widgetRemoteUrl + "/TmpSaveLayoutToSession",
        data: "{\"widgetObj\":" + JSON.stringify(widgetObj) + ",\"widgetPage\":" + JSON.stringify(widgetSetting) + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: null,
        error: WidgetSaveOnFail
    });
    //alert("AJAX");
}


