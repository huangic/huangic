<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200601-1.aspx.cs" Inherits="_20_200600_200601_1" %>

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
              <td class="bg1_title" colspan="2">申請討論區</td>
              </tr>
            <tr>
              <td class="bg1">版主姓名</td>
              <td class="bg2">
                  <asp:Label ID="lab_manager" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
              <td class="bg1"><label for="ctl00_ContentPlaceHolder1_txtNN" id="ctl00_ContentPlaceHolder1_lblNN">討論區中文名稱</label></td>
              <td class="bg2">
             
              <asp:TextBox ID="tb_name" MaxLengt="30" runat="server"></asp:TextBox>
              
              </td>
            </tr>
            <tr>
              <td class="bg1">討論區英文名稱</td>
              <td class="bg2">
              
             <asp:TextBox ID="tb_ename" MaxLengt="30" runat="server"></asp:TextBox>
              
              </td>
            </tr>
            <tr>
              <td class="bg1">討論區簡介
                
                <p>(字數請勿過50字)</p></td>
              <td class="bg2">
                  <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" Rows="5" 
                      TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
              <td class="bg1">討論區型別</td>
              <td class="bg2">

                    
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                        RepeatLayout="Flow">
                        <asp:ListItem Value="1" Selected="True">開放型:開放所有使用者進入觀看，且均可以留言參與討論</asp:ListItem>
                        <asp:ListItem Value="2">半開放型：開放所有使用者進入觀看主題與內容，但必須申請成為會員才能留言</asp:ListItem>
                        <asp:ListItem Value="3">半封閉型：開放所有使用者進入觀看標題，但必須申請成為會員才能留言</asp:ListItem>
                        <asp:ListItem Value="4">封閉型：由版主挑選所有會員，僅會員才能留言討論，非會員者無法到此討論區</asp:ListItem>
                     

                    </asp:RadioButtonList>

</td>
            </tr>
            <tr>
              <td class="bg1">相關設定</td>
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
