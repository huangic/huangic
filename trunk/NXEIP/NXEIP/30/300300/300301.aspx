<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300301.aspx.cs" Inherits="_30_300300_300301" %>

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
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" SelectCountMethod="GetAllCount"
        SelectMethod="GetAll" TypeName="NXEIP.DAO.e01DAO">
    </asp:ObjectDataSource>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300301" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="300301-1.aspx?modal=true&model=new&TB_iframe=true" value="新增上課地點" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    EmptyDataText="查無資料" DataKeyNames="e01_no" OnRowDataBound="GridView1_RowDataBound"
                    GridLines="None" OnRowCommand="GridView1_RowCommand" 
                    EnableViewState="False">
                    <Columns>
                        <asp:BoundField DataField="e01_name" HeaderText="上課地點" 
                            SortExpression="e01_name" />
                        <asp:BoundField DataField="e01_order" HeaderText="排列順序" 
                            SortExpression="e01_order" />
                        <asp:BoundField DataField="e01_createuid" HeaderText="修建者" 
                            SortExpression="e01_createuid" />
                        <asp:BoundField DataField="e01_createtime" HeaderText="修建時間" 
                            SortExpression="e01_createtime" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='<%# Eval("e01_name", "修改{0}") %>'
                                    href='<%# Eval("e01_no", "300301-1.aspx?modal=true&mode=modify&e01_no={0}&TB_iframe=true&height=250&width=600") %>'>
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
        <div id="div_msg" runat="server">
        </div>
    </div>
</asp:Content>

