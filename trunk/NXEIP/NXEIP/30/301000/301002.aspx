<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="301002.aspx.cs" Inherits="_30_301000_301002" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls" TagPrefix="cc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script type="text/javascript">
    function update(msg) {
        __doPostBack('<%=UpdatePanel1.ClientID%>', '');
        tb_remove();
        alert(msg);
    }

    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {
            //  reapply the thick box stuff
            tb_init('a.thickbox');
        }
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxtoolkit:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" SelectCountMethod="GetAllCount"
        SelectMethod="GetAll" TypeName="NXEIP.DAO.BorrowsDAO" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="sdate" Type="String" />
            <asp:Parameter Name="edate" Type="String" />
            <asp:Parameter Name="status" Type="String" />
            <asp:Parameter Name="spots1" Type="Int32" />
            <asp:Parameter Name="equ1" Type="Int32" />
            <asp:Parameter Name="loginuser" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <script type="text/javascript" src="../../js/lytebox.js"></script>
    <asp:Label ID="lab_pageIndex" runat="server" Visible="False"></asp:Label>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="301002" />
    <div class="tableDiv">
        <table>
            <tr>
                <td>
                    欲查詢日期：起&nbsp;<uc2:calendar ID="calendar1" runat="server" _Show="False" />
                    迄&nbsp;
                    <uc2:calendar ID="calendar2" runat="server" />&nbsp;&nbsp;
                    審核狀況：<asp:RadioButtonList ID="rbl_status" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">未核可</asp:ListItem>
                        <asp:ListItem Value="2">核可</asp:ListItem>
                        <asp:ListItem Value="0">全部皆有</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    所在地：<asp:DropDownList ID="ddl_spot" runat="server" CssClass="select4" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_spot_SelectedIndexChanged">
                    </asp:DropDownList>
                    設備名稱：<asp:DropDownList ID="ddl_equ" runat="server" CssClass="select4">
                    </asp:DropDownList>&nbsp;&nbsp;
                    <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="查詢申請單" OnClick="btn_submit_Click" />
                </td>
            </tr>
        </table>
        <div class="bottom">
        </div>
        <div class="header">
            <div class="h1"></div>
            <div class="h2"></div>
            <div class="h3"></div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    EmptyDataText="查無資料" OnRowDataBound="GridView1_RowDataBound" GridLines="None"
                    DataKeyNames="bor_no">
                    <Columns>
                        <asp:BoundField DataField="spo_name" HeaderText="所在地" SortExpression="spo_name" />
                        <asp:BoundField DataField="equ_name" HeaderText="設備名稱" SortExpression="equ_name" />
                        <asp:BoundField DataField="bor_depno" HeaderText="借用單位" SortExpression="bor_depno" />
                        <asp:BoundField DataField="bor_applyuid" HeaderText="申請人" SortExpression="bor_applyuid" />
                        <asp:BoundField DataField="stet" HeaderText="借用時間" SortExpression="stet" />
                        <asp:BoundField DataField="bor_reason" HeaderText="申請事由" SortExpression="bor_reason" />
                        <asp:BoundField DataField="bor_apply" HeaderText="狀態" SortExpression="bor_apply" />
                        <asp:TemplateField HeaderText="審核">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Left" />
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
                            <NXEIP:GooglePagerField />
                        </Fields>
                    </asp:DataPager>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
