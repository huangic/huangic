<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300403.aspx.cs" Inherits="_30_300400_300403" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls" TagPrefix="cc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxtoolkit:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" SelectCountMethod="GetAllCount" SelectMethod="GetAll" TypeName="NXEIP.DAO.RoomsDAO"></asp:ObjectDataSource>
    <script type="text/javascript" src="../../js/lytebox.js"></script>
    <asp:Label ID="lab_pageIndex" runat="server" Visible="False"></asp:Label>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300403" />
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
                    場地名稱：<asp:DropDownList ID="ddl_rooms" runat="server" CssClass="select4">
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
                    EmptyDataText="查無資料" DataKeyNames="roo_no" OnRowDataBound="GridView1_RowDataBound"
                    GridLines="None" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="spo_no" HeaderText="所在地" SortExpression="spo_no" />
                        <asp:BoundField DataField="roo_name" HeaderText="場地名稱" SortExpression="roo_name" />
                        <asp:BoundField DataField="roo_human" HeaderText="容納人數" SortExpression="roo_human" />
                        <asp:BoundField DataField="roo_oneuid" HeaderText="保管人" SortExpression="roo_oneuid" />
                        <asp:BoundField DataField="roo_tel" HeaderText="保管人電話" SortExpression="roo_tel" />
                        <asp:BoundField DataField="roo_floor" HeaderText="所在樓層" SortExpression="roo_floor" />
                        <asp:BoundField DataField="roo_describe" HeaderText="場地描述" SortExpression="roo_describe" />
                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                <asp:Button ID="Button2" runat="server" CommandName="modify" CommandArgument="<%# Container.DataItemIndex %>"
                                    CssClass="edit" /></ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="35px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="Button3" runat="server" CommandName="del" CommandArgument="<%# Container.DataItemIndex %>"
                                    CssClass="delete" OnClientClick=" return confirm('確定要刪除?')" /></ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="35px" />
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