<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100103-5.aspx.cs" Inherits="_10_100100_100103_5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
   
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="100103" SubFunc="修改描述" />
    
   <div class="tableDiv">
        <asp:HiddenField ID="hidden_arg_no" runat="server" />
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
              <tr >
               <td rowspan="3" style=" width:100px">
                 
                 
                   <img alt="縮圖" src='<%=String.Format("100103-1.ashx?album={0}&photo={1}",Request["album"],Request["photo"]) %>' />
                
               </td>
              </tr>
              
              
               <tr>
                     <th>
                        相片名稱
                    </th>
                    <td>
                        <asp:TextBox ID="tb_name" runat="server"></asp:TextBox>
                       
                    </td>
                </tr>
                
                <tr>    
                    <th>
                        相片說明
                    </th>
                    <td>
                        <asp:TextBox ID="tb_desc" runat="server"></asp:TextBox>
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
        <div class="bottom">
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" 
                onclick="btn_ok_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="button" class="a-input" onclick="self.parent.tb_remove()"  value="取消" />
           
         
        </div>
    </div>
    </form>
</body>
</html>
