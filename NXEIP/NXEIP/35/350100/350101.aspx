<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350101.aspx.cs" Inherits="_35_350100_350101" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    &nbsp;<table width="100%" cellspacing="20" cellpadding="0" border="0">
        <tbody>
            <tr>
                <td valign="top" class="style1">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td width="17">
                                    <img width="17" height="22" src="../../image/b01.gif">
                                </td>
                                <td background="../../image/b01-1.gif" class="b01">
                                    帳號管理 / 權限管理 /<strong> 角色設定 </strong>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td width="17">
                                    <img width="17" height="29" src="../../image/b02.gif">
                                </td>
                                <td background="../../image/b02-1.gif" class="a02-15">
                                    角色設定
                                </td>
                                <td background="../../image/b02-1.gif">
                                    <div align="right">
                                        <input type="button" class="thickbox b-input" alt="350101-1.aspx?modal=true&TB_iframe=true&height=250&width=550"
                                            value="新增角色資料">
                                        &nbsp;</div>
                                </td>
                                <td width="17">
                                    <img width="17" height="29" src="../../image/b02-2.gif">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <strong>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <cc1:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
                                    Width="100%" AllowPaging="True" CellPadding="3" CellSpacing="3" CssClass="tableData"
                                    GridLines="None" OnRowCommand="GridView1_RowCommand" DataKeyNames="rol_no" EnableViewState="False"
                                    OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="rol_name" HeaderText="角色名稱" SortExpression="rol_no" />
                                        <asp:BoundField DataField="rol_memo" HeaderText="角色備註" SortExpression="rol_memo" />
                                        <asp:BoundField DataField="rol_createuid" HeaderText="修建者" SortExpression="rol_createuid" />
                                        <asp:BoundField DataField="rol_createtime" HeaderText="修建時間" SortExpression="rol_createtime"
                                            DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                                        <asp:TemplateField HeaderText="人員明細" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <a id="btnShowPopup" runat="server" class="thickbox" href='<%# Eval("rol_no", "350101-2.aspx?rol_no={0}&modal=true&TB_iframe=true") %>'>
                                                    <asp:Image ID="Image1" ImageUrl="~/image/v05.gif" runat="server" />
                                                </a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:ButtonField Text="設定權限" ButtonType="Image" DataTextField="rol_no" HeaderText="設定權限"
                                            ImageUrl="~/image/alter.gif" CommandName="set">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <a id="btnShowPopup" runat="server" class="thickbox" title='<%# Eval("rol_name", "修改{0}") %>'
                                                    href='<%# Eval("rol_no", "350101-1.aspx?modal=true&mode=edit&ID={0}&TB_iframe=true&height=450&width=600") %>'>
                                                    <asp:Image ID="Image1" ImageUrl="~/image/edit.gif" runat="server" />
                                                </a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="刪除">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                    CommandName="disable" ImageUrl="~/image/delete.gif" OnClientClick=" return confirm('確定要刪除?')" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </cc1:GridView>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="17">
                                            <img src="../../image/b02-3.gif" width="17" height="17" />
                                        </td>
                                        <td background="../../image/b02-4.gif">
                                            &nbsp;
                                        </td>
                                        <td width="17">
                                            <img src="../../image/b02-5.gif" width="17" height="17" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>"
                                    SelectCommand="SELECT rol_no, rol_name, rol_memo, rol_createuid, rol_createtime, rol_default FROM role">
                                </asp:SqlDataSource>
                                <div class="pager">
                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="10">
                                        <Fields>
                                            <asp:NextPreviousPagerField ShowNextPageButton="False" />
                                            <asp:NumericPagerField />
                                            <asp:NextPreviousPagerField ShowPreviousPageButton="False" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </strong>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
