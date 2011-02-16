<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200601-12.aspx.cs" Inherits="_20_200600_200601_12" %>

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
      
    
    <div class="tableDiv">
       <div class="talkn">
        <table class="back">
          <tbody>
            
            <tr>
              <td class="bg1_title" colspan="2">討論區訂閱</td>
              </tr>
          
            <tr>
              <td class="bg1">訂閱設定</td>
              <td class="bg2">
                  <asp:CheckBoxList ID="cb_notify" runat="server" RepeatDirection="Horizontal" 
                      RepeatLayout="Flow">
                      <asp:ListItem Value="4">E-mail 通知</asp:ListItem>
                      <asp:ListItem Value="2">個人訊息</asp:ListItem>
                      <asp:ListItem Value="1">E公務</asp:ListItem>
                  </asp:CheckBoxList>
                </td>
            </tr>
            
            <tr>
              <td colspan="2">
                 <div class="bg_">
                 <div class="b0">
                 <ul>
                 <li class="b7">
                    <input type="button" class="b-input"   onclick="self.parent.tb_remove()" value="取消"/>
                    </li>
                 <li class="b7"><asp:Button ID="button_ok" class="b-input" runat="server" Text="確定" 
                         onclick="button_ok_Click" />
                    </li>
                    </ul>
                 </div></div>   </td>
            </tr>
          </tbody>
          </table>
          </div>
      
    
    </div>
    </form>
</body>
</html>
