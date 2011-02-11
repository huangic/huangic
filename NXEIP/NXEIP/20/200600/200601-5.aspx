<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200601-5.aspx.cs" Inherits="_20_200600_200601_5" %>

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
              <td class="bg1_title" colspan="2">回應</td>
              </tr>
            <tr>
              <td class="bg1">回應者</td>
              <td class="bg2">
                  <asp:Label ID="lab_manager" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr>
              <td class="bg1">內容
                
               </td>
              <td class="bg2">
                  <asp:TextBox ID="TextBox1" runat="server" MaxLength="100" Rows="5" 
                      TextMode="MultiLine" Width="370px"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
              <td class="bg1">上傳附件</td>
              <td class="bg2">
                  
                  <uc1:UC_SWFUpload ID="UC_SWFUpload1" runat="server" />
                  
                </td>
            </tr>
            
            <tr>
              <td colspan="2">
                 <div class="bg_">
                 <div class="b0">
                 <ul>
                 <li class="b7">
                    
                     <asp:Button ID="Button2" runat="server" CssClass="b-input" 
                    onclick="Button2_Click" Text="取消" />
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
