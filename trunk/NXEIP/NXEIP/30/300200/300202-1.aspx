<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300202-1.aspx.cs" Inherits="_30_300200_300202_1" %>
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
        SelectMethod="GetAll" TypeName="NXEIP.DAO.ThemeDAO" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="que_no" QueryStringField="no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>" 
        SelectCommand="SELECT que_no, the_no, ans_no, ans_name FROM answers WHERE (que_no = @que_no) AND (the_no = @the_no) AND (ans_status = '1') ORDER BY ans_order">
        <SelectParameters>
            <asp:Parameter Name="que_no" />
            <asp:Parameter Name="the_no" />
        </SelectParameters>
    </asp:SqlDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300202" />
    <div class="tableDiv">
        <table>
            <tbody>
                <tr>
                    <th style="width: 120px">
                        問卷名稱
                    </th>
                    <td>
                        <asp:Label ID="lab_name" runat="server"></asp:Label><asp:Label ID="lab_no" runat="server"
                            Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        問卷說明
                    </th>
                    <td>
                        <asp:Label ID="lab_descript" runat="server"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <div class="select">
             <li> <span class="a-title">票數統計</span></li>
       </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="que_no,the_no"
                    DataSourceID="ObjectDataSource1" OnRowDataBound="GridView1_RowDataBound" ShowHeader="False"
                    Width="100%" CssClass="tableData" CellPadding="3" CellSpacing="3">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="inquire_count">
                                    <div class="box">
                                        <div class="b2">
                                            <li class="ps1">
                                                <asp:Label ID="lab_the_name" runat="server" Text='<%# Eval("the_name") %>'></asp:Label>
                                                <asp:Label ID="lab_the_type" runat="server" Text='<%# Eval("the_type") %>' Visible="False"></asp:Label></li>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <asp:GridView ID="GridView2" runat="server" DataKeyNames="que_no,the_no,ans_no" OnRowCommand="GridView2_RowCommand"
                                        OnRowDataBound="GridView2_RowDataBound" AutoGenerateColumns="False" CssClass="tableData"
                                        BorderWidth="1px" CellPadding="3" CellSpacing="3">
                                        <Columns>
                                            <asp:BoundField DataField="ans_name" HeaderText="選項" SortExpression="ans_name">
                                                <HeaderStyle Width="50%" HorizontalAlign="Left" BorderWidth="1px" />
                                                <ItemStyle BorderWidth="1px" Height="24" CssClass="b3 a-letter-t1" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="票數">
                                                <HeaderStyle Width="44%" />
                                                <ItemStyle BorderWidth="1px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="清單">
                                                <ItemTemplate>
                                                    <a id="btnAnswerList" runat="server" class="thickbox imageButton peruse" title='<%# Eval("ans_name", "清單{0}") %>'
                                                        href='<%#String.Format("300202-b.aspx?modal=true&mode=modify&que_no={0}&the_no={1}&ans_no={2}&TB_iframe=true&height=400&width=700",Eval("que_no"),Eval("the_no"),Eval("ans_no")) %>'>
                                                        <span>清單</span></a>
                                                </ItemTemplate>
                                                <HeaderStyle BorderWidth="1" />
                                                <ItemStyle HorizontalAlign="Center" Width="50" VerticalAlign="Bottom" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle Height="24px" BorderWidth="1px" />
                                    </asp:GridView>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="footer">
                    <div class="f1">
                    </div>
                    <div class="f2">
                    </div>
                    <div class="f3">
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="bottom">
            <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="回上一頁" 
                onclick="btn_submit_Click" />
        </div>
    </div>
</asp:Content>

