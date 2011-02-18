<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300202-2.aspx.cs" Inherits="_30_300200_300202_2" %>
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
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectCountMethod="GetAllCount" SelectMethod="GetAll" TypeName="NXEIP.DAO.BotanizeDAO" OldValuesParameterFormatString="original_{0}" EnablePaging="True">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="que_no" QueryStringField="no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300202" />
    <div class="tableDiv">
        <table>
            <tbody>
                <tr>
                    <th style="width: 120px">
                        問卷名稱
                    </th>
                    <td>
                        <asp:Label ID="lab_name" runat="server"></asp:Label>
                        <asp:Label ID="lab_no" 
                            runat="server" Visible="False"></asp:Label>
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
                <div class="name">填寫問卷者</div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    EmptyDataText="查無資料" DataKeyNames="bot_no" OnRowDataBound="GridView1_RowDataBound"
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="dep_name" HeaderText="單位" SortExpression="dep_name" />
                        <asp:BoundField DataField="pro_name" HeaderText="職稱" SortExpression="pro_name" />
                        <asp:BoundField DataField="peo_name" HeaderText="姓名" SortExpression="peo_name"></asp:BoundField>
                        <asp:BoundField DataField="bot_date" HeaderText="填寫日期" 
                            SortExpression="bot_date" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="查看">
                            <ItemTemplate>
                                <a id="btnShow" runat="server" class="thickbox imageButton peruse" title='<%# Eval("peo_name", "查看{0}") %>'
                                    href='<%#String.Format("300202-c.aspx?modal=true&mode=modify&bot_no={0}&TB_iframe=true&height=500&width=700",Eval("bot_no")) %>'>
                                    <span>查看</span></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="60" />
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
        </asp:UpdatePanel>
        <div class="bottom">
            <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="回上一頁" onclick="btn_submit_Click" />
        </div>
    </div>
</asp:Content>