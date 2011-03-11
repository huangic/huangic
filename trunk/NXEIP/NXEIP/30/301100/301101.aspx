<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="301101.aspx.cs" Inherits="_30_301100_301101" %>
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
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" SelectCountMethod="GetAllCount"
        SelectMethod="GetAll" TypeName="NXEIP.DAO.M01DAO" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="number" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="301101" />
    <div class="tableDiv">
      <table>
            <tr>
                <td valign="bottom">
                    屬性類別：<asp:RadioButtonList ID="rbl_number" runat="server" 
                        RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True" 
                        onselectedindexchanged="rbl_number_SelectedIndexChanged">
                        <asp:ListItem Value="platoon">排照種類</asp:ListItem>
                        <asp:ListItem Value="chekuan">車別</asp:ListItem>
                        <asp:ListItem Value="mark">車輛廠牌</asp:ListItem>
                        <asp:ListItem Value="color">車輛顏色</asp:ListItem>
                        <asp:ListItem Value="source">來源</asp:ListItem>
                        <asp:ListItem Value="factory">廠商</asp:ListItem>
                        <asp:ListItem Value="energy">能源種類</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <div class="bottom">
        </div>
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name"><asp:Label ID="lab_number" runat="server"></asp:Label></div>
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="301101-1.aspx?number=<%=rbl_number.SelectedValue%>&height=300&width=700&TB_iframe=true&modal=true" value="新增屬性" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    EmptyDataText="查無資料" DataKeyNames="m01_no" OnRowDataBound="GridView1_RowDataBound"
                    GridLines="None" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="m01_number" HeaderText="屬性類別" SortExpression="m01_number" />
                        <asp:BoundField DataField="m01_code" HeaderText="屬性編碼" SortExpression="m01_code" />
                        <asp:BoundField DataField="m01_name" HeaderText="屬性名稱" SortExpression="m01_name" />
                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='<%# Eval("m01_name", "修改{0}") %>'
                                    href='<%# Eval("m01_no", "301101-1.aspx?modal=true&mode=modify&no={0}&TB_iframe=true&height=300&width=700") %>'>
                                    <span>修改</span></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="btn_delete" runat="server" CommandName="del" CommandArgument="<%# Container.DataItemIndex %>"
                                    CssClass="delete" OnClientClick="return confirm('確定要刪除?')" />
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

