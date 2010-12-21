<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="300201-3.aspx.cs" Inherits="_30_300200_300201_3" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>問卷題目維護</title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300201" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2"><div class="name">問卷題目維護</div></div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th>問卷名稱</th>
                    <td colspan="5">
                        <asp:Label ID="lab_name" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>問卷說明</th>
                    <td colspan="5">
                        <asp:Label ID="lab_descript" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>調查時間</th>
                    <td colspan="5">
                        <asp:Label ID="lab_sdate" runat="server"></asp:Label> ～ <asp:Label ID="lab_edate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>題目名稱</th>
                    <td colspan="5">
                        <asp:TextBox ID="txt_name" runat="server" Columns="70" Rows="3" 
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>題目種類</th>
                    <td>
                        <asp:RadioButtonList ID="rbl_type" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1">單選</asp:ListItem>
                            <asp:ListItem Value="2">複選</asp:ListItem>
                            <asp:ListItem Value="3">填充</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <th>題目順序</th>
                    <td>
                        <asp:TextBox ID="txt_order" runat="server" Columns="5" Rows="2"></asp:TextBox>
                    </td>
                    <th>是否計分</th>
                    <td>
                        <asp:RadioButtonList ID="rbl_count" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem Value="2">不計分</asp:ListItem>
                            <asp:ListItem Value="1">計分</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:TextBox ID="txt_fraction" runat="server" Columns="5" Rows="2"></asp:TextBox>
                        分</td>
                </tr>
                <tr>
                    <th>答案選項</th>
                    <td colspan="5">
                        項目：
                        <asp:TextBox ID="txt_ansname" runat="server" Columns="40"></asp:TextBox>
                        順序：<asp:TextBox ID="txt_ansorder" runat="server" Columns="5"></asp:TextBox>
                        分數<asp:TextBox ID="txt_ansfraction" runat="server" Columns="5"></asp:TextBox>
                        <asp:Button ID="btn_additem" runat="server" CssClass="b-input" Text="加入" 
                            OnClick="btn_additem_Click" />
                        <br />
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                            DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" 
                            SelectMethod="GetData" TypeName="DS_30TableAdapters.answersTableAdapter">
                            <DeleteParameters>
                                <asp:Parameter Name="Original_que_no" Type="Int32" />
                                <asp:Parameter Name="Original_the_no" Type="Int32" />
                                <asp:Parameter Name="Original_ans_no" Type="Int32" />
                            </DeleteParameters>
                            <SelectParameters>
                                <asp:Parameter Name="que_no" Type="Int32" />
                                <asp:Parameter Name="the_no" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            CellSpacing="3" DataKeyNames="que_no,the_no,ans_no" 
                            ShowHeaderWhenEmpty="True" EmptyDataText="查無資料" 
                            onrowcommand="GridView1_RowCommand" CssClass="tableData" BorderWidth="0px" 
                            CellPadding="3">
                            <Columns>
                                <asp:BoundField DataField="ans_name" HeaderText="項目" SortExpression="ans_name" />
                                <asp:BoundField DataField="ans_order" HeaderText="順序" SortExpression="ans_order" />
                                <asp:BoundField DataField="ans_fraction" HeaderText="分數" SortExpression="ans_fraction" />
                                <asp:TemplateField HeaderText="刪除">
                                    <ItemTemplate>
                                        <asp:Button ID="btn_delete" runat="server" CommandName="delitem" CommandArgument="<%# Container.DataItemIndex %>" CssClass="delete" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="40" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                        </asp:GridView>
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
        <asp:Label ID="lab_queno" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_theno" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_mode" runat="server" Visible="False"></asp:Label>
    </div>
    </form>
</body>
</html>
