<%@ Page Language="C#" AutoEventWireup="true" CodeFile="350203-1.aspx.cs" Inherits="_35_350200_350203_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="DepartmentDataSourceDDL" runat="server" SelectMethod="GetAll"
        TypeName="NXEIP.DAO.DepartmentsDAO"></asp:ObjectDataSource>
    <asp:HiddenField ID="hidden_dep_no" runat="server" />
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="350203" />
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
            <tbody>
                <tr>
                    <th>
                        上層部門
                    </th>
                    <td>
                        <asp:DropDownList ID="ddl_depart" runat="server" DataSourceID="DepartmentDataSourceDDL"
                            DataTextField="dep_name" DataValueField="dep_no">
                        </asp:DropDownList>
                        <br />
                    </td>
                    <th>
                        部門代號
                    </th>
                    <td>
                        <asp:TextBox ID="tbx_dep_code" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        部門中文名稱
                    </th>
                    <td>
                        <asp:TextBox ID="tbx_dep_name" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        部門英文名稱
                    </th>
                    <td>
                        <asp:TextBox ID="tbx_dep_ename" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        電話
                    </th>
                    <td>
                        <asp:TextBox ID="tbx_dep_tel" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        傳真
                    </th>
                    <td>
                        <asp:TextBox ID="tbx_dep_fax" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        地址
                    </th>
                    <td>
                        <asp:TextBox ID="tbx_dep_addr" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        排序編號
                    </th>
                    <td>
                        <asp:TextBox ID="tbx_dep_order" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        部門預設角色
                    </th>
                    <td colspan="3">
                        <asp:DropDownList ID="ddl_role" runat="server" DataSourceID="SqlDataSource1" DataTextField="rol_name"
                            DataValueField="rol_no">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>"
                            SelectCommand="SELECT rol_no, rol_name FROM role"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <th>
                        簡介
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="tbx_dep_note" runat="server" Height="100px" TextMode="MultiLine"
                            Width="370px"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
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
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" OnClick="btn_ok_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="False" />
        </div>
    </div>
    </form>
</body>
</html>
