<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100601.aspx.cs" Inherits="_10_100600_100601" %>

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

    function Redir(msg) {
        alert(msg);
        tb_remove();
        window.location.href = '../../10/100400/100402.aspx';
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
        OldValuesParameterFormatString="original_{0}" SelectCountMethod="GetDataCount" 
        SelectMethod="GetData" TypeName="NXEIP.DAO._100601DAO">
        <SelectParameters>
            <asp:Parameter Name="key" Type="String" />
            <asp:Parameter Name="sdate" Type="DateTime" />
            <asp:Parameter Name="edate" Type="DateTime" />
            <asp:Parameter Name="status" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="100601" />

    <div class="select">
        <span class="a-letter-2">
        <span class="a-letter-1">
            開會事由：<asp:TextBox ID="tbox_reason" runat="server"></asp:TextBox>
            開會時間：起<uc1:calendar ID="calendar1" runat="server" _Show="False" />
            &nbsp;訖<uc1:calendar ID="calendar2" runat="server" _Show="False" />
            &nbsp;狀態：
            <asp:DropDownList ID="ddl_status" runat="server">
                <asp:ListItem Value="1" Selected="True">會議成立</asp:ListItem>
                <asp:ListItem Value="2">會議取消</asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;
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
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="100601-1.aspx?modal=true&TB_iframe=true&height=580&width=720"
                        value="新增會議" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource1" EmptyDataText="查無資料"
                    GridLines="None" OnRowCommand="GridView1_RowCommand" 
                    DataKeyNames="mee_no,mee_sdate,mee_edate,mee_peouid,mee_status" 
                    onrowdatabound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="開會事由">
                            <ItemStyle Width="20%" />
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" CssClass="thickbox" Text='<%#Eval("mee_reason") %>'
                                   NavigateUrl='<%# string.Format("100601-5.aspx?mee_no={0}&modal=true&TB_iframe=true",Eval("mee_no"))%>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="mee_place" HeaderText="開會地點" SortExpression="mee_place">
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mee_sdate" HeaderText="開會時間" SortExpression="mee_sdate">
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mee_host" HeaderText="主持人" SortExpression="mee_host">
                            <ItemStyle Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mee_peouid" HeaderText="聯絡人" SortExpression="mee_peouid">
                            <ItemStyle Width="7%" />
                        </asp:BoundField>
                        
                        <asp:TemplateField HeaderText="紀錄上傳">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="thickbox imageButton edit"
                                   Visible='<%# GetFileVisible((int)Eval("mee_peouid"),(String)Eval("mee_status"))%>' NavigateUrl='<%# string.Format("100601-3.aspx?mee_no={0}&modal=true&TB_iframe=true",Eval("mee_no"))%>'><span>回覆</span></asp:HyperLink>
                                <asp:Label ID="lab_ConferenFile" runat="server" Text='<%# GetConferenFile((int)Eval("mee_no"),(DateTime)Eval("mee_edate"),(String)Eval("mee_status"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="出席回覆">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="thickbox imageButton edit"
                                   Visible='<%# GetStatusVisible((int)Eval("mee_no"),(String)Eval("mee_status"))%>' NavigateUrl='<%# string.Format("100601-2.aspx?mee_no={0}&modal=true&TB_iframe=true",Eval("mee_no"))%>'><span>回覆</span></asp:HyperLink>
                                <asp:Label ID="lab_status" runat="server" Text='<%# GetAttendsStatus((int)Eval("mee_no"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="取消">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass=" imageButton delete" CommandName="del" CommandArgument="<%# Container.DataItemIndex %>"
                                   Visible='<%# GetModifyVisible((int)Eval("mee_peouid"))%>' OnClientClick="return confirm('確定要刪除?')"><span>刪除</span></asp:LinkButton>
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

