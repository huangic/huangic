<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300404.aspx.cs" Inherits="_30_300400_300404" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls" TagPrefix="cc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/tree/DepartTreeListBox.ascx" tagname="DepartTreeListBox" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" SelectCountMethod="GetAllCount"
        SelectMethod="GetAll" TypeName="NXEIP.DAO.CheckerDAO" OldValuesParameterFormatString="original_{0}">
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>"
        SelectCommand="SELECT spo_no, spo_name FROM spot WHERE (spo_function LIKE '____1%') AND (spo_status = '1') ORDER BY spo_no">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>"
        SelectCommand="SELECT roo_no, roo_name FROM rooms WHERE (roo_status = '1') AND (spo_no = @spo_no) ORDER BY roo_floor, roo_name">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="spo_no" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Label ID="lab_pageIndex" runat="server" Visible="False"></asp:Label>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300404" />
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="List" runat="server">
            <div class="tableDiv">
                <div class="header">
                    <div class="h1">
                    </div>
                    <div class="h2">
                        <div class="function">
                            <asp:Button ID="btn_add" runat="server" Text="新增審核人" CssClass="b-input" OnClick="btn_add_Click" />
                        </div>
                    </div>
                    <div class="h3">
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                            AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                            EmptyDataText="查無資料" DataKeyNames="che_no" OnRowDataBound="GridView1_RowDataBound"
                            GridLines="None" OnRowCommand="GridView1_RowCommand"><Columns><asp:TemplateField HeaderText="所在地"></asp:TemplateField><asp:BoundField DataField="roo_no" HeaderText="場地名稱" SortExpression="roo_no" /><asp:BoundField DataField="che_peouid" HeaderText="審核人" SortExpression="che_peouid" /><asp:TemplateField HeaderText="刪除"><ItemTemplate><asp:Button ID="Button3" runat="server" CommandName="del" CommandArgument="<%# Container.DataItemIndex %>" CssClass="delete" OnClientClick=" return confirm('確定要刪除?')" /></ItemTemplate><HeaderStyle HorizontalAlign="Center" /><ItemStyle HorizontalAlign="Center" Width="40" /></asp:TemplateField></Columns></cc1:GridView>
                        <div class="footer">
                            <div class="f1"></div>
                            <div class="f2"></div>
                            <div class="f3"></div>
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
            </div>
        </asp:View>
        <asp:View ID="Insert" runat="server">
        <div class="tableDiv">
              <div class="header">
                  <div class="h1"></div>
                  <div class="h2"></div>
                  <div class="h3"></div>
              </div>
              <table>
                  <tr>
                    <th>場地名稱</th>
                    <td>
                        <asp:DropDownList ID="ddl_spot" runat="server" CssClass="select4" 
                            AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="spo_name" DataValueField="spo_no"
                            AppendDataBoundItems="True" 
                            onselectedindexchanged="ddl_spot_SelectedIndexChanged">
                            <asp:ListItem Value="0">請選擇</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddl_rooms" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource2"
                            DataTextField="roo_name" DataValueField="roo_no">
                            <asp:ListItem Value="0">請選擇</asp:ListItem>
                        </asp:DropDownList>
                  </td>
                </tr>
                <tr>
                    <th>審核人</th>
                    <td>
                        <uc2:DepartTreeListBox ID="DepartTreeListBox1" runat="server" LeafType="People" PeopleType="All" />
                    </td>
                </tr>
              </table>
              <div class="footer">
                  <div class="f1"></div>
                  <div class="f2"></div>
                  <div class="f3"></div>
              </div>
              <div class="bottom">
                  <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="確定" OnClick="btn_submit_Click" />
                  &nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClick="btn_cancel_Click" />
              </div>
          </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
