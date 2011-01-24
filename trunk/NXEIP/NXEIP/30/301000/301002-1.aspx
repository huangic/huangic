<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="301002-1.aspx.cs" Inherits="_30_301000_301002_1" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>設備審核</title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="301002" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">
                    設備審核</div>
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th>
                        所在地
                    </th>
                    <td>
                        <asp:Label ID="lab_spot" runat="server"></asp:Label>
                    </td>
                    <th>
                        設備名稱
                    </th>
                    <td>
                        <asp:Label ID="lab_equ" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        借用單位
                    </th>
                    <td>
                        <asp:Label ID="lab_depart" runat="server"></asp:Label>
                    </td>
                    <th>
                        申請人
                    </th>
                    <td>
                        <asp:Label ID="lab_applyuser" runat="server"></asp:Label>
                        <asp:Label ID="lab_applyuid" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        借用時間
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lab_sdate" runat="server"></asp:Label>&nbsp;~
                        <asp:Label ID="lab_edate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        審核狀態
                    </th>
                    <td colspan="3">
                        <asp:RadioButtonList ID="rbl_apply" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Selected="True" Value="2">核可</asp:ListItem>
                            <asp:ListItem Value="3">不核可</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th>
                        不核可原因
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txt_signmemo" runat="server" Columns="60" Rows="3" TextMode="MultiLine"></asp:TextBox>
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
            <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="確定" OnClick="btn_submit_Click" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()" UseSubmitBehavior="false" />
        </div>
        <asp:Label ID="lab_no" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_mode" runat="server" Visible="False"></asp:Label>
    </div>
    </form>
</body>
</html>

