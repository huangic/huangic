<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100103-2.aspx.cs" Inherits="_10_100100_100103_2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>

<%@ Register src="../../lib/FileUpload.ascx" tagname="FileUpload" tagprefix="uc3" %>
<%@ Register src="../../lib/SWFUpload/UC_SWFUpload.ascx" tagname="UC_SWFUpload" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
   
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="100103" SubFunc="新增相簿" />
    
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
               <tr>
                     <th>
                        相簿名稱
                    </th>
                    <td>
                        <asp:TextBox ID="tb_name" runat="server"></asp:TextBox>
                       
                    </td>
                </tr>
                
                <tr>    
                    <th>
                        相簿說明
                    </th>
                    <td>
                        <asp:TextBox ID="tb_desc" runat="server"></asp:TextBox>
                    </td>
                   

                </tr>
                <tr>
                    <th>
                        相簿開放
                    </th>
                    <td >
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" BorderStyle="None" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1" Selected="True">個人</asp:ListItem>
                            <asp:ListItem Value="2">單位</asp:ListItem>
                             <asp:ListItem Value="3">全府</asp:ListItem>
                        </asp:RadioButtonList>
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
