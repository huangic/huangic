﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100201.aspx.cs" Inherits="_10_100200_100201" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
        TypeName="NXEIP.DAO.MessageDAO" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="GetDataCount">
        <SelectParameters>
            <asp:Parameter Name="peo_uid" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetData2"
        TypeName="NXEIP.DAO.MessageDAO" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="GetDataCount2">
        <SelectParameters>
            <asp:Parameter Name="peo_uid" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100201" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">
                    我發送的訊息
                </div>
                <div class="function">
                    <asp:Button ID="Button2" runat="server" Text="發送訊息" CssClass="b-input" OnClick="Button2_Click" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
            AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
            EmptyDataText="查無資料" OnRowDataBound="GridView1_RowDataBound" GridLines="None"
            OnRowCommand="GridView1_RowCommand"  
            DataKeyNames="mes_no">
            <Columns>
                <asp:BoundField DataField="mes_subject" HeaderText="訊息標題" 
                    SortExpression="mes_subject" />
                <asp:BoundField DataField="mes_content" HeaderText="訊息內容" 
                    SortExpression="mes_content">
                    <ItemStyle Width="40%" />
                </asp:BoundField>
                <asp:BoundField DataField="mes_link" HeaderText="訊息連結" 
                    SortExpression="mes_link">
                </asp:BoundField>
                <asp:BoundField DataField="mes_datetime" HeaderText="發送日期" SortExpression="mes_datetime"
                    DataFormatString="{0:yyyy-MM-dd HH:mm}">
                    <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="刪除">
                    <ItemTemplate>
                        <asp:Button ID="Button3" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
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
            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="25"
                class="googleNavegationBar">
                <Fields>
                    <NXEIP:GooglePagerField />
                </Fields>
            </asp:DataPager>
        </div>
        <div class="bottom">
        </div>
    </div>

    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">
                    我收到的訊息
                </div>
                <div class="function">
                    <asp:Button ID="Button4" runat="server" Text="刪除" CssClass="b-input" OnClientClick=" return confirm('確定要刪除?')" OnClick="Button9_Click" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <cc1:GridView ID="GridView2" runat="server" DataSourceID="ObjectDataSource2" AllowPaging="True"
            AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
            EmptyDataText="查無資料" OnRowDataBound="GridView2_RowDataBound" GridLines="None"
            OnRowCommand="GridView2_RowCommand"  
            DataKeyNames="mes_no">
            <Columns>
                <asp:TemplateField HeaderText="選取">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                </asp:TemplateField>
                <asp:BoundField DataField="mes_senduid" HeaderText="發送人" 
                    SortExpression="mes_senduid">
                <ItemStyle Width="8%" />
                </asp:BoundField>
                <asp:BoundField DataField="mes_subject" HeaderText="訊息標題" 
                    SortExpression="mes_subject" />
                <asp:BoundField DataField="mes_content" HeaderText="訊息內容" 
                    SortExpression="mes_content">
                    <ItemStyle Width="40%" />
                </asp:BoundField>
                <asp:BoundField DataField="mes_link" HeaderText="訊息連結" 
                    SortExpression="mes_link">
                </asp:BoundField>
                <asp:BoundField DataField="mes_datetime" HeaderText="發送日期" SortExpression="mes_datetime"
                    DataFormatString="{0:yyyy-MM-dd HH:mm}">
                    <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="刪除">
                    <ItemTemplate>
                        <asp:Button ID="Button3" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
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
            <asp:DataPager ID="DataPager2" runat="server" PagedControlID="GridView2" PageSize="25"
                class="googleNavegationBar">
                <Fields>
                    <NXEIP:GooglePagerField />
                </Fields>
            </asp:DataPager>
        </div>
    </div>
</asp:Content>

