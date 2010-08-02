<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350101-3.aspx.cs" Inherits="_35_350100_350101_3" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="17">
                <img src="../../image/b02.gif" width="17" height="29" />
            </td>
            <td background="../../image/b02-1.gif" class="a02-15">
                設定權限 - 角色資料
            </td>
            <td background="../../image/b02-1.gif">
                &nbsp;
            </td>
            <td width="17">
                <img src="../../image/b02-2.gif" width="17" height="29" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="3" cellspacing="1" bgcolor="#CCCCCC">
        <tr>
            <td width="100" bgcolor="#eeeeee" class="a-letter-2">
                <div align="right">
                    角色名稱
                </div>
            </td>
            <td width="402" bgcolor="#FFFFFF" class="a-letter-1">
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
            <td width="118" bgcolor="#EEEEEE" class="a-letter-2">
                <div align="right">
                    <div align="right">
                        角色備註</div>
                </div>
            </td>
            <td width="313" bgcolor="#FFFFFF" class="a-letter-1">
                <asp:Label ID="Label2" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <div align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" CssClass="tableData" DataSourceID="SqlDataSource1"
                    GridLines="None" Width="75%" DataKeyNames="sys_no,sfu_no" OnRowDataBound="GridView1_RowDataBound"
                    OnDataBound="GridView1_DataBound" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="sys_name" HeaderText="系統選項" SortExpression="sys_name">
                            <ItemStyle HorizontalAlign="Left" Width="17%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="sfu_name" SortExpression="sfu_name" HeaderText="系統功能">
                            <ItemStyle HorizontalAlign="Left" Width="17%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="選用">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbox1" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="進階選項">
                            <ItemTemplate>
                                <a title='<%# Eval("sfu_name", "開啟 {0} 子系統選項") %>'>
                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                        CommandName="show" ImageUrl="~/image/Add.gif" />
                                </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="子系統選項">
                            <ItemTemplate>
                                <cc1:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CaptionAlign="Top"
                                    CellPadding="3" CellSpacing="3" DataKeyNames="sfu_no" DataSourceID="SqlDataSource2"
                                    CssClass="tableData" GridLines="None" Visible="False" Width="100%" 
                                    onrowdatabound="GridView2_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="sfu_name" HeaderText="子系統" SortExpression="sfu_name">
                                            <HeaderStyle CssClass="b-input" />
                                            <ItemStyle HorizontalAlign="Left" BackColor="White" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="選用">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbox2" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="b-input" />
                                            <ItemStyle HorizontalAlign="Center" Width="20%" BackColor="White" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="sfu_no" HeaderText="sfu_no" ReadOnly="True" 
                                            SortExpression="sfu_no" />
                                    </Columns>
                                </cc1:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>"
                    SelectCommand="SELECT sys.sys_no, sys.sys_name, sysfuction.sfu_no, sysfuction.sfu_name FROM sys LEFT OUTER JOIN sysfuction ON sys.sys_no = sysfuction.sys_no WHERE (sys.sys_status = '1') AND (sysfuction.sfu_status = '1') AND (sysfuction.sfu_parent = 0) ORDER BY sys.sys_order, sys.sys_no, sysfuction.sfu_order">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>"
                    SelectCommand="SELECT         sfu_no, sfu_name
FROM             sysfuction
WHERE         (sfu_status = '1') AND (sfu_parent = @sfu_parent)
ORDER BY  sfu_order, sfu_no">
                    <SelectParameters>
                        <asp:Parameter Name="sfu_parent" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <table width="100%" border="0" cellspacing="10" cellpadding="0">
                    <tr>
                        <td>
                            <div align="center">
                                <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="確定" OnClick="Button1_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button2" runat="server" CssClass="a-input" Text="回上一頁" OnClick="Button2_Click" />
                                &nbsp;</div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
