<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350204-1.aspx.cs" Inherits="_35_350200_350204_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>"
        SelectCommand="SELECT people.peo_uid, people.peo_workid, people.peo_name, departments.dep_name, people.peo_jobtype, people.peo_pfofess, people.peo_ptype, people.peo_arrivedate, people.peo_leave FROM people INNER JOIN departments ON people.dep_no = departments.dep_no">
    </asp:SqlDataSource>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                <tr>
                    <td>
                        <table width="100%" height="500" border="0" cellpadding="0" cellspacing="20">
                            <tr>
                                <td height="22" valign="top">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="17">
                                                <img src="../../image/b01.gif" width="17" height="22" />
                                            </td>
                                            <td background="../../image/b01-1.gif" class="b01">
                                                帳號管理 / 人員管理 /<strong> 人事資料查修</strong>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Panel ID="Panel1" runat="server" Visible="true">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td class="leftheaderbg">
                                                    &nbsp;
                                                </td>
                                                <td class="a02-15 headerbg">
                                                    人事資料查修
                                                </td>
                                                <td background="../../image/b02-1.gif">
                                                    <div align="right">
                                                        &nbsp;<asp:Button ID="Button1" runat="server" Text="匯出Excel" CssClass="b-input" OnClick="Button1_Click" />
                                                    </div>
                                                </td>
                                                <td class="rightheaderbg">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <cc1:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="3" CellSpacing="3" CssClass="tableData" DataKeyNames="peo_uid" DataSourceID="SqlDataSource1"
                                            GridLines="None" Width="100%" OnRowDataBound="GridView1_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="dep_name" HeaderText="單位" SortExpression="dep_name" />
                                                <asp:BoundField DataField="peo_pfofess" HeaderText="職稱" SortExpression="peo_pfofess" />
                                                <asp:BoundField DataField="peo_name" HeaderText="姓名" SortExpression="peo_name" />
                                                <asp:BoundField DataField="peo_ptype" HeaderText="人員類別" SortExpression="peo_ptype" />
                                                <asp:BoundField DataField="peo_workid" HeaderText="人事編號" SortExpression="peo_workid" />
                                                <asp:BoundField DataField="peo_arrivedate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="到職日"
                                                    SortExpression="peo_arrivedate" />
                                                <asp:BoundField DataField="peo_leave" DataFormatString="{0:yyyy-MM-dd}" HeaderText="離職日"
                                                    SortExpression="peo_leave" />
                                                <asp:TemplateField HeaderText="修改">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="modify" ImageUrl="~/image/edit.gif" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="檢視">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                            CommandName="look" ImageUrl="~/image/peruse.gif" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </cc1:GridView>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td class="leftfootbg">
                                                </td>
                                                <td class="footbg">
                                                    &nbsp;
                                                </td>
                                                <td class="rightfootbg">
                                                </td>
                                            </tr>
                                        </table>
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
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel2" runat="server" Visible="true">
                                        <!--人員修改-->
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td class="leftheaderbg">
                                                    &nbsp;
                                                </td>
                                                <td class="a02-15 headerbg">
                                                    人事資料查修
                                                </td>
                                                <td class="rightheaderbg">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>

                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
