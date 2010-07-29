<%@ Page Language="C#" AutoEventWireup="true" CodeFile="350101-2.aspx.cs" Inherits="_35_350100_350101_2" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/eip.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="17">
                    <img src="../../image/b02.gif" width="17" height="29" />
                </td>
                <td background="../../image/b02-1.gif" class="a02-15">
                    人員明細
                </td>
                <td background="../../image/b02-1.gif">
                    &nbsp;
                </td>
                <td width="17">
                    <img src="../../image/b02-2.gif" width="17" height="29" />
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" CssClass="tableData" DataSourceID="SqlDataSource1"
                    Width="100%" EmptyDataText="查無資料!" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="dep_name" HeaderText="單位" SortExpression="dep_name" />
                        <asp:BoundField DataField="typ_cname" HeaderText="職稱" SortExpression="typ_cname" />
                        <asp:BoundField DataField="peo_name" HeaderText="姓名" SortExpression="peo_name" />
                    </Columns>
                </cc1:GridView>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="17">
                            <img src="../../image/b02-3.gif" width="17" height="17" />
                        </td>
                        <td background="../../image/b02-4.gif">
                            &nbsp;
                        </td>
                        <td width="17">
                            <img src="../../image/b02-5.gif" width="17" height="17" />
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>"
                    SelectCommand="SELECT people.peo_name, departments.dep_name, types.typ_cname FROM roleaccount INNER JOIN accounts ON roleaccount.acc_no = accounts.acc_no INNER JOIN people ON accounts.peo_uid = people.peo_uid INNER JOIN departments ON people.dep_no = departments.dep_no INNER JOIN types ON people.peo_pfofess = types.typ_no WHERE (people.peo_jobtype = '1') AND (roleaccount.rol_no = @rol_no)">
                    <SelectParameters>
                        <asp:Parameter Name="rol_no" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <div class="pager">
                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="10">
                        <Fields>
                            <asp:NextPreviousPagerField ShowNextPageButton="False" />
                            <asp:NumericPagerField />
                            <asp:NextPreviousPagerField ShowPreviousPageButton="False" />
                        </Fields>
                    </asp:DataPager>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table width="100%" border="0" cellpadding="0" cellspacing="10" bgcolor="#FFFFFF">
            <tr>
                <td>
                    <div align="center">
                        &nbsp;<asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="關閉視窗" OnClientClick="self.parent.tb_remove()"
                            UseSubmitBehavior="False" />
                        &nbsp;</div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
