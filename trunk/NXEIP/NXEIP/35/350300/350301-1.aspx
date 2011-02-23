<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="350301-1.aspx.cs" Inherits="_35_350300_350301_1" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCountMethod="SearchDataCount" SelectMethod="SearchData" 
        TypeName="NXEIP.DAO.OperatesDAO">
        <SelectParameters>
            <asp:Parameter Name="date" Type="String" />
            <asp:Parameter Name="sfu" Type="String" />
            <asp:Parameter Name="opt" Type="String" />
            <asp:Parameter Name="key" Type="String" />
            <asp:Parameter Name="value" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator2" runat="server" SysFuncNo="350301" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False"
            Width="100%" AllowPaging="True" CellPadding="3" CellSpacing="3" GridLines="None"
            OnRowCommand="GridView1_RowCommand" EmptyDataText="目前無資料" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="sfu_no" HeaderText="功能名稱" SortExpression="sfu_no">
                    <ItemStyle Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="peo_uid" HeaderText="姓名" SortExpression="peo_uid">
                    <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="ope_logintime" HeaderText="操作時間" SortExpression="ope_logintime"
                    DataFormatString="{0:yyyy-MM-dd HH:mm}">
                    <ItemStyle Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="ope_fuction" HeaderText="動作" SortExpression="ope_fuction">
                    <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="ope_memo" HeaderText="備註" SortExpression="ope_memo" />
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
            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="25">
                <Fields>
                    <NXEIP:GooglePagerField />
                </Fields>
            </asp:DataPager>
        </div>
        <div class="bottom">
            <asp:Button ID="btn_ok" runat="server" CssClass="a-input" Text="回上一頁" OnClick="btn_ok_Click" />
        </div>
    </div>
</asp:Content>

