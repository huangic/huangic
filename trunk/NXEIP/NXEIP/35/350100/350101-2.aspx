<%@ Page Language="C#" AutoEventWireup="true" CodeFile="350101-2.aspx.cs" Inherits="_35_350100_350101_2" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>"
        
        SelectCommand="SELECT people.peo_name, departments.dep_name, types.typ_cname FROM roleaccount INNER JOIN accounts ON roleaccount.acc_no = accounts.acc_no INNER JOIN people ON accounts.peo_uid = people.peo_uid INNER JOIN departments ON people.dep_no = departments.dep_no INNER JOIN types ON people.peo_pfofess = types.typ_no WHERE (people.peo_jobtype = @peo_jobtype) AND (roleaccount.rol_no = @rol_no)">
        <SelectParameters>
            <asp:Parameter Name="rol_no" />
            <asp:Parameter Name="peo_jobtype" />
        </SelectParameters>
    </asp:SqlDataSource>
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="350101" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
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
                <div class="footer">
                    <div class="f1">
                    </div>
                    <div class="f2">
                    </div>
                    <div class="f3">
                    </div>
                </div>
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
        <div class="bottom">
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="關閉視窗" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="False" />
        </div>
    </div>
    </form>
</body>
</html>
