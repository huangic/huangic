<%@ Page Language="C#" AutoEventWireup="true" CodeFile="300704-1.aspx.cs" Inherits="_30_300700_300704_1" %>

<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            
            <tr>
                <th style="width:12%" class="a-letter-2">
                身分證字號
                </th>
                <td>
                </td>
                <th style="width:12%">
                姓名
                </th>
                <td>

                </td>
            </tr>
            <tr>
                <th style="width:12%">
                登入帳號
                </th>
                <td>
                
                </td>
                <th style="width:12%">
                登入密碼
                </th>
                <td>
                
                
                </td>
            </tr>
            <tr>
                <th style="width:12%">
                服務單位
                </th>
                <td>
                    
                </td>
                <th style="width:12%">
                職稱
                </th>
                <td>
                   
                </td>
            </tr>
            <tr>
                <th style="width:12%">
                   公務電話
                </th>
                <td>
                </td>
                <th style="width:12%">
                電子郵件
                </th>
                <td>
                </td>
            </tr>
            <tr>
                <th style="width:12%">
                 手機號碼
                </th>
                <td colspan="3">
                </td>
                
            </tr>
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
            
            <asp:Button ID="btn_cancel" runat="server" CssClass="b-input" Text="關閉" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" />
        </div>
    </div>
    </form>
</body>
</html>
