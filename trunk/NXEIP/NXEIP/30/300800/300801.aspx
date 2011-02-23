<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300801.aspx.cs" Inherits="_30_300800_300801" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc2" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function update(msg) {
        __doPostBack('<%=UpdatePanel1.ClientID%>', '');
        tb_remove();
        alert(msg);
    }

    function update2() {
        __doPostBack('<%=UpdatePanel1.ClientID%>', '');
        tb_remove();
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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
        OldValuesParameterFormatString="original_{0}" SelectCountMethod="Get_CheckDataCount" 
        SelectMethod="Get_CheckData" TypeName="NXEIP.DAO.New01DAO">
        <SelectParameters>
            <asp:Parameter Name="dep_no" Type="Int32" />
            <asp:Parameter Name="sd" Type="DateTime" />
            <asp:Parameter Name="ed" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="300801" />

    <div class="select">
        <span class="a-letter-2">
        <span class="a-letter-1">
            發布時間：起<uc1:calendar ID="calendar1" runat="server" _Show="False" />
            &nbsp;訖<uc1:calendar ID="calendar2" runat="server" _Show="False" />
        </span>
            <asp:Button ID="Button1" runat="server" Text="搜尋" CssClass="show b-input2" CausesValidation="False"
                OnClick="Button1_Click" />
        </span>
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
                    GridLines="None" 
                    DataKeyNames="n01_no,s06_no" 
                    onrowdatabound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="n01_subject" HeaderText="最新消息標題" 
                            SortExpression="n01_subject">
                            <ItemStyle Width="40%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="n01_peouid" HeaderText="發布人" 
                            SortExpression="n01_peouid">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="n01_date" HeaderText="發布日期" 
                            SortExpression="n01_date">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="n01_status" HeaderText="審核狀態" 
                            SortExpression="n01_status">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="n01_reason" HeaderText="審核事由" 
                            SortExpression="n01_reason">
                        <ItemStyle Width="25%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="核審">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="thickbox imageButton edit"
                                   NavigateUrl='<%# string.Format("300801-1.aspx?n01_no={0}&modal=true&TB_iframe=true",Eval("n01_no"))%>'><span>回覆</span></asp:HyperLink>
                            </ItemTemplate>
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

</asp:Content>

