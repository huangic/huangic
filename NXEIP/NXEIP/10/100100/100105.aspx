<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100105.aspx.cs" Inherits="_10_100100_100105" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

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
              
              $("#userFolder").fileManager(
              {   treeDiv: "#userFolder",
                  fileDiv: "#filelist",
                  publicDiv:"#publicFolder",
                  fileDeleteButton: "#delFile"
              });

             

              
          });
    
    </script>
     

      


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<uc1:Navigator ID="Navigator1" runat="server"/>


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
                <input id="delFile" type="button" value="刪除" class="b-input" />
    
                <input id="Button3" type="button"  value="權限" class="thickbox b-input" />
                <input id="Button4" type="button"  value="公開" class="thickbox b-input" />
            </div>
            <table id="filelist">
            </table>
    </div>
</div>




    




</asp:Content>

