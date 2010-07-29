function showFile(id) { 

$("#filelist").jqGrid({
           url:"Files.ashx?id="+id,
           datatype:"json",
           width:740,
           height: 380,
           colNames: ['名稱', '修改日期','檔案大小', '類型'],
           colModel:[ {name:'name',index:'name', width:400,align:"right"}, 
                      {name:'date',index:'date', width:100,align:"right"},
                      {name:'size',index:'size', width:100,align:"right"}, 
                      {name:'type',index:'type', width:100, align:"right"}
                      ],
                 
           caption:"檔案清單",
           
           
           });

}