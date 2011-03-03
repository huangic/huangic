<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100204.aspx.cs" Inherits="_10_100200_100204" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
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
        TypeName="NXEIP.DAO.New01DAO" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="GetDataCount">
        <SelectParameters>
            <asp:Parameter Name="use" Type="String" />
            <asp:Parameter Name="key" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="GetPeoDataCount" SelectMethod="GetPeoData" TypeName="NXEIP.DAO.New01DAO">
        <SelectParameters>
            <asp:Parameter Name="peo_uid" Type="Int32" />
            <asp:Parameter Name="status" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hidd_use" runat="server" />
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100204" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <div class="select" >
                <span class="a-letter-2">
                    查詢最新消息：<asp:TextBox ID="tbox_search" runat="server"></asp:TextBox>
                    <span class="a-letter-1">
                        <asp:Button ID="Button1" runat="server" Text="查詢" CssClass="b-input" OnClick="Button1_Click" />
                    </span>
                </span>
                <span class="a-letter-2">
                    <asp:Button ID="Button5" runat="server" Text="全府RSS訂閱" CssClass="b-input" Visible="False" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button6" runat="server" Text="單位RSS訂閱" CssClass="b-input" Visible="False" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button7" runat="server" Text="全府最新消息" CssClass="b-input" OnClick="Button7_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button8" runat="server" Text="單位最新消息" CssClass="b-input" OnClick="Button8_Click" />
                </span>
                
            </div>
            
            <div class="select-3">
                <asp:ListView ID="ListView1" runat="server" DataKeyNames="n01_no" DataSourceID="ObjectDataSource1">
                    <ItemTemplate>
                        <li class="ins"><a title="查看內容" class="thickbox a-letter-s3" href='<%# Eval("n01_no", "100204-2.aspx?modal=true&n01_no={0}&TB_iframe=true") %>'>
                            <%# string.Format("{0} ({1})",Eval("n01_subject"),new ChangeObject()._ADtoROC((DateTime)Eval("n01_date"))) %>
                        </a></li>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <div class="pager">
                <asp:DataPager ID="DataPager2" runat="server" PagedControlID="ListView1" PageSize="25">
                    <Fields>
                        <NXEIP:GooglePagerField />
                    </Fields>
                </asp:DataPager>
            </div>
            <br />
            <div class="tableDiv">
                <div class="header">
                    <div class="h1">
                    </div>
                    <div class="h2">
                        <div class="function">
                            審核狀態查詢：<asp:DropDownList ID="ddl_status" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_status_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="1">通過</asp:ListItem>
                                <asp:ListItem Value="3">送審中</asp:ListItem>
                                <asp:ListItem Value="2">未通過</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Button2" runat="server" Text="新增最新消息" CssClass="b-input" OnClick="Button2_Click" />
                        </div>
                    </div>
                    <div class="h3">
                    </div>
                </div>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource2" AllowPaging="True"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    EmptyDataText="查無資料" OnRowDataBound="GridView1_RowDataBound" GridLines="None"
                    OnRowCommand="GridView1_RowCommand" EnableViewState="False" DataKeyNames="n01_no">
                    <Columns>
                        <asp:TemplateField HeaderText="選取">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="n01_subject" HeaderText="消息主旨" SortExpression="n01_subject" />
                        <asp:BoundField DataField="n01_status" HeaderText="審核狀態" SortExpression="n01_status">
                            <ItemStyle Width="7%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="n01_checkuid" HeaderText="審核人員" SortExpression="n01_checkuid">
                            <ItemStyle Width="7%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="n01_checkdate" HeaderText="審核時間" SortExpression="n01_checkdate"
                            DataFormatString="{0:yyyy-MM-dd HH:mm}">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="n01_reason" HeaderText="退回原因" SortExpression="n01_reason">
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                <asp:Button ID="Button4" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                    CommandName="edit" CssClass="edit" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="Button3" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                    CommandName="del" OnClientClick=" return confirm('確定要刪除?')" CssClass="delete" />
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
                <div class="bottom">
                    <asp:Button ID="Button9" runat="server" Text="刪除" OnClientClick=" return confirm('確定要刪除?')" CssClass="b-input" 
                        onclick="Button9_Click" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

