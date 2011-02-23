<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350101.aspx.cs" Inherits="_35_350100_350101" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function update(msg) {
            tb_remove();
            alert(msg);

            var str = chang('<%=LinkButton1.ClientID%>');
            __doPostBack(str, '')
        }
        
        function chang(str) {
            //將底線換成$符號
            var regex = /\_/g;
            return str.replace(regex, '$');
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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>"
        SelectCommand="SELECT rol_no, rol_name, rol_memo, rol_createuid, rol_createtime, rol_default FROM role">
    </asp:SqlDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="350101" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="350101-1.aspx?modal=true&TB_iframe=true&height=378&width=600"
                        value="新增角色資料" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="True" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    GridLines="None" OnRowCommand="GridView1_RowCommand" DataKeyNames="rol_no"
                    OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="rol_name" HeaderText="角色名稱" SortExpression="rol_no" />
                        <asp:BoundField DataField="rol_memo" HeaderText="角色備註" SortExpression="rol_memo" />
                        <asp:BoundField DataField="rol_createuid" HeaderText="修建者" SortExpression="rol_createuid" />
                        <asp:BoundField DataField="rol_createtime" HeaderText="修建時間" SortExpression="rol_createtime"
                            DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                        <asp:BoundField DataField="rol_default" HeaderText="預設值" SortExpression="rol_default">
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="預設角色">
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CssClass="alter" CommandName="default"
                                    CommandArgument="<%# Container.DataItemIndex %>" ToolTip='<%# Eval("rol_name", "設定{0}為預設角色") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="人員明細" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a class="thickbox imageButton people" href='<%# Eval("rol_no", "350101-2.aspx?rol_no={0}&modal=true&TB_iframe=true") %>'>
                                    <span>人員</span> </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:TemplateField>
                        <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="alter" HeaderText="設定權限"
                            CommandName="set">
                            <ControlStyle CssClass="alter" />
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:ButtonField>
                        <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a class="thickbox imageButton edit" title='<%# Eval("rol_name", "修改{0}") %>' href='<%# Eval("rol_no", "350101-1.aspx?modal=true&mode=edit&ID={0}&TB_iframe=true&height=378&width=600") %>'>
                                    <span>修改</span> </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="Button2" runat="server" CommandName="disable" CssClass="delete" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick=" return confirm('確定要刪除?')" />
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
                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="25">
                        <Fields>
                            <NXEIP:GooglePagerField />
                        </Fields>
                    </asp:DataPager>
                </div>
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click1"></asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
