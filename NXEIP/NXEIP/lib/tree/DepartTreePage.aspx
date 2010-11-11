<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepartTreePage.aspx.cs" Inherits="lib_tree_DepartTreePage" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
    

    <%@ Register src="../CssLayout.ascx" tagname="CssLayout" tagprefix="uc2" %><%@ Register src="DepartmentPanel.ascx" tagname="DepartmentPanel" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
 <uc2:CssLayout ID="CssLayout1" runat="server" />
    
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.cookie.js"></script>

    
    
    
    
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="tableDiv">
  
  
             <div class="header">
                    <div class="h1">
                    </div>
                    <div class="h2">
                        <div class="name">
                            <asp:Label ID="lb_leafType" runat="server" ></asp:Label></div>
                    </div>
                    <div class="h3">
                    </div>
                </div>
                <uc1:DepartmentPanel ID="DepartmentPanel1" runat="server" />
         
            
             <div class="footer">
                    <div class="f1">
                    </div>
                    <div class="f2">
                    </div>
                    <div class="f3">
                    </div>
                </div>

            <div align="center">
                                                     
                           <button type="button" class="TreeSave b-input">確定</button>
                               
                            <button type="button" class="TreeDel a-input">刪除</button>
                           
                            &nbsp;&nbsp;&nbsp;&nbsp;
                          <button type="button" class="a-input" onclick="self.parent.tb_remove()">關閉</button>
                           
                            
                        </div>
           
        


    </div>
    </form>
</body>
</html>
