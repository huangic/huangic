<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100203.aspx.cs" Inherits="_10_100200_100203" %>

<%@ Register Assembly="RssToolkit" Namespace="RssToolkit.Web.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register src="~/lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="Get_RssDataCount" SelectMethod="Get_RssData" TypeName="NXEIP.DAO.RssDAO"
        EnablePaging="True">
        <SelectParameters>
            <asp:Parameter Name="peo_uid" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100203" />
    <div class="tableDiv">
        <div class="select">
            <span class="a-letter-2"><span class="a-letter-1">RSS網址：
                <asp:TextBox ID="tbox_url" runat="server" Width="275px"></asp:TextBox>
                &nbsp;&nbsp;排序<asp:TextBox ID="tbox_order" runat="server" MaxLength="3" Width="50px"></asp:TextBox>&nbsp;&nbsp;
            </span>
                <asp:Button ID="Button1" runat="server" Text="新增" CssClass="b-input" OnClick="Button1_Click" />
            </span>
        </div>
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
            AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
            EmptyDataText="查無資料" GridLines="None" EnableViewState="False" DataKeyNames="rss_no"
            OnRowCommand="GridView1_RowCommand" OnDataBound="GridView1_DataBound">
            <Columns>
                <asp:BoundField DataField="rss_order" HeaderText="排序" SortExpression="rss_order">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="RSS標題">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" CommandArgument='<%# Eval("rss_address") %>'
                            runat="server"><%# Eval("rss_subject") %></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="80%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="刪除">
                    <ItemTemplate>
                        <asp:Button ID="Button_del" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                            CommandName="del" OnClientClick=" return confirm('確定要刪除?')" CssClass="delete" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
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
            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="25">
                <Fields>
                    <NXEIP:GooglePagerField />
                </Fields>
            </asp:DataPager>
        </div>
        <br />
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <cc1:GridView ID="GridView2" runat="server"
            AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
            EmptyDataText="查無資料" GridLines="None" EnableViewState="False">
            <Columns>
                <asp:TemplateField HeaderText="標題名稱">
                    <ItemTemplate>
                        <li class="ins">
                            <asp:HyperLink ID="HyperLink1" Target="_blank" NavigateUrl='<%# Eval("link") %>'
                                runat="server"><%# Eval("title") %></asp:HyperLink>
                        </li>
                    </ItemTemplate>
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
        
    </div>
</asp:Content>

