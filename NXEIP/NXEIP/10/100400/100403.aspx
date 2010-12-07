<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100403.aspx.cs" Inherits="_10_100400_100403" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
    <%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
    function update(msg) {
        __doPostBack('<%=UpdatePanel1.ClientID%>', '')
        tb_remove();
        alert(msg);
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

    jQuery(document).ready(function () {
        jQuery('.show').click(function () {
            jQuery('.show').removeClass("b-input2").addClass("b-input");
            jQuery(this).removeClass("b-input").addClass("b-input2");
        });
    });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager1">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetRep02Data"
        TypeName="NXEIP.DAO._100403DAO" EnablePaging="True" OldValuesParameterFormatString=""
        SelectCountMethod="GetRep02DataCount">
        <SelectParameters>
            <asp:Parameter Name="type" Type="String" />
            <asp:Parameter Name="sd" Type="DateTime" />
            <asp:Parameter Name="ed" Type="DateTime" />
            <asp:Parameter Name="peo_uid" Type="Int32" />
            <asp:Parameter Name="dep_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hidd_type" runat="server" />
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100403" />
    <div class="tableDiv">
        <div class="select" style="text-align: right">
            <div class="center0">
                叫修日期：</div>
            <div class="center1">
                起&nbsp;<uc2:calendar ID="calendar1" runat="server" _Show="false" />
            </div>
            <div class="center1">
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
                    <asp:Button ID="Button1" runat="server" Text="個人維修" CssClass="show b-input2" OnClick="Button1_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="單位維修" CssClass="show b-input" OnClick="Button2_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button3" runat="server" Text="全府維修" CssClass="show b-input" OnClick="Button3_Click" />
                    &nbsp;&nbsp;
                    <input type="button" class="thickbox b-input" alt="100403-1.aspx?modal=true&TB_iframe=true"
                        value="我要叫修" />
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
                    OnRowCommand="GridView1_RowCommand" DataKeyNames="r02_no" OnDataBound="GridView1_DataBound">
                    <Columns>
                        <asp:BoundField DataField="r02_depno" HeaderText="叫修單位" SortExpression="r02_depno">
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="peo_uid" HeaderText="叫修人員(分機)" SortExpression="peo_uid">
                            <ItemStyle Width="13%" />
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
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="Button3" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                    CommandName="del" OnClientClick=" return confirm('確定要刪除?')" CssClass="delete" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="評分">
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='' href='<%# Eval("r02_no", "100403-2.aspx?modal=true&r02_no={0}&TB_iframe=true") %>'>
                                    <span><span>評分</span></span> </a>
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
                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="10">
                        <Fields>
                            <NXEIP:GooglePagerField />
                        </Fields>
                    </asp:DataPager>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

