<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300201-2.aspx.cs" Inherits="_30_300200_300201_2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls" TagPrefix="cc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectCountMethod="GetAllCount"
        SelectMethod="GetAll" TypeName="NXEIP.DAO.ThemeDAO" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="que_no" QueryStringField="no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300201" />
    <div class="tableDiv">
      <table>
            <tbody>
                <tr>
                    <th style="width:120px">問卷名稱</th>
                    <td><asp:Label ID="lab_name" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>問卷說明</th>
                    <td><asp:Label ID="lab_descript" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>調查時間</th>
                    <td><asp:Label ID="lab_sdate" runat="server"></asp:Label> ～ <asp:Label ID="lab_edate" runat="server"></asp:Label></td>
                </tr>
            </tbody>
        </table>
        <br />
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">題目設定</div>
                <div class="function">
                    <input type="button" class="thickbox b-input" value="新增題目" runat="server" id="btn_insert" name="btn_insert" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    EmptyDataText="查無資料" DataKeyNames="que_no,the_no" OnRowDataBound="GridView1_RowDataBound"
                    GridLines="None" OnRowCommand="GridView1_RowCommand" 
                    EnableViewState="False" PageSize="2">
                    <Columns>
                        <asp:BoundField DataField="the_name" HeaderText="題目名稱" SortExpression="the_name" />
                        <asp:BoundField DataField="the_type" HeaderText="題目種類" SortExpression="the_type" />
                        <asp:BoundField DataField="the_count" HeaderText="是否計分" SortExpression="the_count" />
                        <asp:BoundField DataField="the_fraction" HeaderText="記分分數" SortExpression="the_fraction" />
                        <asp:BoundField DataField="the_order" HeaderText="題目順序" SortExpression="the_order" />
                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='<%# Eval("the_name", "修改{0}") %>'
                                    href='<%#String.Format("300201-3.aspx?modal=true&mode=modify&que_no={0}&the_no={1}&TB_iframe=true&height=450&width=800",Eval("que_no"),Eval("the_no")) %>'>
                                    <span>修改</span></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="btn_delete" runat="server" CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" CssClass="delete" OnClientClick=" return confirm('確定要刪除?')" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40" />
                        </asp:TemplateField>
                    </Columns>
                </cc1:GridView>
                <div class="footer">
                    <div class="f1"></div>
                    <div class="f2"></div>
                    <div class="f3"></div>
                </div>
                <div style="text-align: center;"><br>
                    <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="回上一頁" onclick="btn_submit_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
