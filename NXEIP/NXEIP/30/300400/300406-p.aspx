<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="300406-p.aspx.cs" Inherits="_30_300400_300406_p" %>
<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>
<%@ Register assembly="MattBerseth.WebControls" namespace="MattBerseth.WebControls" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>場地使用情況列印</title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
     <style type="text/css">.timecss { FONT-WEIGHT: bold; HEIGHT: 24px; TEXT-DECORATION: none;font-size:13px }
	.timecss1 { FONT-WEIGHT: bold; COLOR: #000000; TEXT-DECORATION: none;font-size:13px }
	.timecss2 { BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; FONT-WEIGHT: bold; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid; TEXT-DECORATION: none;font-size:13px }
	.timecss3 { BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid; TEXT-DECORATION: none;font-size:13px }
	.href1 { COLOR: #000000; TEXT-DECORATION: none;font-size:13px }
	.table1 { BORDER-RIGHT: medium none; BORDER-TOP: medium none;BORDER-LEFT: medium none; BORDER-BOTTOM: medium none; BORDER-COLLAPSE: collapse;font-size:13px}
    </style>
</head>
<body onload="print();">
    <form id="form1" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" SelectCountMethod="GetAllCount" SelectMethod="GetAll" 
        TypeName="NXEIP.DAO.PetitionDAO" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="sdate" Type="String" />
            <asp:Parameter Name="edate" Type="String" />
            <asp:Parameter Name="status" Type="String" />
            <asp:Parameter Name="spots1" Type="Int32" />
            <asp:Parameter Name="rooms1" Type="Int32" />
            <asp:Parameter Name="loginuser" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
      <tr>
        <td>
            <asp:Label ID="lab_sdate" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lab_edate" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lab_spot1" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lab_rooms1" runat="server" Visible="False"></asp:Label>
          </td>
      </tr>
      <tr>
        <td>
            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                <tr align="center" style="height:30px">
                    <td align="left" width="25%">&nbsp;</td>
                    <td align="center" width="50%">
                        <asp:Label ID="lab_outxt" runat="server" CssClass="timecss1">場地使用情況</asp:Label>
                    </td>
                    <td align="right" width="25%"></td>
                </tr>
            </table>
            <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False"
                CellPadding="3" CellSpacing="3" CssClass="table1" EmptyDataText="查無資料" OnRowDataBound="GridView1_RowDataBound"
                GridLines="None" DataKeyNames="pet_no" Width="100%" AllowPaging="True" 
                ShowHeaderWhenEmpty="True" PageSize="2">
                <Columns>
                    <asp:BoundField DataField="spo_name" HeaderText="所在地" SortExpression="spo_name">
                        <HeaderStyle CssClass="timecss2" />
                        <ItemStyle CssClass="timecss3" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="roo_name" HeaderText="場地名稱" SortExpression="roo_name">
                        <HeaderStyle CssClass="timecss2" />
                        <ItemStyle CssClass="timecss3" Width="12%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="pet_depno" HeaderText="借用單位" SortExpression="pet_depno">
                        <HeaderStyle CssClass="timecss2" />
                        <ItemStyle CssClass="timecss3" Width="12%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="uidtel" HeaderText="申請人" SortExpression="uidtel">
                        <HeaderStyle CssClass="timecss2" />
                        <ItemStyle CssClass="timecss3" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="stet" HeaderText="借用時間" SortExpression="stet">
                        <HeaderStyle CssClass="timecss2" />
                        <ItemStyle CssClass="timecss3" Width="12%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="pet_host" HeaderText="主持人" SortExpression="pet_host">
                        <HeaderStyle CssClass="timecss2" />
                        <ItemStyle CssClass="timecss3" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="pet_count" HeaderText="與會人數" SortExpression="pet_count">
                        <HeaderStyle CssClass="timecss2" />
                        <ItemStyle CssClass="timecss3" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="pet_reason" HeaderText="申請事由" SortExpression="pet_reason">
                        <HeaderStyle CssClass="timecss2" />
                        <ItemStyle CssClass="timecss3" Width="19%" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle HorizontalAlign="Left" Height="26px" />
                <RowStyle Height="40px" />
            </cc1:GridView>
        </td>
      </tr>
    </table>
    </form>
</body>
</html>