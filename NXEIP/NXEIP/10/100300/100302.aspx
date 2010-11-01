<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100302.aspx.cs" Inherits="_10_100300_100302" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" SelectCountMethod="GetAllCount"
        SelectMethod="GetAll" TypeName="NXEIP.DAO.C01DAO" 
        OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:Parameter Name="peo_uid" Type="String" />
    </SelectParameters>
    </asp:ObjectDataSource>
<uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300402" />
<div class="tableDiv">
        <div class="header">
            <div class="h1"></div>
            <div class="h2">
                <div class="function">
                    <asp:Button ID="btn_add" runat="server" Text="新增開放人員" CssClass="b-input" OnClick="btn_add_Click" />
                </div>
            </div>
            <div class="h3"></div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    EmptyDataText="查無資料" DataKeyNames="c01_no" OnRowDataBound="GridView1_RowDataBound"
                    GridLines="None" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="單位"></asp:TemplateField>
                        <asp:TemplateField HeaderText="職稱"></asp:TemplateField>
                        <asp:BoundField DataField="peo_uid" HeaderText="姓名" SortExpression="peo_uid" />
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="Button3" runat="server" CommandName="del" 
                                    CommandArgument="<%# Container.DataItemIndex %>" CssClass="delete" 
                                    OnClientClick=" return confirm('確定要刪除?')" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
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
