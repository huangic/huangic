<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100601-3.aspx.cs" Inherits="_10_100600_100601_3" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>
<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc2" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<uc2:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Get_Conferen" TypeName="NXEIP.DAO._100601DAO">
        <SelectParameters>
            <asp:Parameter Name="mee_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100601" SubFunc="上傳會議紀錄" />
    <asp:HiddenField ID="hidd_meeno" runat="server" />
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
            <tbody>
                <tr>
                    <th style="width: 15%">
                        開會事由
                    </th>
                    <td>
                        <asp:Label ID="lab_reason" runat="server"></asp:Label>
                    </td>
                    <th style="width: 15%">
                        開會地點
                    </th>
                    <td>
                        <asp:Label ID="lab_place" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th style="width: 15%">
                        開會日期
                    </th>
                    <td>
                        <asp:Label ID="lab_date" runat="server"></asp:Label>
                    </td>
                    <th style="width: 15%">
                        會議主持人
                    </th>
                    <td>
                        <asp:Label ID="lab_host" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th style="width: 15%">
                        會議紀錄列表
                    </th>
                    <td colspan="3">
                        <cc1:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource1" EmptyDataText="查無資料"
                            GridLines="None" OnRowCommand="GridView1_RowCommand" 
                            DataKeyNames="mee_no,con_no">
                            <Columns>
                                <asp:BoundField DataField="con_file" HeaderText="會議紀錄名稱" 
                                    SortExpression="con_file">
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="刪除">
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass=" imageButton delete" CommandName="del"
                                            CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('確定要刪除?')"><span>刪除</span></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </cc1:GridView>
                    </td>
                </tr>
                <tr>
                    <th style="width: 15%">
                        上傳會議紀錄
                    </th>
                    <td colspan="3">
                        <ul>
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" /></ul>
                            <ul>
                            <asp:FileUpload ID="FileUpload2" runat="server" Width="300px" /></ul>
                            <ul>
                            <asp:FileUpload ID="FileUpload3" runat="server" Width="300px" /></ul>

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
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" OnClick="btn_ok_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" CssClass="a-input" Text="取消" onclick="Button1_Click" />
        </div>
    </div>
    </form>
</body>
</html>
