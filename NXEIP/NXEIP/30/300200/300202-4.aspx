<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300202-4.aspx.cs" Inherits="_30_300200_300202_4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls" TagPrefix="cc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectCountMethod="GetAllCount"
        SelectMethod="GetAll" TypeName="NXEIP.DAO.BotanizeDAO" OldValuesParameterFormatString="original_{0}"
        EnablePaging="True">
        <SelectParameters>
            <asp:Parameter Name="que_no" Type="Int32" />
            <asp:Parameter Name="jobtype" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300202" />
    <div class="tableDiv">
        <table>
            <tbody>
                <tr>
                    <th style="width: 120px">問卷名稱</th>
                    <td>
                        <asp:Label ID="lab_name" runat="server"></asp:Label>
                        <asp:Label ID="lab_no" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>問卷說明</th>
                    <td>
                        <asp:Label ID="lab_descript" runat="server"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <div class="header">
            <div class="h1"></div>
            <div class="h2"><div class="name">未填寫者</div></div>
            <div class="h3"></div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    EmptyDataText="查無資料" DataKeyNames="peo_uid"
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="dep_name" HeaderText="單位" SortExpression="dep_name" />
                        <asp:BoundField DataField="pro_name" HeaderText="職稱" SortExpression="pro_name" />
                        <asp:BoundField DataField="peo_name" HeaderText="姓名" SortExpression="peo_name"></asp:BoundField>
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
        <div class="bottom">
            <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="回上一頁" onclick="btn_submit_Click" />
        </div>
    </div>
</asp:Content>