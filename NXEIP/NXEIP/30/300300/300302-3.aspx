<%@ Page Language="C#" AutoEventWireup="true" CodeFile="300302-3.aspx.cs" Inherits="_30_300300_300302_3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../lib/navigator.ascx" TagName="navigator" TagPrefix="uc1" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc2:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:navigator ID="navigator1" runat="server" SysFuncNo="300302" />
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
                <th>
                    課程類別代號
                </th>
                <td>
                    <asp:TextBox ID="tbox_number" runat="server" MaxLength="3" Width="75px"></asp:TextBox>
                    <span id="span_1" class="a-letter-Red" runat="server">代碼長度最多3位數</span>
                </td>
                <th>
                    排列順序
                </th>
                <td>
                    <asp:TextBox ID="tbox_order" runat="server" Width="75px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    課程類別名稱
                </th>
                <td colspan="3">
                    <asp:TextBox ID="tbox_name" runat="server" Width="330px"></asp:TextBox>
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
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" OnClick="btn_ok_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" />
        </div>
        <div id="div_msg" runat="server">
        </div>
    </div>
    <asp:HiddenField ID="hidd_parent" runat="server" />
    <asp:HiddenField ID="hidd_typno" runat="server" />
    </form>
</body>
</html>
