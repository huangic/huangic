<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="301101-1.aspx.cs" Inherits="_30_301100_301101_1" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>車輛屬性設定</title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="301101" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2"><div class="name">車輛屬性設定</div></div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th>屬性類別</th>
                    <td>
                        <asp:RadioButtonList ID="rbl_number" runat="server" AutoPostBack="True" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="platoon">排照種類</asp:ListItem>
                            <asp:ListItem Value="chekuan">車別</asp:ListItem>
                            <asp:ListItem Value="mark">車輛廠牌</asp:ListItem>
                            <asp:ListItem Value="color">車輛顏色</asp:ListItem>
                            <asp:ListItem Value="source">來源</asp:ListItem>
                            <asp:ListItem Value="factory">廠商</asp:ListItem>
                            <asp:ListItem Value="energy">能源種類</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th>屬性代碼</th>
                    <td>
                        <asp:TextBox ID="txt_code" runat="server" Columns="3" MaxLength="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>屬性名稱</th>
                    <td>
                        <asp:TextBox ID="txt_name" runat="server" Columns="30" MaxLength="30"></asp:TextBox>
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
        <asp:Label ID="lab_number" runat="server" Visible="False"></asp:Label>
    </div>
    </form>
</body>
</html>

