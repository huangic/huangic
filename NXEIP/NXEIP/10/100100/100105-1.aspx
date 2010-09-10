<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100105-1.aspx.cs" Inherits="_10_100100_100105_1" %>

<%@ Register src="../../lib/SWFUpload/UC_SWFUpload.ascx" tagname="UC_SWFUpload" tagprefix="uc1" %>

<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc2:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="addForm">
        <table  width="100%" cellspacing="0" cellpadding="0" border="0">
            <tbody>
                <tr>
                    
                    <td class="leftheaderbg" />
                    <td class="a02-15 headerbg">
                        檔案上傳
                    </td>
                   
                    <td class="rightheaderbg" />
                </tr>
            </tbody>
        </table>
        <div align="center">
        目前目錄:<asp:Literal ID="path" runat="server"></asp:Literal>
        <uc1:UC_SWFUpload ID="UC_SWFUpload1" runat="server" />
        
        
        
        
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
                    onclick="Button1_Click" Text="確定" />
                <asp:Button ID="Button2" runat="server" CssClass="a-input" 
                    onclick="Button2_Click" Text="取消" />
                
            
             </div>
    </div>
    </form>
</body>
</html>
