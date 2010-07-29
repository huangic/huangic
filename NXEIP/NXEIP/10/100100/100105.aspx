<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100105.aspx.cs" Inherits="_10_100100_100105" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" media="screen" href="../../css/jquery-ui-1.8.2.custom.css" /> 
    <link rel="stylesheet" type="text/css" media="screen" href="../../css/ui.jqgrid.css" /> 
    <link rel="stylesheet" type="text/css" media="screen" href="../../css/ui.accordion.css" />
      <script type="text/javascript" src="../../js/jquery-ui-1.8.2.custom.min.js"></script>
     <script type="text/javascript" src="../../js/jquery.cookie.js"></script>
     <script type="text/javascript" src="../../js/jquery.hotkeys.js"></script>
     <script type="text/javascript" src="../../js/jquery.jstree.js"></script>
      <script type="text/javascript" src="../../js/grid.locale-tw.js"></script>
      <script type="text/javascript" src="../../js/jquery.jqGrid.min.js"></script>
       <script type="text/javascript" src="../../js/jquery.jqGrid-helper.js"></script>
      
      
      

       <script type="text/javascript">
           $(function() {


               $("#accordion").accordion({
                   fillSpace: true
               });





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
                   lang: {
                       loading: "目录加载中……"
                   },
                   rules:
                    {
                        draggable: "all"
                    },
                   "themes": {
                       "theme": "classic"
                   },
                   "plugins": ["themes", "json_data"],

                   callback: {
                       ondblclk: function(node) {
                           alert("TEST");
                           showFile(node.id);
                       }
                   }
               })
           })
       
       </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div>
    <div class="folderTree">
        <div id="accordion">
	    <h3 ><a href="#" >使用者資料夾</a></h3>
	    <div id="userFolder"></div>
	    <h3><a href="#">公用資料夾</a></h3>
	    <div id="publicFolder"></div>
	    <h3><a href="#">共享資料夾</a></h3>
	    <div id="shereFolder"></div>
        </div>
    </div>
    
    <div class="fileTable">
            <table id="filelist" >
            </table>
    </div>
</div>




</asp:Content>

