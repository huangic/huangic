<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300303-4.aspx.cs" Inherits="_30_300300_300303_4" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function update(msg) {
        tb_remove();

        $('#<%=hidd_reason.ClientID%>').val(msg);
        
        var str = chang('<%=LinkButton1.ClientID%>');
        __doPostBack(str, '')
    }

    function chang(str) {
        //將底線換成$符號
        var regex=/\_/g;
        return str.replace(regex, '$');
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" SelectCountMethod="Get_e04DataCount"
        SelectMethod="Get_e04Data" TypeName="NXEIP.DAO._300303DAO" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="e02_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hidd_no" runat="server" />
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300303" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">
                   <asp:Label ID="lab_titile" runat="server"></asp:Label>
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    EmptyDataText="查無資料" DataKeyNames="e04_no" OnRowDataBound="GridView1_RowDataBound"
                    GridLines="None" OnRowCommand="GridView1_RowCommand"
                    OnDataBound="GridView1_DataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="選取">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbox" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="e04_depno" HeaderText="單位" SortExpression="e04_depno" />
                        <asp:BoundField DataField="e04_prono" HeaderText="職稱" SortExpression="e04_prono" />
                        <asp:BoundField DataField="e04_peouid" HeaderText="姓名" SortExpression="e04_peouid" />
                        <asp:BoundField DataField="e04_applydate" HeaderText="報名日期" SortExpression="e04_applydate"
                            DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="e04_checkdate" HeaderText="審核日期" SortExpression="e04_checkdate"
                            DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="e04_check" HeaderText="審核狀況" SortExpression="e04_check" />
                        <asp:BoundField DataField="e04_reason" HeaderText="未核可原因" SortExpression="e04_reason">
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="回覆未審核">
                            <ItemTemplate>
                                <asp:Button ID="Button3" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                    CommandName="del" OnClientClick=" return confirm('確定要變更為未審核狀態?')" CssClass="delete" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="9%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="取消報名">
                            <ItemTemplate>
                                <asp:Button ID="Button4" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                    CommandName="del2" OnClientClick=" return confirm('確定要取消報名?')" CssClass="delete" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="9%" />
                        </asp:TemplateField>
                    </Columns>
                </cc1:GridView>
                <div class="pager">
                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="10">
                        <Fields>
                            <NXEIP:GooglePagerField />
                        </Fields>
                    </asp:DataPager>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="bottom">
            <asp:Button ID="btn_pass" runat="server" CssClass="b-input" Text="核可報名" 
                onclick="btn_pass_Click"  />
            &nbsp;
            <input id="Button1" type="button" value="報名未核可" class="thickbox b-input" alt="../../lib/Reason.aspx?uid=&modal=true&TB_iframe=true&height=350&width=450" />
            &nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="回管理課程" OnClick="btn_cancel_Click" />
            &nbsp;
            <asp:Button ID="btn_cancel2" runat="server" CssClass="a-input" Text="回課程檢視" OnClick="btn_cancel2_Click" />
            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click1"></asp:LinkButton>
        </div>
    </div>
    <asp:HiddenField ID="hidd_reason" runat="server" />
</asp:Content>

