<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="200507-1.aspx.cs" Inherits="_20_200500_200507_1" %>
<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>提供網站</title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200507" />
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
                    <th>分類名稱</th>
                    <td><asp:DropDownList ID="ddl_sys06" runat="server" CssClass="select4">
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>網站名稱</th>
                    <td><asp:TextBox ID="txt_name" runat="server" Columns="30" MaxLength="30"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>連結網址</th>
                    <td>
                        <asp:TextBox ID="txt_www" runat="server" Columns="30" MaxLength="200"></asp:TextBox>
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
            <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="確定" OnClick="btn_submit_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()" UseSubmitBehavior="false" />
        </div>
        <div id="div_msg" runat="server">
        </div><asp:Label ID="lab_no" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_mode" runat="server" Visible="False"></asp:Label>
    </div>
</form>
</body>
</html>

