<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350105-1.aspx.cs" Inherits="_35_350100_350105_1" %>

<%@ Register Src="../../lib/navigator.ascx" TagName="navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAll_2"
        TypeName="NXEIP.DAO.SysDAO"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
        SelectMethod="GetSubBySysNo" TypeName="NXEIP.DAO.SysfuctionDAO">
        <SelectParameters>
            <asp:Parameter Name="sys_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:navigator ID="navigator1" runat="server" SysFuncNo="350105" />
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
                <th>
                    系統分類
                </th>
                <td>
                    <asp:DropDownList ID="ddl_sys" runat="server" DataSourceID="ObjectDataSource1" DataTextField="sys_name"
                        DataValueField="sys_no" AutoPostBack="True" 
                        onselectedindexchanged="ddl_sys_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <th>
                    系統子分類
                </th>
                <td>
                    <asp:DropDownList ID="ddl_sysfuction" runat="server" 
                        DataSourceID="ObjectDataSource2" DataTextField="sfu_name" 
                        DataValueField="sfu_no">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    系統編號
                </th>
                <td>
                    <asp:TextBox ID="tbox_sfuNo" runat="server"></asp:TextBox>
                    <asp:Label ID="lab_sfuNo" runat="server"></asp:Label>
                    <span id="span_1" class="a-letter-Red" runat="server">新增後不可更改</span>
                </td>
                <th>
                    系統名稱
                </th>
                <td>
                    <asp:TextBox ID="tbox_name" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    排列順序
                </th>
                <td>
                    <asp:TextBox ID="tbox_order" runat="server"></asp:TextBox>
                </td>
                <th>
                    系統狀態
                </th>
                <td>
                    <asp:RadioButtonList ID="rbl_status" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">上架</asp:ListItem>
                        <asp:ListItem Value="2">下架</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>
                    系統路徑
                </th>
                <td colspan="3">
                    <asp:TextBox ID="tbox_path" runat="server" Width="330px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="footer">
            <div class="f1">
            </div>
            <div class="f2">
            </div>
            <div class="f3">
            </div>
        </div>
        <div class="bottom">
            <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="確定" OnClick="Button1_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" CssClass="a-input" Text="取消" OnClick="Button2_Click" />
        </div>
        <div id="div_msg" runat="server">
        </div>
    </div>
</asp:Content>
