<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300603-2.aspx.cs" Inherits="_30_300600_300603_2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function update(msg) {
        __doPostBack('<%=UpdatePanel1.ClientID%>', '')
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
    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager1">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
        TypeName="NXEIP.DAO.Rep06DAO" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="GetDataCount">
        <SelectParameters>
            <asp:Parameter Name="r05_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hidd_r05no" runat="server" />
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300603" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="300603-3.aspx?r05_no=<%=Request["r05_no"] %>&mode=new&modal=true&TB_iframe=true"
                        value="新增維修類別" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    EmptyDataText="查無資料" OnRowDataBound="GridView1_RowDataBound" GridLines="None"
                    OnRowCommand="GridView1_RowCommand" DataKeyNames="r06_no,r05_no">
                    <Columns>
                        <asp:BoundField DataField="r06_parent" HeaderText="類別名稱" 
                            SortExpression="r06_parent">
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="r06_name" HeaderText="子類別名稱" 
                            SortExpression="r06_name">
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="r06_order" HeaderText="排序" 
                            SortExpression="r06_order">
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="r06_createuid" HeaderText="建立者" 
                            SortExpression="r06_createuid">
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="r06_createtime" HeaderText="建立時間" 
                            SortExpression="r06_createtime" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='' href='<%# string.Format("300603-3.aspx?mode=modify&modal=true&r06_no={0}&r05_no={1}&TB_iframe=true", Eval("r06_no"), Eval("r05_no"))%>'>
                                    <span><span>修改</span></span> </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="Button5" runat="server" CssClass="delete" CommandArgument="<%# Container.DataItemIndex %>"
                                    CommandName="del" OnClientClick=" return confirm('確定要刪除?')" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="bottom">
            <asp:Button ID="Button1" runat="server" Text="回維修項目管理" CssClass="a-input" 
                onclick="Button1_Click" />
           
        </div>
    </div>



</asp:Content>

