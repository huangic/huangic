<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="100301-5.aspx.cs" Inherits="_10_100300_100301_5" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>預約首長行程審核</title>
    <uc1:csslayout ID="CssLayout1" runat="server" />
    <script type="text/javascript" src="../../js/jscolor.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="100301" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1"></div>
            <div class="h2">
              <div class="name"><asp:Label ID="lab_UserName" runat="server" Text="管理者"></asp:Label>--預約首長行程審核</div>
            </div>
            <div class="h3"></div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th>日期時間</th>
                    <td>
                        <asp:Label ID="lab_sdate" runat="server"></asp:Label>　～ <asp:Label ID="lab_edate" runat="server"></asp:Label>
                    </td>
                    <th>
                        標題
                    </th>
                    <td>
                        <asp:Label ID="lab_title" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        地點
                    </th>
                    <td>
                        <asp:Label ID="lab_place" runat="server"></asp:Label>
                    </td>
                    <th>
                        背景顏色
                    </th>
                    <td>
                        <asp:Label ID="lab_bgcolor" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        預計計畫
                    </th>
                    <td>
                        <asp:Label ID="lab_project" runat="server"></asp:Label>
                    </td>
                    <th>
                        紀錄結果
                    </th>
                    <td>
                        <asp:Label ID="lab_result" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>審核狀態</th>
                    <td colspan="3">
                        <asp:RadioButtonList ID="rbl_check" runat="server" RepeatDirection="Horizontal" 
                            RepeatLayout="Flow">
                            <asp:ListItem Value="1">核可</asp:ListItem>
                            <asp:ListItem Value="2">退回</asp:ListItem>
                        </asp:RadioButtonList>(原因：<asp:TextBox ID="txt_reason" runat="server" 
                            Columns="50" MaxLength="50"></asp:TextBox>)
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="footer">
            <div class="f1"></div>
            <div class="f2"></div>
            <div class="f3"></div>
        </div>
        <div class="bottom">
            <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="核可" OnClick="btn_submit_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClick="btn_cancel_Click" />
        </div>
        <div id="div_msg" runat="server">
        </div>
        <asp:Label ID="lab_no" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_peo_uid" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_today" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_source" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_c03_no" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_depart" runat="server" Visible="False"></asp:Label>
    </div>
    </form>
</body>
</html>
