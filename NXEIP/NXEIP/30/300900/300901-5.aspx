<%@ Page Language="C#" AutoEventWireup="true" CodeFile="300901-5.aspx.cs" Inherits="_30_300900_300901_5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>
<%@ Register src="../../lib/tree/DepartTreeTextBox.ascx" tagname="DepartTreeTextBox" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
     <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
     <script type="text/javascript" src="../../js/thickbox.js"></script>
     <script type="text/javascript" src="../../js/jquery.jqprint-0.3.js"></script>
    <script type="text/javascript">
       

       
    
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    
    
  
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="300901"  SubFunc="檢視表單"/>
    
   
    <div class="tableDiv" style="width:80%">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                
            </div>
            <div class="h3">
            </div>
        </div>
         <div class="print">
        <table>
            <tbody>
                <tr>
                    <th>
                        表單名稱
                    </th>
                    <td>
                                               
                        <asp:Label ID="lb_name" runat="server" Text=""></asp:Label>
                                               
                    </td>
                     <th>
                        承辦人員
                    </th>
                    <td>
                                               
                        <asp:Label ID="lb_people" runat="server" Text=""></asp:Label>
                                               
                    </td>


                </tr>
                <tr>
                    <th>
                        表單說明
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lb_description" runat="server" Text=""></asp:Label>
                    </td>
                   
                    

                </tr>
               
                 <tr>
                    <th>
                        提交人
                    </th>
                    <td>
                                               
                        <asp:Label ID="lb_submit" runat="server" Text=""></asp:Label>
                                               
                    </td>
                     <th>
                        提交日期
                    </th>
                    <td>
                                               
                        <asp:Label ID="lb_submit_date" runat="server" Text=""></asp:Label>
                                               
                    </td>


                </tr>
                 


            </tbody>
        </table>
       
        <asp:Table ID="DynamicTable" runat="server" ClientIDMode="Static">
        </asp:Table>
         <asp:Table ID="DynamicFooter" runat="server" ClientIDMode="Static">
        </asp:Table>
       
       </div> 
        
                <div class="footer">
                    <div class="f1">
                        
                    </div>
                    <div class="f2">
                    </div>
                    <div class="f3">
                    </div>
                </div>
                
          <div class="bottom">
        
               <asp:Button ID="Button1" runat="server" CssClass="a-input" 
                Text="取消"  OnClientClick="self.parent.tb_remove()"/>
              <input id="Button2" type="button" class="a-input" value="列印" onclick='$(".print").jqprint();' />
              </div>
        </div>


    </div>
    
    
    </form>
</body>
</html>
