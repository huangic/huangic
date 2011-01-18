<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="300201-1.aspx.cs" Inherits="_30_300200_300201_1" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>問卷維護</title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300201" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2"><div class="name">問卷維護</div></div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th>問卷名稱</th>
                    <td colspan="3">
                        <asp:TextBox ID="txt_name" runat="server" Columns="60" Rows="2" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>問卷說明</th>
                    <td colspan="3">
                        <asp:TextBox ID="txt_descript" runat="server" Columns="60" Rows="5" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>卷尾說明</th>
                    <td colspan="3">
                        <asp:TextBox ID="txt_end" runat="server" Columns="60" Rows="5" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>是否記名</th>
                    <td>
                        <asp:RadioButtonList ID="rbl_register" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem Value="1">記名</asp:ListItem>
                            <asp:ListItem Value="2">不記名</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <th>是否公布結果</th>
                    <td>
                        <asp:RadioButtonList ID="rbl_open" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1">公布</asp:ListItem>
                            <asp:ListItem Value="2">不公布</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th>調查時間</th>
                    <td colspan="3">
                        開始時間<uc3:calendar ID="cl_sdate" runat="server" _Show="False" />
                        <asp:TextBox ID="txt_shour" runat="server" Columns="2"></asp:TextBox>
                        時<asp:TextBox ID="txt_smin" runat="server" Columns="2"></asp:TextBox>
                        分<br />
                        結束時間<uc3:calendar ID="cl_edate" runat="server" _Show="False" /><asp:TextBox ID="txt_ehour" runat="server" Columns="2"></asp:TextBox>
                        時<asp:TextBox ID="txt_emin" runat="server" Columns="2"></asp:TextBox>
                        分
                    </td>
                </tr>
                <tr>
                    <th>是否上架</th>
                    <td colspan="3">
                        <asp:RadioButtonList ID="rbl_status" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem Value="1">上架</asp:ListItem>
                            <asp:ListItem Value="2">下架</asp:ListItem>
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
