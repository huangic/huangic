<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300504.aspx.cs" Inherits="_30_300500_300504" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls" TagPrefix="cc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <script type="text/javascript">
      function update(msg) {
          __doPostBack('<%=UpdatePanel1.ClientID%>', '');
          tb_remove();

          if (msg) {
              alert(msg);
          }
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
        SelectMethod="GetAll" TypeName="NXEIP.DAO.DispatchDAO" 
        OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300504" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">
                </div>
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="300504-1.aspx?height=450&width=800&TB_iframe=true&modal=true"
                        value="新增記錄" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    EmptyDataText="查無資料" DataKeyNames="dis_no" OnRowDataBound="GridView1_RowDataBound"
                    GridLines="None" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="dis_name" HeaderText="電腦名稱" SortExpression="dis_name" />
                        <asp:BoundField DataField="dis_memo" HeaderText="移送記錄" SortExpression="dis_memo" />
                        <asp:BoundField DataField="dis_outpeouid" HeaderText="移出人" SortExpression="dis_outpeouid" />
                        <asp:BoundField DataField="dis_outdate" HeaderText="移出日期" SortExpression="dis_outdate"
                            DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="dis_inpeouid" HeaderText="移入人" SortExpression="dis_inpeouid" />
                        <asp:BoundField DataField="dis_indate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="移入日期"
                            SortExpression="dis_indate" />
                        <asp:BoundField DataField="dis_change" HeaderText="異動狀況" SortExpression="dis_change" />
                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='<%# Eval("dis_name", "修改{0}") %>'
                                    href='<%# Eval("dis_no", "300504-1.aspx?modal=true&mode=modify&no={0}&TB_iframe=true&height=450&width=800") %>'>
                                    <span>修改</span></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="40" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="btn_delete" runat="server" CommandName="del" CommandArgument="<%# Container.DataItemIndex %>"
                                    CssClass="delete" OnClientClick=" return confirm('確定要刪除?')" />
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