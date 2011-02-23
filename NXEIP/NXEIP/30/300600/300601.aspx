<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300601.aspx.cs" Inherits="_30_300600_300601" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>

<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc2" %>

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
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetRep02Data2"
        TypeName="NXEIP.DAO._100403DAO" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="GetRep02DataCount2">
        <SelectParameters>
            <asp:Parameter Name="r05_no" Type="Int32" />
            <asp:Parameter Name="sd" Type="DateTime" />
            <asp:Parameter Name="ed" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="SearchRep05Root"
        TypeName="NXEIP.DAO._100403DAO" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="peo_uid" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hidd_r05no" runat="server" />
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300601" />
    <div class="tableDiv">
        <div class="select-1">
            <asp:ListView ID="lv_cat" runat="server" DataSourceID="ObjectDataSource2" DataKeyNames="r05_no"
                OnItemCommand="lv_cat_ItemCommand">
                <ItemTemplate>
                    <span>
                        <asp:LinkButton ID="lb_cat" runat="server" CssClass='<%# hidd_r05no.Value.Equals( Eval("r05_no").ToString()) ? "a-letter-s1":""  %>'
                            CommandName="click_cat" CommandArgument="<%# Container.DataItemIndex %>"><%# Eval("r05_name") %></asp:LinkButton></span>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="select" style="text-align: right">
            <div class="center1">
                叫修日期：</div>
            <div class="center2">
                起&nbsp;<uc2:calendar ID="calendar1" runat="server" _Show="false" />
            </div>
            <div class="center2">
                迄&nbsp;<uc2:calendar ID="calendar2" runat="server" _Show="False" />
            </div>
            <div class="b5">
                <asp:Button ID="Button4" runat="server" Text="查詢" CssClass="b-input" OnClick="Button4_Click" />
            </div>
        </div>
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <asp:Button ID="Button1" runat="server" Text="下載Excel" CssClass="b-input" OnClick="Button1_Click" />
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
                    OnRowCommand="GridView1_RowCommand" DataKeyNames="r02_no">
                    <Columns>
                        <asp:BoundField DataField="r02_depno" HeaderText="叫修單位" SortExpression="r02_depno">
                            <ItemStyle Width="13%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="peo_uid" HeaderText="叫修人員(分機)" SortExpression="peo_uid">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="r02_date" HeaderText="叫修日期" SortExpression="r02_date">
                            <ItemStyle Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="r02_reason" HeaderText="故障原因" SortExpression="r02_reason">
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="r02_reply" HeaderText="處理狀況" SortExpression="r02_reply">
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='' href='<%# Eval("r02_no", "300601-1.aspx?modal=true&r02_no={0}&TB_iframe=true") %>'>
                                    <span><span>修改</span></span> </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="回覆">
                            <ItemTemplate>
                                <a id="btnShowPopup2" runat="server" class="thickbox imageButton edit" title='' href='<%# Eval("r02_no", "300601-2.aspx?modal=true&r02_no={0}&TB_iframe=true") %>'>
                                    <span><span>回覆</span></span> </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="Button5" runat="server" CssClass="delete" CommandArgument="<%# Container.DataItemIndex %>" CommandName="del" OnClientClick=" return confirm('確定要刪除?')" />
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
    </div>
    <div id="div_chart1" runat="server">
        <asp:Chart ID="Chart1" runat="server">
            <Series>
                <asp:Series ChartArea="ChartArea1" Name="Series1" Label="#VALY">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
</asp:Content>

