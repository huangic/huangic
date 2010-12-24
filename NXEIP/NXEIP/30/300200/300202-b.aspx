<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="300202-b.aspx.cs" Inherits="_30_300200_300202_b" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>問卷統計</title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300202" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">
                    問卷統計</div>
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th colspan="2">
                        問卷資料
                    </th>
                </tr>
                <tr>
                    <th style="width: 130px">
                        問卷主題
                    </th>
                    <td>
                        <asp:Label ID="lab_quename" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        題目
                    </th>
                    <td>
                        <asp:Label ID="lab_thename" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        答案
                    </th>
                    <td>
                        <asp:Label ID="lab_ansname" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        投票者清單
                    </th>
                </tr>
            </tbody>
        </table>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                CssClass="tableData" EmptyDataText="查無資料" ShowHeaderWhenEmpty="True" CellPadding="3"
                CellSpacing="3" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="dep_name" HeaderText="單位" SortExpression="dep_name">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="typ_cname" HeaderText="職稱" SortExpression="typ_cname">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="peo_name" HeaderText="姓名" SortExpression="peo_name">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="bot_date" HeaderText="填寫日期" SortExpression="bot_date"
                        DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" HorizontalAlign="Center" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>"
                SelectCommand="SELECT departments.dep_name, types.typ_cname, people.peo_name, botanize.bot_date FROM casework INNER JOIN botanize ON casework.bot_no = botanize.bot_no INNER JOIN people ON botanize.peo_uid = people.peo_uid INNER JOIN departments ON people.dep_no = departments.dep_no INNER JOIN types ON people.peo_pfofess = types.typ_no WHERE (botanize.bot_status = '1') AND (1 = @model) ORDER BY departments.dep_order, types.typ_order, people.peo_name">
                <SelectParameters>
                    <asp:Parameter Name="model" />
                </SelectParameters>
            </asp:SqlDataSource>
            <div class="footer">
                <div class="f1">
                </div>
                <div class="f2">
                </div>
                <div class="f3">
                </div>
            </div>
        </div>
        <div class="bottom">
            <asp:Button ID="btn_cancel" runat="server" CssClass="b-input" Text="關閉視窗" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" />
        </div>
        <asp:Label ID="lab_queno" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_theno" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_ansno" runat="server" Visible="False"></asp:Label>
    </div>
    </form>
</body>
</html>
