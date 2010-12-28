<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="300202-c.aspx.cs" Inherits="_30_300200_300202_c" %>
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
                        問卷
                    </th>
                </tr>
                <tr>
                    <th style="width: 130px">
                        問卷主題
                    </th>
                    <td>
                        <asp:Label ID="lab_name" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        填寫者
                    </th>
                    <td>
                        <asp:Label ID="lab_people" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        填寫時間
                    </th>
                    <td>
                        <asp:Label ID="lab_date" runat="server"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="inquire" style="width:90%">
            <div class="boxA" style="width:100%">
                <div class="box" style="width:100%">
                    <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" RepeatLayout="Flow" OnItemDataBound="DataList1_ItemDataBound">
                        <ItemTemplate>
                            <div class="content" style="width:100%">
                                <div class="b2" style="width:100%">
                                    <li class="ps1">
                                        <asp:Label ID="lab_thename" runat="server" Text='<%# Eval("the_name") %>' CssClass="a-letter-t1" />
                                        <asp:Label ID="lab_type" runat="server" Text='<%# Eval("the_type") %>' Visible="False" />
                                        <asp:Label ID="lab_queno" runat="server" Visible="False" Text='<%# Eval("que_no") %>'></asp:Label>
                                        <asp:Label ID="lab_theno" runat="server" Visible="False" Text='<%# Eval("the_no") %>'></asp:Label>
                                        <asp:Label ID="lab_casanswer" runat="server" Text='<%# Eval("cas_answer") %>' Visible="False"></asp:Label>
                                    </li>
                                </div>
                                <asp:Label ID="lab_answer" runat="server"></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>"
            
            SelectCommand="SELECT casework.bot_no, casework.cas_no, casework.que_no, casework.the_no, theme.the_name, theme.the_type, casework.cas_answer FROM theme INNER JOIN casework ON theme.que_no = casework.que_no AND theme.the_no = casework.the_no WHERE (casework.bot_no = @bot_no) ORDER BY theme.the_order">
            <SelectParameters>
                <asp:QueryStringParameter Name="bot_no" QueryStringField="bot_no" />
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
        <div class="bottom">
          <asp:Button ID="btn_cancel" runat="server" CssClass="b-input" Text="關閉視窗" OnClientClick="self.parent.tb_remove()" UseSubmitBehavior="false" />
        </div>
    </div>
    <asp:Label ID="lab_botno" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>

