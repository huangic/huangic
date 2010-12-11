<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="300401-1.aspx.cs" Inherits="_30_300400_300401_1" %>
<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新增所在地</title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300401" />
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
                    <th>所在地</th>
                    <td><asp:TextBox ID="txt_name" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>開放功能</th>
                    <td>
                        <asp:CheckBoxList ID="cbl_function" runat="server" RepeatColumns="5" 
                            RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" Width="400px">
                            <asp:ListItem Value="1">美食區</asp:ListItem>
                            <asp:ListItem Value="2">樓層介紹</asp:ListItem>
                            <asp:ListItem Value="3">合作社</asp:ListItem>
                            <asp:ListItem Value="4">維修登錄</asp:ListItem>
                            <asp:ListItem Value="5">場地申請</asp:ListItem>
                        </asp:CheckBoxList>
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

