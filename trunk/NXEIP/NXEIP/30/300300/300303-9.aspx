<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300303-9.aspx.cs" Inherits="_30_300300_300303_9" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function update(msg) {
        tb_remove();
        __doPostBack('<%=UpdatePanel1.ClientID%>', '');
        //alert(msg);
    }

    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {
            //  reapply the thick box stuff
            tb_init('a.thickbox');
        }
    }
    
    </script>
<style type="text/css">
.title1
{
    width:15%;
    text-align:right}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ODS_1" runat="server" SelectMethod="GetDoc10ByS06NO" TypeName="NXEIP.DAO.Doc10DAO"
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="s06_no" Type="Int32" />
            <asp:Parameter Name="peo_uid" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_2" runat="server" SelectMethod="GetDataBye02no" TypeName="NXEIP.DAO.e05DAO"
        OldValuesParameterFormatString="original_{0}" EnablePaging="True" 
        SelectCountMethod="GetDataBye02noCount">
        <SelectParameters>
            <asp:Parameter Name="e02_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ods_level1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetS06_Level1" TypeName="NXEIP.DAO.Sys06DAO"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ods_level2" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetS06_Level2" TypeName="NXEIP.DAO.Sys06DAO">
        <SelectParameters>
            <asp:Parameter Name="s06_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hidd_no" runat="server" />
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300303" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="../../20/200100/200107-2.aspx?cat_no=<%=Get_CatNO() %>&modal=true&model=new&TB_iframe=true" value="上傳講義" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table id="table1">
                    <tr>
                        <th class="title1">
                            學習機構
                        </th>
                        <td>
                            <asp:Label ID="lab_mechani" runat="server"></asp:Label>
                        </td>
                        <th class="title1">
                            課程代碼
                        </th>
                        <td>
                            <asp:Label ID="lab_code" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th class="title1">
                            課程類別
                        </th>
                        <td>
                            <asp:Label ID="lab_typ_name" runat="server"></asp:Label>
                        </td>
                        <th class="title1">
                            課程名稱(期別)
                        </th>
                        <td>
                            <asp:Label ID="lab_name_flag" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th class="title1">
                            課程所屬講義
                        </th>
                        <td colspan="3">
                            <cc1:GridView ID="GridView2" runat="server" DataSourceID="ODS_2" AutoGenerateColumns="False"
                                Width="100%" CellPadding="3" CellSpacing="3" GridLines="None"
                                EmptyDataText="目前無資料" DataKeyNames="e05_no,e05_d09no,e05_d10no" 
                                AllowPaging="True" OnRowCommand="GridView2_RowCommand"
                                PageSize="5" onrowdatabound="GridView2_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="e05_no" HeaderText="檔案名稱" 
                                        SortExpression="e05_no" />
                                    <asp:TemplateField HeaderText="刪除">
                                        <ItemTemplate>
                                            <asp:Button ID="Button1" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="del" OnClientClick=" return confirm('確定要刪除?')" CssClass="delete" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                    </asp:TemplateField>
                                </Columns>
                            </cc1:GridView>
                            <div class="pager">
                                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView2" PageSize="5">
                                    <Fields>
                                        <NXEIP:GooglePagerField />
                                    </Fields>
                                </asp:DataPager>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <th class="title1">
                            檔案類別
                        </th>
                        <td colspan="3">
                            &nbsp;<asp:DropDownList ID="ddl_level_1" runat="server" AppendDataBoundItems="True"
                                AutoPostBack="True" DataSourceID="ods_level1" DataTextField="s06_name" DataValueField="s06_no"
                                OnSelectedIndexChanged="ddl_level_1_SelectedIndexChanged">
                                <asp:ListItem Value="0">請選擇</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;檔案子類別&nbsp;<asp:DropDownList ID="ddl_level_2" runat="server" AppendDataBoundItems="True"
                                AutoPostBack="True" DataSourceID="ods_level2" DataTextField="s06_name" DataValueField="s06_no"
                                OnSelectedIndexChanged="ddl_level_2_SelectedIndexChanged">
                                <asp:ListItem Value="0">請選擇</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th class="title1">
                            講義選取
                        </th>
                        <td colspan="3">
                            <cc1:GridView ID="GridView1" runat="server" DataSourceID="ODS_1" AutoGenerateColumns="False"
                                Width="100%" CellPadding="3" CellSpacing="3" GridLines="None" EnableViewState="False"
                                EmptyDataText="目前無資料" DataKeyNames="d09_no,d10_no">
                                <Columns>
                                    <asp:TemplateField HeaderText="選取">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbox" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="d10_file" HeaderText="檔案" SortExpression="d10_file" />
                                </Columns>
                            </cc1:GridView>
                            <div class="bottom">
                                <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="確定" OnClick="btn_submit_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="bottom">
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="回上一頁" OnClick="btn_cancel_Click" />
        </div>
    </div>
</asp:Content>

