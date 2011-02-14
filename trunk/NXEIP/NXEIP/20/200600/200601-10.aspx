<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200601-10.aspx.cs" Inherits="_20_200600_200601_10" %>

<%@ Register src="../../lib/SWFUpload/UC_SWFUpload.ascx" tagname="UC_SWFUpload" tagprefix="uc1" %>

<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc2" %>

<%@ Register src="../../lib/tree/DepartmentPanel.ascx" tagname="DepartmentPanel" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc2:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
      
    
    <div class="tableDiv">
         
        <table class="back">
          <tbody>
            
              <uc3:DepartmentPanel ID="DepartmentPanel1" runat="server" LeafType="People" 
                   PeopleShowSelf="True" />
            
            <tr>
              <td>
              
                 <div class="b0" style="text-align:center;">
                          
                        <asp:Button ID="button_ok" class="b-input" runat="server" Text="確定" 
                         onclick="button_ok_Click" style="height: 21px" />    
                                   
                     <asp:Button ID="Button2" runat="server" CssClass="b-input" 
                    onclick="Button2_Click" Text="取消" />
                    
              
                    
                 </div> </td>
            </tr>
          </tbody>
          </table>
          
      
    
    </div>
    </form>
</body>
</html>
