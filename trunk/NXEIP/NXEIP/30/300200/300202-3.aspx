<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300202-3.aspx.cs" Inherits="_30_300200_300202_3" %>
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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>" 
        SelectCommand="SELECT que_no, the_no, ans_no, ans_name FROM answers WHERE (que_no = @que_no) AND (the_no = @the_no) AND (ans_status = '1') ORDER BY ans_order">
        <SelectParameters>
            <asp:Parameter Name="que_no" />
            <asp:Parameter Name="the_no" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>" 
        SelectCommand="SELECT que_no, the_no, the_name, the_type FROM theme WHERE (que_no = @que_no) AND (the_type &lt;&gt; '3') AND (the_status = '1') ORDER BY the_order">
        <SelectParameters>
            <asp:QueryStringParameter Name="que_no" QueryStringField="no" />
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
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">票數統計</div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="que_no,the_no"
                    DataSourceID="SqlDataSource2" OnRowDataBound="GridView1_RowDataBound" ShowHeader="False"
                    Width="100%" CssClass="tableData" CellPadding="3" CellSpacing="3">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div style="height: 30px; line-height: 30px;">
                                    <span class="icon">&nbsp;</span>
                                    <asp:Label ID="lab_the_name" runat="server" Text='<%# Eval("the_name") %>'></asp:Label>
                                    <asp:Label ID="lab_the_type" runat="server" Text='<%# Eval("the_type") %>' Visible="False"></asp:Label>
                                </div>
                                <div class="">
                                    <asp:Chart ID="Chart1" runat="server">
                                        <Series>
                                            <asp:Series ChartType="Pie" Name="Series1">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1">
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
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

