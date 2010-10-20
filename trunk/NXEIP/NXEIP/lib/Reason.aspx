<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reason.aspx.cs" Inherits="lib_Reason" %>

<%@ Register src="CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" SelectCountMethod="GetAllCount"
        SelectMethod="GetAll" TypeName="NXEIP.DAO.TermsDAO" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="uid" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
                
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tr>
                <th style="width:15%">
                    簽核意見
                </th>
                <td>
                    <asp:TextBox ID="tbox_reason" runat="server" Height="60px" TextMode="MultiLine" 
                        Width="380px" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <div id="div_show" runat="server">
            <cc1:GridView ID="GridView1" runat="server" AllowPaging="True"
                AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                EmptyDataText="查無資料"
                GridLines="None" OnRowCommand="GridView1_RowCommand" 
                EnableViewState="False" DataSourceID="ObjectDataSource1" PageSize="5" 
                Width="100%">
                <Columns>
                    <asp:BoundField DataField="ter_name" HeaderText="個人詞庫" SortExpression="ter_name" />
                    <asp:TemplateField HeaderText="加入">
                        <ItemTemplate>
                            <asp:Button ID="Button4" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                CommandName="sel" CssClass="edit" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                </Columns>
            </cc1:GridView>
            <div class="pager">
            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" 
                    PageSize="5">
                <Fields>
                    <asp:NextPreviousPagerField ShowNextPageButton="False" />
                    <asp:NumericPagerField />
                    <asp:NextPreviousPagerField ShowPreviousPageButton="False" />
                </Fields>
            </asp:DataPager>
        </div>
        </div>
        <div class="bottom">
            <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="個人詞庫" 
                onclick="Button1_Click" Visible="False" />
            &nbsp;&nbsp;
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" OnClick="btn_ok_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" />
        </div>
    </div>
    <asp:HiddenField ID="hidd_no" runat="server" />
    </form>
</body>
</html>
