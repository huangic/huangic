<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100105-2.aspx.cs" Inherits="_10_100100_100105_2" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/css/eip.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../css/jquery-ui-1.8.2.custom.css" /> 
    <link rel="stylesheet" type="text/css" media="screen" href="../../css/ui.jqgrid.css" /> 
    <link rel="stylesheet" type="text/css" media="screen" href="../../css/ui.accordion.css" />
     <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
     <script type="text/javascript" src="../../js/thickbox.js"></script>
     <script type="text/javascript" src="../../js/jquery-ui-1.8.2.custom.min.js"></script>
     <script type="text/javascript" src="../../js/jquery.cookie.js"></script>
     <script type="text/javascript" src="../../js/jquery.hotkeys.js"></script>
     <script type="text/javascript" src="../../js/jquery.jstree.js"></script>
     <script type="text/javascript" src="../../js/grid.locale-tw.js"></script>
     <script type="text/javascript" src="../../js/jquery.jqGrid.min.js"></script>
     <script type="text/javascript" src="../../js/jquery.FileExplorer-move.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="addForm">
        <table  width="100%" cellspacing="0" cellpadding="0" border="0">
            <tbody>
                <tr>
                    <td class="leftheaderbg" />
                    <td class="a02-15 headerbg">
                        檔案搬移
                    </td>
                   
                    <td class="rightheaderbg" />
                </tr>
            </tbody>
        </table>
        <div>
        
        <div class="folderTree">
        <div id="accordion" >
	    <h3 ><a href="#" >使用者資料夾</a></h3>
	    <div id="userFolder"></div>
	    <h3><a href="#">公用資料夾</a></h3>
	    <div id="publicFolder"></div>
	    
        </div>
    </div>
        
        
        
        
        </div>
              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                
                   <tr>
                    <td class="leftfootbg">
                       
                    </td>
                    <td class="footbg">
                        &nbsp;
                    </td>
                    <td class="rightfootbg">
                        
                    </td>
                </tr>
               
            </table>

            <div align="center">
                
            
                <asp:Button ID="Button1" runat="server" CssClass="b-input" 
                    Text="確定" />
                <asp:Button ID="Button2" runat="server" CssClass="a-input" 
                     Text="取消"  OnClientClick="self.parent.update()"/>
                
            
             </div>
    </div>
    </form>
</body>
</html>
