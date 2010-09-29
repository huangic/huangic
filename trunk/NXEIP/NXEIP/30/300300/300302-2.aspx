<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300302-2.aspx.cs" Inherits="_30_300300_300302_2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" SelectCountMethod="GetClassDataCount"
        SelectMethod="GetClassData" TypeName="NXEIP.DAO.TypesDAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="typ_parent" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300302" />
    <div id="div_title" runat="server"></div>
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="300302-3.aspx?modal=true&mode=new&typ_no=&typ_parent=<%Response.Write(Request["typ_no"]); %>&TB_iframe=true" value="新增子類別課程" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    EmptyDataText="查無資料" DataKeyNames="typ_no" OnRowDataBound="GridView1_RowDataBound"
                    GridLines="None" OnRowCommand="GridView1_RowCommand" 
                    EnableViewState="False">
                    <Columns>
                        <asp:BoundField DataField="typ_number" HeaderText="課程類別代號" SortExpression="typ_number" />
                        <asp:BoundField DataField="typ_cname" HeaderText="課程類別" SortExpression="typ_cname" />
                        <asp:BoundField DataField="typ_order" HeaderText="排列順序" SortExpression="typ_order" />
                        <asp:BoundField DataField="typ_createuid" HeaderText="修建者" SortExpression="typ_createuid" />
                        <asp:BoundField DataField="typ_createtime" HeaderText="修建時間" SortExpression="typ_createtime"
                            DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='<%# Eval("typ_cname", "修改{0}") %>'
                                    href='<%# Eval("typ_no", "300302-3.aspx?modal=true&mode=modify&typ_no={0}&TB_iframe=true&height=250&width=600") %>'>
                                    <span>修改</span> </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="Button3" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                    CommandName="del" OnClientClick=" return confirm('確定要刪除?')" CssClass="delete" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:TemplateField>
                    </Columns>
                </cc1:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
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
        <div class="bottom">
            <asp:Button ID="Button1" runat="server" Text="回課程類別管理" CssClass="a-input" 
                onclick="Button1_Click" />
        </div>
        <div id="div_msg" runat="server">
        </div>
    </div>
</asp:Content>

