﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100105.aspx.cs" Inherits="_10_100100_100105" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     
     <script type="text/javascript" src="../../js/jquery-ui-1.8.2.custom.min.js"></script>
     <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
     <script type="text/javascript" src="../../js/jquery.cookie.js"></script>
     <script type="text/javascript" src="../../js/jquery.hotkeys.js"></script>
     <script type="text/javascript" src="../../js/jquery.jstree.js"></script>
     <script type="text/javascript" src="../../js/grid.locale-tw.js"></script>
     <script type="text/javascript" src="../../js/jquery.jqGrid.min.js"></script>
     <script type="text/javascript" src="../../js/jquery.FileExplorer.js"></script>
     <script type="text/javascript" src="../../js/jquery.FileExplorerPublic.js"></script>
     <script type="text/javascript" src="../../js/jquery.FileExplorerSearch.js"></script>

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

              $("#userFolder").fileManager();

              $("#publicFolder").publicfileManager();

              $("#searchButton").searchfileManager();
            
               
          });
    
    </script>
     

      


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100105"/>


<div>
    <div class="folderTree">
        <div id="accordion">
	        <h3 ><a href="#" >使用者資料夾</a></h3>
	            <div id="userFolder"></div>
	         <h3><a href="#">公用資料夾</a></h3>
	            <div id="publicFolder"></div>
	            <h3><a href="#">搜尋</a></h3>
	        <div id="shereFolder">
	            檔名<input id="searchName" type="text" /><br/>
                <!--
                關鍵字<input id="searchContext" type="text" />	<br/>
                -->
	             <input id="searchButton" type="button" title="搜尋" value="搜尋" />   
	    
	       </div>
        </div>
    </div>
    
    <div class="fileTable">
            <div id="toolbar" style="text-align:right">
                <input id="addFile" type="button" alt="100105-1.aspx?modal=true&TB_iframe=true" title="上傳" value="上傳" class="thickbox b-input" />
                <input id="delFile" type="button" title="刪除" value="刪除" class="b-input" />
                <input id="moveFile" type="button" title="搬移" value="搬移" class="b-input" />
                <input id="copyFile" type="button" title="複製" value="複製" class="b-input" />
    
                <input id="permissionButton" type="button" alt="100105-2.aspx?modal=true&TB_iframe=true&width=600" title="權限"  value="權限" class="b-input" />
                <!--
                <input id="publicFile" type="button"  title="公開"  value="公開" class="thickbox b-input" />
                -->
            </div>
            <table id="filelist">
            </table>
    </div>
   

</div>



<div id="dialog" class="dialog">
 <div id="handleTree"></div>
 </div>


 <div id="shareDialog" class="dialog">
 分享網址:<input id="uri"  class="a01-13"  style=" width:400px" type="text" readonly /><br /><br />
 設定密碼:<input id="pwd" type="password" /><br />
 確認密碼:<input id="pwd2" type="password" /><br />
 </div>



    




    




    




</asp:Content>

