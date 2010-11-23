<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100403-2.aspx.cs" Inherits="_10_100400_100403_2" %>

<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidd_r02no" runat="server" />
    <uc2:Navigator ID="Navigator1" runat="server" SubFunc="" SysFuncNo="100403" />
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
                    叫修單位
                </th>
                <td>
                    <asp:Label ID="lab_dep" runat="server"></asp:Label>
                </td>
                <th style="width: 15%">
                    叫修人員
                </th>
                <td>
                    <asp:Label ID="lab_people" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    叫修日期
                </th>
                <td>
                    <asp:Label ID="lab_date" runat="server" ></asp:Label>
                </td>
                <th style="width: 15%">
                    地點
                </th>
                <td>
                    <asp:Label ID="lab_place" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    故障原因
                </th>
                <td>
                    <asp:Label ID="lab_reason" runat="server" ></asp:Label>
                </td>
                <th style="width: 15%">
                    處理狀況
                </th>
                <td colspan="3">
                    <asp:Label ID="lab_status" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    處理人員
                </th>
                <td>
                    <asp:Label ID="lab_reply_people" runat="server" ></asp:Label>
                </td>
                <th style="width: 15%">
                    處理日期
                </th>
                <td>
                    <asp:Label ID="lab_reply_date" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    處理回覆說明
                </th>
                <td colspan="3">
                    <asp:Label ID="lab_reply" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    評分項目
                </th>
                <td colspan="3">
                    <asp:RadioButtonList ID="rbl_rep03" runat="server" 
                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1" Selected="True">非常滿意</asp:ListItem>
                        <asp:ListItem Value="2">滿意</asp:ListItem>
                        <asp:ListItem Value="3">普通</asp:ListItem>
                        <asp:ListItem Value="4">尚可</asp:ListItem>
                    </asp:RadioButtonList>
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
