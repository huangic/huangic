<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="200507.aspx.cs" Inherits="_20_200500_200507" %>
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
        SelectMethod="GetAll" TypeName="NXEIP.DAO.CommendDAO" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="sys06no" Type="Int32" />
            <asp:Parameter DefaultValue="" Name="keyword" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200507" />
    <div class="tableDiv" style="width:600px">
        <table>
            <tr>
                <td>
                    分類：<asp:DropDownList ID="ddl_sys06" runat="server" CssClass="select4">
                    </asp:DropDownList>
                    &nbsp;&nbsp; 網站名稱：<asp:TextBox ID="txt_keyword" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="搜尋" 
                        onclick="btn_submit_Click" />
                </td>
            </tr>
        </table>
        <div class="bottom">
        </div>
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="200507-1.aspx?height=300&width=600&TB_iframe=true&modal=true"
                        value="提供網站" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    EmptyDataText="查無資料" DataKeyNames="com_no"
                    GridLines="None" OnRowCommand="GridView1_RowCommand" 
                    onrowdatabound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="s06_name" HeaderText="分類名稱" SortExpression="s06_name" />
                        <asp:HyperLinkField DataNavigateUrlFields="com_www" DataNavigateUrlFormatString="{0}"
                            DataTextField="com_name" HeaderText="網站名稱" Target="_blank" 
                            NavigateUrl="/" />
                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='<%# Eval("com_name", "修改{0}") %>'
                                    href='<%# Eval("com_no", "200507-1.aspx?modal=true&mode=modify&no={0}&TB_iframe=true&height=300&width=600") %>'>
                                    <span>修改</span></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="Button3" runat="server" CommandName="del" CommandArgument="<%# Container.DataItemIndex %>"
                                    CssClass="delete" OnClientClick=" return confirm('確定要刪除?')" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40" />
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
                <div id="div_msg" runat="server">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

