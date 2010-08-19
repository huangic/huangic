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
     <script type="text/javascript" src="../../js/jquery.FileExplorer.js"></script>

      <script type="text/javascript">
          function update() {

              tb_remove();

              $('#filelist').trigger("reloadGrid");
              
          }

          function pageLoad(sender, args) {
              if (args.get_isPartialLoad()) {
                  //  reapply the thick box stuff
                  tb_init('a.thickbox input.thickbox');
              }
          }

          $(function () {
              $("#accordion").accordion({
                  fillSpace: true
              });
              $("#folderTree").fileManager(
              { treeDiv: "#userFolder",
                  fileDiv: "#filelist"
              }
              );
          });
    
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
	    <h3><a href="#">搜尋</a></h3>
	    <div id="shereFolder">
	     檔名<input type="text" />	
	          
	    
	    </div>
        </div>
    </div>
    
    <div class="fileTable">
            <div id="toolbar" style="text-align:right">
                <input id="addFile" type="button" alt="100105-1.aspx?modal=true&TB_iframe=true" value="上傳" class="thickbox b-input" />
                <input id="moveFile" type="button" alt="100105-2.aspx?modal=true&width=300&TB_iframe=true" value="搬移" class="thickbox b-input" />
                <input id="Button2" type="button"  value="複製" class="thickbox b-input" />
                <input id="Button3" type="button"  value="權限" class="thickbox b-input" />
                <input id="Button4" type="button"  value="公開" class="thickbox b-input" />
            </div>
            <table id="filelist">
            </table>
    </div>
</div>




</asp:Content>

