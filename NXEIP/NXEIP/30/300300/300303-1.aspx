<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300303-1.aspx.cs" Inherits="_30_300300_300303_1" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/tree/DepartTreeListBox.ascx" tagname="DepartTreeListBox" tagprefix="uc2" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript">
        $(function () {
            //須與form表單ID名稱相同
            $("#ContentPlaceHolder1").validate();
        });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="Get_Data" 
        TypeName="NXEIP.DAO.e03DAO">
        <SelectParameters>
            <asp:Parameter Name="e02_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hidd_no" runat="server" />
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300303" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                
            </div>
            <div class="h3">
            </div>
        </div>
        <table id="table1">
            <tr>
                <th>
                    學習機構
                </th>
                <td>
                    <asp:Label ID="lab_mechani" runat="server"></asp:Label>
                </td>
                <th>
                    課程代碼
                </th>
                <td>
                    <asp:Label ID="lab_code" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    課程類別
                </th>
                <td>
                    <asp:Label ID="lab_typ_name" runat="server"></asp:Label>
                </td>
                <th>
                    課程名稱(期別)
                </th>
                <td>
                    <asp:Label ID="lab_name_flag" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    部門人數限制
                </th>
                <td colspan="3">
                    部門&nbsp;
                    <uc2:DepartTreeListBox ID="DepartTreeListBox1" runat="server" />
                    &nbsp;&nbsp;
                    限制人數&nbsp;<asp:TextBox ID="tbox_people" runat="server" MaxLength="3" 
                        Width="50px"></asp:TextBox>
                    &nbsp;

                    <asp:Button ID="Button2" runat="server" Text="加入" onclick="Button2_Click" CssClass="b-input" />
                </td>
            </tr>
            <tr>
                <th>
                    &nbsp;
                </th>
                <td colspan="3">
                    <div>
                        <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False"
                            Width="100%" CellPadding="3" CellSpacing="3" GridLines="None" OnRowCommand="GridView1_RowCommand"
                            EmptyDataText="查無資料" DataKeyNames="e03_no">
                            <Columns>
                                <asp:TemplateField HeaderText="部門">
                                    <ItemTemplate>
                                        <div>
                                            <%# new UtilityDAO().Get_DepartmentName((int)Eval("e03_depno"))%></div>
                                    </ItemTemplate>
                                    <ItemStyle Width="70%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="人數">
                                    <ItemTemplate>
                                        <div>
                                            <%# Eval("e03_people")%></div>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="刪除">
                                    <ItemTemplate>
                                        <asp:Button ID="Button1" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                            CommandName="del" OnClientClick=" return confirm('確定要刪除?')" CssClass="delete" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                            </Columns>
                        </cc1:GridView>
                    </div>
                </td>
            </tr>
        </table>
        <div class="bottom">
            <asp:Button ID="btn_cancel" runat="server" CssClass="b-input" Text="回課程管理" OnClick="btn_cancel_Click" />
        </div>
    <div>
    </div>
        <asp:HiddenField ID="hidd_data" runat="server" />
    </div>
</asp:Content>

