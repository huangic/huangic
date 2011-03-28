<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="300504-1.aspx.cs" Inherits="_30_300500_300504_1" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc4" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc3" %>
<%@ Register src="../../lib/tree/DepartTreeTextBox.ascx" tagname="DepartTreeTextBox" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>電腦管理</title>
    <uc4:CssLayout ID="CssLayout1" runat="server" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/thickbox.js"></script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                //reapply the thick box stuff
                tb_init('a.thickbox');
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300504" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2"><div class="name">電腦管理</div></div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th>電腦名稱</th>
                    <td colspan="3">
                        <asp:TextBox ID="txt_name" runat="server" Columns="40" MaxLength="40"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>移送記錄</th>
                    <td colspan="3">
                        <asp:TextBox ID="txt_memo" runat="server" Columns="60" Rows="3" 
                            TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>移出人</th>
                    <td>
                        <uc2:DepartTreeTextBox ID="DepartTreeTextBox1" runat="server" LeafType="People" 
                            PeopleType="All" />
                    </td>
                    <th>移出日期</th>
                    <td>
                        <uc3:calendar ID="cl_outdate" runat="server" _Show="True" />
                        </td>
                </tr>
                <tr>
                    <th>移入人</th>
                    <td>
                        <uc2:DepartTreeTextBox ID="DepartTreeTextBox2" runat="server" LeafType="People" 
                            PeopleType="All" />
                    </td>
                    <th>移入日期</th>
                    <td>
                        <uc3:calendar ID="cl_indate" runat="server" _Show="True" /></td>
                </tr>
                
                <tr>
                    <th>異動狀況</th>
                    <td>
                        <asp:RadioButtonList ID="rbl_change" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem Value="1">調撥</asp:ListItem>
                            <asp:ListItem Value="2">借用</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <th>是否歸還</th>
                    <td>
                        <asp:RadioButtonList ID="rbl_retun" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem Value="1">是</asp:ListItem>
                            <asp:ListItem Value="2">否</asp:ListItem>
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
            <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="確定" OnClick="btn_submit_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" />
        </div>
        <asp:Label ID="lab_no" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_mode" runat="server" Visible="False"></asp:Label>
    </div>
    </form>
</body>
</html>

