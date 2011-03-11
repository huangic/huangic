<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100101-1.aspx.cs" Inherits="_10_100100_100101_1" %>
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
   
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="100101" SubFunc="新增IP" />
    
   <div class="tableDiv">
        <asp:HiddenField ID="hidden_arg_no" runat="server" />
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            <div class="function">
                <a href="100101/ip.html" target="_blank">登錄說明</a>
            </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tbody>
               <tr>
                     <th>
                        使用單位
                    </th>
                    <td>
                        
                       
                        <asp:Label ID="lb_dep" runat="server" Text="Label"></asp:Label>
                        
                       
                    </td>
                    <th>
                        使用人員
                    </th>
                    <td>
                        
                       
                        <asp:Label ID="lb_peo" runat="server" Text="Label"></asp:Label>
                        
                       
                    </td>

                </tr>
                
                <tr>    
                    <th>
                        使用者電話
                    </th>
                    <td>
                        
                        <asp:Label ID="lb_tel" runat="server" Text="Label"></asp:Label>
                        
                    </td>
                   <th>
                        電腦名稱
                    </th>
                    <td>
                        <asp:TextBox ID="tb_pcname"  MaxLength="30" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <th>
                       IP位址
                    </th>
                    <td colspan="3" >
                      
                        <asp:TextBox ID="tb_ip1" runat="server" MaxLength="3" Width="50px"></asp:TextBox>
                        .<asp:TextBox ID="tb_ip2" runat="server" MaxLength="3" Width="50px"></asp:TextBox>
                        .<asp:TextBox ID="tb_ip3" runat="server" MaxLength="3"  Width="50px"></asp:TextBox>
                        .<asp:TextBox ID="tb_ip4" runat="server" MaxLength="3"  Width="50px"></asp:TextBox>
                        <asp:Label ID="lb_rager" runat="server" Text="~"></asp:Label>
                        <asp:TextBox ID="tb_ip5" runat="server" MaxLength="3" Width="50px"></asp:TextBox>
                      
                    </td>
                </tr>
                <tr>
                    <th>
                       工作群組
                    </th>
                    <td >
                      
                        <asp:TextBox ID="tb_group"  MaxLength="40" runat="server"></asp:TextBox>
                      
                    </td>
                     <th>
                       電腦用途
                    </th>
                    <td >
                      
                        <asp:TextBox ID="tb_memo" runat="server"  MaxLength="100"></asp:TextBox>
                      
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
