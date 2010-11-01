<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350105.aspx.cs" Inherits="_35_350100_350105" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" SelectCountMethod="GetAllCount"
        SelectMethod="GetAll" TypeName="NXEIP.DAO.SysfuctionDAO"></asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="350105" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <asp:Button ID="Button1" runat="server" Text="新增系統" CssClass="b-input" OnClick="Button1_Click" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    EmptyDataText="查無資料" DataKeyNames="sfu_no" OnRowDataBound="GridView1_RowDataBound"
                    GridLines="None" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="sfu_no" HeaderText="系統編號" SortExpression="sfu_no" />
                        <asp:BoundField DataField="sys_no" HeaderText="分類名稱" SortExpression="sys_no" />
                        <asp:BoundField DataField="sfu_name" HeaderText="系統名稱" SortExpression="sfu_name" />
                        <asp:BoundField DataField="sfu_path" HeaderText="系統路徑" SortExpression="sfu_path" />
                        <asp:BoundField DataField="sfu_order" HeaderText="排序" SortExpression="sfu_order" />
                        <asp:BoundField DataField="sfu_status" HeaderText="狀態" SortExpression="sfu_status" />
                        <asp:BoundField DataField="sfu_createuid" HeaderText="修建者" SortExpression="sfu_createuid" />
                        <asp:BoundField DataField="sfu_createtime" HeaderText="修建時間" SortExpression="sfu_createtime"
                            DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                    <asp:Button ID="Button2" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="modify" CssClass="edit" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:TemplateField>
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
                <div id="div_msg" runat="server">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
