<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100105-2.aspx.cs" Inherits="_10_100100_100105_2" %>



<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
     <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
     <script type="text/javascript" src="../../js/thickbox.js"></script>
     <script type="text/javascript" src="../../js/jquery-ui-1.8.2.custom.min.js"></script>
     <script type="text/javascript" src="../../js/jquery.cookie.js"></script>
     <script type="text/javascript" src="../../js/jquery.hotkeys.js"></script>
     <script type="text/javascript" src="../../js/jquery.jstree.js"></script>
     <script type="text/javascript" src="../../js/jquery.FileExplorer-handle.js"></script>

     <script type="text/javascript">
       
     </script>

</head>
<body>
    <form id="form1" runat="server">
  
 <div class="tableDiv" >
 <div class="header">
   <div class="h1"></div>
   <div class="h2"><div class="name">檔案搬移</div></div>
   <div class="h3"></div>
 </div>
 <div id="accordion2">
  <h3 ><a href="#" >使用者資料夾</a></h3>
 <div id="handleTree">
 
 
 </div>
 </div>

 

 <div class="footer">
   <div class="f1"></div>
   <div class="f2"></div>
   <div class="f3"></div>
 </div>

 <div style="text-align:center">
    <input id="handleOK" type="button"  title="確定"  value="確定" class=" b-input" />
    <input id="CancelButton" type="button"  title="取消"  value="取消"  onclick="self.parent.update()" class="b-input" />
 
 </div>

 </div>


    
    </form>
</body>
</html>
