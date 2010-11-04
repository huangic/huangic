<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="100302-1.aspx.cs" Inherits="_10_100300_100302_1" %>
<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>
<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新增開放人員</title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="100302" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1"></div>
            <div class="h2"><div class="name">新增開放人員</div></div>
            <div class="h3"></div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th>請選擇</th>
                    <td>
                        <asp:DropDownList ID="ddl_depart" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddl_depart_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddl_people" runat="server">
                        </asp:DropDownList>
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
            &nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()" UseSubmitBehavior="false" />
            &nbsp;&nbsp;
            <asp:Button ID="btn_close" runat="server" CssClass="a-input" Text="閉關視窗" OnClientClick="self.parent.tb_remove()" UseSubmitBehavior="false" />
        </div>
        <div id="div_msg" runat="server">
        </div>
    </div>
    </form>
</body>
</html>
