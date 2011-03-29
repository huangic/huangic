<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300704.aspx.cs" Inherits="_30_300700_300704" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc2" %>

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
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="Get_DataCount" SelectMethod="Get_Data" TypeName="NXEIP.DAO.ApplysDAO">
        <SelectParameters>
            <asp:Parameter Name="type" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="300704" />
    <div class="select">
        <span class="a-letter-2"><span class="a-letter-1">審核狀態：<asp:DropDownList ID="DropDownList1"
            runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            <asp:ListItem Value="1">未審核</asp:ListItem>
            <asp:ListItem Value="2">已審核</asp:ListItem>
        </asp:DropDownList>
        </span></span>
    </div>
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource1" EmptyDataText="查無資料"
                    GridLines="None" DataKeyNames="app_no" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="選取">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                                <asp:CheckBox ID="cbox_1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="app_depno" HeaderText="單位" SortExpression="app_depno">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="app_profess" HeaderText="職稱" SortExpression="app_profess">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="app_name" HeaderText="姓名" SortExpression="app_name">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="app_officetel" HeaderText="公務電話" SortExpression="app_officetel">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="app_date" HeaderText="申請日期" SortExpression="app_date"
                            DataFormatString="{0:yyyy-MM-dd HH:mm}">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="app_check" HeaderText="審核狀態" SortExpression="app_check">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="app_checkuid" HeaderText="審核人員" SortExpression="app_checkuid">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="app_checkdate" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                            HeaderText="審核日期" SortExpression="app_checkdate">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="檢視" Visible="False">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="thickbox imageButton edit"
                                    NavigateUrl='<%# string.Format("300704-1.aspx?app_no={0}&modal=true&TB_iframe=true",Eval("app_no"))%>'><span>回覆</span></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
            </Triggers>
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
            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="25">
                <Fields>
                    <NXEIP:GooglePagerField />
                </Fields>
            </asp:DataPager>
        </div>
        <div class="bottom">
            <asp:Button ID="Button1" runat="server" Text="同意" CssClass="b-input" OnClick="Button1_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Text="不同意" CssClass="b-input" OnClick="Button2_Click" />
        </div>
    </div>
</asp:Content>

