<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200402-3.aspx.cs" Inherits="_20_200400_200402_3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc2:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hidd_no" runat="server" />
    <asp:ObjectDataSource ID="ODS_1" runat="server" SelectMethod="Get_e04Data_2" TypeName="NXEIP.DAO._300303DAO"
        EnablePaging="True" SelectCountMethod="Get_e04DataCount_2" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="e02_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200402" SubFunc="報名明細" />

    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <table id="table1">
            <tr>
                <th>
                    學習機構
                </th>
                <td>
                    <asp:Label ID="lab_mechani" runat="server"></asp:Label>
                </td>
                <th>
                    課程代碼
                </th>
                <td>
                    <asp:Label ID="lab_code" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    課程類別
                </th>
                <td>
                    <asp:Label ID="lab_typ_name" runat="server"></asp:Label>
                </td>
                <th>
                    課程名稱(期別)
                </th>
                <td>
                    <asp:Label ID="lab_name_flag" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    課程簡介
                </th>
                <td>
                    <asp:Label ID="lab_memo" runat="server"></asp:Label>
                </td>
                <th>
                    資格條件說明
                </th>
                <td>
                    <asp:Label ID="lab_limit" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    上課地點
                </th>
                <td>
                    <asp:Label ID="lab_e01_name" runat="server"></asp:Label>
                </td>
                <th>
                    招收名額上限
                </th>
                <td>
                    <asp:Label ID="lab_people" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    認證時數
                </th>
                <td>
                    <asp:Label ID="lab_hour" runat="server"></asp:Label>
                </td>
                <th>
                    報名審核狀況
                </th>
                <td>
                    <asp:Label ID="lab_check" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    講師姓名
                </th>
                <td>
                    <asp:Label ID="lab_teacher" runat="server"></asp:Label>
                </td>
                <th>
                    上線開放日期
                </th>
                <td>
                    <asp:Label ID="lab_opendate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    報名起迄日期
                </th>
                <td colspan="3">
                    <asp:Label ID="lab_signdate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    上課起迄時間
                </th>
                <td colspan="3">
                    <asp:Label ID="lab_date" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th colspan="4">
                    報名明細
                </th>
            </tr>
            <tr>
                <td colspan="4">
                    <div>
                        <cc1:GridView ID="GridView1" runat="server" DataSourceID="ODS_1" AutoGenerateColumns="False"
                            Width="100%" AllowPaging="True" CellPadding="3" CellSpacing="3" GridLines="None"
                            EmptyDataText="目前無資料" OnRowDataBound="GridView1_RowDataBound" PageSize="5">
                            <Columns>
                                <asp:BoundField DataField="e04_depno" HeaderText="單位" SortExpression="e04_depno" />
                                <asp:BoundField DataField="e04_peouid" HeaderText="姓名" SortExpression="e04_peouid" />
                                <asp:BoundField DataField="e04_applydate" HeaderText="報名日期" SortExpression="e04_applydate"
                                    DataFormatString="{0:yyyy-MM-dd}"></asp:BoundField>
                            </Columns>
                        </cc1:GridView>
                    </div>
                    <div class="pager">
                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="25">
                            <Fields>
                                <NXEIP:GooglePagerField />
                            </Fields>
                        </asp:DataPager>
                    </div>
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
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="關閉" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" />
        </div>
    </div>

    </form>
</body>
</html>
