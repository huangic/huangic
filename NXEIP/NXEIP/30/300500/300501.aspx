<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300501.aspx.cs" Inherits="_30_300500_300501" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
        OldValuesParameterFormatString="original_{0}" SelectCountMethod="GetAllCount" 
        SelectMethod="GetAll" TypeName="NXEIP.DAO.Sys06DAO"></asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300501" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="300501-1.aspx?modal=true&model=new&TB_iframe=true"
                        value="新增類別" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:gridview id="GridView1" runat="server" datasourceid="ObjectDataSource1" allowpaging="True"
                    autogeneratecolumns="False" cellpadding="3" cellspacing="3" cssclass="tableData"
                    emptydatatext="查無資料" datakeynames="s06_no" onrowdatabound="GridView1_RowDataBound"
                    gridlines="None" onrowcommand="GridView1_RowCommand" 
                    enableviewstate="False">
                    <Columns>
                        <asp:BoundField DataField="sfu_no" HeaderText="系統名稱" SortExpression="sfu_no" />
                        <asp:BoundField DataField="s06_parent" HeaderText="類別大分類" 
                            SortExpression="s06_parent" />

                        <asp:BoundField DataField="s06_name" HeaderText="類別小分類" 
                            SortExpression="s06_name" />

                        <asp:BoundField DataField="s06_createuid" HeaderText="修建者" 
                            SortExpression="s06_createuid" />
                        <asp:BoundField DataField="s06_createtime" HeaderText="修建時間" SortExpression="s06_createtime"
                            DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='<%# Eval("s06_name", "修改{0}") %>'
                                    href='<%# Eval("s06_no", "300501-1.aspx?modal=true&mode=modify&s06_no={0}&TB_iframe=true&height=250&width=600") %>'>
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
                </cc1:gridview>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="pager">
            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="10">
                <Fields>
                    <asp:NextPreviousPagerField ShowNextPageButton="False" />
                    <asp:NumericPagerField />
                    <asp:NextPreviousPagerField ShowPreviousPageButton="False" />
                </Fields>
            </asp:DataPager>
        </div>
    </div>
</asp:Content>

