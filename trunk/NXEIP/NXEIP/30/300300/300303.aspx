<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="300303.aspx.cs" Inherits="_30_300300_300303" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc3" %>
<%@ Register Src="../../lib/calendar.ascx" TagName="calendar" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ODS_e01" runat="server" SelectMethod="GetAll" TypeName="NXEIP.DAO.e01DAO">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_type_1" runat="server" SelectMethod="GetClassParentData"
        TypeName="NXEIP.DAO.TypesDAO"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_type_2" runat="server" SelectMethod="GetClassData"
        TypeName="NXEIP.DAO.TypesDAO">
        <SelectParameters>
            <asp:Parameter Name="typ_parent" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_1" runat="server" SelectMethod="GetData" TypeName="NXEIP.DAO._300303DAO"
        EnablePaging="True" SelectCountMethod="GetDataCount">
        <SelectParameters>
            <asp:Parameter Name="sdate" Type="String" />
            <asp:Parameter Name="edate" Type="String" />
            <asp:Parameter Name="type_1" Type="String" />
            <asp:Parameter Name="type_2" Type="String" />
            <asp:Parameter Name="e01_no" Type="String" />
            <asp:Parameter Name="e02_name" Type="String" />
            <asp:Parameter Name="openuid" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc3:Navigator ID="Navigator1" runat="server" SysFuncNo="300303" />
    <div class="tableDiv">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            起迄時間：起&nbsp;<uc4:calendar ID="calendar1" runat="server" _Show="false" />
                            &nbsp; 迄&nbsp;<uc4:calendar ID="calendar2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            課程名稱：<asp:TextBox ID="tbox_name" runat="server" Width="230px"></asp:TextBox>
                            &nbsp;&nbsp; 上課地點：<asp:DropDownList ID="ddl_e01" runat="server" DataSourceID="ODS_e01"
                                DataTextField="e01_name" DataValueField="e01_no">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp; 課程大類&nbsp;
                            <asp:DropDownList ID="ddl_type_1" runat="server" AutoPostBack="True" DataSourceID="ODS_type_1"
                                DataTextField="typ_cname" DataValueField="typ_no" OnSelectedIndexChanged="ddl_type_1_SelectedIndexChanged">
                            </asp:DropDownList>
                            課程類別&nbsp;<asp:DropDownList ID="ddl_type_2" runat="server" DataSourceID="ODS_type_2"
                                DataTextField="typ_cname" DataValueField="typ_no">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="bottom">
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="查詢課程" OnClick="btn_ok_Click" />
        </div>
    </div>
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <asp:Button ID="Button_new" runat="server" Text="新增課程" CssClass="b-input" OnClick="Button_new_Click" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ODS_1" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="True" CellPadding="3" CellSpacing="3" GridLines="None"
                    OnRowCommand="GridView1_RowCommand" DataKeyNames="e02_no,e02_signedate,e02_edate,e02_flag"
                    EnableViewState="False" EmptyDataText="目前無資料" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="e02_name" HeaderText="課程名稱(期別)" SortExpression="e02_name" />
                        <asp:BoundField DataField="e02_hour" HeaderText="認證時數" SortExpression="e02_hour">
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="e02_signdate" HeaderText="報名起迄日期" SortExpression="e02_signdate"
                            DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="e02_sdate" HeaderText="上課起迄日期" SortExpression="e02_sdate"
                            DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="e02_no" HeaderText="報名狀況" SortExpression="e02_no"></asp:BoundField>
                        <asp:TemplateField HeaderText="部門限制">
                            <ItemTemplate>
                                <asp:Button ID="Button4" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                    CommandName="cond" CssClass="edit" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="講義">
                            <ItemTemplate>
                                <asp:Button ID="Button3" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                    CommandName="book" CssClass="edit" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                <asp:Button ID="Button2" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                    CommandName="edit" CssClass="edit" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                    CommandName="disable" OnClientClick=" return confirm('確定要刪除?')" CssClass="delete" />
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
                            <NXEIP:GooglePagerField />
                        </Fields>
                    </asp:DataPager>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
