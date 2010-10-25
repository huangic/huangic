<%@ Page Language="C#" AutoEventWireup="true" CodeFile="detail.aspx.cs" Inherits="lib_people_detail" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
   
       
   <div class="tableDiv">
       
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th>
                        姓名
                    </th>
                    <td>
                                               
                        <asp:Label ID="lb_name" runat="server" Text="Label"></asp:Label>
                                               
                    </td>
                    


                </tr>

                <tr>
                 <th>
                        連絡電話
                    </th>
                    <td>
                                               
                        <asp:Label ID="lb_tel" runat="server" Text="Label"></asp:Label>
                                               
                    </td>
                
                </tr>

                <tr>
                    <th>
                        分機
                    </th>
                    <td>
                        <asp:Label ID="lb_ext" runat="server" Text="Label"></asp:Label>
                    </td>
                  

                </tr>
                <tr>
                     <th>
                        電子信箱
                    </th>
                    <td>
                        <asp:Label ID="lb_email" runat="server" Text="Label"></asp:Label>
                        
                    </td>
                    
                </tr>
                 

            </tbody>
        </table>
        <div class="footer">
            <div class="f1">
            </div>
            <div class="f2">
            </div>
            <div class="f3">
            </div>
        </div>
        
    </div>
    </form>
</body>
</html>
