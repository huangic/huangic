<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300201.aspx.cs" Inherits="_30_300200_300201" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls" TagPrefix="cc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
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
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" SelectCountMethod="GetAllCount"
        SelectMethod="GetAll" TypeName="NXEIP.DAO.QuestionaryDAO" 
        OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300201" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">問卷維護</div>
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="300201-1.aspx?height=450&width=800&TB_iframe=true&modal=true" value="新增問卷" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    EmptyDataText="查無資料" DataKeyNames="que_no" OnRowDataBound="GridView1_RowDataBound"
                    GridLines="None" OnRowCommand="GridView1_RowCommand" EnableViewState="False">
                    <Columns>
                        <asp:BoundField DataField="que_name" HeaderText="問卷名稱" SortExpression="que_name" />
                        <asp:BoundField DataField="que_sdate" HeaderText="開放時間" SortExpression="que_sdate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                        <asp:BoundField DataField="que_edate" HeaderText="結束時間" SortExpression="que_edate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                        <asp:BoundField DataField="que_register" HeaderText="是否記名" SortExpression="que_register" />
                        <asp:BoundField DataField="que_status" HeaderText="是否上架" SortExpression="que_status" />
                        <asp:TemplateField HeaderText="題目設定">
                            <ItemTemplate>
                                <asp:Button ID="btn_set" runat="server" CommandName="setup" CommandArgument="<%# Container.DataItemIndex %>"
                                    CssClass="set" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="60" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="預覽">
                            <ItemTemplate>
                                <asp:Button ID="btn_preview" runat="server" CommandName="preview" CommandArgument="<%# Container.DataItemIndex %>"
                                    CssClass="view" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='<%# Eval("que_name", "修改{0}") %>'
                                    href='<%# Eval("que_no", "300201-1.aspx?modal=true&mode=modify&no={0}&TB_iframe=true&height=450&width=800") %>'>
                                    <span>修改</span></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="btn_delete" runat="server" CommandName="del" CommandArgument="<%# Container.DataItemIndex %>"
                                    CssClass="delete" OnClientClick=" return confirm('確定要刪除?')" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="複製">
                            <ItemTemplate>
                                <a id="btnCopy" runat="server" class="thickbox imageButton ver" title='<%# Eval("que_name", "複製 {0}") %>'
                                    href='<%# Eval("que_no", "300201-1.aspx?modal=true&mode=copy&no={0}&TB_iframe=true&height=450&width=800") %>'>
                                    <span>修改</span></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40" />
                        </asp:TemplateField>
                    </Columns>
                </cc1:GridView>
                <div class="footer">
                    <div class="f1"></div>
                    <div class="f2"></div>
                    <div class="f3"></div>
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
