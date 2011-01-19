<%@ Page Language="C#" AutoEventWireup="true" CodeFile="300604-1.aspx.cs" Inherits="_30_300600_300604_1" %>

<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>
<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidd_r04no" runat="server" />
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="300604" />
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
                <th style="width: 15%">
                    廠商名稱
                </th>
                <td>
                    <asp:TextBox ID="tbox_name" runat="server" MaxLength="40"></asp:TextBox>
                </td>
                <th style="width: 15%">
                    連絡人
                </th>
                <td>
                    <asp:TextBox ID="tbox_cont" runat="server" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    連絡電話
                </th>
                <td>
                    <asp:TextBox ID="tbox_tel" runat="server" MaxLength="20"></asp:TextBox>
                </td>
                <th style="width: 15%">
                    傳真號碼
                </th>
                <td>
                    <asp:TextBox ID="tbox_fax" runat="server" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    電子郵件
                </th>
                <td colspan="3">
                    <asp:TextBox ID="tbox_mail" runat="server" MaxLength="100" Width="250px"></asp:TextBox>
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
            <asp:Button ID="Button1" runat="server" Text="送出" CssClass="b-input" 
                onclick="Button1_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="關閉" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" />
        </div>
    </div>
    </form>
</body>
</html>
